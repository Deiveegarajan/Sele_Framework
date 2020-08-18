using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using System;

namespace Selenium.Tests.Pages
{
    public class AccessCodePage
    {
        public static class AccessCodePageElements
        {
            public static readonly By C = By.XPath("");
            public static readonly By Us = By.XPath("");
            public static readonly By lo = By.XPath("");
        }
        private readonly RemoteWebDriver _driver;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Google Home screen class.
        /// </summary>
        /// <param name="driver">.</param>
        public AccessCodePage(RemoteWebDriver driver)
        {
            _driver = driver;
        }

        public static AccessCodePage Connect(RemoteWebDriver driver)
        {
            return new AccessCodePage(driver);
        }
        #endregion


    } 
}
