using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using System;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.AdminModule
{
    [TestClass]
    [TestCategory("AdminModule")]
    public partial class Byggesak : SeleniumTestBase
    {
        [TestMethod]
        public void AddEditDeleteBygningstyper()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Bygningstyper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Byggesak", "Bygningstyper", "Bygningstyper (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "114");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("114");
                adminPage.EditDescription("114", "Description", "Edit Automation Testing",Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteTiltakstyper()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Tiltakstyper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Byggesak", "Tiltakstyper", "Tiltakstyper (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "10");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("10");
                adminPage.EditDescription("10", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }
            
        [TestMethod]
        public void AddEditDeleteGebyrtyper()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                var time = DateTime.Now;  //12.02.2020 07:04:05   4:25 5:00
                string fromDate = time.ToString("dd.MM.yyyy");

                #region Navigate to Vedtakstyper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Byggesak", "Gebyrtyper", "Gebyrtyper (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Description", "Automation_Testing");
                adminPage.SetTextBox("Cost", "1000");
                adminPage.SetTextBox("From date", fromDate);
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("Automation_Testing", "Description", "Edit_Automation Testing", Description.TextBox, false);
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