using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Pages;

namespace Selenium.Tests.Regression.Case
{
    [TestClass]
    [TestCategory("WorkFlowTest")]
    public partial class WorkFlow : SeleniumTestBase
    {
        [TestMethod]
        public void WorkFlowTests()
        {
            Run((driver) =>
            {
                #region Logon Elements
                var frontPage = FrontPage.Connect(driver);

                var loginWindow = frontPage.OpenLogin();
                loginWindow.Login("guilt", "guilt");
                loginWindow.SelectModule();
                #endregion

                #region Change Role
                DashboardPage dashboardPage = new DashboardPage(driver);
                dashboardPage.ChangeRole(GlobalEnum.RolesInApplication.MainRegistrar);
                #endregion

                #region Create new Case
                CasePage casePage = new CasePage(driver);
                casePage.CreateCase("Case workflow with two users involved: ad-hoc:07");
                #endregion

                #region Work Flow 
                WorkFlowPage workFlowPage = new WorkFlowPage(driver);
                workFlowPage.CreateWorkFlow("P","New Work Flow","case new work flow", "Parallell behandling", "28.08.2019");

                workFlowPage.AddWorkFlowSubTask("sjekkpunkt", "Aktivisert","New Work Flow Sub Task", "Sub task for new work flow", "Parallell behandling", "03.09.2019");
                workFlowPage.VerifySubTaskCreated("New Work Flow Sub Task");

                workFlowPage.ChooseSubTask("ODS", "Aktivisert", "ODS Work Flow Sub Task", "ODS Active status Sub task for new work flow", "Parallell behandling", "03.09.2019");
                workFlowPage.VerifyChooseSubTaskTypeCreated("ODS Work Flow Sub Task");

                workFlowPage.OutgoingRegistryEntryDocument("ODS document title", "Utgående post/Outbound", "Standard brev", "Reservert");
                #endregion

                #region Logout from Application
                LogoutPage logoutPage = new LogoutPage(driver);
                logoutPage.LogoutApplication();
                #endregion
            });
        }
    }
}
