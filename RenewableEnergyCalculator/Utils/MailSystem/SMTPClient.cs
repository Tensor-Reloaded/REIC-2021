using System;
using MailKit.Net.Smtp;

namespace MailSystem
{
    class SMTPClient
    {
        private String name = "smtp.gmail.com";
        private int port = 465;
        private bool enableSSL = true;
        public SmtpClient server = new SmtpClient();

        public String getName()
        {
            return name;
        }

        public int getPort()
        {
            return port;
        }

        public bool getEnableSSL()
        {
            return enableSSL;
        }
    }
}
