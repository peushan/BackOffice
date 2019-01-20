using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace BackOfficeAutomation.pageObjects
{
    public class QuoteScreen : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//*[@id='counter_currency_chosen']")]
        private IWebElement drpCurrencyWant;

        [FindsBy(How = How.XPath, Using = "//*[@id='counter_currency_chosen']/div/div/input")]
        private IWebElement textCurrencytype;

        [FindsBy(How = How.XPath, Using = "//input[@class='form-control input-lg toggle-acpt-transaction-btn transaction_currency_field']")]
        private IWebElement textCurrencyAmountWant;

        [FindsBy(How = How.XPath, Using = "//div[@id = 'new_transaction_transaction_direct_debit_attributes_customer_bank_account_id_chosen']")]
        private IWebElement drpSelectCoreAccount;

        [FindsBy(How = How.XPath, Using = "//input[@id='new_transaction_base_currency_amount']")]
        private IWebElement textBaseCurrency;

        [FindsBy(How = How.XPath, Using = "//a[@class='btn btn-primary btn-block btn-success accept-transaction']")]
        private IWebElement btnProcess;

        [FindsBy(How = How.XPath, Using = "//a[@id='show_processing_popup']")]
        private IWebElement btnSubmitTransaction;

        [FindsBy(How = How.XPath, Using = "//input[@id='keyword']")]
        private IWebElement txtCustomername;


        By AccountNumber = By.XPath("//div[@id='account_number']");
        

        public QuoteScreen(IWebDriver driver)
        {
            BasePage.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(30)));
        }


        public void SelectCurrency(string Currency, string Amount, string Account)
        {
            Click(drpCurrencyWant);
            SendKeys(textCurrencytype, Currency);
            textCurrencytype.SendKeys(Keys.Enter);
            SendKeys(textCurrencyAmountWant, Amount);
            Click(textBaseCurrency);
            try
            {
                Click(drpSelectCoreAccount);
                By textSelectAccount = By.XPath("//ul[@class='chosen-results']/li[@title='" + Account + "']");
                IWebElement textSelectAccount1 = driver.FindElement(textSelectAccount);
                textSelectAccount1.Click();
                WaitforVisibility(AccountNumber);
                Sleep(4);
                Click(btnProcess);
            }
            catch (WebDriverException e)
            {
                Sleep(8);
                Click(btnProcess);         
            }

        }


        public bool VerifyQuotes(string CustomerName)
        {
            bool displaysuccess = false;
            if (txtCustomername.GetAttribute("value").Contains(CustomerName))
            {
                displaysuccess = true;
            }
            else
            {
                displaysuccess = false;
            }

            return displaysuccess;
        }


        public PaymentScreen ReviewTrnsaction()
        {
            Click(btnSubmitTransaction);
            return new PaymentScreen(driver);
        }
    }
}