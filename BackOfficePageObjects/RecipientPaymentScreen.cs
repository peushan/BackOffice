using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BackOfficeAutomation.pageObjects
{
    public class RecipientPaymentScreen:BasePage
    {

        
        [FindsBy(How = How.XPath, Using = "//div[@class='chosen-drop']/ul[@class='chosen-results']/li[@title='Recipient']")]
        private IWebElement dropSelectionCritiria;

        [FindsBy(How = How.XPath, Using = "//input[@id='keyword']")]
        private IWebElement txtSearchField;

        [FindsBy(How = How.XPath, Using = "//div[@class='release_payments_btns hide']/a[@class='btn btn-sm btn-success']")]
        private IWebElement btnReleasePayment;

        [FindsBy(How = How.XPath, Using = "//div[@id='column_chosen']")]
        private IWebElement txtSelectSearch;

        [FindsBy(How = How.XPath, Using = "//i[@class='icon-search']")]
        private IWebElement btnSearch;


        [FindsBy(How = How.XPath, Using = "//div[@class='col-sm-4']/div/div[@class='panel-body']/div/div")]
        private IWebElement panelText;

        public RecipientPaymentScreen(IWebDriver driver)
        {
            BasePage.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(30)));
        }


        public void SelectRecipient(string RecipientName)
        {
            Click(txtSelectSearch);
            Click(dropSelectionCritiria);
            SendKeys(txtSearchField, RecipientName);
            Click(btnSearch);
            Click(btnReleasePayment);
        }


    }
}