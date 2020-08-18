using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using System;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.AdminModule
{
    [TestClass]
    [TestCategory("AdminModule")]
    public partial class Arkivstruktur : SeleniumTestBase
    {
        [TestMethod]
        public void AddEditDeleteArkiv()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Arkiv and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstruktur", "Arkiv", "Arkiv (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "GD");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("GD");
                adminPage.EditDescription("GD", "Description", "Edit Automation Testing",Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteArkivdeler()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Arkivdeler and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstruktur", "Arkivdeler", "Arkivdeler (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Record section", "AT");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.SelectHtmlComboBox("Registry office", "Edit Automation Testing");
                adminPage.SelectHtmlComboBox("Primary organizing principle", "K-Kode fagklasse/fellesklasse bokmål");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("AT", "Description", "Edit_Automation Testing", Description.TextBox,false);
                adminPage.ClickDeleteAndConfirmDelete();
                

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }
            
        [TestMethod]
        public void AddEditDeleteArkivstatus()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Vedtakstyper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstruktur");
                adminPage.NavigateToMenuItem("Arkivstatus", "Arkivstatus");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Registrystat", "D");
                adminPage.SetTextBox("Status description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("D");
                adminPage.EditDescription("D", "Status description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteBortsettingskoder()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Bortsettingskoder and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstruktur", "Bortsettingskoder", "Bortsettingskoder (Alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Filing code", "D");
                adminPage.SetTextBox("Description", "Automation_Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("D");
                adminPage.EditDescription("D", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteJournalenhet()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Journalenhet and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstruktur", "Journalenhet", "Journalenhet (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Registry management unit", "AT");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("AT", "Description", "Edit_Automation Testing", Description.TextBox, false);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteOrdningsprinsipp()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Vedtakstyper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstruktur", "Ordningsprinsipp", "Ordningsprinsipp (Alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Principles for order values", "AT");
                adminPage.SetTextBox("Caption", "OrdiningsCaption");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("AT", "Description", "Edit_Automation Testing", Description.TextBox, false);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteTypeordningsprinsipp()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Bortsettingskoder and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstruktur");
                adminPage.NavigateToMenuItem("Type ordningsprinsipp", "Type ordningsprinsipp");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Type order principles", "AT");
                adminPage.SetTextBox("Description", "Automation_Testing");
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
    }
}