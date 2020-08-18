using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using System;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;
using System.Linq;

namespace Selenium.Tests.Pages
{
	class LoginWindow
	{
		public static class Elements
		{
			
            public static readonly By UserNameTextBoxOLD = By.Id("username");
            public static readonly By PasswordTextBoxOLD = By.Id("password");
            public static readonly By RememberMeCheckboxOLD = By.XPath("//input[@id='rememberMe']");
            public static readonly By PrimaryButton = By.CssSelector(".btn-primary");
			internal static readonly By ErrorArea = By.CssSelector(".alert-danger");
            public static readonly By Ecm = By.XPath("//input[@id='user-identification']");
            public static readonly By SendButton = By.XPath("//button[text()='Send']");
            public static readonly By SecondaryButton = By.XPath("//button[text()='Login as a different user']");
            public static readonly By UserNameTextBoxNew = By.Id("Username");
            public static readonly By PasswordTextBoxNew = By.Id("Password");
            public static readonly By RememberMeCheckboxNEW = By.XPath("//input[@id='RememberLogin']");
        }

        private readonly RemoteWebDriver _driver;
		private readonly string _parentWindow;

        public LoginWindow(RemoteWebDriver driver, string parentWindow)
		{
			_driver = driver;
			_parentWindow = parentWindow;
			driver.WaitUntilPageLoaded();
		}
		
		public void Login(string username, string password, int retries = 1)
		{
			
			var windowHandle = _driver.CurrentWindowHandle;
			_driver.SwitchTo().Window(windowHandle);

			int i = 0;
			do
			{
                _driver.WaitForElementVisible(Elements.PrimaryButton);
				
				EnterUsernameAndPassword(username, password);

                if (!_driver.MoveToClickAndWaitForClosedWindow(Elements.PrimaryButton, TimeSpan.FromSeconds(5)))
                {
                    //The click failed to to login user and close the window:
                    _driver.AddScreenshotToTestContext("Login_Failed");
                }
                else
                {
                    CommonMethods.PlayWait(4000);
                    _driver.SwitchTo().Window(_parentWindow);
                    var primaryButton = _driver.FindElements(Elements.PrimaryButton);
                    if(primaryButton.Any())
                    { primaryButton.FirstOrDefault().Click(); }
                    return;
                }

            } while (i++ < retries);

            var errorText = TryGetErrorMessage(out var msg) ? ", Error: " + msg : "";
			throw new InvalidOperationException($"In login-window: Click on login did not close window. Image added to test. {errorText}");
		}


		private void EnterUsernameAndPassword(string username, string password)
		{
            IWebElement usernameElement = null;
            IWebElement passwordElement = null;
            IWebElement rememberMeElement = null;

            if(_driver.FindElements(Elements.UserNameTextBoxOLD).Any())
            {
                usernameElement = _driver.WaitForElementVisible(Elements.UserNameTextBoxOLD);
            }
            else
            {
                usernameElement = _driver.WaitForElementVisible(Elements.UserNameTextBoxNew);
            }

            if (_driver.FindElements(Elements.PasswordTextBoxOLD).Any())
            {
                passwordElement = _driver.WaitForElementVisible(Elements.PasswordTextBoxOLD);
            }
            else
            {
                passwordElement = _driver.WaitForElementVisible(Elements.PasswordTextBoxNew);
            }

            if (_driver.FindElements(Elements.RememberMeCheckboxOLD).Any())
            {
                rememberMeElement = _driver.WaitForElementVisible(Elements.RememberMeCheckboxOLD);
            }
            else
            {
                rememberMeElement = _driver.WaitForElementVisible(Elements.RememberMeCheckboxNEW);
            }

            usernameElement.Clear();
            usernameElement.SendKeys(username);

            passwordElement.Clear();
            passwordElement.SendKeys(password);

            if (!rememberMeElement.Selected)
                rememberMeElement.Click();
		}

		public bool TryGetErrorMessage(out string errorMessage)
		{
			try
			{
				var element = _driver.FindElement(Elements.ErrorArea);
				if (element.Displayed)
				{
					errorMessage = element.Text;
					return true;
				}
				Console.WriteLine("Found error-element, but is is not displayed.");
			}
			catch { }

			errorMessage = null;
			return false;
		}

        #region Select Modul
        /// <summary>
        /// Select Module
        /// </summary>
        /// <param name="moduleName"></param>
        public void SelectModule(string moduleName = "")
        {
            _driver.WaitForElementVisible(By.XPath("//span[text()='Logout']"));
            selectApplicationModule(_driver, string.IsNullOrEmpty(moduleName) ? ApplicationModules.RecordManagement.GetStringValue() : moduleName);
        }

        #endregion

        #region'select module' 
        /// <summary>
        /// select module
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="moduleName"></param>
        private void selectApplicationModule(RemoteWebDriver driver, string moduleName)
        {
            driver.FindElement(By.XPath(string.Format("//input[@id='{0}']//following::span[@class='thumbnail']", moduleName))).Click(); 
        }
        #endregion


        #region Ecm Selection
        /// <summary>
        /// Ecm Selection
        /// </summary>
        /// <param name="driver"></param>
        public void EcmSelection(RemoteWebDriver driver)
        {
            if (driver.FindElements(Elements.Ecm).Count > 0)
            {
                IWebElement elements = driver.FindElement(Elements.Ecm);
                elements.SendKeys("a@elementsecm.no");
                CommonMethods.PlayWait(2000);
                driver.ClickOnElement(Elements.SendButton);
                CommonMethods.PlayWait(3000);
            }
        }
        #endregion
    }
}                    