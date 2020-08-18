using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.AdminModule
{
    [TestClass]
    [TestCategory("AdminModule")]
    public partial class Vedtaksoppfølging : SeleniumTestBase
    {
        [TestMethod]
        public void AddEditDeleteVedtakstatus()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Vedtaksstatus and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Vedtaksoppfølging", "Vedtaksstatus", "Vedtaksstatus(alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Decision status", "K");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("K");
                adminPage.EditDescription("K", "Description", "Automation Testing",Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteVedtakstype()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Vedtakstyper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Vedtaksoppfølging", "Vedtakstype", "Vedtakstyper(alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Decision code", "AT");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("AT");
                adminPage.EditDescription("AT", "Description", "Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }
    }
}