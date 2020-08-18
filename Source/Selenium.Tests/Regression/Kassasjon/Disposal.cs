using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.Kassasjon
{
    [TestClass]
    [TestCategory("Kassasjon (Disposal)")]
    public class Disposal : SeleniumTestBase
    {
        [TestMethod]
        public void InheritDisposalCodeFromCaseToReg()
        {
            //TC#19026

            #region ---- Variable Declaration ----

            #region Variables
            string PreservationTimeLabel = "Preservation time";
            string disposalCodeLabel = "Disposal code";
            string preservationTime10 = "10";
            string preservationTime6 = "6";
            string disposalCodeValue = "Kasseres";
            string classificationCode = "221";
            string caseTitle1 = "Inherit disposal code from case to registry entry - Case 1";
            string caseTitle2 = "Inherit disposal code from case to registry entry - Case 2";
            string registryEntryTitle = "RegEntry";
            #endregion

            #endregion

            #region ---- Systemadministrasjon Module ----
            Selenium_Run((driver) =>
            {
                #region Instanstiation
                var logoutPage = LogoutPage.Connect(driver);
                var adminPage = AdminstratorPage.Connect(driver);
                #endregion

                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Arkivstruktur-->Arkivdeler--> Arkivdeler (all) And Click PA1
                adminPage.NavigateToMenuItem("Arkivstruktur", "Arkivdeler", "Arkivdeler (alle)");
                CommonMethods.PlayWait(2000);
                adminPage.ClickItemInGrid("PA1");
                #endregion

                #region Update Disposal Code & Preservation Time 10 yrs for the PA1
                CommonMethods.PlayWait(8000);
                driver.SendTextToTextBoxField(PreservationTimeLabel, preservationTime10);
                adminPage.SelectKendoDropdownAndAddValue(disposalCodeLabel, disposalCodeValue);
                adminPage.Save();
                #endregion

                #region Verify disposal code and preservation time 10 are updated
                Assert.IsTrue(adminPage.GetPreservationTime() == preservationTime10, string.Format("Preservation Time {0} is not updated in Admin Module", preservationTime10));
                Assert.IsTrue(adminPage.GetDisposalCode() == disposalCodeValue, string.Format("Disposal Code {0} is not updated in Admin Module", disposalCodeValue));
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
                var adminPage = AdminstratorPage.Connect(driver);
                var registryPage = RegistryEntryPage.Connect(driver);
                var dashboardPage = DashboardPage.Connect(driver);
                var logoutPage = LogoutPage.Connect(driver);
                #endregion

                #region Login to Saksbehandling
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Change Role
                dashboardPage.ChangeRole(RolesInApplication.MainRegistrar);
                #endregion

                #region Create Case
                casePage.AddTitle(caseTitle1);
                casePage.SelectKendoDropdownAndAddValue("Record section", "Personalarkiv - FNR");
                casePage.SaveCase();
                #endregion

                #region Edit case and verify Disposal details
                casePage.EditCase();
                Assert.IsTrue(casePage.GetPreservationTime() == preservationTime10, string.Format("Preservation Time {0} is not updated in Case", preservationTime10));
                Assert.IsTrue(casePage.GetDisposalCode() == disposalCodeValue, string.Format("Disposal Code {0} is not updated in Case", disposalCodeValue));
                #endregion

                #region Add Registry Entry
                casePage.ClickCancelEditCaseTitleButton();
                registryPage.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                registryPage.AddRegTitle(registryEntryTitle);
                registryPage.AddRegToAddress("AA");
                registryPage.ClickSaveBttn();
                #endregion

                #region Edit Registry Entry And Verify Disposal
                registryPage.ClickEditButton();
                CommonMethods.PlayWait(4000);
                Assert.IsTrue(registryPage.GetPreservationTime() == preservationTime10, string.Format("Preservation Time {0} is not updated in Registry Entry", preservationTime10));
                Assert.IsTrue(registryPage.GetDisposalCode() == disposalCodeValue, string.Format("Disposal Code {0} is not updated in Registry Entry", disposalCodeValue));
                #endregion

                #region Logout from Application
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion

            #region ---- Systemadministrasjon Module ----
            Selenium_Run((driver) =>
            {
                #region Instanstiation
                var logoutPage = LogoutPage.Connect(driver);
                var adminPage = AdminstratorPage.Connect(driver);
                #endregion

                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Arkivstruktur-->Ordningsverdi And Click Serach ordningsverdi as "221"
                adminPage.NavigateToMenuItem("Arkivstruktur", "Ordningsverdi", "Søk");
                CommonMethods.PlayWait(2000);
                adminPage.SetTextBoxValueInSearchPopup("Principles for order values", "EMNE", true);
                adminPage.SetTextBoxValueInSearchPopup("Order values", classificationCode);
                adminPage.ClickOnButton("Search");
                CommonMethods.PlayWait(5000);
                adminPage.ClickItemInGrid(classificationCode);
                #endregion

                #region Update Disposal Code & Preservation Time 10 yrs for the PA1
                CommonMethods.PlayWait(8000);
                driver.SendTextToTextBoxField(PreservationTimeLabel, preservationTime6);
                adminPage.SelectKendoDropdownAndAddValue(disposalCodeLabel, disposalCodeValue);
                adminPage.Save();
                #endregion

                #region Verify disposal code and preservation time 10 are updated
                Assert.IsTrue(adminPage.GetPreservationTime() == preservationTime6, string.Format("Preservation Time {0} is not updated in Admin Module", preservationTime6));
                Assert.IsTrue(adminPage.GetDisposalCode() == disposalCodeValue, string.Format("Disposal Code {0} is not updated in Admin Module", disposalCodeValue));
                #endregion
            });
            #endregion

            #region ---- Saksbehandling Module ----

            Selenium_Run((driver) =>
            {
                #region Instantiation
                var casePage = CasePage.Connect(driver);
                var adminPage = AdminstratorPage.Connect(driver);
                var registryPage = RegistryEntryPage.Connect(driver);
                var dashboardPage = DashboardPage.Connect(driver);
                var logoutPage = LogoutPage.Connect(driver);
                #endregion

                #region Login to Saksbehandling
                FrontPage.LoginToApplication(driver);
                #endregion

                #region Change Role
                dashboardPage.ChangeRole(RolesInApplication.MainRegistrar);
                #endregion

                #region Create Case
                casePage.CreateCaseWithClassificationCode(caseTitle2, classificationCode);
                #endregion

                #region Edit case and verify Disposal details
                casePage.EditCase();
                Assert.IsTrue(casePage.GetPreservationTime() == preservationTime6, string.Format("Preservation Time {0} is not updated in Case", preservationTime6));
                Assert.IsTrue(casePage.GetDisposalCode() == disposalCodeValue, string.Format("Disposal Code {0} is not updated in Case", disposalCodeValue));
                #endregion

                #region Add Registry Entry
                casePage.ClickCancelEditCaseTitleButton();
                registryPage.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                registryPage.AddRegTitle(registryEntryTitle);
                registryPage.AddRegToAddress("AA");
                registryPage.ClickSaveBttn();
                #endregion

                #region Edit Registry Entry And Verify Disposal
                registryPage.ClickEditButton();
                CommonMethods.PlayWait(4000);
                Assert.IsTrue(registryPage.GetPreservationTime() == preservationTime6, string.Format("Preservation Time {0} is not updated in Registry Entry", preservationTime6));
                Assert.IsTrue(registryPage.GetDisposalCode() == disposalCodeValue, string.Format("Disposal Code {0} is not updated in Registry Entry", disposalCodeValue));
                #endregion

                #region Logout from Application
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion
        }
    }
}
