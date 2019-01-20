using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace BackOfficeAutomation.pageObjects
{
    public class CustomersScreen : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//ul[@class='breadcrumb no-margin pull-left text-uppercase']")]
        private IWebElement txtHeaderCustomer;

        [FindsBy(How = How.XPath, Using = "//a[@class='btn btn-sm btn-primary ']")]
        private IWebElement btnAddCustomerAccount;

        [FindsBy(How = How.XPath, Using = "//input[@id='integrated_customer_external_uid']")]
        private IWebElement txtKBaccessnumber;

        [FindsBy(How = How.XPath, Using = "//input[@class='btn btn-sm btn-success']")]
        private IWebElement btnContinue;

        [FindsBy(How = How.XPath, Using = "//a[@title='Log Out']")]
        private IWebElement btnLogout;

        [FindsBy(How = How.XPath, Using = "//ul[@class='breadcrumb no-margin pull-left text-uppercase']/li")]
        private IWebElement txtCustomerHeader;

        [FindsBy(How = How.XPath, Using = "//button[@class='close']")]
        private IWebElement btnClose;

        [FindsBy(How = How.XPath, Using = "//div[@class='container-fluid']/em")]
        private IWebElement txtSuccessMsg;

        [FindsBy(How = How.XPath, Using = "//div[@id='column_chosen']")]
        private IWebElement txtSelectSearch;

        [FindsBy(How = How.XPath, Using = "//div[@class='chosen-drop']/ul[@class='chosen-results']/li[@title='KB Access Number']")]
        private IWebElement txtSelectKBAccessnumber;

        [FindsBy(How = How.XPath, Using = "//input[@id='keyword']")]
        private IWebElement txtSearchField;

        [FindsBy(How = How.XPath, Using = "//i[@class='icon-search']")]
        private IWebElement btnSearch;

        [FindsBy(How = How.XPath, Using = "//tbody/tr/td[1]/a")]
        private IWebElement lnkFirstRecord;

        [FindsBy(How = How.XPath, Using = "//tr[@data-index='0']/td[2]")]
        private IWebElement tableRecord;

        By lablCreateAccount = By.XPath("//h3[@class='text-primary']");
        By lablCustomerDetails = By.XPath("//h3[@class='text-primary']");
        By btnImportcustomer = By.XPath("//a[@class='btn btn-sm btn-primary ']");
        By btnNewQuote = By.XPath("//a[@class='btn btn-sm btn-primary new_quote']");
        By btnsignOut = By.XPath("//a[@title='Log Out']");

       

        public CustomersScreen(IWebDriver driver)
        {
            BasePage.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(30)));
        }

        public bool VerifyCustomerScreen()
        {
            bool displaysuccess = false;
            if (txtCustomerHeader.Text.Equals("CUSTOMERS"))
            {
                displaysuccess = true;              
                btnAddCustomerAccount.Click();
            }
            else
            {
                displaysuccess = false;
            }
           
            return displaysuccess;
        }

        public CustomersDetailsScreen navigateCustomerRegistration(string AccessNumber)
        {
            WaitforVisibility(lablCreateAccount);
            SendKeys(txtKBaccessnumber, AccessNumber);
            Click(btnContinue);
            Sleep(7);
            WaitforVisibility(lablCustomerDetails);
            return new CustomersDetailsScreen(driver);
        }

        public bool VerifyCustomerSaved()
        {
            bool displaysuccess = false;
            if (txtSuccessMsg.Text.Equals("Account Details was added successfully."))
            {
               
                displaysuccess = true;
                Click(btnClose);
            }
            else
            {
                displaysuccess = false;
            }
            return displaysuccess;
        }

        public void SelectSearchCritirea()
        {
            Click(txtSelectSearch);
            Click(txtSelectKBAccessnumber);
        }

        public void SearchCustomer(string KBAccessnumber)
        {
            SendKeys(txtSearchField, KBAccessnumber);
            Click(btnSearch);
            Sleep(2);
        }

        public bool VerifyCustomerSearched(string KBAccessnumber)
        {
            bool displaysuccess = false;
            if (tableRecord.Text.Equals(KBAccessnumber))
            {
                displaysuccess = true;
            }
            else
            {
                displaysuccess = false;
            }
         
            return displaysuccess;
        }

        public CustomersDetailsScreen SelectCustomer()
        {
            Click(lnkFirstRecord);
            return new CustomersDetailsScreen(driver);
        }
    }
}