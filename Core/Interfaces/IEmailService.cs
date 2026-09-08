namespace MyEventApi.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendPaymentConfirmation(string email, string name, string eventTitle, string eventDate, string eventLocation, string registrationId, decimal eventPrice);
    }
}
