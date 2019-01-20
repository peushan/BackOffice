using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;

namespace BackOfficeAutomation.pageObjects
{
    public class FCAaccountScreen : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//a[@class='btn btn-sm btn-primary ']")]
        private IWebElement btnCreateNewFCA;

        [FindsBy(How = How.XPath, Using = "//div[@id='customer_bank_account_currency_id_chosen']/div[@class='chosen-drop']/div/input[@type='text']")]
        private IWebElement txtEnterCurrency;

        [FindsBy(How = How.XPath, Using = "//div[@id='customer_bank_account_currency_id_chosen']")]
        private IWebElement drpCurrency;

        [FindsBy(How = How.Id, Using = "customer_bank_account_currency_id_chosen")]
        private IWebElement drpCurrency1;

        [FindsBy(How = How.XPath, Using = "//div[@class='modal-footer']/input[@type='submit']")]
        private IWebElement btnCreateFCA;

        [FindsBy(How = How.XPath, Using = "//div[@class='container-fluid']/em")]
        private IWebElement txtSuccessMsg;

        [FindsBy(How = How.XPath, Using = "//button[@class='close']")]
        private IWebElement btnClose;

        [FindsBy(How = How.XPath, Using = "//h3[@class='modal-title']")]
        private IWebElement titleFCA;

        [FindsBy(How = How.XPath, Using = "//div[@class='fixed-table-body']//table[@class='table table-bordered table-striped table-hover']/tbody")]
        private IWebElement fcaTable;

        [FindsBy(How = How.XPath, Using = "//div[@id='column_chosen']")]
        private IWebElement dropSearch;

        [FindsBy(How = How.XPath, Using = "//li[@title='Account Number']")]
        private IWebElement selectSearchCriti;
        
        [FindsBy(How = How.XPath, Using = "//input[@id='keyword']")]
        private IWebElement txtSearch;

        [FindsBy(How = How.XPath, Using = "//i[@class='icon-search']")]
        private IWebElement btnSearch;

        [FindsBy(How = How.XPath, Using = "//div[@class='dropdown']//button[@data-toggle='dropdown']")]
        private IWebElement btnCloseBlock;

        [FindsBy(How = How.XPath, Using = "//button[@class='btn commit btn-success']")]
        private IWebElement btnYes;

        [FindsBy(How = How.XPath, Using = "//h4[@class='modal-title']")]
        private IWebElement txtCloseMsg;

        [FindsBy(How = How.XPath, Using = "//div[@id='cba_closure_reason_thing_id_chosen']//a[@class='chosen-single']")]
        private IWebElement dropCloseReason;

        [FindsBy(How = How.XPath, Using = "//input[@value='Close']")]
        private IWebElement btnCloseConfirm;

        [FindsBy(How = How.XPath, Using = "//div[@class='alert alert-danger affix']")]
        private IWebElement txtErrorNotification;

        [FindsBy(How = How.XPath, Using = "//button[@class='close icon-remove']")]
        private IWebElement btnErrorClose;


        By fcaTable1 = By.XPath("//div[@class='fixed-table-body']//table[@class='table table-bordered table-striped table-hover']/tbody");
        By txtCloseMsg1 = By.XPath("//h4[@class='modal-title']");
        By dropCloseReason1 = By.XPath("//div[@id='cba_closure_reason_thing_id_chosen']//a[@class='chosen-single']");


