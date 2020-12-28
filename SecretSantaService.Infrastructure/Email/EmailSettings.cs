namespace SecretSantaService.Infrastructure.Email
{
    public class EmailSettings
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string FromAddress { get; set; }
    }
}
