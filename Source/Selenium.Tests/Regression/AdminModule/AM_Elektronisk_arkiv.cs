using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.AdminModule
{
    [TestClass]
    [TestCategory("AdminModule")]
    public partial class Elektronisk_arkiv : SeleniumTestBase
    {
        [TestMethod]
        public void AddEditDeleteDokumentkategori()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Dokumentkategori and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Elektronisk arkiv", "Dokumentkategori", "Dokumentkategori (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "AT");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("AT", "Description", "Edit_Automation Testing", Description.TextBox, false);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteDokumentmaltyper()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Dokumentmaltyper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Elektronisk arkiv", "Dokumentmaltyper", "Dokumentmaltyper  (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("Automation Testing", "Description", "Edit_Automation Testing", Description.TextBox, false);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteDokumentstatus()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Dokumentstatus and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Elektronisk arkiv", "Dokumentstatus", "Dokumentstatus (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "GD");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("GD");
                adminPage.EditDescription("GD", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteDokumenttilknytning()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Dokumenttilknytning and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Elektronisk arkiv", "Dokumenttilknytning", "Dokumenttilknytning (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Linking code", "GD");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("GD");
                adminPage.EditDescription("GD", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteLagringsenhet()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Lagringsenhet and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Elektronisk arkiv", "Lagringsenhet", "Lagringsenhet (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Storage unit", "GDR");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("GDR", "Description", "Edit_Automation Testing", Description.TextBox, false);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteLagringsformat()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Lagringsformat and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Elektronisk arkiv", "Lagringsformat", "Lagringsformat  (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Storage media", "GD");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("GD");
                adminPage.EditDescription("GD", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteVariantformat()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Variantformat and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Elektronisk arkiv", "Variantformat", "Variantformat (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "GD");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("GD");
                adminPage.EditDescription("GD", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void InheritDisposalCodeFromRegToDocument()
        {
            #region Variables
            string PreservationTimeLabel = "Preservation time";
            string preservationTime10 = "10";
            string dropdownValue = "Kasseres";
            string caseTitle = "NewCase";
            string registryEntryTitle = "RegEntry";
            #endregion

            /*

                        #region Systemadministrasjon Module
                        Selenium_Run((driver) =>
                        {
                            #region Instanstiation
                            var logoutPage = LogoutPage.Connect(driver);
                            var adminPage = AdminstratorPage.Connect(driver);
                            #endregion

                            #region Login to Admin Module
                            FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                            #endregion

                            #region Navigate to Dokumentkategori 
                            AdminstratorPage _adminPage = AdminstratorPage.Connect(driver);
                            _adminPage.NavigateToMenuItem("Elektronisk arkiv", "Dokumentkategori", "Dokumentkategori (alle)");
                          //  _adminPage.ClickOnItemInGrid("RT Automatic Test 1");
                            #endregion

                            #region Update Disposal Code & Preservation Time 10 yrs for the RT1
                            CommonMethods.PlayWait(3000);
                            adminPage.SelectHtmlComboBox("Disposal code", "K");
                            driver.SendTextToTextBoxField(PreservationTimeLabel, preservationTime10);
                            adminPage.Save();
                            #endregion

                            #region Logout from Admin Module
                            LogoutPage logout = LogoutPage.Connect(driver);
                            logout.LogoutApplication();
                            #endregion
                        });
                        #endregion
            */
            #region   Arkivansvarlig Module
            Selenium_Run((driver) =>
            {
                #region Instantiation
                var casePage = CasePage.Connect(driver);
                var adminPage = AdminstratorPage.Connect(driver);
                var registryPage = RegistryEntryPage.Connect(driver);
                var dashboardPage = DashboardPage.Connect(driver);
                var logoutPage = LogoutPage.Connect(driver);
                #endregion

                #region Login to Arkivansvarlig
                FrontPage.LoginToApplication(driver);
                #endregion
                try
                {
                    #region Change Role
                    dashboardPage.ChangeRole(RolesInApplication.MainRegistrar);
                    #endregion

                    #region Create Case
                    casePage.AddTitle(caseTitle);
                    casePage.SaveCase();
                    #endregion

                    #region Add Registry Entry
                    registryPage.CreateRegistryEntry(RegistryEntryType.OutgoingType.GetStringValue());
                    registryPage.AddRegTitle(registryEntryTitle);
                    registryPage.AddRegToAddress("AA");
                    registryPage.SelectKendoDropdownAndAddValue("Document category", "RT");
                    #endregion

                    #region attach document
                    DocumentPage document = DocumentPage.Connect(driver);
                    document.ClickAndAddDocTemplate(AttachmentType.DocumentTemplate.GetStringValue());
                    document.SelectDocument(DocumentTemplateType.Dokumentmal.GetStringValue(), DocumentSubType.Standardbrev.GetStringValue());
                    document.SaveAndEdit();
                    document.HandleAlertPopupToOpenWordDocument();
                    document.DocumentWebLoginWindow();
                    document.CloseDocument();
                    #endregion

                    #region Edit Registry Entry And Verify Disposal
                    registryPage.ClickEditButton();
                    CommonMethods.PlayWait(4000);
                    registryPage.ClickMoreArrow();
                    //Assert.IsTrue(registryPage.GetDisposalCode() == dropdownValue, string.Format("Disposal Code {0} is not updated in Registry Entry", dropdownValue));
                    //Assert.IsTrue(registryPage.GetPreservationTime() == preservationTime10, string.Format("Preservation Time {0} is not updated in Registry Entry", preservationTime10));
                    #endregion


                    #region Verify Document Details And Verify Disposal
                    document.ViewAttachmentInformation("Document details");
                    document.VerifyDropdownValue("Document category", "RT - Automatic Test 1");
                    //  registryPage.VerifyDocumentDetails("Document details");
                    Assert.IsTrue(registryPage.GetDisposalCode() == dropdownValue, string.Format("Disposal Code {0} is not updated in Registry Entry", dropdownValue));
                    Assert.IsTrue(registryPage.GetPreservationTime() == preservationTime10, string.Format("Preservation Time {0} is not updated in Registry Entry", preservationTime10));
                    #endregion

                }
                catch (System.Exception)
                {
                }
                #region Logout from Application
                logoutPage.LogoutApplication();
                #endregion
            });
            #endregion
        }
    }
    } 
    

   
