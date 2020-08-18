using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using System;

namespace Selenium.Tests.Pages
{
    public class WorkFlowPage
    {
        public static class WorkFlowPageLocators
        {
            public static readonly By FolderType              = By.XPath("//*[normalize-space(.)='Folder type']//following::span[4]//following::input");
            public static readonly By CaseFlow                = By.XPath("//span[contains(text(),'Case flow')]");
            public static readonly By Process                 = By.XPath("//span[contains(text(),'Process')]");
            public static readonly By CreateNewWorkFlow       = By.XPath("//span[contains(text(),'-- Create new workflow')]");
            public static readonly By Title                   = By.XPath("//input[@id='title']");
            public static readonly By page                    = By.XPath("//div[@class='popup-detail-form']");
            public static readonly By Description             = By.XPath("//textarea[@id='description']");
            public static readonly By Status                  = By.XPath("(//*[normalize-space(.)='Status'])[2]/following::span[5]/following::input[1]");
            public static readonly By DueDate                 = By.XPath("(//*[normalize-space(.)='Due date'])//following::input");
            public static readonly By SaveWorkFlow            = By.XPath("(//*[normalize-space(.)='Save'])[6]");
            public static readonly By CaseOfficer             = By.XPath("//*[normalize-space(.)='Case officer']//following::span[@class='select2-selection__rendered']//li//input");
            public static readonly By ProcessingSequence      = By.XPath("(//*[normalize-space(.)='Processing sequence'])/following::span[5]/following::input[1]");
            public static readonly By glyphicon               = By.XPath("//i[@class='glyphicon glyphicon-option-horizontal']");
            public static readonly By SubTaskButton           = By.LinkText("New subtask");
            public static readonly By SubTaskType             = By.XPath("(//*[normalize-space(.)='New subtask'])[3]//following::span[3]//following::input");
            public static readonly By DocumentTitle           = By.XPath("//input[@id='documentTitle']");
        }
        private readonly RemoteWebDriver _driver;
        public WorkFlowPage(RemoteWebDriver driver)
        {
            _driver = driver;
        }

        #region Create New work flow
        /// <summary>
        /// Click on Case flow option following process buttion to create a work flow
        /// </summary>
        public void CreateWorkFlow(string status, string title, string description, string processingSequences, string dueDate)
        {
            //click on Case flow button
            _driver.WaitForElementVisible(WorkFlowPageLocators.CaseFlow);
            _driver.ClickOnElement(WorkFlowPageLocators.CaseFlow);

            //click on Process button
            _driver.WaitForElementVisible(WorkFlowPageLocators.Process);
            _driver.ClickOnElement(WorkFlowPageLocators.Process);

            //Click on Create new workflow button
            _driver.WaitForElementVisible(WorkFlowPageLocators.CreateNewWorkFlow);
            _driver.ClickOnElement(WorkFlowPageLocators.CreateNewWorkFlow);

            //Select the status of workflow
            _driver.WaitForElementVisible(WorkFlowPageLocators.Status);
            _driver.SelectKendoDropDownAndValue(WorkFlowPageLocators.Status, status);

            //Enter the workflow title
            _driver.WaitForElementVisible(WorkFlowPageLocators.Title);
            _driver.EnterText(WorkFlowPageLocators.Title, title);
            CommonMethods.PlayWait(2000);

            //Enter the description of the workflow
            _driver.EnterText(WorkFlowPageLocators.Description, description);
            CommonMethods.PlayWait(2000);

            //Select Case Officer
            _driver.WaitForElementVisible(WorkFlowPageLocators.CaseOfficer);
            _driver.SelectKendoDropDownAndValue(WorkFlowPageLocators.CaseOfficer, "Bharti Devendrappa");

            //Select Processing Sequence
            _driver.SelectKendoDropDownAndValue(WorkFlowPageLocators.ProcessingSequence, processingSequences);

            //Due Date
            _driver.ClickOnElement(WorkFlowPageLocators.DueDate);
            _driver.EnterText(WorkFlowPageLocators.DueDate, dueDate);
            CommonMethods.PlayWait(2000);

            //Save workflow
            _driver.ClickOnElement(WorkFlowPageLocators.SaveWorkFlow);
            CommonMethods.PlayWait(4000);
        }
        #endregion

