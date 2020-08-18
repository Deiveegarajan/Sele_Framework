using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;

namespace Selenium.Tests.Drivers
{
	class FirefoxDriverEx : FirefoxDriver, IGetTestContext
	{
		public static FirefoxOptions GetOptions(bool headless)
		{
			var options = new FirefoxOptions();
			if (headless)
			{
				options.AddArguments("--headless");
			}

			//options.AddArguments(
			//	"download.prompt_for_download",
			//	"disable-popup-blocking",
			//	"--disable-notifications",
			//	"disable-infobars",
			//	"disable-extensions",
			//	"chrome.switches",
			//	"--disable-extensions",
			//	"--test-type");

			return options;
		}

		public FirefoxDriverEx(TestContext testContext, string chromeDriverDirectory, FirefoxOptions options)
			: base(chromeDriverDirectory, options) //NOTE: Do not specify default commandTimeouts. Use explicit waiting.
		{
			TestContext = testContext;
		}

		public TestContext TestContext { get; }
	}
}
