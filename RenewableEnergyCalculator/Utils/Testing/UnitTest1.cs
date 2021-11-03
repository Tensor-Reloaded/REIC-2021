using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SeleniumExtras;
using OpenQA.Selenium;

namespace TestDemo1
{
    [TestClass]
    public class UnitTest1
    {    protected WebDriver driver;

        [TestMethod]
        public void TestMethod1()
        {
            SignInPage signInPage = new SignInPage(driver);
            HomePage homePage = signInPage.LoginValidUser("userName", "password");
            Assert.IsTrue(homePage.getMessageText().Contains("Hello userName"));
        }

    }
}
