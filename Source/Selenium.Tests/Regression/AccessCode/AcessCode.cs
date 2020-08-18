using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using System;
using System.IO;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.AccessCode
{
    [TestClass]
    [TestCategory("AccessCode")]
    public partial class AccessCode : SeleniumTestBase
    {
        [TestMethod]
        public void AccessCodeManualScreening_05()
        {
            //TC#138846

            #region Private Variables
            var caseId = string.Empty;
            var getRandomNumberCase = CommonMethods.GetRandomNumber();
            var getRandomNumberRE = CommonMethods.GetRandomNumber();

            var caseTitle = string.Format("Text1 Text2 and this is Omar Lie and Robert Vabo and more {0}", getRandomNumberCase);
            var registryEntryTitle = string.Format("Text1 Text2 and this is Omar Lie and Robert Vabo and more {0}", getRandomNumberRE);
            var screenedRegistryTitleFull = string.Format("***** Text2 and this is Omar Lie and ***** ***** and ***** {0}", getRandomNumberRE);
            #endregion

            #region Case Level - Step 1 to 8
            //Step 1
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
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide title automatically when creating new post", false);
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide sender/recipient automatically when creating new post", false);
                adminPage.Save();
                #endregion

                #region Guilt User - authorized for access code 6 for adm unit ENG
                adminPage.NavigateToMenuItem("Person", "Person");

                adminPage.SelectPerson("GUILT");
                adminPage.AddOrEditAutorization("6", "Adm.unit", "ENG");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Sda User - is NOT Authorized for access code 6 on adm unit ENG
                adminPage.ClickDropDownInAdminModule("Person");
                adminPage.SelectPerson("SDA");

                adminPage.RemoveAutorization("6", "SDA");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion

            });
            #endregion

            //Step 2,3
            #region Login to RM module with Guilt user as TstIndia1
            Selenium_Run((driver) =>
            {
                #region Login to RM Module
                FrontPage.LoginToApplication(driver);
                #endregion

                //Step 3
                #region Create new Case and edit title 
                CasePage casePage = new CasePage(driver);

                casePage.AddTitle(caseTitle);
                casePage.ClickMoreDetailsButtonInCreateCase();
                casePage.SelectKendoDropdownAndAddValue("Access code", "6");
                casePage.SaveCase();

                casePage.EditCaseTitle();

                // Mark text as Screened and check the text color
                casePage.EditCaseTitleScreenedTextOrPersonName("Text1", TitleModify.Screened.GetStringValue());

                // Mark text as person name and check the text format
                casePage.EditCaseTitleScreenedTextOrPersonName("Omar", TitleModify.PersonName.GetStringValue(), "Lie");

                //Mark text as Screened and Persons name and check the text color and text format
                casePage.EditCaseTitleScreenedTextOrPersonName("Robert", TitleModify.Screened.GetStringValue(), "Vabo");
                casePage.EditCaseTitleScreenedTextOrPersonName("Robert", TitleModify.PersonName.GetStringValue(), "Vabo");

                // Mark text as Screened and check the text color
                casePage.EditCaseTitleScreenedTextOrPersonName("more", TitleModify.Screened.GetStringValue());


                casePage.SaveCase();

                casePage.VerifyCaseTitleColorAndFormat("Text1", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Omar", TitleBackgroundFormat.Italic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Robert", TitleBackgroundFormat.RedWithItalic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Vabo", TitleBackgroundFormat.RedWithItalic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("more", TitleBackgroundFormat.Red.GetStringValue());

                caseId = casePage.GetCaseNumber();
                #endregion

                #region Logout from RM Module 
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion

            });
            #endregion

            //Step 4. 5 
            #region Login to RM module with SDA user as TstIndia2
            Selenium_Run((driver) =>
            {
                //Step 4
                CasePage casePage = new CasePage(driver);
                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");
                casePage.QuickSearchCaseId(caseId);
                var screenedFullCaseTitle = casePage.GetScreenedCaseTitle();

                #region Verify text in case title Marked as Red stars(Screened) and Italic(Marked as Person name)
                //Verify the text is Red star screened
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 0, TitleBackgroundFormat.Red.GetStringValue(), 0);

                //Verify the text is visible with blue color
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("Text2", 1, TitleBackgroundFormat.Blue.GetStringValue());

                //Verify the text is Visible with italic and blue color
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("Omar", 5, TitleBackgroundFormat.Italic.GetStringValue(), 0, "Lie");

                //Verify the text is Red stars
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 8, TitleBackgroundFormat.Red.GetStringValue(), 1);
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 9, TitleBackgroundFormat.Red.GetStringValue(), 2);

                //Verify the text is Red star screened
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 11, TitleBackgroundFormat.Red.GetStringValue(), 3);
                #endregion


                //Step 5
                #region Scenario 1 - Edit Case and Validate case title
                casePage.EditCaseTitle();
                #region Verify text in case title Marked as Red stars(Screened) and Italic(Marked as Person name)
                //Verify the text is Red star screened
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 0, TitleBackgroundFormat.Red.GetStringValue(), 0);

                //Verify the text is visible with blue color
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("Text2", 1, TitleBackgroundFormat.Blue.GetStringValue());

                //Verify the text is Visible with italic and blue color
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("Omar", 5, TitleBackgroundFormat.Italic.GetStringValue(), 0, "Lie");

                //Verify the text is Red stars
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 8, TitleBackgroundFormat.Red.GetStringValue(), 1);
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 9, TitleBackgroundFormat.Red.GetStringValue(), 2);

                //Verify the text is Red star screened
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 11, TitleBackgroundFormat.Red.GetStringValue(), 3);
                #endregion

                //validate case title is editable
                casePage.ValidateCaseOrRegistryTitleEditable();
                casePage.ClickCancelEditCaseTitleButton();
                #endregion

                #region Scenario 2 - Validate case in SAKSMAPPER
                casePage.ClickOnLeftDashboardMenuItem("Saksmapper");
                casePage.SendTextToTextBoxField("Date", "0");
                casePage.ClickOnSearchCriteriaButton();
                casePage.ClickOnListOrGridViewInCaseList();
                CommonMethods.PlayWait(5000);
                casePage.VerifyCaseTitleInSaksmapper(caseId, screenedFullCaseTitle);
                #endregion

                #region Scenario 3 - Validate case in Recent Cases
                #region i) List view in recent cases 
                casePage.ClickOnLeftDashboardMenuItem("Siste saker / recent cases");
                casePage.ClickOnListOrGridViewInCaseList();
                casePage.VerifyCaseTitleInSaksmapper(caseId, screenedFullCaseTitle);
                #endregion

                #region ii) Grid view in recent cases 
                casePage.ClickOnListOrGridViewInCaseList("Grid");
                casePage.VerifyCaseTitleInRecentCasesGridView(caseId, screenedFullCaseTitle);
                #endregion

                #region Logout from RM Module    
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion

                #endregion

            });
            #endregion

            //Step 6, 7
            #region Login to RM module with Guilt user as TstIndia1
            Selenium_Run((driver) =>
            {
                //Step 6
                #region Login to RM Module
                FrontPage.LoginToApplication(driver);
                CasePage casePage = new CasePage(driver);
                casePage.QuickSearchCaseId(caseId);
                casePage.EditCaseTitle();
                #endregion

                #region Verify the title color and format
                casePage.VerifyCaseTitleColorAndFormat("Text1", TitleBackgroundFormat.Red.GetStringValue(), "CaseTitleEditMode");
                casePage.VerifyCaseTitleColorAndFormat("Omar", TitleBackgroundFormat.Italic.GetStringValue(), "CaseTitleEditMode");
                casePage.VerifyCaseTitleColorAndFormat("Robert", TitleBackgroundFormat.RedWithItalic.GetStringValue(), "CaseTitleEditMode");
                casePage.VerifyCaseTitleColorAndFormat("Vabo", TitleBackgroundFormat.RedWithItalic.GetStringValue(), "CaseTitleEditMode");
                casePage.VerifyCaseTitleColorAndFormat("more", TitleBackgroundFormat.Red.GetStringValue(), "CaseTitleEditMode");
                casePage.ClickCancelEditCaseTitleButton();
                #endregion

                //Step 7
                #region Remove the screening of title 
                casePage.EditCaseTitle();
                casePage.EditCaseTitleScreenedTextOrPersonName("Text1", TitleModify.RemoveScreeningFromText.GetStringValue(), "more");
                casePage.SaveCase();

                #region Verify all the text is unscreened but title has ITALICS
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("Text1", 0, TitleBackgroundFormat.Blue.GetStringValue());
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("Omar", 5, TitleBackgroundFormat.Italic.GetStringValue(), 0, "Lie");
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("Robert", 8, TitleBackgroundFormat.Italic.GetStringValue());
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("Vabo", 9, TitleBackgroundFormat.Italic.GetStringValue());
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("more", 11, TitleBackgroundFormat.Blue.GetStringValue());
                #endregion

                #region Logout from RM Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion

            });
            #endregion

            //Step 8
            #region Login to RM module with SDA user as TstIndia2
            Selenium_Run((driver) =>
            {
                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");
                CasePage casePage = new CasePage(driver);
                casePage.QuickSearchCaseId(caseId);
                casePage.EditCaseTitle();

                #region Verify the text is unscreened and Italic
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("Text1", 0, TitleBackgroundFormat.Blue.GetStringValue());
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("Omar", 5, TitleBackgroundFormat.Italic.GetStringValue(), 0, "Lie");
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("Robert", 8, TitleBackgroundFormat.Italic.GetStringValue());
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("Vabo", 9, TitleBackgroundFormat.Italic.GetStringValue());
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("more", 11, TitleBackgroundFormat.Blue.GetStringValue());
                #endregion

                #region Logout from RM Module   
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion

            });
            #endregion
            #endregion

            #region Registry Level - Step 9

            //Step 2,3
            #region ---- Login to RM module with Guilt user as TstIndia 1 ----
            Selenium_Run((driver) =>
            {
                LogoutPage logoutPage = new LogoutPage(driver);

                #region Login to RM Module
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Open case created
                CasePage casePage = new CasePage(driver);
                casePage.QuickSearchCaseId(caseId);
                #endregion

                #region Add Registry Entry with access code
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);
                regEntry.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                regEntry.AddRegTitle(registryEntryTitle);
                regEntry.AddRegToAddress("AA");
                regEntry.SelectKendoDropdownAndAddValue("Access code", "6");
                regEntry.ClickSaveBttn();
                #endregion

                #region Mark as screened and Mark as Person in Reg Entry Title

                #region Edit Registry Entry
                regEntry.ClickEditButton();
                #endregion

                #region Mark "Text1" & select "Mark text as screened"
                regEntry.EditRegistryTitleScreenedTextOrPersonName("Text1", TitleModify.Screened.GetStringValue());
                #endregion

                #region Mark "Omar Lie" and select "mark text as persons name"
                regEntry.EditRegistryTitleScreenedTextOrPersonName("Omar", TitleModify.PersonName.GetStringValue(), "Lie");
                #endregion

                #region Mark "Robert Vabo" and select "Mark text as screened"
                regEntry.EditRegistryTitleScreenedTextOrPersonName("Robert", TitleModify.Screened.GetStringValue(), "Vabo");
                #endregion

                #region Mark "Robert Vabo" and select "Mark as Person"
                regEntry.EditRegistryTitleScreenedTextOrPersonName("Robert", TitleModify.PersonName.GetStringValue(), "Vabo");
                #endregion

                #region Mark "More" and select "Mark text as screened"
                regEntry.EditRegistryTitleScreenedTextOrPersonName("more", TitleModify.Screened.GetStringValue());
                #endregion

                regEntry.ClickSaveBttn();

                #endregion

                #region Verify Text in Reg Entry Title marked as screened and marked as person
                regEntry.VerifyRegistryTitleColorAndFormat("Text1", TitleBackgroundFormat.Red.GetStringValue());
                regEntry.VerifyRegistryTitleColorAndFormat("Omar", TitleBackgroundFormat.Italic.GetStringValue());
                regEntry.VerifyRegistryTitleColorAndFormat("Robert", TitleBackgroundFormat.RedWithItalic.GetStringValue());
                regEntry.VerifyRegistryTitleColorAndFormat("Vabo", TitleBackgroundFormat.RedWithItalic.GetStringValue());
                regEntry.VerifyRegistryTitleColorAndFormat("more", TitleBackgroundFormat.Red.GetStringValue());
                #endregion

                #region Logout from Application
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 4,5
            #region ---- Login to RM module with Sda User as TST India 2 ----
            Selenium_Run((driver) =>
            {
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                CasePage casePage = CasePage.Connect(driver);
                RegistryEntryPage regEntry = RegistryEntryPage.Connect(driver);

                #region Login to Saksbehandling with different user
                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");
                #endregion

                #region Find the case
                casePage.QuickSearchCaseId(caseId);
                CommonMethods.PlayWait(5000);
                var screenedFullRegistryTitle = regEntry.GetScreenedRegistryTitle();
                #endregion

                #region Verify Text in Registry Title marked as red stars (screened) and italic (marked as person)

                //Verify the text is Red star screened
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("*****", 0, TitleBackgroundFormat.Red.GetStringValue(), 0);

                //Verify the text is visible with blue color
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Text2", 1, TitleBackgroundFormat.Blue.GetStringValue());

                //Verify the text is Visible with italic and blue color
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Omar", 5, TitleBackgroundFormat.Italic.GetStringValue(), 0, "Lie");

                //Verify the text is Red stars
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("*****", 8, TitleBackgroundFormat.Red.GetStringValue(), 1);
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("*****", 9, TitleBackgroundFormat.Red.GetStringValue(), 2);

                //Verify the text is Red star screened
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("*****", 11, TitleBackgroundFormat.Red.GetStringValue(), 3);
                #endregion

                #region Adhoc Testing for Registry Title

                #region Scenario 1 - Validate Registry entry title in Journalposter

                #region  In List view
                CommonMethods.PlayWait(2000);
                #region List view in Journalposter

                casePage.ClickOnLeftDashboardMenuItem("Journalposter");
                casePage.SendTextToTextBoxField("Record date", "0");
                casePage.SendTextToTextBoxField("Contents", "");
                casePage.ClickOnSearchButtonInFilter();
                casePage.ClickOnListOrGridViewInCaseList();
                regEntry.ClickRegistry(screenedRegistryTitleFull, false);
                CommonMethods.PlayWait(5000);
                #endregion

                // Validate Registry Title in List view of Journalposter
                #region Verify Text in Registry Title marked as red stars (screened) and italic (marked as person)

                //Verify the text is Red star screened
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("*****", 0, TitleBackgroundFormat.Red.GetStringValue(), 0);

                //Verify the text is visible with blue color
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Text2", 1, TitleBackgroundFormat.Blue.GetStringValue());

                //Verify the text is Visible with italic and blue color
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Omar", 5, TitleBackgroundFormat.Italic.GetStringValue(), 0, "Lie");

                //Verify the text is Red stars
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("*****", 8, TitleBackgroundFormat.Red.GetStringValue(), 1);
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("*****", 9, TitleBackgroundFormat.Red.GetStringValue(), 2);

                //Verify the text is Red star screened
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("*****", 11, TitleBackgroundFormat.Red.GetStringValue(), 3);
                #endregion
                #endregion
                #endregion

                #region Scenario 2 - Validate Registry entry title is editable but not updated In Journalposter -> List view

                CommonMethods.PlayWait(2000);
                regEntry.VerifyRegistryEntryTitleEditableInJournalposter(caseId, screenedRegistryTitleFull, " Edit Registry Title In Journalposter");

                #endregion

                #endregion

                #region Logout from Application
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 6,7
            #region ---- Login to RM module with Guilt User as TST India 1 ----
            Selenium_Run((driver) =>
            {
                LogoutPage logoutPage = new LogoutPage(driver);
                CasePage casePage = new CasePage(driver);
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);

                #region Login to Saksbehandling
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Find the case
                casePage.QuickSearchCaseId(caseId);
                #endregion

                #region Verify Text in Registry Entry Title marked as screened and marked as person
                CommonMethods.PlayWait(5000);
                regEntry.VerifyRegistryTitleColorAndFormat("Text1", TitleBackgroundFormat.Red.GetStringValue());
                regEntry.VerifyRegistryTitleColorAndFormat("Omar", TitleBackgroundFormat.Italic.GetStringValue());
                regEntry.VerifyRegistryTitleColorAndFormat("Robert", TitleBackgroundFormat.RedWithItalic.GetStringValue());
                regEntry.VerifyRegistryTitleColorAndFormat("Vabo", TitleBackgroundFormat.RedWithItalic.GetStringValue());
                regEntry.VerifyRegistryTitleColorAndFormat("more", TitleBackgroundFormat.Red.GetStringValue());

                #endregion

                #region Remove the above screening of title (rightclick in the title field and select Remove screening)

                regEntry.ClickEditButton();
                CommonMethods.PlayWait(5000);

                regEntry.EditRegistryTitleScreenedTextOrPersonName("Text1", TitleModify.RemoveScreeningFromText.GetStringValue(), "more");

                regEntry.ClickSaveBttn();

                #endregion

                #region All text is now unscreened (no red colour) but names are still italic (marked as person name)
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Text1", 0, TitleBackgroundFormat.Blue.GetStringValue());
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Omar", 5, TitleBackgroundFormat.Italic.GetStringValue(), 0, "Lie");
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Robert", 8, TitleBackgroundFormat.Italic.GetStringValue());
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Vabo", 9, TitleBackgroundFormat.Italic.GetStringValue());
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("more", 11, TitleBackgroundFormat.Blue.GetStringValue());
                #endregion

                #region All text is now unscreened (no red colour) but names are still italic (marked as person name) in Edit Mode

                regEntry.ClickEditButton();

                #region All text is now unscreened (no red colour) but names are still italic (marked as person name)
                regEntry.VerifyRegistryTitleColorAndFormat("Text1", TitleBackgroundFormat.Blue.GetStringValue(), "Edit");
                regEntry.VerifyRegistryTitleColorAndFormat("Omar", TitleBackgroundFormat.Italic.GetStringValue(), "Edit");
                regEntry.VerifyRegistryTitleColorAndFormat("Lie", TitleBackgroundFormat.Italic.GetStringValue(), "Edit");
                regEntry.VerifyRegistryTitleColorAndFormat("Robert", TitleBackgroundFormat.Italic.GetStringValue(), "Edit");
                regEntry.VerifyRegistryTitleColorAndFormat("Vabo", TitleBackgroundFormat.Italic.GetStringValue(), "Edit");
                regEntry.VerifyRegistryTitleColorAndFormat("more", TitleBackgroundFormat.Blue.GetStringValue(), "Edit");
                #endregion

                regEntry.ClickCancelButton();
                #endregion

                #region Logout from Application
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 8
            #region ---- Login to RM module with SDA user as TstIndia 2 ----
            Selenium_Run((driver) =>
            {
                LogoutPage logoutPage = new LogoutPage(driver);
                CasePage casePage = new CasePage(driver);
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);


                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");

                casePage.QuickSearchCaseId(caseId);
                CommonMethods.PlayWait(5000);
                #region Verify the text is unscreened and Italic. The user sda should be able to see the whole title (nothing screened) but names are in italic
                CommonMethods.PlayWait(5000);
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Text1", 0, TitleBackgroundFormat.Blue.GetStringValue());
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Omar", 5, TitleBackgroundFormat.Italic.GetStringValue(), 0, "Lie");
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Robert", 8, TitleBackgroundFormat.Italic.GetStringValue());
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Vabo", 9, TitleBackgroundFormat.Italic.GetStringValue());
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("more", 11, TitleBackgroundFormat.Blue.GetStringValue());
                #endregion

                #region Logout from RM Module                  
                logoutPage.LogoutApplication();
                #endregion

            });
            #endregion

            #endregion

            #region Report Level - Step 10
            //Step 10
            #region ---- Login to RM module with GUILT user as TstIndia 1 ----
            Selenium_Run((driver) =>
            {
                FrontPage.LoginToApplication(driver);

                #region Changing role
                DashboardPage dashboardPage = new DashboardPage(driver);
                dashboardPage.ChangeRole(RolesInApplication.MainRegistrar);
                #endregion

                #region Search case
                CasePage casePage = new CasePage(driver);
                CommonMethods.PlayWait(8000);
                casePage.QuickSearchCaseId(caseId);
                #endregion

                #region Change Registry entry Status into J
                CommonMethods.PlayWait(5000);
                // Click Set Status j
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);
                regEntry.ClickRegistryLevelCheckBox("this");
                CommonMethods.PlayWait(2000);
                regEntry.ClickRegistryLevelOptions("Set status J");
                CommonMethods.PlayWait(3000);
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            #region Login to RM module with SDA user as TstIndia2
            Selenium_Run((driver) =>
            {
                #region Login to application
                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");
                #endregion

                #region Search case
                CasePage casePage = new CasePage(driver);
                CommonMethods.PlayWait(8000);
                casePage.QuickSearchCaseId(caseId);
                CommonMethods.PlayWait(5000);
                #endregion

                #region Check the different reports and verify that parts of Registry entry title is Screened(****)

                #region Journal daily Report

                #region PDF
                ReportPage reportPage = new ReportPage(driver);
                reportPage.ClickReportByTypeAndName("Journaler", "Journal (dagens)");
                reportPage.ReportPopUpFileFormats(ReportFormat.PDF.GetStringValue(), "Utgående post/Outbound", "Journalført/Registered", "0");
                CommonMethods.PlayWait(10000);
                reportPage.ReportVerification(ReportFormat.PDF.GetStringValue(), "Journal", caseTitle, registryEntryTitle, screenedRegistryTitleFull);
                reportPage.CloseCurrentBrowserTab();
                #endregion

                #region TEXT
                reportPage.ClickReportByTypeAndName("Journaler", "Journal (dagens)");
                reportPage.ReportPopUpFileFormats(ReportFormat.Text.GetStringValue(), "Utgående post/Outbound", "Journalført/Registered", "0");
                CommonMethods.PlayWait(10000);
                reportPage.ReportVerification(ReportFormat.Text.GetStringValue(), "Journal", caseTitle, registryEntryTitle, screenedRegistryTitleFull);
                reportPage.CloseCurrentBrowserTab();
                #endregion

                #region HTML
                reportPage.ClickReportByTypeAndName("Journaler", "Journal (dagens)");
                reportPage.ReportPopUpFileFormats(ReportFormat.HTML.GetStringValue(), "Utgående post/Outbound", "Journalført/Registered", "0");
                CommonMethods.PlayWait(10000);
                reportPage.ReportVerification(ReportFormat.HTML.GetStringValue(), "Journal", caseTitle, registryEntryTitle, screenedRegistryTitleFull);
                reportPage.CloseCurrentBrowserTab();
                #endregion

                #region Word
                //reportPage.ClickReportByTypeAndName("Journaler", "Journal (dagens)");
                //reportPage.ReportPopUpFileFormats(ReportFormat.Word.GetStringValue(), "Utgående post/Outbound", "Journalført/Registered", "0");
                //CommonMethods.PlayWait(10000);
                //reportPage.ReportVerification(ReportFormat.Word.GetStringValue(), "Journal", caseTitle, registryEntryTitle, screenedRegistryTitleFull);
                //reportPage.CloseCurrentBrowserTab();
                #endregion

                #endregion

                #region OEP Report

                #region PDF
                reportPage.ClickReportByTypeAndName("Journaler", "Avstemmingsrapport OEP");
                reportPage.ReportPopUpFileFormats(ReportFormat.PDF.GetStringValue(), "Utgående post/Outbound", "Journalført/Registered", "0");
                CommonMethods.PlayWait(10000);
                reportPage.ReportVerification(ReportFormat.PDF.GetStringValue(), "Journal", caseTitle, registryEntryTitle, screenedRegistryTitleFull);
                reportPage.CloseCurrentBrowserTab();
                #endregion

                #region TEXT
                reportPage.ClickReportByTypeAndName("Journaler", "Avstemmingsrapport OEP");
                reportPage.ReportPopUpFileFormats(ReportFormat.Text.GetStringValue(), "Utgående post/Outbound", "Journalført/Registered", "0");
                CommonMethods.PlayWait(10000);
                reportPage.ReportVerification(ReportFormat.Text.GetStringValue(), "Journal", caseTitle, registryEntryTitle, screenedRegistryTitleFull);
                reportPage.CloseCurrentBrowserTab();
                #endregion

                #region HTML
                reportPage.ClickReportByTypeAndName("Journaler", "Avstemmingsrapport OEP");
                reportPage.ReportPopUpFileFormats(ReportFormat.HTML.GetStringValue(), "Utgående post/Outbound", "Journalført/Registered", "0");
                CommonMethods.PlayWait(10000);
                reportPage.ReportVerification(ReportFormat.HTML.GetStringValue(), "Journal", caseTitle, registryEntryTitle, screenedRegistryTitleFull);
                reportPage.CloseCurrentBrowserTab();
                #endregion

                #endregion

                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            #endregion

            #region AccessCode Level - Step 11

            #region ---- Admin Module - Prereq ----
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Tilgangskoder and check the Access code is 5 & 6 is setup
                AdminstratorPage adminPage = new AdminstratorPage(driver);
                adminPage.NavigateToMenuItem("Brukeradministrasjon/tilgangsstyring", "Tilgangskoder", "Tilgangskoder (alle)");

                //Access Code 5
                adminPage.VerifyAccessCodeExist("5");
                adminPage.ClickItemInGrid("5");
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide title automatically when creating new post", true);
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide sender/recipient automatically when creating new post", true);
                adminPage.Save();

                //Access Code 6
                adminPage.VerifyAccessCodeExist("6");
                adminPage.ClickItemInGrid("6");
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide title automatically when creating new post", false);
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide sender/recipient automatically when creating new post", false);
                adminPage.Save();
                #endregion

                #region Guilt User - authorized for access code 5 for adm unit ENG
                adminPage.NavigateToMenuItem("Person", "Person");

                adminPage.SelectPerson("GUILT");
                adminPage.AddOrEditAutorization("5", "Adm.unit", "ENG");
                adminPage.AddOrEditAutorization("6", "Adm.unit", "ENG");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion

            });
            #endregion

            #region ---- Login to RM module with GUILT user as TstIndia 1 ----
            Selenium_Run((driver) =>
            {
                #region Private Variables
                var caseIdACL = string.Empty;
                var caseTitleACL = string.Format("Text1 Text2 and this is Omar Lie and Robert Vabo and more {0} - AccessCodeLevel", CommonMethods.GetRandomNumber());
                #endregion

                #region Login to Application
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Create case with access code and mark as screened and mark as person

                CasePage casePage = new CasePage(driver);
                casePage.AddTitle(caseTitleACL);
                casePage.ClickMoreDetailsButtonInCreateCase();
                casePage.SelectKendoDropdownAndAddValue("Access code", "6");
                casePage.SaveCase();

                casePage.EditCaseTitle();

                #region Mark as screened and Mark as Person
                // Mark text as Screened and check the text color
                casePage.EditCaseTitleScreenedTextOrPersonName("Text1", TitleModify.Screened.GetStringValue());

                // Mark text as person name and check the text format
                casePage.EditCaseTitleScreenedTextOrPersonName("Omar", TitleModify.PersonName.GetStringValue(), "Lie");

                //Mark text as Screened and Persons name and check the text color and text format
                casePage.EditCaseTitleScreenedTextOrPersonName("Robert", TitleModify.Screened.GetStringValue(), "Vabo");
                casePage.EditCaseTitleScreenedTextOrPersonName("Robert", TitleModify.PersonName.GetStringValue(), "Vabo");

                // Mark text as Screened and check the text color
                casePage.EditCaseTitleScreenedTextOrPersonName("more", TitleModify.Screened.GetStringValue());
                #endregion

                casePage.SaveCase();
                #endregion

                #region Verify Text in Case Title marked as screened and marked as person
                casePage.VerifyCaseTitleColorAndFormat("Text1", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Omar", TitleBackgroundFormat.Italic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Robert", TitleBackgroundFormat.RedWithItalic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Vabo", TitleBackgroundFormat.RedWithItalic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("more", TitleBackgroundFormat.Red.GetStringValue());

                caseIdACL = casePage.GetCaseNumber();
                #endregion

                #region Edit and change the access code to an accesscode that will screen the title automatically and save
                casePage.EditCaseTitle();
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);
                regEntry.RemoveDropdownValue("Access code");
                casePage.SelectKendoDropdownAndAddValue("Access code", "5");
                casePage.SaveCase();
                #endregion

                #region Verfiy the Case Title will be marked as red color (screened) and italic (marked as person)
                //VerifyCaseTitleIsRedAndNotScreenedAndHasItalics
                casePage.VerifyCaseTitleColorAndFormat("Text1", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Text2", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Omar", TitleBackgroundFormat.RedWithItalic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Lie", TitleBackgroundFormat.RedWithItalic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Robert", TitleBackgroundFormat.RedWithItalic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Vabo", TitleBackgroundFormat.RedWithItalic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("more", TitleBackgroundFormat.Red.GetStringValue());
                #endregion

                #region Edit and change back to the access code 6 and save
                casePage.EditCaseTitle();
                regEntry.RemoveDropdownValue("Access code");
                casePage.SelectKendoDropdownAndAddValue("Access code", "6");
                casePage.SaveCase();
                #endregion

                #region Verfiy the Case Title the person name will still be marked as person name and manually screened text will not be screened anymore
                //VerifyCaseTitleIsNotScreenedAndHasItalics
                casePage.VerifyCaseTitleColorAndFormat("Text1", TitleBackgroundFormat.Blue.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Text2", TitleBackgroundFormat.Blue.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Omar", TitleBackgroundFormat.Italic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Robert", TitleBackgroundFormat.Italic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Vabo", TitleBackgroundFormat.Italic.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("more", TitleBackgroundFormat.Blue.GetStringValue());
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion
            #endregion
        }

        [TestMethod]
        public void AccessCode_AutomaticScreening_TitleAndSender_07()
        {
            //TC#138860

            #region Private Variables
            var caseId = string.Empty;
            var caseTitle = "Automatic screening (title and sender_receiver) - Update reg.entry with access code - 07";
            var registryEntryTitle = "Registry Entry";
            #endregion

            //Step 1
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
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Sda User - is Authorized for access code 6 on adm unit ENG
                adminPage.ClickDropDownInAdminModule("Person");
                adminPage.SelectPerson("SDA");

                adminPage.AddOrEditAutorization("6", "Adm.unit", "ENG");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion

            });
            #endregion

            //Step 2, 3, 4
            #region Login to RM module with Guilt user as TstIndia1
            Selenium_Run((driver) =>
            {
                #region Login to RM Module
                FrontPage.LoginToApplication(driver);
                #endregion

                //Step 2
                #region Create case with access code

                CasePage casePage = new CasePage(driver);
                casePage.AddTitle(caseTitle);
                casePage.SaveCase();
                CommonMethods.PlayWait(5000);
                caseId = casePage.GetCaseNumber();

                #endregion

                //Step 3
                #region Add Registry Entry with access code
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);
                regEntry.CreateRegistryEntry(RegistryEntryType.IncomingType.GetStringValue());
                regEntry.AddRegTitle(registryEntryTitle);
                regEntry.AddRegToAddress("AA");
                regEntry.ClickSaveBttn();
                #endregion

                //Step 4
                #region Edit Registry entry and Save
                regEntry.ClickEditButton();
                regEntry.ClickMoreArrow();
                regEntry.SelectKendoDropdownAndAddValue("Access code", "6");
                regEntry.ClickSaveBttn();
                #endregion

                #region Step 4 Verification

                #region Title is screened
                regEntry.VerifyRegistryTitleColorAndFormat("Registry", TitleBackgroundFormat.Red.GetStringValue());
                regEntry.VerifyRegistryTitleColorAndFormat("Entry", TitleBackgroundFormat.Red.GetStringValue());
                #endregion

                regEntry.ClickEditButton();

                #region Registry entry is saved with access code 6
                regEntry.ClickMoreArrow();
                regEntry.VerifyDropDownValue("Access code", "6 - Offentlighetsloven § 6 - ikke sett til dato");
                #endregion

                #region Sender is marked as unoff (unoff checkbox is checked)
                regEntry.ClickToAddressReciepientName("AA - Anda Apotek");
                regEntry.VerifyRestrictedCheckboxValue();
                regEntry.ClickCancelButtonInRestrictedPopup();
                #endregion

                regEntry.ClickCancelButton();
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 5
            #region ---- Login to RM module with Sda User as TST India 2 ----
            Selenium_Run((driver) =>
            {
                LogoutPage logoutPage = new LogoutPage(driver);
                CasePage casePage = new CasePage(driver);
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);

                #region Login to Saksbehandling with different user
                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");
                #endregion

                #region Find the case
                casePage.QuickSearchCaseId(caseId);
                CommonMethods.PlayWait(5000);
                #endregion

                #region Step 5 Verification
                #region Title is screened
                regEntry.VerifyRegistryTitleColorAndFormat("Registry", TitleBackgroundFormat.Red.GetStringValue());
                regEntry.VerifyRegistryTitleColorAndFormat("Entry", TitleBackgroundFormat.Red.GetStringValue());
                #endregion

                #region Sender is screened
                regEntry.VerifySenderTextScreened("Anda Apotek");
                #endregion

                #endregion

                #region Logout from Application
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion
        }

        [TestMethod]
        public void AccessCodeUserNotAutorizedForCorrectAdmUnit_01()
        {
            //TC#138739  
            #region Private Variables
            var caseId = string.Empty;
            var title = "User NOT authorized for Accesscode for the correct AdmUnit";
            var registryEntryTitle = "Registry entry - Access code 6";
            #endregion

            Selenium_Run((driver) =>
            {
                //Step 1
                #region ---- Admin Module - Prereq ----

                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Tilgangskoder and check the Access code is 6 is setup
                AdminstratorPage adminPage = new AdminstratorPage(driver);
                adminPage.NavigateToMenuItem("Brukeradministrasjon/tilgangsstyring", "Tilgangskoder", "Tilgangskoder (alle)");
                adminPage.VerifyAccessCodeExist("6");
                adminPage.ClickItemInGrid("6");
                #endregion

                #region Verify the title button on/off
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide title automatically when creating new post", true);
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide sender/recipient automatically when creating new post", true);
                adminPage.Save();
                #endregion

                #region Guilt User - authorized for access code 6 for adm unit HOS
                adminPage.NavigateToMenuItem("Person", "Person");
                adminPage.SelectPerson("GUILT");
                adminPage.AddOrEditAutorization("6", "Adm.unit", "HOS");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Sda User - is NOT Authorized for access code 6 on adm unit ENG
                adminPage.ClickDropDownInAdminModule("Person");
                adminPage.SelectPerson("SDA");
                adminPage.AddOrEditAutorization("6", "Adm.unit", "ENG");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion

            });

            Selenium_Run((driver) =>
            {
                //Step 2
                #region Login with 'SDA' user and Select edit case option
                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");
                #endregion

                //Step 3
                #region Create case with case responsible 
                CasePage casePage = new CasePage(driver);
                casePage.AddTitle(title);
                casePage.ClickMoreDetailsButtonInCreateCase();
                casePage.SelectKendoDropdownAndAddValue("Access code", "6");
                casePage.SelectKendoDropdownUsingPartialText("Case responsible", "Srinibash Gecko Dash", "Srinibash Gecko Dash (ENG)", "Srinibash Gecko Dash (English Department)");
                casePage.SaveCase();
                caseId = casePage.GetCaseNumber();
                #endregion

                //Step 4 
                #region Create note/Remarks and verify access code 
                casePage.AddRemarksOrNotes(title);
                casePage.VerifyAccessCodeInRemarks("Access code", "6");
                #endregion

                //Step 5 
                #region Add Outgoing registry entry with Access code 6
                RegistryEntryPage registryEntry = new RegistryEntryPage(driver);
                casePage.ClickOnButton("Registry entries");
                registryEntry.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                registryEntry.AddRegTitle(registryEntryTitle);
                registryEntry.AddRegToAddress("AA");
                registryEntry.SelectKendoDropdownAndAddValue("Access code", "6");
                #endregion

                #region  Document attachment
                DocumentPage documentPage = new DocumentPage(driver);
                documentPage.ClickAndAddDocTemplate(AttachmentType.DocumentTemplate.GetStringValue());
                documentPage.SelectDocument(DocumentTemplateType.Dokumentmal.GetStringValue(), DocumentSubType.Standardbrev.GetStringValue());
                registryEntry.ClickSaveBttn();
                #endregion

                #region Open Elements Window And Login And Edit Save Document
                OpenElementsWindowAndLoginAndEditSaveDocument(driver, "sda");
                #endregion

                #region Change registry entry status to F/done
                registryEntry.ClickEditButton();
                registryEntry.ChangeREStatusToFerdigOrDone();
                registryEntry.ClickSaveBttn();
                #endregion

                #region Logout from RM Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });

            Selenium_Run((driver) =>
            {
                //Step 6
                #region Login with 'Guilt' user
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Search case number
                CasePage casePage = new CasePage(driver);
                casePage.QuickSearchCaseId(caseId);
                var screenedFullCaseTitle = casePage.GetScreenedCaseTitle();
                #endregion

                #region Verify the case title is not accessible or star
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 0, TitleBackgroundFormat.Red.GetStringValue(), 0);
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 2, TitleBackgroundFormat.Red.GetStringValue(), 2);
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 4, TitleBackgroundFormat.Red.GetStringValue(), 4);
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 7, TitleBackgroundFormat.Red.GetStringValue(), 7);
                casePage.VerifyCaseTitleMarkedAsStarsAndItalic("*****", 8, TitleBackgroundFormat.Red.GetStringValue(), 8);
                #endregion

                #region Verify Registry Title is not accessible or star
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("*****", 0, TitleBackgroundFormat.Red.GetStringValue(), 0);
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("*****", 2, TitleBackgroundFormat.Red.GetStringValue(), 2);
                regEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("*****", 5, TitleBackgroundFormat.Red.GetStringValue(), 5);
                #endregion

                //Step 8
                #region Ad-hoc testing
                #region Scenario 1 :Verify the case title is editable
                casePage.EditCaseTitle();
                casePage.ValidateCaseOrRegistryTitleEditable();
                casePage.ClickCancelEditCaseTitleButton();
                #endregion

                #region Scenario 2 : Verify the Registry entry title is editable
                casePage.ValidateCaseOrRegistryTitleEditable("registryEntry");
                #endregion

                #region Scenario 3 :Verify the New Registry entry is able to create
                regEntry.VerifyRegistryEntryAbleToCreate(RegistryEntryType.IncomingType.GetStringValue(), "You do not have privileges to add a registry entry ro this case");
                #endregion

                #region Verify Remark/Note is not accessible
                casePage.ClickOnButton("Remarks");
                var remarkGUI = casePage.GetRemarkTitle();
                Assert.IsTrue("*****" == remarkGUI, "Remark is accessible for this user");
                #endregion

                #region Scenario 4 : Verify quickmenu options on Case level to see information that is restricted.
                casePage.VerifyMoreOptionsInCaseLevel("Copy case", "You have either not the rights to copy this case, or it is not allowed to write to this case");
                casePage.VerifyMoreOptionsInCaseLevel("Renumber registry entries", "Du mangler rettigheter til å renummerere sakens journalposter");
                #endregion

                #region Scenario 5 : Verify options on Document level to see information that is restricted.
                driver.ClickOnButton("Registry entries");
                DocumentPage documentPage = new DocumentPage(driver);
                CommonMethods.PlayWait(7000);
                var docTitle = documentPage.GetDocumentTitle();
                Assert.IsTrue("*****" == docTitle, "Document title is accessible for this user");
                #endregion

                #region Scenario 6 :Verify options on Registry entry level to see information that is restricted.
                regEntry.VerifyRegistryOptionsAccessible("Links", "You haven't got privileges to create a new record");
                #endregion
                #endregion

                //Step 9
                #region Validate Registry Entry title and Document title in SAKSMAPPER search view
                casePage.ClickOnLeftDashboardMenuItem("Saksmapper");
                casePage.SendTextToTextBoxField("Date", "0");
                casePage.ClickOnSearchCriteriaButton();
                CommonMethods.PlayWait(5000);

                #region Verify Case title information that is restricted.
                casePage.VerifyCaseTitleInSaksmapper(caseId, screenedFullCaseTitle);
                #endregion

                #region Verify Registry Entry title and Document title to see information that is restricted.
                casePage.VerifyRegistryTitleAndDocumentTitleInSaksmapper(caseId, "*****", "*****", true);
                #endregion
                #endregion

                #region Logout from RM Module    
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
        }

        [TestMethod]
        public void AccessCode_UserAuthForEntireOrganization_04()
        {
            //TC#138740

            #region Private Variables
            var caseId = string.Empty;
            var caseTitle = "User cleared for an access code for an entire organization - 04";
            var registryEntryTitle = "Registry Entry";
            var remarkTitle = "04 - User cleared for an access code for an entire organization";
            #endregion

            //Step 1
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

                #region Guilt User - authorized for access code 6 for adm unit ENG - TSTIndia 2
                adminPage.NavigateToMenuItem("Person", "Person");

                adminPage.SelectPerson("GUILT");
                adminPage.AddOrEditAutorization("6", "Adm.unit", "ENG");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Sda User - Caseworker role has adm unit HOS, and has Access code 6, cleared for All Organization - TSTIndia 1
                adminPage.ClickDropDownInAdminModule("Person");
                adminPage.SelectPerson("SDA");

                //Add Authorization
                adminPage.AddOrEditAutorization("6", "Adm.unit", "HOS", true);

                //Add role
                adminPage.AddOrEditRole("Caseworker - HOS", "caseworker - eng", "HOS", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - HOS");

                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 2, 3, 4, 5
            #region Login to RM module with Guilt user as TstIndia1
            Selenium_Run((driver) =>
            {
                //Step 2
                #region Login to RM Module
                FrontPage.LoginToApplication(driver);
                #endregion

                //Step 3
                #region Create a case with access code 6 and Adm unit as ENG. TstIndia2 as case responsible.

                CasePage casePage = new CasePage(driver);
                casePage.AddTitle(caseTitle);
                casePage.ClickMoreDetailsButtonInCreateCase();
                casePage.SelectKendoDropdownAndAddValue("Access code", "6");
                casePage.SelectKendoDropdownUsingPartialText("Case responsible", "guilt guilt", "guilt guilt  (ENG)", "guilt guilt  (English Department)");
                casePage.SaveCase();
                CommonMethods.PlayWait(5000);
                caseId = casePage.GetCaseNumber();

                #endregion

                #region Step 3 Verfication
                casePage.VerifyCaseTitleColorAndFormat("User", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("access", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("code", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("organization ", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("04 ", TitleBackgroundFormat.Red.GetStringValue());
                #endregion

                //Step 4
                #region Create note
                casePage.AddRemarksOrNotes(remarkTitle);
                #endregion

                //Step 5
                #region Create a registry entry with a doc in the case with access code 6 and Adm Unit as ENG. Set status to F

                #region Add Registry Entry
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);
                casePage.ClickOnButton("Registry entries");
                regEntry.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                regEntry.AddRegTitle(registryEntryTitle);
                regEntry.AddRegToAddress("AA");
                #endregion

                #region Attach Document
                DocumentPage documentPage = new DocumentPage(driver);
                documentPage.ClickAndAddDocTemplate(AttachmentType.DocumentTemplate.GetStringValue());
                documentPage.SelectDocument(DocumentTemplateType.Dokumentmal.GetStringValue(), DocumentSubType.Standardbrev.GetStringValue());
                documentPage.SaveAndEdit();
                #endregion

                #region Handle the chrome window "Open Elements Window"
                documentPage.HandleAlertPopupToOpenWordDocument();
                #endregion

                #region Handle Login To Application window
                documentPage.DocumentWebLoginWindow();
                documentPage.EditAndSaveWordDocument();
                #endregion

                #region Close the document
                documentPage.CloseDocument();
                #endregion

                #region Change status to F/Done
                CommonMethods.PlayWait(8000);
                regEntry.ClickEditButton();
                CommonMethods.PlayWait(2000);
                regEntry.ChangeREStatusToFerdigOrDone();
                regEntry.ClickSaveBttn();
                #endregion

                #endregion

                #region Logout
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 6
            #region Login with user SDA TstIndia1 as Caseworker role
            Selenium_Run((driver) =>
            {
                #region Login to RM Module
                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");
                #endregion

                #region  User TstIndia1 has access to read case , Note, reg entry and open doc since user is cleared for the access code in the entire organization. All text is visable for user TstIndia1

                #region Search case
                CasePage casePage = new CasePage(driver);
                casePage.QuickSearchCaseId(caseId);
                #endregion

                #region Verify CaseTitle
                var caseTitleGUI = casePage.GetScreenedCaseTitle();
                Assert.IsTrue(caseTitle == caseTitleGUI, "User SDA cannot see the case title" + caseTitle);
                #endregion

                #region Verify Registry Title
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);
                var regEntTitleGUI = regEntry.GetScreenedRegistryTitle();
                Assert.IsTrue(registryEntryTitle == regEntTitleGUI, "User SDA cannot see the case title" + registryEntryTitle);
                #endregion

                #region Verify Remark/Note
                casePage.ClickOnButton("Remarks");
                var remarkGUI = casePage.GetRemarkTitle();
                Assert.IsTrue(remarkTitle == remarkGUI, "User SDA cannot see the case title" + registryEntryTitle);
                #endregion

                #region Verify able to open doc
                casePage.ClickOnButton("Registry entries");
                regEntry.ClickAttachmentOption(registryEntryTitle, "Open");
                CommonMethods.PlayWait(3000);
                DocumentPage documentPage = new DocumentPage(driver);
                documentPage.HandleAlertPopupToOpenWordDocument();
                documentPage.DocumentWebLoginWindow("sda");
                CommonMethods.PlayWait(10000);
                documentPage.CloseDocument();
                #endregion

                #endregion

                #region Logout
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion
        }

        [TestMethod]
        public void AutomaticScreeningRemoveAccessCode_08()
        {
            //TC#138861

            #region Private Variables
            var caseId = string.Empty;
            var caseTitle = "Automatic screening (title and sender_receiver) - Remove access code - 08";
            var registryEntryTitle = "Registry Entry";
            #endregion

            //Step 1
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

                #region Guilt User - authorized for access code 6 for adm unit ENG - TSTIndia 2
                adminPage.NavigateToMenuItem("Person", "Person");

                adminPage.SelectPerson("GUILT");
                adminPage.AddOrEditAutorization("6", "Adm.unit", "ENG");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 2, 3, 4, 5
            #region Login to RM module with Guilt user
            Selenium_Run((driver) =>
            {
                //Step 2, 3
                #region Login to RM Module
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Create case without an access code.
                CasePage casePage = new CasePage(driver);
                casePage.AddTitle(caseTitle);
                casePage.SaveCase();
                CommonMethods.PlayWait(5000);
                caseId = casePage.GetCaseNumber();
                #endregion

                //Step 4
                #region Add Income Registry Entry with Access code 6
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);
                casePage.ClickOnButton("Registry entries");
                regEntry.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                regEntry.AddRegTitle(registryEntryTitle);
                regEntry.AddRegToAddress("AA");
                casePage.SelectKendoDropdownAndAddValue("Access code", "6");
                regEntry.ClickSaveBttn();
                #endregion

                #region Verify Text in Reg Entry Title marked as screened
                regEntry.VerifyRegistryTitleColorAndFormat("Registry", TitleBackgroundFormat.Red.GetStringValue());
                regEntry.VerifyRegistryTitleColorAndFormat("Entry", TitleBackgroundFormat.Red.GetStringValue());
                #endregion

                #region Verify the Resticted button is Selected
                regEntry.ClickEditButton();
                regEntry.ModifyRegistryEntryToField("AA - Anda Apotek");
                regEntry.VerifyRestrictedCheckBoxIsSelected("Restricted");
                #endregion

                //Step 5
                #region Edit Registry Entry
                regEntry.RemoveDropdownValue("Access code");
                regEntry.ClickSaveBttn();
                #endregion

                #region Verify Text in Reg Entry Title marked as Unscreened
                regEntry.VerifyRegistryTitleColorAndFormat("Registry", TitleBackgroundFormat.Blue.GetStringValue());
                regEntry.VerifyRegistryTitleColorAndFormat("Entry", TitleBackgroundFormat.Blue.GetStringValue());
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion
        }

        [TestMethod]
        public void AccessCodeShouldAddedToAllAttachments()
        {
            //TC#138816 
            #region Private Variables
            var caseId = string.Empty;
            var caseTitle = "Access code should be added to all attachments";
            var registryEntryTitle = "Registry Entry";
            var filePathForSingleAttachment = "\"C:\\Projects\\AutoIT\\Test1.docx\"";
            var filePathForMultipleAttachment = "\"C:\\Projects\\AutoIT\\Test2.docx\" \"C:\\Projects\\AutoIT\\Test3.docx\"";
            #endregion

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
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide title automatically when creating new post", false);
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide sender/recipient automatically when creating new post", false);
                adminPage.Save();
                #endregion

                #region Guilt User - authorized for access code 6 for adm unit ENG
                adminPage.NavigateToMenuItem("Person", "Person");

                adminPage.SelectPerson("GUILT");
                adminPage.AddOrEditAutorization("6", "Adm.unit", "ENG");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion

            });
            #endregion

            //Step 1 to 
            #region Login to RM module with Guilt user
            Selenium_Run((driver) =>
            {
                //Step 1
                #region Login to RM Module
                FrontPage.LoginToApplication(driver);
                #endregion

                //Step 2
                #region Create case without an access code.
                CasePage casePage = new CasePage(driver);
                casePage.AddTitle(caseTitle);
                casePage.SaveCase();
                CommonMethods.PlayWait(5000);
                caseId = casePage.GetCaseNumber();
                #endregion

                #region Add Income Registry Entry with Access code 6
                RegistryEntryPage regEntry = new RegistryEntryPage(driver);
                casePage.ClickOnButton("Registry entries");
                regEntry.CreateRegistryEntry(RegistryEntryType.IncomingType.GetStringValue());
                regEntry.AddRegTitle(registryEntryTitle);
                regEntry.AddRegToAddress("AA");
                casePage.SelectKendoDropdownAndAddValue("Access code", "6");
                casePage.SelectKendoDropdownAndAddValue("Access group", "Arkivarer");
                #endregion

                //Step 3
                #region Document Attachment from local Directory
                DocumentPage documentPage = new DocumentPage(driver);
                documentPage.AttachFileFromDirectory("Attach", "File", filePathForSingleAttachment);
                documentPage.SelectDocument();
                documentPage.AttachFileFromDirectory("Attach", "File", filePathForMultipleAttachment);
                documentPage.SelectDocument();
                regEntry.ClickSaveBttn();
                CommonMethods.PlayWait(5000);
                #endregion

                //Step 4
                #region Verify that Access code and pursuant is present in the document details
                for (int i = 1; i <= 3; i++)
                {
                    documentPage.ViewAttachmentInformation(string.Format("Test{0}", i), "Document details", i);
                    regEntry.VerifyDropDownValue("Access code", "6 - Offentlighetsloven § 6 - ikke sett til dato");
                    regEntry.VerifyDropDownValue("Access group", "Ofl. § 6");
                    documentPage.ClickOnButton("cancel");
                }
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion
        }

        [TestMethod]
        public void AccessCode_ShowMergedDocument()
        {
            //TC#138873
            #region Private Variables
            string caseId = string.Empty;
            string caseTitle = "Show merged documents: access code";
            string registryEntryTitle = "Registry Entry";
            string filePath = string.Format("{0}\\{1}", Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), GlobalVariables.DocumentUploadFolderName), GlobalVariables.DocumentNameToUpload);
            string uploadDocContentToVerify1 = "Saksoversikt";
            string uploadDocContentToVerify2 = "New case - Functions_Case folder";
            string editDocumentText = GlobalVariables.EditDataStatement.Replace("{Space}", " ").Trim();
            #endregion

            //Step 2
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
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide title automatically when creating new post", false);
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide sender/recipient automatically when creating new post", false);
                adminPage.Save();
                #endregion

                #region Guilt User - authorized for access code 6 for adm unit ENG
                adminPage.NavigateToMenuItem("Person", "Person");

                adminPage.SelectPerson("GUILT");
                adminPage.AddOrEditAutorization("6", "Adm.unit", "ENG");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Sda User - is NOT Authorized for access code 6 on adm unit ENG
                adminPage.ClickDropDownInAdminModule("Person");
                adminPage.SelectPerson("SDA");

                adminPage.RemoveAutorization("6", "SDA");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion

            });
            #endregion

            //Step 3 to 6
            #region Login to RM module with Guilt user as TstIndia1
            Selenium_Run((driver) =>
            {
                FrontPage.LoginToApplication(driver);
                #region Changing role
                DashboardPage dashboardPage = new DashboardPage(driver);
                dashboardPage.ChangeRole(RolesInApplication.MainRegistrar);
                #endregion

                //Step 3
                #region Create new case
                CasePage casePage = new CasePage(driver);
                casePage.CreateCase(caseTitle);
                caseId = casePage.GetCaseNumber();
                #endregion

                //Step 4 
                #region Add Outgoing registry entry
                RegistryEntryPage registryEntry = new RegistryEntryPage(driver);
                registryEntry.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                registryEntry.AddRegTitle(registryEntryTitle);
                registryEntry.AddRegToAddress("AA");
                #endregion

                #region Main Document attachment from Application
                DocumentPage documentPage = new DocumentPage(driver);
                documentPage.ClickAndAddDocTemplate(AttachmentType.DocumentTemplate.GetStringValue());
                documentPage.SelectDocument(DocumentTemplateType.Dokumentmal.GetStringValue(), DocumentSubType.Standardbrev.GetStringValue());
                CommonMethods.PlayWait(7000);
                documentPage.SaveAndEdit();
                #endregion

                #region Open Elements Window And Login And Edit Save Document
                OpenElementsWindowAndLoginAndEditSaveDocument(driver);
                #endregion

                #region Document Attachment from local Directory
                registryEntry.ClickEditButton();
                documentPage.AttachFileFromDirectory("Attach", "File", filePath);
                documentPage.SelectDocument();
                registryEntry.ClickSaveBttn();
                #endregion

                #region Leave Executive officer as “unassigned” and Approvers empty
                registryEntry.ClickEditButton();
                casePage.SelectKendoDropdownAndAddValue("Status", "Til godkjening/For approval");
                registryEntry.ClickSaveBttn();
                #endregion

                // Step 5
                #region Add Access code into the Attachment
                documentPage.ViewAttachmentInformation(GlobalVariables.DocumentNameToUpload, "Document details");
                casePage.SelectKendoDropdownAndAddValue("Access code", "6");
                casePage.Click("Save");
                #endregion

                #region  Verify the attachment title
                documentPage.VerifyRegistryAttachmentColor(GlobalVariables.DocumentNameToUpload, TitleBackgroundFormat.Red.GetStringValue());
                #endregion

                //Step 6
                #region Registry entry Functions – Show merged documents
                registryEntry.RegistryEntryViewMenu("Show merged documents");
                #endregion

                //Step 6 Verification. Both documents should show
                #region Verify the contents in downloaded pdf
                var pdfContents = documentPage.GetPDFContents();

                //Standard brev document text verification
                Assert.IsTrue(pdfContents.Contains(registryEntryTitle), string.Format("Downloaded PDF document does not contain the text {0}", registryEntryTitle));
                Assert.IsTrue(pdfContents.Contains(caseId), string.Format("Downloaded PDF document does not contain the text {0}", caseId));
                Assert.IsTrue(pdfContents.Contains(editDocumentText), string.Format("Downloaded PDF document does not contain the text {0}", editDocumentText));

                //Uploaded document text verification
                Assert.IsTrue(pdfContents.Contains(uploadDocContentToVerify1), string.Format("Downloaded PDF document does not contain the text {0}", uploadDocContentToVerify1));
                Assert.IsTrue(pdfContents.Contains(uploadDocContentToVerify2), string.Format("Downloaded PDF document does not contain the text {0}", uploadDocContentToVerify2));
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 7
            #region ---- Admin Module - Prereq ----
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Tilgangskoder and check the Access code is 6 is setup
                AdminstratorPage adminPage = new AdminstratorPage(driver);
                adminPage.NavigateToMenuItem("Brukeradministrasjon/tilgangsstyring");
                adminPage.NavigateToMenuItem("Person", "Person");
                #endregion

                #region GUILT User - is NOT Authorized for access code 6 on adm unit ENG
                adminPage.SelectPerson("GUILT");
                adminPage.RemoveAutorization("6", "GUILT");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Logout
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion

            });
            #endregion

            //Step 8, 9
            #region Login to RM module with Guilt user as TstIndia1
            Selenium_Run((driver) =>
            {
                #region Login to RM Module
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Changing role
                DashboardPage dashboardPage = new DashboardPage(driver);
                dashboardPage.ChangeRole(RolesInApplication.MainRegistrar);
                #endregion

                #region Search case
                CasePage casePage = new CasePage(driver);
                RegistryEntryPage registryEntry = new RegistryEntryPage(driver);
                DocumentPage documentPage = new DocumentPage(driver);
                CommonMethods.PlayWait(8000);
                casePage.QuickSearchCaseId(caseId);
                CommonMethods.PlayWait(4000);
                #endregion

                //Step 8
                #region Verify the attachment title should be screened (with stars)
                documentPage.VerifyRegistryAttachmentColor("*****", TitleBackgroundFormat.Red.GetStringValue());
                #endregion

                //Step 9
                #region Registry entry Functions – Show merged documents
                registryEntry.RegistryEntryViewMenu("Show merged documents");
                #endregion

                //Step 9 Verification. only one documents should show
                #region Verify the contents in downloaded pdf
                var pdfContents = documentPage.GetPDFContents();

                //Standard brev document text verification
                Assert.IsTrue(pdfContents.Contains(registryEntryTitle), string.Format("Downloaded PDF document does not contain the text {0}", registryEntryTitle));
                Assert.IsTrue(pdfContents.Contains(caseId), string.Format("Downloaded PDF document does not contain the text {0}", caseId));
                Assert.IsTrue(pdfContents.Contains(editDocumentText), string.Format("Downloaded PDF document does not contain the text {0}", editDocumentText));

                //Uploaded document text verification
                Assert.IsFalse(pdfContents.Contains(uploadDocContentToVerify1), string.Format("Downloaded PDF document does contain the text {0}", uploadDocContentToVerify1));
                Assert.IsFalse(pdfContents.Contains(uploadDocContentToVerify2), string.Format("Downloaded PDF document does contain the text {0}", uploadDocContentToVerify2));
                #endregion

                #region Logout
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 10
            #region ---- Admin Module - Add access code for User 1 (guilt) ----
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Tilgangskoder and check the Access code is 6 is setup
                AdminstratorPage adminPage = new AdminstratorPage(driver);
                adminPage.NavigateToMenuItem("Brukeradministrasjon/tilgangsstyring");
                adminPage.NavigateToMenuItem("Person", "Person");
                #endregion

                #region GUILT User - is NOT Authorized for access code 6 on adm unit ENG
                adminPage.SelectPerson("GUILT");
                adminPage.AddOrEditAutorization("6", "Adm.unit", "ENG");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 10 Finalize the registrt entry
            #region Login to RM module with Guilt user as TstIndia1
            Selenium_Run((driver) =>
            {
                #region Login to RM Module
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Changing role
                DashboardPage dashboardPage = new DashboardPage(driver);
                dashboardPage.ChangeRole(RolesInApplication.MainRegistrar);
                #endregion

                #region Search case
                CasePage casePage = new CasePage(driver);
                RegistryEntryPage registryEntry = new RegistryEntryPage(driver);
                DocumentPage documentPage = new DocumentPage(driver);
                CommonMethods.PlayWait(8000);
                casePage.QuickSearchCaseId(caseId);
                #endregion

                #region Change status to F/Done
                CommonMethods.PlayWait(8000);
                registryEntry.ClickEditButton();
                CommonMethods.PlayWait(2000);
                registryEntry.ChangeREStatusToFerdigOrDone();
                registryEntry.ClickSaveBttn();
                #endregion

                #region Logout
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 11
            #region Login with 'SDA' user and Select edit case option
            Selenium_Run((driver) =>
            {
                #region Login to RM Module
                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");
                #endregion

                #region Changing role
                DashboardPage dashboardPage = new DashboardPage(driver);
                dashboardPage.ChangeRole(RolesInApplication.MainRegistrar);
                #endregion

                #region Search case
                CasePage casePage = new CasePage(driver);
                RegistryEntryPage registryEntry = new RegistryEntryPage(driver);
                DocumentPage documentPage = new DocumentPage(driver);
                CommonMethods.PlayWait(8000);
                casePage.QuickSearchCaseId(caseId);
                #endregion

                #region Registry entry Functions – Show merged documents
                registryEntry.RegistryEntryViewMenu("Show merged documents");
                #endregion

                //Verification. only one documents should show
                #region Verify the contents in downloaded pdf
                var pdfContents = documentPage.GetPDFContents();

                //Standard brev document text verification
                Assert.IsTrue(pdfContents.Contains(registryEntryTitle), string.Format("Downloaded PDF document does not contain the text {0}", registryEntryTitle));
                Assert.IsTrue(pdfContents.Contains(caseId), string.Format("Downloaded PDF document does not contain the text {0}", caseId));
                Assert.IsTrue(pdfContents.Contains(editDocumentText), string.Format("Downloaded PDF document does not contain the text {0}", editDocumentText));

                //Uploaded document text verification
                Assert.IsFalse(pdfContents.Contains(uploadDocContentToVerify1), string.Format("Downloaded PDF document does contain the text {0}", uploadDocContentToVerify1));
                Assert.IsFalse(pdfContents.Contains(uploadDocContentToVerify2), string.Format("Downloaded PDF document does contain the text {0}", uploadDocContentToVerify2));
                #endregion

                #region Logout
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion
        }

        [TestMethod]
        public void AccessCode_UserEditregistryEntryAndCaseMemberOfAccessGroup_03()
        {
            //TC#138738 

            #region Private Variables
            var caseId = string.Empty;
            var caseTitle = "EDIT registry entry and member of Accessgroup - 03";
            var registryEntryTitle = "Registry Entry";
            #endregion
   
            //Step 1
            #region ---- Admin Module - Prereq ----
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Tilgangskoder and check the Access code is 6 is setup
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Brukeradministrasjon/tilgangsstyring", "Tilgangskoder", "Tilgangskoder (alle)");
                adminPage.VerifyAccessCodeExist("6");
                adminPage.ClickItemInGrid("6");
                #endregion

                #region Verify the title button on/off
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide title automatically when creating new post", true);
                adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide sender/recipient automatically when creating new post", true);
                adminPage.Save();
                #endregion

                #region Guilt User - Add or Verify the member of Access group is present under Arkivarer 
                adminPage.NavigateToMenuItem("Tilg.grupper/Adr.grupper", "Søk");

                adminPage.ClickOnButton("Search");
                adminPage.AddOrVerifyMemberOfAccessGroup("Arkivarer", "guilt guilt");
                adminPage.AddOrVerifyMemberOfAccessGroup("Arkivarer", "Srinibash Gecko Dash");
                #endregion

                #region Guilt User - authorized for access code 6 for adm unit ENG - TSTIndia 2
                adminPage.NavigateToMenuItem("Person", "Person");
                adminPage.SelectPerson("GUILT");
                adminPage.AddOrEditAutorization("6", "Adm.unit", "ENG");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion

                #region Sda User - Caseworker role has adm unit HOS, and has Access code 6, cleared for All Organization - TSTIndia 1
                adminPage.ClickDropDownInAdminModule("Person");
                adminPage.SelectPerson("SDA");
                adminPage.AddOrEditAutorization("6", "Adm.unit", "ENG");
                adminPage.AddOrEditRole("Caseworker - ENG", "caseworker - eng", "ENG", "Saksarkiv 1 (el)", "Sentral journal", "Caseworker - ENG");
                #endregion
                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion
 
            //Step 2, 3, 4, 5
            #region Login to RM module with SDA user as TstIndia2
            Selenium_Run((driver) =>
            {
                //Step 2
                #region Login to RM Module
                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");
                #endregion

                #region Verify User Is LogOn With the Correct use Role
                CasePage casePage = CasePage.Connect(driver);
                casePage.VerifyUserIsLogOnWithCorrectRole(RolesInApplication.CaseWorkerENG.GetStringValue());
                #endregion

                //Step 3
                #region Create a case with access code 6 and Adm unit as ENG. TstIndia2 as case responsible
                casePage.AddTitle(caseTitle);
                casePage.ClickMoreDetailsButtonInCreateCase();
                casePage.SelectKendoDropdownAndAddValue("Access code", "6");
                casePage.SelectKendoDropdownUsingPartialText("Case responsible", "Srinibash Gecko Dash", "Srinibash Gecko Dash (ENG)", "Srinibash Gecko Dash (English Department)");
                casePage.SelectKendoDropdownAndAddValue("Access group", "Arkivarer");
                casePage.SaveCase();
                CommonMethods.PlayWait(5000);
                caseId = casePage.GetCaseNumber();

                #endregion

                #region Step 3 Verfication
                casePage.VerifyCaseTitleColorAndFormat("EDIT", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("registry", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Accessgroup", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("03", TitleBackgroundFormat.Red.GetStringValue());
                #endregion

                //Step 4
                #region Create a registry entry with a doc in the case with access code 6 and Adm Unit as ENG. Set status to F
                #region Add Outgoing registry entry with Access code 6
                RegistryEntryPage registryEntry = RegistryEntryPage.Connect(driver);
                casePage.ClickOnButton("Registry entries");
                registryEntry.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                registryEntry.AddRegTitle(registryEntryTitle);
                registryEntry.AddRegToAddress("AA");
                registryEntry.RemoveDropdownValue("Access code");
                casePage.SelectKendoDropdownAndAddValue("Access code", "6");
                registryEntry.VerifyDropDownValue("Access code", "6 - Offentlighetsloven § 6 - ikke sett til dato");
                CommonMethods.PlayWait(3000);
                registryEntry.VerifyDropDownValue("Access group", "Arkivarer");
                #endregion

                #region Main Document attachment from Application
                DocumentPage documentPage = DocumentPage.Connect(driver);
                documentPage.ClickAndAddDocTemplate(AttachmentType.DocumentTemplate.GetStringValue());
                documentPage.SelectDocument(DocumentTemplateType.Dokumentmal.GetStringValue(), DocumentSubType.Standardbrev.GetStringValue());
                documentPage.SaveAndEdit();
                #endregion

                #region Open Elements Window And Login And Edit Save Document
                OpenElementsWindowAndLoginAndEditSaveDocument(driver, "sda");
                #endregion

                #region Verify registry entry title is Screened/red color
                registryEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Registry", 0, TitleBackgroundFormat.Red.GetStringValue());
                registryEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Entry", 1, TitleBackgroundFormat.Red.GetStringValue());
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
            #endregion

            //Step 5
            #region Login to RM module with Guilt user as TstIndia1
            Selenium_Run((driver) =>
            {
                #region Login to RM Module
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Edit Registry entry title
                var casePage = CasePage.Connect(driver);
                casePage.VerifyUserIsLogOnWithCorrectRole(RolesInApplication.CaseWorkerENG.GetStringValue());
                casePage.QuickSearchCaseId(caseId);
                casePage.EditCaseTitle("is editable");
                #endregion

                #region Edit Registry entry title
                RegistryEntryPage regEntry = RegistryEntryPage.Connect(driver);
                CommonMethods.PlayWait(3000);
                regEntry.EditRegistryEntryTitle("is editable");
                #endregion

                #region Logout
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            //Step 6
            #region Repeat the same steps above, but this time remove TstIndia1 from the Arkivarer group
            #region ---- Admin Module - Prereq ----Remove User from the Arkivarer group
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Guilt User - Remove User from the Arkivarer group
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Brukeradministrasjon/tilgangsstyring", "Tilg.grupper/Adr.grupper", "Søk");

                adminPage.ClickOnButton("Search");
                adminPage.RemoveUserFromAccessGroup("Arkivarer", "guilt guilt");
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            #region Login to RM module with SDA user as TstIndia2
            Selenium_Run((driver) =>
            {
                #region Login to RM Module
                FrontPage.LoginToApplication(driver, ApplicationModules.RecordManagement.GetStringValue(), "SDA", "SDA");
                #endregion

                #region Create a case with access code 6 and Adm unit as ENG. TstIndia2 as case responsible.
                CasePage casePage = CasePage.Connect(driver);
                casePage.AddTitle(caseTitle);
                casePage.ClickMoreDetailsButtonInCreateCase();
                casePage.SelectKendoDropdownAndAddValue("Access code", "6");
                casePage.SelectKendoDropdownUsingPartialText("Case responsible", "Srinibash Gecko Dash", "Srinibash Gecko Dash (ENG)", "Srinibash Gecko Dash (English Department)");
                casePage.SaveCase();
                CommonMethods.PlayWait(5000);
                caseId = casePage.GetCaseNumber();
                #endregion

                #region Step 3 Verfication
                casePage.VerifyCaseTitleColorAndFormat("EDIT", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("registry", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("Accessgroup", TitleBackgroundFormat.Red.GetStringValue());
                casePage.VerifyCaseTitleColorAndFormat("03", TitleBackgroundFormat.Red.GetStringValue());
                #endregion

                #region Add Outgoing registry entry with Access code 6
                RegistryEntryPage registryEntry = RegistryEntryPage.Connect(driver);
                casePage.ClickOnButton("Registry entries");
                registryEntry.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                registryEntry.AddRegTitle(registryEntryTitle);
                registryEntry.AddRegToAddress("AA");
                registryEntry.SelectKendoDropdownAndAddValue("Access code", "6");
                casePage.SelectKendoDropdownAndAddValue("Access group", "Arkivarer");
                #endregion

                #region Main Document attachment from Application
                DocumentPage documentPage = DocumentPage.Connect(driver);
                documentPage.ClickAndAddDocTemplate(AttachmentType.DocumentTemplate.GetStringValue());
                documentPage.SelectDocument(DocumentTemplateType.Dokumentmal.GetStringValue(), DocumentSubType.Standardbrev.GetStringValue());
                documentPage.SaveAndEdit();
                #endregion

                #region Open Elements Window And Login And Edit Save Document
                OpenElementsWindowAndLoginAndEditSaveDocument(driver, "sda");
                #endregion

                #region Verify registry entry title is Screened/red color
                registryEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Registry", 0, TitleBackgroundFormat.Red.GetStringValue());
                registryEntry.VerifyRegistryTitleMarkedAsStarsAndItalic("Entry", 1, TitleBackgroundFormat.Red.GetStringValue());
                #endregion

                #region Logout from Admin Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            #region Login to RM module with Guilt user as TstIndia1
            Selenium_Run((driver) =>
            {
                #region Login to RM Module
                FrontPage.LoginToApplication(driver);
                #endregion

                var casePage = CasePage.Connect(driver);
                casePage.QuickSearchCaseId(caseId);
                casePage.ValidateCaseOrRegistryTitleEditable();

                #region Edit Registry entry title
                RegistryEntryPage regEntry = RegistryEntryPage.Connect(driver);
                casePage.ValidateCaseOrRegistryTitleEditable("registryEntry");
                #endregion

                #region Logout
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion
            #endregion
        }
 
        #region "PRIVATE METHODS"

        #region Access code setup
        /// <summary>
        /// Access code setup 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="Level1"></param>
        /// <param name="Level2"></param>
        /// <param name="Level3"></param>
        /// <param name="accessCodeValue"></param>
        /// <param name="automaticTitle"></param>
        /// <param name="AutomaticSenderRecipient"></param>
        private void AccessCodeSetup(RemoteWebDriver driver, string Level1, string Level2, string Level3, string accessCodeValue, bool automaticTitle, bool AutomaticSenderRecipient)
        {
            #region Login to Admin Module
            FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
            #endregion

            #region Navigate to Tilgangskoder and check the Access code is 6 is setup
            AdminstratorPage adminPage = new AdminstratorPage(driver);
            adminPage.NavigateToMenuItem(Level1, Level2, Level3);
            adminPage.VerifyAccessCodeExist(accessCodeValue);
            adminPage.ClickItemInGrid(accessCodeValue);
            adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide title automatically when creating new post", automaticTitle);
            adminPage.ClickCheckBoxInAdminModuleSwitchOnOff("Hide sender/recipient automatically when creating new post", AutomaticSenderRecipient);
            adminPage.Save();
            #endregion
        }
        #endregion

        #region Open Elements Window And Login And Edit Save Document
        /// <summary>
        /// Open Elements Window And Login And Edit Save Document
        /// </summary>
        /// <param name="driver"></param>
        private void OpenElementsWindowAndLoginAndEditSaveDocument(RemoteWebDriver driver,string userName="guilt")
        {
            #region Handle the chrome window "Open Elements Window"
            DocumentPage documentPage = new DocumentPage(driver);
            documentPage.HandleAlertPopupToOpenWordDocument();
            #endregion

            #region Handle Login To Application window
            documentPage.DocumentWebLoginWindow(userName);
            documentPage.EditAndSaveWordDocument(GlobalVariables.EditDataStatement);
            #endregion

            #region Close the document
            documentPage.CloseDocument();
            #endregion

            CommonMethods.PlayWait(5000);
        }
        #endregion
        #endregion "PRIVATE METHODS"
    }
}