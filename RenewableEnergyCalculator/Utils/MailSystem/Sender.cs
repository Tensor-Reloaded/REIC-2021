using System;

namespace MailSystem
{
    public sealed class Sender
    {

        private static Sender _instance;

        private string _name = "REIC Team";
        private string _emailAddress = "barbugalexandru@gmail.com";
        private string _password = "";

        private Sender() {
            _password = System.IO.File.ReadAllText(@"C:\Users\shank\source\repos\MailSystem\password.txt");
        }

        public static Sender GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Sender();
            }
            return _instance;
        }

        public String getName()
        {
            return _name;
        }

        public String getEmailAddress()
        {
            return _emailAddress;
        }

        public String getPassword()
        {
            return _password;
        }

    }
}
