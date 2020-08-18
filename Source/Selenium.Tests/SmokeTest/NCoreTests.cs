using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Pages;

namespace Selenium.Tests.SmokeTest
{
	[TestClass]
	[TestCategory("SmokeTest")]
	public partial class NCoreTests : SeleniumTestBase
	{
		[TestMethod]
		public void OpenNCore()
		{
			Run((driver) =>
			{
				var frontPage = NCoreFrontPage.Connect(driver);

				//driver.Close();
				Assert.AreEqual("Ephorte nCore services", frontPage.GetHeaderText());
			});
		}

		[TestMethod]
		public void CanOpenServiceEndpoints()
		{
			Run((driver) =>
			{
				var page = NCoreFrontPage.Connect(driver);

				var serviceHrefs = page.GetAllServiceHrefs().ToList();
				Assert.AreNotEqual(0, serviceHrefs.Count());
			});
		}

		[TestMethod]
		public void VerifyAllServiceHrefs()
		{
			Run((driver) =>
			{
				var page = NCoreFrontPage.Connect(driver);
				driver.AddScreenshotToTestContext("FirstPage");
				var url = driver.Url;

				var serviceHrefs = page.GetAllServiceHrefs().Select(href => href.GetAttribute("href")).ToList();

				foreach (var href in serviceHrefs)
				{
					driver.Navigate().GoToUrl(href);
					var element = driver.WaitForElementVisible(By.Id("content"));
					driver.AddScreenshotToTestContext("href");
					Console.WriteLine(element.Text);
				}
			});
		}


		protected void Run(Action<RemoteWebDriver> action)
		{
			Run(action, "ncore");
		}
	}
}
