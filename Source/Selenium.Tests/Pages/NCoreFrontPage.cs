using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Selenium.Tests.Pages
{
	class NCoreFrontPage
	{
		public static class Elements
		{
			public static readonly By FrontPageHeader = By.CssSelector("h1");
		}

		private readonly RemoteWebDriver _driver;

		private NCoreFrontPage(RemoteWebDriver driver)
		{
			_driver = driver;
		}

		public string GetHeaderText() => _driver.FindElement(Elements.FrontPageHeader).Text;

		public static NCoreFrontPage Connect(RemoteWebDriver driver)
		{
			driver.WaitForElementVisible(Elements.FrontPageHeader);
			return new NCoreFrontPage(driver);
		}

		internal IEnumerable<IWebElement> GetAllServiceHrefs()
		{
			return _driver.FindElementsByCssSelector("a[href$=\".svc\"]");
		}
	}
}
