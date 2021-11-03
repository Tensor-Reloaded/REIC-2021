using System;

namespace MailSystem
{
    class Recepient
    {
        private String _name = "Alex";
        private String _emailAddress = "alecsx344@gmail.com";

        public Recepient(String name, String emailAddress)
        {
            _name = name;
            _emailAddress = emailAddress;
        }

        public String getEmailAddress()
        {
            return _emailAddress;
        }

        public void setEmailAddress(String emailAddress)
        {
            _emailAddress = emailAddress;
        }

        public void setName(String name)
        {
            _name = name;
        }
    }
}
