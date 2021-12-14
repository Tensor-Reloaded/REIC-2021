namespace RenewableEnergyCalculator.MailSystem
{
    public class Recepient
    {
        // default name of the recipient
        private string _name = "Alex";
        // default address of the recipient
        private string _emailAddress = "barbugalexandru@gmail.com";

        public Recepient(string name, string emailAddress)
        {
            _name = name;
            _emailAddress = emailAddress;
        }

        public string GetEmailAddress()
        {
            return _emailAddress;
        }

        public void SetEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
    }
}