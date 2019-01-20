using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace BackOfficeAutomation.pageObjects
{
    public class HomeScreen : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//i[@class='fa fa-users']")]
        private IWebElement lnkCustomers;

        [FindsBy(How = How.XPath, Using = "//a[@rel='clients_information']")]
        private IWebElement tabCustomer;

        [FindsBy(How = How.XPath, Using = "//button[@class='close']")]
        private IWebElement btnClose;

        [FindsBy(How = How.XPath, Using = "//div[@class='container-fluid']/em")]
        private IWebElement txtSuccessMsg;

        [FindsBy(How = How.XPath, Using = "//a[@title='Log Out']")]
        private IWebElement btnLogout;

        By tabCustomers = By.XPath("//a[@rel='clients_information']");
        By btnsignOut = By.XPath("//a[@title='Log Out']");

        
        public HomeScreen(IWebDriver driver)
        {
            BasePage.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(30)));
        }

        public void NavigateCustomerTab()
        {
            lnkCustomers.Click();
            WaitforVisibility(tabCustomers);
           
        }

        public CustomersScreen NavigateCustomer()
        {
            tabCustomer.Click();
            return new CustomersScreen(driver);
        }

        public bool VerifySignInSuccess()
        {
            bool displaySuccess = false;
            if (txtSuccessMsg.Text.Equals("Signed in successfully."))
            {
                displaySuccess = true;
                Click(btnClose);
              
            }
            else
            {
                displaySuccess = false;
            }
            return displaySuccess;

        }
    }
}
