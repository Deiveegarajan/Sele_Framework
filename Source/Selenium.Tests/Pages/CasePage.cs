using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Pages
{
    public class CasePage
    {
        public static class CaseLocators
        {
            public static readonly By NewCaseButton          = By.XPath("//button[@title='New case']");
            public static readonly By CaseTitleID            = By.Id("titleCensor");
            public static readonly By CaseTitle              = By.XPath("//h3[@class='censored-editbox editable-click censored-editbox editable-click case-title']");
            public static readonly By RegistryTitle          = By.XPath("//h4[@class='details-header-text']");
            public static readonly By saveCase               = By.XPath("//span[contains(text(),'Save')]");
            public static readonly By ExpandMenuLocator      = By.XPath("");
            public static readonly By CollapseMenuLocator    = By.XPath("//button[@class='btn btn-md btn-link pull-left btn-menu-toggle']");
            public static readonly By CaseNumber             = By.XPath("//div[@class='header-text-number']");
            public static readonly By FolderType             = By.XPath("//*[normalize-space(.)='Folder type']//following::span[4]//following::input");
            public static readonly By EditTitle              = By.XPath("//span[text()='Case']");
            public static readonly By CancelCaseTitle        = By.XPath("//span[text()='Cancel']");
            public static readonly By ClassId                = By.XPath("//*[normalize-space(.)='Classification']//following::td[5]//following::span//li//input");
            public static readonly By CaseParties            = By.XPath("//span[contains(text(),'Case parties')]");
            public static readonly By CasePartyButton        = By.XPath("//span[contains(text(),'Case party')]");
            public static readonly By ShortName              = By.XPath("//*[normalize-space(.)='Short name']//following::span[@class='select2-selection__rendered']//li//input");
            public static readonly By CasePartySave          = By.XPath("//button[@class='btn btn-sm btn-success']");
            public static readonly By EditCaseParties        = By.XPath("//i[@class='glyphicon glyphicon-pencil']");
            public static readonly By DeleteCaseParties      = By.XPath("//button[@class='btn btn-xs btn-link']//i[@class='glyphicon glyphicon-remove']");
            public static readonly By ConfirmDelete          = By.XPath("//button[text()='Confirm delete']");
            public static readonly By EditTitleInnerText     = By.XPath("//h3[@id='titleCensor']");
            public static readonly By QuickSearchId          = By.XPath("//input[@class='input-sm input-xxs quick-search-custom-input form-control']");
            public static readonly By ListButton             = By.XPath("//i[@class='glyphicon glyphicon-menu-hamburger']");
            public static readonly By GridButton             = By.XPath("//i[@class='glyphicon glyphicon-th']");
            public static readonly By SearchButton           = By.XPath("//button[text()='Search'][1]");
            public static readonly By NoteText               = By.XPath("//textarea[@id='Text']");
            public static readonly By EditRemarks            = By.XPath("//i[@class='glyphicon glyphicon-pencil']");
            public static readonly By NotesArrowButton       = By.XPath("//i[@class='caret']");
            public static readonly By RemakSave              = By.XPath("//button[text()='Save']");
            public static readonly By RemakCancel            = By.XPath("//button[text()='Cancel']");
            public static readonly By RemakTitleNormalMode   = By.XPath("//p[@class = 'list-group-item-text']");
            public static readonly By MoreOptions            = By.XPath("//span[@class='glyphicon glyphicon-option-horizontal']");
            public static readonly By EditCaseButton         = By.XPath("//button[@id='toggleState']");
            public static readonly By PreservationTime       = By.XPath("//input[@id='preservationTime']");
            public static readonly By MoreDetailsInCase      = By.XPath("//span[text()='More details']");
        }

        private readonly RemoteWebDriver _driver;

        public CasePage(RemoteWebDriver driver)
        {
            _driver = driver;
        }
        public static CasePage Connect(RemoteWebDriver driver)
        {
            return new CasePage(driver);
        }

        #region Click New Case button, Enter title and click Save  
        /// <summary>
        /// Create new Case
        /// </summary>
        /// <returns>Parameter of type System.Boolean for True or False</returns>
        public void AddTitle(string caseTitle)
        {
            //wait until the wait for dash board and New Case Buttion is Visibled//Click on New case button
            CommonMethods.PlayWait(10000);
            _driver.ClickOnElement(CaseLocators.NewCaseButton);

            // Enter Title 
            _driver.WaitForElementVisible(CaseLocators.CaseTitleID);
            _driver.EnterText(CaseLocators.CaseTitleID, caseTitle);

            //Select the folder type
            //_driver.SelectKendoDropdownAndAddValue("Folder type", "Adm. enhetsmappe");

            //Handle Error PopUp if any errors when saving case
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region  Enter More  Detail in the Create case if required
        /// <summary>
        /// Enter More  Detail in the Create case if required
        /// </summary>
        public void ClickMoreDetailsButtonInCreateCase()
        {
            CommonMethods.PlayWait(2000);
            if (_driver.FindElements(CaseLocators.MoreDetailsInCase).Any())
            {
                _driver.ClickOnElement(CaseLocators.MoreDetailsInCase);
            }
        }
        #endregion

        #region Create case
        /// <summary>
        /// Create case with title
        /// </summary>
        /// <param name="title"></param>
        public void CreateCase(string title)
        {
            AddTitle(title);
            SaveCase();
        }
        #endregion

        #region Case with Classification
        /// <summary>
        /// Create case with Classification
        /// </summary>
        /// <param name="title"></param>
        /// <param name="classId"></param>
        public void CreateCaseWithClassificationCode(string title, string classId)
        {
            AddTitle(title);
            AddClassification(classId);
            SaveCase();
        }
        #endregion

        #region Save case
        /// <summary>
        /// Save the case
        /// </summary>
        public void SaveCase()
        {
            //Click Save case
            _driver.WaitForElementVisible(CaseLocators.saveCase);
            _driver.ClickOnElement(CaseLocators.saveCase);
            CommonMethods.PlayWait(2000);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
            CommonMethods.PlayWait(5000);

            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Edit case
        /// <summary>
        /// Edit the case
        /// </summary>
        public void EditCaseTitle(string caseTitle)
        {
            CommonMethods.PlayWait(5000);
            _driver.ClickOnElement(CaseLocators.EditTitle);
            IWebElement element = _driver.FindElement(CaseLocators.EditTitleInnerText);
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(caseTitle);
            CommonMethods.PlayWait(2000);
            _driver.ClickOnElement(CaseLocators.saveCase);
            CommonMethods.PlayWait(2000);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Click on case title
        /// <summary>
        /// Click on case title to edit
        /// </summary>
        /// <returns></returns>
        public void EditCaseTitle()
        {
            CommonMethods.PlayWait(5000);
            _driver.ClickOnElement(CaseLocators.EditTitle);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
            //IWebElement element = _driver.FindElement(CaseLocators.EditTitleInnerText);
            //return element;
        }
        #endregion

        #region Edit case title as screened text ot person name
        /// <summary>
        /// Edit case title as screened text ot person name
        /// </summary>
        /// <param name="selectedText"></param>
        /// <param name="Option"></param>
        public void EditCaseTitleScreenedTextOrPersonName(string selectedText, string Option = "", string endTitle = "")
        {
            CommonMethods.PlayWait(2000);
            var element = _driver.FindElement(CaseLocators.EditTitleInnerText).Text;
            string[] splitText = element.Split(' ');

            foreach (var markedTextScreened in splitText)
            {
                if (markedTextScreened == selectedText)
                {
                    var title = markedTextScreened;
                    IWebElement selectedTitle = _driver.FindElement(By.XPath("//h3[@id='titleCensor']//span[text()='" + title + "']"));
                    _driver.MouseDoubleClickOnElement(selectedTitle);

                    if (endTitle != "")
                    {
                        IWebElement destinationTitle = _driver.FindElement(By.XPath(string.Format("//h3[@id='titleCensor']//span[text()='{0}']", endTitle)));
                        Actions action = new Actions(_driver);
                        action.DragAndDrop(selectedTitle, destinationTitle).Perform();
                    }
                    CommonMethods.PlayWait(2000);
                    _driver.MouseRightClickOnElement(selectedTitle);
                    CommonMethods.PlayWait(3000);

                    if (Option == TitleModify.Screened.GetStringValue())
                    {
                        _driver.FindElement(By.XPath("//span[text()='screened']")).Click();
                    }
                    else if (Option == TitleModify.PersonName.GetStringValue())
                    {
                        _driver.FindElement(By.XPath("//span[text()='person name']")).Click();
                    }
                    else if (Option == TitleModify.RemoveScreeningFromText.GetStringValue())
                    {
                        _driver.FindElement(By.XPath("//span[text()='screening']")).Click();
                    }
                    else if (Option == TitleModify.RemoveMarkingPersonName.GetStringValue())
                    {
                        _driver.FindElement(By.XPath("//span[text()='Remove marking of ']")).Click();
                    }
                }
            }
        }
        #endregion

        #region Verify title text color, format and hidden letters
        /// <summary>
        /// Verify the Edited Title text color, format And hidden letters
        /// </summary>
        /// <param name="selectedText"></param>
        /// <param name="wordPosition"></param>
        /// <param name="wordStarPosition"></param>
        /// <param name="endTitle">If we use multiple words together pass the last word as the endtitle</param>
        public void VerifyCaseTitleMarkedAsStarsAndItalic(string selectedText, int wordPosition, string color = "", int wordStarPosition = 0, string endTitle = "")
        {
            CommonMethods.PlayWait(3000);
            var element = _driver.FindElement(By.XPath("//h3[@class='censored-editbox editable-click case-title']")).Text;
            IWebElement selectedTitle = _driver.FindElement(By.XPath("//h3[@class='censored-editbox editable-click case-title']//span[contains(text(),'" + selectedText + "')]"));

            string[] splitText = element.Split(' ');

            if (selectedText == "*****")
            {
                var starWords = _driver.FindElements(By.XPath("//h3[@class='censored-editbox editable-click case-title']//span[contains(text(),'" + selectedText + "')]"));
                var list = splitText.Select(c => c.Contains("*****"));

                //var li = list.ElementAt(wordPosition).ToString();

                if (list.ElementAt(wordPosition) == true)
                {
                    SeleniumExtensions.DrawHighlight(starWords[wordStarPosition]);
                    // Star is present in the expected position in the case title
                    //Verify the text color 
                    var highlighTextColor = starWords[wordStarPosition].GetAttribute("class");
                    if (highlighTextColor == "censored-text" && color == TitleBackgroundFormat.Red.GetStringValue())
                    {
                        Assert.IsTrue("censored-text" == highlighTextColor, "Red color ({0}) is not exits at this position {1} in the case title", selectedText, wordPosition);
                    }
                    else if (highlighTextColor == "marked-name censored-text" && color == TitleBackgroundFormat.RedWithItalic.GetStringValue())
                    {
                        Assert.IsTrue("marked-name censored-text" == highlighTextColor, "Red color ({0}) with ITALIC is not exits at this position {1} in the case title", selectedText, wordPosition);
                    }
                    else if (highlighTextColor == "marked-name censored-text" && color == TitleBackgroundFormat.Red.GetStringValue())
                    {
                        Assert.IsTrue("marked-name censored-text" == highlighTextColor, "Red color ({0}) with ITALIC is not exits at this position {1} in the case title", selectedText, wordPosition);
                    }
                    else
                    {
                        Assert.Fail("Star ({0}) is not matches the text at this position {1} in the case title", selectedText, wordPosition);
                    }
                }
                else
                {
                    Assert.Fail("Star ({0}) is not exits at this position {1} in the case title", selectedText, wordPosition);
                }
            }
            else
            {
                var titleIndexValue = splitText[wordPosition];
                Assert.IsTrue(selectedText == titleIndexValue, "Word ({0}) is exits at this position {1} in the case title", selectedText, wordPosition);

                //Double word with/without Italic
                if (endTitle != "")
                {
                    var selectedTitles = _driver.FindElement(By.XPath("//h3[@class='censored-editbox editable-click case-title']//span[contains(text(),'" + selectedText + "')]"));
                    SeleniumExtensions.DrawHighlight(selectedTitles);
                    string wordToVerify = selectedText + " " + endTitle;
                    var highlightTextColor = selectedTitle.GetAttribute("class");
                    if (highlightTextColor == "marked-name" && color == TitleBackgroundFormat.Italic.GetStringValue())
                    {
                        Assert.IsTrue("marked-name" == highlightTextColor, "Blue color {0} with Italic is not exits at this position {1} in the case title", selectedText, wordPosition);
                    }
                    else if (highlightTextColor == "censored-text" && color == TitleBackgroundFormat.Red.GetStringValue())
                    {
                        Assert.IsTrue("marked-name" == highlightTextColor, "Blue color {0} with Italic is not exits at this position {1} in the case title", selectedText, wordPosition);
                    }
                    else if (highlightTextColor == null)
                    {
                        Assert.IsNull(highlightTextColor == null, "Blue color ({0}) word is not exits at this position {1} in the case title", selectedText, wordPosition);
                    }
                    else
                    {
                        Assert.Fail("Star ({0}) is not matches the text at this position {1} in the case title", selectedText, wordPosition);
                    }
                }
                else
                {
                    // Singe word with/without Italic 
                    SeleniumExtensions.DrawHighlight(selectedTitle);
                    var highlighTextColor = selectedTitle.GetAttribute("class");
                    if (highlighTextColor == "marked-name" && color == TitleBackgroundFormat.Red.GetStringValue())
                    {
                        Assert.IsTrue("marked-name" == highlighTextColor, "Blue color {0} with Italic is not exits at this position {1} in the case title", selectedText, wordPosition);
                    }
                    else if (string.IsNullOrEmpty(highlighTextColor) && color == TitleBackgroundFormat.Blue.GetStringValue())
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(highlighTextColor), "Blue color ({0}) word is not exits at this position {1} in the case title", selectedText, wordPosition);
                    }
                    else if (highlighTextColor == "marked-name" && color == TitleBackgroundFormat.Italic.GetStringValue())
                    {
                        Assert.IsTrue("marked-name" == highlighTextColor, "Blue color {0} with Italic is not exits at this position {1} in the case title", selectedText, wordPosition);
                    }
                    else
                    {
                        Assert.Fail("Star ({0}) is not matches the text at this position {1} in the case title", selectedText, wordPosition);
                    }
                }
            }
        }
        #endregion

        #region Get the screened case title
        /// <summary>
        /// Get the screened case title
        /// </summary>
        /// <returns></returns>
        public string GetScreenedCaseTitle()
        {
            CommonMethods.PlayWait(3000);
            var element = _driver.FindElement(CaseLocators.CaseTitle).Text;
            return element;
        }
        #endregion 

        #region Get the remark tile
        /// <summary>
        /// Get the remark title
        /// </summary>
        /// <returns></returns>
        public string GetRemarkTitle()
        {
            CommonMethods.PlayWait(3000);
            SeleniumExtensions.DrawHighlight(_driver.FindElement(CaseLocators.RemakTitleNormalMode));
            var element = _driver.FindElement(CaseLocators.RemakTitleNormalMode).Text;
            return element;
        }
        #endregion 

        #region Verify the case title color and style
        /// <summary>
        /// Verify the case title color and style
        /// </summary>
        /// <param name="selectedText"></param>
        /// <param name="color"></param>
        /// <param name="mode">If we use multiple words together pass the last word as the endtitle</param>
        public void VerifyCaseTitleColorAndFormat(string selectedText, string color = "", string mode = "")
        {
            CommonMethods.PlayWait(3000);

            var element = (mode == "") ? _driver.FindElement(CaseLocators.CaseTitle).Text : _driver.FindElement(CaseLocators.EditTitleInnerText).Text;

            string[] splitText = element.Split(' ');

            foreach (var markedTextScreened in splitText)
            {
                if (markedTextScreened == selectedText)
                {
                    var title = markedTextScreened;
                    var selectedTitle = (mode == "") ? _driver.FindElement(By.XPath("//h3[@class='censored-editbox editable-click censored-editbox editable-click case-title']//span[contains(text(),'" + selectedText + "')]")) :
                        _driver.FindElement(By.XPath("//h3[@id='titleCensor']//span[contains(text(),'" + selectedText + "')]"));

                    CommonMethods.PlayWait(1000);
                    SeleniumExtensions.DrawHighlight(selectedTitle);
                    var highlighTextColor = selectedTitle.GetAttribute("class");

                    // Singe word with/without Red color 
                    if (highlighTextColor == "censored-text" && color == TitleBackgroundFormat.Red.GetStringValue())
                    {
                        Assert.IsTrue("censored-text" == highlighTextColor, "Red color ({0}) is not exits at this position in the case title", selectedText);
                    }
                    else if (highlighTextColor == "marked-name censored-text" && color == TitleBackgroundFormat.RedWithItalic.GetStringValue())
                    {
                        Assert.IsTrue("marked-name censored-text" == highlighTextColor, "Red color ({0}) with ITALIC is not exits at this position in the case title", selectedText);
                    }
                    // Singe word with/without Italic 
                    else if (highlighTextColor == "marked-name" && color == TitleBackgroundFormat.Italic.GetStringValue())
                    {
                        Assert.IsTrue("marked-name" == highlighTextColor, "Blue color {0} with Italic is not exits at this position in the case title", selectedText);
                    }
                    else if (string.IsNullOrEmpty(highlighTextColor) && color == TitleBackgroundFormat.Blue.GetStringValue())
                    {
                        Assert.IsTrue(string.IsNullOrEmpty(highlighTextColor), "Blue color ({0}) word is not exits at this position in the case title", selectedText);
                    }
                    else
                    {
                        Assert.Fail("The seleceted word '{0}' is mismatching the text color/format :{1}", selectedText, color);
                    }
                }
            }
        }
        #endregion

        #region Get case Number
        /// <summary>
        /// Get the case number
        /// </summary>
        /// <returns></returns>
        public string GetCaseNumber()
        {
            string latestCase = _driver.FindElement(CaseLocators.CaseNumber).Text;
            // Assert.IsNotNull(latestCase,"Unable to Identify the case number");
            return latestCase;
        }
        #endregion

        #region Select Module
        /// <summary>
        /// Select Module
        /// </summary>
        /// <param name="moduleName"></param>
        public void SelectModule(string moduleName = "")
        {
            _driver.WaitForElementVisible(By.XPath("//span[text()='Logout']"));
            selectModule(_driver, string.IsNullOrEmpty(moduleName) ? ApplicationModules.RecordManagement.GetStringValue() : moduleName);
        }
        #endregion

        #region'select module' 
        /// <summary>
        /// select module
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="moduleName"></param>
        private void selectModule(RemoteWebDriver driver, string moduleName)
        {
            driver.FindElement(By.Id(moduleName)).SendKeys(Keys.Enter);
        }
        #endregion

        #region Add class Id
        /// <summary>
        /// Add classification id for the case
        /// </summary>
        /// <param name="classId">Add the classification code</param>
        public void AddClassification(string classId)
        {
            _driver.WaitForElementVisible(CaseLocators.ClassId);
            _driver.SelectKendoDropDownAndValue(CaseLocators.ClassId, classId);
        }
        #endregion

        #region Add Parties
        /// <summary>
        /// Add New parties
        /// </summary>
        /// <param name="shortName"></param>
        public void AddParties(string shortName, string shortNameValue, bool restricted = true)
        {
            CommonMethods.PlayWait(2000);
            _driver.ClickOnButton("Case parties");

            CommonMethods.PlayWait(2000);
            _driver.ClickOnButton("Case party");

            CommonMethods.PlayWait(2000);
            _driver.SelectKendoDropdownAndAddValue(shortName, shortNameValue);

            CommonMethods.PlayWait(1000);
            IWebElement restrictedButton = _driver.FindElement(By.Id("IsRestricted"));
            bool isRestricted = restrictedButton.Selected;
            SeleniumExtensions.DrawHighlight(restrictedButton);

            if (restricted == true && isRestricted == false)
            {
                _driver.FindElement(By.Id("IsRestricted")).Click();
            }
            else if(restricted == false && isRestricted == true)
            {
                _driver.FindElement(By.Id("IsRestricted")).Click();
            }

            CommonMethods.PlayWait(2000);
            _driver.ClickOnElement(CaseLocators.CasePartySave);
            CommonMethods.PlayWait(2000);
        }
        #endregion

        #region Add Party
        /// <summary>
        /// Add New party
        /// </summary>
        /// <param name="shortName"></param>
        /// <param name="shortNameValue"></param>
        public void AddParties(string shortName, string shortNameValue)
        {
            CommonMethods.PlayWait(2000);
            _driver.ClickOnButton("Case parties");

            CommonMethods.PlayWait(2000);
            _driver.ClickOnButton("Case party");

            CommonMethods.PlayWait(2000);
            //AddValueToShortNameInCaseParties(shortName, shortNameValue);
            _driver.SelectKendoDropdownAndAddValue(shortName, shortNameValue, true);

            CommonMethods.PlayWait(2000);
            _driver.ClickOnElement(CaseLocators.CasePartySave);
            CommonMethods.PlayWait(2000);
        }
        #endregion

        #region Add value to Shortname in Case parties
        /// <summary>
        /// Add New party
        /// </summary>
        /// <param name="shortName"></param>
        /// <param name="shortNameValue"></param>
        public void AddValueToShortNameInCaseParties(string shortName, string shortNameValue)
        {
            try
            {
                IList<IWebElement> element = null;

                element = _driver.FindElements(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//input", shortName)));

                if (element.Count == 0)
                {
                    element = _driver.FindElements(By.XPath(string.Format("//*[normalize-space(.)='{0}']//input", shortName)));
                }
                CommonMethods.PlayWait(000);
                for (int i = 0; i < element.Count; i++)
                {
                    if (element[i].Displayed)
                    {
                        element[i].Click();
                        element[i].SendKeys(Keys.Control + "a");
                        element[i].SendKeys(shortNameValue);
                        CommonMethods.PlayWait(4000);
                        element[i].SendKeys(Keys.ArrowDown);
                        element[i].SendKeys(Keys.Enter);
                    }
                }
            }

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region Edit Case Parties
        /// <summary>
        /// Edit the case parties
        /// </summary>
        /// <param name="EditShortName"></param>
        public void EditCaseParties(string EditShortName)
        {
            CommonMethods.PlayWait(2000);
            _driver.WaitForElementVisible(CaseLocators.EditCaseParties);
            _driver.ClickOnElement(CaseLocators.EditCaseParties);

            CommonMethods.PlayWait(2000);
            _driver.WaitForElementVisible(CaseLocators.ShortName);
            _driver.SelectKendoDropDownAndValue(CaseLocators.ShortName, EditShortName);

            CommonMethods.PlayWait(2000);
            _driver.WaitForElementVisible(CaseLocators.CasePartySave);
            _driver.ClickOnElement(CaseLocators.CasePartySave);
        }
        #endregion

        #region Delete case parties
        /// <summary>
        /// Delete the case parties
        /// </summary>
        public void DeleteCaseParties()
        {
            CommonMethods.PlayWait(2000);
            _driver.WaitForElementVisible(CaseLocators.DeleteCaseParties);
            _driver.ClickOnElement(CaseLocators.DeleteCaseParties);

            CommonMethods.PlayWait(1000);
            _driver.WaitForElementVisible(CaseLocators.ConfirmDelete);
            _driver.ClickOnElement(CaseLocators.ConfirmDelete);
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
            catch
            {
                try
                {
                    _driver.SelectKendoDropdownAndAddValues(dropdownLabelName, dropdownValue);
                }
                catch (Exception ex)
                {
                    var errorMessage = string.Format("Unable to select the value in the drop down:{0} - Value:{1}\n {2}", dropdownLabelName, dropdownValue, ex);
                    CommonMethods.ThrowExceptionAndBreakTC(errorMessage);
                }
            }
        }
        #endregion

        #region Send Value to the text box
        /// <summary>
        /// Select value to the text box 
        /// </summary>
        /// <param name="textBoxName"></param>
        /// <param name="textBoxValue"></param>
        public void SendTextToTextBoxField(string textBoxName, string textBoxValue)
        {
            CommonMethods.PlayWait(3000);
            _driver.SendTextToTextBoxField(textBoxName, textBoxValue);
        }
        #endregion

        #region search case id
        /// <summary>
        /// Search Id
        /// </summary>
        /// <param name="searchId"></param>
        public void QuickSearchCaseId(string searchId)
        {
            _driver.EnterText(CaseLocators.QuickSearchId, searchId);
            _driver.ClickOnElement(CaseLocators.QuickSearchId);
            CommonMethods.PlayWait(3000);
        }
        #endregion

        #region Validate Case Or Registry Title Editable
        /// <summary>
        /// Validate the case title is editable
        /// </summary>
        public void ValidateCaseOrRegistryTitleEditable(string titleName = "case")
        { 
            IWebElement element = null;
            CommonMethods.PlayWait(4000);
            if (titleName == "case")
            {
               element = _driver.FindElement(CaseLocators.CaseTitle);
            }
            else if(titleName == "registryEntry")
            {
                element = _driver.FindElement(CaseLocators.RegistryTitle);
            }
            var titleEditable = element.GetAttribute("contenteditable");
            SeleniumExtensions.DrawHighlight(element);
            Assert.IsNull(titleEditable, "{0} title is Editable",titleName);
        }
        #endregion

        #region Click on the Cancel Edit Case Title Button
        /// <summary>
        /// Click on the Cancel Edit Case Title Button
        /// </summary>
        public void ClickCancelEditCaseTitleButton()
        {
            CommonMethods.PlayWait(2000);
            _driver.ClickOnElement(CaseLocators.CancelCaseTitle);
        }
        #endregion

        #region Click On Left Dashboard Menu Item
        /// <summary>
        /// Click On Left Dashboard Menu Item
        /// </summary>
        /// <param name="menuItem"></param>
        public void ClickOnLeftDashboardMenuItem(string menuItem)
        {
            CommonMethods.PlayWait(2000);
            DashboardPage dashboardPage = new DashboardPage(_driver);
            // Expand Left menu
            if (dashboardPage.IsMenuControlCollapsed())
                dashboardPage.ExpandMenu();
            _driver.ClickOnElement(By.XPath(string.Format("//span[text()='{0}']",menuItem)));
        }
        #endregion

        #region
        /// <summary>
        /// Click on search button in the search criteria
        /// </summary>
        public void ClickOnSearchCriteriaButton()
        {
            CommonMethods.PlayWait(3000);
            _driver.SelectKendoDropdownAndAddValue("Case type", "");
            _driver.ClickOnElement(CaseLocators.SearchButton);
        }
        #endregion

        #region Click on Search Button
        /// <summary>
        /// Click on Search Button directly
        /// </summary>
        public void ClickOnSearchButton()
        {
            CommonMethods.PlayWait(3000);
            _driver.ClickOnElement(CaseLocators.SearchButton);
        }
        #endregion 

        #region Click On List Or Grid View In Case List
        /// <summary>
        /// Click On List Or Grid View In Case List button
        /// </summary>
        /// <param name="viewType"></param>
        public void ClickOnListOrGridViewInCaseList(string viewType = "List")
        {
            CommonMethods.PlayWait(4000);
            if (viewType == "List")
            {
                _driver.ClickOnElement(CaseLocators.ListButton);
            }
            else
            {
                _driver.ClickOnElement(CaseLocators.GridButton);
            }
        }
        #endregion

        #region Verify Case Title In Saksmapper
        /// <summary>
        /// Verify Case Title In Saksmapper is matching with the actual title
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="screenedFullCaseTitle"></param>
        public void VerifyCaseTitleInSaksmapper(string caseId, string screenedFullCaseTitle) 
        {
            CommonMethods.PlayWait(5000);
            var caseIdWithTitle = _driver.FindElement(By.XPath(string.Format("//span[@title='{0} - {1}'][2]", caseId, screenedFullCaseTitle))).Text;
            var elementCaseTitle = _driver.FindElement(By.XPath(string.Format("//span[@title='{0} - {1}'][2]", caseId, screenedFullCaseTitle)));
            SeleniumExtensions.DrawHighlight(elementCaseTitle);
            Assert.IsTrue(caseIdWithTitle == screenedFullCaseTitle, "Screened case title is mismatching : " + screenedFullCaseTitle);
        }
        #endregion

        #region Verify Registry Title And Document Title In Saksmapper search view
        /// <summary>
        /// Verify Registry Title And Document Title In Saksmapper search view
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="registryTitle"></param>
        /// <param name="DocumentTitle"></param>
        /// <param name="IsDocumentTitle"></param>
        public void VerifyRegistryTitleAndDocumentTitleInSaksmapper(string caseId, string registryTitle, string DocumentTitle ="", bool IsDocumentTitle = false)
        {
            CommonMethods.PlayWait(5000);
            var clickChevronDownButton = By.XPath(string.Format("//span[text()='{0}']//ancestor::li[@class='no-focus list-group-item-container']//following-sibling::i[@class='glyphicon glyphicon-chevron-down']", caseId));
            _driver.ClickOnElement(clickChevronDownButton);

            CommonMethods.PlayWait(2000);
            var elementRegistryTitle = _driver.FindElement(By.XPath(string.Format("//span[text()='{0}']//following::a[contains(text(),'{1}')]", caseId, registryTitle)));
            var registryEntryTitle = elementRegistryTitle.Text;
            elementRegistryTitle.DrawHighlight();
            Assert.IsTrue(registryEntryTitle.Contains(registryTitle), "Registry Entry title is mismatching");

            if (IsDocumentTitle == true)
            {
                CommonMethods.PlayWait(3000);
                var clickDocumentChevronDownButton = By.XPath(string.Format("//span[text()='{0}']//following::div[@title='View documents']//i", caseId));
                _driver.ClickOnElement(clickDocumentChevronDownButton);

                CommonMethods.PlayWait(3000);
                var elementDocumentTitle = _driver.FindElement(By.XPath(string.Format("//span[text()='{0}']//following::a[text()='{1}']", caseId, DocumentTitle)));
                var DocumentText = elementDocumentTitle.Text;
                elementDocumentTitle.DrawHighlight();
                Assert.IsTrue(DocumentText == DocumentTitle, "Document title is mismatching");
            }
        }
        #endregion

        #region Verify Case Title In Recent Cases Grid View
        /// <summary>
        /// Verify Case Title In Recent Cases Grid View
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="screenedFullCaseTitle"></param>
        public void VerifyCaseTitleInRecentCasesGridView(string caseId, string screenedFullCaseTitle)
        {
            CommonMethods.PlayWait(5000);
            IWebElement caseIdWithTitle = _driver.FindElement(By.XPath(string.Format("//a[contains(text(),'{0}')]//following::td[1]", caseId)));
            SeleniumExtensions.DrawHighlight(caseIdWithTitle);

            var caseIdWithTitles = _driver.FindElement(By.XPath(string.Format("//a[contains(text(),'{0}')]//following::td[1]", caseId))).Text;

            Assert.IsTrue(caseIdWithTitles == screenedFullCaseTitle, "Screened case title is mismatching in 'Recent Cases' grid view : " + screenedFullCaseTitle);
        }
        #endregion

        #region
        /// <summary>
        /// Click on search button in the search criteria
        /// </summary>
        public void ClickOnSearchButtonInFilter()
        {
            CommonMethods.PlayWait(3000);
            _driver.ClickOnElement(CaseLocators.SearchButton);
        }
        #endregion

        #region Add Remarks
        /// <summary>
        /// Add Remarks/notes into the case
        /// </summary>
        /// <param name="notes"></param>
        public void AddRemarksOrNotes(string notes)
        {
            CommonMethods.PlayWait(3000);

            //Click on remarks Tab
            _driver.ClickOnButton("Remarks");
            CommonMethods.PlayWait(2000);

            //Click on Ad remark buttton
            _driver.ClickOnButton("Remark");
            _driver.HandleErrorPopUpAndThrowErrorMessage();

            //Add text in the remark and click save
            CommonMethods.PlayWait(2000);
            IWebElement remarkNotes = _driver.FindElement(CaseLocators.NoteText);
            remarkNotes.SendKeys(notes);
            CommonMethods.PlayWait(2000);
            _driver.ClickOnElement(CaseLocators.RemakSave);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
            CommonMethods.PlayWait(3000);
        }
        #endregion

        #region ClickOnButton
        /// <summary>
        /// Click on button using span 
        /// </summary>
        /// <param name="text"></param>
        public void ClickOnButton(string text)
        {
            _driver.ClickOnButton(text);
        }
        #endregion

        #region Select Kendo dropdown with partial text
        /// <summary>
        /// Select Kendo dropdown with partial text
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="dropdownName">label name</param>
        /// <param name="dropdownStartsWith">Name starts with</param>
        /// <param name="dropdownValue">complete name</param>
        /// <param name="dropdownTitle">send the complete title from tag (inspect element)</param>
        /// <returns></returns>
        public void SelectKendoDropdownUsingPartialText(string dropdownName, string dropdownStartsWith, string dropdownValue, string dropdownTitle)
        {
            _driver.SelectKendoDropdownUsingPartialText(dropdownName, dropdownStartsWith, dropdownValue, dropdownTitle);
        }
        #endregion

        #region Verify Access Code In Remarks
        /// <summary>
        /// Verify Access Code In Remarks
        /// </summary>
        /// <param name="dropdownName"></param>
        /// <param name="dropdownValue"></param>
        public void VerifyAccessCodeInRemarks(string dropdownName, string dropdownValue)
        {
            CommonMethods.PlayWait(2000);
            _driver.ClickOnElement(CaseLocators.EditRemarks);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
            CommonMethods.PlayWait(1000);
            _driver.ClickOnElement(CaseLocators.NotesArrowButton);
            _driver.VerifyDropDownValue(dropdownName, dropdownValue);
            CommonMethods.PlayWait(3000);
            if(_driver.FindElement(CaseLocators.RemakSave).Enabled)
            {
                _driver.ClickOnElement(CaseLocators.RemakSave);
                _driver.HandleErrorPopUpAndThrowErrorMessage();
            }
            else if(_driver.FindElement(CaseLocators.RemakCancel).Enabled)
            {
                _driver.ClickOnElement(CaseLocators.RemakCancel);
                _driver.HandleErrorPopUpAndThrowErrorMessage();
            }
        }
        #endregion

        #region  Verify case party is restricted
        /// <summary>
        /// Verify case party is restricted
        /// </summary>
        /// <param name="caseParties"></param>
        public void VerifyCasePartryIsRestricted(string partyFormat, bool casePartiesIsRestricted)
        {
            CommonMethods.PlayWait(2000);
            _driver.ClickOnButton("Case parties");

            CommonMethods.PlayWait(2000);
            IWebElement restictedParty = _driver.FindElement(By.XPath(string.Format("//div[@class='active-child']//span[text()='{0}']",partyFormat)));
            var starWord = restictedParty.Text;

            SeleniumExtensions.DrawHighlight(restictedParty);
            CommonMethods.PlayWait(2000);
            if (casePartiesIsRestricted == true)
            {
                Assert.IsTrue(partyFormat == starWord, "The case party is not restricted");
            }
            else
            {
                Assert.IsTrue(partyFormat == starWord, "The case party is restricted");
            }
            
        }
        #endregion

        #region button click
        /// <summary>
        /// Button Click
        /// </summary>
        /// <param name="buttonName"></param>
        public void Click(string buttonName)
        {
            _driver.ClickOnButton(buttonName);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Verify More Options In Case Level
        /// <summary>
        /// Verify More Options In Case Level
        /// </summary>
        /// <param name="menuOption"></param>
        public void VerifyMoreOptionsInCaseLevel(string menuOption, string errorMessage)
        {
            _driver.ClickOnElement(CaseLocators.MoreOptions);
            CommonMethods.PlayWait(3000);
            _driver.ClickOnElement(By.XPath(string.Format("//ul[@class='dropdown-menu case-menu no-pull']//a[text()='{0}']", menuOption)));
            CommonMethods.PlayWait(3000);
            _driver.ValidateRedPopUp(errorMessage);
        }
        #endregion

        #region Click on Edit case button
        /// <summary>
        ///  Click on Edit case button
        /// </summary>
        public void EditCase()
        {
            CommonMethods.PlayWait(3000);
            _driver.ClickOnElement(CaseLocators.EditCaseButton);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Get Preservation Time Value
        /// <summary>
        /// Get Preservation Time
        /// </summary>
        /// <returns></returns>
        public string GetPreservationTime()
        {
            CommonMethods.PlayWait(2000);
            var preservationElement = _driver.FindElement(CaseLocators.PreservationTime);
            preservationElement.DrawHighlight();
            return preservationElement.GetAttribute("value");
        }
        #endregion

        #region Get Disposal Code Value
        /// <summary>
        /// Get innner text from the drop down
        /// </summary>
        /// <returns></returns>
        public string GetDisposalCode()
        {
            CommonMethods.PlayWait(2000);
            return GetDropDownValue("Disposal");
        }
        #endregion

        #region Get inner text from the drop down
        /// <summary>
        /// Get innner text from the drop down
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="dropdownName"></param>
        /// <returns></returns>
        public string GetDropDownValue(string dropdownName)
        {
            return _driver.GetDropDownValue(dropdownName);
        }
        #endregion

        #region Verify User is LogOn With Correct Role
        /// <summary>
        /// Verify User is LogOn With Correct Role
        /// </summary>
        /// <param name="role"></param>
        public void VerifyUserIsLogOnWithCorrectRole(string role)
        {
            CommonMethods.PlayWait(6000);
            IWebElement userRole = _driver.FindElement(By.XPath(string.Format("//div[@class='logo-role-info']//span[contains(text(),'{0}')]", role)));
            var logOnUserRole = userRole.Text;
            userRole.DrawHighlight();
            bool b = logOnUserRole.Contains(role);
            Assert.IsTrue(logOnUserRole.Contains(role), string.Format("The User({0}) role is invalid", role));
        }
        #endregion
    }
}
 