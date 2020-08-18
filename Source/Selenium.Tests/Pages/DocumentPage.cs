using AutoIt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using Selenium.Tests.Base.Selenium.Core;
using System;
using System.IO;
using System.Linq;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Pages
{
    public class DocumentPage
    {
        public static class DocumentPageLocators
        {
            public static readonly By Attachment = By.XPath("//span[@data-bind=\"t: 'Attach'\"]");
            public static readonly By submit = By.XPath("//input[@value='Select']");
            public static readonly By Username = By.XPath("//input[@id='username']");
            public static readonly By Password = By.XPath("//input[@id='password']");
            public static readonly By LoginButton = By.XPath("//button[@class='btn btn-primary']");
            public static readonly By SaveAndEditDocument = By.XPath("//span[@data-bind=\"t: 'SaveAndEditDocument', if: $root.isPendingDocumentMerge\"]");
            public static readonly By SelectDocument = By.XPath("//input[@type='submit']");
            public static readonly By MainDocButton = By.XPath("//tr[@class='ui-sortable-handle']//td//input[@type='checkbox']");
            public static readonly By DocCancleButton = By.XPath("//button[@class='btn btn-default btn-sm btn-link']//span[text()='Cancel']");
            public static readonly By DocumentTitleRestricted = By.XPath("//span[@class='attachment-name restricted']");
            public static readonly By DocumentVersions = By.Id("tab_versions_description");
            public static readonly By Message = By.XPath("//iframe[@title='Rich Text Editor, messageDocument']");
            public static readonly By MessageFormat = By.XPath("//span[@title='Variant' and text()='Arkivformat']");
            public static readonly By PDFImage = By.XPath("//span[@class='custom-document-metrics document-icon ra-pdf']");
        }

        #region Document attachment variables
        private readonly RemoteWebDriver _driver;
        private readonly string _attachType = "//button[contains(text()";
        private readonly string _docTemplateType = "//span[contains(text()";
        private readonly string _docSubType = "//div[@class = 'list-item-holder']//span[contains(text()";
        #endregion

        #region constructor
        /// <summary>
        /// Document page constrcutor
        /// </summary>
        /// <param name="driver"></param>
        public DocumentPage(RemoteWebDriver driver)
        {
            _driver = driver;
        }

        public static DocumentPage Connect(RemoteWebDriver driver)
        {
            return new DocumentPage(driver);
        }
        #endregion

        #region Add template
        /// <summary>
        /// add the attachment type
        /// </summary>
        /// <param name="attachmentType"></param>
        public void ClickAndAddDocTemplate(string attachmentType)
        {
            _driver.WaitForElementVisible(DocumentPageLocators.Attachment);
            _driver.ClickOnElement(DocumentPageLocators.Attachment);

            // Select Attachment type from Attach Dropdown
            var attachType = string.Format("{0},'{1}')]", _attachType, attachmentType);
            _driver.WaitForElementVisible(By.XPath(attachType));
            _driver.ClickOnElement(By.XPath(attachType));
        }
        #endregion

        #region document selection
        /// <summary>
        /// select the document
        /// </summary>
        /// <param name="DocumentTemplateType"></param>
        /// <param name="DocumentSubType"></param>
        public void SelectDocument(string DocumentTemplateType, string DocumentSubType)
        {
            // Select Document Template Type
            var docTemplateType = string.Format("{0},'{1}')]", _docTemplateType, DocumentTemplateType);
            _driver.WaitForElementVisible(By.XPath(docTemplateType));
            _driver.ClickOnElement(By.XPath(docTemplateType));

            // Select Document Type
            var docSubType = string.Format("{0},'{1}')]", _docSubType, DocumentSubType);
            _driver.WaitForElementVisible(By.XPath(docSubType));
            _driver.ClickOnElement(By.XPath(docSubType));

            CommonMethods.PlayWait(3000);
            // Click Sebmit button 
            _driver.ClickOnElement(DocumentPageLocators.submit);
        }
        #endregion

        #region Save and Edit Document 
        /// <summary>
        /// // Click save and edit document
        /// </summary>
        public void SaveAndEdit()
        {
            CommonMethods.PlayWait(3000); //Wait Explicitly       
            //CommonMethods.ProcessKill("EphorteDesktopClient");
            //CommonMethods.ProcessKill("WinWord");
            //CommonMethods.ProcessKill("Excel");
            _driver.ClickOnElement(DocumentPageLocators.SaveAndEditDocument);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region AutoItX for OpenElementsDesktopClient
        /// <summary>
        /// Handling Alert pop up for OpenElementsDesktopClient
        /// </summary>
        public void HandleAlertPopupToOpenWordDocument()
        {
            CommonMethods.PlayWait(5000);
            AutoItX.WinWait("Untitled - Google Chrome", "", 2);
            AutoItX.WinActivate("Untitled - Google Chrome"); // Activate so that next set of actions happens on this window
            AutoItX.Send("{LEFT}");
            AutoItX.Send("{Enter}");
        }
        #endregion

        #region Document Web Login Window
        /// <summary>
        /// Document web login window click
        /// </summary>
        public void DocumentWebLoginWindow(string username = "guilt")
        {
            //Login Window
            CommonMethods.PlayWait(15000);

            AutoItX.WinActivate("LoginWebView");

            if (AutoItX.WinExists("LoginWebView", "").ToString() == "1")
            {
                AutoItX.WinWait("LoginWebView", "", 2);
                AutoItX.WinActivate("LoginWebView");
                CommonMethods.PlayWait(4000);
                AutoItX.Send("{TAB}");
                AutoItX.Send(username);
                CommonMethods.PlayWait(5000);
                AutoItX.Send("{TAB}");
                // Usually checks a checkbox (if it's a "real" checkbox.)
                AutoItX.Send("{SPACE}");
                CommonMethods.PlayWait(5000);
                AutoItX.Send("{TAB}");
                AutoItX.Send("{ENTER}");
            }
        }
        #endregion

        #region Edit And Save Word Document with RegistryEntryTitle
        /// <summary>
        /// Edit And Save Word Document the opened word document
        /// </summary>
        /// <param name="RegistryEntryTitle"></param>
        /// <param name="editText"></param>
        public void EditAndSaveWordDocument(string RegistryEntryTitle, string editText = "Edit document")
        {
            //Running .exe files hence using Explicitly wait
            CommonMethods.PlayWait(10000);
            AutoItX.WinWait(RegistryEntryTitle + "  - Word", "", 10);

            //Activate - So that next set of actions happen on this window
            AutoItX.WinActivate(RegistryEntryTitle + "  - Word", "");
            AutoItX.WinActivate("[CLASS:OpusApp]", "");

            //Set input focus to the edit control of Upload window using the handle returned by WinWait
            AutoItX.ControlClick(RegistryEntryTitle + " - Word", "", "_WwG1");

            AutoItX.Send("!+{SPACE}", 0);
            AutoItX.Send("x", 0);
            AutoItX.Send("{ENTER}");
            AutoItX.Send(editText);
            AutoItX.Send("^s");
        }
        #endregion

        #region Edit And Save Word Document
        /// <summary>
        /// Edit And Save Word Document the opened word document
        /// </summary>
        /// <param name="editText"></param>
        public void EditAndSaveWordDocument(string editText = "Edit document")
        {
            //Running .exe files hence using Explicitly wait
            CommonMethods.PlayWait(10000);

            if (AutoItX.WinExists("[CLASS:OpusApp]", "").ToString() == "1")
            {
                //Activate - So that next set of actions happen on this window
                AutoItX.WinActivate("[CLASS:OpusApp]", "");

                //Set input focus to the edit control of Upload window using the handle returned by WinWait
                AutoItX.ControlClick("[CLASS:OpusApp]", "", "", "_WwG1");

                AutoItX.Send("!+{SPACE}", 0);
                AutoItX.Send("x", 0);
                AutoItX.Send("{ENTER}");
                AutoItX.Send(editText);
                AutoItX.Send("^s");
            }
            else { CommonMethods.ThrowExceptionAndBreakTC("EDC Word Document is not opened"); }
        }
        #endregion

        #region Close word document - All
        /// <summary>
        /// Close opened word document
        /// </summary>
        /// <param name="documentCount"></param>
        public void CloseDocument(int documentCount = 2)
        {
            CommonMethods.PlayWait(3000);

            //Close one document only 
            AutoItX.WinActivate("[CLASS:OpusApp]", "");
            AutoItX.WinClose(AutoItX.WinGetClassList("[CLASS:OpusApp]", ""));

            //Close all documents in the documentCount flag if any
            for (int i = 0; i < documentCount; i++)
            {
                CommonMethods.PlayWait(2000);
                AutoItX.WinActivate("[CLASS:OpusApp]", "");
                AutoItX.WinClose("[CLASS:OpusApp]", "");
            }
        }

        #endregion

        #region Close word document
        /// <summary>
        /// Close opened word document
        /// </summary>
        public void CloseDocument()
        {
            CommonMethods.PlayWait(5000);

            //Close one document only 
            AutoItX.WinActivate("[CLASS:OpusApp]", "");
            AutoItX.WinClose("[CLASS:OpusApp]", "");
            CommonMethods.ProcessKill("WinWord");
        }

        #endregion

        #region Document Mark as complete
        /// <summary>
        /// Document checkin message click mark as complete
        /// </summary>
        public void DocumentCheckinMessgaeClickMarkAsComplete()
        {
            // Add Script for Checkin Msg
            // Process.Start(@"C:\Projects\AutoIT\AutoItX Scripts\CheckinScript.exe");
            AutoItX.WinActivate("Mark as complete");
            var winHandle = AutoItX.WinGetHandle("[REGEXPCLASS:(?i)HwndWrapper[ElementsDesktopClient.exe;;.*]]");
            AutoItX.WinActivate(winHandle);
            AutoItX.ControlClick("Mark as complete", "", "TextBlock");
            AutoItX.MouseClick("left", 1);
            AutoItX.MouseClick("left", 1);

            AutoItX.WinActivate("[REGEXPCLASS:(?i)HwndWrapper[ElementsDesktopClient.exe;;.*]]");
            var winTitle = AutoItX.WinGetTitle("[REGEXPCLASS:(?i)HwndWrapper[ElementsDesktopClient.exe;;.*]]");

            CommonMethods.PlayWait(3000);

            AutoItX.WinActivate(@"[X:1535\Y:842\W:356\H:188]");

            //Click on the edc
            AutoItX.MouseClick();

            //Select master as complete
            AutoItX.Send("{ENTER}");

            //Move to the ok button
            AutoItX.Send("{TAB}");
            AutoItX.Send("{TAB}");
            AutoItX.Send("{TAB}");

            //Click on ok button
            AutoItX.Send("{ENTER}");
            AutoItX.MouseClick();
        }
        #endregion

        #region Add attachment
        /// <summary>
        /// Add attachment
        /// </summary>
        /// <param name="attach"></param>
        /// <param name="option"></param>
        public void AddAttachment(string attach, string option)
        {
            CommonMethods.PlayWait(3000);
            _driver.ClickOnButton(attach);
            CommonMethods.PlayWait(2000);
            _driver.ClickOnButton(option);
        }
        #endregion

        #region Attach file from directory/local disk
        /// <summary>
        /// Attach file from directory/local disk
        /// </summary>
        /// <param name="attach"></param>
        /// <param name="option"></param>
        /// <param name="filePath"></param>
        public void AttachFileFromDirectory(string attach, string option, string filePath)
        {
            AddAttachment(attach, option);
            UploadFileAsAttachment(filePath);
        }
        #endregion

        #region Upload file as attachment
        /// <summary>
        /// Upload file as attachment
        /// </summary>
        /// <param name="filePath"></param>
        public void UploadFileAsAttachment(string filePath)
        {
            // string filePath = "\"C:\\Users\\Public\\Pictures\\Sample Pictures\\Koala.jpg\" \"C:\\Users\\Public\\Pictures\\Sample Pictures\\Hydrangeas.jpg\"";
            AutoItX.WinWait("[CLASS:#32770]", "", 5);
            AutoItX.WinWaitActive("Open");
            AutoItX.ControlFocus("Open", "", "Edit1");
            AutoItX.ControlSetText("Open", "", "Edit1", filePath);
            CommonMethods.PlayWait(3000);
            AutoItX.ControlClick("Open", "", "Button1");
        }
        #endregion

        #region Select document option
        /// <summary>
        /// Select document option
        /// </summary>
        /// <param name="docOption"></param>
        public void SelectDocument(bool docOption = false)
        {
            CommonMethods.PlayWait(2000);
            if (docOption == true)
            {
                _driver.ClickOnElement(DocumentPageLocators.MainDocButton);
            }
            else
            {
                _driver.WaitForElementVisible(DocumentPageLocators.SelectDocument);
                _driver.ClickOnElement(DocumentPageLocators.SelectDocument);
            }
        }
        #endregion

        #region View attachment details
        /// <summary>
        /// View attachment details
        /// </summary>
        /// <param name="documentName"></param>
        /// <param name="attachmentOption"></param>
        /// <param name="documentCount"></param>
        public void ViewAttachmentInformation(string documentName, string attachmentOption, int documentCount = 1)
        {
            CommonMethods.PlayWait(3000);
            try
            {
                _driver.MouseHoverClickOnElement(_driver.FindElement(By.XPath(string.Format("//span[text()='{0}']//following::button[@class='btn btn-sm btn-link no-underline dropdown-toggle']", documentName))));
            }
            catch
            {
                try
                {
                    _driver.MouseHoverClickOnElement(_driver.FindElement(By.XPath(string.Format("//span[contains(text(),'{0}')]//following::button[@class='btn btn-sm btn-link no-underline dropdown-toggle']", documentName))));
                }
                catch
                {
                    _driver.MouseHoverClickOnElement(_driver.FindElement(By.XPath(string.Format("//span[contains(text(),'{0}')]//following::button[@class='no-underline dropdown-toggle btn btn-sm btn-link']", documentName))));
                }
            }
            CommonMethods.PlayWait(2000);
            try
            {
                _driver.MouseHoverClickOnElement(_driver.FindElement(By.XPath(string.Format("//li[@class='attachment dropdown']//button[text()='{0}']", attachmentOption))));
            }
            catch
            {
                var documents = _driver.FindElements(By.XPath(string.Format("//li[{0}]//button[text()='{1}']", documentCount, attachmentOption)));

                var removeValue = documents.LastOrDefault();
                removeValue.Click();
            }
        }
        #endregion

        public void ViewAttachmentInformation(string documentOption)
        {
            CommonMethods.PlayWait(3000);
            IWebElement attachmentDropdown = _driver.FindElement(By.XPath("//span[@class='attachment-name']//following::button[@class='btn btn-sm btn-link no-underline dropdown-toggle']"));
            attachmentDropdown.Click();

            CommonMethods.PlayWait(3000);
            IWebElement docOption = _driver.FindElement(By.XPath(string.Format("//ul[@class='dropdown-menu no-pull']//following::li//span[text()='{0}']", documentOption)));
            docOption.Click();
        }

        #region Verify the registry title attachment is restricted
        /// <summary>
        /// Verify the registry title attachment is restricted
        /// </summary>
        /// <param name="documentName">Word/Text to mark</param>
        /// <param name="color"></param>
        public void VerifyRegistryAttachmentColor(string documentName, string color = "")
        {
            CommonMethods.PlayWait(3000);
            var element = _driver.FindElement(By.XPath(string.Format("//span[@title='{0}']", documentName)));

            CommonMethods.PlayWait(1000);
            SeleniumExtensions.DrawHighlight(element);
            var highlighTextColor = element.GetAttribute("class");

            if (highlighTextColor == "attachment - name" && color == TitleBackgroundFormat.Blue.GetStringValue())
            {
                Assert.IsTrue("attachment - name" == highlighTextColor, "Title ({0}) of the document is not restricted", documentName);
            }
            else if (highlighTextColor == "attachment-name restricted" && color == TitleBackgroundFormat.Red.GetStringValue())
            {
                Assert.IsTrue("attachment-name restricted" == highlighTextColor, "Red color ({0}) is not exits at this position in the case title", documentName);
            }
            else
            {
                Assert.Fail("The Title '{0}' verification is not successful", documentName);
            }
        }
        #endregion

        #region Get dropdown value inner text
        /// <summary>
        /// Get dropdown value inner text
        /// </summary>
        /// <param name="dropdownName"></param>
        public void VerifyDropdownValue(string dropdownName, string dropdownValue)
        {
            CommonMethods.PlayWait(2000);
            _driver.VerifyDropDownValue(dropdownName, dropdownValue);
        }
        #endregion

        #region Click on Save/Cancel button
        /// <summary>
        /// Click on Save/Cancel button
        /// </summary>
        /// <param name="button"></param>
        public void ClickOnButton(string button)
        {
            CommonMethods.PlayWait(2000);
            if(button == "save")
            {
                _driver.ClickOnButton(button);
            }
            else if(button =="cancel")
            {
                _driver.ClickOnElement(DocumentPageLocators.DocCancleButton);
            }
        }
        #endregion

        #region Verify PDF Contents

        #region GetPDFContents
        /// <summary>
        /// Gets the pdf contents from file
        /// </summary>
        /// <returns>PDF contents as text</returns>
        public string GetPDFContents()
        {
            CommonMethods.PlayWait(Convert.ToInt32(GlobalVariables.WaitTimeForPDFToDownload));
            object pdfFilePath = string.Empty;
            try
            {
                string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(pathUser, "Downloads");
                DirectoryInfo directoryInfo = new DirectoryInfo(pathDownload);

                var currentUrL = _driver.Url.ToString();

                var registryNumber = string.Empty;
                for (var i = 0; i < currentUrL.Split('/').Count(); i++)
                {
                    var currentitem = currentUrL.Split('/')[i];
                    if (currentitem.StartsWith("registryEnt"))
                    {
                        registryNumber = currentUrL.Split('/')[i + 1];
                        break;
                    }
                }

                pdfFilePath = GetNewestPDFFile(directoryInfo, string.Format("merged_documents_{0}", registryNumber)).FullName;
            }
            catch { CommonMethods.ThrowExceptionAndBreakTC("PDF file path is not correct"); }

            return ReadPDF(pdfFilePath.ToString());
        }
        #endregion

        #region ReadPDF from file
        /// <summary>
        /// Reads the contents from PDF file specified in the file path
        /// </summary>
        /// <param name="filePath">PDF file path to read the contents</param>
        /// <returns>String text</returns>
        private string ReadPDF(string filePath)
        {
            PDDocument doc = null;

            try
            {
                doc = PDDocument.load(filePath);
                PDFTextStripper stripper = new PDFTextStripper();
                return stripper.getText(doc);
            }
            finally
            {
                if (doc != null)
                {
                    doc.close();
                }
            }
        }
        #endregion

        #region Get the latest downloaded file
        /// <summary>
        /// Get the latest downloaded file
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public FileInfo GetNewestPDFFile(DirectoryInfo directory, string fileName)
        {
            return directory.GetFiles("*", SearchOption.AllDirectories)
                .OrderByDescending(f => (f == null ? DateTime.MinValue : f.LastWriteTime))
                .Where(f => f.Extension == ".pdf").FirstOrDefault(f => f.FullName.Contains(fileName));
        }
        #endregion

        #region Get the Document tile
        /// <summary>
        /// Get the remark title
        /// </summary>
        /// <returns></returns>
        public string GetDocumentTitle()
        {
            CommonMethods.PlayWait(3000);
            SeleniumExtensions.DrawHighlight(_driver.FindElement(DocumentPageLocators.DocumentTitleRestricted));
            var element = _driver.FindElement(DocumentPageLocators.DocumentTitleRestricted).Text;
            return element;
        }
        #endregion

        #endregion

        #region Save Word Document
        /// <summary>
        /// Save Word Document the opened word document
        /// </summary>
        /// <param name="registryEntryTitle"></param>
        public void SaveWordDocument(string registryEntryTitle = "")
        {
            //Running .exe files hence using Explicitly wait
            CommonMethods.PlayWait(10000);

            //Activate - So that next set of actions happen on this window
            if (string.IsNullOrEmpty(registryEntryTitle))
                AutoItX.WinActivate("[CLASS:OpusApp]", "");
            else
                AutoItX.WinActivate(registryEntryTitle + "  - Word", "");

            AutoItX.Send("^s");
        }
        #endregion

        #region Copy the contents of opened Word
        /// <summary>
        /// Copy the contents of opened Word
        /// </summary>
        public void CopyContentsOfOpenedWordDoc()
        {
            CommonMethods.PlayWait(5000);
            AutoItX.WinActivate("[CLASS:OpusApp]", "");
            AutoItX.Send("^a");
            AutoItX.Send("^c");
            //Get the contents in the clipboard
            CommonMethods.PlayWait(3000);
        }
        #endregion

        #region Close PDF
        /// <summary>
        /// Close PDF
        /// </summary>
        public void ClosePDF()
        {
            CommonMethods.PlayWait(5000);
            
            //AutoItX.WinWaitActive("[CLASS:AcrobatSDIWindow]");
            AutoItX.WinActivate("[CLASS:AcrobatSDIWindow]");
            AutoItX.WinClose("[CLASS:AcrobatSDIWindow]");

            AutoItX.WinActivate("Adobe Acrobat Reader DC");
            AutoItX.WinClose("Adobe Acrobat Reader DC");
            CommonMethods.PlayWait(3000);
        }
        #endregion

        #region Add Message For Document
        /// <summary>
        /// Add Message For Document
        /// </summary>
        /// <param name="messageContent"></param>
        public void AddMessageForDocument(string messageContent)
        {
            CommonMethods.PlayWait(3000);
            IWebElement docMessage = _driver.FindElement(DocumentPageLocators.Message);
            _driver.MouseHoverClickAndSendKeys(docMessage, messageContent);
        }
        #endregion

        #region Verify Document Or Message Is Converted To PDF
        /// <summary>
        /// Verify Document Or Message Is Converted To PDF
        /// </summary>
        public void VerifyDocumentOrMessageIsConvertedToPDF()
        {
            CommonMethods.PlayWait(3000);
            _driver.ClickOnElement(DocumentPageLocators.DocumentVersions);

            IWebElement messageFormat = _driver.FindElement(DocumentPageLocators.MessageFormat);
            messageFormat.DrawHighlight();
            string format = messageFormat.Text;
            Assert.AreEqual(format, "Arkivformat","The Document is not properly converted into PDF format");

            IWebElement pdfImage = _driver.FindElement(DocumentPageLocators.PDFImage);
            pdfImage.DrawHighlight();
            bool isPdfIconDisplayed = pdfImage.Displayed;
            Assert.IsTrue(isPdfIconDisplayed, "PDF icon is not exists");
        }
        #endregion
    }
}