        public FCAaccountScreen(IWebDriver driver)
        {
            BasePage.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(30)));
        }

        public void CreateNewFCA(string Currency)
        {
            Click(drpCurrency1);
            SendKeys(txtEnterCurrency, Currency);
            txtEnterCurrency.SendKeys(Keys.Enter);
            Click(btnCreateFCA);
        }

        public bool VerifyFCAAccountCreate()
        {
            bool displaySuccess = false;
            if (txtSuccessMsg.Text.Equals("Bank account was created successfully and is Active."))
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

        public bool VerifyFCAAccountTitle()
        {
            bool displaySuccess = false;
            if (titleFCA.Text.Equals("Create New FCA"))
            {
                displaySuccess = true;
            }
            else
            {
                displaySuccess = false;
            }
            return displaySuccess;

        }

        public bool FCAAvailabilityCheck(string FCAAccountNumber)
        {
            bool FCAAvailability = false;
            WaitforVisibility(fcaTable1);
            List<IWebElement> Rows = new List<IWebElement>(fcaTable.FindElements(By.TagName("tr")));
            String FCAAvailable = "No";
            foreach (var elemTr in Rows)
            {
                List<IWebElement> Columns = new List<IWebElement>(elemTr.FindElements(By.TagName("td")));

                foreach (var elemTd in Columns)
                {
                    if (elemTd.Text.Equals(FCAAccountNumber))
                    {
                        FCAAvailable = "Yes";
                        break;
                    }
                    else
                    {
                        FCAAvailable = "No";
                    }
                }
                if (FCAAvailable.Equals("Yes"))
                {
                    FCAAvailability = true;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return FCAAvailability;
        }

        public void NavigateFCAPopUp()
        {
            Click(btnCreateNewFCA);
        }

        public void SearchFCAAccountNumber(string FCAAccountNumber)
        {
            Click(dropSearch);
            Click(selectSearchCriti);
            SendKeys(txtSearch, FCAAccountNumber);
            Click(btnSearch);
            
        }

        public void SelectStatus(string Status)
        {
            Click(btnCloseBlock);
            IWebElement selectStatus = driver.FindElement(By.XPath("//ul[@class='dropdown-menu']//a[contains(text(), '" + Status + "')]"));
            Click(selectStatus);
        }

        public bool VerifyFCAAccountBlock()
        {
            Click(btnYes);
            bool displaySuccess = false;
            if (txtSuccessMsg.Text.Equals("Account was blocked successfully"))
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

        public bool VerifyFCAAccountUnblock()
        {
            Click(btnYes);
            bool displaySuccess = false;
            if (txtSuccessMsg.Text.Equals("Account was unblocked successfully"))
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
        
        public bool ClickOnCloseConfiramation()
        {
            bool displaySuccess = false;
            WaitforVisibility(txtCloseMsg1);
            if (txtCloseMsg.Text.Equals("This FCA will be closed and cannot be reopened"))
            {
                displaySuccess = true;
                Click(btnYes);
            }
            else
            {
                displaySuccess = false;
            }
            return displaySuccess;

        }

        public bool VerifyErrorMessage()
        {
            bool displaySuccess = false;
            if (txtErrorNotification.Text.Equals("The balance must be $0 to close this FCA. Please transfer funds before closing the FCA"))
            {
                displaySuccess = true;
                Click(btnErrorClose);
            }
            else
            {
                displaySuccess = false;
            }
            return displaySuccess;

        }

        public void NavigateCloseAccount(string CloseReason)
        {
            WaitforVisibility(dropCloseReason1);
            Click(dropCloseReason);
            IWebElement txtCloserReason = driver.FindElement(By.XPath("//li[@title='" + CloseReason + "']"));
            Click(txtCloserReason);
            Click(btnCloseConfirm);
        }

        public bool VerifyFCAAccountClose()
        {
            bool displaySuccess = false;
            if (txtSuccessMsg.Text.Equals("Account was closed successfully"))
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

        public bool VerifytheBlockConfirmation()
        {
            WaitforVisibility(txtCloseMsg1);
            bool displaySuccess = false;
            if (txtCloseMsg.Text.Equals("This will block all transactions on this FCA"))
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

        public bool VerifytheUnblockConfirmation()
        {
            WaitforVisibility(txtCloseMsg1);
            bool displaySuccess = false;
            if (txtCloseMsg.Text.Equals("Setting this FCA to active will allow full transactional access to the FCA"))
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