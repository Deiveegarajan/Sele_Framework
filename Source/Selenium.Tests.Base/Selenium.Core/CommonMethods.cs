using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Selenium.Tests.Base.Selenium.Core
{
    public static class CommonMethods
    {
        public static readonly By RedToastContainer = By.Id("toast-container");
        public static readonly By RedErrorContainer = By.XPath("//div[@class='toast toast-error']");
        public static readonly By RedToastMessage = By.XPath("//div[@class='toast-message']");

        #region KendoDropdown
        public static string NewCaseDrawdown = "//div[@id='caseNewForm']//span[@id='select2-pcp2-container']" +
           "//input[contains(@class,'select2-search--inline select2-search__single')]";

        private static string registry => "//span[@class='select2 select2-container select2-container--default select2-container--below select2-container--open']//span[@class='select2-selection__arrow']";

        private static string NewFormCase => "//div[@id='caseNewForm']";

        /// <summary>
        /// Select kendo dropdown value and set the value 
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SelectKendoDropdownAndSelectValue(this IWebDriver webDriver, string dropDownValue)
        {
            try
            {
                var elements = webDriver.FindElements(By.XPath(registry));
                IWebElement element = webDriver.FindElement(By.XPath(registry));
                IList<IWebElement> listofElm = webDriver.FindElements(By.XPath(NewFormCase));

                if (element != null)
                {
                    SelectElement select = new SelectElement(element);

                    //IReadOnlyCollection

                    var kendrolist = listofElm.ToList();

                    foreach (var lst in kendrolist)
                    {
                        string output = lst.Text;
                        if (output == dropDownValue)
                        {
                            element.SendKeys(dropDownValue);
                            select.SelectByText(dropDownValue);
                        }

                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (Exception e)
            {
                e.GetType();
                return false;
            }
        }
        #endregion

        #region GetDescriptionFromEnum
        /// <summary>
        /// Method to get Enum Description value
        /// </summary>
        /// <param name="enumValue">specify the enum</param>
        /// <returns>description of the enum</returns>
        public static string GetDescriptionFromEnum(object enumValue)
        {
            string strDescription = string.Empty;
            var enumType = enumValue.GetType().GetField(enumValue.ToString());

            if (enumType != null)
            {
                object[] attrs = enumType.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((System.ComponentModel.DescriptionAttribute)attrs[0]).Description;
            }

            return strDescription;
        }
        #endregion

        #region GenerateRandomNumber

        /// <summary>
        /// Method to generate a random string
        /// </summary>
        /// <param name="length">provide the length of the random string to be returned</param>
        /// <returns>Random Number</returns>
        public static string GenerateRandomNumber(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        
        //Declaring global variables
        public static string timerValue;
        public static int iterationCount = 1;
        public static string browserType;

        /// <summary>
        /// Generates Random Number between range provided.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandomNumber(int min = int.MinValue, int max = int.MaxValue)
        {
            lock (syncLock)
            {
                // synchronize
                return getrandom.Next(min, max);
            }
        }
        #endregion

        #region Throw Exception and Breack
        /// <summary>
        /// Throw error message and break the TC. This method can be used in combination of Assert Messages.
        /// </summary>
        /// <param name="errorMessage">Error Message to be displayed</param>
        public static void ThrowExceptionAndBreakTC(string errorMessage = "Error")
        {
            throw new Exception(errorMessage);
        }
        #endregion

        #region Process Kill
        /// <summary>
        /// To Kill the Process
        /// </summary>
        public static void ProcessKill(string ProcessName)
        {
            Process[] processes = Process.GetProcessesByName(ProcessName);
            foreach (Process process in processes)
            {
                process.Kill();
                process.WaitForExit();
            }
        }
        #endregion

        #region kill Internet explorer and Firefox to stop them locking files
        /// <summary>
        /// We ned to kill Internet explorer and Firefox to stop them locking files
        /// </summary>
        /// <param name="ProcessName"></param>
        public static void KillProcess(string ProcessName)
        {
            ProcessName = ProcessName.ToLower();
            foreach (Process P in Process.GetProcesses())
            {
                if (P.ProcessName.ToLower().StartsWith(ProcessName))
                    P.Kill();
            }
        }
        #endregion

        #region Clear cache
        /// <summary>
        /// Clear the caches
        /// </summary>
        public static void ClearCache()
        {
            string IE1 = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + @"\AppData\Local\Microsoft\Intern~1";
            string IE2 = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + @"\AppData\Local\Microsoft\Windows\History";
            string IE3 = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + @"\AppData\Local\Microsoft\Windows\Tempor~1";
            string IE4 = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + @"\AppData\Roaming\Microsoft\Windows\Cookies";
            string IE5 = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + @"\AppData\Local\Microsoft\Windows\Caches";
            string IE6 = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + @"\AppData\Local\Microsoft\WebsiteCache";
            string IE7 = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + @"\AppData\LocalLow\Microsoft\CryptnetUrlCache";
            string Flash = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + @"\AppData\Roaming\Macromedia\Flashp~1";
            var InternetCache = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            var Cookies = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
            //var Temp = Environment.ExpandEnvironmentVariables("%TEMP%");

            //try { KillProcess("iexplore"); } catch { }
            ClearAllSettings(new string[] { IE1, IE2, IE3, IE4, IE5, IE6, IE7, Flash, InternetCache, Cookies });
        }
        #endregion

        #region Clear paths
        /// <summary>
        /// Clear all the setting and paths
        /// </summary>
        /// <param name="ClearPath"></param>
        private static void ClearAllSettings(string[] ClearPath)
        {
            foreach (string HistoryPath in ClearPath)
            {
                if (Directory.Exists(HistoryPath))
                {
                    DoDelete(new DirectoryInfo(HistoryPath));
                }

            }
        }
        #endregion

        #region Delete directory
        private static void DoDelete(DirectoryInfo folder)
        {
            try
            {
                foreach (FileInfo file in folder.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch(Exception e)
                    {
                        e.GetType();
                    }

                }
                foreach (DirectoryInfo subfolder in folder.GetDirectories())
                {
                    DoDelete(subfolder);
                }
            }
            catch (Exception e)
            {
                e.GetType();
            }
        }
        #endregion

        #region Refresh the browser
        /// <summary>
        /// Refresh the current browser
        /// </summary>
        public static void RefreshBrowserWindow(object o)
        {
            string driverInstance = o.GetType().ToString();
            IWebDriver webDriver = (IWebDriver)o;
            webDriver.Navigate().Refresh();
        }
        #endregion

        #region Refresh
        /// <summary>
        /// Refresh page and wait 5 secs before and 10 secs after refresh
        /// </summary>
        public static void RefreshScreenAndWaitPrePost(RemoteWebDriver driver,object o)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromSeconds(2.0); 
            RefreshBrowserWindow(o);
            wait.PollingInterval = TimeSpan.FromSeconds(5.0); 
        }
        #endregion

        #region Explicit wait
        /// <summary>
        /// Implicitly Wait for milliseconds.
        /// </summary>
        /// <param name="waitTime">Time to wait</param>
        public static void PlayWait(int waitTime)
        {
           Thread.Sleep(waitTime);
        }
        #endregion

        #region Log
        /// <summary>
        /// Public method which includes logic related to Create the log
        /// </summary>
        /// <param name="LogText">Parameter of type System.string for LogText</param>
        public static void CreateLog(string LogText)
        {

            try
            {
                //Capturing the directory path
                string directoryPath = GetApplicationLogResultsPath() + browserType + "\\";
                //Verifying the correct path
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                // Create a writer and open the file:
                StreamWriter log;

                if (!File.Exists(directoryPath + timerValue + ".txt"))
                {
                    log = new StreamWriter(directoryPath + timerValue + ".txt");
                }
                else
                {

                    log = File.AppendText(directoryPath + timerValue + ".txt");
                }

                // Write to the file:
                log.WriteLine(DateTime.Now);
                log.WriteLine(LogText);
                log.WriteLine();

                // Close the stream:
                log.Close();

            }
            catch (Exception e)
            {
                e.GetBaseException();
            }
        }
        #endregion

        #region Log Results path
        /// <summary>
        /// Get application log results path
        /// </summary>
        /// <returns></returns>
        private static string GetApplicationLogResultsPath()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Handle red color popup error message
        /// <summary>
        /// Handle red color Error PopUp And Throw Error Message
        /// </summary>
        public static void HandleErrorPopUpAndThrowErrorMessage(this RemoteWebDriver _driver)
        {
            PlayWait(2000);
            string toastMessage = string.Empty;
            var toastContainer = _driver.FindElements(RedToastContainer);
            var errorContainer = _driver.FindElements(RedErrorContainer);
            
            if (toastContainer.Any())
            {
                if (errorContainer.Any() && errorContainer.FirstOrDefault().Displayed)
                {
                    //Red error pop up is visble
                    toastMessage = _driver.FindElement(RedToastMessage).Text;
                    toastContainer.FirstOrDefault().DrawHighlight();
                    Assert.Fail(string.Format("Error Pop up Window is Visible with following Message:\n {0}", toastMessage.Trim()));
                }
            }
            else
            {
               //Do Nothing
            }
        }
        #endregion

        #region  Validate Red PopUp, highlight with popup Msg validation check
        /// <summary>
        /// Validate Red PopUp, highlight with popup Msg validation check
        /// </summary>
        public static void ValidateRedPopUp(this RemoteWebDriver _driver,string redPopUpText)
        {
            PlayWait(2000);
            var toastContainer = _driver.FindElement(RedToastContainer);
            var errorContainer = _driver.FindElement(RedErrorContainer);
            if (errorContainer.Displayed)
            {
                string toastMessage = _driver.FindElement(RedToastMessage).Text;

                // You have either not have the rights to copy this case/registry/operation, or it is not allowed to write to this case/registry/operation
                if (toastMessage.ToString().Contains(redPopUpText))
                {
                    // Error message is correct. Do nothing
                    errorContainer.DrawHighlight();
                }
                else
                {
                    Assert.Fail(string.Format("Error Message: {0}. The actual red pop up message do not matches the expected one.", redPopUpText));
                }
            }
        }
        #endregion
    }

    public static class GlobalVariables
    {
        public static string CaseFolderType;
        public static string CaseTitle = "Case - AutomaticTest";
        public static string RoleType;
        public static bool IsCaseWorkerRole = true;
        public static string DocumentTemplateType = "Dokumentmal";
        public static string DocumentType = "Standard brev";
        public static string ImportRegistryTitle;
        public static string ImportType;
        public static string ImportCaseId;
        public static string CASEQuickMenuOptions;
        public static string CaseValue;
        public static bool SetCaseStatus = false;
        public static string RegistryEntryTitle = "Registry Entry - DocumentType";
        public static string FileFormatValue = "PDF";
        public static string DocumentTitle;
        public static string EditDataStatement = "{Space}Automatic{Space}Test{Space}Edit{Space}Doc";
        public static string SearchUserName = "AA";
        public static string DocumentNameToUpload = "CaseFolder";
        public static string DocumentUploadFolderName = "Desktop\\Automatic TCs";
        public static string MeetingType;
        public static string ProcessName;
        public static string MeetingTabName;
        public static string NewHandlingTitle = "New Handling for Meeting";
        public static string NewSuggestion = "This is a suggestion proposed by proposer";
        public static string Decision = "Decision taken by the proposed team";
        public static string LoginUsername = "";
        public static string LoginPassword = "";
        public static string SiteURL = "";
        public static string WaitTimeForPDFToDownload = "10000";
    }
}
