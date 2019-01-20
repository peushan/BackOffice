using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Threading;


namespace BackOfficeAutomation
{
    public class BasePage : BaseTest
    {
        [FindsBy(How = How.XPath, Using = "//a[@title='Log Out']")]
        private IWebElement btnLogout;

        [FindsBy(How = How.XPath, Using = "//button[@class='close']")]
        private IWebElement btnClose;

        By btnsignOut = By.XPath("//a[@title='Log Out']");


        protected void WaitforVisibility(By locator)
        {
            Sleep(1);
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        protected void SendKeys(IWebElement element, String text)
        {
            try
            {
                element.Click();
                element.Clear();
                element.SendKeys(text);

            }
            catch (WebDriverException e)
            {
                try
                {

                    element.SendKeys(text);
                }
                catch (Exception d)
                {

                }

            }
            catch (Exception e)
            {

            }
        }

        protected void Click(IWebElement element)
        {
            try
            {
                if (element.Displayed && element.Enabled)
                {
                    //Log.Info("BOU Click the element " + element.GetAttribute("value") + " / " + element.Text + " / " + element);
                    element.Click();
                }
                else
                {
                    Sleep(1);
                    //Log.Info("BOU Click the element " + element.GetAttribute("value") + " / " + element.Text + " / " + element);
                    element.Click();

                }
            }
            catch (NoSuchElementException f)
            {
                element.Click();
            }
        }

        protected void Sleep(int seconds)
        {
            try
            {
                Thread.Sleep(seconds * 1000);
            }
            catch (Exception e)
            {

            }
        }

        public void SignOut()
        {
            try
            {
                Click(btnClose);
                Click(btnLogout);
            }
            catch (Exception e)
            {
                WaitforVisibility(btnsignOut);
                Click(btnLogout);
            }
        }
    }
}
