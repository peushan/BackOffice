using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace BackOfficeAutomation.pageObjects
{
    public class CustomersDetailsScreen : BasePage
    {

        [FindsBy(How = How.XPath, Using = "//input[@name='commit']")]
        private IWebElement btnSave;

        [FindsBy(How = How.XPath, Using = "//input[@id='customer_users_attributes_0_email']")]
        private IWebElement txtEmail;

        [FindsBy(How = How.XPath, Using = "//input[@id='customer_source']")]
        private IWebElement txtBankEntity;

        [FindsBy(How = How.XPath, Using = "//input[@id='customer_external_account_type_attributes_value']")]
        private IWebElement txtAccountType;

        [FindsBy(How = How.XPath, Using = "//div[@id='primary-slider']/div[2]/span[7]/span")]
        private IWebElement lnkFCATab;
        
        [FindsBy(How = How.XPath, Using = "//a[@class='btn btn-sm btn-primary ']")]
        private IWebElement btnCreateNewFCA;

        [FindsBy(How = How.XPath, Using = "//input[@class='company_name_txt form-control input-sm']")]
        private IWebElement txtStoredAccountName;

        [FindsBy(How = How.XPath, Using = "//input[@class='form-control input-sm users_email_txt']")]
        private IWebElement txtStoredEmail;

        [FindsBy(How = How.XPath, Using = "//input[@class='address_address_line_1_txt form-control input-sm']")]
        private IWebElement txtStoredStreetAddreess;

        [FindsBy(How = How.XPath, Using = "//input[@class='address_address_line_2_txt form-control input-sm']")]
        private IWebElement txtStoredAddressline2;

        [FindsBy(How = How.XPath, Using = "//input[@class='address_suburb_txt form-control input-sm']")]
        private IWebElement txtStoredSuburb;

        [FindsBy(How = How.XPath, Using = "//input[@class='address_city_txt form-control input-sm']")]
        private IWebElement txtStoredCity;

        [FindsBy(How = How.XPath, Using = "//a[@class='btn btn-sm btn-primary new_quote']")]
        private IWebElement btnQuote;
        

        By btnSave1 = By.XPath("//input[@name='commit']");
        By lnkFCA = By.XPath("//div[@id='primary-slider']/div[2]/span[7]/span/a");


        public CustomersDetailsScreen(IWebDriver driver)
        {
            BasePage.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(30)));
        }

        public bool VerifyCustomerDetails(string BankEntity, string AccountType)
        {
            bool CustomerDetails;
           
            if (txtBankEntity.GetAttribute("value").Equals(BankEntity) && txtAccountType.GetAttribute("value").Equals(AccountType))
            {
                CustomerDetails = true;
            }
            else
            {
                CustomerDetails = false;
            }
            return CustomerDetails;
        }

        public bool VerifyCustomerDetailPage()
        {
            bool displaysuccess = false;
            if (btnQuote.Displayed)
            {
                displaysuccess = true;
            }
            else
            {
                displaysuccess = false;
            }

            return displaysuccess;

        }

        public CustomersScreen SavingData()
        {
            WaitforVisibility(btnSave1, "Customer Screen - Save Button is not visible");
            btnSave.Click();
            Sleep(5);
            return new CustomersScreen(driver);
        }

        public FCAaccountScreen NavigatetoFCA()
        {
            WaitforVisibility(lnkFCA, "Customer Details Screen : FCA link is not Visible");
            Click(lnkFCATab);
            return new FCAaccountScreen(driver);
        }

        public JObject DataStore()
        { 
            var dataobject = new JObject();
            dataobject.Add("Account Name", txtStoredAccountName.GetAttribute("value"));
            dataobject.Add("Email", txtStoredEmail.GetAttribute("value"));
            dataobject.Add("Street Address", txtStoredStreetAddreess.GetAttribute("value"));
            dataobject.Add("Address Line2", txtStoredAddressline2.GetAttribute("value"));
            dataobject.Add("Suburb", txtStoredSuburb.GetAttribute("value"));
            dataobject.Add("City", txtStoredCity.GetAttribute("value"));
            return dataobject;
        }

        public QuoteScreen navigateQuote()
        {
            Click(btnQuote);
            return new QuoteScreen(driver);
        }

    }
}