using BackOfficeAutomation;
using BackOfficeAutomation.pageObjects;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace BackOfficeAutomationCore
{
    [TestFixture]
    public class SmokeTestsNunit : BaseTest
    {
        
        [SetUp]
        public void SetupDriver()
        {
            GetDriver();
        }

        [TearDown]
        public void CloseDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
            else
            {
                driver = null;
            }
        }

        /* Login to Cymonz Back Office and Sign Out  */

        [TestCase("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@")]
        public void CymonzLogin(string UserName, string Password)
        {
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome(UserName,Password);
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SignOut();
            
        }

        /* Cymonz Customer Registration */

        [TestCase("35551", "Individual")]
        public void KBCustomerRegistration(string KBAccessNo, string CustomerType)
        {
           
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(),"User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            Assert.True(customersScreen.VerifyCustomerScreen(), "Customer Screen not Displayed");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.navigateCustomerRegistration(KBAccessNo);
            Assert.True(customersDetailsScreen.VerifyCustomerDetails("Kiwibank", CustomerType), "Customer information is incorrect");
            customersDetailsScreen.SavingData();
            Assert.True(customersScreen.VerifyCustomerSaved(), "Customer Registration Fail");
            customersScreen.SignOut();
          
        }

        /* Cymonz FCA Creation
            Pre Conditions - Customer should be AEOI & CDD Green */

        [TestCase("2558", "USD")]
        public void KB_FCA_Creation(string KBAccessNo,string CurrencyType)
        {     
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer(KBAccessNo);
            Assert.True(customersScreen.VerifyCustomerSearched(KBAccessNo), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
            fCAaccountScreen.NavigateFCAPopUp();
            Assert.True(fCAaccountScreen.VerifyFCAAccountTitle(), "BOU not see the FCA popup");
            fCAaccountScreen.CreateNewFCA(CurrencyType);
            Assert.True(fCAaccountScreen.VerifyFCAAccountCreate(), "FCA Account create Fail");
            customersScreen.SignOut();           
        }

        /* Cymonz FCA Blocking Flow */

        [TestCase("2558", "2558USD05")]
        public void KB_FCA_Blocking(string KBAccessNo, string FCAAccountNo)
        {
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer(KBAccessNo);
            Assert.True(customersScreen.VerifyCustomerSearched(KBAccessNo), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
            Assert.True(fCAaccountScreen.FCAAvailabilityCheck(FCAAccountNo), "FCA Account " + FCAAccountNo + " is Not available");
            fCAaccountScreen.SearchFCAAccountNumber(FCAAccountNo);
            fCAaccountScreen.SelectStatus("Block");
            Assert.True(fCAaccountScreen.VerifytheBlockConfirmation(), "BOU not seen see the block confirmation popup");
            Assert.True(fCAaccountScreen.VerifyFCAAccountBlock(), "FCA Account Blocked Failed");
            customersScreen.SignOut();
        }


        /* Cymonz FCA Unblocking Flow */

        [TestCase("2558", "2558JPY00")]
        public void KB_FCA_UnBlocking(string KBAccessNo, string FCAAccountNo)
        {
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer(KBAccessNo);
            Assert.True(customersScreen.VerifyCustomerSearched(KBAccessNo), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
            Assert.True(fCAaccountScreen.FCAAvailabilityCheck(FCAAccountNo), "FCA Account " + FCAAccountNo + " is Not available");
            fCAaccountScreen.SearchFCAAccountNumber(FCAAccountNo);
            fCAaccountScreen.SelectStatus("Unblock");
            Assert.True(fCAaccountScreen.VerifytheUnblockConfirmation(), "BOU not seen see the Unblock confirmation popup");
            Assert.True(fCAaccountScreen.VerifyFCAAccountUnblock(), "FCA Account Unblocked Failed");
            customersScreen.SignOut();
        }


        /* Cymonz FCA Closing Flow 0 Balance*/

        [TestCase("2558", "2558USD00", "Poor Service")]
        public void KB_FCA_Closing(string KBAccessNo, string FCAAccountNo, string CloseReason)
        {
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer(KBAccessNo);
            Assert.True(customersScreen.VerifyCustomerSearched(KBAccessNo), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
            Assert.True(fCAaccountScreen.FCAAvailabilityCheck(FCAAccountNo), "FCA Account " + FCAAccountNo + " is Not available");
            fCAaccountScreen.SearchFCAAccountNumber(FCAAccountNo);
            fCAaccountScreen.SelectStatus("Close");
            Assert.True(fCAaccountScreen.ClickOnCloseConfiramation(), "BOU not seen see the close confirmation popup");
            fCAaccountScreen.NavigateCloseAccount(CloseReason);
            Assert.True(fCAaccountScreen.VerifyFCAAccountClose(), "FCA Account Closed Failed");
            customersScreen.SignOut();
        }


        /* Cymonz FCA Closing Flow with FCA balance (User cannot close the FCA)*/

        [TestCase("2558", "2558AUD00", "Poor Service")]
        public void KB_FCA_ClosingWithAmount(string KBAccessNo, string FCAAccountNo, string CloseReason)
        {
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer(KBAccessNo);
            Assert.True(customersScreen.VerifyCustomerSearched(KBAccessNo), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
            Assert.True(fCAaccountScreen.FCAAvailabilityCheck(FCAAccountNo), "FCA Account " + FCAAccountNo + " is Not available");
            fCAaccountScreen.SearchFCAAccountNumber(FCAAccountNo);
            fCAaccountScreen.SelectStatus("Close");
            Assert.True(fCAaccountScreen.VerifyErrorMessage(), "BOU not seen error message");
            customersScreen.SignOut();
        }


        /* Cymonz Payment Work Flow with Existing Recipient*/

        [TestCase("2558", "SOCIAL CLUB", "Peter Panagoda","10", "AUD")]
        public void KB_Payments(string KBAccessNo,string CoreAccountName,
                string RecipientName, string PayAmount,string Currency)
        {
           
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer(KBAccessNo);
            Assert.True(customersScreen.VerifyCustomerSearched(KBAccessNo), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            var data = new JObject();
            data = customersDetailsScreen.DataStore();
            QuoteScreen quoteScreen = customersDetailsScreen.navigateQuote();
            string account = data.GetValue("Account Name").ToString();
            Assert.True(quoteScreen.VerifyQuotes(account), "Not Reach to Quotes Screen");
            quoteScreen.SelectCurrency(Currency, PayAmount ,CoreAccountName);
            PaymentScreen paymentScreen = quoteScreen.ReviewTrnsaction();

            var paneldata = new JObject();
            paneldata = paymentScreen.VerifyPanel();
            string reference = paneldata.GetValue("1").ToString();
            paymentScreen.SelectRecipient(RecipientName, PayAmount);
            RecipientPaymentScreen recipientPaymentScreen = paymentScreen.GotoSettlements();
            recipientPaymentScreen.SelectRecipient(RecipientName);

            customersScreen.SignOut();
        }



        /* Cymonz Payment Work Flow change balance of FCA accounts*/

        [TestCase("2558", "SOCIAL CLUB", "Account Number 2558USD06", "50", "USD")]
        public void KB_Payments_FCA_BalanceChange(string KBAccessNo, string CoreAccountName,
                string RecipientName, string PayAmount, string Currency)
        {

            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer(KBAccessNo);
            Assert.True(customersScreen.VerifyCustomerSearched(KBAccessNo), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            var data = new JObject();
            data = customersDetailsScreen.DataStore();
            QuoteScreen quoteScreen = customersDetailsScreen.navigateQuote();
            string account = data.GetValue("Account Name").ToString();
            Assert.True(quoteScreen.VerifyQuotes(account), "Not Reach to Quotes Screen");
            quoteScreen.SelectCurrency(Currency, PayAmount, CoreAccountName);
            PaymentScreen paymentScreen = quoteScreen.ReviewTrnsaction();
            paymentScreen.SelectRecipient(RecipientName, PayAmount);     
            customersScreen.SignOut();
        }


        /* Cymonz Payment Work Flow with New Recipient*/

        [TestCase("2558", "SOCIAL CLUB", "Peter","Panagoda", "10", "AUD")]
        public void KB_Payments_New_Recipient(string KBAccessNo, string CoreAccountName,
                string RecipientFName, string RecipientLName, string PayAmount, string Currency)
        {

            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer(KBAccessNo);
            Assert.True(customersScreen.VerifyCustomerSearched(KBAccessNo), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            customersDetailsScreen.DataStore();
            QuoteScreen quoteScreen = customersDetailsScreen.navigateQuote();
            quoteScreen.SelectCurrency(Currency, PayAmount, CoreAccountName);
            PaymentScreen paymentScreen = quoteScreen.ReviewTrnsaction();
            NewRecipientScreen newRecipientScreen = paymentScreen.AddRecipient("Create New Recipient");
            PaymentScreen paymentScreenafterRecipient = newRecipientScreen.CreateRecipient(RecipientFName, RecipientLName);
            paymentScreenafterRecipient.SelectRecipient(RecipientFName +" "+ RecipientLName, PayAmount);
            RecipientPaymentScreen recipientPaymentScreen = paymentScreen.GotoSettlements();
            recipientPaymentScreen.SelectRecipient(RecipientFName + " " + RecipientLName);
            customersScreen.SignOut();
        }


    }
}

