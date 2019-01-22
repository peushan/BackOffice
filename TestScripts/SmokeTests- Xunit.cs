using BackOfficeAutomation;
using BackOfficeAutomation.pageObjects;
using Xunit;

namespace BackOfficeAutomationCore
{

    /*public class SmokeTests : BaseTest
    {

        // Login to Cymonz Back Office and Sign Out ! 
        [Theory]
        [InlineData("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@")]
        public void CymonzLogin(string UserName, string Password)
        {
            GetDriver();
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome(UserName,Password);
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SignOut();
            driver.Quit();
        }

        // Cymonz Customer Registration
        [Fact]
        public void KBCustomerRegistration()
        {
            GetDriver();
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(),"User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            Assert.True(customersScreen.VerifyCustomerScreen(), "Customer Screen not Displayed");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.navigateCustomerRegistration("1143383");
            Assert.True(customersDetailsScreen.VerifyCustomerDetails("Kiwibank", "Individual"), "Customer information is incorrect");
            customersDetailsScreen.SavingData();
            Assert.True(customersScreen.VerifyCustomerSaved(), "Customer Registration Fail");
            customersScreen.SignOut();
            driver.Quit();
        }



        [Fact]
        public void KB_FCA_Creation()
        {
            GetDriver();
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer("2558");
            Assert.True(customersScreen.VerifyCustomerSearched("2558"), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
            fCAaccountScreen.NavigateFCAPopUp();
            Assert.True(fCAaccountScreen.VerifyFCAAccountTitle(), "BOU not see the FCA popup");
            fCAaccountScreen.CreateNewFCA("JPY");
            Assert.True(fCAaccountScreen.VerifyFCAAccountCreate(), "FCA Account create Fail");
            customersScreen.SignOut();
            driver.Quit();
        }




        [Fact]
        public void KB_FCA_Blocking()
        {
            GetDriver();
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer("2558");
            Assert.True(customersScreen.VerifyCustomerSearched("2558"), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
            Assert.True(fCAaccountScreen.FCAAvailabilityCheck("2558SBD00"), "FCA Account " + "2558SBD00" + " is Not available");
            fCAaccountScreen.SearchFCAAccountNumber("2558SBD00");
            fCAaccountScreen.SelectStatus("Block");
            Assert.True(fCAaccountScreen.VerifytheBlockConfirmation(), "BOU not seen see the block confirmation popup");
            Assert.True(fCAaccountScreen.VerifyFCAAccountBlock(), "FCA Account Blocked Failed");
            customersScreen.SignOut();
            driver.Quit();
        }




        [Fact]
        public void KB_FCA_UnBlocking()
        {
            GetDriver();
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer("2558");
            Assert.True(customersScreen.VerifyCustomerSearched("2558"), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
            Assert.True(fCAaccountScreen.FCAAvailabilityCheck("2558SBD00"), "FCA Account " + "2558SBD00" + " is Not available");
            fCAaccountScreen.SearchFCAAccountNumber("2558SBD00");
            fCAaccountScreen.SelectStatus("Unblock");
            Assert.True(fCAaccountScreen.VerifytheUnblockConfirmation(), "BOU not seen see the Unblock confirmation popup");
            Assert.True(fCAaccountScreen.VerifyFCAAccountUnblock(), "FCA Account Unblocked Failed");
            customersScreen.SignOut();
            driver.Quit();
        }


  

        [Fact]
        public void KB_FCA_Closing()
        {
            GetDriver();
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer("2558");
            Assert.True(customersScreen.VerifyCustomerSearched("2558"), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            FCAaccountScreen fCAaccountScreen = customersDetailsScreen.NavigatetoFCA();
            Assert.True(fCAaccountScreen.FCAAvailabilityCheck("2558ZAR01"), "FCA Account " + "2558ZAR01" + " is Not available");
            fCAaccountScreen.SearchFCAAccountNumber("2558ZAR01");
            fCAaccountScreen.SelectStatus("Close");
            Assert.True(fCAaccountScreen.ClickOnCloseConfiramation(), "BOU not seen see the close confirmation popup");
            fCAaccountScreen.NavigateCloseAccount("Poor Service");
            Assert.True(fCAaccountScreen.VerifyFCAAccountClose(), "FCA Account Closed Failed");
            customersScreen.SignOut();
            driver.Quit();
        }

        [Fact]
        public void KB_Payments()
        {
            GetDriver();
            LoginScreen loginScreen = new LoginScreen(driver);
            HomeScreen homeScreen = loginScreen.navigateHome("peushan.panagoda@kiwibank.co.nz", "1qaz2wsx@");
            Assert.True(homeScreen.VerifySignInSuccess(), "User Sign in Fail");
            homeScreen.NavigateCustomerTab();
            CustomersScreen customersScreen = homeScreen.NavigateCustomer();
            customersScreen.SelectSearchCritirea();
            customersScreen.SearchCustomer("770654");
            Assert.True(customersScreen.VerifyCustomerSearched("770654"), "Customer not searched");
            CustomersDetailsScreen customersDetailsScreen = customersScreen.SelectCustomer();
            customersDetailsScreen.DataStore();
            QuoteScreen quoteScreen = customersDetailsScreen.navigateQuote();
            quoteScreen.SelectCurrency("AUD", "10", "STEVES");
            PaymentScreen paymentScreen = quoteScreen.ReviewTrnsaction();
            paymentScreen.SelectRecipient("Pamodh Panagoda", "10");
            RecipientPaymentScreen recipientPaymentScreen = paymentScreen.GotoSettlements();




            customersScreen.SignOut();
            driver.Quit();
        }
    }*/
}

