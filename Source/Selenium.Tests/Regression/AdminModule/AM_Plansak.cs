using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using System;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.AdminModule
{
    [TestClass]
    [TestCategory("AdminModule")]
    public partial class Plansak : SeleniumTestBase
    {
        [TestMethod]
        public void AddEditDeletePlanbestemmelser()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Planbestemmelser and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Plansak", "Planbestemmelser", "Planbestemmelser (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "AT");
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
        public void AddEditDeletePlanStatus()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Planstatus and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Plansak", "Planstatus", "Planstatus (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "AT");
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
        public void AddEditDeletePlanType()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Plantype and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Plansak", "Plantype", "Plantype (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "199");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("199");
                adminPage.EditDescription("199", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteReguleringsformal()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Reguleringsformål and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Plansak", "Reguleringsformål", "Reguleringsformål (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "10");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyAccessCodeExist("10");
                adminPage.EditDescription("10", "Description", "Edit_Automation Testing", Description.TextBox);
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