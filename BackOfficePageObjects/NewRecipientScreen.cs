using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace BackOfficeAutomation.pageObjects
{
    public class NewRecipientScreen:BasePage
    {
        [FindsBy(How = How.XPath, Using = "//*[@id='beneficiary_payment_detail_beneficiary_bank_detail_attributes_address_attributes_country_id_chosen']/a")]
        private IWebElement drpCountrysending;

        [FindsBy(How = How.XPath, Using = "//*[@id='beneficiary_payment_detail_beneficiary_bank_detail_attributes_address_attributes_country_id_chosen']/div/div/input")]
        private IWebElement txtCountrySend;

        [FindsBy(How = How.XPath, Using = "//input[@value='Next']")]
        private IWebElement btnNext;

        [FindsBy(How = How.XPath, Using = "//input[@class='address_first_name_txt form-control input-sm']")]
        private IWebElement txtFirstName;

        [FindsBy(How = How.XPath, Using = "//input[@class='address_last_name_txt form-control input-sm']")]
        private IWebElement txtLastName;

        [FindsBy(How = How.XPath, Using = "//input[@class='address_address_line_1_txt form-control input-sm']")]
        private IWebElement txtAddressline1;

        [FindsBy(How = How.XPath, Using = "//input[@class='address_address_line_2_txt form-control input-sm']")]
        private IWebElement txtAddressline2;

        [FindsBy(How = How.XPath, Using = "//input[@class='address_suburb_txt form-control input-sm']")]
        private IWebElement txtSuburb;

        [FindsBy(How = How.XPath, Using = "//input[@class='address_city_txt form-control input-sm']")]
        private IWebElement txtCity;

        [FindsBy(How = How.XPath, Using = "//div[@class='address_mobile_txt']/input")]
        private IWebElement txtMobileNumber;

        [FindsBy(How = How.XPath, Using = "//input[@class='beneficiary_bank_detail_name_txt form-control input-sm']")]
        private IWebElement txtBankname;

        [FindsBy(How = How.XPath, Using = "//input[@class='beneficiary_bank_detail_address_address_line_1_txt form-control input-sm']")]
        private IWebElement txtBankAddress1;

        [FindsBy(How = How.XPath, Using = "//input[@class='beneficiary_bank_detail_branch_name_txt form-control input-sm']")]
        private IWebElement txtBankBranch;

        [FindsBy(How = How.XPath, Using = "//input[@class='beneficiary_bank_detail_bank_code_txt form-control input-sm']")]
        private IWebElement txtBankCode;

        [FindsBy(How = How.XPath, Using = "//input[@class='text beneficiary_bank_detail_swift_code_txt  form-control input-sm']")]
        private IWebElement txtSwift;

        [FindsBy(How = How.XPath, Using = "//div[@class='beneficiary_bank_detail_account_number_txt input-group']/input")]
        private IWebElement txtAccountNumber;

        [FindsBy(How = How.XPath, Using = "//input[@value='Save Recipient']")]
        private IWebElement btnSaveRecipient;
                                    


        By TextSelectDelivery = By.XPath("//div[@class='form-group']/big");
        By TextFirstName = By.XPath("//input[@class='address_first_name_txt form-control input-sm']");

        public NewRecipientScreen(IWebDriver driver)
        {
            BasePage.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(10)));
        }

        public PaymentScreen CreateRecipient(string RecipientFirstName,string RecipientLastName)
        {
            Click(drpCountrysending);
            SendKeys(txtCountrySend, "Australia");
            txtCountrySend.SendKeys(Keys.Enter);
            WaitforVisibility(TextSelectDelivery);
            Click(btnNext);
            WaitforVisibility(TextFirstName);

            SendKeys(txtFirstName, RecipientFirstName);
            SendKeys(txtLastName, RecipientLastName);
            SendKeys(txtAddressline1, "167 Pretoria Street");
            SendKeys(txtAddressline2, "Hutt Central");
            SendKeys(txtSuburb, "Lower Hutt");
            SendKeys(txtCity, "Wellington");
            SendKeys(txtMobileNumber, "54645443");
            SendKeys(txtBankname, "ANZ");
            SendKeys(txtBankAddress1, "67 High Street");
            SendKeys(txtBankBranch, "Lower Hutt");
            SendKeys(txtBankCode, "121");
            SendKeys(txtSwift, "KIWINZ20");
            SendKeys(txtAccountNumber, "677232332");
            Click(btnSaveRecipient);
            Sleep(4);
            return new PaymentScreen(driver);
            
        }


    }
}