#if RELEASE
#define HEADLESS
#endif

#define FIREFOX

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Drivers;


namespace Selenium.Tests
{
	public static class Driver
	{
		public static void Run(TestContext testContext, string url, Action<RemoteWebDriver> action)
		{
			using(var driver = Get(testContext, url))
			{
				try
				{
					action(driver);
				}
				 catch
				{
					driver.AddScreenshotToTestContext("SeleniumTest_Screenshot");                    
                    throw;
				}
                finally
                {
                    driver.Manage().Cookies.DeleteAllCookies();
                    driver.Quit();
                }
            }
		}

		private static RemoteWebDriver Get(TestContext testContext, string url)
		{
#if HEADLESS
			const bool headless = true;
#else
			const bool headless = false;
#endif
			const string defaultBrowser = "chrome";
			var browser = Environment.GetEnvironmentVariable("SeleniumDriver") ?? defaultBrowser;
			switch (browser.ToLower())
			{
				case "firefox":
					return Get(testContext, url, "firefox", headless);
				case "chrome":
					return Get(testContext, url, "chrome", headless);
				default:
					throw new InvalidOperationException($"Browser '{browser}' not supported");
			}
		}
		private static RemoteWebDriver Get(TestContext testContext, string url, string driver, bool headless)
		{
			var webDriver = GetDriver(testContext, driver, headless);

            webDriver.Manage().Cookies.DeleteAllCookies();
            webDriver.Navigate().GoToUrl(url);
			

			var window = webDriver.Manage().Window;
			window.Size = new Size(1024, 800);
			window.Position = new Point(0, 0);
			return webDriver;
		}

		static RemoteWebDriver GetDriver(TestContext testContext, string driver, bool headless)
		{
			var driverDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			switch (driver)
			{
				case "firefox":
					return new FirefoxDriverEx(testContext, driverDirectory, FirefoxDriverEx.GetOptions(headless));
				case "chrome":
					return new ChromeDriverEx(testContext, driverDirectory, ChromeDriverEx.GetOptions(headless));
				default:
					throw new ArgumentOutOfRangeException($"Driver {driver} not supported");
			}
		}
	}
}
