using MyEventApi.Core.Entity;
using MyEventApi.Core.Interfaces;
using Resend;
using System.Xml.Linq;

namespace MyEventApi.Infrastructure.Services
{
    public class ResendEmailService : IEmailService
    {
        private readonly IResend _resend;
        private readonly string _fromEmail;
        private readonly IEmailLogRepository _emailLogRepository;

        public ResendEmailService(IResend resend, IConfiguration configuration, IEmailLogRepository emailLogRepository)
        {
            _resend = resend;
            _fromEmail = configuration["Resend:FromEmail"];
            _emailLogRepository = emailLogRepository;
        }
        public async Task SendPaymentConfirmation(string email, string name, string eventTitle, string eventDate, string eventLocation, string registrationId, decimal eventPrice)
        {
            try
            {
                var log = new EmailLogEntity
                {
                    Id = Guid.NewGuid(),
                    RegistrationId = Guid.Parse(registrationId),
                    Email = email,
                    Subject = "Confirmação de vaga",
                    Sent = DateTime.UtcNow
                };

               var message = new EmailMessage
                {
                    From = _fromEmail,
                    To = { email },
                    Subject = "Sua vaga está confirmada! 🌸 Fiorella Club",
                    HtmlBody = $"""
                    <!DOCTYPE html>
                    <html lang="pt-BR">
                    <head>
                        <meta charset="UTF-8">
                        <meta name="viewport" content="width=device-width, initial-scale=1.0">
                    </head>
                    <body style="margin: 0; padding: 0; background-color: #f5e6e8; font-family: Georgia, serif;">
                        <div style="max-width: 600px; margin: 0 auto; padding: 40px 20px;">
                            <!-- Header -->
                            <div style="text-align: center; margin-bottom: 32px;">
                                <h1 style="color: #4a0b16; font-size: 36px; margin: 0; letter-spacing: 2px;">
                                    Fiorella Club
                                </h1>
                                <p style="color: #c07a82; font-size: 14px; margin: 4px 0 0; letter-spacing: 4px; text-transform: uppercase;">
                                    Criando momentos, conexões e arte
                                </p>
                            </div>

                            <!-- Card principal -->
                            <div style="background: white; border-radius: 24px; overflow: hidden; box-shadow: 0 4px 24px rgba(74, 11, 22, 0.08);">
                
                                <!-- Banner -->
                                <div style="background: linear-gradient(135deg, #4a0b16, #940c0c); padding: 40px 32px; text-align: center;">
                                    <div style="font-size: 48px; margin-bottom: 12px;">🌸</div>
                                    <h2 style="color: white; font-size: 28px; margin: 0 0 8px;">
                                        Vaga Confirmada!
                                    </h2>
                                    <p style="color: rgba(255,255,255,0.85); font-size: 16px; margin: 0;">
                                        Olá, {name}! Estamos te esperando 🎉
                                    </p>
                                </div>

                                <!-- Conteúdo -->
                                <div style="padding: 32px;">
                                    <p style="color: #4a0b16; font-size: 16px; line-height: 1.8; margin: 0 0 24px; text-align: center;">
                                        Seu pagamento foi confirmado com sucesso! 
                                        Sua vaga no encontro está garantida. Mal podemos esperar para te receber!
                                    </p>

                                    <!-- Detalhes do evento -->
                                    <div style="background: #fce3e4; border-radius: 16px; padding: 24px; margin-bottom: 24px;">
                                        <h3 style="color: #4a0b16; font-size: 20px; margin: 0 0 16px; text-align: center;">
                                            {eventTitle}
                                        </h3>
                                        <table style="width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td style="padding: 10px 0; color: #4a0b16; font-size: 15px;">
                                                    📅 <strong>Data</strong>
                                                </td>
                                                <td style="padding: 10px 0; color: #940c0c; font-size: 15px; text-align: right;">
                                                    {eventDate}
                                                </td>
                                            </tr>
                                            <tr style="border-top: 1px solid rgba(74,11,22,0.1);">
                                                <td style="padding: 10px 0; color: #4a0b16; font-size: 15px;">
                                                    📍 <strong>Local</strong>
                                                </td>
                                                <td style="padding: 10px 0; color: #940c0c; font-size: 15px; text-align: right;">
                                                    {eventLocation}
                                                </td>
                                            </tr>
                                            <tr style="border-top: 1px solid rgba(74,11,22,0.1);">
                                                <td style="padding: 10px 0; color: #4a0b16; font-size: 15px;">
                                                    💎 <strong>Valor pago</strong>
                                                </td>
                                                <td style="padding: 10px 0; color: #940c0c; font-size: 15px; text-align: right;">
                                                    {eventPrice.ToString("C", new System.Globalization.CultureInfo("pt-BR"))}
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <!-- O que levar -->
                                    <div style="border: 1px solid #fce3e4; border-radius: 16px; padding: 24px; margin-bottom: 24px;">
                                        <h4 style="color: #4a0b16; font-size: 16px; margin: 0 0 16px;">
                                            💡 O que você precisa saber
                                        </h4>
                                        <ul style="color: #940c0c; font-size: 14px; line-height: 1.8; margin: 0; padding-left: 20px;">
                                            <li style="margin-bottom: 8px;">Chegue com 10 minutinhos de antecedência.</li>
                                            <li style="margin-bottom: 8px;">Vista roupas confortáveis.</li>
                                            <li>Traga sua energia e vontade de criar!</li>
                                        </ul>
                                    </div>

                                    <!-- Lembrete de Cancelamento (NOVO) -->
                                    <div style="background: rgba(148, 12, 12, 0.05); border-radius: 12px; padding: 16px; margin-bottom: 24px; text-align: center;">
                                        <p style="color: #940c0c; font-size: 13px; line-height: 1.6; margin: 0;">
                                            <strong>Lembrete importante:</strong> Como preparamos os materiais com carinho e exclusividade para você, não realizamos reembolsos. Transferências de titularidade podem ser feitas avisando a nossa equipe com até 48h de antecedência do encontro.
                                        </p>
                                    </div>

                                    <!-- Código de confirmação -->
                                    <div style="background: #f9f9f9; border-radius: 12px; padding: 16px; text-align: center; margin-bottom: 24px;">
                                        <p style="color: #c07a82; font-size: 11px; letter-spacing: 3px; text-transform: uppercase; margin: 0 0 6px;">
                                            Código de confirmação
                                        </p>
                                        <p style="color: #4a0b16; font-size: 13px; font-family: monospace; margin: 0; opacity: 0.6; word-break: break-all;">
                                            {registrationId}
                                        </p>
                                    </div>

                                    <p style="color: #4a0b16; font-size: 15px; line-height: 1.8; margin: 0; text-align: center;">
                                        Dúvidas? Fale com a gente pelo 
                                        <a href="https://wa.me/553499732-9304" style="color: #940c0c; text-decoration: none; font-weight: bold;">
                                            WhatsApp
                                        </a> 
                                        ou pelo 
                                        <a href="https://instagram.com/fiorellaclub_" style="color: #940c0c; text-decoration: none; font-weight: bold;">
                                            Instagram
                                        </a>
                                    </p>
                                </div>

                                <!-- Footer do card -->
                                <div style="background: #4a0b16; padding: 24px 32px; text-align: center;">
                                    <p style="color: rgba(252, 227, 228, 0.9); font-size: 18px; margin: 0 0 8px;">
                                        Nos vemos em breve! 🌸
                                    </p>
                                    <p style="color: rgba(252, 227, 228, 0.5); font-size: 12px; margin: 0; letter-spacing: 2px; text-transform: uppercase;">
                                        Fiorella Club
                                    </p>
                                </div>
                            </div>

                            <!-- Footer do email -->
                            <div style="text-align: center; margin-top: 24px;">
                                <p style="color: #c07a82; font-size: 12px; margin: 0;">
                                    © {DateTime.Now.Year} Fiorella Club. Todos os direitos reservados.
                                </p>
                            </div>
                        </div>
                    </body>
                    </html>
                    """
                };

                await _resend.EmailSendAsync(message);

                log.Status = "sent";
                await _emailLogRepository.SaveLogEmail(log);
            }
            catch(Exception ex)
            {
                var log = new EmailLogEntity
                {
                    Id = Guid.NewGuid(),
                    RegistrationId = Guid.Parse(registrationId),
                    Email = email,
                    Subject = "Confirmação de vaga",
                    Sent = DateTime.UtcNow,
                    Status = "failed",
                    ErrorMessage = ex.Message
                };
                await _emailLogRepository.SaveLogEmail(log);
                throw;
            };
        }
    }
}
