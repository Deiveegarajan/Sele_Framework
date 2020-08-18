using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using System;

namespace Selenium.Tests
{
    public class SeleniumTestBase
	{
		public TestContext TestContext { get; set; }

		protected void Run(Action<RemoteWebDriver> action, string location = "elements")
		{
            //const string defaultUrl = "https://master.elementscloud.no";
            //const string defaultUrl = "https://apps.elementscloud.no";
            const string defaultUrl = "https://cloud.elements-ecm.no";

            var webSiteRootUrl = Environment.GetEnvironmentVariable("WebSiteRootUrl") ?? defaultUrl;

			Driver.Run(TestContext, $"{webSiteRootUrl}/{location}", action);
		}

        protected void Selenium_Run(Action<RemoteWebDriver> action)
        {
            //const string defaultUrl = "https://master.elementscloud.no/elements";
            //const string defaultUrl = "https://apps.elementscloud.no/elements";
            //const string defaultUrl = "https://helperservice.elements-ecm.no/Login";
            // const string defaultUrl = "https://services.elements-ecm.no:8085/Elements/";
            //const string defaultUrl = "https://services.elements-ecm.no:8085/ElementsRedirect?username=a@elementscloud.no";
            //const string defaultUrl = "https://gvs-elements.gecko.no/master";
            //const string defaultUrl = "https://app01v19-3.elements-ecm.no/elements";

            const string defaultUrl = "https://services.elements-ecm.no:8085/ElementsRedirect?username=a@elementsecm.no";
            //const string defaultUrl = "https://svc01master.elements-ecm.no/ElementsRedirect?username=a@tests.no";


             
            GlobalVariables.SiteURL = defaultUrl;

            var webSiteRootUrl = Environment.GetEnvironmentVariable("WebSiteRootUrl") ?? defaultUrl;
            Driver.Run(TestContext, $"{webSiteRootUrl}", action);
        }
    }
}
