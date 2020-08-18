using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using System;
using System.Linq;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Pages
{
    public class DashboardPage
    {
        public static class DashboardPageLocators
        {
            public static readonly By ExpandCollapseMenuLocator = By.XPath("//div[@class='header-bar rm-em-sa-header-bar']//following::button[contains(@class,'btn-menu-toggle')][1]");
            public static readonly By CollapseMenuLocator = By.XPath("//button[@class='btn btn-md btn-link pull-left btn-menu-toggle']");
            public static readonly By UserCaret = By.XPath("//div[@class='user-caret']");
            public static readonly By SelectRole = By.XPath("//ul[@class='dropdown-menu user-dropdown-menu dropdown-menu show']//li[1]//button[@class='btn-block btn-as-dropdown-link menu-item-dropdown no-border dropdown-item btn btn-default']");
            public static readonly By MainRegistrar = By.XPath("//ul[@class='dropdown-menu show']//li//button[text()='Main registrar - Eng']");
            public static readonly By Administrator = By.XPath("");
            public static readonly By Secretary = By.XPath("//ul[@class='dropdown-menu show']//li//button[text()='Secretary of the Board - Eng']");
            public static readonly By CaseWorkerENG = By.XPath("//ul[@class='dropdown-menu show']//li//button[text()='Caseworker - ENG']");
        }

        private readonly RemoteWebDriver _driver;

        /// <summary>
        /// constructor
        /// Edit document
        /// </summary >
        /// <param name="driver"></param>
        public DashboardPage(RemoteWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static DashboardPage Connect(RemoteWebDriver driver)
        {
            return new DashboardPage(driver);
        }

        #region Methods

        /// <summary>
        /// Implicityly Wait for milliseconds. Default wait time
        /// </summary>
        public void ExpandMenu()
        {
            CommonMethods.PlayWait(3000);
            IWebElement ExpandMenuWebElement = MenuControlCollapsedElement(_driver);
            ExpandMenuWebElement.DrawHighlight();
            ExpandMenuWebElement.Click();
        }

        /// <summary>
        /// Menu Control Collapsed Element
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public IWebElement MenuControlCollapsedElement(object o)
        {
            IWebDriver webDriver = (IWebDriver)o;
            IWebElement collapseElement = null;
            var collapseElements = _driver.FindElements(DashboardPageLocators.CollapseMenuLocator);
            if (collapseElements.Any())
            {
                collapseElement = collapseElements.FirstOrDefault();
            }
            return collapseElement;
        }

        /// <summary>
        /// Check if the menu is collapsed
        /// </summary>
        /// <returns></returns>
        public bool IsMenuControlCollapsed()
        {
            IWebElement ExpandMenuWebElement = MenuControlCollapsedElement(_driver);
            return (ExpandMenuWebElement != null) ? SeleniumExtensions.VerifyIsElementPresent(ExpandMenuWebElement) : false;
        }
        #endregion

        #region
        public void ChangeRole(Enum RolesOfPerson)
        {
            try
            {
                CommonMethods.PlayWait(5000);
                // Expand Left menu
                if (IsMenuControlCollapsed())
                    ExpandMenu();

                // Expand User Caret to click Logout
                
                SeleniumExtensions.ClickOnElement(_driver, DashboardPageLocators.UserCaret);
                
                SeleniumExtensions.ClickOnElement(_driver, DashboardPageLocators.SelectRole);
                CommonMethods.PlayWait(3000);

                switch (RolesOfPerson)
                {
                    case RolesInApplication.MainRegistrar:
                        _driver.ClickOnElement(DashboardPageLocators.MainRegistrar);
                        break;

                    case RolesInApplication.Administrator:
                        _driver.ClickOnElement(DashboardPageLocators.Administrator);
                        break;

                    case RolesInApplication.CaseWorkerENG:
                        _driver.ClickOnElement(DashboardPageLocators.CaseWorkerENG);
                        break;
                    case RolesInApplication.SecretaryOfTheBoard:
                        _driver.ClickOnElement(DashboardPageLocators.Secretary);
                        break;
                }
            }
            catch (Exception)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to change role \n" + RolesOfPerson.GetStringValue());
            }
        }
        #endregion
    }
}
