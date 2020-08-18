using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using System;

namespace Selenium.Tests.Pages
{
    class LogoutPage
    {
        public static class LogoutPageElements
        {
            public static readonly By ChangeLanguage = By.XPath("//select[@class='select-container form-control']");
            public static readonly By UserCaret = By.XPath("//div[@class='user-caret']");
            public static readonly By logout = By.XPath("//button[@class='btn-block btn-as-dropdown-link menu-item-button logout-container dropdown-item']");
        }
        private readonly RemoteWebDriver _driver;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Google Home screen class.
        /// </summary>
        /// <param name="driver">.</param>
        public LogoutPage(RemoteWebDriver driver)
        {
            _driver = driver;
        }

        public static LogoutPage Connect(RemoteWebDriver driver)
        {
            return new LogoutPage(driver);
        }
        #endregion 

        #region  Methods

        /// <summary>
        /// Login to Application
        /// </summary>
        /// <param name="searchContent">Parameter of type Syste.String for searchContent</param>
        /// <param name="moduleName">Module Name to login</param>
        /// <returns>Parameter of type System.Boolean for True or False</returns>
        public void LogoutApplication()
        {
            try
            {
                DashboardPage dashboardPage = new DashboardPage(_driver);
                // Expand Left menu
                if (dashboardPage.IsMenuControlCollapsed())
                    dashboardPage.ExpandMenu();

                // Expand User Caret to click Logout
                SeleniumExtensions.ClickOnElement(_driver, LogoutPageElements.UserCaret);

                // Click Logout button
                _driver.WaitForElementVisible(LogoutPageElements.logout);
                _driver.ClickOnElement(LogoutPageElements.logout);

                //Clear browser cookies
                _driver.Manage().Cookies.DeleteAllCookies();
            }
            catch(Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to expand left menu in Dashboard \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);         
            }
        }
    }
    #endregion
}
