using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Tests.Pages;

namespace Selenium.Tests.SmokeTest
{
	[TestClass]
	[TestCategory("SmokeTest")]
	public partial class SeleniumTests : SeleniumTestBase
	{

		[TestMethod]
		public void ShowFrontpage()
		{
			Run((driver) =>
			{
				var frontPage = FrontPage.Connect(driver);

				frontPage.WriteDbInfoToConsole();
				//driver.Close();
			});
		}

		[TestMethod]
		public void ShowFrontpageAndClickLoginOnce()
		{
			ShowFrontpageAndClickLogin(0);
		}

		//[Ignore]
		//[DataRow(1)]
		//[DataRow(2)]
		//[DataRow(3)]
		//[DataTestMethod]
		public void ShowFrontpageAndClickLogin(int iteration)
		{
			Run((driver) =>
			{
                var frontPage = FrontPage.Connect(driver);

                var loginWindow = frontPage.OpenLogin();
                loginWindow.Login("guilt", "guilt");

                Console.WriteLine($"Number of windows: {driver.WindowHandles.Count}");
			});
		}

	}
}
