using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Selenium.Tests.Base.Selenium.Core;
using Selenium.Tests.Drivers;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Selenium.Tests
{
	public static class RemoteWebDriverExtensionMethods
	{
		public static IWebElement WaitForElementVisible(this RemoteWebDriver driver, By by)
			=> WaitForElementVisible(driver, by, new TimeSpan(0, 0, 45));

		public static IWebElement WaitForElementVisible(this RemoteWebDriver driver, By by, TimeSpan timeout)
		{
			var wait = new WebDriverWait(driver, timeout);
			wait.PollingInterval = TimeSpan.FromSeconds(0.2); ;

			Exception exception = null;
			IWebElement elementToBeDisplayed = null;
			try
			{
				bool element = wait.Until(condition =>
				{
					try
					{
						// First check if any are found. This does not throw any exceptions
						var elements = driver.FindElements(by);
						if (elements.Count == 0)
							return false;
						// Then get the element
						elementToBeDisplayed = driver.FindElement(by);
						return elementToBeDisplayed.Displayed;
					}
					catch (StaleElementReferenceException ex)
					{
						exception = ex;
						return false;
					}
					catch (NoSuchElementException ex)
					{
						exception = ex;
						return false;
					}
				});
			}
			catch (WebDriverTimeoutException)
			{
				if (exception != null)
					throw exception;
				throw;
			}

			if (elementToBeDisplayed == null)
				throw new NoSuchElementException();
			return elementToBeDisplayed;
		}

		public static void MoveToAndClick(this RemoteWebDriver self, By by, bool waitUntilDocumentReady = true)
			=> self.MoveToAndClick(self.FindElement(by), waitUntilDocumentReady);

		public static void MoveToAndClick(this RemoteWebDriver self, IWebElement element, bool waitUntilDocumentReady = true)
		{
			new Actions(self)
				.MoveToElement(element)
				.Click()
				.Perform();

			if (waitUntilDocumentReady)
			{
				self.WaitUntilPageLoaded();
			}
		}


        #region Ecm Selection
        /// <summary>
        /// Ecm Selection
        /// </summary>
        /// <param name="driver"></param>
        public static void EcmSelection(this RemoteWebDriver driver)
        {
            CommonMethods.PlayWait(3000);
            if (driver.FindElements(By.XPath("//input[@id='user-identification']")).Any())
            {
                IWebElement elements = driver.FindElement(By.XPath("//input[@id='user-identification']"));
                elements.SendKeys("a@elementsecm.no");
                CommonMethods.PlayWait(2000);
                driver.ClickOnElement(By.XPath("//button[text()='Send']"));
                CommonMethods.PlayWait(3000);
            }
        }
        #endregion

        public static void AddScreenshotToTestContext(this RemoteWebDriver driver, string partialFilename)
		{
			var screenshot = driver.GetScreenshot();
			var fileName = Path.Combine(Path.GetTempPath(), $"{DateTime.Now.Ticks}_{partialFilename}.png");
			screenshot.SaveAsFile(fileName);
			((IGetTestContext)driver).TestContext.AddResultFile(fileName);

		}


		public static void WaitUntilPageLoaded(this RemoteWebDriver driver)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
			wait.PollingInterval = TimeSpan.FromSeconds(0.2);
			wait.Until(d =>
			{
				return ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete");
			});
		}

		public static bool RunAndWaitForClosedWindow(this RemoteWebDriver driver, Action action, TimeSpan timeout)
		{
			var numberOfWindows = driver.WindowHandles.Count;
			action();
            CommonMethods.PlayWait(4000);
            var waitUntil = DateTime.Now.Add(timeout);
            if(numberOfWindows == driver.WindowHandles.Count)
            {
                //If the window is still open, then it might be ecm window. handle ECM window.
                EcmSelection(driver);
            }
			while (numberOfWindows == driver.WindowHandles.Count)
			{
				if (DateTime.Now > waitUntil)
					return false;
				Thread.Sleep(TimeSpan.FromSeconds(0.2));
			}
			return true;
		}

		public static bool RunAndWaitForNewWindow(this RemoteWebDriver driver, Action action, TimeSpan timeout)
		{
			var numberOfWindows = driver.WindowHandles.Count;
			action();
            CommonMethods.PlayWait(4000);
			var waitUntil = DateTime.Now.Add(timeout);
			while (numberOfWindows == driver.WindowHandles.Count)
			{
				if (DateTime.Now > waitUntil)
				{
					return false;
				}
				Thread.Sleep(TimeSpan.FromSeconds(0.2));
			}
			var newWindowHandle = driver.WindowHandles.Last();
			driver.SwitchTo().Window(newWindowHandle);
			return true;
		}

		public static bool MoveToClickAndWaitForClosedWindow(this RemoteWebDriver driver, By by, TimeSpan timeout)
		{
			return driver.RunAndWaitForClosedWindow(
				() => driver.MoveToAndClick(by, false), 
				timeout);
		}

	}
}
