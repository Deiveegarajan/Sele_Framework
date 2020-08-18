using AutoIt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using System;
using System.Windows.Forms;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.DMB_Handling
{
    [TestClass]
    [TestCategory("DMB Handling")]
    public partial class DMB : SeleniumTestBase
    {
        [TestMethod]
        [Timeout(5000000)]
        public void RecommendationProposalDecisionProtocolExtractLetterCaseParty()
        {
            // TC#77817
            // TC#74091
            // TC#75690

            #region ---- Variable Declaration ----

            #region Variables
            GlobalVariables.RoleType = " Secretary of the Board - Eng ";
            GlobalVariables.MeetingTabName = "Documents";
            GlobalVariables.ProcessName = "Kommunestyret";
            GlobalVariables.MeetingType = "Kommunestyret";
            string proposerName = "Jarle Trydal";
            string installingText = "This is Installing Text";
            string oppsummeringText = "This is Oppsummering Text";
            string caseNumber = string.Empty;
            string caseTitle = string.Empty;
            string actualVal = string.Empty;
            #endregion

            #region Date & Time
            var time = DateTime.Now;  //12.02.2018 07:04:05   4:25 5:00
            var secondTime = time.AddHours(1).AddMinutes(23);
            var secondTime2 = time.AddHours(2).AddMinutes(52);
            var secondTime3 = time.AddHours(3).AddMinutes(52);
            var secondTime4 = time.AddHours(4).AddMinutes(52);
            var firstTime = time.AddMinutes(13);
            string meetingStartTimeAndDate = firstTime.ToString("dd.MM.yyyy HH:mm");
            string meetingEndTimeAndDate = secondTime.ToString("dd.MM.yyyy HH:mm");
            string meetingEndTimeAndDateForNoNumber = secondTime2.ToString("dd.MM.yyyy HH:mm");
            string meetingEndTimeAndDateForJustThisMeeting = secondTime3.ToString("dd.MM.yyyy HH:mm");
            string meetingEndTimeAndDateFromSubsequentMeeting = secondTime4.ToString("dd.MM.yyyy HH:mm");
            string myMeetingTiming = firstTime.ToString("HH.mm") + " - " + secondTime.ToString("HH.mm");
            string myMeetingTimingWithoutEndTime = firstTime.ToString("HH.mm") + " - ";
            string myMeetingTimingForNoNumber = firstTime.ToString("HH.mm") + " - " + secondTime2.ToString("HH.mm");
            string myMeetingTimingJustThisMeeting = firstTime.ToString("HH.mm") + " - " + secondTime3.ToString("HH.mm");
            string myMeetingTimingFromSubsequentMeeting = firstTime.ToString("HH.mm") + " - " + secondTime4.ToString("HH.mm");
            string dateInSaksProtokoll = time.ToString("dd/yyyy");

            string dateofMonth = time.ToString("dd");
            switch (dateofMonth)
            {
                case "01":
                    dateofMonth = 1.ToString();
                    break;
                case "02":
                    dateofMonth = 2.ToString();
                    break;
                case "03":
                    dateofMonth = 3.ToString();
                    break;
                case "04":
                    dateofMonth = 4.ToString();
                    break;
                case "05":
                    dateofMonth = 5.ToString();
                    break;
                case "06":
                    dateofMonth = 6.ToString();
                    break;
                case "07":
                    dateofMonth = 7.ToString();
                    break;
                case "08":
                    dateofMonth = 8.ToString();
                    break;
                case "09":
                    dateofMonth = 9.ToString();
                    break;

            }

            string meetingStartTime = time.ToString("HH:mm"); //13:25
            //AdminCommon.Document.DocumentHelpers DocumentHelpers = new AdminCommon.Document.DocumentHelpers();
            string dayOfWeek = time.DayOfWeek.ToString();
            string day = null;
            switch (dayOfWeek)
            {
                case "Monday":
                    day = "mon";
                    break;
                case "Tuesday":
                    day = "tue";
                    break;
                case "Wednesday":
                    day = "wed";
                    break;
                case "Thursday":
                    day = "thu";
                    break;
                case "Friday":
                    day = "fri";
                    break;
                case "Saturday":
                    day = "sat";
                    break;
                case "Sunday":
                    day = "sun";
                    break;
            }
            #endregion

            #endregion

            #region ---- Meeting Module ----
            Selenium_Run((driver) =>
            {
                #region Instantiation
                var dashboardPage = DashboardPage.Connect(driver);
                var logoutPage = LogoutPage.Connect(driver);
                #endregion

                #region Login to Møtemodul
                FrontPage.LoginToApplication(driver, ApplicationModules.MeetingModule.GetStringValue());
                #endregion

                #region Change Role
                //Check if role secretary is selected. if yes, then no need to change role else change tole to secretary
                var meeting = MeetingPage.Connect(driver);
                if (meeting.GetRoleName().ToLowerInvariant() != RolesInApplication.SecretaryOfTheBoard.GetStringValue().ToLowerInvariant())
                {                    
                    dashboardPage.ChangeRole(RolesInApplication.SecretaryOfTheBoard);
                }
                #endregion

                #region Create New Meeting
                meeting.CreateNewMeeting(GlobalVariables.MeetingType, meetingEndTimeAndDate, meetingStartTimeAndDate);
                #endregion

                #region Logout from Application
                logoutPage.LogoutApplication();
                #endregion
            });

            #endregion

            #region ---- Saksbehandling Module ----
            Selenium_Run((driver) =>
            {
                #region Instanstiation
                var casePage = CasePage.Connect(driver);
                var registryEntry = RegistryEntryPage.Connect(driver);
                var documentPage = DocumentPage.Connect(driver);
                var meetingPage = MeetingPage.Connect(driver);
                var logoutPage = LogoutPage.Connect(driver);
                #endregion

                #region Login to Saksbehandling
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Add Case
                caseTitle = "Recommendation Summary Proposal Decision ProtocalExtract and Letter to case party";
                GlobalVariables.CaseTitle = caseTitle;
                casePage.CreateCase(GlobalVariables.CaseTitle);
                caseNumber = casePage.GetCaseNumber();
                #endregion

                #region Create Registry, Add Processing, Saksfralegg Doc, Add Instilling & Oppsummering, close document, mark as completed

                #region Create Registry entry CaseDraftType
                registryEntry.CreateRegistryEntry(RegistryEntryType.CaseDraftType.GetStringValue());
                GlobalVariables.RegistryEntryTitle = "Registry Entry - Case Draft for Meeting" + " " + CommonMethods.GetRandomNumber();
                registryEntry.AddRegTitle(GlobalVariables.RegistryEntryTitle);
                #endregion

                #region Add Processing name - Handling
                CommonMethods.PlayWait(4000);
                registryEntry.AddProcessing(GlobalVariables.ProcessName);
                #endregion

                #region Add SaksframleggDoc, Login to application, save and edit document
                documentPage.ClickAndAddDocTemplate(AttachmentType.DocumentTemplate.GetStringValue());
                documentPage.SelectDocument(DocumentTemplateType.Dokumentmal.GetStringValue(), DocumentSubType.Saksframlegg.GetStringValue());
                registryEntry.ClickSaveBttn();
                documentPage.HandleAlertPopupToOpenWordDocument();
                documentPage.DocumentWebLoginWindow();
                GlobalVariables.EditDataStatement = "Draft Creation for new meeting";
                documentPage.EditAndSaveWordDocument(GlobalVariables.EditDataStatement);
                #endregion

                #region Edit Document - Add Instilling & Oppsummering

                CommonMethods.PlayWait(5000);

                #region Enter Installing Text
                meetingPage.AddInstillingTextToSaksframlegg(installingText);
                #endregion

                CommonMethods.PlayWait(5000);

                #region Enter Oppsummering Text
                meetingPage.AddOppsummeringTextToSaksframlegg(oppsummeringText);
                #endregion

                #endregion

                #region Save and close document
                documentPage.SaveWordDocument();
                documentPage.CloseDocument();
                CommonMethods.PlayWait(9000);
                #endregion

                //Mark as complete
                #region Change registry entry status to F/done
                registryEntry.ClickEditButton();
                registryEntry.ChangeREStatusToFerdigOrDone();
                registryEntry.ClickSaveBttn();
                #endregion
                #endregion

                #region Logout from Application
                CommonMethods.PlayWait(4000);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            #region---- Login to Admin Module for 2nd Time and Select 'Reuse existing numbers from this and subsequent meetings ----
            Selenium_Run((driver) =>
            {
                #region Instanstiation
                var casePage = CasePage.Connect(driver);
                var dashboardPage = DashboardPage.Connect(driver);
                var registryEntry = RegistryEntryPage.Connect(driver);
                var documentPage = DocumentPage.Connect(driver);
                var meetingPage = MeetingPage.Connect(driver);
                var logoutPage = LogoutPage.Connect(driver);
                var adminPage = AdminstratorPage.Connect(driver);
                #endregion

                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Change Role to Secretary of Boards - Eng
                CommonMethods.PlayWait(2000);
                dashboardPage.ChangeRole(RolesInApplication.SecretaryOfTheBoard);
                #endregion

                #region Navigate to alle, Select Numbering option and save
                adminPage.NavigateToMenuItem("Utvalgsbehandling", "Styre, råd og utvalg", "Styre, råd og utvalg (alle)");
                CommonMethods.PlayWait(2000);
                adminPage.ClickItemInGrid(GlobalVariables.MeetingType);
                CommonMethods.PlayWait(2000);
                meetingPage.SelectListValueAndSelectByText(MeetingPage.MeetingPageLocators.NumberingSchemeDropdown, "Reuse existing numbers from just this meeting");
                adminPage.Save();
                CommonMethods.PlayWait(4000);
                meetingPage.SelectListValueAndSelectByText(MeetingPage.MeetingPageLocators.NumberingSchemeDropdown, "Only new numbers from the DMB number series");
                adminPage.Save();
                #endregion

                #region Logout from Application
                logoutPage.LogoutApplication();
                #endregion

            });

            #endregion

            #region ---- Meeting Module ----

            Selenium_Run((driver) =>
            {
                #region Instantiation
                var meeting = MeetingPage.Connect(driver);
                var dashboardPage = DashboardPage.Connect(driver);
                var documentPage = DocumentPage.Connect(driver);
                var reportPage = ReportPage.Connect(driver);
                var logoutPage = LogoutPage.Connect(driver);
                #endregion

                #region Login to Møtemodul
                FrontPage.LoginToApplication(driver, ApplicationModules.MeetingModule.GetStringValue());
                #endregion

                #region Select My Meeting and Finish Meeting process in progress bar

                #region Change Role
                //Check if role secretary is selected. if yes, then no need to change role else change tole to secretary

                if (meeting.GetRoleName().ToLowerInvariant() != RolesInApplication.SecretaryOfTheBoard.GetStringValue().ToLowerInvariant())
                {
                    dashboardPage.ChangeRole(RolesInApplication.SecretaryOfTheBoard);
                }
                #endregion

                #region Filter for Kommunestyret
                meeting.FilterAndRemovetMeetingsInDashboard(MeetingTypes.Formannskapet.GetStringValue());
                #endregion

                #region Find My Meeting and Drag from Queue to Case List and click save
                CommonMethods.PlayWait(4000);
                meeting.CalenderDate(day, dateofMonth);
                CommonMethods.PlayWait(2000);
                meeting.SelectMyMeeting(myMeetingTiming, day, dateofMonth);
                CommonMethods.PlayWait(10000);
                meeting.SearchAndDragRegistryEntry(GlobalVariables.RegistryEntryTitle);
                CommonMethods.PlayWait(5000);
                meeting.ClickSaveInCasePlan();

                #region Getting Number from Case Draft
                // Trimming string to Generate Assigned Number to Case Draft
                string assignedNumber1 = meeting.ReturnNumberAssignedToCaseDraft(GlobalVariables.RegistryEntryTitle); // "1 PS 268/18 Registry Entr"
                string TrimmedAssignedNumber1 = assignedNumber1.Trim(new char[] { ' ', 'P', 'S', 'R', 'e', 'g', 'i', 's', 'E', 't', 'r', '-', 'n', 'y', 'C', 'a', 'D', 'f', 'o', 'M', }); // "1 PS 268/18"
                char[] myChar1 = { 'S', 'P', ' ' };
                string actualAssignedNumber1 = TrimmedAssignedNumber1.TrimStart(myChar1); // "268/18"
                string names1 = actualAssignedNumber1; // "268/18"
                int index1 = names1.IndexOf("/"); // 3
                string element1 = names1.Replace("Type", "").Replace("PS", "").Replace("No", "").Replace(".", "").Trim(); // "268/2018"
                string Test = names1.Insert(index1 + "/".Length, "20").ToUpper(); // 268/2018
                var newstring = Test.Replace("Type".ToUpperInvariant(), "").Replace("PS".ToUpperInvariant(), "").Replace("No".ToUpperInvariant(), "").Replace(".", "").Trim();
                actualVal = newstring.Insert(0, "sak"); // "sak268/2018"

                #endregion

                #endregion

                #region Click Create Agenda, Click Start Icon from Progress Bar and start button Case Draft, Add Proposal, Decision and  Save Meeting

                #region Click on Create Agenda
                CommonMethods.PlayWait(4000);
                meeting.ClickCreateAgenda();
                CommonMethods.PlayWait(4000);
                meeting.HandleErrorPopUpAndThrowErrorMessage();
                CommonMethods.PlayWait(8000);

                #endregion

                #region Start Meeting
                //Click Start Meeting
                meeting.ClickStartButton();
                CommonMethods.PlayWait(5000);
                #endregion

                #region Steps to Finish Meeting
                //Click Start Handling
                CommonMethods.PlayWait(5000);
                CommonMethods.RefreshBrowserWindow(driver);
                CommonMethods.PlayWait(10000);
                meeting.ClickStartHandlingButton();
                CommonMethods.PlayWait(5000);

                #region Add Proposal (Suggestion) and Decision (Vedtak)
                meeting.AddNewSuggestion(proposerName);

                // Add Decision 
                meeting.AddBehandling();

                #endregion

                //Click Save Handling in Case Draft
                CommonMethods.PlayWait(5000);
                meeting.ClickSaveHandlingButton();

                //Verify Text -> Finished in left pane
                CommonMethods.PlayWait(8000);
                meeting.VerifyFinishedTextIsVisible();
                #endregion

                #endregion

                #endregion

                #region Click Documents in Meeting Module and verify (Motedokumenter, Saksprotokoller, Saksdokumenter)
                meeting.ClickTabElement(MeetingTabControl.Documents.GetStringValue());

                // Verify Meeting Documents or Motedokumenter
                meeting.ClickAndVerifyMeetingDocuments(MeetingListTabValues.MeetingDocuments.GetStringValue(), false);

                // verify Saksprotokoller
                meeting.ClickAndVerifyMeetingDocuments(MeetingListTabValues.Caseprotocols.GetStringValue(), false);

                // verify Saksdokumenter
                meeting.ClickAndVerifyMeetingDocuments(MeetingListTabValues.Casedocuments.GetStringValue(), false);
                #endregion

                #region Click and Validate Case Protocol or Saksprotokoller

                //Open case protocol
                meeting.ClickAndVerifyMeetingDocuments(MeetingListTabValues.Caseprotocols.GetStringValue());
                meeting.ClickCaseProtocolDocuments(GlobalVariables.RegistryEntryTitle);
                CommonMethods.PlayWait(3000);
                meeting.OpenInDesktopApplication();
                documentPage.HandleAlertPopupToOpenWordDocument();
                documentPage.DocumentWebLoginWindow();
                CommonMethods.PlayWait(10000);
                documentPage.CopyContentsOfOpenedWordDoc();
                string validate = Clipboard.GetText();

                #region Case Protocol Validation

                StringAssert.Contains(validate, GlobalVariables.NewSuggestion, "The new suggestion Text is not available in the case protocol document");
                StringAssert.Contains(validate, GlobalVariables.Decision, "The Decision Text is not available in the case protocol document");
                StringAssert.Contains(validate, proposerName, "The proposer name is not available in the case protocol document");
                StringAssert.Contains(validate, installingText, "The Instilling (Bookmark Recommendation) Text is not available in the case protocol document");
                StringAssert.Contains(validate, oppsummeringText, "The Oppsummering (Bookmark Summary)  is not available in the case protocol document");
                StringAssert.Contains(validate, actualVal, "The number assigned to Case Draft  is not available in the case protocol document");

                //close document
                documentPage.CloseDocument();
                #endregion

                #endregion

                #region Below code is commented. Can be used in other TCs. ---- OpenInDesktopApplication

                #region Click and Validate Meeting Protocol or Moteprotokoll OpenInDesktopApplication

                meeting.ClickAndVerifyMeetingDocuments(MeetingDocListValues.MeetingProtocol.GetStringValue());//Moteprotokoll
                //CommonMethods.PlayWait(3000);
                //meeting.OpenInDesktopApplication();
                //documentPage.HandleAlertPopupToOpenWordDocument();
                //documentPage.DocumentWebLoginWindow();
                //CommonMethods.PlayWait(5000);

                //#region Validate PDF file is real
                //meeting.VerifyPDFfile();
                //#endregion

                //meeting.SearchAndVerifyInstillingAndOppsummeringTextInPDF(installingText, oppsummeringText);
                //meeting.CopyContentFromPDFFile();

                //string PdfReader = Clipboard.GetText();
                ////Validating Text in PDF
                //CommonMethods.PlayWait(10000);
                //StringAssert.Contains(PdfReader, element1, "PS number assigned to Case Draft in Meeting Protocol is not available ");
                //StringAssert.Contains(PdfReader, actualVal, "PS number assigned to Case Draft in Meeting Protocol is not available ");
                //StringAssert.Contains(PdfReader, GlobalVariables.NewSuggestion, "The new suggestion Text is not available in the Meeting protocol document");
                //StringAssert.Contains(PdfReader, GlobalVariables.Decision, "The Decision Text is not available in the Meeting protocol document");

                ////Close PDF document
                //documentPage.ClosePDF();
                #endregion

                #endregion

                #region Click and Validate Meeting Protocol or Moteprotokoll open in office online - Handle Bug 199932
                CommonMethods.PlayWait(3000);
                meeting.OpenInOfficeOnline();
                documentPage.HandleAlertPopupToOpenWordDocument();
                documentPage.DocumentWebLoginWindow();
                CommonMethods.PlayWait(10000);
                meeting.VerifyPDFOpenedInOfficeOnline();
                #endregion

                #region Finish Meeting, Click After button is activated

                meeting.ClickTabElement(MeetingTabControl.CaseList.GetStringValue());

                //Click Finished
                CommonMethods.PlayWait(5000);
                meeting.ClickFinishedButton();

                //Click After
                CommonMethods.PlayWait(2000);
                meeting.ClickAfterButton();

                CommonMethods.PlayWait(4000);

                #endregion

                #region Protokollering grid (Left hand side):
                CommonMethods.PlayWait(1000);
                CommonMethods.RefreshBrowserWindow(driver);
                CommonMethods.PlayWait(10000);
                // Select record and from quick menu select "Protokollutdrag" option 
                meeting.OpenProtocolAgendaItemInRecordManagementModule();
                CommonMethods.PlayWait(4000);
                //Close browser window
                reportPage.CloseCurrentBrowserTab();
                #endregion

                #region Logout from Application
                logoutPage.LogoutApplication();
                #endregion
            });

            #endregion

            #region ---- Saksbehandling Module ----

            Selenium_Run((driver) =>
            {
                #region Instantiation
                var casePage = CasePage.Connect(driver);
                var registryPage = RegistryEntryPage.Connect(driver);
                var meeting = MeetingPage.Connect(driver);
                var dashboardPage = DashboardPage.Connect(driver);
                var documentPage = DocumentPage.Connect(driver);
                var reportPage = ReportPage.Connect(driver);
                var logoutPage = LogoutPage.Connect(driver);
                #endregion

                #region Login to Saksbehandling
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Search for Case and Open Case
                casePage.QuickSearchCaseId(caseNumber);
                CommonMethods.PlayWait(3000);
                #endregion

                #region Validate X (Internt notat uten oppfølging/internal) type registry entry

                casePage.ClickOnButton("Registry entries");
                registryPage.ClickRegistryByType("Internt notat uten oppfølging/internal", false);

                // Open Document 
                registryPage.ClickAttachmentOption("Summary Registry Entry - Case Draft", "Open");

                #region Validate X Type Doc Content
                documentPage.HandleAlertPopupToOpenWordDocument();
                documentPage.DocumentWebLoginWindow();
                CommonMethods.PlayWait(10000);
                documentPage.CopyContentsOfOpenedWordDoc();
                string validateXTypeDocContent = Clipboard.GetText();
                StringAssert.Contains(validateXTypeDocContent, GlobalVariables.NewSuggestion, "The new suggestion Text is not available in the X Type document");
                StringAssert.Contains(validateXTypeDocContent, GlobalVariables.Decision, "The Decision Text is not available in the X Type document");
                StringAssert.Contains(validateXTypeDocContent, proposerName, "The proposer name is not available in the X Type document");
                StringAssert.Contains(validateXTypeDocContent, installingText, "The Instilling (Bookmark Recommendation) Text is not available in the X Type document");
                StringAssert.Contains(validateXTypeDocContent, oppsummeringText, "The Oppsummering (Bookmark Summary)  is not available in the X Type document");

                //close document
                documentPage.CloseDocument();
                #endregion

                #endregion

                #region Create 3 Case Parties in RE

                #region Add Case Parties -> Case Party 1 with AA
                casePage.AddParties("Shortname", "AA");
                #endregion

                #region Add Case Parties -> Case Party 2 with AB
                casePage.AddParties("Shortname", "AB");
                #endregion

                #region Add Case Parties -> Case Party 3 with SV
                casePage.AddParties("Shortname", "SV");
                #endregion

                CommonMethods.PlayWait(3000);

                #endregion

                #region Go back to Registry Entry
                casePage.ClickOnButton("Registry entries");
                CommonMethods.PlayWait(3000);
                registryPage.ClickRegistryByType("Saksframlegg/Case draft");
                CommonMethods.PlayWait(3000);
                #endregion

                #region Click on RE menu item and choose Create party letter (Include decision) and Validate

                // Click on choose Create party letter (Include decision)
                registryPage.RegistryEntryViewMenu("Create party letter (Include decision)");

                #region Validate choose Create party letter (Include decision)
                CommonMethods.PlayWait(5000);

                // Valiate U Type RE
                registryPage.ValidateUTypeRE("Utgående post/Outbound");

                // Same RE Title
                registryPage.ValidateRETitle(GlobalVariables.RegistryEntryTitle);

                // Validate Case Parties
                registryPage.ValidateCasePartiesInRE("Anda Apotek");
                registryPage.ValidateCasePartiesInRE("Aktiebolaget Bolinda");
                registryPage.ValidateCasePartiesInRE("Sosialistisk Venstreparti");

                #endregion


                // add document
                documentPage.ClickAndAddDocTemplate(AttachmentType.DocumentTemplate.GetStringValue());
                documentPage.SelectDocument(DocumentTemplateType.Dokumentmal.GetStringValue(), DocumentSubType.Standardbrev.GetStringValue());
                registryPage.ClickSaveBttn();
                CommonMethods.PlayWait(5000);
                registryPage.ClickAttachmentOption("Standard brev", "Open");
                documentPage.HandleAlertPopupToOpenWordDocument();
                documentPage.DocumentWebLoginWindow("guilt");
                CommonMethods.PlayWait(8000);
                AutoItX.Send("{LEFT}");
                AutoItX.Send("{ENTER}");
                CommonMethods.PlayWait(3000);

                #region Validate Decision, Proposer, Proposal, Instilling and Oppsummering in Include Decision
                documentPage.CopyContentsOfOpenedWordDoc();
                string validateForIncludeDecision = Clipboard.GetText();
                CommonMethods.PlayWait(2000);
                StringAssert.Contains(validateForIncludeDecision, GlobalVariables.NewSuggestion, "The new suggestion Text is not available in the Include decision document");
                StringAssert.Contains(validateForIncludeDecision, GlobalVariables.Decision, "The Decision Text is not available in the Include decision document");
                StringAssert.Contains(validateForIncludeDecision, proposerName, "The proposer name is not available in the Include decision document");
                StringAssert.Contains(validateForIncludeDecision, installingText, "The Instilling (Bookmark Recommendation) Text is not available in the Include decision document");
                StringAssert.Contains(validateForIncludeDecision, oppsummeringText, "The Oppsummering (Bookmark Summary)  is not available in the Include decision document");
                StringAssert.Contains(validateForIncludeDecision, actualVal, "The case draft value assigned in meeting module  is not available in the Include decision document");

                //close document
                documentPage.CloseDocument();
                #endregion

                CommonMethods.PlayWait(5000);

                #endregion

                #region Click Case Draft 

                CommonMethods.PlayWait(3000);
                // Select Create party letter (Include complete procedure) from Menu Item
                casePage.ClickOnButton("Registry entries");
                CommonMethods.PlayWait(3000);
                registryPage.ClickRegistryByType("Saksframlegg/Case draft");
                CommonMethods.PlayWait(3000);
                #endregion

                #region Click on RE menu item and choose Create party letter (Include complete procedure) and Validate

                // Click on choose Create party letter (Include decision)
                registryPage.RegistryEntryViewMenu("Create party letter (Include complete procedure)");

                #region Validate "Create party letter (Include complete procedure)"

                // Valiate U Type RE
                registryPage.ValidateUTypeRE("Utgående post/Outbound");

                // validate Same RE Title
                registryPage.ValidateRETitle(GlobalVariables.RegistryEntryTitle);

                // Validate Case Parties
                registryPage.ValidateCasePartiesInRE("Anda Apotek");
                registryPage.ValidateCasePartiesInRE("Aktiebolaget Bolinda");
                registryPage.ValidateCasePartiesInRE("Sosialistisk Venstreparti");

                #endregion

                // add document
                documentPage.ClickAndAddDocTemplate(AttachmentType.DocumentTemplate.GetStringValue());
                documentPage.SelectDocument(DocumentTemplateType.Dokumentmal.GetStringValue(), DocumentSubType.BrevDocument.GetStringValue());
                registryPage.ClickSaveBttn();

                registryPage.ClickAttachmentOption("Brev - Word", "Open");
                documentPage.HandleAlertPopupToOpenWordDocument();
                documentPage.DocumentWebLoginWindow("guilt");
                CommonMethods.PlayWait(8000);
                AutoItX.Send("{LEFT}");
                AutoItX.Send("{ENTER}");
                CommonMethods.PlayWait(3000);

                #region Validate Decision, Proposer, Proposal, Instilling and Oppsummering in Include Complete Procedure
                documentPage.CopyContentsOfOpenedWordDoc();
                string validateForIncludeCompleteProcedure = Clipboard.GetText();
                CommonMethods.PlayWait(2000);
                StringAssert.Contains(validateForIncludeCompleteProcedure, GlobalVariables.NewSuggestion, "The new suggestion Text is not available in the Include Complete Procedure document");
                StringAssert.Contains(validateForIncludeCompleteProcedure, GlobalVariables.Decision, "The Decision Text is not available in the Include Complete Procedure document");
                StringAssert.Contains(validateForIncludeCompleteProcedure, proposerName, "The proposer name is not available in the Include Complete Procedure document");
                StringAssert.Contains(validateForIncludeCompleteProcedure, installingText, "The Instilling (Bookmark Recommendation) Text is not available in the Include Complete Procedure document");
                StringAssert.Contains(validateForIncludeCompleteProcedure, oppsummeringText, "The Oppsummering (Bookmark Summary)  is not available in the Include Complete Procedure document");

                CommonMethods.PlayWait(5000);
                // close document
                documentPage.CloseDocument();
                #endregion

                CommonMethods.PlayWait(5000);
                #endregion

                #region Logout from Application
                logoutPage.LogoutApplication();
                #endregion

            });
            #endregion
        }

        #region Private Methods



        #region Open Elements Window And Login And Edit Save Document
        /// <summary>
        /// Open Elements Window And Login And Edit Save Document
        /// </summary>
        /// <param name="driver"></param>
        private void OpenElementsWindowAndLoginAndEditSaveDocument(RemoteWebDriver driver, string userName = "guilt")
        {
            #region Handle the chrome window "Open Elements Window"
            var documentPage = DocumentPage.Connect(driver);
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

        #endregion

        #region Additional test attributes

        //Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void MyTestInitialize()
        {
            CommonMethods.ProcessKill("WinWord");
            CommonMethods.ProcessKill("Excel");
            CommonMethods.ProcessKill("AcroRd32");
        }

        ////Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            CommonMethods.ProcessKill("WinWord");
            CommonMethods.ProcessKill("Excel");
            CommonMethods.ProcessKill("AcroRd32");
        }

        #endregion
    }
}
