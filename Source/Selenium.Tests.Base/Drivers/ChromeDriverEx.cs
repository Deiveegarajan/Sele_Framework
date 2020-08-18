using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace Selenium.Tests.Drivers
{
	class ChromeDriverEx : ChromeDriver, IGetTestContext
	{
		public static ChromeOptions GetOptions(bool headless)
		{
			var options = new ChromeOptions();
			if (headless)
			{
				options.AddArguments("headless");
			}

			options.AddArguments(
				"download.prompt_for_download",
				"disable-popup-blocking",
				"--disable-notifications",
				"disable-infobars",
				"disable-extensions",
				"chrome.switches",
				"--disable-extensions",
				"--test-type");
			return options;
		}

		public ChromeDriverEx(TestContext testContext, string chromeDriverDirectory, ChromeOptions options)
			: base(chromeDriverDirectory, options) //NOTE: Do not specify default commandTimeouts. Use explicit waiting.
		{
			TestContext = testContext;
		}

		public TestContext TestContext { get; }

		public void AddScreenshotToTestContext(string partialFilename)
		{
			var screenshot = GetScreenshot();
			var fileName = Path.Combine(Path.GetTempPath(), $"{partialFilename}_{DateTime.Now.Ticks}.png");
			screenshot.SaveAsFile(fileName);
			TestContext.AddResultFile(fileName);
		}
	}
}
