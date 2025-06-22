using SimpleBookStore.Service.IService;
using System.Text;
using System.Text.Json;

namespace SimpleBookStore.Service
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly string _senderMail;
        private readonly string _apiKey;

        public EmailService( HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _senderMail = configuration["EmailSettings:SenderMail"]!;
            _apiKey = configuration["EmailSettings:ApiKey"]!;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("api-key", _apiKey);

            var payload = new
            {
                sender = new { email = _senderMail },
                to = new[] { new { email = toEmail} },
                subject = subject,
                htmlContent = body
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.brevo.com/v3/smtp/email", content);
            var result = await response.Content.ReadAsStringAsync();
        }
    }
}
