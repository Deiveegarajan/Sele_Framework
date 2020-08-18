using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.Reports
{
    [TestClass]
    [TestCategory("Reports")]
    public partial class Reports : SeleniumTestBase
    {
        [TestMethod]
        public void ReportDailyJournal_Dagens()
        {
            //TC#63787 
            #region Private Variables
            var caseId = string.Empty;
            var caseTitle = "ReportDailyJournal_Dagens";
            var registryEntryTitle = "Registry Entry";
            #endregion

            #region Login to RM module with Guilt user
            Selenium_Run((driver) =>
            {
                //Step 1

                #region Login to RM Module
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Create a case 
                CasePage casePage = CasePage.Connect(driver);
                casePage.CreateCase(caseTitle);
                CommonMethods.PlayWait(5000);
                caseId = casePage.GetCaseNumber();
                #endregion

                #region Create a Outgoing registry entry
                RegistryEntryPage registryEntry = RegistryEntryPage.Connect(driver);
                casePage.ClickOnButton("Registry entries");
                registryEntry.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                registryEntry.AddRegTitle(registryEntryTitle);
                registryEntry.AddRegToAddress("AA");
                registryEntry.ClickSaveBttn();
                #endregion

                #region Check the different reports and verify that parts of Registry entry title
                #region Journal daily Report
                #region PDF
                ReportPage reportPage = new ReportPage(driver);
                reportPage.ClickReportByTypeAndName("Journaler", "Journal (dagens)");
                reportPage.ReportPopUpFileFormats(ReportFormat.PDF.GetStringValue(), "Utgående post/Outbound", "Reservert", "0");
                CommonMethods.PlayWait(10000);
                //Step 2
                reportPage.ReportVerification(ReportFormat.PDF.GetStringValue(), "Journal", caseTitle, registryEntryTitle);
                reportPage.CloseCurrentBrowserTab();
                #endregion
                #endregion
                #endregion

                #region Logout from RM Module
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion
        }
    }
}
