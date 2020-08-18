using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Tests.Base.Selenium.Core;
using System;
using System.Collections;
using System.Linq;

namespace Selenium.Tests.Pages
{
    public class AdminstratorPage
    {
        public static class AdminstratorPageLocators
        {
            public static readonly By Save               = By.XPath("//span[text()='Save']");
            public static readonly By UserId             = By.XPath("//label[text()='User ID']//following-sibling::div//input");
            public static readonly By AccesCodeTab       = By.XPath("//div[@class='css-1kgyzh5']");
            public static readonly By AdmUnitTab         = By.XPath("//label[text()='Adm.unit']//following-sibling::div//input");
            public static readonly By AddAutorization    = By.XPath("//span[text()='Add authorization']");
            public static readonly By AddRole            = By.XPath("//span[text()='Add role']");
            public static readonly By DoneButton         = By.XPath("//button[text()='Done']");
            public static readonly By MemberGroup        = By.XPath("//label[text()='Members']/following::div[@class='multivalue-container']");
            public static readonly By PreservationTime   = By.XPath("//input[@id='PreservationTime']");
            public static readonly By DisposalCode       = By.XPath("//input[@id='DisposalCodeId']//parent::div//parent::div//parent::div//child::div[contains(@class, 'singleValue')]");
            public static readonly By DefaultRole        = By.XPath("//label[text()='Default role']//input");
            public static readonly By DeleteButton       = By.XPath("//button[@title='Delete']");
            public static readonly By ConfirmDeleteButton = By.XPath("//div[@class='edit-buttons']//button[text()='Confirm delete']");
            public static readonly By DelimiterAdmUnit   = By.Id("SeparatorSign");
        }
        private readonly RemoteWebDriver _driver;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Google Home screen class.
        /// </summary>
        /// <param name="driver">.</param>
        public AdminstratorPage(RemoteWebDriver driver)
        {
            _driver = driver;
        }

        public static AdminstratorPage Connect(RemoteWebDriver driver)
        {
            return new AdminstratorPage(driver);
        }
        #endregion

        #region Navigate to menu item
        /// <summary>
        /// Navigate to menu item in Admin module
        /// </summary>
        /// <param name="Level1"></param>
        /// <param name="Level2"></param>
        /// <param name="Level3"></param>
        public void NavigateToMenuItem(string Level1, string Level2, string Level3)
        {

            CommonMethods.PlayWait(5000);
            _driver.MouseHoverClickOnElement(_driver.FindElement(By.XPath(string.Format("//span[text()='{0}']//parent::button", Level1))));
            CommonMethods.PlayWait(2000);
            _driver.MouseHoverClickOnElement(_driver.FindElement(By.XPath(string.Format("//span[text()='{0}']//parent::button", Level2))));
            CommonMethods.PlayWait(4000);
            _driver.MouseHoverClickOnElement(_driver.FindElement(By.XPath(string.Format("//span[text()='{0}']//parent::button", Level3))));
            CommonMethods.PlayWait(2000);
        }
        public void NavigateToMenuItem(string Level1, string Level2)
        {
            CommonMethods.PlayWait(5000);
            _driver.MouseHoverClickOnElement(_driver.FindElement(By.XPath(string.Format("//span[text()='{0}']//parent::button", Level1))));
            CommonMethods.PlayWait(3000);
            try
            {
                _driver.MouseHoverClickOnElement(_driver.FindElement(By.XPath(string.Format("(//*[normalize-space(.)='{0}'])[3]", Level2))));
            }
            catch
            {
                _driver.MouseHoverClickOnElement(_driver.FindElement(By.XPath(string.Format("//span[text()='{0}']//parent::button", Level2))));
            }
            CommonMethods.PlayWait(2000);
        }
        public void NavigateToMenuItem(string Level3)
        {
           if(!string.IsNullOrEmpty(Level3))
           CommonMethods.PlayWait(5000);
            _driver.MouseHoverClickOnElement(_driver.FindElement(By.XPath(string.Format("//span[text()='{0}']//parent::button", Level3))));
        }
        #endregion

        #region Verify Access code exist
        /// <summary>
        /// Verfiy the Access code is exist in the Grid       
        /// </summary>
        public void VerifyAccessCodeExist(string AccessCodeNumber)
        {
            CommonMethods.PlayWait(3000);
            var element = _driver.FindElement(By.XPath(string.Format("//td[text()='{0}']", AccessCodeNumber))).Text;

            Assert.IsTrue(element == AccessCodeNumber);
        }
        #endregion

