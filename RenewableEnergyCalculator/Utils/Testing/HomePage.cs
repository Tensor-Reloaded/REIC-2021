using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo1
{
    public class HomePage
    {
        protected WebDriver driver;

        private By messageBy = By.TagName("h1");

        public HomePage(WebDriver driver)
        {
            this.driver = driver;
            if (!driver.Title.Equals("Home Page of logged in user"))
            {
                throw new Exception("This is not Home Page of logged in user," + " current page is: " + driver.Url);
            }
        }

        public String getMessageText()
        {
            return driver.FindElement(messageBy).Text;
        }

        public HomePage manageProfile()
        {
            return new HomePage(driver);
        }
    }
}
