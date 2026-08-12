using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Playwright001.Utilities
{
    internal class ReportManager
    {
        protected ExtentReports _extent;
        protected ExtentSparkReporter _sparkReporter;

        public ExtentReports GetExtentReports()
        { 
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string reporterpath = Path.GetFullPath(Path.Combine(baseDir, @"C:\Users\DELL\source\repos\Playwright001\Playwright001\Reports"));
        string fullpath = Path.Combine(reporterpath, "AutomationReport.html");

            _sparkReporter = new ExtentSparkReporter(fullpath);

            _sparkReporter.Config.DocumentTitle = "Automation Test Execution Report";
            _sparkReporter.Config.ReportName = "Selenium Regression Results";
            _sparkReporter.Config.Theme=Theme.Dark;

            _extent = new ExtentReports();
            _extent.AttachReporter(_sparkReporter);

            _extent.AddSystemInfo("Environment", "QA");
            _extent.AddSystemInfo("Tester", "Shahab");
            _extent.AddSystemInfo("Browser", "Chrome");

            return _extent;
        }
    }
}
