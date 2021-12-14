using MailKit.Net.Smtp;

namespace RenewableEnergyCalculator.MailSystem
{
    public class SMTPClient
    {
        // name of the smtp provider
        private readonly string name = "smtp.gmail.com";
        // port for secure SMTP
        private readonly int port = 465;
        // option when connecting to the mail server
        private readonly bool enableSSL = true;
        // the mail server
        public SmtpClient server = new SmtpClient();

        public string GetName()
        {
            return name;
        }

        public int GetPort()
        {
            return port;
        }

        public bool GetEnableSSL()
        {
            return enableSSL;
        }
    }
}