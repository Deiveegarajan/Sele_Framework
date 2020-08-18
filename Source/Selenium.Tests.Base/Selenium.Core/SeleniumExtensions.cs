using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using System.Reactive.Linq;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Selenium.Tests.Base.Selenium.Core
{
    public static class SeleniumExtensions
    {
        #region Declaration
        /// <summary>
        /// Declaring the global variables
        /// </summary>
        public static string text = string.Empty;
        public static int celno;
        public static IWebElement childElement = null;
        public static IList<IWebElement> folders = null;
        public static IList<IWebElement> childElements = null;
        public static int pageIdNumber;
        public static int pgeid = 1;
        public static string resultsDateFormat = "Mddyyyyhmms";
        public static RemoteWebDriver driver;
        #endregion

        #region Enter text
        /// <summary>
        /// Enter the texts to pass the values into filed
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="elementType"></param>
        /// <param name="by">Parameter of Enum type By .</param>
        /// <param name="elementText"></param>

        public static void EnterText(this RemoteWebDriver driver, By by, string elementText)
        {
            try
            {
                IWebElement element = driver.FindElement(by);
                if (element != null)
                {
                    element.SendKeys(elementText);
                }

                else
                {
                    throw new NoSuchElementException();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region SelectListValue
        /// <summary>
        /// Public method which includes logic related to Select the value from List box element through Xpath.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        /// <param name="by">Parameter of Enum type By .</param>
        /// <param name="value">Parameter of type System.String for value.</param>
        /// <returns>True or false</returns>
        public static bool SelectListValue(this RemoteWebDriver driver, By by, string value)
        {
            try
            {
                IWebElement element = driver.FindElement(by);
                if (element != null)
                {
                    element.SendKeys(Keys.ArrowDown);
                    SelectElement elements = new SelectElement(element);
                    elements.SelectByValue(value);
                    return true;
                }
                else
                {
                    throw new NoSuchElementException();
                }

            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion SelectListValue

        #region SelectListValueAndSelectByText
        /// <summary>
        /// Public method which includes logic related to Select the value from List box element through Xpath.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        /// <param name="by">Parameter of Enum type By .</param>
        /// <param name="text">Parameter of type System.String for text.</param>
        /// <returns>True or false</returns>
        public static bool SelectListValueAndSelectByText(this RemoteWebDriver driver, By by, string text)
        {
            try
            {
                IWebElement element = driver.FindElement(by);
                if (element != null)
                {
                    element.SendKeys(Keys.ArrowDown);
                    element.Click();
                    SelectElement elements = new SelectElement(element);
                    elements.SelectByText(text);
                    return true;
                }
                else
                {
                    throw new NoSuchElementException();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion SelectListValueByXpathAndSelectByText

        #region'Select List Values
        /// <summary>
        /// Select the list values
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SelectListValues(this RemoteWebDriver driver, By by, string value)
        {
            try
            {
                IWebElement element = driver.FindElement(by);
                if (element != null)
                {
                    element.SendKeys(value);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region SelectListValue
        /// <summary>
        /// Public method which includes logic related to select the value from listbox
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver</param>
        /// <param name="listBoxElement">Parameter of type System.String for listBoxElement </param>
        /// <param name="by">Parameter of Enum type By .</param>
        /// <param name="tagName">Parameter of Enum type By .</param>
        /// <param name="value">Parameter of type System.String for value</param>
        /// <returns>Parameter of type System.Boolean for true or false</returns>
        public static bool SelectListDrapdown(RemoteWebDriver driver, IWebElement listBoxElement, By by, By tagName, string value)
        {

            MouseHoverClickOnElement(driver, listBoxElement);
            IWebElement popup = driver.FindElement(by);
            IList<IWebElement> optionElements = popup.FindElements(tagName);
            for (int index = 0; index <= optionElements.Count - 1; index++)
            {
                if (optionElements[index].Text.Equals(value))
                {
                    driver.ClickElement(optionElements[index]);
                    return true;
                }

            }
            return false;
        }
        #endregion

        #region SelectListBoxValues
        /// <summary>
        /// Public method which includes logic related to select the values from listbox
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver</param>
        /// <param name="listBoxElement">Parameter of type System.String for listBoxElement </param>
        /// <param name="by">Parameter of Enum type By .</param>
        /// <param name="option">Parameter of Enum type By .</param>
        /// <param name="values">Parameter of type System.String[] for values</param>
        /// <returns>Parameter of type System.string for null or failed item lists</returns>
        public static string SelectListBoxValues(this RemoteWebDriver driver, IWebElement listBoxElement, By dropdown, By option, string[] expectedValues)
        {

            string failedItems = null;
            for (int dropdownIndex = 0; dropdownIndex <= expectedValues.Length - 1; dropdownIndex++)
            {
                //We need this as each time it calls ClickElement, OptionElements will be empty
                driver.MouseHoverClickOnElement(listBoxElement);
                IWebElement popup = driver.FindElement(dropdown);
                IList<IWebElement> optionElements = popup.FindElements(option);
                for (int index = 0; index <= optionElements.Count - 1; index++)
                {
                    if (optionElements[index].Text == expectedValues[dropdownIndex])
                    {
                        if (driver.ClickElement(optionElements[index]) == false)
                        {
                            failedItems = failedItems + "," + optionElements[index];
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return failedItems;
        }
        #endregion

        #region SelectTListValueFromFrame
        /// <summary>
        /// Publuic method which includes logic related to Select the value from list box in frames
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver</param>
        /// <param name="listBoxElement">Parameter of type System.String for listBoxElement</param>
        /// <param name="by">Parameter of Enum type By .</param>
        /// <param name="tagName">Parameter of Enum type By .</param>
        /// <param name="listValue">Parameter of type System.String for listValue</param>
        /// <returns>Parameter of type Sytem.Boolean for True or False</returns>     
        public static bool SelectTListValueFromFrame(this RemoteWebDriver driver, IWebElement listBoxElement, By by, By tagName, string listValue)
        {
            IWebElement requiredPopup = null;
            CommonMethods.PlayWait(1000);
            driver.MouseHoverClickOnElement(listBoxElement);
            IList<IWebElement> popups = driver.FindElements(by);
            for (int index = 0; index <= popups.Count; index++)
            {
                if (popups[index].Text.Contains(listValue))
                {
                    requiredPopup = popups[index];
                    break;
                }
            }

            IList<IWebElement> optionElements = requiredPopup.FindElements(tagName);
            for (int index = 0; index <= optionElements.Count - 1; index++)
            {
                if (optionElements[index].Text == listValue)
                {
                    driver.MouseHoverClickOnElement(optionElements[index]);
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Click On Element 
        /// <summary>
        /// Public method which includes logic related to Clicking Element through Xpath.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        /// <param name="by">Parameter of Enum type By .</param>
        /// <returns>True or false</returns>
        public static bool ClickOnElement(this RemoteWebDriver driver, By by)
        {
            try
            {
                IWebElement element = driver.FindElement(by);
                if (element != null)
                {
                    try
                    {
                        element.SendKeys(Keys.Enter);
                        return true;
                    }
                    catch (Exception)
                    {
                        element.Click();
                        return true;
                    }
                }
                else
                {
                    throw new NoSuchElementException();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region ClickOnElement using string 
        /// <summary>
        /// Public method which includes logic related to Clicking Element through Xpath.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        /// <param name="elementValue">Parameter of type System.String for xPath checking.</param>
        /// <returns>True or false</returns>
        public static bool ClickOnElement(this RemoteWebDriver driver, string elementValue)
        {
            try
            {
                IWebElement element = driver.FindElementByXPath(elementValue);
                if (element != null)
                {
                    try
                    {
                        element.SendKeys(Keys.Enter);
                        return true;
                    }
                    catch (Exception)
                    {
                        element.Click();
                        return true;
                    }
                }
                else
                {
                    throw new NoSuchElementException();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region ClickOnChkbox
        /// <summary>
        /// Public method which includes logic related to Clicking Element through Name.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        /// <param name="by">Parameter of Enum type By .</param>
        /// /// <param name="value">Parameter of type System.String for value.</param>
        /// <returns>True or false</returns>
        public static bool ClickOnChkbox(this RemoteWebDriver driver, By by, string value)
        {
            try
            {
                IWebElement element = driver.FindElement(by);

                if (element != null)
                {
                    if (value.ToUpper() == "ON" && element.Selected == false)
                    {
                        try
                        {
                            element.SendKeys(Keys.Space);
                            return true;
                        }
                        catch (Exception)
                        {
                            element.Click();
                            return true;
                        }
                    }
                    else if (value.ToUpper() == "OFF" && element.Selected == true)
                    {
                        try
                        {
                            element.SendKeys(Keys.Space);
                        }
                        catch (Exception)
                        {
                            element.Click();
                        }

                    }
                }
                else
                {
                    throw new NoSuchElementException();
                }

            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
            return true;
        }
        #endregion

        #region ClickOnRadioBtn
        /// <summary>
        /// Public method which includes logic related to Clicking on Radio button
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        /// <param name="element">Parameter of type OpenQA.Selenium.IWebElement for element.</param>
        /// <returns>Trur or False</returns>
        public static bool ClickOnRadioBtn(this RemoteWebDriver driver, IWebElement element)
        {
            try
            {
                if (element != null)
                {
                    try
                    {
                        element.SendKeys(Keys.Enter);
                        return true;
                    }
                    catch (Exception)
                    {
                        element.Click();
                        return true;
                    }
                }
                else
                {
                    throw new NoSuchElementException();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region ClickElement
        /// <summary>
        /// Public method which includes logic related to Clicking on Element 
        /// </summary>
        /// <param name="webElement">Parameter of type OpenQA.Selenium.IWebElement for webElement.</param>
        /// <returns>True or false</returns>
        public static bool ClickElement(this RemoteWebDriver driver, IWebElement webElement)
        {
            try
            {
                if (webElement != null)
                {
                    webElement.Click();
                    return true;
                }
                else
                {
                    throw new NoSuchElementException();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region SubmitButton
        /// <summary>
        /// Public method which includes logic related to clicking on button through Xpath
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        /// <param name="by">Parameter of Enum type By .</param>
        /// <returns>True or false</returns>
        public static bool SubmitButton(this RemoteWebDriver driver, By by)
        {
            try

            {
                IWebElement element = driver.FindElement(by);
                if (element != null)
                {
                    element.Submit();
                    return true;
                }
                else
                {
                    throw new NoSuchElementException();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region VerifyElementByXpath
        /// <summary>
        /// Public method which includes logic related to Verifying element through xPath.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        /// <param name="by">Parameter of Enum type By .</param>
        /// <returns>True or false</returns>
        public static bool VerifyElement(this RemoteWebDriver driver, By by)
        {
            try
            {
                IWebElement element = driver.FindElement(by);
                if (element != null)
                {
                    return true;
                }
                else
                {
                    throw new NoSuchElementException();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region FindElement
        /// <summary>
        /// Public method which includes logic related to Finding Element thorugh Xpath.
        /// </summary>
        /// <param name="webDriver">Parameter of type OpenQA.Selenium.IWebDriver for webDriver.</param>
        /// <param name="xPath">Parameter of type System.String for xPath.</param>
        /// <returns>WebElement</returns>
        public static IWebElement FindElement(this IWebDriver webDriver, string xPath)
        {
            try
            {
                IWebElement element = webDriver.FindElement(By.XPath(xPath));
                if (element == null)
                {
                    return null;
                }
                return element;
            }
            catch (Exception)
            {
                throw new NoSuchElementException ();
            }
        }
        #endregion

        #region SwitchtoNewWindow
        /// <summary>
        /// Public method which includes logic related to Switching new window.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        public static void SwitchtoNewWindow(this RemoteWebDriver driver)
        {
            try
            {
                //string existingWindowHandle = driver.CurrentWindowHandle;
                string NewWindowHandle = string.Empty;
                ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
                NewWindowHandle = windowHandles[windowHandles.Count - 1];
                driver.SwitchTo().Window(NewWindowHandle);
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                //return false;
            }
        }
        #endregion

        #region SwitchtoNewFrame
        /// <summary>
        /// Public method which includes logic related to Switching new frame.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        public static void SwitchtoNewFrameByIndex(this RemoteWebDriver driver, int frameIndex)
        {
            try
            {
                driver.SwitchTo().Frame(frameIndex);
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region SwitchtoNewFrame
        /// <summary>
        /// Public method which includes logic related to Switching new frame.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        public static void SwitchtoNewFrame(this RemoteWebDriver driver, IWebElement frameElement)
        {
            try
            {
                driver.SwitchTo().Frame(frameElement);
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region SwitchtoNewFrame
        /// <summary>
        /// Public method which includes logic related to Switching new frame.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        public static void SwitchtoNewFrameByName(this RemoteWebDriver driver, string frameName)
        {
            try
            {
                driver.SwitchTo().Frame(frameName);
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }

        }
        #endregion

        #region MouseRightClickOnElement
        /// <summary>
        /// Public method which includes logic related to MouseRightClickOnElement.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>   
        /// <param name="Element">Parameter of type OpenQA.Selenium.IWebElement for webelement.</param>
        public static void MouseRightClickOnElement(this RemoteWebDriver driver, IWebElement element)
        {
            try
            {
                Actions builder = new Actions(driver);
                builder.ContextClick(element).Build().Perform();
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region MouseDoubleClickOnElement
        /// <summary>
        /// Public method which includes logic related to MouseDoubleClickOnElement.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>   
        /// <param name="Element">Parameter of type OpenQA.Selenium.IWebElement for webelement.</param>
        public static void MouseDoubleClickOnElement(this RemoteWebDriver driver, IWebElement element)
        {
            try
            {
                Actions builder = new Actions(driver);
                builder.DoubleClick(element).Build().Perform();
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region Keyboard Key press
        /// <summary>
        /// Public method which includes logic related to do Keyboard keys on specified element
        /// </summary>
        /// <param name="element">Parameter of type OpenQA.Selenium.IwebElement for element</param>
        public static void KeyboardKeys(this RemoteWebDriver driver,By by, string Keyboardkey)
        {
            try
            {
                IWebElement element = driver.FindElement(by);
                switch (Keyboardkey)
                {
                    case  "Tab"       : element.SendKeys(Keys.Tab);
                        break;
                    case "Alt"        : element.SendKeys(Keys.Alt);
                        break;
                    case "ArrowDown"  : element.SendKeys(Keys.ArrowDown);
                        break; 
                    case "ArrowLeft"  : element.SendKeys(Keys.ArrowLeft);
                        break;
                    case "ArrowRight" : element.SendKeys(Keys.ArrowRight);
                        break;
                    case "ArrowUp"    : element.SendKeys(Keys.ArrowUp);
                        break;
                    case "Backspace"  : element.SendKeys(Keys.Backspace);
                        break;
                    case "Clear"      : element.SendKeys(Keys.Clear);
                        break;
                    case "Enter"      : element.SendKeys(Keys.Enter);
                        break;
                    case "PageDown"   : element.SendKeys(Keys.PageDown);
                        break;
                    case "PageUp"     :  element.SendKeys(Keys.PageUp);
                        break;
                    case "Shift"      : element.SendKeys(Keys.Shift);
                        break;
                    case "Control"    : element.SendKeys(Keys.Control);
                        break;
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Press the keyboard key \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region Click button
        /// <summary>
        /// Click on a button 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="elementType"></param>
        public static void ClickModule(RemoteWebDriver driver, By by, string elementType)
        {
            try
            {
                driver.FindElement(By.XPath("//span[@title=" + "'" + by + "'" + "]")).Click();
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region ElementDrawHighlight
        /// <summary>
        /// Draw blue border around the Element to Highlight in GUI
        /// </summary>
        /// <param name="element">Parameter of type OpenQA.Selenium.IwebElement for element</param>
        public static void DrawHighlight(this IWebElement element)
        {
            var rc = (RemoteWebElement)element;
            var driver = (IJavaScriptExecutor)rc.WrappedDriver;
            var script = @"arguments[0].style.cssText = ""border-width: 5px; border-style: solid; border-color: blue""; ";
            driver.ExecuteScript(script, rc);
            Observable.Timer(new TimeSpan(0, 0, 3)).Subscribe(p =>
            {
                try
                {
                    var clear = @"arguments[0].style.cssText = ""border-width: 0px; border-style: solid; border-color: blue""; ";
                    driver.ExecuteScript(clear, rc);
                }
                catch
                {
                    //Do nothing
                }
            });
        }
        #endregion ElementDrawHighlight

        #region ElementDrawHighlight
        /// <summary>
        /// Draw blue border around the Element to Highlight in GUI
        /// </summary>
        /// <param name="_driver"></param>
        /// <param name="by"></param>
        public static void DrawHighlight(this RemoteWebDriver _driver, By by)
        {
            IWebElement element = _driver.FindElement(by);
            var rc = (RemoteWebElement)element;
            var driver = (IJavaScriptExecutor)rc.WrappedDriver;
            var script = @"arguments[0].style.cssText = ""border-width: 5px; border-style: solid; border-color: blue""; ";
            driver.ExecuteScript(script, rc);
            Observable.Timer(new TimeSpan(0, 0, 3)).Subscribe(p =>
            {
                var clear = @"arguments[0].style.cssText = ""border-width: 0px; border-style: solid; border-color: blue""; ";
                driver.ExecuteScript(clear, rc);
            });
        }
        #endregion

        #region MouseHoverClickOnElement
        /// <summary>
        /// Public method which includes logic related to MouseHoverClickOnElement.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>   
        /// <param name="Element">Parameter of type OpenQA.Selenium.IWebElement for webelement.</param>
        public static void MouseHoverClickOnElement(this RemoteWebDriver driver, IWebElement element)
        {
            try
            {
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Build().Perform();
                CommonMethods.PlayWait(1000);
                builder.Click(element).Build().Perform();
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region MouseHoverSendKeys
        /// <summary>
        /// Public method which includes logic related to MouseHoverClickOnElement.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>   
        /// <param name="Element">Parameter of type OpenQA.Selenium.IWebElement for webelement.</param>
        public static void MouseHoverSendKeys(this RemoteWebDriver driver, IWebElement element, string text)
        {
            try
            {
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Build().Perform();
                CommonMethods.PlayWait(1000);
                builder.SendKeys(text).Build().Perform();
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region MouseHoverClickAndSendKeys
        /// <summary>
        /// Public method which includes logic related to Mouse Hover Click On Element and Send the Values.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>   
        /// <param name="Element">Parameter of type OpenQA.Selenium.IWebElement for webelement.</param>
        /// <param name="text">Parameter of string type to pass the values</param>
        public static void MouseHoverClickAndSendKeys(this RemoteWebDriver driver, IWebElement element, string text)
        {
            try
            {
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Click().Build().Perform();
                CommonMethods.PlayWait(1000);
                builder.SendKeys(text).Build().Perform();
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region MouseHoverClickOnElementUsingSpace
        /// <summary>
        /// Public method which includes logic related to MouseHoverClickOnElement.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>   
        /// <param name="Element">Parameter of type OpenQA.Selenium.IWebElement for webelement.</param>
        public static void MouseHoverClickOnElementUsingSpace(this RemoteWebDriver driver, IWebElement element)
        {
            try
            {
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Build().Perform();
                builder.MoveToElement(element).SendKeys(Keys.Space).Build().Perform();
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region MouseHoverOnElement
        /// <summary>
        /// Public method which includes logic related to MouseHoverClickOnElement.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>   
        /// <param name="Element">Parameter of type OpenQA.Selenium.IWebElement for webelement.</param>
        public static void MouseHoverOnElement(this RemoteWebDriver driver, IWebElement element)
        {
            try
            {
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Build().Perform();
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region MenuNavigation
        /// <summary>
        /// Public method which includes logic related to MenuNavigation.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>   
        /// <param name="MainMenu">Parameter of type OpenQA.Selenium.IWebElement for MainMenu.</param>
        /// <param name="SubMenu">Parameter of type OpenQA.Selenium.IWebElement for SubMenu.</param>
        public static void MenuNavigation(this RemoteWebDriver driver, IWebElement mainMenu, By by)
        {
            try
            {
                Actions builder = new Actions(driver);
                builder.MoveToElement(mainMenu).Build().Perform();
                //sync
                CommonMethods.PlayWait(1000);
                IWebElement subMenu = driver.FindElement(by);
                builder.MoveToElement(subMenu).Click().Perform();
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region VerifyIsElementEnabled
        /// <summary>
        /// Public method which includes logic related to verification of enabled status.
        /// </summary>
        /// <param name="webelement">Parameter of type OpenQA.Selenium.IWebElement for webelement.</param>
        /// <returns>True or False</returns>
        public static bool VerifyIsElementEnabled(this IWebElement webelement)
        {
            return webelement.Enabled;
        }
        #endregion

        #region VerifyIsElementPresent
        /// <summary>
        /// This method used to verify the element is present in UI
        /// </summary>       
        /// <param name="element">specify the element</param>
        /// <returns>true or false</returns>
        public static bool VerifyIsElementPresent(IWebElement element)
        {
            return element.Displayed;
        }
        #endregion VerifyIsElementPresent

        #region VerifyEditField
        /// <summary>
        /// Public method which includes logic related to verification of element text through name.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver.</param>
        /// <param name="by">Parameter of Enum type By .</param>
        /// <param name="ExpText">Parameter of type System.String for ExpText.</param>
        /// <returns>True or False</returns>
        public static bool VerifyEditFieldTextByName(this RemoteWebDriver driver, By by, string expText)
        {
            try
            {
                IWebElement webelement = driver.FindElement(by);
                if (webelement != null)
                {
                    if (webelement.Enabled == true)
                    {
                        text = webelement.Text;
                    }
                    else
                    {
                        text = webelement.GetAttribute("value");
                    }

                    if (text.ToUpper() == expText.ToUpper())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }//End of Main if
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region VerifyListValue
        /// <summary>
        /// Public method which includes logic related to verification of list box value.
        /// </summary>
        /// <param name="webelement">Parameter of type OpenQA.Selenium.IwebElement for webelement.</param>
        /// <param name="ExpText">Parameter of type System.String for ExpText.</param>
        /// <returns>True or False</returns>
        public static bool VerifyListValue(this IWebElement webelement, string expText)
        {
            try
            {
                SelectElement selectedValue = new SelectElement(webelement);
                string actualText = selectedValue.SelectedOption.Text;

                if (actualText.ToUpper() == expText.ToUpper())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region CaptureSnapshot
        /// <summary>
        /// Public method which includes logic related to capturing the screenshot.
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebElement for webelement.</param>
        /// <param name="Filepath">Parameter of type System.String for FilePath.</param>
        /// <param name="FileName">Parameter of type System.String for FileName.</param>       
        public static void CaptureSnapshot(this RemoteWebDriver driver, string filePath, string fileName)
        {
            try
            {
                string directoryPath = filePath;
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string screenshot = ss.AsBase64EncodedString;
                byte[] screenshotAsByteArray = ss.AsByteArray;
                if (Directory.Exists(directoryPath))
                {
                    ss.SaveAsFile(directoryPath + fileName + ".Png",ScreenshotImageFormat.Png);
                    ss.ToString();
                }
                else
                {
                    Directory.CreateDirectory(directoryPath);
                    ss.SaveAsFile(directoryPath + fileName + ".Png",ScreenshotImageFormat.Png);
                    ss.ToString();
                }

            }
            catch (Exception e)
            {
                e.GetBaseException();
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region ClearField
        /// <summary>
        /// Public method which includes logic related to clearing the fileds.
        /// </summary>
        /// <param name="we">Parameter of type OpenQA.Selenium.IwebElement for webelement.</param>       
        public static void ClearField(this IWebElement we)
        {
            try
            {
                //Clearing the gallery name edit field
                if (CommonMethods.browserType == "IE")
                {
                    we.SendKeys(Keys.Control + "a");
                    we.SendKeys(Keys.Delete);
                    if (we.GetAttribute("value") != "")
                    {
                        we.Clear();
                    }
                }
                else
                {
                    we.Clear();
                    if (we.GetAttribute("value") != "")
                    {
                        we.SendKeys(Keys.Control + "a");
                        we.SendKeys(Keys.Delete);
                    }
                }
            }
            catch (Exception e)
            {
                e.GetBaseException();
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region GetPopupErrorMsg
        /// <summary>
        /// Public method which includes logic related to get the alert message
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <returns>String value:Alert message</returns>
        public static string GetPopupErrorMsg(this RemoteWebDriver driver)
        {
            try
            {
                //Handling Alert popups
                IAlert alert = driver.SwitchTo().Alert();
                string actualPopupText = alert.Text;
                CommonMethods.PlayWait(1000);
                alert.Accept();
                CommonMethods.PlayWait(1000);
                return actualPopupText;
            }
            catch (NoAlertPresentException Ae)
            {
                Ae.GetType();
                string actualPopupText = null;
                return actualPopupText;
            }
        }
        #endregion

        #region GetPopupErrorMsgWithDismiss
        /// <summary>
        /// Public method which includes logic related to get the alert message
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <returns>String Value:Alert Message</returns>
        public static string GetPopupErrorMsgWithDismiss(this RemoteWebDriver driver)
        {
            try
            {
                //Handling Alert popups
                IAlert alert = driver.SwitchTo().Alert();
                string actualPopupText = alert.Text;
                alert.Dismiss();
                return actualPopupText;
            }
            catch (NoAlertPresentException Ae)
            {
                Ae.GetType();
                string actualPopupText = null;
                return actualPopupText;
            }
        }
        #endregion

        #region IsAlertPresent
        /// <summary>
        /// Public method which includes logic related to close the alert message
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <returns>True or False</returns>
        public static void IsAlertPresent(this RemoteWebDriver driver)
        {
            try
            {
                //Handling Alert popups
                IAlert alert = driver.SwitchTo().Alert();
                if (alert.Text != null)
                {
                    alert.Accept();
                }
                else
                {
                    CommonMethods.CreateLog("IsAlertPresent:Alert was not existed");
                }
            }
            catch (NoAlertPresentException e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region ReadingDataFromHtmlTable
        /// <summary>
        /// Public method which includes to read the data from html table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="noofPagesIdElement">Parameter of type OpenQA.Selenium.IwebElement for WebElement</param>
        /// <param name="nextPage">Parameter of Enum type By .</param>
        /// <param name="tablePropertyName">Parameter of type System.String for TableId.</param>
        /// <param name="tablePropertyValue">Parameter of type System.String for TableId.</param>
        /// <param name="tableRow">Parameter of Enum type By .</param>
        /// <param name="tableCell">Parameter of Enum type By .</param>
        /// <param name="ExpText">Parameter of type System.String for ExpText.</param>
        /// <returns>True or False</returns>
        public static bool ReadingDataFromHtmlTable(this RemoteWebDriver driver, int pgeIdnumber, By nextPage,
            string tablePropertyName, string tablePropertyValue, By tableRow, By tableCell, string expText)
        {
            text = string.Empty;
            IWebElement tableElement = null;
            //Capturing the error if any using try-catch
            try
            {
                switch (tablePropertyName.ToUpper())
                {

                    case "NAME":
                        tableElement = driver.FindElementByName(tablePropertyName);
                        break;
                    case "CLASSNAME":
                        tableElement = driver.FindElementByClassName(tablePropertyValue);
                        break;
                    case "XPATH":
                        tableElement = driver.FindElementByXPath(tablePropertyValue);
                        break;
                    case "ID":
                    default:
                        tableElement = driver.FindElementById(tablePropertyValue);
                        break;
                }
                //Retrivng the webelement                
                //Collecting total no fo row elements for a page
                IList<IWebElement> trCollection = tableElement.FindElements(tableRow);
                //Initilizing the rowno,cell no
                int rowno, celno;
                rowno = 1;
                //Assinging the each row element from a list
                for (int tdrow = 0; tdrow <= trCollection.Count; tdrow++)
                {
                    celno = 1;
                    //Collecting the total no of cells data for a page
                    IList<IWebElement> tdCollection = trCollection[tdrow].FindElements(tableCell);
                    //Assigning the each cell data from list
                    for (int tdColumnNumber = 0; tdColumnNumber <= tdCollection.Count; tdColumnNumber++)
                    {
                        //Retriving the each cell data
                        string actualData = (tdCollection[tdColumnNumber].Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;
                            //Exit from loop
                            if (Exit(expText) == false)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count)
                        {
                            //Comparing the total no of pages
                            if (pgeIdnumber != pgeid && !(driver.FindElement(nextPage)).GetAttribute("class").Contains("disabled"))
                            {
                                //Clicking on element
                                driver.ClickElement(driver.FindElement(nextPage));
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                //goto First;
                                return ReadingDataFromHtmlTable(driver, pgeIdnumber,
                                    nextPage, tablePropertyName, tablePropertyValue, tableCell, tableRow, expText);
                            }
                            else
                            {
                                if (Exit(expText) == false)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }

                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                }
            }

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace + "\n" + e.GetBaseException());
                return false;
            }
            finally
            {
                pgeid = 1;
            }
            return true;
        }
        #endregion

        #region VerifyCellPropertyValueFromHtmlTable
        /// <summary>
        /// Public method which includes to read the data from html table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="noofPagesIdElement">Parameter of type OpenQA.Selenium.IwebElement for WebElement</param>
        /// <param name="nextPage">Parameter of Enum type By .</param>
        /// <param name="tablePropertyName">Parameter of type System.String for TableId.</param>
        /// <param name="tablePropertyValue">Parameter of type System.String for TableId.</param>
        /// <param name="tableRow">Parameter of Enum type By .</param>
        /// <param name="tableCell">Parameter of Enum type By .</param>
        /// <param name="propertyName">Parameter of type System.String for ExpText.</param>
        /// <param name="propertyValue">Parameter of type System.String for ExpText.</param>
        /// <param name="ExpText">Parameter of type System.String for ExpText.</param>
        /// <returns>True or False</returns>
        public static bool VerifyCellPropertyValueFromHtmlTable(this RemoteWebDriver driver, int pgeIdnumber, string nextPage,
            string tablePropertyName, string tablePropertyValue, By tableRow, By tableCell, string propertyName, string propertyValue, string expText)
        {
            IWebElement CellElement = null;
            text = string.Empty;
            //Capturing the error if any using try-catch
            IWebElement tableElement = null;
            //Capturing the error if any using try-catch
            try
            {
                switch (tablePropertyName.ToUpper())
                {

                    case "NAME":
                        tableElement = driver.FindElementByName(tablePropertyValue);
                        break;
                    case "CLASSNAME":
                        tableElement = driver.FindElementByClassName(tablePropertyValue);
                        break;
                    case "XPATH":
                        tableElement = driver.FindElementByXPath(tablePropertyValue);
                        break;
                    case "ID":
                    default:
                        tableElement = driver.FindElementById(tablePropertyValue);
                        break;
                }
                CommonMethods.PlayWait(1000);
                //Collecting total no fo row elements for a page
                IList<IWebElement> trCollection = tableElement.FindElements(tableRow);
                //Initilizing the rowno,cell no
                int rowno, celno;
                rowno = 1;
                //Assinging the each row element from a list
                for (int tdrow = 0; tdrow <= trCollection.Count; tdrow++)
                {
                    celno = 1;
                    //Collecting the total no of cells data for a page
                    IList<IWebElement> tdCollection = trCollection[tdrow].FindElements(tableRow);
                    //Assigning the each cell data from list
                    for (int tdcol = 0; tdcol <= tdCollection.Count; tdcol++)
                    {
                        //Retriving the each cell data
                        string actualData = (tdCollection[tdcol].Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;
                            CellElement = tdCollection[tdcol];
                            //Exit from loop
                            if (Exit(expText) == false)
                            {
                                return false;
                            }
                            else
                            {
                                if (!CellElement.GetAttribute(propertyName).Contains(propertyValue))
                                {
                                    CommonMethods.CreateLog("Verify Cell element proeprty value from html table:No cell element was found with the follwing information" +
                                        " " + propertyName + ":" + propertyValue);
                                    return false;
                                }
                                return true;
                            }
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count)
                        {
                            //Comparing the total no of pages
                            if (pgeIdnumber != pgeid)
                            {
                                //Clicking on element
                                driver.ClickOnElement(nextPage);
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                return VerifyCellPropertyValueFromHtmlTable(driver, pageIdNumber,
                                    nextPage, tablePropertyName, tablePropertyValue, tableRow, tableCell, propertyName, propertyValue,
                                    expText);
                            }
                            else
                            {
                                if (Exit(expText) == false)
                                {
                                    return false;
                                }
                                else
                                {
                                    if (!CellElement.GetAttribute(propertyName).Contains(propertyValue))
                                    {
                                        CommonMethods.CreateLog("Verify Cell element proeprty value from html table:No cell element was found with the follwing information" +
                                            " " + propertyName + ":" + propertyValue);
                                        return false;
                                    }
                                    return true;
                                }

                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                }//end of foreach (IWebElement rowelemnt in tr_collection)
                 //Exit crieteria            
            }//End of try

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace + "\n" + e.GetBaseException());
                return false;
            }//End of catch

            finally
            {
                pgeid = 1;
            }

            return true;
        }
        #endregion

        #region ReadingElementFromHtmltable
        /// <summary>
        /// Public method which includes logic related to read the data from html table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="noofPagesIdElement">Parameter of type OpenQA.Selenium.IwebElement for WebElement</param>
        /// <param name="nextPage">Parameter of Enum type By .</param>
        /// <param name="tablePropertyName">Parameter of type System.String for TableId.</param>
        /// <param name="tablePropertyValue">Parameter of type System.String for TableId.</param>
        /// <param name="tableRow">Parameter of Enum type By .</param>
        /// <param name="tableCell">Parameter of Enum type By .</param>
        /// <param name="ExpText">Parameter of type System.String for ExpText.</param>
        /// <param name="PropertyName">Parameter of type System.String for strPropertyName.</param>
        /// <param name="ProeprtyValue">Parameter of type System.String for strProeprtyValue.</param>
        /// <returns>WebElement:Cell object</returns>
        public static IWebElement ReadingElementFromHtmltable(this RemoteWebDriver driver, int pageIdNumber, By nextPage,
            string tablePropertyName, string tablePropertyValue, By tableRow, By tableCell, string expText, string propertyName, string propertyValue)
        {
            text = string.Empty;
            childElement = null;
            IWebElement tableElement = null;
            //Capturing the error if any using try-catch
            try
            {
                switch (tablePropertyName.ToUpper())
                {

                    case "NAME":
                        tableElement = driver.FindElementByName(tablePropertyValue);
                        break;
                    case "CLASSNAME":
                        tableElement = driver.FindElementByClassName(tablePropertyValue);
                        break;
                    case "XPATH":
                        tableElement = driver.FindElementByXPath(tablePropertyValue);
                        break;
                    case "ID":
                    default:
                        tableElement = driver.FindElementById(tablePropertyValue);
                        break;
                }
                //Collecting total no fo row elements for a page
                IList<IWebElement> trCollection = tableElement.FindElements(tableRow);
                //Initilizing the rowno,cell no
                int rowno, celno;
                rowno = 1;
                //Assinging the each row element from a list
                for (int tdrow = 0; tdrow <= trCollection.Count; tdrow++)
                {
                    celno = 1;
                    //Collecting the total no of cells data for a page
                    IList<IWebElement> tdCollection = trCollection[tdrow].FindElements(tableCell);
                    //Assigning the each cell data from list
                    for (int tdcol = 0; tdcol <= tdCollection.Count; tdcol++)
                    {
                        //Retriving the each cell data
                        string actualData = (tdCollection[tdcol].Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;
                            try
                            {
                                switch (propertyName)
                                {

                                    case "TagName":
                                        childElement = tdCollection[celno - 1].FindElement(By.TagName(propertyValue));
                                        break;
                                    case "ClassName":
                                        childElement = tdCollection[celno - 1].FindElement(By.ClassName(propertyValue));
                                        break;
                                    case "Xpath":
                                        childElement = tdCollection[celno - 1].FindElement(By.XPath(propertyValue));
                                        break;
                                    case "Id":
                                    default:
                                        childElement = tdCollection[celno - 1].FindElement(By.Id(propertyValue));
                                        break;
                                }

                            }//End of try
                            catch (Exception e1)
                            {
                                e1.GetBaseException();
                            }//End of Catch

                            //End of if
                            //Exit from loop
                            if (Exit(expText) == false)
                            {
                                return null;
                            }
                            else
                            {
                                return childElement;
                            }
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count)
                        {
                            //Comparing the total no of pages
                            if (pageIdNumber != pgeid)
                            {
                                //Clicking on element
                                driver.ClickOnElement(nextPage);
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                return ReadingElementFromHtmltable(driver, pageIdNumber, nextPage, tablePropertyName,
                                   tablePropertyValue, tableRow, tableCell, expText, propertyName, propertyValue);
                            }
                            else
                            {
                                if (Exit(expText) == false)
                                {
                                    return null;
                                }
                                else
                                {
                                    return childElement;
                                }

                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                }//end of foreach (IWebElement rowelemnt in tr_collection)
                 //Exit crieteria            
            }//End of try

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace + "\n" + e.GetBaseException());
                return null;
            }//End of catch
            finally
            {
                pgeid = 1;
            }
            return childElement;
        }
        #endregion

        #region ReadingElementFromHtmltable
        /// <summary>
        /// Public method which includes logic related to read the data from html table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="noofPagesIdElement">Parameter of type OpenQA.Selenium.IwebElement for WebElement</param>
        /// <param name="nextPage">Parameter of Enum type By .</param>
        /// <param name="tablePropertyName">Parameter of type System.String for TableId.</param>
        /// <param name="tablePropertyValue">Parameter of type System.String for trXpath.</param>
        /// <param name="rowPropertyName">Parameter of type System.String for tdXpath.</param>
        /// <param name="rowPorpertyvalue">Parameter of type System.String for ExpText.</param>
        /// <param name="cellPropertyName">Parameter of type System.String for strPropertyName.</param>
        /// <param name="cellPropertyName">Parameter of type System.String for strProeprtyValue.</param>
        /// <param name="propertyName">Parameter of type System.String for strPropertyName.</param>
        /// <param name="propertyValue">Parameter of type System.String for strProeprtyValue.</param>
        /// <returns>WebElement:Cell object</returns>
        public static IWebElement ReadingElementFromHtmltable(this RemoteWebDriver driver, int pageIdNumber, By nextPage,
            string tablePropertyName, string tablePropertyValue, string rowPropertyName, string rowPorpertyvalue, string cellPropertyName, string cellPropertyValue, string expText, string propertyName, string propertyValue)
        {
            text = string.Empty;
            childElement = null;
            IWebElement tableElement = null;
            IList<IWebElement> trCollection = null;
            IList<IWebElement> tdCollection = null;
            //Capturing the error if any using try-catch
            try
            {
                switch (tablePropertyName.ToUpper())
                {

                    case "NAME":
                        tableElement = driver.FindElementByName(tablePropertyValue);
                        break;
                    case "CLASSNAME":
                        tableElement = driver.FindElementByClassName(tablePropertyValue);
                        break;
                    case "XPATH":
                        tableElement = driver.FindElementByXPath(tablePropertyValue);
                        break;
                    case "ID":
                    default:
                        tableElement = driver.FindElementById(tablePropertyValue);
                        break;
                }
                //Piece of code which should be executed before loop starts  
                CommonMethods.PlayWait(2000);
                //Collecting total no fo row elements for a page     
                switch (rowPropertyName.ToUpper())
                {

                    case "NAME":
                        trCollection = tableElement.FindElements(By.Name(rowPorpertyvalue));
                        break;
                    case "CLASSNAME":
                        trCollection = tableElement.FindElements(By.ClassName(rowPorpertyvalue));
                        break;
                    case "XPATH":
                        trCollection = tableElement.FindElements(By.XPath(rowPorpertyvalue));
                        break;
                    case "TAGNAME":
                        trCollection = tableElement.FindElements(By.TagName(rowPorpertyvalue));
                        break;
                    case "ID":
                    default:
                        trCollection = tableElement.FindElements(By.Id(rowPorpertyvalue));
                        break;
                }
                //Initilizing the rowno,cell no
                int rowno, celno;
                rowno = 1;
                //Assinging the each row element from a list
                for (int tdrow = 0; tdrow <= trCollection.Count; tdrow++)
                {
                    celno = 1;
                    //Collecting the total no of cells data for a page
                    //Piece of code which should be executed before loop starts  
                    CommonMethods.PlayWait(1000);
                    //Collecting total no fo row elements for a page     
                    switch (cellPropertyName.ToUpper())
                    {

                        case "NAME":
                            tdCollection = tableElement.FindElements(By.Name(cellPropertyValue));
                            break;
                        case "CLASSNAME":
                            tdCollection = tableElement.FindElements(By.ClassName(cellPropertyValue));
                            break;
                        case "XPATH":
                            tdCollection = tableElement.FindElements(By.XPath(cellPropertyValue));
                            break;
                        case "TAGNAME":
                            tdCollection = tableElement.FindElements(By.TagName(cellPropertyValue));
                            break;
                        case "ID":
                        default:
                            tdCollection = tableElement.FindElements(By.Id(cellPropertyValue));
                            break;
                    }
                    //Assigning the each cell data from list
                    for (int tdcol = 0; tdcol <= tdCollection.Count; tdcol++)
                    {
                        //Retriving the each cell data
                        string actualData = (tdCollection[tdcol].Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;
                            try
                            {
                                switch (propertyName)
                                {

                                    case "TAGNAME":
                                        childElement = tdCollection[tdcol].FindElement(By.TagName(propertyValue));
                                        break;
                                    case "CLASSNAME":
                                        childElement = tdCollection[tdcol].FindElement(By.ClassName(propertyValue));
                                        break;
                                    case "XPATH":
                                        childElement = tdCollection[tdcol].FindElement(By.XPath(propertyValue));
                                        break;
                                    case "ID":
                                    default:
                                        childElement = tdCollection[tdcol].FindElement(By.Id(propertyValue));
                                        break;
                                }

                            }//End of try
                            catch (Exception e1)
                            {
                                e1.GetBaseException();
                            }//End of Catch

                            //End of if
                            //Exit from loop
                            if (Exit(expText) == false)
                            {
                                return null;
                            }
                            else
                            {
                                return childElement;
                            }
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count)
                        {
                            //Comparing the total no of pages
                            if (pageIdNumber == 0)
                            {
                                return null;
                            }
                            if (pageIdNumber != pgeid)
                            {
                                //Clicking on element
                                if (nextPage == null)
                                {
                                    return null;
                                }
                                driver.ClickOnElement(nextPage);
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                return ReadingElementFromHtmltable(driver, pageIdNumber, nextPage, tablePropertyName,
                                   tablePropertyValue, rowPropertyName, rowPorpertyvalue, cellPropertyName,
                                   cellPropertyValue, expText, propertyName, propertyValue);
                            }
                            else
                            {
                                if (Exit(expText) == false)
                                {
                                    return null;
                                }
                                else
                                {
                                    return childElement;
                                }

                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                }//end of foreach (IWebElement rowelemnt in tr_collection)
                //Exit crieteria            
            }//End of try

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace + "\n" + e.GetBaseException());
                return null;
            }//End of catch
            finally
            {
                pgeid = 1;
            }
            return childElement;
        }
        #endregion

        #region ReadingCellElementFromHtmltable
        /// <summary>
        /// Public method which includes logic related to read the Cell element from html table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="pageIdNumber">Parameter of type OpenQA.Selenium.IwebElement for WebElement</param>
        /// <param name="nextPage">Parameter of Enum type By .</param>
        /// <param name="tablePropertyName">Parameter of type System.String for TableId.</param>
        /// <param name="tableRow">Parameter of Enum type By .</param>
        /// <param name="tableCell">Parameter of Enum type By .</param>
        /// <param name="ExpText">Parameter of type System.String for ExpText.</param>
        /// <param name="tablePropertyValue">Parameter of type System.String for strPropertyName.</param>
        /// <param name="tablePropertyValue">Parameter of type System.String for strProeprtyValue.</param>
        /// <returns>WebElement:Cell object</returns>
        public static IWebElement ReadingCellElementFromHtmltable(this RemoteWebDriver driver, int pageIdNumber, By nextPage,
            string tablePropertyName, string tablePropertyValue, By tableRow, By tableCell, string expText)
        {
            text = string.Empty;
            childElement = null;
            IWebElement tableElement = null;
            //Capturing the error if any using try-catch
            try
            {
                switch (tablePropertyName.ToUpper())
                {

                    case "NAME":
                        tableElement = driver.FindElementByName(tablePropertyValue);
                        break;
                    case "CLASSNAME":
                        tableElement = driver.FindElementByClassName(tablePropertyValue);
                        break;
                    case "XPATH":
                        tableElement = driver.FindElementByXPath(tablePropertyValue);
                        break;
                    case "ID":
                    default:
                        tableElement = driver.FindElementById(tablePropertyValue);
                        break;
                }
                //Piece of code which should be executed before loop starts  
                CommonMethods.PlayWait(2000);
                //Collecting total no fo row elements for a page                
                IList<IWebElement> trCollection = tableElement.FindElements(tableRow);
                //Initilizing the rowno,cell no
                int rowno, celno;
                rowno = 1;
                //Assinging the each row element from a list
                for (int tdrow = 0; tdrow <= trCollection.Count; tdrow++)
                {
                    celno = 1;
                    //Collecting the total no of cells data for a page
                    IList<IWebElement> tdCollection = trCollection[tdrow].FindElements(tableCell);
                    //Assigning the each cell data from list
                    for (int tdcol = 0; tdcol <= tdCollection.Count; tdcol++)
                    {
                        //Retriving the each cell data
                        string actualData = (tdCollection[tdcol].Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;
                            childElement = tdCollection[tdcol];
                            if (Exit(expText) == false)
                            {
                                return null;
                            }
                            else
                            {
                                return childElement;
                            }
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count)
                        {
                            //Comparing the total no of pages
                            if (pageIdNumber != pgeid && !(driver.FindElement(nextPage)).GetAttribute("class").Contains("disabled"))
                            {
                                //Clicking on element
                                driver.ClickElement(driver.FindElement(nextPage));
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                return ReadingCellElementFromHtmltable(driver, pageIdNumber, nextPage, tablePropertyName,
                                   tablePropertyValue, tableRow, tableCell, expText);
                            }
                            else
                            {
                                if (Exit(expText) == false)
                                {
                                    return null;
                                }
                                else
                                {
                                    return childElement;
                                }

                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                }//end of foreach (IWebElement rowelemnt in tr_collection)
                //Exit crieteria          
            }//End of try

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace + "\n" + e.GetBaseException());
                return null;
            }//End of catch
            finally
            {
                pgeid = 1;
            }
            return childElement;
        }
        #endregion

        #region ReadingCellElementFromHtmltable
        /// <summary>
        /// Public method which includes logic related to read the cell element from table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.RemoteWebDriver for driver</param>
        /// <param name="pageIdNumber">Parameter of type System.String for pageIdNumber</param>
        /// <param name="nextPage">Parameter of Enum type By .</param>
        /// <param name="tablePropertyName">Parameter of type System.String for tablePropertyName</param>
        /// <param name="tablePropertyValue">Parameter of type System.String for tablePropertyvalue</param>
        /// <param name="rowPropertyName">Parameter of type System.String for rowPropertyName</param>
        /// <param name="rowPorpertyvalue">Parameter of type System.String for rowPropertyvalue</param>
        /// <param name="cellPropertyName">Parameter of type System.String for cellPropertyName</param>
        /// <param name="cellPropertyValue">Parameter of type System.String for cellPropertyValue</param>
        /// <param name="expText">Parameter of type System.String for pageIdNumber</param>
        /// <returns></returns>
        public static IWebElement ReadingCellElementFromHtmltable(this RemoteWebDriver driver, int pageIdNumber, By nextPage,
            string tablePropertyName, string tablePropertyValue, string rowPropertyName, string rowPorpertyvalue, string cellPropertyName, string cellPropertyValue, string expText)
        {
            text = string.Empty;
            childElement = null;
            IWebElement tableElement = null;
            IList<IWebElement> trCollection = null;
            IList<IWebElement> tdCollection = null;
            //Capturing the error if any using try-catch
            try
            {
                switch (tablePropertyName.ToUpper())
                {

                    case "NAME":
                        tableElement = driver.FindElementByName(tablePropertyValue);
                        break;
                    case "CLASSNAME":
                        tableElement = driver.FindElementByClassName(tablePropertyValue);
                        break;
                    case "XPATH":
                        tableElement = driver.FindElementByXPath(tablePropertyValue);
                        break;
                    case "ID":
                    default:
                        tableElement = driver.FindElementById(tablePropertyValue);
                        break;
                }
                //Piece of code which should be executed before loop starts  
                CommonMethods.PlayWait(2000);
                //Collecting total no fo row elements for a page     
                switch (rowPropertyName.ToUpper())
                {

                    case "NAME":
                        trCollection = tableElement.FindElements(By.Name(rowPorpertyvalue));
                        break;
                    case "CLASSNAME":
                        trCollection = tableElement.FindElements(By.ClassName(rowPorpertyvalue));
                        break;
                    case "XPATH":
                        trCollection = tableElement.FindElements(By.XPath(rowPorpertyvalue));
                        break;
                    case "TAGNAME":
                        trCollection = tableElement.FindElements(By.TagName(rowPorpertyvalue));
                        break;
                    case "ID":
                    default:
                        trCollection = tableElement.FindElements(By.Id(rowPorpertyvalue));
                        break;
                }

                //Initilizing the rowno,cell no
                int rowno, celno;
                rowno = 1;
                //Assinging the each row element from a list
                for (int tdrow = 0; tdrow <= trCollection.Count; tdrow++)
                {
                    celno = 1;
                    //Collecting the total no of cells data for a page
                    switch (cellPropertyName.ToUpper())
                    {

                        case "NAME":
                            tdCollection = tableElement.FindElements(By.Name(cellPropertyValue));
                            break;
                        case "CLASSNAME":
                            tdCollection = tableElement.FindElements(By.ClassName(cellPropertyValue));
                            break;
                        case "XPATH":
                            tdCollection = tableElement.FindElements(By.XPath(cellPropertyValue));
                            break;
                        case "TAGNAME":
                            tdCollection = tableElement.FindElements(By.TagName(cellPropertyValue));
                            break;
                        case "ID":
                        default:
                            tdCollection = tableElement.FindElements(By.TagName(cellPropertyValue));
                            break;
                    }
                    //Assigning the each cell data from list
                    for (int tdcol = 0; tdcol <= tdCollection.Count; tdcol++)
                    {
                        //Retriving the each cell data
                        string actualData = (tdCollection[tdcol].Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;
                            childElement = tdCollection[tdcol];
                            if (Exit(expText) == false)
                            {
                                return null;
                            }
                            else
                            {
                                return childElement;
                            }
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count)
                        {
                            //Comparing the total no of pages
                            if (pageIdNumber != pgeid && !driver.FindElement(nextPage).GetAttribute("class").Contains("disabled"))
                            {
                                //Clicking on element
                                driver.ClickElement(driver.FindElement(nextPage));
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                return ReadingCellElementFromHtmltable(driver, pageIdNumber, nextPage, tablePropertyName,
                                   tablePropertyValue, rowPropertyName, rowPorpertyvalue, cellPropertyName, cellPropertyValue, expText);
                            }
                            else
                            {
                                if (Exit(expText) == false)
                                {
                                    return null;
                                }
                                else
                                {
                                    return childElement;
                                }

                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                }//end of foreach (IWebElement rowelemnt in tr_collection)
                //Exit crieteria          
            }//End of try

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace + "\n" + e.GetBaseException());
                return null;
            }//End of catch
            finally
            {
                pgeid = 1;
            }
            return childElement;
        }
        #endregion

        #region ClickonImgInHtmltable
        /// <summary>
        /// Public method which includes logic related to click on image html table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="nextPageLink">Parameter of type OpenQA.Selenium.IwebElement for WebElement</param>
        /// <param name="nextPageId">Parameter of type System.String for nextPageId.</param>
        /// <param name="tableElement">Parameter of type OpenQA.Selenium.IwebElement for WebElement</param>
        /// <param name="tableRow">Parameter of Enum type By .</param>
        /// <param name="tableCell">Parameter of Enum type By .</param>
        /// <param name="ExpText">Parameter of type System.String for ExpText.</param>
        /// <param name="PropertyName">Parameter of type System.String for strPropertyName.</param>
        /// <param name="ProeprtyValue">Parameter of type System.String for strProeprtyValue.</param>
        /// <param name="elementIndex">Parameter of type System.int for index.</param>
        /// <returns>True or False values</returns>
        public static bool ClickonImgInHtmltable(this RemoteWebDriver driver, int pageIdNumber, IWebElement nextPageLink,
            IWebElement tableElement, By tableRow, By tableCell, string expText, string propertyName, string propertyValue, int elementIndex)
        {

            //Capturing the error if any using try-catch
            try
            {
                //Initilising the elements                
                childElement = null;
                text = string.Empty;
                CommonMethods.PlayWait(1000);
                //Collecting total no fo row elements for a page              
                IList<IWebElement> trCollection = tableElement.FindElements(tableRow);
                //Initilizing the rowno,cell no
                int rowno, celno;
                rowno = 1;
                //Assinging the each row element from a list
                for (int trIndex = 0; trIndex <= trCollection.Count - 1; trIndex++)
                {
                    celno = 1;
                    //Collecting the total no of cells data for a page
                    IList<IWebElement> tdCollection = trCollection[trIndex].FindElements(tableCell);
                    //Assigning the each cell data from list
                    for (int tdIndex = 0; tdIndex <= tdCollection.Count - 1; tdIndex++)
                    {
                        //Retriving the each cell data
                        string actualData = (tdCollection[tdIndex].Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;
                            try
                            {
                                switch (propertyName.ToUpper())
                                {

                                    case "TAGNAME":
                                        childElement = tdCollection[tdIndex + elementIndex].FindElement(By.TagName(propertyValue));
                                        break;
                                    case "CLASSNAME":
                                        childElement = tdCollection[tdIndex + elementIndex].FindElement(By.ClassName(propertyValue));
                                        break;
                                    case "XPATH":
                                        childElement = tdCollection[tdIndex + elementIndex].FindElement(By.XPath(propertyValue));
                                        break;
                                    case "CSSSELECTOR":
                                        childElement = tdCollection[tdIndex + elementIndex].FindElement(By.CssSelector(propertyValue));
                                        break;
                                    case "ID":
                                    default:
                                        childElement = tdCollection[tdIndex + elementIndex].FindElement(By.Id(propertyValue));
                                        break;
                                }

                                if (childElement != null)
                                {
                                    if (childElement.GetAttribute("type") == "radio")
                                    {
                                        ClickOnRadioBtn(driver, childElement);

                                    }
                                    else
                                    {
                                        MouseHoverClickOnElement(driver, childElement);
                                    }

                                    if (Exit(expText) == false)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }//End of nested if
                            }
                            catch (Exception e1)
                            {
                                e1.GetBaseException();
                            }

                            //End of if
                            //Exit from loop
                            if (Exit(expText) == false)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count)
                        {
                            //Comparing the total no of pages
                            if (pageIdNumber != pgeid && !nextPageLink.GetAttribute("class").Contains("disabled"))
                            {
                                //Clicking on element
                                driver.ClickElement(nextPageLink);
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                return ClickonImgInHtmltable(driver, pageIdNumber, nextPageLink, tableElement,
                                    tableRow, tableCell, expText, propertyName, propertyValue, elementIndex);
                            }
                            else
                            {
                                if (Exit(expText) == false)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }

                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                }//end of foreach (IWebElement rowelemnt in tr_collection)
                //Exit crieteria     
            }

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace + "\n" + e.GetBaseException());
                return false;
            }//End of catch

            finally
            {
                pgeid = 1;
            }

            return true;
        }
        #endregion

        #region ClickonImgInHtmltable
        /// <summary>
        /// Public method which includes logic related to click on image html table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="pageIdNumber">Parameter of type System.String for pageIdNumber</param>
        /// <param name="nextPageXpath">Parameter of type System.String for nextpageXpath</param>
        /// <param name="tablePropertyName">Parameter of type System.String for tablePropertyName</param>
        /// <param name="tablePropertyValue">Parameter of type System.String for tablePropertyvalue</param>
        /// <param name="rowPropertyName">Parameter of type System.String for rowPropertyName</param>
        /// <param name="rowPorpertyvalue">Parameter of type System.String for rowPropertyvalue</param>
        /// <param name="cellPropertyName">Parameter of type System.String for cellPropertyName</param>
        /// <param name="cellPropertyValue">Parameter of type System.String for cellPropertyValue</param>
        /// <param name="expText">Parameter of type System.String for cellPropertyValue</param>
        /// <param name="PropertyName">Parameter of type System.String for strPropertyName.</param>
        /// <param name="ProeprtyValue">Parameter of type System.String for strProeprtyValue.</param>
        /// <param name="elementIndex">Parameter of type System.int for index.</param>
        /// <returns>True or False values</returns>
        public static bool ClickonImgInHtmltable(this RemoteWebDriver driver, int pageIdNumber, IWebElement nextPageXpath,
            string tablePropertyName, string tablePropertyValue, string rowPropertyName,
            string rowPorpertyvalue, string cellPropertyName, string cellPropertyValue, string expText,
            string propertyName, string propertyValue, int elementIndex)
        {
            text = string.Empty;
            childElement = null;
            IWebElement tableElement = null;
            IList<IWebElement> trCollection = null;
            IList<IWebElement> tdCollection = null;
            //Capturing the error if any using try-catch
            try
            {
                switch (tablePropertyName.ToUpper())
                {

                    case "NAME":
                        tableElement = driver.FindElementByName(tablePropertyValue);
                        break;
                    case "CLASSNAME":
                        tableElement = driver.FindElementByClassName(tablePropertyValue);
                        break;
                    case "XPATH":
                        tableElement = driver.FindElementByXPath(tablePropertyValue);
                        break;
                    case "ID":
                    default:
                        tableElement = driver.FindElementById(tablePropertyValue);
                        break;
                }
                //Piece of code which should be executed before loop starts  
                CommonMethods.PlayWait(2000);
                //Collecting total no fo row elements for a page     
                switch (rowPropertyName.ToUpper())
                {

                    case "NAME":
                        trCollection = tableElement.FindElements(By.Name(rowPorpertyvalue));
                        break;
                    case "CLASSNAME":
                        trCollection = tableElement.FindElements(By.ClassName(rowPorpertyvalue));
                        break;
                    case "XPATH":
                        trCollection = tableElement.FindElements(By.XPath(rowPorpertyvalue));
                        break;
                    case "TAGNAME":
                        trCollection = tableElement.FindElements(By.TagName(rowPorpertyvalue));
                        break;
                    case "ID":
                    default:
                        trCollection = tableElement.FindElements(By.Id(rowPorpertyvalue));
                        break;
                }

                //Initilizing the rowno,cell no
                int rowno, celno;
                rowno = 1;
                //Assinging the each row element from a list
                for (int tdrow = 0; tdrow <= trCollection.Count; tdrow++)
                {
                    celno = 1;
                    //Collecting the total no of cells data for a page
                    switch (cellPropertyName.ToUpper())
                    {

                        case "NAME":
                            tdCollection = trCollection[tdrow].FindElements(By.Name(cellPropertyValue));
                            if (tdCollection.Count == 0)
                            {
                                tdCollection = tableElement.FindElements(By.Name(cellPropertyValue));
                            }
                            break;
                        case "CLASSNAME":
                            tdCollection = trCollection[tdrow].FindElements(By.ClassName(cellPropertyValue));
                            if (tdCollection.Count == 0)
                            {
                                tdCollection = tableElement.FindElements(By.ClassName(cellPropertyValue));
                            }
                            break;
                        case "XPATH":
                            tdCollection = trCollection[tdrow].FindElements(By.XPath(cellPropertyValue));
                            if (tdCollection.Count == 0)
                            {
                                tdCollection = tableElement.FindElements(By.XPath(cellPropertyValue));
                            }
                            break;
                        case "TAGNAME":
                            tdCollection = trCollection[tdrow].FindElements(By.TagName(cellPropertyValue));
                            if (tdCollection.Count == 0)
                            {
                                tdCollection = tableElement.FindElements(By.TagName(cellPropertyValue));
                            }
                            break;
                        case "ID":
                        default:
                            tdCollection = trCollection[tdrow].FindElements(By.Id(cellPropertyValue));
                            if (tdCollection.Count == 0)
                            {
                                tdCollection = tableElement.FindElements(By.Id(cellPropertyValue));
                            }
                            break;
                    }
                    //Assigning the each cell data from list
                    for (int tdcol = 0; tdcol <= tdCollection.Count; tdcol++)
                    {
                        //Retriving the each cell data
                        string actualData = (tdCollection[tdcol].Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;
                            try
                            {
                                switch (propertyName.ToUpper())
                                {

                                    case "TAGNAME":
                                        childElement = tdCollection[tdrow + elementIndex].FindElement(By.TagName(propertyValue));
                                        if (childElement == null)
                                        {
                                            childElement = tdCollection[elementIndex].FindElement(By.TagName(propertyValue));
                                        }
                                        break;
                                    case "CLASSNAME":
                                        childElement = tdCollection[tdrow + elementIndex].FindElement(By.ClassName(propertyValue));
                                        if (childElement == null)
                                        {
                                            childElement = tdCollection[elementIndex].FindElement(By.TagName(propertyValue));
                                        }
                                        break;
                                    case "XPATH":
                                        childElement = tdCollection[tdrow + elementIndex].FindElement(By.XPath(propertyValue));
                                        if (childElement == null)
                                        {
                                            childElement = tdCollection[elementIndex].FindElement(By.TagName(propertyValue));
                                        }
                                        break;
                                    case "CSSSELECTOR":
                                        childElement = tdCollection[tdrow + elementIndex].FindElement(By.CssSelector(propertyValue));
                                        if (childElement == null)
                                        {
                                            childElement = tdCollection[elementIndex].FindElement(By.TagName(propertyValue));
                                        }
                                        break;
                                    case "ID":
                                    default:
                                        childElement = tdCollection[tdrow + elementIndex].FindElement(By.Id(propertyValue));
                                        if (childElement == null)
                                        {
                                            childElement = tdCollection[elementIndex].FindElement(By.TagName(propertyValue));
                                        }
                                        break;
                                }

                                if (childElement != null)
                                {
                                    if (childElement.GetAttribute("type") == "radio")
                                    {
                                        ClickOnRadioBtn(driver, childElement);

                                    }
                                    else
                                    {
                                        MouseHoverClickOnElement(driver, childElement);
                                    }

                                    if (Exit(expText) == false)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }//End of nested if
                            }
                            catch (Exception e1)
                            {
                                e1.GetBaseException();
                            }


                            //End of if
                            //Exit from loop
                            if (Exit(expText) == false)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count)
                        {
                            //Comparing the total no of pages
                            if (pageIdNumber != pgeid && !nextPageXpath.GetAttribute("class").Contains("disabled"))
                            {
                                //Clicking on element
                                driver.ClickElement(nextPageXpath);
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                return ClickonImgInHtmltable(driver, pageIdNumber, nextPageXpath, tablePropertyName,
                                    tablePropertyValue, rowPropertyName, rowPorpertyvalue,
                                    cellPropertyName, cellPropertyValue, expText, propertyName, propertyValue, elementIndex);
                            }
                            else
                            {
                                if (Exit(expText) == false)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }

                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                }//end of foreach (IWebElement rowelemnt in tr_collection)
                //Exit crieteria     
            }

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace + "\n" + e.GetBaseException());
                return false;
            }//End of catch

            finally
            {
                pgeid = 1;
            }
            return true;
        }
        #endregion

        #region GetElementsFromTableCell
        /// <summary>
        /// Public method which includes logic related to click on image html table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="nextPageLink">Parameter of type OpenQA.Selenium.IwebElement for WebElement</param>
        /// <param name="tableElement">Parameter of type OpenQA.Selenium.IwebElement for WebElement</param>
        /// <param name="pageIdNumber">Parameter of type System.String for nextPageId.</param>
        /// <param name="tableRow">Parameter of Enum type By .</param>
        /// <param name="tableCell">Parameter of Enum type By .</param>
        /// <param name="ExpText">Parameter of type System.String for ExpText.</param>
        /// <param name="PropertyName">Parameter of type System.String for strPropertyName.</param>
        /// <param name="ProeprtyValue">Parameter of type System.String for strProeprtyValue.</param>
        /// <param name="elementIndex">Parameter of type System.int for index.</param>
        /// <returns>True or False values</returns>
        public static IList<IWebElement> GetElementsFromTableCell(this RemoteWebDriver driver, int pageIdNumber, IWebElement nextPageLink,
            IWebElement tableElement, By tableRow, By tableCell, string expText, string propertyName, string propertyValue, int elementIndex)
        {
            text = string.Empty;
            //Capturing the error if any using try-catch
            try
            {
                //Piece of code which should be executed before loop starts
                CommonMethods.PlayWait(1000);
                //Collecting total no fo row elements for a page              
                IList<IWebElement> trCollection = tableElement.FindElements(tableRow);
                //Initilizing the rowno,cell no
                int rowno, celno;
                rowno = 1;
                //Assinging the each row element from a list
                for (int trIndex = 0; trIndex <= trCollection.Count - 1; trIndex++)
                {
                    celno = 1;
                    //Collecting the total no of cells data for a page
                    IList<IWebElement> tdCollection = trCollection[trIndex].FindElements(tableCell);
                    //Assigning the each cell data from list
                    for (int tdIndex = 0; tdIndex <= tdCollection.Count - 1; tdIndex++)
                    {
                        //Retriving the each cell data
                        string actualData = (tdCollection[tdIndex].Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;

                            try
                            {
                                switch (propertyName.ToUpper())
                                {

                                    case "TAGNAME":
                                        childElements = tdCollection[tdIndex + elementIndex].FindElements(By.TagName(propertyValue));
                                        break;
                                    case "CLASSNAME":
                                        childElements = tdCollection[tdIndex + elementIndex].FindElements(By.ClassName(propertyValue));
                                        break;
                                    case "XPATH":
                                        childElements = tdCollection[tdIndex + elementIndex].FindElements(By.XPath(propertyValue));
                                        break;
                                    case "ID":
                                    default:
                                        childElements = tdCollection[tdIndex + elementIndex].FindElements(By.Id(propertyValue));
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                e.GetBaseException();
                            }

                            //End of if
                            //Exit from loop
                            if (Exit(expText) == false)
                            {
                                return null;
                            }
                            else
                            {
                                return childElements;
                            }
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count)
                        {
                            //Comparing the total no of pages
                            if (pageIdNumber != pgeid && !nextPageLink.GetAttribute("class").Contains("disabled"))
                            {
                                //Clicking on element
                                driver.ClickElement(nextPageLink);
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                return GetElementsFromTableCell(driver, pageIdNumber, nextPageLink, tableElement,
                                    tableRow, tableCell, expText, propertyName, propertyValue, elementIndex);
                            }
                            else
                            {
                                if (Exit(expText) == false)
                                {
                                    return null;
                                }
                                else
                                {
                                    return childElements;
                                }
                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                }//end of foreach (IWebElement rowelemnt in tr_collection)
                 //Exit crieteria           
            }//End of try

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace + "\n" + e.GetBaseException());
                return null;
            }//End of catch
            finally
            {
                pgeid = 1;
            }
            return childElements;
        }
        #endregion

        #region ClickonImgInHtmltableByTagName
        /// <summary>
        /// Public method which includes logic related to click on image html table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="tableElement">Parameter of type OpenQA.Selenium.IwebElement for WebElement</param>
        /// <param name="nextPage">Parameter of Enum type By </param>
        /// <param name="ExpText">Parameter of type System.String for ExpText.</param>
        /// <param name="PropertyName">Parameter of type System.String for strPropertyName.</param>
        /// <param name="ProeprtyValue">Parameter of type System.String for strProeprtyValue.</param>
        /// <param name="elementIndex">Parameter of type System.int for index.</param>
        /// <returns>True or False values</returns>
        public static bool ClickonImgInHtmltableByTagName(this RemoteWebDriver driver, int pageIdNumber, By nextPage,
            IWebElement tableElement, string expText, string propertyName, string propertyValue, int elementIndex)
        {
            //Capturing the error if any using try-catch
            try
            {
                text = string.Empty;
                CommonMethods.PlayWait(1000);
                //Collecting total no fo row elements for a page              
                IList<IWebElement> trCollection = tableElement.FindElements(By.TagName("tr"));
                //Initilizing the rowno,cell no
                int rowno, celno;
                rowno = 1;
                //Assinging the each row element from a list
                for (int trIndex = 0; trIndex <= trCollection.Count - 1; trIndex++)
                {
                    celno = 1;
                    //Collecting the total no of cells data for a page
                    IList<IWebElement> tdCollection = tableElement.FindElements(By.TagName("td"));
                    //Assigning the each cell data from list
                    for (int tdIndex = 0; tdIndex <= tdCollection.Count - 1; tdIndex++)
                    {
                        //Retriving the each cell data
                        string actualData = (tdCollection[tdIndex].Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;
                            try
                            {
                                switch (propertyName.ToUpper())
                                {

                                    case "TAGNAME":
                                        childElement = tdCollection[elementIndex].FindElement(By.TagName(propertyValue));
                                        break;
                                    case "CLASSNAME":
                                        childElement = tdCollection[elementIndex].FindElement(By.ClassName(propertyValue));
                                        break;
                                    case "XPATH":
                                        childElement = tdCollection[elementIndex].FindElement(By.XPath(propertyValue));
                                        break;
                                    case "ID":
                                    default:
                                        childElement = tdCollection[elementIndex].FindElement(By.Id(propertyValue));
                                        break;
                                }

                                if (childElement != null)
                                {
                                    if (childElement.GetAttribute("type") == "radio")
                                    {
                                        ClickOnRadioBtn(driver, childElement);

                                    }
                                    else
                                    {
                                        CommonMethods.PlayWait(2000);
                                        childElement.Click();
                                    }

                                    if (Exit(expText) == false)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }//End of nested if
                            }
                            catch (Exception e)
                            {
                                e.GetBaseException();
                            }

                            //End of if
                            //Exit from loop
                            if (Exit(expText) == false)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count)
                        {
                            //Comparing the total no of pages
                            if (pageIdNumber != pgeid)
                            {
                                //Clicking on element
                                driver.ClickOnElement(nextPage);
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                return ClickonImgInHtmltableByTagName(driver, pageIdNumber, nextPage, tableElement,
                                    expText, propertyName, propertyValue, elementIndex);
                            }
                            else
                            {
                                if (Exit(expText) == false)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }

                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                }//end of foreach (IWebElement rowelemnt in tr_collection)            
            }//End of try

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace + "\n" + e.GetBaseException());
                return false;
            }//End of catch

            finally
            {
                pgeid = 1;
            }

            return true;
        }
        #endregion ClickonImgInHtmltableByTagName

        #region ClickOnButtonFromGrid
        /// <summary>
        /// Public method which includes logic related to click on image html table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="tableElement">Parameter of type OpenQA.Selenium.IwebElement for WebElement</param>
        /// <param name="nextPage">Parameter of Enum type By </param>
        /// <param name="ExpText">Parameter of type System.String for ExpText.</param>
        /// <param name="PropertyName">Parameter of type System.String for strPropertyName.</param>
        /// <param name="ProeprtyValue">Parameter of type System.String for strProeprtyValue.</param>
        /// <param name="elementIndex">Parameter of type System.int for index.</param>
        /// <returns>True or False values</returns>
        public static bool ClickOnButtonFromGrid(this RemoteWebDriver driver, int pageIdNumber, By nextPageId,
            IWebElement tableElement, string expText, string propertyName, string propertyValue)
        {
            text = string.Empty;

            //Capturing the error if any using try-catch
            try
            {
                CommonMethods.PlayWait(1000);
                //Collecting total no fo row elements for a page              
                IList<IWebElement> trCollection = tableElement.FindElements(By.TagName("tr"));
                //Initilizing the rowno,cell no
                int rowno, celno;
                rowno = 1;
                //Assinging the each row element from a list
                for (int trIndex = 0; trIndex <= trCollection.Count - 1; trIndex++)
                {
                    celno = 1;
                    //Collecting the total no of cells data for a page
                    IList<IWebElement> tdCollection = trCollection[trIndex].FindElements(By.TagName("td"));
                    //Assigning the each cell data from list
                    for (int tdIndex = 0; tdIndex <= tdCollection.Count - 1; tdIndex++)
                    {
                        //Retriving the each cell data
                        string actualData = (tdCollection[tdIndex].Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;
                            try
                            {
                                switch (propertyName.ToUpper())
                                {

                                    case "TAGNAME":
                                        childElement = trCollection[trIndex].FindElement(By.TagName(propertyValue));
                                        break;
                                    case "CLASSNAME":
                                        childElement = trCollection[trIndex].FindElement(By.ClassName(propertyValue));
                                        break;
                                    case "XPATH":
                                        childElement = trCollection[trIndex].FindElement(By.XPath(propertyValue));
                                        break;
                                    case "ID":
                                    default:
                                        childElement = trCollection[trIndex].FindElement(By.Id(propertyValue));
                                        break;
                                }

                                if (childElement != null)
                                {
                                    if (childElement.GetAttribute("type") == "radio")
                                    {
                                        driver.ClickOnRadioBtn(childElement);

                                    }
                                    else
                                    {
                                        CommonMethods.PlayWait(2000);
                                        childElement.Click();
                                    }

                                    if (Exit(expText) == false)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }//End of nested if
                            }
                            catch (Exception e)
                            {
                                e.GetBaseException();
                            }

                            //End of if
                            //Exit from loop
                            if (Exit(expText) == false)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count && rowno == trCollection.Count)
                        {
                            //Comparing the total no of pages
                            if (pageIdNumber != pgeid)
                            {
                                //Clicking on element
                                driver.ClickOnElement(nextPageId);
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                return ClickOnButtonFromGrid(driver, pageIdNumber, nextPageId,
                                    tableElement, expText, propertyName, propertyValue);
                            }
                            else
                            {
                                if (Exit(expText) == false)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }

                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                }//end of foreach (IWebElement rowelemnt in tr_collection)
                 //Exit crieteria           
            }//End of try

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace + "\n" + e.GetBaseException());
                return false;
            }//End of catch
            finally
            {
                pgeid = 1;
            }
            return true;
        }
        #endregion ClickOnButtonFromGrid

        #region VerifyRecorddatafromHtmltable
        /// <summary>
        /// Public method which includes logic related to verify the record from html table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="nextPageLink">Parameter of type OpenQA.Selenium.IwebElement for noofPagesIdElement.</param>
        /// <param name="tableElement">Parameter of type OpenQA.Selenium.IwebElement for noofPagesIdElement.</param>
        /// <param name="pgeIdnumber">Parameter of type System.String for TableId.</param>
        /// <param name="tableRow">Parameter of Enum type By .</param>
        /// <param name="tableCell">Parameter of Enum type By .</param>
        /// <param name="ExpText">Parameter of type System.String for ExpText.</param>
        /// <param name="strchr">Parameter of type System.String for strchr.</param>
        /// <returns>True or False</returns>
        public static bool VerifyRecorddatafromHtmltable(this RemoteWebDriver driver, int pgeIdnumber, IWebElement nextPageLink, IWebElement tableElement, By tableRow, By tableCell, string expText, string charValue)
        {
            //Capturing the error if any using try-catch
            try
            {
                text = string.Empty;
                //Collecting total no fo row elements for a page
                IList<IWebElement> trCollection = tableElement.FindElements(tableRow);
                //Initilizing the rowno,cell no
                int rowno, celno, trno;
                rowno = 1;
                trno = 0;
                //Assinging the each row element from a list
                for (int tdrow = 0; tdrow <= trCollection.Count; tdrow++)
                {
                    celno = 0;
                    //Collecting the total no of cells data for a page
                    IList<IWebElement> tdCollection = trCollection[tdrow].FindElements(tableRow);
                    //Assigning the each cell data from list
                    for (int tdcol = 0; tdcol <= tdCollection.Count; tdcol++)
                    {
                        //Retriving the each cell data
                        string actualData = (tdCollection[tdcol].Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;
                            if (charValue != "")
                            {
                                for (int itr = celno; itr <= tdCollection.Count; itr++)
                                {
                                    if (tdCollection[itr].Text.Trim() == (charValue))
                                    {
                                        if (Exit(expText) == false)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            return true;
                                        }
                                    }//End of nested if
                                }//End of for
                            }//End of if

                            //Exit from loop
                            if (Exit(expText) == false)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count - 1)
                        {
                            //Comparing the total no of pages
                            if (pgeIdnumber != pgeid)
                            {
                                //Clicking on element
                                driver.ClickElement(nextPageLink);
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                return VerifyRecorddatafromHtmltable(driver, pageIdNumber, nextPageLink, tableElement,
                                    tableRow, tableCell, expText, charValue);
                            }
                            else
                            {
                                if (Exit(expText) == false)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }

                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                    trno = trno + 1;
                }//end of foreach (IWebElement rowelemnt in tr_collection)
                 //Exit crieteria           
            }//End of try

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace + "\n" + e.GetBaseException());
                return false;
            }//End of catch
            finally
            {
                pgeid = 1;
            }
            return true;
        }
        #endregion

        #region GetCellIndexfromHtmltable
        /// <summary>
        /// Public method which includes logic related to get the Cell index from html table
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="tableElement">Parameter of type OpenQA.Selenium.IwebElement for noofPagesIdElement.</param>
        /// <param name="nextPageId">Parameter of type OpenQA.Selenium.IwebElement for nextPageId.</param>
        /// <param name="nextPageId">Parameter of Enum type By </param>
        /// <param name="tableRow">Parameter of Enum type By .</param>
        /// <param name="tableCell">Parameter of Enum type By .</param>
        /// <param name="ExpText">Parameter of type System.String for ExpText.</param>
        /// <returns>Cell number</returns>
        public static int GetCellIndexfromHtmltable(this RemoteWebDriver driver, IWebElement tableElement, int pgeIdnumber, By nextPageId, By tableRow, By tableCell, string expText)
        {
            //Capturing the error if any using try-catch
            try
            {
                //Collecting total no fo row elements for a page
                IList<IWebElement> trCollection = tableElement.FindElements(tableRow);
                //Initilizing the rowno,cell no
                int rowno, trno;
                rowno = 1;
                trno = 0;
                //Assinging the each row element from a list
                foreach (IWebElement rowelemnt in trCollection)
                {
                    celno = 0;
                    //Collecting the total no of cells data for a page
                    IList<IWebElement> tdCollection = rowelemnt.FindElements(tableCell);
                    //Assigning the each cell data from list
                    foreach (IWebElement colement in tdCollection)
                    {
                        //Retriving the each cell data
                        string actualData = (colement.Text).Trim();
                        //Comparing the data
                        if (actualData.ToUpper() == expText.ToUpper())
                        {
                            text = actualData;
                            //Exit from loop
                            return celno;
                        }
                        //Clicking on page next and repeating the process
                        else if (celno == tdCollection.Count - 1)
                        {
                            //Comparing the total no of pages
                            if (pgeIdnumber != pgeid)
                            {
                                //Clicking on element
                                driver.ClickOnElement(nextPageId);
                                pgeid = pgeid + 1;
                                CommonMethods.PlayWait(1000);
                                // return driver.GetCellIndexfromHtmltable(pageIdNumber, nextPageId, tableElement, tableRow, tableCell, expText);
                            }
                            else
                            {
                                return celno;

                            }
                        }//end of else if (celno == td_collection.Count)
                        //Increamnt of cell value for each iteartion 
                        celno = celno + 1;
                    }//end of foreach (IWebElement colement in td_collection)
                    //Increment of row value for each iteration
                    rowno = rowno + 1;
                    trno = trno + 1;
                }//end of foreach (IWebElement rowelemnt in tr_collection)
                return celno;
            }

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return -1;
            }
            finally
            {
                pgeid = 1;
            }
        }
        #endregion

        #region GetRecordsCountfromHtmltable
        /// <summary>
        /// Public method which includes logic related to Get the records count from html talbe
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="tableElement">Parameter of type System.String for TableId.</param>
        /// <param name="tableRow">Parameter of Enum type By .</param>
        /// <returns>Records count</returns>
        public static int GetRecordsCountfromHtmltable(this RemoteWebDriver driver, IWebElement tableElement, By tableRow)
        {
            //Capturing the error if any using try-catch
            try
            {
                //Collecting total no fo row elements for a page
                IList<IWebElement> trCollection = tableElement.FindElements(tableRow);
                return trCollection.Count;
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return 0;
            }
        }
        #endregion

        #region ChildObjects
        /// <summary>
        /// Public method which includes logic related to get the collection of elements from a parent element
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="PropertyName">Parameter of type System.String for PropertyName</param>
        /// <param name="propertyValue">Parameter of Enum type By .</param>
        /// <returns>Collection of Webelements</returns>
        public static IList<IWebElement> ChildObjects(this RemoteWebDriver driver,By propertyValue, string propertyName)
        {
            //Capturing the error if any using try-catch
            try
            {
                //Retrivng the webelements
                switch (propertyName)
                {
                    case "TagName":
                        folders = driver.FindElements(propertyValue);
                        return folders;
                    case "ClassName":
                        folders = driver.FindElements(propertyValue);
                        return folders;
                    case "Id":
                        folders = driver.FindElements(propertyValue);
                        return folders;
                    case "Xpath":
                    default:
                        folders = driver.FindElements(propertyValue);
                        return folders;
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return null;
            }
        }
        #endregion

        #region DragAndDrop
        /// <summary>
        /// Public method which includes logic related to Drag and drop the elements
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.Selenium.IwebDrvier for driver.</param>
        /// <param name="SourceElemnt">Parameter of type OpenQA.Selenium.IwebElement for SourceElement.</param>
        /// <param name="DestinationElmnt">Parameter of type OpenQA.Selenium.IwebElement for DestinationElement.</param>
        public static void DragAndDrop(this RemoteWebDriver driver, IWebElement sourceElement, IWebElement destinationElement)
        {
            //Capturing the error if any using try-catch
            try
            {
                //Drag and drop
                Actions action = new Actions(driver);
                action.DragAndDrop(sourceElement, destinationElement).Perform();
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region CaptureLogAndScreenShot
        /// <summary>
        /// Public method which includes logic related to Capturing the loig and screenshot
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.RemoteWebDriver for driver</param>
        /// <param name="scenarioName">Parameter of type System.String for scenarioName</param>
        /// <param name="stepDetails">Parameter of type System.String for stepDetails</param>
        /// <param name="logPath">Parameter of type System.String for logPath</param>
        public static void CaptureLogAndScreenShot(RemoteWebDriver driver, string scenarioName, string stepDetails, string logPath)
        {
            try
            {
                // CommonMethods.CreateLog(scenarioName + ":" + stepDetails);
                CaptureSnapshot(driver, logPath, scenarioName + DateTime.Now.ToString(resultsDateFormat));
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region GetRowElementFromHtmlTable
        /// <summary>
        /// Public method which includes logic related to get the row element from webtable
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.RemoteWebDriver for driver</param>
        /// <param name="pageIdNumber">Parameter of type System.string for pageIdNumber</param>
        /// <param name="nextPageBtnPropertyName">Parameter of type System.string for nextPageBtnPropertyName</param>
        /// <param name="nextPageBtnPropertyValue">Parameter of type System.string for nextPageBtnPropertyValue</param>
        /// <param name="tablePropertyName">Parameter of type System.string for tablePropertyName</param>
        /// <param name="tablePropertyValue">Parameter of type System.string for tablePropertyValue</param>
        /// <param name="rowPropertyName">Parameter of type System.string for rowPropertyName</param>
        /// <param name="rowPorpertyvalue">Parameter of type System.string for rowPorpertyvalue</param>
        /// <param name="expRowText">Parameter of type System.string for expRowText</param>
        /// <param name="elementAttributeName">Parameter of type System.string for elementAttributeName</param>
        /// <param name="elementAttributeValue">Parameter of type System.string for elementAttributeValue</param>
        /// <returns>Parameter of type OpenQA.IwebElement for rowElement</returns>
        public static IWebElement GetRowElementFromHtmlTable(this RemoteWebDriver driver, int pageIdNumber, string nextPageBtnPropertyName,
            string nextPageBtnPropertyValue, string tablePropertyName, string tablePropertyValue, string rowPropertyName, string rowPorpertyvalue, string expRowText, string elementAttributeName, string elementAttributeValue)
        {
            text = string.Empty;
            childElement = null;
            IWebElement tableElement = null;
            IList<IWebElement> trCollection = null;
            //Capturing the error if any using try-catch
            try
            {
                switch (tablePropertyName.ToUpper())
                {

                    case "NAME":
                        tableElement = driver.FindElementById(tablePropertyValue);
                        break;
                    case "CLASSNAME":
                        tableElement = driver.FindElementByClassName(tablePropertyValue);
                        break;
                    case "XPATH":
                        tableElement = driver.FindElementByXPath(tablePropertyValue);
                        break;
                    case "ID":
                    default:
                        tableElement = driver.FindElementById(tablePropertyValue);
                        break;
                }
                //Piece of code which should be executed before loop starts  
                CommonMethods.PlayWait(2000);
                //Collecting total no fo row elements for a page     
                switch (rowPropertyName.ToUpper())
                {

                    case "NAME":
                        trCollection = tableElement.FindElements(By.Name(rowPorpertyvalue));
                        break;
                    case "CLASSNAME":
                        trCollection = tableElement.FindElements(By.ClassName(rowPorpertyvalue));
                        break;
                    case "XPATH":
                        trCollection = tableElement.FindElements(By.XPath(rowPorpertyvalue));
                        break;
                    case "TAGNAME":
                        trCollection = tableElement.FindElements(By.TagName(rowPorpertyvalue));
                        break;
                    case "ID":
                    default:
                        trCollection = tableElement.FindElements(By.Id(rowPorpertyvalue));
                        break;
                }

                for (int rowIndex = 0; rowIndex <= trCollection.Count - 1; rowIndex++)
                {
                    if (expRowText != "")
                    {
                        if (trCollection[rowIndex].Text.ToUpper().Contains(expRowText.ToUpper().Trim()))
                        {
                            return trCollection[rowIndex];
                        }
                        else if (pgeid != pageIdNumber && rowIndex == trCollection.Count - 1)
                        {
                            if (pageIdNumber == 0)
                            {
                                return null;
                            }
                            switch (nextPageBtnPropertyName.ToUpper())
                            {

                                case "NAME":
                                    if (driver.ClickOnElement(nextPageBtnPropertyValue) == false)
                                    {
                                        return GetRowElementFromHtmlTable(driver, pageIdNumber, nextPageBtnPropertyName,
                                            nextPageBtnPropertyValue, tablePropertyName, tablePropertyName, rowPropertyName, rowPorpertyvalue, expRowText, elementAttributeName, elementAttributeValue);
                                    }
                                    break;
                                case "LINKTEXT":
                                    if (driver.ClickOnElement(nextPageBtnPropertyValue) == false)
                                    {
                                        return GetRowElementFromHtmlTable(driver, pageIdNumber, nextPageBtnPropertyName,
                                            nextPageBtnPropertyValue, tablePropertyName, tablePropertyName, rowPropertyName, rowPorpertyvalue, expRowText, elementAttributeName, elementAttributeValue);
                                    }
                                    break;
                                case "XPATH":
                                default:
                                    if (driver.ClickOnElement(nextPageBtnPropertyValue) == false)
                                    {
                                        return GetRowElementFromHtmlTable(driver, pageIdNumber, nextPageBtnPropertyName,
                                            nextPageBtnPropertyValue, tablePropertyName, tablePropertyName, rowPropertyName, rowPorpertyvalue, expRowText, elementAttributeName, elementAttributeValue);
                                    }
                                    break;
                            }
                        }
                    }
                    else if (elementAttributeName != "")
                    {
                        if (trCollection[rowIndex].GetAttribute(elementAttributeName) == elementAttributeValue)
                        {
                            return trCollection[rowIndex];
                        }
                    }

                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return null;
            }
            finally
            {
                pgeid = 1;
            }
            return null;
        }
        #endregion 

        #region GetElementFromRowElement
        /// <summary>
        /// Public methid which includes logic related to get the element from the specified cell in a row
        /// To retrieve the Image kind of elements it is mandatory to pass its attributes
        /// </summary>
        /// <param name="driver">Parameter of type OpenQA.RemoteWebDriver</param>
        /// <param name="rowElement">Parameter of type OpenQA.IWebElement</param>
        /// <param name="type">Parameter of type System.String</param>
        /// <param name="tagNameValue">Parameter of Enum type By .</param>
        /// <param name="attributeName">Parameter of type System.string</param>
        /// <param name="attributeValue">Parameter of type System.string</param>
        /// <param name="expCellText">Parameter of type System.string</param>
        /// <returns>Parameter of type OpenQA.IWebElement</returns>
        public static IWebElement GetElementFromRowElement(this RemoteWebDriver driver, IWebElement rowElement, string type, By tagNameValue, string attributeName, string attributeValue, string expCellText)
        {
            IList<IWebElement> tdCollection = rowElement.FindElements(By.TagName("td"));
            for (int tdIndex = 0; tdIndex <= tdCollection.Count - 1; tdIndex++)
            {
                try
                {
                    switch (type.ToUpper())
                    {
                        case "LINK":
                            if (tdCollection[tdIndex].FindElement(tagNameValue).Text.ToUpper().Trim() == expCellText.ToUpper().Trim())
                            {
                                return tdCollection[tdIndex].FindElement(tagNameValue);
                            }
                            break;

                        case "IMAGE":

                            if (tdCollection[tdIndex].FindElement(tagNameValue).GetAttribute(attributeName).ToUpper().Trim() == attributeValue.ToUpper().Trim())
                            {
                                return tdCollection[tdIndex].FindElement(tagNameValue);
                            }
                            break;

                        case "TEXT":
                            string ccc = tdCollection[tdIndex].FindElement(tagNameValue).GetAttribute(attributeName).ToUpper().Trim();
                            if (tdCollection[tdIndex].FindElement(tagNameValue).GetAttribute(attributeName).ToUpper().Trim() == attributeValue.ToUpper().Trim())
                            {
                                return tdCollection[tdIndex].FindElement(tagNameValue);
                            }
                            break;

                        case "NONE":
                        default:
                            if (tdCollection[tdIndex].Text.ToUpper().Trim() == expCellText.ToUpper().Trim())
                            {
                                return tdCollection[tdIndex];
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                }
            }
            return null;
        }
        #endregion

        #region Exit

        /// <summary>
        /// Exit
        /// </summary>
        /// <param name="expText"></param>
        /// <returns></returns>
        public static bool Exit(string expText)
        {
            if (text.ToUpper() != expText.ToUpper())
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Select Kendo Drop down using By

        /// <summary>
        /// Select values from Kendo drop down
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SelectKendoDropDownAndValue(this RemoteWebDriver driver, By by, string value)
        {
            try
            {
                IWebElement element = driver.FindElement(by);
                if (element != null)
                {
                    element.Click();
                    element.SendKeys(Keys.Control + "a");
                    CommonMethods.PlayWait(1000);
                    element.SendKeys(value);
                    CommonMethods.PlayWait(2000);
                    element.SendKeys(Keys.Enter);
                    return true;
                }
                else
                {
                    throw new NoSuchElementException();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region Kendo Drop down       
        /// <summary>
        /// Select kendo dropdown and add the value
        /// </summary>
        /// <param name="driver">Driver</param>
        /// <param name="dropdownName">Name of the dropdown to add text</param>
        /// <param name="dropdownValue">Value of the drop to fill</param>
        /// <param name="clickDownArrow"></param>
        /// <returns></returns>
        public static bool SelectKendoDropdownAndAddValue(this RemoteWebDriver driver, string dropdownName, string dropdownValue, bool clickDownArrow = false)
        {
            CommonMethods.PlayWait(2000);
            try
            {
                IWebElement element = null;
                try
                {
                    
                    element= driver.FindElement(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//input", dropdownName)));
                }
                catch (Exception)
                {
                    element = driver.FindElement(By.XPath(string.Format("//*[normalize-space(.)='{0}']//input", dropdownName)));
                }

                if (element != null)
                {
                    element.Click();
                    element.SendKeys(Keys.Control + "a");
                    element.SendKeys(dropdownValue);
                    CommonMethods.PlayWait(4000);
                    if(clickDownArrow)
                    {
                        element.SendKeys(Keys.ArrowDown);
                        CommonMethods.PlayWait(1000);
                    }
                    element.SendKeys(Keys.Enter);
                    return true;
                }
                else
                {
                    throw new NoSuchElementException();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region Kendo Drop down for duplicate dropdown in the same page      
        /// <summary>
        /// Select kendo dropdown and add the value for duplicate dropdown in the same page
        /// </summary>
        /// <param name="driver">Driver</param>
        /// <param name="dropdownName">Name of the dropdown to add text</param>
        /// <param name="dropdownValue">Value of the drop to fill</param>
        /// <returns></returns>
        public static bool SelectKendoDropdownAndAddValues(this RemoteWebDriver driver, string dropdownName, string dropdownValue, bool clickOnElement = true)
        {
            CommonMethods.PlayWait(2000);
            try
            {
                IList<IWebElement> element = null;

                element = driver.FindElements(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//input", dropdownName)));

                if(element.Count == 0)
                {
                    element = driver.FindElements(By.XPath(string.Format("//*[normalize-space(.)='{0}']//input", dropdownName)));
                }
                CommonMethods.PlayWait(000);
                for (int i = 0; i < element.Count; i++)
                {
                    if (element[i].Displayed)
                    {
                        if (clickOnElement)
                        {
                            element[i].Click();
                        }
                        element[i].SendKeys(Keys.Control + "a");
                        element[i].SendKeys(dropdownValue);
                        CommonMethods.PlayWait(4000);
                        element[i].SendKeys(Keys.Enter);
                    }
                }
                return true;
            }

            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region Send Value to the text box
        /// <summary>
        /// Select value to the text box 
        /// </summary>
        /// <param name="textBoxName"></param>
        /// <param name="textBoxValue"></param>
        public static void SendTextToTextBoxField(this RemoteWebDriver driver, string textBoxName, string textBoxValue)
        {
            IWebElement element = null;
            CommonMethods.PlayWait(2000);
            try
            {   
                try
                {
                    element = driver.FindElement(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//input", textBoxName)));
                }
                catch (Exception)
                {
                    try
                    {
                        element = driver.FindElement(By.XPath(string.Format("//*[normalize-space(.)='{0}']//input", textBoxName)));
                    }
                    catch (Exception)
                    {
                        element = driver.FindElement(By.XPath(string.Format("//label[text()='{0}']//following-sibling::input", textBoxName)));
                    }                    
                }

                if (element != null)
                {
                    element.Click();
                    element.SendKeys(Keys.Control + "a");
                    element.SendKeys(textBoxValue);
                    CommonMethods.PlayWait(2000);
                }
                else
                {
                    throw new NoSuchElementException();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region Get innner text from the drop down
        /// <summary>
        /// Get innner text from the drop down
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="dropdownName"></param>
        /// <returns></returns>
        public static string GetDropDownValue(this RemoteWebDriver driver, string dropdownName)
        {
            CommonMethods.PlayWait(2000);
            IWebElement element = null;
            try
            {
                try
                {
                    element = driver.FindElement(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//input", dropdownName)));
                }
                catch (Exception)
                {
                    element = driver.FindElement(By.XPath(string.Format("//*[normalize-space(.)='{0}']//input", dropdownName)));
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Get the text from the field \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
            element.DrawHighlight();
            return string.IsNullOrEmpty(element.Text) ? element.GetAttribute("value") : element.Text;
        }
        #endregion

        #region Page refresh
        /// <summary>
        /// Refresh the page
        /// </summary>
        public static void Refresh(this RemoteWebDriver driver)
        {
            CommonMethods.PlayWait(3000);
            driver.Navigate().Refresh();
        }
        #endregion

        #region Select Kendo dropdown with partial text
        /// <summary>
        /// Select Kendo dropdown with partial text
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="dropdownName">label name</param>
        /// <param name="dropdownStartsWith">Name starts with</param>
        /// <param name="dropdownValue">complete name</param>
        /// <param name="dropdownTitle">send the complete title from tag (inspect element)</param>
        /// <returns></returns>
        public static bool SelectKendoDropdownUsingPartialText(this RemoteWebDriver driver, string dropdownName, string dropdownStartsWith, string dropdownValue, string dropdownTitle)
        {
            CommonMethods.PlayWait(2000);
            try
            {
                IWebElement element = null;
                try
                {
                    element = driver.FindElement(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//input", dropdownName)));
                }
                catch (Exception)
                {
                    element = driver.FindElement(By.XPath(string.Format("//*[normalize-space(.)='{0}']//input", dropdownName)));
                }

                if (element != null)
                {
                    MouseHoverClickOnElement(driver, element);
                    element.SendKeys(Keys.Control + "a");
                    element.SendKeys(dropdownStartsWith);
                    CommonMethods.PlayWait(5000);

                    var drawdownSugestions = driver.FindElements(By.XPath(string.Format("//ul[@class='select2-results__options']")));

                    foreach (var select in drawdownSugestions)
                    {
                        var selectOption = select.Text;

                        if (selectOption != "No results found")
                        {
                            var drawdownValues = selectOption.Split('\r');

                            foreach (var option in drawdownValues)
                            {
                                var selectedOption = option.Replace("\n", "");

                                if (selectedOption == dropdownValue)
                                {
                                    driver.FindElement(By.XPath(string.Format("//ul[@class='select2-results__options']//li[@title='{0}']", dropdownTitle))).Click();
                                    break;
                                }
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    throw new NoSuchElementException();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region Verify drop down value is exists
        /// <summary>
        /// Verify drop down value is exists
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="dropdownName"></param>
        /// <param name="dropdownValue"></param>
        /// <returns></returns>
        public static bool VerifyDropDownValue(this RemoteWebDriver driver, string dropdownName, string dropdownValue)
        {
            string innerText = string.Empty;
            CommonMethods.PlayWait(2000);
            try
            {
                IWebElement element = null;
                try
                {
                    element = driver.FindElement(By.XPath(string.Format("//label[text()='{0}']//parent::div//following::div[3]//child::div[contains(text(),'{1}')]", dropdownName, dropdownValue)));
                }
                catch (Exception)
                {
                    try
                    {
                        element = driver.FindElement(By.XPath(string.Format("//*[normalize-space(.)='{0}']//following::span[@title='{1}']", dropdownName, dropdownValue)));
                    }
                    catch
                    {
                        element = driver.FindElement(By.XPath(string.Format("//*[normalize-space(.)='{0}']//following::div[text()='{1}'][2]", dropdownName, dropdownValue)));
                    }
                }

                innerText = element.GetAttribute("Title");

                if (innerText == null || innerText == "")
                {
                    innerText = element.Text;
                }
                element.DrawHighlight();
                Assert.IsTrue(innerText.Contains(dropdownValue), "The value "+ dropdownValue + " is not matching in dropdown " + dropdownName);
                return true;
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to find the Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }
        #endregion

        #region Click on button 
        /// <summary>
        /// Click on button using span 
        /// </summary>
        /// <param name="text"></param>
        public static void ClickOnButton(this RemoteWebDriver driver, string text)
        {
            CommonMethods.PlayWait(3000);
            try
            {
                driver.ClickOnElement(By.XPath(string.Format("//button[text()='{0}']", text)));
            }
            catch(Exception)
            {
                driver.ClickOnElement(By.XPath(string.Format("//span[text()='{0}']", text)));
            }
        }
        #endregion

        #region Remove Multiple Options And Select required From Dropdown
        /// <summary>
        /// Remove Multiple Options And Select required From Dropdown
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="dropdownName"></param>
        /// <param name="dropdownValue">Pass any of the value which is already contains in the dropdown</param>
        /// <returns></returns>
        public static bool RemoveMultipleOptionAndSelectRequiredFromDropdown(this RemoteWebDriver driver, string dropdownName, string dropdownValue)
        {
            CommonMethods.PlayWait(2000);
            try
            {
                IList<IWebElement> element = null;
                try
                {
                     element = driver.FindElements(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//ul[@class='select2-selection__rendered']", dropdownName)));
                }
                catch (Exception)
                {
                     element = driver.FindElements(By.XPath(string.Format("//*[normalize-space(.)='{0}']//following-sibling::div//ul[@class='select2-selection__rendered']", dropdownName)));
                }

                if (element != null)
                {
                    foreach (var options in element)
                    {
                        var combinedText= options.Text;
                        string[] option = combinedText.Split('\n');
                       
                        foreach (var select in option)
                        {
                           var selectedOption = select.Replace("\r", "");
                            if (selectedOption == "×")
                            {
                                IWebElement dropdownValues = driver.FindElement(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//ul[@class='select2-selection__rendered']//span[text()='{1}']//parent::li//child::span[text()='×']",
                               dropdownName, selectedOption)));
                                CommonMethods.PlayWait(2000);
                                dropdownValues.Click();
                            }
                        }
                        driver.SelectKendoDropdownAndAddValue(dropdownName, dropdownValue);
                    }
                    return true;
                }
                else
                {
                    throw new NoSuchElementException();
                }
                
            }
            
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }

        }
        #endregion

        #region Remove dropdwon value
        /// <summary>
        /// Remove the dropdown value
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="dropdownName"></param>
        public static void RemoveDropdownValue(this RemoteWebDriver driver,string dropdownName)
        {
            CommonMethods.PlayWait(2000);
            try
            {
                try
                {
                    driver.FindElement(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//input//parent::li//parent::span//span[text()='×']", dropdownName))).Click();
                }
                catch (Exception)
                {
                    driver.FindElement(By.XPath(string.Format("//*[normalize-space(.)='{0}']//following-sibling::div//input//parent::li//parent::span//span[text()='×']", dropdownName))).Click();
                }
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion
    }
}


