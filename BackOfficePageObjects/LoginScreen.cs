﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace BackOfficeAutomation.pageObjects
{
    public class LoginScreen : BasePage
    {
        [FindsBy(How = How.Id, Using = "back_office_user_email")]
        private IWebElement txtuserName;

        [FindsBy(How = How.Id, Using = "back_office_user_password")]
        private IWebElement txtPassword;

        [FindsBy(How = How.XPath, Using = "//input[@value='Log In']")]
        private IWebElement btnLogin;

        public LoginScreen(IWebDriver driver)
        {
            BasePage.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(10)));
        }

        public HomeScreen navigateHome(string userName, string password)
        {
            SendKeys(txtuserName, userName);
            SendKeys(txtPassword, password);
            Click(btnLogin);
            return new HomeScreen(driver);
        }

    }
}
