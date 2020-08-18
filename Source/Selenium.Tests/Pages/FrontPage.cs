using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Selenium.Tests.Base.Selenium.Core;
using System;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Pages
{
    class FrontPage
	{
		public static class Elements
		{
			public static readonly By DatabaseList        = By.Id("select-database");
			public static readonly By PrimaryButton       = By.CssSelector(".btn.btn-primary");
            public static readonly By SecondaryButton     = By.XPath("//button[text()='Login as a different user']");
            public static readonly By SelectDatabase      = By.Id("select-database");
            public static readonly By ChangeLanguage      = By.XPath("//select[@class='select-container form-control']");
           //public static readonly string DatabaseName    = "GECKO-EPHORTE-V6_SQL-EPH-V6-GUILT-(Missing tenant description)";
          public static readonly string DatabaseName    = "EPHORTE-GUI-LOADTEST";
            public static readonly By Ecm                 = By.XPath("//input[@id='user-identification']");
            public static readonly By SendButton          = By.XPath("//button[text()='Send']");
            public static readonly By LoginButton         = By.XPath("//button[contains(text(),'Login')]");        
        }

		private readonly RemoteWebDriver _driver;
		private readonly string _windowHandle;

		private FrontPage(RemoteWebDriver driver)
		{
			_driver = driver;
			_windowHandle = driver.CurrentWindowHandle;
		}

		public static FrontPage Connect(RemoteWebDriver driver)
		{
			driver.WaitForElementVisible(Elements.PrimaryButton);
			return new FrontPage(driver);
		}

        public static FrontPage ConnectEcm(RemoteWebDriver driver)
        {
            var v = driver.FindElements(Elements.Ecm).Count > 0 ? driver.WaitForElementVisible(Elements.SendButton) : driver.WaitForElementVisible(Elements.PrimaryButton);
            return new FrontPage(driver);
        }

        public void WriteDbInfoToConsole()
		{
			var elementSelectDb = _driver.FindElement(Elements.DatabaseList);
			var list = new SelectElement(elementSelectDb);
			foreach (var x in list.Options)
			{
				Console.WriteLine($"{x.GetAttribute("value")} {x.Text} - {(x.Selected ? "(Selected)" : "")}");
			}
		}

		public LoginWindow OpenLogin()
		{
            var frontpageWindow = _driver.CurrentWindowHandle;
			var element = _driver.FindElement(Elements.PrimaryButton);
			var timeout = TimeSpan.FromSeconds(10);
			if (!_driver.RunAndWaitForNewWindow(() => _driver.MoveToAndClick(element), timeout))
				throw new TimeoutException("Retry time expired");

			return new LoginWindow(_driver, frontpageWindow);
		}

        #region Select Language and Database
        /// <summary>
        /// Database Selection
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="database"></param>
        public void DatabaseSelection(RemoteWebDriver driver, string database)
        {
            driver.WaitForElementVisible(By.XPath("//button[contains(text(),'Login')]"));
            driver.EnterText(Elements.SelectDatabase, database);
            CommonMethods.PlayWait(3000);
        }

        /// <summary>
        /// Change the language
        /// </summary>
        /// <param name="language"></param>
        public void ChangeLanguage(string language)
        {
            _driver.SelectListValueAndSelectByText(Elements.ChangeLanguage, language);
        }

        #region Select Language and Database
        /// <summary>
        /// Select ecm and Select database, Language
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public LoginWindow OpenElement(RemoteWebDriver driver)
        {
            _driver.Manage().Window.Maximize();
            _driver.EcmSelection();

            driver.WaitForElementVisible(Elements.LoginButton);

            if(driver.FindElements(Elements.SelectDatabase).Count > 0)
            {
                IWebElement element1 = driver.FindElement(Elements.SelectDatabase);
                if (element1 != null)
                {
                    try
                    {
                        _driver.SelectListValueAndSelectByText(Elements.SelectDatabase, Elements.DatabaseName);
                    }
                    catch
                    {
                        CommonMethods.PlayWait(2000);
                        element1.SendKeys(Elements.DatabaseName);
                    }
                }
            }

            CommonMethods.PlayWait(3000);
            _driver.SelectListValueAndSelectByText(Elements.ChangeLanguage, "English");

            var frontpageWindow = _driver.CurrentWindowHandle;
            CommonMethods.PlayWait(2000);
            IWebElement primaryButton = null;
            try
            {
                primaryButton = _driver.FindElement(Elements.SecondaryButton);
            }
            catch 
            {
                CommonMethods.PlayWait(3000);
                primaryButton = _driver.FindElement(Elements.PrimaryButton);
            }
            
            var timeout = TimeSpan.FromSeconds(10);
            if (!_driver.RunAndWaitForNewWindow(() => _driver.MoveToAndClick(primaryButton), timeout))
                throw new TimeoutException("Retry time expired");
            return new LoginWindow(_driver, frontpageWindow);
        }
        #endregion

        #region Login into Application
        /// <summary>
        /// Login Into Application
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="moduleName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public static void LoginToApplication(RemoteWebDriver driver, string moduleName = "", string userName = "guilt", string password = "guilt")
        {
            var frontPage = ConnectEcm(driver);
            var loginWindow = frontPage.OpenElement(driver);
            loginWindow.Login(userName, password);
            CommonMethods.PlayWait(4000);
            loginWindow.SelectModule(moduleName);
            if (moduleName != ApplicationModules.Administrator.GetStringValue() || moduleName != ApplicationModules.MeetingModule.GetStringValue())
                CommonMethods.PlayWait(15000);
        }
        #endregion
        #endregion
    }
}
