using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.SmokeTest
{
	[TestClass]
	[TestCategory("SeleniumSmokeTest")]
	public partial class SeleniumSmokeTests : SeleniumTestBase
	{
		[TestMethod]
        public void ConvertDocumentIntoPDF()
        {
            #region Private Variables
            var caseId = string.Empty;
            var caseTitle = "Case to Convert the document into PDF";
            var registryEntryTitle = "Registry Entry";
            #endregion

            Selenium_Run((driver) =>
            {
                FrontPage.LoginToApplication(driver);

                #region Create case with access code
                CasePage casePage = CasePage.Connect(driver);
                casePage.AddTitle(caseTitle);
                casePage.SaveCase();
                CommonMethods.PlayWait(5000);
                caseId = casePage.GetCaseNumber();
                #endregion

                #region Add U-type Registry Entry 
                RegistryEntryPage regEntry = RegistryEntryPage.Connect(driver);
                regEntry.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                regEntry.AddRegTitle(registryEntryTitle);
                regEntry.AddRegToAddress("AA");
                DocumentPage documentPage = DocumentPage.Connect(driver);
                documentPage.ClickAndAddDocTemplate(AttachmentType.Message.GetStringValue());
                documentPage.AddMessageForDocument("Convert the message into PDF");
                regEntry.ClickSaveBttn();
                #endregion

                #region Change registry entry status to F/done
                regEntry.ClickEditButton();
                regEntry.ChangeREStatusToFerdigOrDone();
                regEntry.ClickSaveBttn();
                #endregion

                #region Convert document to PDF from Attachment/Message
                documentPage.ViewAttachmentInformation("Registry Entry", "Convert to PDF", 1);
                CommonMethods.PlayWait(3000);
                CommonMethods.RefreshBrowserWindow(driver);
                CommonMethods.RefreshBrowserWindow(driver);
                CommonMethods.PlayWait(15000);
                #endregion

                #region Verify the Document is Converted to PDF
                documentPage.VerifyDocumentOrMessageIsConvertedToPDF();
                #endregion

                #region Logout from RM Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
        }
    }
}
