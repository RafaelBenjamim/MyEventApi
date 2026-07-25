using MyEventApi.Core.Enums;

namespace MyEventApi.Core.Dtos
{
    public class UpdatePaymentSettingsRequest
    {
        public EPaymentProvider PaymentProvider { get; set; }
        public string? InfinitePayHandle { get; set; }
        public string? PagBankAppKey { get; set; }
        public string? PagBankAppId { get; set; }
    }
}
