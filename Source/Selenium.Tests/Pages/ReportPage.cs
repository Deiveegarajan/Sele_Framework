using AutoIt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using System.Linq;
using System.Windows.Forms;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;
using Keys = OpenQA.Selenium.Keys;

namespace Selenium.Tests.Pages
{
    public class ReportPage
    {
        public static class ReportPageLocators
        {
            public static readonly By ReportsButton = By.XPath("//ul[@class='nav nav-tabs search-tabs']//li[6]//span[text()='Reports']");
            //ul[@class='nav nav-tabs search-tabs']//li[6]//span[text()='Reports']
        }

        private readonly RemoteWebDriver _driver;

        /// <summary>
        /// constructor
        /// Edit document
        /// </summary >
        /// <param name="driver"></param>
        public ReportPage(RemoteWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static ReportPage Connect(RemoteWebDriver driver)
        {
            return new ReportPage(driver);
        }

        #region Open Jounaler Documents
        /// <summary>
        /// Open Jounaler Documents
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="reportName"></param>
        public void ClickReportByTypeAndName(string reportType, string reportName)
        {
            CommonMethods.PlayWait(2000);
            DashboardPage dashboardPage = new DashboardPage(_driver);
            // Expand Left menu
            if (dashboardPage.IsMenuControlCollapsed())
                dashboardPage.ExpandMenu();
            CommonMethods.PlayWait(2000);

            _driver.ClickOnElement(ReportPageLocators.ReportsButton);
            CommonMethods.PlayWait(2000);
            _driver.ClickOnButton(reportType);
            CommonMethods.PlayWait(2000);
            var reportNameElement = _driver.FindElements(By.XPath(string.Format("//span[text()='{0}']", reportName)));
            if (reportNameElement.Any() && reportNameElement.FirstOrDefault().Displayed)
            { _driver.ClickOnButton(reportName); }
            else
            {
                //Open the reportname menu
                _driver.ClickOnButton(reportType);
                _driver.ClickOnButton(reportName);
            }
            CommonMethods.PlayWait(3000);
        }
        #endregion

        #region Remove Multiple Option And Select Required From Dropdown
        /// <summary>
        /// Remove Multiple Option And Select Required From Dropdown
        /// </summary>
        /// <param name="dropdownName"></param>
        /// <param name="dropdownValue"></param>
        public void RemoveMultipleOptionAndSelectRequiredFromDropdown(string dropdownName, string dropdownValue)
        {
            _driver.RemoveMultipleOptionAndSelectRequiredFromDropdown(dropdownName, dropdownValue);
        }
        #endregion

        #region Report Verification
        /// <summary>
        /// Document Verification
        /// </summary>
        /// <param name="reportFormat"></param>
        /// <param name="reportName"></param>
        /// <param name="caseTitle"></param>
        /// <param name="registryEntryTile"></param>
        /// <param name="screenedRegistryTitleFull"></param>
        public void ReportVerification(string reportFormat, string reportName, string caseTitle, string registryEntryTile, string screenedRegistryTitleFull="")
        {
            try
            {
                string clipboardTextPDF = string.Empty;
                Clipboard.Clear();
                if (reportFormat == ReportFormat.PDF.GetStringValue() || reportFormat == ReportFormat.Text.GetStringValue() || reportFormat == ReportFormat.HTML.GetStringValue())
                {
                    var tabs = _driver.WindowHandles;
                    _driver.SwitchTo().Window(tabs[1]);

                    var databaseName = FrontPage.Elements.DatabaseName;
                    AutoItX.WinActivate("Elements - " + databaseName + " - Google Chrome", "");

                    CommonMethods.PlayWait(5000);

                    AutoItX.MouseClick();
                    AutoItX.Send("{ENTER}");
                    AutoItX.Send("^a");
                    AutoItX.Send("^c");

                    clipboardTextPDF = Clipboard.GetText();
                }
                if (reportFormat == ReportFormat.Word.GetStringValue())
                {

                }

                VerifyContents(reportFormat, reportName, caseTitle, registryEntryTile, screenedRegistryTitleFull, clipboardTextPDF);
            }
            catch (System.Exception ex)
            {
                var exception = ex;
            }

        }

        /// <summary>
        /// Verify the Text in multiple report formats
        /// </summary>
        /// <param name="reportFormat"></param>
        /// <param name="reportName"></param>
        /// <param name="caseTitle"></param>
        /// <param name="registryEntryTile"></param>
        /// <param name="screenedRegistryTitleFull"></param>
        /// <param name="clipboardTextPDF"></param>
        private void VerifyContents(string reportFormat, string reportName, string caseTitle, string registryEntryTile, string screenedRegistryTitleFull, string clipboardTextPDF)
        {
            if (!string.IsNullOrEmpty(reportName))
            {
                StringAssert.Contains(clipboardTextPDF, reportName, string.Format("Report Name: {0} is not available in the {1}", reportName, ReportFormat.PDF.GetStringValue()));
            }
            StringAssert.Contains(clipboardTextPDF, caseTitle, string.Format("Case Title is not matching in {0} for {1}", reportFormat, reportName));
            StringAssert.Contains(clipboardTextPDF, screenedRegistryTitleFull, string.Format("Screened(****) Registry Title is not matching in {0} for {1}", reportFormat, reportName));
            Assert.IsFalse(clipboardTextPDF.Contains(registryEntryTile), string.Format("Registry Title is not matching in {0} for {1}", reportFormat, reportName));
        }
        #endregion

        #region Report Verification
        /// <summary>
        /// Close current browser tab
        /// </summary>
        public void CloseCurrentBrowserTab()
        {
            var tabs = _driver.WindowHandles;
            //below code will switch to new tab
            _driver.SwitchTo().Window(tabs[1]);
            _driver.Close();
            //Switch back to your original tab
            _driver.SwitchTo().Window(tabs[0]);
        }
        #endregion

        #region Search Criteria for report
        /// <summary>
        /// Search Criteria for report
        /// </summary>
        /// <param name="reportFormat"></param>
        /// <param name="documentType"></param>
        /// <param name="status"></param>
        /// <param name="recordDate"></param>
        public void ReportPopUpFileFormats(string reportFormat, string documentType, string status, string recordDate= "0")
        {
            CasePage casePage = new CasePage(_driver);

            var reportFormatCheckbox = _driver.FindElement(By.XPath(string.Format("//label[text()='{0}']", reportFormat)));
            reportFormatCheckbox.DrawHighlight();
            reportFormatCheckbox.Click();
            _driver.SendTextToTextBoxField("Record date", recordDate);
            RemoveMultipleOptionAndSelectRequiredFromDropdown("Document type", documentType);
            RemoveMultipleOptionAndSelectRequiredFromDropdown("Status", status);
            casePage.ClickOnSearchButton();
        }
        #endregion
    }
}
