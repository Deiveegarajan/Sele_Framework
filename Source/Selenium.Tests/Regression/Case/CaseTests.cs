using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using System;
using System.IO;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.Case
{
    [TestClass]
    [TestCategory("BasicSmokeTests")]
    public partial class CaseTests : SeleniumTestBase
    {
        [Ignore]
        [TestMethod]
        public void CreateCase()
        {
            Selenium_Run((driver) =>
            {
                #region logon into Elements
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Create new Case with Title
                CasePage casePage = new CasePage(driver);
                casePage.CreateCase("Smoke Test - Create new case");
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
        }

        [Ignore]
        [TestMethod]
        public void EditCase()
        {
            Run((driver) =>
            {
                #region logon into Elements
                var frontPage = FrontPage.Connect(driver);

                var loginWindow = frontPage.OpenLogin();
                loginWindow.Login("guilt", "guilt");
                loginWindow.SelectModule();
                #endregion

                #region Edit Case Title
                CasePage casePage = new CasePage(driver);
                casePage.CreateCase("Smoke test - Edit Case");
                #endregion

                #region Edit Case Title
                casePage.EditCaseTitle("Smoke test create case has edited");
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
        }

        [Ignore]
        [TestMethod]
        public void EditCaseClassification()
        {
            Run((driver) =>
            {
                #region logon into Elements
                var frontPage = FrontPage.Connect(driver);

                var loginWindow = frontPage.OpenLogin();
                loginWindow.Login("guilt", "guilt");
                loginWindow.SelectModule();
                #endregion

                #region Create new Case
                CasePage casePage = new CasePage(driver);
                casePage.CreateCaseWithClassificationCode("Smoke test - Create new case", "009");
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
        }

        [Ignore]
        [TestMethod]
        public void RedPopUpHandleErrorMessage()
        {
            Run((driver) =>
            {
                #region logon into Elements
                var frontPage = FrontPage.Connect(driver);

                var loginWindow = frontPage.OpenLogin();
                loginWindow.Login("guilt", "guilt");
                #endregion

                #region Create new Case
                CasePage casePage = new CasePage(driver);
                casePage.SelectModule();
                casePage.CreateCase("Create new Case");
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
        }
        [Ignore]
        [TestMethod]
        public void AddDeleteCaseParties()
        {
            Run((driver) =>
            {
                #region logon into Elements
                var frontPage = FrontPage.Connect(driver);

                var loginWindow = frontPage.OpenLogin();
                loginWindow.Login("guilt", "guilt");
                loginWindow.SelectModule();
                #endregion

                #region Create new Case
                CasePage casePage = new CasePage(driver);
                casePage.CreateCase("Smoke test- Add Delete Case Parties");
                #endregion

                #region Add and Delete Case Parties
                casePage.AddParties("Short name", "AA");
                casePage.EditCaseParties("AB");
                casePage.DeleteCaseParties();
                #endregion 

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
        }

        [Ignore]
        [TestMethod]
        public void AddDeleteExternalReceipient()
        {
            Run((driver) =>
            {
                #region logon into Elements
                var frontPage = FrontPage.Connect(driver);

                var loginWindow = frontPage.OpenLogin();
                loginWindow.Login("guilt", "guilt");
                loginWindow.SelectModule();
                #endregion

                #region Create new Case
                CasePage casePage = new CasePage(driver);
                casePage.CreateCase("Smoke test - Create new case");
                #endregion

                #region Add and Delete To address in the Registry Entry  
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);
                regEntry.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                regEntry.AddRegTitle("Registry entry title");
                regEntry.AddRegToAddress("AA");
                regEntry.AddMultipleRegToAddress("AB");
                regEntry.ClickSaveBttn();
                regEntry.EditRegistryToAddress("Aktiebolaget Bolinda", "elements@evry.com");
                regEntry.ClickSaveBttn();
                regEntry.DeleteRegistrEntryToAddress("Aktiebolaget Bolinda");
                regEntry.ClickSaveBttn();
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
        }
        [Ignore]
        [TestMethod]
        public void RegistryEntry()
        {
            Run((driver) =>
            {
                #region logon into Elements
                var frontPage = FrontPage.Connect(driver);

                var loginWindow = frontPage.OpenLogin();
                loginWindow.Login("guilt", "guilt");
                loginWindow.SelectModule();
                #endregion

                #region Create new Case
                CasePage casePage = new CasePage(driver);
                casePage.CreateCase("Smoke test - Create new case");
                #endregion

                #region Registry Entry 
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);
                regEntry.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                regEntry.AddRegTitle("Registry entry title");
                regEntry.AddRegToAddress("AA");
                regEntry.ClickSaveBttn();
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion

            });
        }

        [Ignore]
        [TestMethod]
        public void EditRegistryEntryTitle()
        {
            Run((driver) =>
            {
                #region logon into Elements
                var frontPage = FrontPage.Connect(driver);

                var loginWindow = frontPage.OpenLogin();
                loginWindow.Login("guilt", "guilt");
                loginWindow.SelectModule();
                #endregion

                #region Create new Case
                CasePage casePage = new CasePage(driver);
                casePage.CreateCase("Smoke test - Create new case");
                #endregion

                #region Registry Entry 
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);
                regEntry.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                regEntry.AddRegTitle("Registry entry title");
                regEntry.AddRegToAddress("AA");
                regEntry.ClickSaveBttn();
                #endregion

                #region Edit Registry entry title
                regEntry.EditRegistryEntryTitle("Registry entry title has edited");
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
        }

        [Ignore]
        [TestMethod]
        public void CasePartyScreenedOrRestricted()
        {
            // #Case 138908
            #region Private variables
            var caseId = string.Empty; var caseId2 = string.Empty;
            var caseScreenedTitle = string.Format("Case party screened or restricted");
            var caseUnScreenedTitle = string.Format("Case party unscreened not restricted");
            #endregion

            #region Case Level - Step 1 to 4
            //Pre-requisite
            #region ---- Admin Module - Prereq ----
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Tilgangskoder and check the Access code is 6 is setup
                AdminstratorPage adminPage = new AdminstratorPage(driver);
                adminPage.NavigateToMenuItem("Brukeradministrasjon/tilgangsstyring", "Tilgangskoder", "Tilgangskoder (alle)");
                adminPage.VerifyAccessCodeExist("6");
                adminPage.ClickItemInGrid("6");
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide title automatically when creating new post", true);
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide sender/recipient automatically when creating new post", true);
                adminPage.Save();
                #endregion

                #region Guilt User - authorized for access code 6 for adm unit ENG
                adminPage.NavigateToMenuItem("Person", "Person");

                adminPage.SelectPerson("GUILT");
                adminPage.AddOrEditAutorization("6", "Adm.unit", "ENG");
                #endregion

                #region Sda User - is NOT Authorized for access code 6 on adm unit ENG
                adminPage.ClickDropDownInAdminModule("Person");
                adminPage.SelectPerson("SDA");

                adminPage.RemoveAutorization("6", "SDA");
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 1-3 
            #region Login to RM module with Guilt user as TstIndia1
            Selenium_Run((driver) =>
            {
                #region Login to RM Module as User 1 -Guilt
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Create new Case and edit title 
                CasePage casePage = new CasePage(driver);

                casePage.AddTitle(caseScreenedTitle);
                casePage.SelectKendoDropdownAndAddValue("Access code", "6");
                casePage.SaveCase();
                caseId = casePage.GetCaseNumber();
                #endregion

                #region create a new case party and add restricted (checkbox)
                casePage.AddParties("Short name", "AA");
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 4
            #region Login to RM module with user 2 SDA
            Selenium_Run((driver) =>
            {
                #region Login to RM Module as User 2 - SDA User
                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");
                #endregion

                #region Open Case Number and verify the case parties is resticted 
                CasePage casePage = new CasePage(driver);
                casePage.QuickSearchCaseId(caseId);
                casePage.VerifyCasePartryIsRestricted("*****", true);
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 5 
            #region Login to RM module with user 1 Guilt
            Selenium_Run((driver) =>
            {
                #region Login to RM Module as User 1 - Guilt User
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Open Case Number and verify the case parties is resticted 
                CasePage casePage = new CasePage(driver);
                casePage.CreateCase(caseUnScreenedTitle);
                caseId2 = casePage.GetCaseNumber();
                #endregion

                #region create a new case party and add restricted (checkbox)
                casePage.AddParties("Short name", "AA", false);
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            #region Login to RM module with user 2 SDA
            Selenium_Run((driver) =>
            {
                #region Login to RM Module as User 2 - SDA User
                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");
                #endregion

                #region Open Case Number and verify the case parties is resticted 
                CasePage casePage = new CasePage(driver);
                casePage.QuickSearchCaseId(caseId2);
                casePage.VerifyCasePartryIsRestricted("Anda Apotek", false);
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion
            #endregion
        }

    }
}
