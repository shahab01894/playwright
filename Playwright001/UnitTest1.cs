using AventStack.ExtentReports;
using Microsoft.Playwright;
using Playwright001.Utilities;
using SkiaSharp;

namespace Playwright001
{
    public class Tests
    {
        CommonUtilities _utilities=new CommonUtilities();
        ReportManager _reportManager=new ReportManager();
        protected static ExtentReports extent;
        protected ExtentTest test;

        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;

        [SetUp]
        public async Task Setup()
        {
            // Initialize Playwright once per test
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true,
                SlowMo = 50,
                Timeout = 80000
            });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();

            // Initialize ExtentReports (make sure ReportManager returns the same instance)
            if (extent == null)
            {
                extent = _reportManager.GetExtentReports();
            }
        }

        [Test]
        public async Task Amazon()
        {
         
           
            test = extent.CreateTest("Test case 001");

            await _page.GotoAsync(MyResource.AmzUrl);
            test.Log(Status.Pass, "Navigated to the URL");
            await _page.ClickAsync("//input[@data-action-type='DISMISS']");
            test.Log(Status.Pass, "Click on the dismiss button");
        }
        [Test]
        public async Task DemoQA1()
        {
           
           // extent = _reportManager.GetExtentReports();
            test = extent.CreateTest("Test case 002");
            // await page.GotoAsync("https://www.amazon.com/");

            await _page.GotoAsync(MyResource.DemoQA);
            test.Log(Status.Pass, "Navigated to the URL");
            var excelData= _utilities.GetExcelData();
            test.Log(Status.Pass, "Get the Excel data sucessfully");
            await _page.Locator("#addNewRecordButton").ClickAsync();
            test.Log(Status.Pass, "Click on the Add button");
            await _page.Locator("//input[@placeholder='First Name']").FillAsync(excelData["FirstName"]);
            test.Log(Status.Pass, "Click on the Add button");

            await _page.Locator("//input[@placeholder='Last Name']").FillAsync(excelData["Lastname"]);
            test.Log(Status.Pass, " ");
            await _page.Locator("//input[@placeholder='name@example.com']").FillAsync(excelData["Email"]);
            test.Log(Status.Pass, " ");
            await _page.Locator("//input[@placeholder='Age']").FillAsync(excelData["Age"]);
            test.Log(Status.Pass, " ");
            await _page.Locator("//input[@placeholder='Salary']").FillAsync(excelData["Salary"]);
            test.Log(Status.Pass, " ");
            await _page.Locator("//input[@placeholder='Department']").FillAsync(excelData["Department"]);
            test.Log(Status.Pass, "Enter the department success");
            await _page.Locator("#submit").ClickAsync();
            test.Log(Status.Pass, "Click on the submit button");


            /* await page.Locator("//input[@data-action-type='DISMISS']").ClickAsync();
             var btndismiss = page.Locator("//input[@data-action-type='DISMISS']");
             var isVisible=await btndismiss.IsVisibleAsync();
             if (isVisible == false)
             {
                 Console.WriteLine("Dismiss is not present on the screen");
             }*/
        }
        [TearDown]
        public async Task Teardown()
        {
            if (_context != null)
                await _context.CloseAsync();

            if (_browser != null)
                await _browser.CloseAsync();

            _playwright?.Dispose();

        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent.Flush();  // Flush only ONCE at the end
        }
    }
}
