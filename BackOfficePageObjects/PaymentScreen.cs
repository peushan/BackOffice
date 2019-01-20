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

        [FindsBy(How = How.XPath, Using = "//a[@title='Settlements']/i[@class='fa fa-check']")]
        private IWebElement tabSettelment;

        [FindsBy(How = How.XPath, Using = "//a[@rel='beneficiary_payments']")]
        private IWebElement LnkRecipientPayments;

        By ContractDownloadicon = By.XPath("//i[@class='icon-download']");
        By RecipientHeader = By.XPath("//div[@class='panel-heading']/h4[contains(text(),'Recipients')]");
        By PanelVisibility = By.XPath("//div[@class='col-sm-4']/div/div[@class='panel-body']/div/div");

        public PaymentScreen(IWebDriver driver)
        {
            BasePage.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(30)));
        }

        public void SelectRecipient(string RecipientName, string PayAmount)
        {
            WaitforVisibility(RecipientHeader);
            Click(drpRecipient);
            Sleep(3);
            By textSelectRecipient = By.XPath("//ul[@class='chosen-results']/li[@title='" + RecipientName + "']");
            IWebElement textSelectRecipient1 = driver.FindElement(textSelectRecipient);
            textSelectRecipient1.Click();
            SendKeys(textAmount, PayAmount);
            Sleep(3);
            Click(btnSubmit);
        }

        JObject paneldata = new JObject();
        public JObject VerifyPanel()
        {
            WaitforVisibility(PanelVisibility);
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
            if(paneldata.GetValue("1").ToString().Contains("Buy Currency\r\nAUD"))
            {
                Console.WriteLine("2");
            }
            else
            {
                Console.WriteLine("3");
            }
            return paneldata;
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


        public RecipientPaymentScreen GotoSettlements()
        {
            WaitforVisibility(ContractDownloadicon);
            Click(tabSettelment);
            Click(LnkRecipientPayments);

            return new RecipientPaymentScreen(driver);
        }
    }
}