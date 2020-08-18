using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.AdminModule
{
    [TestClass]
    [TestCategory("AdminModule")]
    public partial class SGK_Funksjoner : SeleniumTestBase
    {
        [TestMethod]
        public void AddEditDeleteAktivitetstyper()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Vedtaksstatus and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Aktivitetstyper", "Aktivitetstyper (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Short code for task type", "AT");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("AT");
                adminPage.EditDescription("AT", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteArbeidsflytmaler()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Change Role to Secretary of Boards - Eng
                DashboardPage dashboardPage = DashboardPage.Connect(driver);
                CommonMethods.PlayWait(2000);
                dashboardPage.ChangeRole(RolesInApplication.MainRegistrar);
                #endregion

                #region Navigate to Maler (alle) and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Arbeidsflytmaler", "Maler (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SelectHtmlComboBox("Object type", "Case workflow");
                adminPage.SetTextBox("Title", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("Automation Testing");
                adminPage.EditDescription("Automation Testing", "Title", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteBehandlingsform()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Behandlingsform and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Behandlingsform", "Behandlingsform (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Processing type", "GDR");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("GDR");
                adminPage.EditDescription("GDR", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteBeslutningskoder()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Beslutningskoder and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Beslutningskoder", "Beslutningskoder (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Decision code", "AT");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("AT");
                adminPage.EditDescription("AT", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteFraser()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Fraser and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Fraser", "Fraser (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Phrase", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("Automation Testing");
                adminPage.EditDescription("Automation Testing", "Phrase", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }
   
        [TestMethod]
        public void AddEditDeleteImportsentraler()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Importsentraler and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Importsentral", "Importsentraler (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SelectHtmlComboBox("Type", "IMAP");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.SelectHtmlComboBox("Person", "EPHORTE");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("Automation Testing", "Description", "Edit_Automation Testing", Description.TextBox,false);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteRapportkategorier()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Rapportkategorier and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Rapportkategorier", "Rapportkategorier (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "AT");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                adminPage.VerifyDesctiptionIsExist("AT");
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("AT", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteKundefunksjoner()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Kundefunksjoner and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Kundefunksjoner", "Kundefunksjoner (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SelectHtmlComboBox("Object type", "Case");
                adminPage.SetTextBox("Order", "1");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.SetTextBox("Function", "AT");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("AT", "Description", "Edit_Automation Testing", Description.TextBox,false);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteLenketyper()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Lenketyper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Lenketyper", "Lenketyper (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "AT");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("AT");
                adminPage.EditDescription("AT", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();
            
                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteSaksmappetyper()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Saksmappetyper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Saksmappetyper", "Saksmappetyper (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "AT");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("AT");
                adminPage.EditDescription("AT", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }
        [TestMethod]
        public void AddEditDeleteSkjematyper()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Fraser and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Skjematyper", "Skjematyper (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Schema name", "Name 1000");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.SetTextBox("Schema template", "Automation Schema template");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("Automation Testing");
                adminPage.EditDescription("Automation Testing", "Description", "Edit_Automation Testing", Description.TextBox,false);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteTilleggsattributter()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Tilleggsattributter and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Tilleggsattributter", "Tilleggsattributter (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "AT");
                adminPage.SetTextBox("No.", "10");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.SetTextBox("Caption", "Tilleggsattributter Captions ");
                adminPage.SelectHtmlComboBox("Field type", "Text box");
                adminPage.SelectHtmlComboBox("Type", "Note");
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
        public void AddEditDeleteVarslingsform()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Varslingsform and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Varslingsform", "Varslingsform (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Code", "GDR");
                adminPage.SetTextBox("Notification type", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("GDR");
                adminPage.EditDescription("Automation Testing", "Notification type", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteLogging()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Logging and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("SGK-funksjoner", "Logging", "Logging (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SelectHtmlComboBox("Table name", "ADMINDEL");
                adminPage.SetTextBox("Text", "Automation Testing");
                adminPage.SetTextBox("Being kept", "10");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("ADMINDEL", "Text", "Edit_Automation Testing", Description.TextBox, false);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }
    }
} 
    

   
