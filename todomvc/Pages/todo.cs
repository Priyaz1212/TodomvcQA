using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todomvc.Helpers;
using todomvc.Global;
using static todomvc.Global.GlobalDefinitions;
using OpenQA.Selenium.Chrome;

namespace todomvc.Pages
{
    public class todo
    {
        #region webeElements
        private IWebElement newtodo => Driver.driver.FindElement(By.CssSelector(".new-todo"));
        private IList<IWebElement> todolist => Driver.driver.FindElements(By.CssSelector(".todo-list label"));
        private IWebElement editedItem => Driver.driver.FindElement(By.CssSelector(".editing > .edit"));
        private IList<IWebElement> todolistAll => Driver.driver.FindElements(By.CssSelector(".todo-list li"));
        private IList<IWebElement> todolistAllCompleted => Driver.driver.FindElements(By.CssSelector(".todo-list .completed"));

        private IWebElement toggleAll => Driver.driver.FindElement(By.CssSelector(".main > label"));

        //Fileters
        private IWebElement todocount => Driver.driver.FindElement(By.CssSelector(".todo-count strong"));
        private IList<IWebElement> filters => Driver.driver.FindElements(By.CssSelector(".filters a"));
        private IWebElement filtersClearComp => Driver.driver.FindElement(By.CssSelector(".clear-completed"));
        #endregion

        #region methods
        public int AddNewTodoListItem(string elementValue)
        {
            Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".new-todo"), 5);
            
            List<string> expected = new List<string>();
            newtodo.SendKeys(elementValue);
            newtodo.SendKeys(Keys.Enter);

