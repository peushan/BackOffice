using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace BackOfficeAutomation.pageObjects
{
    public class PaymentTransactionScreen:BasePage
    {

        [FindsBy(How = How.XPath, Using = "//a[@title='Settlements']/i[@class='fa fa-check']")]
        private IWebElement tabSettelment;

        [FindsBy(How = How.XPath, Using = "//a[@rel='beneficiary_payments']")]
        private IWebElement LnkRecipientPayments;

        [FindsBy(How = How.XPath, Using = "//i[@class='icon-download']")]
        private IWebElement btnBackTransactin;



        By ContractDownloadicon = By.XPath("//i[@class='icon-download']");
       

        public PaymentTransactionScreen(IWebDriver driver)
        {
            BasePage.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(30)));
        }

        public RecipientPaymentScreen GotoSettlements()
        {
            WaitforVisibility(ContractDownloadicon, "Transaction / Contract download Page is not Visible");
            Click(tabSettelment);
            Click(LnkRecipientPayments);
            return new RecipientPaymentScreen(driver);
        }

        public bool VerifyTransactionPage()
        {
            bool displaysuccess = false;
            if (btnBackTransactin.Displayed)
            {
                displaysuccess = true;
            }
            else
            {
                displaysuccess = false;
            }

            return displaysuccess;
        }


        

    }
}