        #region Click Access Code In Grid
        /// <summary>
        /// Click the Access Code In Grid       
        /// </summary>
        public void ClickItemInGrid(string ItemName)
        {
            CommonMethods.PlayWait(2000);
            _driver.MouseHoverClickOnElement(_driver.FindElement(By.XPath(string.Format("//td[text()='{0}']", ItemName))));
        }
        #endregion

        #region Click on Check box in Admin module
        /// <summary>
        /// Click Check Box In Admin Module to SwitchOnOff
        /// </summary>
        /// <param name="checkBoxName"></param>
        /// <param name="option"></param>
        public void ClickCheckBoxInAdminModuleSwitchOnOff(string checkBoxName, bool option)
        {
            CommonMethods.PlayWait(2000);
            IWebElement element = _driver.FindElement(By.XPath(string.Format("//label[contains(text(),'{0}')]//input", checkBoxName)));
            CommonMethods.PlayWait(3000);

            var checkBox = element.Selected;

            if (checkBox == false && option == true)
            {
                element.Click();
            }
            else if (checkBox == true && option == false)
            {
                element.Click();
            }
        }
        #endregion

        #region save 
        /// <summary>
        /// save changes in admin page
        /// </summary>
        public void Save()
        {
            _driver.WaitForElementVisible(AdminstratorPageLocators.Save);
            IWebElement element = _driver.FindElement(AdminstratorPageLocators.Save);
            var save = element.Enabled;
            CommonMethods.PlayWait(3000);
            if (save == true)
            {
                _driver.DrawHighlight(AdminstratorPageLocators.Save);
                _driver.ClickOnElement(AdminstratorPageLocators.Save);
            }
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Select Person
        /// <summary>
        /// Select the person from the Admin moduele
        /// </summary>
        /// <param name="userName"></param>
        public void SelectPerson(string userName)
        {
            CommonMethods.PlayWait(4000);
            _driver.SelectKendoDropDownAndValue(AdminstratorPageLocators.UserId, userName);
            CommonMethods.PlayWait(4000);
            ClickItemInGrid(userName);
            CommonMethods.PlayWait(4000);
        }
        #endregion

        #region Add or Verify the member exists in the member group
        /// <summary>
        /// Add or Verify the member exists in the member group
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="memberName"></param>
        public void AddOrVerifyMemberOfAccessGroup(string groupName, string memberName)
        {
            CommonMethods.PlayWait(5000);
            var groupNameElement = _driver.FindElement(By.XPath(string.Format("//td[text()='{0}']", groupName)));
            string accessGroupName = groupNameElement.Text;
            SeleniumExtensions.DrawHighlight(groupNameElement);
            Assert.IsTrue(accessGroupName == groupName,"Member name doesn't exists");

            CommonMethods.PlayWait(2000);
            if (groupNameElement.Displayed) groupNameElement.Click();
            CommonMethods.PlayWait(3000);
            var listOfMembers = _driver.FindElements(AdminstratorPageLocators.MemberGroup);
            if (!listOfMembers.Any()) throw new NoSuchElementException();

            int count = 0;
            string user = string.Empty;

            foreach (var individualMember in listOfMembers)
            {
                user = individualMember.Text;

                if (user == memberName)
                {
                    SeleniumExtensions.DrawHighlight(_driver.FindElement(By.XPath(string.Format("//label[text()='Members']/following::div[@class='multivalue-container']//button[contains(text(),'{0}')]", memberName))));
                    Assert.IsTrue(user == memberName, "User ({0}) doesn't exists in the member group", memberName);
                }
                else
                {
                    count++;
                }
            }
            if (count == listOfMembers.Count)
            {
                SelectKendoDropdownAndAddValue("Members", memberName);
                SeleniumExtensions.DrawHighlight(_driver.FindElement(By.XPath(string.Format("//label[text()='Members']/following::div[@class='multivalue-container']//button[contains(text(),'{0}')]", memberName))));
                Save();
            }
        }
        #endregion

        #region Remove User From the Access Group
        /// <summary>
        /// Remove User From the Access Group
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="memberName"></param>
        public void RemoveUserFromAccessGroup(string groupName, string memberName)
        {
            CommonMethods.PlayWait(5000);
            var groupNameControl = _driver.FindElement(By.XPath(string.Format("//td[text()='{0}']", groupName)));
            string accessGroupName = groupNameControl.Text;
            SeleniumExtensions.DrawHighlight(groupNameControl);
            Assert.IsTrue(accessGroupName == groupName,"Member name doesn't exists");

            CommonMethods.PlayWait(2000);
            if (groupNameControl.Displayed) groupNameControl.Click();

            bool isMemberExist = false;
            try
            {
                CommonMethods.PlayWait(3000);
                IWebElement member = _driver.FindElement(By.XPath(string.Format("//label[text()='Members']/following::div[@class='multivalue-container']//button[contains(text(),'{0}')]", memberName)));

                if (member.Text == memberName)
                {
                    SeleniumExtensions.DrawHighlight(member);
                    _driver.FindElement(By.XPath(string.Format("//label[text()='Members']/following::div[@class='multivalue-container']//following::button[contains(text(),'{0}')]//following::*[name()='path'][1]", memberName))).Click();
                }
                Save();
            }
            catch (Exception e)
            {
                if (!isMemberExist)
                    CommonMethods.ThrowExceptionAndBreakTC("Member is not available in the member group \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
    
        #endregion

        #region Add or edit the Autorization
        /// <summary>
        /// Add or edit the Autorization
        /// </summary>
        /// <param name="accessCode"></param>
        /// <param name="AdmUnit"></param>
        /// <param name="authorizedForOrg"></param>
        public void AddOrEditAutorization(string accessCode, string AdmUnit, string dropDownValue, bool authorizedForOrg = false)
        {
            CommonMethods.PlayWait(4000);
            By accessCodeXpath = By.XPath(string.Format("//span[text()='{0}']", accessCode));

            try
            {
                //Wait for element visible
                _driver.WaitForElementVisible(accessCodeXpath, new TimeSpan(0, 0, 20));
                
                //If Autorization exist, then open and verify
                var accessCodeValue = _driver.FindElement(accessCodeXpath);
                if (accessCode == accessCodeValue.Text)
                {
                    _driver.ClickOnElement(By.XPath(string.Format("//div[@id='Authorizations']//li//span[text()='{0}']", accessCode)));
                    CommonMethods.PlayWait(2000);
                    ClickCheckBoxInAdminModuleSwitchOnOff("Authorised for the organisation", authorizedForOrg);
                    if (!authorizedForOrg)
                    {
                        ClearDropDown(AdmUnit);
                        CommonMethods.PlayWait(2000);
                        AddDropdownValue(dropDownValue);
                    }
                    CommonMethods.PlayWait(2000);
                    Done();
                    Save();
                }
            }
            catch (WebDriverTimeoutException)
            {
                //Add new Autorization
                AddAutorization(accessCode, dropDownValue, authorizedForOrg);
                Save();
            }
            catch (Exception e)
            {
                CommonMethods.ThrowExceptionAndBreakTC("Unable to Click on Element \n" + e.GetType().FullName + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
        #endregion

        #region Add or edit the Role
        /// <summary>
        /// Add or edit the Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="roleID"></param>
        /// <param name="recordSection"></param>
        /// <param name="admUnit"></param>
        /// <param name="recordManagementUnit"></param>
        /// <param name="roleTitle"></param>
        public void AddOrEditRole(string roleName, string roleID, string admUnit, string recordSection, string recordManagementUnit, string roleTitle)
        {
            var roleValue = _driver.FindElements(By.XPath(string.Format("//span[text()='{0}']", roleName)));
            if (roleValue.Any())
            {
                //Role already Exist
                if (roleName == roleValue.FirstOrDefault().Text)
                {
                    //Click on the Role
                    _driver.ClickOnElement(By.XPath(string.Format("//div[@id='Roles']//li//span[text()='{0}']", roleName)));

                    //ID adm. unit
                    CommonMethods.PlayWait(2000);
                    ClearDropDown("ID adm. unit", true);
                    SelectKendoDropdownAndAddValue("ID adm. unit", admUnit);

                    //RoleTitle
                    _driver.SendTextToTextBoxField("Roletitle", roleTitle);

                    //Verify or selected role as default
                    VerifyOrSelectRoleAsDefault();

                    Done();
                    Save();
                }
            }
            else
            {
                //Add new role
                AddRole(roleName, roleID, admUnit, recordSection, recordManagementUnit, roleTitle);
                Save();
            }
        }
        #endregion

        #region Add Role
        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="roleID"></param>
        /// <param name="recordSection"></param>
        /// <param name="admUnit"></param>
        /// <param name="recordManagementUnit"></param>
        /// <param name="roleTitle"></param>
        public void AddRole(string roleName, string roleID, string admUnit, string recordSection, string recordManagementUnit, string roleTitle)
        {
            //Click on the Role
            _driver.ClickOnElement(AdminstratorPageLocators.AddRole);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
            CommonMethods.PlayWait(5000);

            //RoleID
            SelectKendoDropdownAndAddValue("RoleID", roleID);

            //ID adm. unit
            SelectKendoDropdownAndAddValue("ID adm. unit", admUnit);

            //Record section
            SelectKendoDropdownAndAddValue("Record section", recordSection);

            //Registry management unit
            SelectKendoDropdownAndAddValue("Registry management unit", recordManagementUnit);

            //RoleTitle
            _driver.SendTextToTextBoxField("Roletitle", roleTitle);

            //Verify or selected role as default
            VerifyOrSelectRoleAsDefault();
            Done();
        }
        #endregion

        #region //Verify the role is selected as default
        /// <summary>
        /// //Verify the role is selected as default
        /// </summary>
        public void VerifyOrSelectRoleAsDefault()
        {
            CommonMethods.PlayWait(3000);
            IWebElement defaultRole = _driver.FindElement(AdminstratorPageLocators.DefaultRole);
            var isDefaultRole = defaultRole.Selected;
            _driver.DrawHighlight(By.XPath("//label[text()='Default role']"));
            if (isDefaultRole == false)
            {
                defaultRole.Click();
                var IsDefaultRoleSelected = defaultRole.Selected;
                Assert.IsTrue(IsDefaultRoleSelected, "The role is not defaulted");
            }
        }
        #endregion

        #region Clear dropdown value
        /// <summary>
        /// Add or edit the Autorization
        /// </summary>
        /// <param name="dropdownLabelName"></param>
        /// <param name="singleValuedropdown"></param>
        public void ClearDropDown(string dropdownLabelName, bool singleValuedropdown = false)
        {
            if (singleValuedropdown)
            {
                var dropdown = _driver.FindElement(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//input", dropdownLabelName)));
                dropdown.SendKeys(Keys.Delete);
            }
            else
            {
                var output = _driver.ClickOnElement(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//input", dropdownLabelName)));
                var output1 = _driver.FindElements(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//following::*[name()='path']", dropdownLabelName)));

                var removeValue = output1.LastOrDefault();
                var removeLastButOne = output1.Take(output1.Count() - 1);
                var removeValue1 = removeLastButOne.LastOrDefault();
                var dd = (IWebElement)removeValue1;
                if(dd !=null)
                dd.Click();
            }
        }

        #endregion

        #region Verify dropdown exist
        /// <summary>
        /// Verify whether dropdown is exist in the Autorization
        /// </summary>
        /// <param name="accessCode"></param>
        public void VerifyAutorizationDropdownValueExists(string dropdownName, string dropdownValue)
        {
            var accessCodeValue = _driver.GetDropDownValue(dropdownName);
            CommonMethods.PlayWait(2000);
            Assert.IsTrue(accessCodeValue.Contains(dropdownValue), string.Format("The dropdown {0} value is invalid, for dropdown {1}", dropdownValue,dropdownName));
        }
        #endregion

        #region Add Autorization
        /// <summary>
        /// Add Autorization
        /// </summary>
        /// <param name="accessCodeValue"></param>
        /// <param name="admUnit"></param>
        /// <param name="authorizedForOrg"></param>
        public void AddAutorization(string accessCodeValue, string admUnit, bool authorizedForOrg = false)
        {
            CommonMethods.PlayWait(2000);
            _driver.ClickOnElement(AdminstratorPageLocators.AddAutorization);
            _driver.HandleErrorPopUpAndThrowErrorMessage();
            SelectKendoDropdownAndAddValue("Access code", accessCodeValue);
            CommonMethods.PlayWait(2000);
            SelectKendoDropdownAndAddValue("Adm.unit", admUnit);
            CommonMethods.PlayWait(2000);
            ClickCheckBoxInAdminModuleSwitchOnOff("Authorised for the organisation", authorizedForOrg);
            Done();
        }
        #endregion

        #region Add drop down value
        /// <summary>
        /// Add drop down value
        /// </summary>
        /// <param name="dropdownName"></param>
        public void AddDropdownValue(string dropdownName)
        {
            _driver.SelectKendoDropDownAndValue(AdminstratorPageLocators.AdmUnitTab, dropdownName);
            CommonMethods.PlayWait(2000);
        }
        #endregion

        #region Click done
        /// <summary>
        /// click on done button
        /// </summary>
        public void Done()
        {
            _driver.ClickOnElement(By.XPath("//button[text()='Done']"));
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Click dropdown in admin moudle
        /// <summary>
        /// Click dropdown in AdminMoudle
        /// </summary>
        /// <param name="name"></param>
        public void ClickDropDownInAdminModule(string name)
        {
            _driver.ClickOnElement(By.XPath(string.Format("(//*[normalize-space(.)='{0}'])[3]",name)));
        }
        #endregion

        #region Remove Autorization
        /// <summary>
        /// Remove Autorization
        /// </summary>
        /// <param name="accessCodeValue"></param>
        /// <param name="userName"></param>
        public void RemoveAutorization(string accessCodeValue, string userName)
        {
            _driver.ClickOnElement(By.XPath(string.Format("//td[text()='{0}']", userName)));
            var autorizationCloseButton = _driver.FindElements(By.XPath(string.Format("//span[text()='{0}']//parent::button//parent::li//child::button[@class='selection_remove']", accessCodeValue)));
            if (autorizationCloseButton.Any())
            {
                autorizationCloseButton.FirstOrDefault().Click();
                _driver.HandleErrorPopUpAndThrowErrorMessage();
            }
            Save();
        }
        #endregion

        #region ClickOnButton
        /// <summary>
        /// Click on button using span 
        /// </summary>
        /// <param name="text"></param>
        public void ClickOnButton(string text)
        {
            _driver.ClickOnButton(text);
        }
        #endregion

        #region Select dropdown value
        /// <summary>
        /// Select dropdown value from the Kendo dropdown
        /// </summary>
        /// <param name="dropdownLabelName"></param>
        /// <param name="dropdownValue"></param>
        public void SelectKendoDropdownAndAddValue(string dropdownLabelName, string dropdownValue)
        {
            try
            {
                _driver.SelectKendoDropdownAndAddValue(dropdownLabelName, dropdownValue);
            }
            catch
            {
                try
                {
                    _driver.SelectKendoDropdownAndAddValues(dropdownLabelName, dropdownValue);
                }
                catch (Exception ex)
                {
                    var errorMessage = string.Format("Unable to select the value in the drop down:{0} - Value:{1}\n {2}", dropdownLabelName, dropdownValue, ex);
                    CommonMethods.ThrowExceptionAndBreakTC(errorMessage);
                }
            }
        }
        #endregion

        #region Get Preservation Time Value
        /// <summary>
        /// Get Preservation Time
        /// </summary>
        /// <returns></returns>
        public string GetPreservationTime()
        {
            CommonMethods.PlayWait(2000);
            var preservationElement = _driver.FindElement(AdminstratorPageLocators.PreservationTime);
            preservationElement.DrawHighlight();
            return preservationElement.GetAttribute("value");
        }
        #endregion

        #region Get Disposal Code Value
        /// <summary>
        /// Get innner text from the drop down
        /// </summary>
        /// <returns></returns>
        public string GetDisposalCode()
        {
            CommonMethods.PlayWait(2000);
            var disposalCode = _driver.FindElement(AdminstratorPageLocators.DisposalCode);
            disposalCode.DrawHighlight();
            return disposalCode.Text;
        }
        #endregion

        #region Sets the value for the text in the search popup
        /// <summary>
        /// Sets the value for the text in the search popup
        /// </summary>
        /// <param name="textboxLabel"></param>
        /// <param name="textValue"></param>
        /// <param name="isDropdown"></param>
        public void SetTextBoxValueInSearchPopup(string textboxLabel, string textValue, bool isDropdown = false)
        {
            CommonMethods.PlayWait(2000);
            var searchTextBox = _driver.FindElement(string.Format("//label[contains(text(),'{0}')]//parent::div//child::div//child::input", textboxLabel));
            searchTextBox.DrawHighlight();
            CommonMethods.PlayWait(2000);
            searchTextBox.Click();
            searchTextBox.SendKeys(textValue);
            if (isDropdown)
            {
                CommonMethods.PlayWait(2000);
                searchTextBox.SendKeys(Keys.Enter);
            }
        }
        #endregion

        #region Add new description
        /// <summary>
        /// Add new description
        /// </summary>
        /// <param name="defaultValues_AdmUnitValue"></param>
        public void ClickNew(string defaultValues_AdmUnitValue)
        {
            CommonMethods.PlayWait(3000);
            _driver.ClickOnButton("New");
            SelectKendoDropdownAndAddValue("Adm. Unit", defaultValues_AdmUnitValue);
            Save();
        }
        #endregion

        #region Verify Desctiption Is Exist
        /// <summary>
        /// Verify Desctiption Is Exist
        /// </summary>
        /// <param name="description"></param>
        public void VerifyDesctiptionIsExist(string description)
        {
            CommonMethods.PlayWait(3000);
            IWebElement groupName = _driver.FindElement(By.XPath(string.Format("//td[text()='{0}']", description)));
            groupName.DrawHighlight();
            var innerText = groupName.Text;
            Assert.AreEqual(innerText, description, "Group Name doesn't exist");
        }
        #endregion

        #region Edit Description
        /// <summary>
        /// Edit Description
        /// </summary>
        /// <param name="description"></param>
        /// <param name="dropdownName"></param>
        /// <param name="dropdownValue"></param>
        /// <param name="editOption"></param>
        public void EditDescription(string description,string dropdownName, string dropdownValue, Enum editOption, bool isCodeorDescriptionExist=true)
        {
            if (isCodeorDescriptionExist == true)
            {
                CommonMethods.PlayWait(4000);
                IWebElement descriptionValue = _driver.FindElement(By.XPath(string.Format("//td[text()='{0}']", description)));
                descriptionValue.Click();
            }
            CommonMethods.PlayWait(2000);
            switch (editOption)
            {
                case GlobalEnum.Description.DropdownHTMLComboBox:
                    SelectHtmlComboBox(dropdownName, dropdownValue);
                    break;
                case GlobalEnum.Description.TextBox:
                    SetTextBox(dropdownName, dropdownValue);
                    break;
                case GlobalEnum.Description.KendoDropdown:
                    SelectKendoDropdownAndAddValue(dropdownName, dropdownValue);
                    break;
            }
            Save();
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Delete Description
        /// <summary>
        /// Delete Description
        /// </summary>
        /// <param name="description"></param>
        public void ClickDeleteAndConfirmDelete()
        {
            CommonMethods.PlayWait(3000);
            _driver.DrawHighlight(AdminstratorPageLocators.DeleteButton);
            _driver.WaitForElementVisible(AdminstratorPageLocators.DeleteButton);
            _driver.FindElement(AdminstratorPageLocators.DeleteButton).Click();
            _driver.HandleErrorPopUpAndThrowErrorMessage();

            CommonMethods.PlayWait(3000);
            _driver.WaitForElementVisible(AdminstratorPageLocators.ConfirmDeleteButton);
            _driver.DrawHighlight(AdminstratorPageLocators.ConfirmDeleteButton);
            _driver.FindElement(AdminstratorPageLocators.ConfirmDeleteButton).Click();
            _driver.HandleErrorPopUpAndThrowErrorMessage();
        }
        #endregion

        #region Set Text Box
        /// <summary>
        /// Set Text Box
        /// </summary>
        /// <param name="textBoxLabel"></param>
        /// <param name="textBoxValue"></param>
        public void SetTextBox(string textBoxLabel, string textBoxValue)
        {
            CommonMethods.PlayWait(3000);
            IWebElement dropdownLabelName = null;
            try
            {
                dropdownLabelName = _driver.FindElement(By.XPath(string.Format("//label[text()='{0}']//following-sibling::input", textBoxLabel)));
            }
            catch
            {
                dropdownLabelName = _driver.FindElement(By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//input", textBoxLabel)));
            }
            dropdownLabelName.Clear();
            dropdownLabelName.SendKeys(textBoxValue);
        }
        #endregion

        #region Set Text Box
        /// <summary>
        /// Select Html Combo Box
        /// </summary>
        /// <param name="dropdownName"></param>
        /// <param name="drodownValue"></param>
        public void SelectHtmlComboBox(string dropdownName, string drodownValue)
        {
            By dropdownLabelName = null;
            CommonMethods.PlayWait(3000);
            try
            {
                dropdownLabelName = By.XPath(string.Format("//label[text()='{0}']//following-sibling::select", dropdownName));
                _driver.SelectListValueAndSelectByText(dropdownLabelName, drodownValue);
            }
            catch
            {
                try
                {
                    dropdownLabelName = By.XPath(string.Format("//label[text()='{0}']//following-sibling::div//input", dropdownName));
                }
                catch
                {
                    dropdownLabelName = By.XPath(string.Format("//label[text()='{0}']//following-sibling::input", dropdownName));
                }             
                _driver.SelectKendoDropDownAndValue(dropdownLabelName, drodownValue);
            }
        }
        #endregion
    }
}
