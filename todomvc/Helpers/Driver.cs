using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todomvc.Global;

namespace todomvc.Helpers
{
    public class Driver
    {
        public static IWebDriver driver { get; set; }
        public static string BaseURL
        {
            get { return Base.URL; }
        }

        public static void NavigateURL()
        {
            driver.Navigate().GoToUrl(BaseURL);

        }
        public static void ImplicitWaitMethod()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        public static IWebElement WaitForElementToBeClickable(IWebDriver driver, By by, int timeOutinSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutinSeconds));
            return (wait.Until(ExpectedConditions.ElementToBeClickable(by)));
        }

        public void OpenNewTab()
        {
            ((IJavaScriptExecutor)Driver.driver).ExecuteScript("window.open();");
            Driver.driver.SwitchTo().Window(Driver.driver.WindowHandles.Last());

            Driver.driver.Navigate().GoToUrl("https://todomvc.com/examples/react/");

        }
    }
}
