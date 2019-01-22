using BackOfficeAutomation;
using BackOfficeAutomation.pageObjects;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Serilog;

namespace BackOfficeAutomationCore
{
    [TestFixture]
    public class SystemTestsNunit : BaseTest
    {

        [SetUp]
        public void SetupDriver()
        {
            GetDriver();
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

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
        public void KB_BackOfficeLogin(string UserName, string Password)
        {
            BackOfficeUITest(() =>
            {
                LoginScreen loginScreen = new LoginScreen(driver);
                HomeScreen homeScreen = loginScreen.navigateHome(UserName, Password);
                Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
                homeScreen.SignOut();
            });
        }

        /* Cymonz Customer Registration */

        [TestCase("35551", "Individual")]
        public void KBCustomerRegistration(string KBAccessNo, string CustomerType)
        {
            BackOfficeUITest(() =>
            {
                LoginScreen loginScreen = new LoginScreen(driver);
                HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
                Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
                homeScreen.NavigateCustomerTab();
                CustomersScreen customersScreen = homeScreen.NavigateCustomer();
                Assert.True(customersScreen.VerifyCustomerScreen(), "Customer Screen not Displayed");
                CustomersDetailsScreen customersDetailsScreen = customersScreen.navigateCustomerRegistration(KBAccessNo);
                Assert.True(customersDetailsScreen.VerifyCustomerDetails("Kiwibank", CustomerType), "Customer information is incorrect");
                customersDetailsScreen.SavingData();
                Assert.True(customersScreen.VerifyCustomerSaved(), "Customer Registration Fail");
                customersScreen.SignOut();
            });
        }


        /* CDD error message Verification */

        [TestCase("770654", "SGD")]
        public void KB_FCA_CDDChecks(string KBAccessNo, string CurrencyType)
        {
            BackOfficeUITest(() =>
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
                Assert.True(customersDetailsScreen.VerifyCustomerDetailPage(), "Customer Details Screen is not Displayed");
                FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
                fCAaccountScreen.NavigateFCAPopUp();
                Assert.True(fCAaccountScreen.VerifyFCAAccountTitle(), "BOU not see the FCA popup");
                fCAaccountScreen.CreateNewFCA(CurrencyType);
                Assert.True(fCAaccountScreen.VerifyCDDAEOIChecks("The customer is not CDD complete. Please arrange to have their details updated in In Touch, then try again."), "BOU not see correct CDD/AEOI error message");
                customersScreen.SignOut();
            });
        }


        /* AEOI error message Verification */

        [TestCase("116153", "PGK")]
        public void KB_FCA_AEOIChecks(string KBAccessNo, string CurrencyType)
        {
            BackOfficeUITest(() =>
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
                Assert.True(customersDetailsScreen.VerifyCustomerDetailPage(), "Customer Details Screen is not Displayed");
                FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
                fCAaccountScreen.NavigateFCAPopUp();
                Assert.True(fCAaccountScreen.VerifyFCAAccountTitle(), "BOU not see the FCA popup");
                fCAaccountScreen.CreateNewFCA(CurrencyType);
                Assert.True(fCAaccountScreen.VerifyCDDAEOIChecks("The customer is not CDD and AEOI complete. Please arrange to have their details updated in In Touch, then try again."), "BOU not see correct CDD/AEOI error message");
                customersScreen.SignOut();
            });
        }


        /* Cymonz FCA Creation
            Pre Conditions - Customer should be AEOI & CDD Green */

