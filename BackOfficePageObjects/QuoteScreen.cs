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

        [FindsBy(How = How.XPath, Using = "//a[@class='btn btn-primary btn-block btn-success accept-transaction submit-transaction']")]
        private IWebElement btnProcess;

        [FindsBy(How = How.XPath, Using = "//a[@id='show_processing_popup']")]
        private IWebElement btnSubmitTransaction;

        [FindsBy(How = How.XPath, Using = "//input[@id='keyword']")]
        private IWebElement txtCustomername;

        [FindsBy(How = How.XPath, Using = "//div[@id='account_number']")]
        private IWebElement txtAccountNumber;

        By AccountNumber = By.XPath("//div[@id='account_number']");
        By SubmitTransactionbtn = By.XPath("//a[@id='show_processing_popup']");
        By Processbtn = By.XPath("//a[@class='btn btn-primary btn-block btn-success accept-transaction submit-transaction']");

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
                WaitforVisibility(AccountNumber, "Quote Screen : Account number is not visible");
                Sleep(4);
                
            }
            catch (WebDriverException e)
            {
                Sleep(8);
                    
            }

        }

        public bool VerifyAccountNumberDisplayed(string AccountNumber)
        {
            bool displaysuccess = false;
            if (txtAccountNumber.Text.Contains(AccountNumber))
            {
                displaysuccess = true;
                WaitforVisibility(Processbtn, "Quote Screen : Trasaction Proceed button is not visible"); 
                Click(btnProcess);
            }
            else
            {
                displaysuccess = false;
            }

            return displaysuccess;

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
            WaitforVisibility(SubmitTransactionbtn,"Payment Transaction page is not visible");
            Click(btnSubmitTransaction);
            return new PaymentScreen(driver);
        }
    }
}