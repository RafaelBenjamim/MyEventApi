using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using MyEventApi.Core.Entities;
using MyEventApi.Core.Interfaces;
using MyEventApi.Core.Results;
using Microsoft.Extensions.Configuration;

namespace MyEventApi.Infrastructure.Services
{
    public class InfinitePayPaymentGateway : IPaymentGateway
    {
        private readonly HttpClient _httpClient;
        private readonly string _webhookUrl;
        private readonly string _frontUrl;

        public InfinitePayPaymentGateway(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _webhookUrl = configuration["InfinitePay:WebhookUrl"]
                ?? throw new InvalidOperationException("InfinitePay:WebhookUrl não configurado.");
            _frontUrl = configuration["InfinitePay:FrontUrl"]
                ?? throw new InvalidOperationException("InfinitePay:FrontUrl não configurado.");
        }

        public async Task<PaymentChargeResult> CreateCharge(
            StoreEntity store,
            decimal amount,
            string description,
            Guid orderId,
            string customerName,
            string customerEmail,
            string customerPhone)
        {
            if (string.IsNullOrEmpty(store.InfinitePayHandle))
                throw new InvalidOperationException("Esta loja não tem o InfiniteTag configurado.");

            var payload = new
            {
                handle = store.InfinitePayHandle,
                order_nsu = orderId.ToString(),
                items = new[]
                {
                    new
                    {
                        quantity = 1,
                        price = (int)(amount * 100),
                        description = description
                    }
                },
                webhook_url = _webhookUrl,
                redirect_url = $"{_frontUrl}/confirmacao/{orderId}",  
                customer = new
                {
                    name = customerName,
                    email = customerEmail,
                    phone_number = customerPhone
                }
            };

            var response = await _httpClient.PostAsJsonAsync("/links", payload);
            var rawJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"Erro ao criar cobrança InfinitePay: {rawJson}");

            var result = JsonSerializer.Deserialize<InfinitePayLinkResponse>(rawJson);

            if (result is null || string.IsNullOrEmpty(result.PaymentUrl))
                throw new InvalidOperationException("InfinitePay não retornou um link de pagamento válido.");

            return new PaymentChargeResult
            {
                PaymentUrl = result.PaymentUrl,
                ExternalId = orderId.ToString()
            };
        }
    }

    internal class InfinitePayLinkResponse
    {
        [JsonPropertyName("url")]
        public string PaymentUrl { get; set; } = string.Empty;
    }
}