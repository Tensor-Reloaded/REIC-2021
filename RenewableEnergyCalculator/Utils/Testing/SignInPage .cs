using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo1
{
    public class SignInPage
    {
        protected WebDriver driver;

     
        private By usernameBy = By.Name("user_name");
        private By passwordBy = By.Name("password");
        private By signinBy = By.Name("sign_in");

        public SignInPage(WebDriver driver)
        {
            this.driver = driver;
        }

        public HomePage LoginValidUser(String userName, String password)
        {
            driver.FindElement(usernameBy).SendKeys(userName);
            driver.FindElement(passwordBy).SendKeys(password);
            driver.FindElement(signinBy).Click();
            return new HomePage(driver);
        }
    }
   
   
}
