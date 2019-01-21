using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BackOfficeAutomation.pageObjects
{
    public class PaymentScreen : BasePage
    {

        [FindsBy(How = How.XPath, Using = "//*[@id='transaction_beneficiary_transactions_attributes_0_beneficiary_id_chosen']/a")]
        private IWebElement drpRecipient;

        [FindsBy(How = How.XPath, Using = "//input[@name='transaction[beneficiary_transactions_attributes][0][amount]']")]
        private IWebElement textAmount;

        [FindsBy(How = How.XPath, Using = "//input[@name='commit']")]
        private IWebElement btnSubmit;

        [FindsBy(How = How.XPath, Using = "//div[@class='col-sm-12 space-20']/h3")]
        private IWebElement txtReferenceid;
        

       
        By RecipientHeader = By.XPath("//div[@class='panel-heading']/h4[contains(text(),'Recipients')]");
        By PanelVisibility = By.XPath("//div[@class='col-sm-4']/div/div[@class='panel-body']/div/div");

        public PaymentScreen(IWebDriver driver)
        {
            BasePage.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(30)));
        }

        public PaymentTransactionScreen SelectRecipient(string RecipientName, string PayAmount)
        {
            WaitforVisibility(RecipientHeader, "Recipient Selection Page is not visible");
            Click(drpRecipient);
            Sleep(3);
            By textSelectRecipient = By.XPath("//ul[@class='chosen-results']/li[@title='" + RecipientName + "']");
            IWebElement textSelectRecipient1 = driver.FindElement(textSelectRecipient);
            textSelectRecipient1.Click();
            SendKeys(textAmount, PayAmount);
            Sleep(3);
            Click(btnSubmit);

            return new PaymentTransactionScreen(driver);
        }

        JObject paneldata = new JObject();
        public bool VerifyPanel(string Currency, string Amount)
        {

            var payReference = txtReferenceid.Text.Replace("Payments for ", string.Empty);
            bool displaysuccess = false;
            WaitforVisibility(PanelVisibility, "Payment Screen : Panel data is not visible");
            ReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//div[@class='col-sm-4']/div/div[@class='panel-body']/div/div"));
            int i = 0;
            foreach (IWebElement row in rows)
            {  
                string panelValues = row.Text;
                {             
                    paneldata.Add(i.ToString(), panelValues);
                    i++;
                }

            }
            if(paneldata.GetValue("1").ToString().Contains("Buy Currency\r\n"+ Currency) && (paneldata.GetValue("2").ToString().Contains("Amount\r\n"+ Amount)))
            {
                displaysuccess = true;
            }
            else
            {
                displaysuccess = false;
            }
            return displaysuccess;
        }

        public NewRecipientScreen AddRecipient(string RecipientName)
        {
            Click(drpRecipient);
            Sleep(3);
            By textSelectRecipient = By.XPath("//ul[@class='chosen-results']/li[@title='" + RecipientName + "']");
            IWebElement textSelectRecipient1 = driver.FindElement(textSelectRecipient);
            textSelectRecipient1.Click();
            return new NewRecipientScreen(driver);
        }

    }
}