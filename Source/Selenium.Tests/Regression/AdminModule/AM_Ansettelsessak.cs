using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using System;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.AdminModule
{
    [TestClass]
    [TestCategory("AdminModule")]
    public partial class Ansettelsessak : SeleniumTestBase
    {
        [TestMethod]
        public void AddEditDeleteKunngjoringsmater()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Kunngjøringsmåter and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Ansettelsessak", "Kunngjøringsmåter", "Kunngjøringsmåter (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Announcement method", "AT");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("AT");
                adminPage.EditDescription("AT", "Description", "Edit Automation Testing",Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteStillingstyper()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Stillingstyper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Ansettelsessak", "Stillingstyper", "Stillingstyper (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Position type", "AT");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("AT");
                adminPage.EditDescription("AT", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }
            
        [TestMethod]
        public void AddEditDeleteStillingsstatus()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Vedtakstyper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Ansettelsessak", "Stillingsstatus", "Stillingsstatus (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Position status", "11");
                adminPage.SetTextBox("Description", "Automation_Testing");
                adminPage.Save();
                adminPage.VerifyDesctiptionIsExist("11");
                adminPage.EditDescription("11", "Description", "Edit_Automation Testing", Description.TextBox);
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