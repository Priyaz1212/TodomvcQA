using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todomvc.Pages;
using static todomvc.Pages.todo;

namespace todomvc.Test
{
    [TestFixture]
    [Parallelizable]
    public class Testtodo :Global.Base
    {
        todo todoobj;

        //validate or Check add new Items in todolist
        [Test, Order(1)]
        public void TC1_01ValidateAddNewItem()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();

            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);

        }

        //Check "edit" item in the todo list
        [Test, Order(2)]
        public void TC1_02ValidateEditItem()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();

            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
            int editOccurences = todoobj.EditItem(excel.title, "Test1New");
            Assert.That(editOccurences == 1, "Item failed to edited");

        }

        //Check  "check/tick" item in the todo list
        [Test, Order(3)]
        public void TC_03ValidateItemChecked()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();

            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);

            //Read row 3 data from excel
            AddNewTodo(3, out excel);

            //Select or check Item
            SelectItem(todoobj, excel.title);


        }
        
        
        //Check "check/tick" all items in the todo list

        [Test, Order(4)]
        public void TC1_04ValidateAllItemsChecked()
        {
            string str = "line-through";
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();

            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
            AddNewTodo(3, out excel);
            AddNewTodo(4, out excel);
            int checkedOcc = todoobj.SelectAllItems(str);
            Assert.That(checkedOcc == 3, "Failed to select/tick all items");

        }

        //Check "Deselect" item in the todo list
        [Test, Order(5)]
        public void TC1_05ValidateItemDeselect()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();
            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);

            //Select Item
            SelectItem(todoobj, excel.title);
            //Deselect Item
            UncheckItem(todoobj, excel.title);

        }
        //Check "Deselect" all items in the todo list
        [Test, Order(6)]
        public void TC1_06ValidateAllItemsDeselect()
        {

            string str = "line-through";
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();

            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
            AddNewTodo(3, out excel);

            int ClearOcc = todoobj.SelectAllItems(str);
            Assert.That(ClearOcc == 2, "Failed to select/tick Item");

            int checkedOcc = todoobj.UncheckAllItems(str);
            Assert.That(checkedOcc == 0, "Failed to Deselect/untick Item");

        }


        //Check "X" button in the todo list
        [Test, Order(7)]
        public void TC1_07ValidateDeleteItem()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();
            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
            AddNewTodo(3, out excel);

            DeleteItem(todoobj, excel.title);


        }
        
        //Check Delete all,click "X" button for all the items

        [Test, Order(8)]
        public void TC1_08ValidateDeleteAllItems()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();
            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
            AddNewTodo(3, out excel);

            int deleteOcc = todoobj.DeleteAllItems();
            Assert.That(deleteOcc == 0, "Failed to delete items");

        }

        //Check "Down arrow" button click in the todo list textbox

        [Test, Order(9)]
        public void TC1_09ValidateSelectAll()
        {
            string str = "line-through";
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();
            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
            AddNewTodo(3, out excel);
            AddNewTodo(4, out excel);

            //click down arrow to select
            int checkedOcc = todoobj.ClickToggleAll(str);
            Assert.That(checkedOcc == 3, "Failed to select/tick all items");

            //Click Again to deselect
            int checkedOcc1 = todoobj.ClickToggleAll(str);
            Assert.That(checkedOcc1 == 0, "Failed to select/tick all items");

        }

        //Check edit and clear item, the item is deleted
        [Test, Order(10)]
        public void TC1_010ValidateClearItem()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();

            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);

            int ClearOcc = todoobj.ClearItem(excel.title);
            Assert.That(ClearOcc == 0, "Item failed to clear");

        }

        //Check the Count
        [Test, Order(11)]
        public void TC2_01ValidateFilterCount()
        {

            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();
            //Add 3 items
            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
            AddNewTodo(3, out excel);
            AddNewTodo(4, out excel);

            //Get Count
            int count = todoobj.GetCount();
            Assert.That(count == 3, "Error in todo-count");

            //Delete one Item
            DeleteItem(todoobj, excel.title);
            
            int newCount = todoobj.GetCount();
            Assert.That(newCount == 2, "Error in todo-count");

            string str = "line-through";
            //Click down arrow to select  
            int checkedOcc = todoobj.ClickToggleAll(str);
            Assert.That(checkedOcc == 2, "Failed to select/tick all items");

            //Get Count
            int countadd = todoobj.GetCount();
            Assert.That(countadd == 0, "Error in todo-count");

            //Click Again to deselect
            int checkedOcc1 = todoobj.ClickToggleAll(str);
            Assert.That(checkedOcc1 == 0, "Failed to select/tick all items");

            //Get Count
            int countadd1 = todoobj.GetCount();
            Assert.That(countadd1 == 2, "Error in todo-count");
        }

        //Validate click "All" filter
        [Test, Order(12)]
        public void TC2_02ValidateFilterAll()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();
            //Add 3 items
            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
            AddNewTodo(3, out excel);
            AddNewTodo(4, out excel);

            SelectItem(todoobj, excel.title);

            //Get Count
            int count = todoobj.FilterElement("All");
            Assert.That(count == 3, "Error in Filter All");
           
        }

        //Validate click "Active" filter
        [Test, Order(13)]
        public void TC2_03ValidateFilterActive()
        {

            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();
            //Add 3 items
            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
            AddNewTodo(3, out excel);
            AddNewTodo(4, out excel);

            SelectItem(todoobj, excel.title);

            //Get Count
            int count = todoobj.FilterElement("Active");
            Assert.That(count == 2, "Error in Filter Active");

        }

        //Validate click "Completed" filter
        [Test, Order(14)]
        public void TC2_04ValidateFilterCompleted()
        {
            string compstr = "completed";
  
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();
            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
            

            //Select 1 item as complete
            SelectItem(todoobj, excel.title);

            AddNewTodo(3, out excel);
            AddNewTodo(4, out excel);

            //Get Count of completed
            int count = todoobj.FilterElement(compstr);
            Assert.That(count == 1, "Error in Filter");

            //Click All
            todoobj.ClickFilterElement("All");

            SelectItem(todoobj, excel.title);

            //Get Count of completed
            int count2 = todoobj.FilterElement(compstr);
            Assert.That(count2 == 2, "Error in Filter");

            //Click All
            todoobj.ClickFilterElement("All");

            //Delete All Items
            DeleteAllItems();

            //Get Count of completed
            int count3 = todoobj.FilterElement(compstr);
            Assert.That(count3 == 0, "Error in Filter");

        }

        //Validate click "Clear Completed" filter
        [Test, Order(15)]
        public void TC2_05ValidateClearCompletedVisible()
        {
            string clearcompstr = "Clear completed";
            string checkstr = "line-through";
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();
            //Add 3 items
            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
            AddNewTodo(3, out excel);
            AddNewTodo(4, out excel);


            //As no item is selected as complete btnEnabled should return False
            bool btnEnabled = todoobj.ClearCompletedVisible(clearcompstr,checkstr);
            Assert.That(!btnEnabled , "Failed: Clear completed button visible");

            //Select 1 item as complete
            SelectItem(todoobj, excel.title);

            //As one item is selected as complete isbtnEnabled should return True
            bool isbtnEnabled = todoobj.ClearCompletedVisible(clearcompstr, checkstr);
            Assert.That(isbtnEnabled, "Failed: Clear completed button not visible");

        }

        [Test, Order(16)]
        public void TC2_06ValidateClearCompletedClick()
        {
            string clearcompstr = "Clear completed";
            string checkstr = "line-through";

            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();
            //Add 3 items
            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
            AddNewTodo(3, out excel);
            AddNewTodo(4, out excel);

            //Select Item
            SelectItem(todoobj, excel.title);

            ClearCompletedClick(todoobj, checkstr);

            //Select all items as completed
            int checkedOcc = todoobj.SelectAllItems(checkstr);
            Assert.That(checkedOcc == 2, "Failed to select/tick Items");

            ClearCompletedClick(todoobj, checkstr);

        }

        //Check if dupliacte todo text is being added
        [Test, Order(17)]
        public void TC3_01ValidateAddDuplicateItem() //Negative Test Case
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();

            //Read from Excel
            listItems excel = new listItems();
            AddNewTodo(2, out excel);
 
            int dupocc = todoobj.AddNewTodoListItem(excel.title);
            if (dupocc > 1)
            {
                Assert.That(dupocc == 1, "Duplicate todo item is added to list");
            }
        }

        //Check for certain special characters input
        [Test, Order(18)]
        public void TC3_02ValidateAddNewSpCharItem()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();
            //Read from Excel
            listItems excel = new listItems();
            todoobj.GetExcel(6, "TodoListItems", out excel);

            int occurences = todoobj.AddNewTodoListItem(excel.title);
            Assert.That(occurences == 1, "Error when input certain special Characters");
    
        }

        //Check updating same element in multiple tabs
        [Test, Order(19)]
        public void TC3_03ValidateOpenNewTab()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            todoobj = new todo();
            //Read from Excel
            listItems excel = new listItems();

            AddNewTodo(2, out excel);

            AddNewTodo(3, out excel);
            todoobj.OpeNewTab();

            todoobj.EditItem(excel.title, "TestNew");

            todoobj.Firsttab();
            
            int count = todoobj.FindItemOccurrence(excel.title);
            Assert.That(count == 0, "Item value not refreshed with the latest value");
            
            int editOccurences = todoobj.EditItem(excel.title, "TestNewValuee5325");
            Assert.That(editOccurences == 0, "Item value being updated is not refreshed");

        }
        public void AddNewTodo(int row,  out listItems excel )
        {
            todoobj.GetExcel(row, "TodoListItems", out excel);
            int occurences = todoobj.AddNewTodoListItem(excel.title);
            Assert.That(occurences == 1, "Item not added to the list");
        }


        public void SelectItem(todo todoobj, string text)
        {
            int ClearOcc = todoobj.SelectItem(text, "line-through");
            Assert.That(ClearOcc == 1, "Failed to select/tick Item");
        }

        public void DeleteAllItems()
        {
            //Delete All Items
            int deleteOccAll = todoobj.DeleteAllItems();
            Assert.That(deleteOccAll == 0, "Failed to delete items");
        }

        public void DeleteItem(todo todoobj, string text)
        {
            //Delete one Item
            int deleteOcc = todoobj.DeleteItem(text);
            Assert.That(deleteOcc == 0, "Failed to delete item");
        }

        public void UncheckItem(todo todoobj, string text)
        {
            int checkedOcc = todoobj.UncheckItem(text, "line-through");
            Assert.That(checkedOcc == 0, "Failed to Deselect/untick Item");
        }

        public void ClearCompletedClick(todo todoobj, string checkstr)
        {
            int countofClearCompleted = todoobj.ClearCompletedClick(checkstr);
            Assert.That(countofClearCompleted == 0, "Error in click Clear Completed");

        }

    }
}