        #region Add the work flow sub task
        /// <summary>
        /// Add sub task
        /// </summary>
        /// <param name="status"></param>
        /// <param name="subTaskTitle"></param>
        /// <param name="subTaskDescription"></param>
        /// <param name="dueDate"></param>
        public void AddWorkFlowSubTask(string subTaskType, string subTaskStatus, string subTaskTitle, string subTaskDescription, string processingSequences, string dueDate)
        {
            _driver.ClickOnElement(WorkFlowPageLocators.glyphicon);
            _driver.ClickOnElement(WorkFlowPageLocators.SubTaskButton);
            CommonMethods.PlayWait(2000);

            //Select the status of workflow
            _driver.SelectKendoDropDownAndValue(WorkFlowPageLocators.SubTaskType, subTaskType);
            CommonMethods.PlayWait(2000);

            //Select the status of workflow
            _driver.WaitForElementVisible(WorkFlowPageLocators.Status);
            _driver.SelectKendoDropDownAndValue(WorkFlowPageLocators.Status, subTaskStatus);

            //Enter the subtask title
            _driver.WaitForElementVisible(WorkFlowPageLocators.Title);
            _driver.EnterText(WorkFlowPageLocators.Title, subTaskTitle);
            CommonMethods.PlayWait(2000);

            //Select Case Officer
            _driver.WaitForElementVisible(WorkFlowPageLocators.CaseOfficer);
            _driver.SelectKendoDropDownAndValue(WorkFlowPageLocators.CaseOfficer, "Bharti Devendrappa");

            //Select Processing Sequence
            _driver.SelectKendoDropDownAndValue(WorkFlowPageLocators.ProcessingSequence, processingSequences);

            //Due Date
            _driver.ClickOnElement(WorkFlowPageLocators.DueDate);
            _driver.EnterText(WorkFlowPageLocators.DueDate, dueDate);
            CommonMethods.PlayWait(2000);

            //Save workflow
            _driver.ClickOnElement(WorkFlowPageLocators.SaveWorkFlow);
            CommonMethods.PlayWait(3000);
        }
        #endregion

        #region Verify the task has created
        public string VerifySubTaskCreated(string subTaskTitle)
        {
            //Click on dropdown button to view the created sub task
            CommonMethods.PlayWait(2000);
            _driver.ClickOnElement(By.XPath("//div[@class='task-settings']//following::i[@class='glyphicon glyphicon-chevron-up']"));
            var workFlowTitle = _driver.FindElement(By.XPath("//span[@class='task-status overdue']//following::span[@class='task-title']")).Text;
            CommonMethods.PlayWait(2000);

            //Verify the Sub task has created under the workflow
            Assert.AreEqual(workFlowTitle, subTaskTitle, "Sub task has not created for the workflow");
            return workFlowTitle;
        }
        #endregion

        #region Verify the choose sub task type has created
        /// <summary>
        /// Verify choose sub task has created
        /// </summary>
        /// <param name="subTaskTitle"></param>
        /// <returns></returns>
        public string VerifyChooseSubTaskTypeCreated(string subTaskTitle)
        {
            //Click on dropdown button to view the created sub task
            _driver.ClickOnElement(By.XPath("//div[@class='milestone-caseworker']"));
            CommonMethods.PlayWait(2000);

            //Get the workflow title
            var workFlowTitle = _driver.FindElement(By.XPath("//div[@class='ods active']")).Text;
            CommonMethods.PlayWait(2000);

            //Verify the Sub task has created under the workflow
            Assert.IsTrue(workFlowTitle.Contains(subTaskTitle), "choose sub task type task has not created for the workflow");
            return workFlowTitle;
        }
        #endregion

        #region Choose the sub Task
        /// <summary>
        /// Choose the sub task
        /// </summary>
        /// <param name="subTaskType"></param>
        /// <param name="subTaskStatus"></param>
        /// <param name="subTaskTitle"></param>
        /// <param name="subTaskDescription"></param>
        /// <param name="processingSequences"></param>
        /// <param name="dueDate"></param>
        public void ChooseSubTask(string subTaskType, string subTaskStatus, string subTaskTitle, string subTaskDocumentTitle, string processingSequences, string dueDate)
        {
            CommonMethods.PlayWait(2000);
            //Click on arrow button to view the workflow options
            _driver.ClickOnElement(By.XPath("//button[@class='btn btn-xs btn-link dropdown-toggle']//i[@class='glyphicon glyphicon-option-horizontal']"));
            _driver.ClickOnElement(By.XPath("//li[3]//button[@class='btn btn-xs btn-link btn-block btn-link-pseudo task-settings-btn']"));
            CommonMethods.PlayWait(2000);

            //Select the status of workflow
            _driver.WaitForElementVisible(By.XPath("//*[normalize-space(.)='New subtask']//following::span[@class='select2-selection__rendered']//li//input[1]"));
            _driver.SelectKendoDropDownAndValue(By.XPath("//*[normalize-space(.)='New subtask']//following::span[@class='select2-selection__rendered']//li//input[1]"), subTaskType);
            CommonMethods.PlayWait(3000);

            //Enter the subtask title
            _driver.WaitForElementVisible(WorkFlowPageLocators.Title);
            _driver.EnterText(WorkFlowPageLocators.Title, subTaskTitle);
            CommonMethods.PlayWait(2000);

            //Select Case Officer
            _driver.WaitForElementVisible(WorkFlowPageLocators.CaseOfficer);
            _driver.SelectKendoDropDownAndValue(WorkFlowPageLocators.CaseOfficer, "Bharti Devendrappa");
            CommonMethods.PlayWait(2000);

            //Select Processing Sequence
            _driver.WaitForElementVisible(WorkFlowPageLocators.ProcessingSequence);
            _driver.SelectKendoDropDownAndValue(WorkFlowPageLocators.ProcessingSequence, processingSequences);
            CommonMethods.PlayWait(2000);

            //Select the status of workflow
            _driver.WaitForElementVisible(WorkFlowPageLocators.Status);
            _driver.SelectKendoDropDownAndValue(By.XPath("(//*[normalize-space(.)='Status'])[2]/following::span[5]/following::input[1]"), subTaskStatus);
            CommonMethods.PlayWait(2000);

            //Save workflow
            _driver.ClickOnElement(WorkFlowPageLocators.SaveWorkFlow);
            CommonMethods.PlayWait(5000);
        }
        #endregion

