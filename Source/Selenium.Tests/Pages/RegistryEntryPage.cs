using AutoIt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using System;
using System.Linq;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Pages
{
    public class RegistryEntryPage
    {
        public static class RegistryEntryLocators
        {
            public static readonly By RegistryButton = By.XPath("//div[@class='btn-group pull-left']//button[@type='button']");
            public static readonly By RegistryEntryTitle = By.XPath("//h4[@id='titleCensor']");
            public static readonly By RegistryEntryType = By.XPath("//span[@class='text-capitalize'][contains(text(),'Utgående post/Outbound')]");
            public static readonly By Receiver = By.XPath("//div[@class='recipients-field']//li[@class='select2-search select2-search--inline']//input");
            public static readonly By FromAddress = By.XPath("//div[@class='senders-field']//li[@class='select2-search select2-search--inline']//input");
            public static readonly By SelectHighlighted = By.XPath("//span[@class='select2-container select2-container--default select2-container--open']//li[1]");
            public static readonly By AttachDocument = By.XPath("//button[contains(text(),'Document template')]");
            public static readonly By Username = By.XPath("//input[@id='username']");
            public static readonly By Password = By.XPath("//input[@id='password']");
            public static readonly By LoginButton = By.XPath("//button[@class='btn btn-primary']");
            public static readonly By Save = By.XPath("//button[@class='btn btn-custom small-screen-edit btn-success']//span[contains(text(),'Save')]");
            public static readonly By EditRegistryEntryTitle = By.XPath("//*[normalize-space(.)='Selected registryentry']//following::span[text()='Edit']");
            public static readonly By RegistryEntryHeader = By.XPath("//h4[@id='titleCensor']");
            public static readonly By EditRegEntry = By.XPath("//span[text()='Edit']//parent::a[@class='btn btn-link btn-sm btn-custom']");
            public static readonly By EmailRecipient = By.Id("field-Email");
            public static readonly By RecientEditSaveOK = By.XPath("(//span[text()='OK'])[1]");
            public static readonly By RegistryEntryTitleNormalMode = By.XPath("//h4[@class='details-header-text']");
            public static readonly By RegistryEntryUlList = By.XPath("//div[@class='listcontainer-col']//ul[@class='listview list-group scroll-list tab-default']");
            public static readonly By RegistryEntryLiList = By.XPath("//span[@class='registryentry-title']");
            public static readonly By RegistryMoreArrow = By.XPath("//button[@class='btn btn-xs toggle-button']");
            public static readonly By RegistryCancelButton = By.XPath("//button[@class='btn btn-link btn-sm btn-custom small-screen-edit']//span[contains(text(),'Cancel')]");
            public static readonly By RegistryRestrictedCheckbox = By.XPath("//input[@id='field-IsRestricted']");
            public static readonly By RegistryRestrictedCancelBtnInPopup = By.XPath("//div[@class='popup-detail-form medium medium-details-scope larger-width front']//div//span[contains(text(),'Cancel')]");
            public static readonly By RestictedButton = By.Id("field-IsRestricted");
            public static readonly By RegistryEntryViewMenuButton = By.XPath("//i[@class='glyphicon glyphicon-option-horizontal always-visible']");
            public static readonly By RegistryEntryProcessing = By.XPath("//ul[@class='select2-selection__rendered ui-sortable']");
            public static readonly By RegistryEntryToAddressLiList = By.XPath("//div[@class='recipients-field']//ul[@class='select2-selection__rendered']//child::li[@class='select2-selection__choice']");
            public static readonly By RegistryEntryTitleInJournalposter = By.XPath("//div[@id='titleCensor']");
            public static readonly By RegistryEntrySaveButtonInJournalposter = By.XPath("//button[@class='btn btn-sm btn-custom btn-success']");
            public static readonly By RegistryEntryEditButtonInJournalposter = By.XPath("//div[@class='menu-colored-structure menu-right menu-color menu-top menu-result list-top-header']//span[contains(text(),'Edit')]");
            public static readonly By PreservationTime = By.XPath("//div[@class='form-group']//input[@id='preservationTime']");
        }

        private readonly RemoteWebDriver _driver;
        private readonly string _regEntryTypeXPath = "//span[@class='text-capitalize'][contains(text(),";

        public RegistryEntryPage(RemoteWebDriver driver)
        {
            _driver = driver;
        }
        public static RegistryEntryPage Connect(RemoteWebDriver driver)
        {
            return new RegistryEntryPage(driver);
        }

        #region Create Registry Entry
        /// <summary>
        /// Click Registry entry link and click the specified registry entry type
        /// </summary>
        /// <param name="registryType">Registry Type to select/click</param>
        public void CreateRegistryEntry(string entryType)
        {
            //Click on New Registry Entry
            _driver.WaitForElementVisible(RegistryEntryLocators.RegistryButton);
            _driver.ClickOnElement(RegistryEntryLocators.RegistryButton);

            var regEntryXPath = string.Format("{0}'{1}')]", _regEntryTypeXPath, entryType);

            // Click on Registry Entry type
            _driver.WaitForElementVisible(By.XPath(regEntryXPath));
            _driver.ClickOnElement(By.XPath(regEntryXPath));
        }
        #endregion

        #region Enter Registry Entry Title
        /// <summary>
        /// Enter Registry Entry Title
        /// </summary>
        /// <param name="RegistryEntryTitle"></param>
        public void AddRegTitle(string RegistryEntryTitle)
        {
            CommonMethods.PlayWait(2000);
            _driver.WaitForElementVisible(RegistryEntryLocators.RegistryEntryTitle);
            _driver.EnterText(RegistryEntryLocators.RegistryEntryTitle, RegistryEntryTitle);
        }
        #endregion

        #region Add Registry To Address
        /// <summary>
        /// Add address to the registry 
        /// </summary>
        public void AddRegToAddress(string searchKeyword)
        {
            // Enter Registry Enter Receiver
            CommonMethods.PlayWait(3000);
            try
            {
                _driver.EnterText(RegistryEntryLocators.Receiver, searchKeyword);
            }
            catch
            {
                _driver.EnterText(RegistryEntryLocators.FromAddress, searchKeyword);
            }           

            // Select the highlighted search option
            CommonMethods.PlayWait(5000);
            _driver.ClickOnElement(RegistryEntryLocators.SelectHighlighted);
        }
        #endregion 

        #region Add Multiple Reg To Address
        /// <summary>
        /// Add multiple regitry entry To Address
        /// </summary>
        /// <param name="searchKeyword"></param>
        public void AddMultipleRegToAddress(string searchKeyword)
        {
            CommonMethods.PlayWait(3000);
            IWebElement element = _driver.FindElement(By.XPath("//*[normalize-space(.)='Registry entry details']//following::div[@class='sender-recipient-container']//following::input"));
            element.Click();
            element.SendKeys("AB");
            CommonMethods.PlayWait(2000);
            element.SendKeys(Keys.Enter);
        }
        #endregion 

        #region Edit registry To address
        /// <summary>
        /// Edit registry To address
        /// </summary>
        /// <param name="recipientName"></param>
        /// <param name="email"></param>
        public void EditRegistryToAddress(string recipientName, string email)
        {
            CommonMethods.PlayWait(2000);
            _driver.ClickOnElement(RegistryEntryLocators.EditRegEntry);
            CommonMethods.PlayWait(2000);
            _driver.ClickOnElement(By.XPath(string.Format("//span[text()='{0}']", recipientName)));
            CommonMethods.PlayWait(2000);

            //Enter email Id
            _driver.WaitForElementVisible(RegistryEntryLocators.EmailRecipient);
            _driver.EnterText(RegistryEntryLocators.EmailRecipient, email);
            CommonMethods.PlayWait(3000);

            _driver.WaitForElementVisible(RegistryEntryLocators.RecientEditSaveOK);
            _driver.ClickOnElement(RegistryEntryLocators.RecientEditSaveOK);
            CommonMethods.PlayWait(3000);
        }
        #endregion

        #region Delete RegistryEntry To address
        /// <summary>
        /// Delete RegistryEntry To address
        /// </summary>
        /// <param name="recipientName"></param>
        public void DeleteRegistrEntryToAddress(string recipientName)
        {
            CommonMethods.PlayWait(3000);
            _driver.ClickOnElement(RegistryEntryLocators.EditRegEntry);

            CommonMethods.PlayWait(3000);
            _driver.ClickOnElement(By.XPath(string.Format("//span[text()='{0}']//parent::li//child::span[1]", recipientName)));
        }
        #endregion

        #region save registry
        /// <summary>
        /// save registry
        /// </summary>
        public void ClickSaveBttn()
        {
            CommonMethods.PlayWait(2000);
            _driver.ClickOnElement(RegistryEntryLocators.Save);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
            CommonMethods.PlayWait(3000);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion 

        #region Edit Registry Entry title
        /// <summary>
        /// Edit the Registry Entry title
        /// </summary>
        public void EditRegistryEntryTitle(string registryTitle)
        {
            CommonMethods.PlayWait(5000);
            _driver.WaitForElementVisible(RegistryEntryLocators.EditRegistryEntryTitle);
            _driver.ClickOnElement(RegistryEntryLocators.EditRegistryEntryTitle);

            CommonMethods.PlayWait(4000);
            IWebElement element = _driver.FindElement(RegistryEntryLocators.RegistryEntryHeader);
            element.SendKeys(Keys.Control + "a");
            CommonMethods.PlayWait(2000);

            element.SendKeys(registryTitle);
            CommonMethods.PlayWait(2000);

            _driver.ClickOnElement(RegistryEntryLocators.Save);
            CommonMethods.PlayWait(2000);
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
            catch
            {
                try
                {
                    _driver.SelectKendoDropdownAndAddValues(dropdownLabelName, dropdownValue);
                }
                catch (Exception ex)
                {
                    var errorMessage =  string.Format("Unable to select the value in the drop down:{0} - Value:{1}\n {2}", dropdownLabelName, dropdownValue, ex);
                    CommonMethods.ThrowExceptionAndBreakTC(errorMessage);
                }
            }
        }
        #endregion

        #region Edit registry
        /// <summary>
        /// Edit registry entry
        /// </summary>
        public void ClickEditButton()
        {
            CommonMethods.PlayWait(6000);
            _driver.WaitForElementVisible(RegistryEntryLocators.EditRegEntry);
            _driver.ClickOnElement(RegistryEntryLocators.EditRegEntry);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
            CommonMethods.PlayWait(2000);
        }
        #endregion

        #region Edit Registry title as screened text ot person name
        /// <summary>
        /// Edit Registry title as screened text ot person name
        /// </summary>
        /// <param name="selectedText">Word/Text to mark</param>
        /// <param name="Option">Options to change coclor or italics</param>
        /// <param name="endTitle">Second Word/Text to mark</param>
        public void EditRegistryTitleScreenedTextOrPersonName(string selectedText, string Option = "", string endTitle = "")
        {
            CommonMethods.PlayWait(2000);
            var registryEntryTitleInEditMode = _driver.FindElement(RegistryEntryLocators.RegistryEntryTitle).Text;
            string[] splitText = registryEntryTitleInEditMode.Split(' ');

            foreach (var markedTextScreened in splitText)
            {
                if (markedTextScreened == selectedText)
                {
                    var title = markedTextScreened;
                    IWebElement selectedWord = _driver.FindElement(By.XPath("//h4[@id='titleCensor']//span[text()='" + title + "']"));
                    _driver.MouseDoubleClickOnElement(selectedWord);

                    if (endTitle != "")
                    {
                        IWebElement destinationTitle = _driver.FindElement(By.XPath(string.Format("//h4[@id='titleCensor']//span[text()='{0}']", endTitle)));
                        Actions action = new Actions(_driver);
                        action.DragAndDrop(selectedWord, destinationTitle).Perform();
                    }
                    CommonMethods.PlayWait(2000);
                    _driver.MouseRightClickOnElement(selectedWord);
                    CommonMethods.PlayWait(3000);

                    if (Option == TitleModify.Screened.GetStringValue())
                    {
                        _driver.FindElement(By.XPath("//span[text()='screened']")).Click(); break;
                    }
                    else if (Option == TitleModify.PersonName.GetStringValue())
                    {
                        _driver.FindElement(By.XPath("//span[text()='person name']")).Click(); break;
                    }
                    else if (Option == TitleModify.RemoveScreeningFromText.GetStringValue())
                    {
                        _driver.FindElement(By.XPath("//span[text()='screening']")).Click(); break;
                    }
                    else if (Option == TitleModify.RemoveMarkingPersonName.GetStringValue())
                    {
                        _driver.FindElement(By.XPath("//span[text()='Remove marking of ']")).Click(); break;
                    }
                }
            }
        }

        #region Verify the registry title color and style
        /// <summary>
        /// Verify the registry title color and style
        /// </summary>
        /// <param name="selectedText">Word/Text to mark</param>
        /// <param name="color"></param>
        /// <param name="mode">If we use multiple words together pass the last word as the endtitle</param>
        public void VerifyRegistryTitleColorAndFormat(string selectedText, string color = "", string mode = "")
        {
            CommonMethods.PlayWait(3000);
            var element = (mode == "") ? _driver.FindElement(RegistryEntryLocators.RegistryEntryTitleNormalMode).Text : _driver.FindElement(RegistryEntryLocators.RegistryEntryTitle).Text;

            string[] splitText = element.Split(' ');

            foreach (var markedTextScreened in splitText)
            {
                if (markedTextScreened == selectedText)
                {
                    var title = markedTextScreened;
                    var selectedTitle = (mode == "") ? _driver.FindElement(By.XPath("//h4[@class='details-header-text']//span[contains(text(),'" + selectedText + "')]")) :
                        _driver.FindElement(By.XPath("//h4[@id='titleCensor']//span[contains(text(),'" + selectedText + "')]"));

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
        #endregion

        #region Verify title text color, format and hidden letters
        /// <summary>
        /// Verify the Edited Registry Title text color, format And hidden letters
        /// </summary>
        /// <param name="selectedText">Word to select</param>
        /// <param name="wordPosition">Word position</param>
        /// <param name="wordStarPosition">Word as Starr Position</param>
        /// <param name="endTitle">If we use multiple words together pass the last word as the endtitle</param>
        public void VerifyRegistryTitleMarkedAsStarsAndItalic(string selectedText, int wordPosition, string color = "", int wordStarPosition = 0, string endTitle = "")
        {
            CommonMethods.PlayWait(3000);
            var element = _driver.FindElement(RegistryEntryLocators.RegistryEntryTitleNormalMode).Text;
            IWebElement selectedTitle = _driver.FindElement(By.XPath("//h4[@class='details-header-text']//span[contains(text(),'" + selectedText + "')]"));

            string[] splitText = element.Split(' ');

            if (selectedText == "*****")
            {
                var starWords = _driver.FindElements(By.XPath("//h4[@class='details-header-text']//span[contains(text(),'" + selectedText + "')]"));
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
                    var selectedTitles = _driver.FindElement(By.XPath("//h4[@class='details-header-text']//span[contains(text(),'" + selectedText + "')]"));
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
                    else if (highlighTextColor == "censored-text" && color == TitleBackgroundFormat.Red.GetStringValue())
                    {
                        Assert.IsTrue("censored-text" == highlighTextColor, "Red color ({0}) is not exits at this position {1} in the case title", selectedText, wordPosition);
                    }
                    else
                    {
                        Assert.Fail("Star ({0}) is not matches the text at this position {1} in the case title", selectedText, wordPosition);
                    }
                }
            }
        }
        #endregion

        #region Select (click on chedkbox next to) Registry Entry
        /// <summary>
        /// Select (click on chedkbox next to) Registry Entry
        /// </summary>
        /// <param name="registryTitle">Partial Registry Title</param>
        public void ClickRegistryLevelCheckBox(string registryTitle = "")
        {
            CommonMethods.PlayWait(2000);
            SwitchRegistryEntryView();
            CommonMethods.PlayWait(2000);
            var checkbox = _driver.FindElement(By.XPath(string.Format("//span[@class='registryentry-title']//span[contains(text(),'{0}')]/ancestor::div[2]//following::div[@class='enhanced-container actions check dynamic']", registryTitle)));
            checkbox.DrawHighlight();
            checkbox.Click();
        }
        #endregion

        #region Select (click on checkbox next to) Registry Entry
        /// <summary>
        /// Select (click on checkbox next to) Registry Entry
        /// </summary>
        /// <param name="caseNumber">Case Number</param>
        /// <param name="view"></param>
        public void ClickRegistryLevelCheckBox(string caseNumber = "", string view = "Grid")
        {
            CommonMethods.PlayWait(2000);
            SwitchRegistryEntryView(view);
            CommonMethods.PlayWait(2000);
            var checkbox = _driver.FindElement(By.XPath(string.Format("//a[contains(text(),'{0}')]//parent::td//parent::tr//child::input", caseNumber)));
            checkbox.DrawHighlight();
            checkbox.Click();
        }
        #endregion

        #region  Click Registry Entry Level options like "Set Status J"
        /// <summary>
        /// Click Registry Entry Level options like "Set Status J"
        /// </summary>
        /// <param name="registryLevelOption">Options like "Set Status J"</param>
        public void ClickRegistryLevelOptions(string registryLevelOption)
        {
            var button = _driver.FindElement(By.XPath(string.Format("//button[@class='btn btn-sm btn-link no-underline condensed-toggle-btn']//span[contains(text(),'{0}')]", registryLevelOption)));
            button.DrawHighlight();
            button.Click();
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Click on the Registry Entry with multiple registry entry
        /// <summary>
        /// Click on the Registry Entry with multiple registry entry
        /// </summary>
        /// <param name="registryTitle">Registry Title</param>
        /// <param name="switchRegistryEntryView"></param>
        public void ClickRegistry(string registryTitle, bool switchRegistryEntryView = true)
        {
            CommonMethods.PlayWait(2000);

            if (switchRegistryEntryView)
            {
                SwitchRegistryEntryView();
            }

            CommonMethods.PlayWait(2000);

            var ulItem = _driver.FindElement(RegistryEntryLocators.RegistryEntryUlList);
            var li = ulItem.FindElements(RegistryEntryLocators.RegistryEntryLiList);

            foreach (var regEnt in li)
            {
                if (regEnt.Text.Replace("\n", "").Replace("\r", "").Trim() == registryTitle)
                {
                    regEnt.Click(); break;
                }
            }
        }
        #endregion

        #region Click on the Registry Entry with multiple registry entry in Grid View
        /// <summary>
        /// Click on the Registry Entry with multiple registry entry in Grid View
        /// </summary>
        /// <param name="caseNumber">Case Number</param>
        /// <param name="view"></param>
        public void ClickRegistry(string caseNumber)
        {
            CommonMethods.PlayWait(2000);

            SwitchRegistryEntryView("Grid");

            CommonMethods.PlayWait(2000);

            var registryEntry = _driver.FindElement(By.XPath(string.Format("//a[contains(text(),'{0}')]//parent::td//parent::tr", caseNumber)));
            registryEntry.DrawHighlight();

            var registryEntryToCLick = _driver.FindElement(By.XPath(string.Format("//a[contains(text(),'{0}')]//parent::td//parent::tr//child::td[2]", caseNumber)));
            registryEntryToCLick.DrawHighlight();
            registryEntryToCLick.Click();
        }
        #endregion

        #region Switch the registry entry view to list or grid
        /// <summary>
        /// Switch the registry entry view to list or grid
        /// </summary>
        /// <param name="view"></param>
        public void SwitchRegistryEntryView(string view = "List")
        {
            CommonMethods.PlayWait(3000);
            IWebElement button = null;
            try
            {
                button = _driver.FindElement(string.Format("//div[@class='btn-group btn-group-xs btn-toggle-group pull-right search-view-menu']//button[@class='btn btn-toggle btn-{0}']", view == "List" ? "list" : "grid"));
            }
            catch
            {
                button = _driver.FindElement(string.Format("//div[contains(@class,'btn-group btn-group-xs btn-toggle-group pull-right search-view-menu')]//button[@class='btn btn-toggle btn-{0}']", view == "List" ? "list" : "grid"));
            }
            button.Click();
        }

        #endregion

        #region Verify Registry Title In Saksmapper
        /// <summary>
        /// Verify Registry Title In Saksmapper is matching with the actual title
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="screenedFullCaseTitle"></param>
        public void VerifyRegistryTitleInSaksmapper(string caseId, string screenedFullCaseTitle)
        {
            CommonMethods.PlayWait(5000);
            var registryTitleGUI = _driver.FindElement(RegistryEntryLocators.RegistryEntryTitleNormalMode);
            SeleniumExtensions.DrawHighlight(registryTitleGUI);
            Assert.IsTrue(registryTitleGUI.Text == screenedFullCaseTitle, "Screened registry title in JournalPost is mismatching : " + screenedFullCaseTitle);
        }
        #endregion

        #region Get the screened registry title
        /// <summary>
        /// Get the screened registry title
        /// </summary>
        /// <returns></returns>
        public string GetScreenedRegistryTitle()
        {
            CommonMethods.PlayWait(3000);
            return _driver.FindElement(RegistryEntryLocators.RegistryEntryTitleNormalMode).Text;
        }
        #endregion

        #region Click on Registry Entry More button
        /// <summary>
        /// Click on Registry Entry More(Arrow) button
        /// </summary>
        public void ClickMoreArrow()
        {
            CommonMethods.PlayWait(3000);
            var moreArrowElement = _driver.FindElements(RegistryEntryLocators.RegistryMoreArrow);
            if (moreArrowElement.Any())
                moreArrowElement.FirstOrDefault().Click();
        }
        #endregion

        #region Click on Registry Entry reciepient names button
        /// <summary>
        /// Click on Registry Entry reciepient names button
        /// </summary>
        /// <param name="reciepientName">Name of the person</param>
        public void ClickToAddressReciepientName(string reciepientName)
        {
            CommonMethods.PlayWait(3000);
            ModifyRegistryEntryToField(reciepientName);
        }
        #endregion

        #region Click on Registry Entry cancel button
        /// <summary>
        /// Click on Registry Entry cancel button
        /// </summary>
        public void ClickCancelButton()
        {
            CommonMethods.PlayWait(3000);
            var cancelButtonElement = _driver.FindElement(RegistryEntryLocators.RegistryCancelButton);
            cancelButtonElement.Click();
            CommonMethods.PlayWait(3000);
        }
        #endregion

        #region Verify restricted checkbox value
        /// <summary>
        /// Verify restricted checkbox value
        /// </summary>
        /// <param name="value">Value to verify</param>
        public void VerifyRestrictedCheckboxValue(bool value = true)
        {
            CommonMethods.PlayWait(3000);
            var restrictedCheckboxElement = _driver.FindElement(RegistryEntryLocators.RegistryRestrictedCheckbox);
            restrictedCheckboxElement.DrawHighlight();
            Assert.IsTrue(restrictedCheckboxElement.Selected == value, "Restricted checkbox for the To adress person is not matching");
        }
        #endregion

        #region Click cancel in the restricted pop up window
        /// <summary>
        /// Verify restricted checkbox value
        /// </summary>
        public void ClickCancelButtonInRestrictedPopup()
        {
            CommonMethods.PlayWait(3000);
            var cancelButtonElement = _driver.FindElement(RegistryEntryLocators.RegistryRestrictedCancelBtnInPopup);
            cancelButtonElement.Click();
        }
        #endregion

        #region Verify In registry entry
        /// <summary>
        /// Verify In registry entry
        /// </summary>
        /// <param name="dropdownName"></param>
        /// <param name="dropdownValue"></param>
        public void VerifyDropDownValue(string dropdownName, string dropdownValue)
        {
            CommonMethods.PlayWait(2000);
            _driver.VerifyDropDownValue(dropdownName, dropdownValue);
        }
        #endregion

        #region Verify sender is screened
        /// <summary>
        /// Verify sender is screened
        /// </summary>
        /// <param name="senderName"></param>
        public void VerifySenderTextScreened(string senderName)
        {
            CommonMethods.PlayWait(3000);
            var restrictedSenderElement = _driver.FindElement(By.XPath(string.Format("//li[@class='inline-list restricted' and contains(text(), '{0}')]", senderName)));
            restrictedSenderElement.DrawHighlight();
            Assert.IsTrue(restrictedSenderElement.GetAttribute("Class") == "inline-list restricted", "Sender Text (To person): " + senderName + " is not red color");
        }
        #endregion

        #region Modify the Registry Entry To Filed
        /// <summary>
        /// Modify the Registry Entry To Filed
        /// </summary>
        /// <param name="toFieldName"></param>
        public void ModifyRegistryEntryToField(string toFieldName)
        {
            CommonMethods.PlayWait(5000);
            _driver.ClickOnElement(By.XPath(string.Format("//span[contains(text(),'{0}')]", toFieldName)));
        }
        #endregion

        #region Verify Checkbox Is Selected
        /// <summary>
        /// Verify Checkbox Is Selected Or not
        /// </summary>
        /// <param name="buttonName"></param>
        public void VerifyRestrictedCheckBoxIsSelected(string buttonName)
        {
            CommonMethods.PlayWait(3000);
            IWebElement checkBox = _driver.FindElement(RegistryEntryLocators.RestictedButton);
            bool checkBoxSelect = checkBox.Selected;

            if (checkBoxSelect == false)
            {
                checkBox.Click();
            }
            bool checkBoxSelects = checkBox.Selected;
            SeleniumExtensions.DrawHighlight(_driver.FindElement(By.XPath("//span[text()='Restricted']")));
            Assert.IsTrue(true == checkBoxSelects, "Checkbox {0} is Selected", checkBoxSelects);

            _driver.ClickOnButton("OK");
        }
        #endregion

        #region Remove value from dropdown
        /// <summary>
        /// Remove Value from dropdown
        /// </summary>
        public void RemoveDropdownValue(string dropdownName)
        {
            CommonMethods.PlayWait(2000);
            _driver.RemoveDropdownValue(dropdownName);
        }
        #endregion

        #region Change drop down status to F/Done
        /// <summary>
        /// Change drop down status to F/Done
        /// </summary>
        public void ChangeREStatusToFerdigOrDone(string dropdownName = "Status")
        {
            CommonMethods.PlayWait(2000);
            ClickMoreArrow();
            SelectKendoDropdownAndAddValue(dropdownName, "Ferdig/Done");
        }
        #endregion

        #region Click on the attachment option in the document
        /// <summary>
        /// Click on the attachment option in the document
        /// </summary>
        /// <param name="documentName"></param>
        /// <param name="attachmentOption"></param>
        public void ClickAttachmentOption(string documentName, string attachmentOption)
        {
            CommonMethods.PlayWait(3000);
            var docElement = _driver.FindElement(By.XPath(string.Format("//span[contains(text(),'{0}')]//following::button[@class='btn btn-sm btn-link no-underline dropdown-toggle']", documentName)));
            docElement.SendKeys(Keys.Space);
            docElement.SendKeys(Keys.Tab);
            CommonMethods.PlayWait(2000);
            AutoItX.Send("{Enter}");
        }
        #endregion

        #region Registry Entry View Menu
        /// <summary>
        /// Registry Entry View Menu
        /// </summary>
        /// <param name="menuItem"></param>
        public void RegistryEntryViewMenu(string menuItem)
        {
            CommonMethods.PlayWait(3000);
            _driver.ClickOnElement(RegistryEntryLocators.RegistryEntryViewMenuButton);
            CommonMethods.PlayWait(2000);
            _driver.ClickOnElement(By.XPath(string.Format("//a[text()='{0}']", menuItem)));
            CommonMethods.PlayWait(10000);
        }
        #endregion

        #region Get innner text from the drop down
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

        #region Verify Registry Options are Accessible
        /// <summary>
        /// Verify Registry Options are Accessible
        /// </summary>
        /// <param name="regOption"></param>
        /// <param name="errorMessage"></param>
        public void VerifyRegistryOptionsAccessible(string regOption, string errorMessage)
        {
            CommonMethods.PlayWait(3000);
            _driver.ClickOnElement(By.XPath(string.Format("//ul[@class='nav nav-tabs sub-collections-nav-tabs nav-in-sub']//following::span[text()='{0}']", regOption)));
            CommonMethods.PlayWait(2000);
            _driver.ValidateRedPopUp(errorMessage);
        }
        #endregion

        /// <summary>
        /// Verify Registry entry is able to create
        /// </summary>
        /// <param name="regOption"></param>
        /// <param name="errorMessage"></param>
        public void VerifyRegistryEntryAbleToCreate(string entryType, string errorMessage)
        {
            CommonMethods.PlayWait(3000);
            _driver.ClickOnButton("Registry entry");
            CommonMethods.PlayWait(2000);
            var regEntryXPath = By.XPath(string.Format("{0}'{1}')]", _regEntryTypeXPath, entryType));
            // Click on Registry Entry type
            _driver.WaitForElementVisible(regEntryXPath);
            _driver.ClickOnElement(regEntryXPath);
            CommonMethods.PlayWait(2000);
            _driver.ValidateRedPopUp(errorMessage);
        }

        #region Add Processing or Handling
        /// <summary>
        /// Add Process
        /// </summary>
        /// <param name="processName"></param>
        public void AddProcessing(string processName)
        {
            SelectKendoDropdownUsingPartialText("Processing", processName, processName + " (Politisk sak)", processName + " (Politisk sak)");
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

        #region Click on the Registry Entry by Type name with multiple registry entry
        /// <summary>
        /// Click on the Registry Entry by Type with multiple registry entry
        /// </summary>
        /// <param name="registryType">Registry Title</param>
        /// <param name="switchRegistryEntryView"></param>
        public void ClickRegistryByType(string registryType, bool switchRegistryEntryView = true)
        {
            CommonMethods.PlayWait(2000);

            if (switchRegistryEntryView)
            {
                SwitchRegistryEntryView();
            }

            CommonMethods.PlayWait(2000);

            var registryEntryType = _driver.FindElement(By.XPath(string.Format("//span[contains(@class,'text-overflow-ellipsis')]//span[contains(text(),'{0}')]", registryType)));
            registryEntryType.DrawHighlight();
            registryEntryType.Click();
        }
        #endregion

        #region Validate U type RE availability on RE Open Screen 
        /// <summary>
        /// Validate U type RE availability on RE Open Screen 
        /// </summary>
        /// <param name="RETypeName"></param>
        public void ValidateUTypeRE(string RETypeName)
        {
            var registryEntryType = _driver.FindElement(By.XPath("//span[@class='large-screen']"));
            registryEntryType.DrawHighlight();
            Assert.IsTrue(registryEntryType.Text == RETypeName, "Registry Entry Type is not " + RETypeName + "after Click on choose Create party letter (Include decision). " + "The Party letter Reg Entry is not converted to U Type RE");
        }
        #endregion

        #region Validate Case Draft RE title with U Type RE title for Case Party

        /// <summary>
        /// Validate Case Draft RE title with U Type RE title for Case Party
        /// </summary>
        /// <param name="RETitle"></param>
        public void ValidateRETitle(string RETitle)
        {
            CommonMethods.PlayWait(3000);
            var registryTitle = _driver.FindElement(RegistryEntryLocators.RegistryEntryTitle).Text;
            Assert.IsTrue(registryTitle == RETitle, "U Type RE title do not matches with Case Draft RE Title. " + "after Click on choose Create party letter (Include decision).");
        }
        #endregion

        #region Validate Case Parties in RE
        /// <summary>
        /// Validate case party in RE
        /// </summary>
        /// <param name="recipientName"></param>
        public void ValidateCasePartiesInRE(string recipientName)
        {
            bool recipientNameExist = false;
            var recipientNameList = _driver.FindElements(RegistryEntryLocators.RegistryEntryToAddressLiList);

            foreach (var recipient in recipientNameList)
            {
                var recipientNameGUI = recipient.Text;

                if(recipientNameGUI.Contains(recipientName))
                {
                    recipientNameExist = true;
                    break;
                }
            }

            Assert.IsTrue(recipientNameExist == true, "Case Party -> " + recipientName + " not available in U Type RE, " + "after Click on choose Create party letter (Include decision).");
        }
        #endregion

        #region Edit Registry Entry Title in Journalposter
        /// <summary>
        /// Edit Registry Entry Title in Journalposter
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="registryTitle"></param>
        /// <param name="newTitle"></param>
        public void VerifyRegistryEntryTitleEditableInJournalposter(string caseId, string registryTitle, string newTitle)
        {
            CommonMethods.PlayWait(3000);
            ClickRegistry(caseId);
            ClickRegistryLevelCheckBox(caseId, "Grid");

            CommonMethods.PlayWait(2000);
            var regEdit = _driver.FindElement(RegistryEntryLocators.RegistryEntryEditButtonInJournalposter);
            regEdit.Click();
            _driver.HandleErrorPopUpAndThrowErrorMessage();

            CommonMethods.PlayWait(3000);
            var regTitle = _driver.FindElement(RegistryEntryLocators.RegistryEntryTitleInJournalposter);
            regTitle.SendKeys(newTitle);

            var regSave = _driver.FindElement(RegistryEntryLocators.RegistryEntrySaveButtonInJournalposter);
            regSave.Click();
            _driver.HandleErrorPopUpAndThrowErrorMessage();

            var registryEntryNewTitle = _driver.FindElement(By.XPath(string.Format("//a[contains(text(),'{0}')]//parent::td//parent::tr//child::td[5]", caseId)));
            registryEntryNewTitle.DrawHighlight();
            var regNewTitle = registryEntryNewTitle.Text;

            Assert.IsTrue(regNewTitle.Trim() == (registryTitle.Trim()), string.Format("Registry entry title {0} is editable for case id {1} in Journalposter", registryTitle, caseId));
            Assert.IsTrue(regNewTitle.Trim() != (registryTitle.Trim() + newTitle.Trim()), string.Format("Registry entry title {0} is editable for case id {1} in Journalposter", registryTitle, caseId));
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
            var preservationElement = _driver.FindElement(RegistryEntryLocators.PreservationTime);
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
            return GetDropDownValue("Disposal code");
        }
        #endregion
    }
}