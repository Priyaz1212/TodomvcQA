using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todomvc.Config;
using todomvc.Helpers;
using static todomvc.Global.GlobalDefinitions;

namespace todomvc.Global
{
    public class Base
    {

        #region To access Path from resource file
        public static string URL = Resource.URL;
        public static int Browser = Int32.Parse(Resource.Browser);
        public static string ExcelPath = Resource.ExcelPath;
        public static string ScreenshotPath = Resource.ScreenShotPath;
        public static string ReportPath = Resource.ReportPath;
        #endregion

        #region reports
        public static AventStack.ExtentReports.ExtentReports extent;
        public static AventStack.ExtentReports.ExtentTest test;
        #endregion



        #region setup and tear down
        [OneTimeSetUp]
        public void ExtentStart()
        {
            //Initialize report
            string reportName = ReportPath
            + Path.DirectorySeparatorChar + "Report_" + DateTime.Now.ToString("_dd-MM-yyyy_HHmm")
            + Path.DirectorySeparatorChar;

            //start reporters
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportName);
            extent = new AventStack.ExtentReports.ExtentReports();

            extent.AttachReporter(htmlReporter);


        }
        #endregion

        #region setup
        [SetUp]
        public void Intialize()
        {
            switch (Browser)
            {
                case 1:
                    Driver.driver = new FirefoxDriver();
                    Driver.driver.Manage().Window.Maximize();
                    break;
                case 2:
                    Driver.driver = new ChromeDriver();
                    Driver.driver.Manage().Window.Maximize();
                    break;

            }

            Driver.NavigateURL();
            
        }
        #endregion

        #region Teardown
        [TearDown]
        public void TearDown()
        {
            // Screenshot
            String img = SaveScreenShotClass.SaveScreenshot(Driver.driver, "Report");
            
            var exec_status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMessage = TestContext.CurrentContext.Result.Message;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? ""
            : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);

            string TC_Name = TestContext.CurrentContext.Test.Name;
            string base64 = SaveScreenShotClass.GetScreenshot(Driver.driver);

            Status logStatus = Status.Pass;
            switch (exec_status)
            {
                case TestStatus.Failed:

                    logStatus = Status.Fail;
                    test.Log(Status.Fail, exec_status + errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64).Build());
                    break;

                case TestStatus.Skipped:

                    logStatus = Status.Skip;
                    test.Log(Status.Skip, errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64).Build());
                    break;

                case TestStatus.Inconclusive:

                    logStatus = Status.Warning;
                    test.Log(Status.Warning, "Test ");
                    break;

                case TestStatus.Passed:

                    logStatus = Status.Pass;
                    test.Log(Status.Pass, "Test Passed");
                    break;

                default:
                    break;
            }

            // Close the driver :)          
            Driver.driver.Close();
            Driver.driver.Quit();
        }

        [OneTimeTearDown]
        protected void ExtentClose()
        {
            // calling Flush writes everything to the log file (Reports)
            extent.Flush();
        }
        #endregion
    }
}