        [TestCase("2558", "DKK")]
        public void KB_FCA_Creation(string KBAccessNo, string CurrencyType)
        {
            BackOfficeUITest(() =>
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
                Assert.True(customersDetailsScreen.VerifyCustomerDetailPage(), "Customer Details Screen is not Displayed");
                FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
                fCAaccountScreen.NavigateFCAPopUp();
                Assert.True(fCAaccountScreen.VerifyFCAAccountTitle(), "BOU not see the FCA popup");
                fCAaccountScreen.CreateNewFCA(CurrencyType);
                Assert.True(fCAaccountScreen.VerifyFCAAccountCreate(), "FCA Account create Fail");
                customersScreen.SignOut();
            });
        }

        /* Cymonz FCA Blocking Flow */

        [TestCase("2558", "2558USD05")]

        public void KB_FCA_Blocking(string KBAccessNo, string FCAAccountNo)
        {
            BackOfficeUITest(() =>
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
                Assert.True(customersDetailsScreen.VerifyCustomerDetailPage(), "Customer Details Screen is not Displayed");
                FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
                Assert.True(fCAaccountScreen.FCAAvailabilityCheck(FCAAccountNo), "FCA Account " + FCAAccountNo + " is Not available");
                fCAaccountScreen.SearchFCAAccountNumber(FCAAccountNo);
                fCAaccountScreen.SelectStatus("Block");
                Assert.True(fCAaccountScreen.VerifytheBlockConfirmation(), "BOU not seen see the block confirmation popup");
                Assert.True(fCAaccountScreen.VerifyFCAAccountBlock(), "FCA Account Blocked Failed");
                customersScreen.SignOut();
            });
        }


        /* Cymonz FCA Unblocking Flow */

        [TestCase("2558", "2558USD05")]
        public void KB_FCA_UnBlocking(string KBAccessNo, string FCAAccountNo)
        {
            BackOfficeUITest(() =>
            {
                LoginScreen loginScreen = new LoginScreen(driver);
                HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
                Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
                Log.Information("User Login Successfully");
                homeScreen.NavigateCustomerTab();
                CustomersScreen customersScreen = homeScreen.NavigateCustomer();
                customersScreen.SelectSearchCritirea();
                customersScreen.SearchCustomer(KBAccessNo);
                Assert.True(customersScreen.VerifyCustomerSearched(KBAccessNo), "Customer not searched");
                CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
                Assert.True(customersDetailsScreen.VerifyCustomerDetailPage(), "Customer Details Screen is not Displayed");
                FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
                Assert.True(fCAaccountScreen.FCAAvailabilityCheck(FCAAccountNo), "FCA Account " + FCAAccountNo + " is Not available");
                fCAaccountScreen.SearchFCAAccountNumber(FCAAccountNo);
                fCAaccountScreen.SelectStatus("Unblock");
                Assert.True(fCAaccountScreen.VerifytheUnblockConfirmation(), "BOU not seen see the Unblock confirmation popup");
                Assert.True(fCAaccountScreen.VerifyFCAAccountUnblock(), "FCA Account Unblocked Failed");
                customersScreen.SignOut();
            });
        }


        /* Cymonz FCA Closing Flow 0 Balance*/

        [TestCase("2558", "2558USD00", "Poor Service")]
        public void KB_FCA_Closing(string KBAccessNo, string FCAAccountNo, string CloseReason)
        {
            BackOfficeUITest(() =>
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
                Assert.True(customersDetailsScreen.VerifyCustomerDetailPage(), "Customer Details Screen is not Displayed");
                FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
                Assert.True(fCAaccountScreen.FCAAvailabilityCheck(FCAAccountNo), "FCA Account " + FCAAccountNo + " is Not available");
                fCAaccountScreen.SearchFCAAccountNumber(FCAAccountNo);
                fCAaccountScreen.SelectStatus("Close");
                Assert.True(fCAaccountScreen.ClickOnCloseConfiramation(), "BOU not seen see the close confirmation popup");
                fCAaccountScreen.NavigateCloseAccount(CloseReason);
                Assert.True(fCAaccountScreen.VerifyFCAAccountClose(), "FCA Account Closed Failed");
                customersScreen.SignOut();
            });
        }


        /* Cymonz FCA Closing Flow with FCA balance (User cannot close the FCA)*/

        [TestCase("2558", "2558AUD00", "Poor Service")]
        public void KB_FCA_ClosingWithAmount(string KBAccessNo, string FCAAccountNo, string CloseReason)
        {
            BackOfficeUITest(() =>
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
                Assert.True(customersDetailsScreen.VerifyCustomerDetailPage(), "Customer Details Screen is not Displayed");
                FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
                Assert.True(fCAaccountScreen.FCAAvailabilityCheck(FCAAccountNo), "FCA Account " + FCAAccountNo + " is Not available");
                fCAaccountScreen.SearchFCAAccountNumber(FCAAccountNo);
                fCAaccountScreen.SelectStatus("Close");
                Assert.True(fCAaccountScreen.VerifyErrorMessage(), "BOU not seen error message");
                customersScreen.SignOut();
            });
        }


        /* Cymonz Payment Work Flow with Existing Recipient*/

        [TestCase("2558", "SOCIAL CLUB", "Justin Panagoda", "20.00", "AUD", "389011083458900")]
        //[TestCase("89590", "KID XMAS PARTY", "Peushan Panagoda", "10", "AUD", "389000077431401")]
        public void KB_Payments(string KBAccessNo, string CoreAccountName,
                string RecipientName, string PayAmount, string Currency, string AccountNumber)
        {
            BackOfficeUITest(() =>
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
                Assert.True(customersDetailsScreen.VerifyCustomerDetailPage(), "Customer Details Screen is not Displayed");
                var data = new JObject();
                data = customersDetailsScreen.DataStore();
                QuoteScreen quoteScreen = customersDetailsScreen.navigateQuote();
                string account = data.GetValue("Account Name").ToString();
                Assert.True(quoteScreen.VerifyQuotes(account), "Not Reach to Quotes Screen");
                quoteScreen.SelectCurrency(Currency, PayAmount, CoreAccountName);
                Assert.True(quoteScreen.VerifyAccountNumberDisplayed(AccountNumber), "Account Number is not Visible");
                PaymentScreen paymentScreen = quoteScreen.ReviewTrnsaction();
                Assert.True(paymentScreen.VerifyPanel(Currency, PayAmount), "Panel Data showing incorrect");
                PaymentTransactionScreen paymentTransactionScreen = paymentScreen.SelectRecipient(RecipientName, PayAmount);
                RecipientPaymentScreen recipientPaymentScreen = paymentTransactionScreen.GotoSettlements();
                recipientPaymentScreen.SelectRecipient(RecipientName);
                customersScreen.SignOut();
            });
        }


        /* Cymonz Payment Work Flow change balance of FCA accounts*/

        [TestCase("2558", "SOCIAL CLUB", "Account Number 2558SGD01", "50.00", "SGD", "389011083458900")]
        [TestCase("89590", "KID XMAS PARTY", "Peushan Panagoda", "10.00", "AUD", "389000077431401")]
        public void KB_Payments_FCA_BalanceChange(string KBAccessNo, string CoreAccountName,
                string RecipientName, string PayAmount, string Currency, string AccountNumber)
        {
            BackOfficeUITest(() =>
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
                Assert.True(customersDetailsScreen.VerifyCustomerDetailPage(), "Customer Details Screen is not Displayed");
                var data = new JObject();
                data = customersDetailsScreen.DataStore();
                QuoteScreen quoteScreen = customersDetailsScreen.navigateQuote();
                string account = data.GetValue("Account Name").ToString();
                Assert.True(quoteScreen.VerifyQuotes(account), "Not Reach to Quotes Screen");
                quoteScreen.SelectCurrency(Currency, PayAmount, CoreAccountName);
                Assert.True(quoteScreen.VerifyAccountNumberDisplayed(AccountNumber), "Account Number is not Visible");
                PaymentScreen paymentScreen = quoteScreen.ReviewTrnsaction();
                PaymentTransactionScreen paymentTransactionScreen = paymentScreen.SelectRecipient(RecipientName, PayAmount);
                Assert.True(paymentTransactionScreen.VerifyTransactionPage(), "Transaction Page is not visible");
                customersScreen.SignOut();
            });
        }


        /* Cymonz Payment Work Flow with New Recipient*/

        [TestCase("2558", "SOCIAL CLUB", "Justin", "Panagoda", "10.00", "AUD", "389011083458900", "Australia")]
        //[TestCase("89590", "KID XMAS PARTY", "Peushan Panagoda", "10", "AUD", "389000077431401")]
        public void KB_Payments_New_Recipient(string KBAccessNo, string CoreAccountName,
                string RecipientFName, string RecipientLName, string PayAmount, string Currency, string AccountNumber, string RecipientCurrency)
        {
            BackOfficeUITest(() =>
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
                Assert.True(customersDetailsScreen.VerifyCustomerDetailPage(), "Customer Details Screen is not Displayed");
                customersDetailsScreen.DataStore();
                QuoteScreen quoteScreen = customersDetailsScreen.navigateQuote();
                quoteScreen.SelectCurrency(Currency, PayAmount, CoreAccountName);
                Assert.True(quoteScreen.VerifyAccountNumberDisplayed(AccountNumber), "Account Number is not Visible");
                PaymentScreen paymentScreen = quoteScreen.ReviewTrnsaction();
                Assert.True(paymentScreen.VerifyPanel(Currency, PayAmount), "Panel Data showing incorrect");
                NewRecipientScreen newRecipientScreen = paymentScreen.AddRecipient("Create New Recipient");
                PaymentScreen paymentScreenafterRecipient = newRecipientScreen.CreateRecipient(RecipientFName, RecipientLName, RecipientCurrency);
                PaymentTransactionScreen paymentTransactionScreen = paymentScreenafterRecipient.SelectRecipient(RecipientFName + " " + RecipientLName, PayAmount);
                RecipientPaymentScreen recipientPaymentScreen = paymentTransactionScreen.GotoSettlements();
                recipientPaymentScreen.SelectRecipient(RecipientFName + " " + RecipientLName);
                customersScreen.SignOut();

            });
        }


    }
}

