using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using System;
using AutoIt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Interactions;
using System.Linq;

namespace Selenium.Tests.Pages
{
    public class MeetingPage
    {
        public static class MeetingPageLocators
        {
            public static readonly By RoleName = By.XPath("//div[@class='text-overflow-ellipsis smaller-text']");
            public static readonly By NewMeetingButton = By.XPath("//button[@class='btn btn-sm btn-default btn-new-meeting toggle-close-open']");
            public static readonly By NewMeetingPublishedButton = By.XPath("//input[contains(text(),'Published')]");
            public static readonly By NewMeetingSaveButton = By.XPath("//button[@class='btn btn-default btn-success']");
            public static readonly By NumberingSchemeDropdown= By.Id("NumberingScheme");
            public static readonly By RightFilterButton = By.XPath("//ancestor::div[@class = 'list-group queue-list-group']//button[@class='btn btn-sm btn-link btn-filter filter-toggle-button no-theme collapsed btn-custom enhanced']");
            public static readonly By LeftFilterButton = By.XPath("//div[@class = 'list-group']//button[@class='btn btn-sm btn-link btn-filter filter-toggle-button no-theme collapsed btn-custom enhanced']");
            public static readonly By RightFilterText = By.XPath("//input[@id='leftColumnSearch']");
            public static readonly By DropDestinationBox1 = By.XPath("//ul[@class='listview list-group item-list scroll-list ko_container ui-sortable']");
            public static readonly By DropDestinationBox2 = By.XPath("//ul[@class='listview list-group item-list scroll-list ui-sortable ko_container']");
            public static readonly By MeetingSave = By.XPath("//span[contains(text(),'Save')]");
            public static readonly By MeetingCreateAgenda = By.XPath("//span[contains(text(),'Create agenda')]");
            public static readonly By MeetingStartButton = By.XPath("//div[contains(text(),'Start')]");
            public static readonly By StartHandlingButton = By.XPath("//div[@class='ready'][text()='Start']");
            public static readonly By AddSuggestionButton = By.XPath("//span[contains(text(),'New suggestion')]");
            public static readonly By AddSuggestionDate = By.XPath("//input[@id='exampleInputdate']");
            public static readonly By NewSuggestionTextArea = By.XPath("//div[contains(@id,'newSuggestionsRegister')]//child::iframe");
            public static readonly By NewSuggestionSaveButton = By.XPath("//button[@class='btn btn-sm btn-default form-group-margin-4-2 btn-success']//span[contains(text(),'Save')]");
            public static readonly By NewDecisionTextArea = By.XPath("//div[contains(@id,'decisionId')]//child::iframe");
            public static readonly By NewDecisionSaveButton = By.XPath("//button[@class='btn btn-sm btn-default btn-success']//span[contains(text(),'Save')]");
            public static readonly By SaveHandlingButton = By.XPath("//div[@class='during'][text() = 'Save']");
            public static readonly By FinishedHandlingText = By.XPath("//div[@class='finished'][text() = 'Finished']");
            public static readonly By DecisionTab = By.XPath("//div[contains(text(),'Decision')]");
            public static readonly By OpenInOfficeOnline = By.XPath("//button[@class='btn btn-sm btn-default btn-doc-menu'][@title = 'Open in Office Online']");
            public static readonly By OpenInDesktopApplication = By.XPath("//button[@class='btn btn-sm btn-default btn-doc-menu'][@title = 'Open in desktop application']");
            public static readonly By FinishedButton = By.XPath("//li[contains(@class,'arrow-next arrow-active')]");
            public static readonly By AfterButton = By.XPath("//li[@class='state active']//div[contains(text(),'After')]");
            public static readonly By OpenProtocolAgendaMenuButton = By.XPath("//button[@class='dmb-filter-trigger btn btn-sm btn-default pull-left generic-toggle-close-open collapsed']");
            public static readonly By OpenProtocolAgendaButton = By.XPath("//span[contains(text(),'Open registryEntry in record management module')]");
            public static readonly By PDFErrorMessage = By.XPath("//div[@id='WACDialogText']");
            public static readonly By PDFErrorMessage2 = By.XPath("//div[@class='warning-message']");
        }
        private readonly RemoteWebDriver _driver;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the MeetingPage class.
        /// </summary>
        /// <param name="driver">.</param>
        public MeetingPage(RemoteWebDriver driver)
        {
            _driver = driver;
        }