            int count = FindItemOccurrence(elementValue);
            return count;
        }
        public int FindItemOccurrence(string elementValue)
        {
            int count = 0;
            List<string> expected = new List<string>();
            if (todolist.Count() > 0)
            {
                foreach (IWebElement item in todolist)
                {
                    string actualElementValue = item.Text;
                    if (elementValue.ToLower().Equals(actualElementValue.ToLower()))
                        count++;
                }

            }
            return count;
        }
        public int EditItem(string elementValue, string newelementValue)
        {
                Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".new-todo"), 5);
                if (todolist.Count() > 0)
                {
                    foreach (IWebElement item in todolist)
                    {
                        string actualElementValue = item.Text;
                        if (elementValue.ToLower().Equals(actualElementValue.ToLower()))
                        {
                            Actions builder = new Actions(Driver.driver);
                            builder.DoubleClick(item).Perform();
                        
                        if (editedItem != null)
                        {
                            //Clear text for edit item
                            editedItem.Click();
                            editedItem.SendKeys(Keys.Control + "A");
                            editedItem.SendKeys(Keys.Delete);

                            //Send New Text to the edit item
                            editedItem.SendKeys(newelementValue);
                            editedItem.SendKeys(Keys.Enter);
                        }
                        }
                    }
                }
            int count = FindItemOccurrence(newelementValue);
            return count;
        }
        public int ClearItem(string elementValue)
        {
            Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".new-todo"), 5);

            if (todolist.Count() > 0)
            {
                foreach (IWebElement item in todolist)
                {
                    string actualElementValue = item.Text;
                    if (elementValue.ToLower().Equals(actualElementValue.ToLower()))
                    {
                        Actions builder = new Actions(Driver.driver);
                        builder.DoubleClick(item).Perform();

                        if (editedItem != null)
                        {
                            //Clear text for edit item
                            editedItem.Click();
                            editedItem.SendKeys(Keys.Control + "A");
                            editedItem.SendKeys(Keys.Delete);
                            editedItem.SendKeys(Keys.Enter);
                           
                        }
                    }
                }
            }
            int count = FindItemOccurrence(elementValue);
            return count;
        }
        public int DeleteItem(string elementValue)
        {
            Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".new-todo"), 5);
            Actions action = new Actions(Driver.driver);
            int j = todolistAll.Count();
            if (todolistAll.Count() > 0)
            {
                foreach (IWebElement item in todolistAll)
                {                 //Find element with label text
                    IWebElement actualElement = item.FindElement(By.CssSelector(".todo-list label"));
                    string actualElementValue = actualElement.Text;
                    
                    //Find Delete Element
                    IWebElement actualElementdel = item.FindElement(By.CssSelector(".todo-list .destroy"));
  
                    if (elementValue.Equals(actualElementValue))
                    {
                        action.Click(item).Perform();
                        action.MoveToElement(actualElementdel).Click().Perform();
                    }
                }
            }
            int count = FindItemOccurrence(elementValue);
            return count;
        }
        public int DeleteAllItems()
        {
            Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".new-todo"), 5);
            Actions action = new Actions(Driver.driver);
            int j = todolistAll.Count();
            if (todolistAll.Count() > 0)
            {
                foreach (IWebElement item in todolistAll)
                {
                    //Find Delete Element
                    IWebElement actualElementdel = item.FindElement(By.CssSelector(".todo-list .destroy"));

                    action.Click(item).Perform();
                    action.MoveToElement(actualElementdel).Click().Perform();
                }
            }
            int count = todolist.Count();
            return count;
        }
        public int SelectItem(string elementValue, string checkstr)
        {
            Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".new-todo"), 5);
            Actions action = new Actions(Driver.driver);
            int j = todolistAll.Count();
            if (todolistAll.Count() > 0)
            {
                foreach (IWebElement item in todolistAll)
                {   //Find element with label text
                    IWebElement actualElement = item.FindElement(By.CssSelector(".todo-list label"));
                    string actualElementValue = actualElement.Text;
     
                    //Find Delete Element
                    IWebElement actualCheckElement = item.FindElement(By.CssSelector(".todo-list .toggle"));

                    if (elementValue.Equals(actualElementValue))
                    {
                        action.Click(item).Perform();
                        action.MoveToElement(actualCheckElement).Click().Perform();
                    }

                }
            }
            int count = IsItemSelected(elementValue, checkstr); ;
            return count;
        }
        public int SelectAllItems(string checkstr)
        {
            Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".new-todo"), 5);
            Actions action = new Actions(Driver.driver);
            int j = todolistAll.Count();
            if (todolistAll.Count() > 0)
            {
                foreach (IWebElement item in todolistAll)
                {   

                    //Find toggle Element
                    IWebElement actualCheckElement = item.FindElement(By.CssSelector(".todo-list .toggle"));
                    action.Click(item).Perform();
                    action.MoveToElement(actualCheckElement).Click().Perform();
                   

                }
            }
            int count = GetCountAllItemsSelected(checkstr);
            return count;
        }
        public int IsItemSelected(string elementValue, string checkstr)
        {
            int count = 0; 
            Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".new-todo"), 5);
            Actions action = new Actions(Driver.driver);
            int j = todolistAllCompleted.Count();
            if (todolistAllCompleted.Count() > 0)
            {
                foreach (IWebElement item in todolistAllCompleted)
                {   //Find element with label text
                    IWebElement actualElement = item.FindElement(By.CssSelector(".todo-list label"));
                    string actualElementValue = actualElement.Text;

                    if (elementValue.Equals(actualElementValue))
                    {
                        string cssvalue = actualElement.GetCssValue("text-decoration");
                        if(cssvalue.Contains(checkstr))
                        {
                            count++;
                        }
                    }

                }
            }
            return count;
        }
        public int GetCountAllItemsSelected(string checkstr)
        {
            int count = 0;
            Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".new-todo"), 5);
            Actions action = new Actions(Driver.driver);
            int j = todolistAllCompleted.Count();
            if (todolistAllCompleted.Count() > 0)
            {
                foreach (IWebElement item in todolistAllCompleted)
                {   //Find element with label text
                    IWebElement actualElement = item.FindElement(By.CssSelector(".todo-list label"));
                    string actualElementValue = actualElement.Text;

                    string cssvalue = actualElement.GetCssValue("text-decoration");
                    if (cssvalue.Contains(checkstr))
                    {
                        count++;
                    }

                }
            }
            return count;
        }

        public int UncheckItem(string elementValue, string checkstr)
        {
            Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".new-todo"), 5);
            Actions action = new Actions(Driver.driver);
            int j = todolistAllCompleted.Count();
            if (todolistAllCompleted.Count() > 0)
            {
                foreach (IWebElement item in todolistAllCompleted)
                {   
                    //Find element with label text
                    IWebElement actualElement = item.FindElement(By.CssSelector(".todo-list label"));
                    string actualElementValue = actualElement.Text;

                    //Find Delete Element
                    IWebElement actualCheckElement = item.FindElement(By.CssSelector(".todo-list .toggle"));

                    if (elementValue.Equals(actualElementValue))
                    {
                        action.Click(item).Perform();
                        action.MoveToElement(actualCheckElement).Click().Perform();
                    }

                }
            }

            int count = IsItemSelected(elementValue, checkstr); ;
            return count;
        }
        public int UncheckAllItems(string checkstr)
        {
            Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".new-todo"), 5);
            Actions action = new Actions(Driver.driver);
            int j = todolistAllCompleted.Count();
            if (todolistAllCompleted.Count() > 0)
            {
                foreach (IWebElement item in todolistAllCompleted)
                {
                    //Find element with label text
                    IWebElement actualElement = item.FindElement(By.CssSelector(".todo-list label"));
                    string actualElementValue = actualElement.Text;

                    //Find Delete Element
                    IWebElement actualCheckElement = item.FindElement(By.CssSelector(".todo-list .toggle"));

                    action.Click(item).Perform();
                    action.MoveToElement(actualCheckElement).Click().Perform();

                }
            }

            int count = GetCountAllItemsSelected(checkstr); ;
            return count;
        }

        public int ClickToggleAll(string checkstr)
        {
            
            Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".main > label"), 5);
            if(toggleAll != null)
            {
                toggleAll.Click();
            }
            int count = GetCountAllItemsSelected(checkstr); ;
            return count;
        }

        public int GetCount()
        {
            string cnt = "0";
            if (todolist.Count() > 0)
            {
                Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".todo-count strong"), 5);
                if (todocount != null)
                {
                    cnt = todocount.Text;
                }
            }
                
            int count = Convert.ToInt32(cnt);
            return count;
        }

        
        public void ClickFilterElement(string filter)
        {
            Driver.WaitForElementToBeClickable(Driver.driver, By.CssSelector(".new-todo"), 5);

            int cnt = 0;
            if (todolist.Count() > 0)
            {
                int j = filters.Count();
                if(filters.Count() > 0)
                {
                    foreach (IWebElement item in filters)
                    {
                        if (filter.ToLower().Equals(item.Text.ToLower()))
                        {
                            item.Click();
                        }
                    }
                }
                
            }

        }

        public int FilterElement(string filter)
        {
            ClickFilterElement(filter);

            int cnt = 0;
            if (todolist.Count() > 0)
            {
                cnt = todolist.Count();
            }

            int count = cnt;
            return count;
        }

        public int ClearCompletedClick(string checkstr)
        {
            if (todolist.Count() > 0)
            {
                int countSelected = GetCountAllItemsSelected(checkstr);
                if (countSelected > 0)
                {
                    filtersClearComp.Click();
                }
            }
            int count = GetCountAllItemsSelected(checkstr);
            return count;
        }

        public bool ClearCompletedVisible(string clearcompleted, string checkstr)
        {
            string txt = "";
            bool btnEnabled = false;
            if (todolist.Count() > 0)
            {
                int countSelected = GetCountAllItemsSelected(checkstr);
                if (countSelected  > 0)
                {
                    txt = filtersClearComp.Text;
                    if (txt.Equals(clearcompleted))
                        btnEnabled = true;
                }
            }
            return btnEnabled;
        }

        public void OpeNewTab()
        {
            ((IJavaScriptExecutor)Driver.driver).ExecuteScript("window.open();");
            Driver.driver.SwitchTo().Window(Driver.driver.WindowHandles.Last());
            Driver.NavigateURL();
  
        }
        public void Firsttab()
        {
            Driver.driver.SwitchTo().Window(Driver.driver.WindowHandles.First());
        }
         
        #endregion

        #region excel methods
        public struct listItems
        {
            public string title;

        }

        public void GetExcel(int rowNumber, string worksheet, out listItems excelData)
        {
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);

            excelData.title = ExcelLib.ReadData(rowNumber, "Title");

        }
        #endregion

    }
}