        #region Outgoing Registry Entry Document
        /// <summary>
        /// Enter the document outgoing registry entry details
        /// </summary>
        /// <param name="documentTitle"></param>
        /// <param name="type"></param>
        /// <param name="documentTemplate"></param>
        /// <param name="documentStatus"></param>
        public void OutgoingRegistryEntryDocument(string documentTitle, string type, string documentTemplate, string documentStatus)
        {
            _driver.ClickOnElement(By.XPath("//div[@class='settings-action-container pull-right']//i[@class='glyphicon glyphicon-option-horizontal']"));
            _driver.ClickOnElement(By.XPath("//div[@class='task-settings open']//i[@class='glyphicon glyphicon-option-horizontal']//following::li[1]//button"));
            CommonMethods.PlayWait(2000);

            //Enter the Document title
            _driver.WaitForElementVisible(WorkFlowPageLocators.Title);
            _driver.EnterText(WorkFlowPageLocators.DocumentTitle, documentTitle);
            CommonMethods.PlayWait(2000);

            //select Type
            _driver.WaitForElementVisible(By.XPath("//*[normalize-space(.)='Type']//following::span[4]//li//input"));
            _driver.SelectKendoDropDownAndValue(By.XPath("//*[normalize-space(.)='Type']//following::span[4]//li//input"), type);
          
            //select Document template
            _driver.WaitForElementVisible(By.XPath("//*[normalize-space(.)='Document template']//following::span[@class='select2-selection__rendered']//li//input"));
            _driver.SelectKendoDropDownAndValue(By.XPath("//*[normalize-space(.)='Document template']//following::span[@class='select2-selection__rendered']//li//input"), documentTemplate);
            
            //select the Document Status
            _driver.WaitForElementVisible(By.XPath("(//*[normalize-space(.)='Status'])[3]//following::span[2]//li//input"));
            _driver.SelectKendoDropDownAndValue(By.XPath("(//*[normalize-space(.)='Status'])[3]//following::span[2]//li//input"), documentStatus);
            CommonMethods.PlayWait(2000);

            //Click on the subtask menu button
            _driver.WaitForElementVisible(By.XPath("//button[@class='recipients-button colon control-label btn btn-default btn-xs pull-left']"));
            _driver.ClickOnElement(By.XPath("//button[@class='recipients-button colon control-label btn btn-default btn-xs pull-left']"));
            CommonMethods.PlayWait(2000);

            //Click on the edit button to edit the sub task
            _driver.WaitForElementVisible(By.XPath("//button[@class='btn btn-sm btn-block btn-search btn-success']"));
            _driver.ClickOnElement(By.XPath("//button[@class='btn btn-sm btn-block btn-search btn-success']"));
            CommonMethods.PlayWait(2000);

            _driver.WaitForElementVisible(By.XPath("//*[contains(text(),'Sender of recieved registry entry')]//following::div//button"));
            _driver.ClickOnElement(By.XPath("//*[contains(text(),'Sender of recieved registry entry')]//following::div//button"));
            CommonMethods.PlayWait(2000);

            _driver.WaitForElementVisible(By.XPath("//*[contains(text(),'OK')]"));
            _driver.ClickOnElement(By.XPath("//*[contains(text(),'OK')]"));
            CommonMethods.PlayWait(2000);

            //Save workflow
            _driver.WaitForElementVisible(WorkFlowPageLocators.SaveWorkFlow);
            _driver.ClickOnElement(WorkFlowPageLocators.SaveWorkFlow);
            CommonMethods.PlayWait(3000);
        }
        #endregion
    }
}