        public static MeetingPage Connect(RemoteWebDriver driver)
        {
            return new MeetingPage(driver);
        }
        #endregion

        #region Get Role Name
        /// <summary>
        /// Gets the role name already login user
        /// </summary>
        /// <returns></returns>
        public string GetRoleName()
        {
            var roleNameControl = _driver.FindElement(MeetingPageLocators.RoleName);
            roleNameControl.DrawHighlight();
            return roleNameControl.Text;
        }
        #endregion

        #region CreateNewMeeting
        /// <summary>
        /// Clicking on Create New Meeting Button in Meeting Module to add a new meeting
        /// </summary>
        public void CreateNewMeeting(string meetingType, string endDate, string startDate = "")
        {
            //Click New Meeting
            var newMeetingButton = _driver.FindElement(MeetingPageLocators.NewMeetingButton);
            newMeetingButton.DrawHighlight();
            newMeetingButton.Click();
            CommonMethods.PlayWait(3000);

            // Selecting council type
            SelectKendoDropdownUsingPartialText("Select council", meetingType, meetingType, meetingType);

            // From Date
            if (startDate != "")
            {
                AddCalendarDateControl("From", startDate);
            }

            //To Date
            AddCalendarDateControl("To", endDate);

            // Mark as published
            var publishedElement = _driver.FindElement(MeetingPageLocators.NewMeetingPublishedButton);
            publishedElement.DrawHighlight();
            publishedElement.Click();
            _driver.HandleErrorPopUpAndThrowErrorMessage();

            CommonMethods.PlayWait(5000);
            // Click Save
            var saveButton = _driver.FindElement(MeetingPageLocators.NewMeetingSaveButton);
            saveButton.DrawHighlight();
            saveButton.Click();
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Select dropdown value
        /// <summary>
        /// Select dropdown value from the Kendo dropdown
        /// </summary>
        /// <param name="dropdownLabelName"></param>
        /// <param name="dropdownValue"></param>
        public void SelectKendoDropdownAndAddValue(string dropdownLabelName, string dropdownValue)
        {
            try
            {
                _driver.SelectKendoDropdownAndAddValue(dropdownLabelName, dropdownValue);
            }
            catch (Exception)
            {
                _driver.SelectKendoDropdownAndAddValues(dropdownLabelName, dropdownValue);
            }

        }
        #endregion

        #region Add value to Calendar Date Control
        /// <summary>
        /// Adds value to the calendar date control
        /// </summary>
        ///<param name="controlName"></param>
        /// <param name="value"></param>
        public void AddCalendarDateControl(string controlName, string value)
        {
            var element = _driver.FindElement(By.XPath(string.Format("//label[contains(text(),'{0}')]//following-sibling::div//child::input", controlName)));
            element.DrawHighlight();
            element.SendKeys(value);
        }
        #endregion

        #region Add Instilling Text to Saksframlegg doc
        /// <summary>
        /// Add Instilling Text to Saksframlegg doc
        /// </summary>
        /// <param name="installingText"></param>
        public void AddInstillingTextToSaksframlegg(string installingText)
        {
            // Enter Installing Text
            AutoItX.WinActivate("[CLASS:OpusApp]", "");
            CommonMethods.PlayWait(2000);
            AutoItX.Send("^f");
            CommonMethods.PlayWait(2000);
            AutoItX.Send("Innstilling");
            AutoItX.Send("{ENTER}");
            AutoItX.Send("{ESC}");
            CommonMethods.PlayWait(2000);
            AutoItX.Send("{RIGHT}");
            AutoItX.Send("{ENTER}");
            AutoItX.Send(installingText);
        }
        #endregion

        #region Add Oppsummering Text to Saksframlegg doc
        /// <summary>
        /// Add Oppsummering Text to Saksframlegg doc
        /// </summary>
        /// <param name="oppsummeringText"></param>
        public void AddOppsummeringTextToSaksframlegg(string oppsummeringText)
        {
            // Enter Oppsummering Text
            AutoItX.WinActivate("[CLASS:OpusApp]", "");
            CommonMethods.PlayWait(2000);
            AutoItX.Send("^f");
            CommonMethods.PlayWait(2000);
            AutoItX.Send("^a");
            AutoItX.Send("{DEL}");
            CommonMethods.PlayWait(2000);
            AutoItX.Send("oppsummering");
            AutoItX.Send("{ENTER}");
            AutoItX.Send("{ESC}");
            CommonMethods.PlayWait(2000);
            AutoItX.Send("{RIGHT}");
            AutoItX.Send("{RIGHT}");
            AutoItX.Send("{RIGHT}");
            AutoItX.Send("{RIGHT}");
            AutoItX.Send("{RIGHT}");
            AutoItX.Send("{ENTER}");
            AutoItX.Send("{ENTER}");
            AutoItX.Send(oppsummeringText);
            AutoItX.Send("^s");
        }
        #endregion

        #region Get innner text from the drop down
        /// <summary>
        /// Get innner text from the drop down
        /// </summary>
        /// <param name="dropdownName"></param>
        /// <returns></returns>
        public string GetDropDownValue(string dropdownName)
        {
            return _driver.GetDropDownValue(dropdownName);
        }
        #endregion

        #region Select Kendo dropdown with partial text
        /// <summary>
        /// Select Kendo dropdown with partial text
        /// </summary>
        /// <param name="dropdownName">label name</param>
        /// <param name="dropdownStartsWith">Name starts with</param>
        /// <param name="dropdownValue">complete name</param>
        /// <param name="dropdownTitle">send the complete title from tag (inspect element)</param>
        /// <returns></returns>
        public bool SelectKendoDropdownUsingPartialText(string dropdownName, string dropdownStartsWith, string dropdownValue, string dropdownTitle)
        {
            return _driver.SelectKendoDropdownUsingPartialText(dropdownName, dropdownStartsWith, dropdownValue, dropdownTitle);
        }
        #endregion

        #region SelectListValueAndSelectByText
        /// <summary>
        /// Public method which includes logic related to Select the value from List box element through Xpath.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        /// <param name="by">Parameter of Enum type By .</param>
        /// <param name="text">Parameter of type System.String for text.</param>
        /// <returns>True or false</returns>
        public bool SelectListValueAndSelectByText(By by, string text)
        {
           return _driver.SelectListValueAndSelectByText(by, text);
        }
        #endregion

        #region Delete or remove meeting in the meetings dashboard
        /// <summary>
        /// Delete or remove meeting in the meetings dashboard
        /// </summary>
        public void FilterAndRemovetMeetingsInDashboard(string meetingType)
        {
            var element = _driver.FindElement(By.XPath(string.Format("//div[@class='{0} event-header']//i[@class='glyphicon glyphicon-remove']", meetingType)));
            element.DrawHighlight();
            element.Click();
        }
        #endregion

        #region Identify Calender Date
        /// <summary>
        /// Identify Calender date in Meeting module front page 
        /// </summary>
        public void CalenderDate(string day, string dateofMonth)
        {
            try
            {
                var element = _driver.FindElement(By.XPath(string.Format("//td[@class = 'fc-day-top fc-{0} fc-today ui-state-highlight']//child::span[text() = '{1}']", day, dateofMonth)));
                Actions actions = new Actions(_driver);
                actions.MoveToElement(element).Perform();

                _driver.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch (Exception)
            {
                throw new AssertFailedException("Meeting date not found. Day = " + day + "Date =" + dateofMonth);
            }
        }        
        #endregion

        #region Click on Element - Java Script
        /// <summary>
        /// Click on Element - Java Script
        /// </summary>
        /// <param name="element"></param>
        public void Javascriptclick(string element)
        {
            IWebElement webElement = _driver.FindElement(By.XPath(element));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

            js.ExecuteScript("arguments[0].click();", webElement);
        }
        #endregion

        #region Selecting newly created meeting

        /// <summary>
        /// Selecting newly created meeting
        /// </summary>
        #region Select My Meeting
        public void SelectMyMeeting(string meetingTiming, string day, string dateofMonth)
        {
            try
            {
                var element = _driver.FindElements(By.XPath(string.Format("//a[@class='fc-day-grid-event fc-h-event fc-event fc-start fc-end']//child::div//child::div[contains(text(),'{0}')]", meetingTiming)));
                Actions actions = new Actions(_driver);
                actions.MoveToElement(element.LastOrDefault()).Perform();
                element.LastOrDefault().DrawHighlight();
                CommonMethods.PlayWait(3000);
                element.LastOrDefault().Click();
            }
            catch (Exception)
            {
                throw new AssertFailedException("Meeting date not found. Day = " + day + "Date =" + dateofMonth + "Timings = " + meetingTiming);
            }
        }

        #endregion

        #endregion

        #region Click Search for Registry Entry and Drag to Case Plan
        /// <summary>
        /// Click Search for Registry Entry and Drag to Case Plan
        /// </summary>
        /// <param name="rightFilter"></param>
        /// <param name="registryEntryTitle"></param>
        public void SearchAndDragRegistryEntry(string registryEntryTitle, bool rightFilter = true)
        {
            CommonMethods.PlayWait(5000);

            #region Click on Filter button
            if (rightFilter)
            {
                var rightFilterButton = _driver.FindElement(MeetingPageLocators.RightFilterButton);
                rightFilterButton.DrawHighlight();
                rightFilterButton.Click();
            }
            else
            {
                var leftFilterButton = _driver.FindElement(MeetingPageLocators.LeftFilterButton);
                leftFilterButton.DrawHighlight();
                leftFilterButton.Click();
            }
            #endregion

            CommonMethods.PlayWait(3000);

            #region Type and Search the case title
            // Click Search edit box and search for Case Draft
            try
            {
                var rightFilterText = _driver.FindElement(MeetingPageLocators.RightFilterText);
                rightFilterText.DrawHighlight();
                rightFilterText.SendKeys(registryEntryTitle);
            }
            catch
            {
                CommonMethods.PlayWait(3000);
                AutoItX.Send("{TAB}");
                CommonMethods.PlayWait(1000);
                AutoItX.Send(registryEntryTitle);
            }         
            #endregion

            CommonMethods.PlayWait(15000);

            #region Drag and drop case title
            try
            {
                var caseDraft = _driver.FindElement(By.XPath(string.Format("//a[contains(text(),'{0}')]", registryEntryTitle)));
                caseDraft.DrawHighlight();

                IWebElement destinationBox;
                try
                {
                    destinationBox = _driver.FindElement(MeetingPageLocators.DropDestinationBox1);
                }
                catch 
                {
                    destinationBox = _driver.FindElement(MeetingPageLocators.DropDestinationBox2);
                }
                
                destinationBox.DrawHighlight();

                Actions clickANdHold = new Actions(_driver);
                clickANdHold.MoveToElement(caseDraft).ClickAndHold(caseDraft).Perform();

                DragAndDrop(caseDraft, destinationBox);

                Actions releaseCaseDraft = new Actions(_driver);
                releaseCaseDraft.Release(caseDraft).Perform();
            }
            catch
            {
                
            }
            #endregion

            CommonMethods.PlayWait(5000);
        }
        #endregion

        #region DragAndDrop
        /// <summary>
        /// Public method which includes logic related to Drag and drop the elements
        /// </summary>
        /// <param name="SourceElemnt">Parameter of type OpenQA.Selenium.IwebElement for SourceElement.</param>
        /// <param name="DestinationElmnt">Parameter of type OpenQA.Selenium.IwebElement for DestinationElement.</param>
        public void DragAndDrop(IWebElement sourceElement, IWebElement destinationElement)
        {
            _driver.DragAndDrop(sourceElement, destinationElement);
        }
        #endregion

        #region Click Save in Meeting
        /// <summary>
        /// Click Save in Case Plan
        /// </summary>
        public void ClickSaveInCasePlan()
        {
            var saveButton = _driver.FindElement(MeetingPageLocators.MeetingSave);
            saveButton.DrawHighlight();
            saveButton.Click();
            HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Return the case draft number
        /// <summary>
        /// Return the ase draft number
        /// </summary>
        /// <param name="registryDraftTitle"></param>
        /// <returns></returns>
        public string ReturnNumberAssignedToCaseDraft(string registryDraftTitle)
        {
            CommonMethods.PlayWait(5000);
            var numberElement = _driver.FindElement(By.XPath(string.Format("//li//a[contains(text(), '{0}')]//parent::div//preceding-sibling::div[1]//a", registryDraftTitle)));
            if(string.IsNullOrEmpty(numberElement.Text))
            {
                throw new AssertFailedException("Unable to get the number assigned for the case draft = " + registryDraftTitle);
            }
            numberElement.DrawHighlight();
            return numberElement.Text.Trim();
        }
        #endregion

        #region Create Agenda
        /// <summary>
        /// Click Create Agenda button
        /// </summary>
        public void ClickCreateAgenda()
        {
            var createAgendaButton = _driver.FindElement(MeetingPageLocators.MeetingCreateAgenda);
           
            if (createAgendaButton.Displayed)
            {
                createAgendaButton.DrawHighlight();
                createAgendaButton.Click();
                HandleErrorPopUpAndThrowErrorMessage();
            }
            else
            {
                Assert.Fail("Create Agenda button is disabled or not displayed");
            }
        }
        #endregion

        #region Handle red color popup error message
        /// <summary>
        /// Handle red color Error PopUp And Throw Error Message
        /// </summary>
        public void HandleErrorPopUpAndThrowErrorMessage()
        {
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Start Meeting
        /// <summary>
        /// Click Start button
        /// </summary>
        public void ClickStartButton()
        {
            var startButton = _driver.FindElement(MeetingPageLocators.MeetingStartButton);

            if (startButton.Displayed)
            {
                startButton.DrawHighlight();
                startButton.Click();
                HandleErrorPopUpAndThrowErrorMessage();
            }
            else
            {
                Assert.Fail("Start button is not activated");
            }
        }
        #endregion

        #region Click Start Handling button
        /// <summary>
        /// Click Start Handling button in the left pane of registry entry. Click Start Meeting to enable this button
        /// </summary>
        public void ClickStartHandlingButton()
        {
            var startHandling = _driver.FindElement(MeetingPageLocators.StartHandlingButton);
            _driver.WaitForElementVisible(MeetingPageLocators.StartHandlingButton);
            startHandling.DrawHighlight();
            startHandling.Click();
            HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Add new Suggestion
        /// <summary>
        /// Open Proposal and Add new suggestion
        /// </summary>
        public void AddNewSuggestion(string proposerName)
        {
            var time = DateTime.Now;  //12.02.2018 07:04:05   4:25 5:00
            string meetingStartTimeAndDate = time.ToString("dd.MM.yyyy");

            // Click on Add Suggestion
            var newGuggestionButton = _driver.FindElement(MeetingPageLocators.AddSuggestionButton);
            newGuggestionButton.DrawHighlight();
            newGuggestionButton.Click();
            HandleErrorPopUpAndThrowErrorMessage();

            CommonMethods.PlayWait(3000);

            // Add Proposer
            SelectKendoDropdownUsingPartialText("Proposer", proposerName, "Jarle Trydal (Medlem - Arbeiderpartiet)", "Jarle Trydal (Medlem - Arbeiderpartiet)");

            CommonMethods.PlayWait(3000);

            // Add date
            var newGuggestionDate = _driver.FindElement(MeetingPageLocators.AddSuggestionDate);
            newGuggestionDate.SendKeys(meetingStartTimeAndDate.Replace(".", "/"));

            CommonMethods.PlayWait(2000);

            // Write a proposal
            AutoItX.Send("{TAB}");
            AutoItX.Send(GlobalVariables.NewSuggestion);

            CommonMethods.PlayWait(2000);

            // Click Save
            var newGuggestionSaveButton = _driver.FindElement(MeetingPageLocators.NewSuggestionSaveButton);
            newGuggestionSaveButton.Click();
            HandleErrorPopUpAndThrowErrorMessage();

            // New Suggestion Closure
            var newGuggestionButtonClosure = _driver.FindElement(MeetingPageLocators.AddSuggestionButton);
            newGuggestionButtonClosure.DrawHighlight();
            newGuggestionButtonClosure.Click();

            CommonMethods.PlayWait(2000);
        }
        #endregion

        #region Add Behandling or Decision
        public void AddBehandling()
        {
            var newDecisionTextArea = _driver.FindElement(MeetingPageLocators.DecisionTab);
            newDecisionTextArea.Click();
            CommonMethods.PlayWait(2000);
            newDecisionTextArea.Click();
            CommonMethods.PlayWait(2000);
            AutoItX.Send("{TAB}");
            AutoItX.Send(GlobalVariables.Decision);
            CommonMethods.PlayWait(2000);

            //Click Save
            var newDecisionSaveButton = _driver.FindElement(MeetingPageLocators.NewDecisionSaveButton);
            newDecisionSaveButton.DrawHighlight();
            newDecisionSaveButton.Click();
            HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Click Save Handling button 
        /// <summary>
        /// Click Save Handling button in the left pane of registry entry. Click Start Meeting to enable this button
        /// </summary>
        public void ClickSaveHandlingButton()
        {
            var saveHandlingButton = _driver.FindElement(MeetingPageLocators.SaveHandlingButton);
            saveHandlingButton.DrawHighlight();
            saveHandlingButton.Click();
            HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Verify Finished Text and its status
        /// <summary>
        /// Verify Finished Text and its status
        /// </summary>
        public void VerifyFinishedTextIsVisible()
        {
            var finishedHandlingText = _driver.FindElement(MeetingPageLocators.FinishedHandlingText);
            finishedHandlingText.DrawHighlight();
            Assert.IsTrue(finishedHandlingText.Displayed, "Finished Text is not available");
            Assert.IsTrue(finishedHandlingText.Enabled, "Finished Text is disabled");
        }
        #endregion

        #region Click Documents Tab element
        /// <summary>
        /// Click Tab Elements  =  Documents
        /// </summary>
        /// <param name="tabElementName"></param>
        public void ClickTabElement(string tabElementName)
        {
            var tabElement = _driver.FindElement(By.XPath(string.Format("//a[@class='tab-element'] [text()='{0}']", tabElementName)));
            tabElement.DrawHighlight();
            tabElement.Click();
        }
        #endregion

        #region Verify Meeting Documents Tab is available

        /// <summary>
        /// Verify Meeting Document Aka Motedokumenter
        /// </summary>
        /// <param name="listTabName"></param>
        /// <param name="clickTabElement"></param>
        public void ClickAndVerifyMeetingDocuments(string listTabName, bool clickTabElement = true)
        {
            CommonMethods.PlayWait(3000);
            var documentTabs = _driver.FindElement(By.XPath(string.Format("//div[contains(text(),'{0}')]", listTabName)));
            documentTabs.DrawHighlight();
            Assert.IsTrue(documentTabs.Displayed, "Document Tab is not displayed " + listTabName);
            if(clickTabElement)
            {
                documentTabs.Click();
            }
        }
        #endregion

        #region Click Case Protocol Documents Under Documents Tab
        /// <summary>
        /// Click Case Protocol Documents Under Documents Tab
        /// </summary>
        public void ClickCaseProtocolDocuments(string caseDraftTitle)
        {
            CommonMethods.PlayWait(3000);
            var documentTabs = _driver.FindElement(By.XPath(string.Format("//div[contains(text(),'{0}')]", caseDraftTitle)));
            documentTabs.DrawHighlight();
            documentTabs.Click();
        }
        #endregion

        #region Click Open in Desktop Application button
        /// <summary>
        /// Click Open in Desktop Application button
        /// </summary>
        public void OpenInDesktopApplication()
        {
            CommonMethods.PlayWait(3000);
            var openDesktopApplicationElement = _driver.FindElement(MeetingPageLocators.OpenInDesktopApplication);
            openDesktopApplicationElement.DrawHighlight();
            _driver.MouseHoverClickOnElement(openDesktopApplicationElement);           
        }
        #endregion

        #region Copy contents from opened PDF file
        /// <summary>
        ///Copy contents from opened PDF file
        /// </summary>
        public void CopyContentFromPDFFile()
        {
            CommonMethods.PlayWait(5000);
            AutoItX.WinActivate("Adobe Acrobat Reader DC");
            AutoItX.Send("^a");
            AutoItX.Send("^c");
            AutoItX.Send("{ENTER}");
            //Get the contents in the clipboard
            CommonMethods.PlayWait(3000);
        }
        #endregion

        #region VerifyPDFfile
        /// <summary>
        /// To verify any File if it is PDF or not
        /// </summary>
        public void VerifyPDFfile()
        {
            AutoItX.WinActivate("Adobe Acrobat Reader DC");
            AutoItX.AutoItSetOption("WinTitleMatchMode", 2);

            if (AutoItX.WinExists("Adobe Acrobat Reader DC", "").ToString() == "1")
            {
                AutoItX.Send("{Enter}");
                //PDF exist
            }
            else
            {
                Assert.Fail("Fail to convert into pdf. PDF file is not opened.");
            }
        }
        #endregion

        #region VerifyPDFfile exist with title
        /// <summary>
        /// To verify any File if it is PDF or not
        /// </summary>
        public void VerifyPDFfile(string pdfTitle)
        {
            AutoItX.WinActivate("Adobe Acrobat Reader DC");
            AutoItX.AutoItSetOption("WinTitleMatchMode", 2);

            if (AutoItX.WinExists(pdfTitle + ".pdf - Adobe Acrobat Reader DC", "").ToString() == "1")
            {
                AutoItX.Send("{Enter}");
                //PDF exist
            }
            else
            {
                Assert.Fail("Fail to convert into pdf. PDF file is not opened.");
            }
        }
        #endregion

        #region Verify PDF document contains Innstiling and Oppsumering Text in Meeting protocol
        public void SearchAndVerifyInstillingAndOppsummeringTextInPDF(string installingText, string oppsummeringText)
        {
            CommonMethods.PlayWait(5000);
            AutoItX.WinActivate("Adobe Acrobat Reader DC");

            CommonMethods.PlayWait(2000);
            AutoItX.Send("^f");
            CommonMethods.PlayWait(2000);
            AutoItX.Send(installingText);
            AutoItX.Send("{ENTER}");
            CommonMethods.PlayWait(3000);

            var textNotFoundErrorMessage = AutoItX.WinGetText("Acrobat Reader", "").Replace("\n", "").Replace("\r", "").Trim();

            //Verify word exist
            if (textNotFoundErrorMessage.Contains("Adobe Acrobat Reader has finished searching the document. No more matches were found."))
            {
                //Fail
                AutoItX.Send("{ENTER}");
                Assert.Fail("The Instilling (Bookmark Recommendation) Text is not available in the Meeting protocol document");
            }

            CommonMethods.PlayWait(2000);
            AutoItX.Send("^a");
            CommonMethods.PlayWait(2000);
            AutoItX.Send(oppsummeringText);
            AutoItX.Send("{ENTER}");
            CommonMethods.PlayWait(2000);

            if (textNotFoundErrorMessage.Contains("Adobe Acrobat Reader has finished searching the document. No more matches were found."))
            {
                //Fail
                AutoItX.Send("{ENTER}");
                Assert.Fail("The Oppsummering (Bookmark Recommendation) Text is not available in the Meeting protocol document");
            }

            CommonMethods.PlayWait(2000);
            AutoItX.Send("{ESC}");
            AutoItX.Send("{ESC}");
            CommonMethods.PlayWait(2000);
        }
        #endregion

        #region Click Open in office online button
        /// <summary>
        /// Click Open in office online button
        /// </summary>
        public void OpenInOfficeOnline()
        {
            CommonMethods.PlayWait(3000);
            var openDesktopApplicationElement = _driver.FindElement(MeetingPageLocators.OpenInOfficeOnline);
            openDesktopApplicationElement.DrawHighlight();
            _driver.MouseHoverClickOnElement(openDesktopApplicationElement);
        }
        #endregion

        #region Verify if PDF opened in the office online
        /// <summary>
        /// Verify if PDF opened in the office online - Handle Bug 199932
        /// </summary>
        public void VerifyPDFOpenedInOfficeOnline()
        {
            var errorPDFElement = _driver.FindElements(MeetingPageLocators.PDFErrorMessage);
            
            if(errorPDFElement.Any() && errorPDFElement.FirstOrDefault().Displayed)
            {
                errorPDFElement.FirstOrDefault().DrawHighlight();
                Assert.Fail("Error message displayed in PDF-\n Viewing of .pdf files has been disabled in Word Online. Please get in touch with your helpdesk.\n" + " Related to Bug#199932");
            }

            var errorPDFMessage = _driver.FindElements(MeetingPageLocators.PDFErrorMessage2);

            if (errorPDFMessage.Any() && errorPDFMessage.FirstOrDefault().Displayed)
            {
                errorPDFMessage.FirstOrDefault().DrawHighlight();
                Assert.Fail("Failed to open pdf in office online Meeting Protocol - Related to Bug#199932\n" + "Actual Error Message:\n" + errorPDFMessage.FirstOrDefault().Text);
            }



            

        }
        #endregion

        #region Click Finished button
        /// <summary>
        /// Click Finished button
        /// </summary>
        public void ClickFinishedButton()
        {
            try
            {
                CommonMethods.PlayWait(3000);
                var finisheButton = _driver.FindElement(MeetingPageLocators.FinishedButton);
                finisheButton.DrawHighlight();
                _driver.MouseHoverClickOnElement(finisheButton);
            }
            catch (Exception)
            {
                Assert.Fail("Finished button is not activated");
            }
            
        }
        #endregion

        #region Click After Button
        /// <summary>
        /// Click After Button
        /// </summary>
        public void ClickAfterButton()
        {
            try
            {
                CommonMethods.PlayWait(3000);
                var afterButton = _driver.FindElement(MeetingPageLocators.AfterButton);
                afterButton.DrawHighlight();
                afterButton.Click();
            }
            catch (Exception)
            {
                Assert.Fail("After button is not activated");
            }

        }
        #endregion

        #region  Open Protocol Agenda Item in record management module
        /// <summary>
        /// Open Protocol Agenda Item in record management module
        /// </summary>
        public void OpenProtocolAgendaItemInRecordManagementModule()
        {
            CommonMethods.PlayWait(3000);
            var openProtocolAgendaMenuButton = _driver.FindElement(MeetingPageLocators.OpenProtocolAgendaMenuButton);
            openProtocolAgendaMenuButton.DrawHighlight();
            openProtocolAgendaMenuButton.Click();

            CommonMethods.PlayWait(3000);
            var openProtocolAgendaButton = _driver.FindElement(MeetingPageLocators.OpenProtocolAgendaButton);
            openProtocolAgendaButton.DrawHighlight();
            openProtocolAgendaButton.Click();

            CommonMethods.PlayWait(3000);
            Assert.IsTrue(_driver.WindowHandles.Count == 2, "Open Protocol Agenda Item in record management module did not open in new window tab");
        }
        #endregion

        public void Verify(string option)
        {
            var verifyUnRead = _driver.FindElements(By.XPath("//a[@class='list-group-item registry-entry active unread']"));
            var verifyRead = _driver.FindElements(By.XPath("//a[@class='list-group-item registry-entry active read']"));

            if (verifyUnRead.Any() && option == "UnRead")
            {
                var isUnreadDisplyed = _driver.FindElement(By.XPath("//a[@class='list-group-item registry-entry active unread']")).Displayed;
                Assert.IsTrue(isUnreadDisplyed, "Registy entry is not active to Unread");
            }

            else if (verifyRead.Any() && option == "Read")
            {
                var isreadDisplyed = _driver.FindElement(By.XPath("//a[@class='list-group-item registry-entry active read']")).Displayed;
                Assert.IsTrue(isreadDisplyed, "Registy entry not active to read");
            }
        }
    }
}
