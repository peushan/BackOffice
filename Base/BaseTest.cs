﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Extensions;
using Serilog;
using System;


namespace BackOfficeAutomation
{
    public class BaseTest
    {
        private static string browser = "Chrome";
        private static string baseURL = "https://internationalservices-test.kbtest.cloud/back-office/login";
        public static IWebDriver driver;


        public IWebDriver GetDriver()
        {
            if (driver == null)
            {
                BrowserLaunch();
                
            }
            return driver;
        }

        public IWebDriver BrowserLaunch()
        {
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                case "Firefox":
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
                    service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                    driver = new FirefoxDriver(service);
                    break;
                case "ChromeHeadless":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("headless");
                    options.AddArguments("no-sandbox");
                    options.AddArguments("disable-gpu");
                    options.AddArguments("window-size=1920,1080");
                    //options.AddArguments("--window-size=2560,1440");
                    //options.AddArguments("--whitelisted-ips");

                    driver = new ChromeDriver(options);
                    break;
            }
            driver.Manage().Window.Maximize();
            Goto(baseURL);
            return driver;
        }

        public void Goto(string url)
        {
            driver.Url = url;
        }

        protected void BackOfficeUITest(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                var screenshot = driver.TakeScreenshot();
                var date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                var filePath = "C:\\Users\\ppanag01\\source\\repos\\BackOfficeAutomationCore\\BackOfficeAutomationCore\\logs\\" + date + ".jpg";

                screenshot.SaveAsFile(filePath, OpenQA.Selenium.ScreenshotImageFormat.Png);

                // This would be a good place to log the exception message and
                // save together with the screenshot

                throw;
            }
        }

        protected void BackOfficeUITest1(Action action, string testcase)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                var screenshot = driver.TakeScreenshot();
                var date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH_mm_ss");
                var filePath = "C:\\Users\\ppanag01\\source\\repos\\BackOfficeAutomationCore\\BackOfficeAutomationCore\\logs\\" + date + " - " + testcase + ".jpg";

                screenshot.SaveAsFile(filePath, OpenQA.Selenium.ScreenshotImageFormat.Png);

                // This would be a good place to log the exception message and
                // save together with the screenshot

                throw;
            }
        }
    }

}
