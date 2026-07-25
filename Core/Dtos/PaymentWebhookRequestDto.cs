using System.Text.Json.Serialization;

namespace MyEventApi.Core.Dtos
{
    public class PaymentWebhookRequestDto
    {
        [JsonPropertyName("order_nsu")]
        public string OrderNsu { get; set; } = string.Empty;

        [JsonPropertyName("transaction_nsu")]
        public string TransactionNsu { get; set; } = string.Empty;

        [JsonPropertyName("paid_amount")]
        public int PaidAmount { get; set; }

        [JsonPropertyName("capture_method")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("receipt_url")]
        public string ReceiptUrl { get; set; } = string.Empty;

        [JsonPropertyName("invoice_slug")]
        public string InvoiceSlug { get; set; } = string.Empty;
    }
}
