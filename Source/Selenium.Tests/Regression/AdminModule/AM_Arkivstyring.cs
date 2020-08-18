using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;
using static Selenium.Tests.Base.Selenium.Core.GlobalEnum;

namespace Selenium.Tests.Regression.AdminModule
{
    [TestClass]
    [TestCategory("AdminModule")]
    public partial class Arkivstyring : SeleniumTestBase
    {
        [TestMethod]
        public void AddEditDeleteAdressegruppe()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Adressegrupper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Adressegrupper", "Adressegrupper (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Shortname", "AT");
                adminPage.SetTextBox("Name", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("AT");
                adminPage.EditDescription("AT", "Name", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteAdresser()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Adresser and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Adresser", "Adresser (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Name", "Automation Testing");
                adminPage.SetTextBox("Shortname", "AT");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("Automation Testing", "Name", "Edit_Automation Testing", Description.TextBox, false);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteAdresseType()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Adressetyper and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Adressetype", "Adressetyper (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Contacts", "AT");
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
        public void AddEditDeleteAvgraderingskode()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Avgraderingskode and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Avgraderingskode", "Avgraderingskode (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Depreciation code", "GD");
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
        public void AddEditDeleteAvskrivingsmate()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Avskrivingsmåte and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring");
                adminPage.NavigateToMenuItem("Avskrivingsmåte", "Avskrivingsmåte");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Mode of depreciation", "GDR");
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
        public void AddEditDeleteDokumenttype()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Dokumenttype and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Dokumenttype", "Dokumenttype (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Document type", "D");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("D", "Description", "Edit_Automation Testing", Description.TextBox, false);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteForsendelsesmate()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Forsendelsesmåte and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Forsendelsesmåte", "Forsendelsesmåte (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Dispatch method", "AT");
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
        public void AddEditDeleteForsendelsesstaus()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Forsendelsesstaus and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Forsendelsesstaus", "Forsendelsesstaus (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Dispatch status", "GD");
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
        public void AddEditDeleteInformasjonstype()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Informasjonstype and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Informasjonstype", "Informasjonstype (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Information type", "AT");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.SetTextBox("Prompt 1", "GDR");
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
        public void AddEditDeleteKassasjonstype()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Kassasjonskode and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Kassasjonskode", "Kassasjonskode (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Disposal code", "GD");
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
        public void AddEditDeleteLovForskrift()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Lov/forskrift - presendes and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Lov/forskrift - presendes", "Lov/forskrift (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Shortname", "AT");
                adminPage.SetTextBox("Law/requirements", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("AT");
                adminPage.EditDescription("AT", "Law/requirements", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteProsjektregister()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Prosjektregister and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Prosjektregister", "Prosjektregister (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Project ID", "GD");
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
        public void AddEditDeleteSakspartrolle()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Informasjonstype and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring");
                adminPage.NavigateToMenuItem("Sakspartrolle", "Sakspartrolle");
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
        public void AddEditDeleteTilleggskoder()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Tilleggskoder and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Tilleggskoder", "Tilleggskoder (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Ad. Code", "5");
                adminPage.SetTextBox("Description", "Automation Testing");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.VerifyDesctiptionIsExist("5");
                adminPage.EditDescription("5", "Description", "Edit_Automation Testing", Description.TextBox);
                adminPage.ClickDeleteAndConfirmDelete();

                #region Logout from Admin Module
                LogoutPage logoutPage = LogoutPage.Connect(driver);
                logoutPage.LogoutApplication();
                #endregion
                #endregion
            });
        }

        [TestMethod]
        public void AddEditDeleteStoppord()
        {
            Selenium_Run((driver) =>
            {
                #region Login to Admin Module
                FrontPage.LoginToApplication(driver, ApplicationModules.Administrator.GetStringValue());
                #endregion

                #region Navigate to Stoppord and add, save and delete
                AdminstratorPage adminPage = AdminstratorPage.Connect(driver);
                adminPage.NavigateToMenuItem("Arkivstyring", "Stoppord stikkordsindeksering", "Stoppord (alle)");
                adminPage.ClickOnButton("New");
                adminPage.SetTextBox("Word", "AutomationTesting");
                adminPage.Save();
                driver.HandleErrorPopUpAndThrowErrorMessage();
                adminPage.EditDescription("AutomationTesting", "Word", "EditAutomationTesting", Description.TextBox);
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
    

   
