using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Tzy.Train.B2C.UI.Services;

namespace Tzy.Train.B2C.UI
{
	public class Helper : IDisposable
	{
		public IWebDriver driver { get; set; }
		public Browser browser { get; set; }
		public static string B2B_URL { get; set; }
		public string WEB_URL { get; set; }
		public static string NODE_URL { get; set; }
		// public static string driver { get; set; }
		public Configsetting Configsetting { get; private set; }
		public UIServices services { get; set; }

		public Helper()
		{
			browser = new Browser();
			services = new UIServices();
			services.ExtentStart();
		}
		public static ExtentTest test;
		private static WebDriverWait wait;

        /// <summary>
        /// Initializes the WebDriver and ExtentTest for a test.
        /// </summary>
        /// <param name="testName">Name of the test for ExtentReports.</param>
        /// <param name="url">URL to navigate to.</param>
        /// <returns>The initialized WebDriver.</returns>
        public (IWebDriver Driver, ExtentTest ExtentTest) InitializeTest(string testName, string browsertype, string url)
		{
			var extentTest = services.EXTENT.CreateTest(testName);
			extentTest.Log(Status.Info, $"Initializing test: {testName}");

			WEB_URL = url;
			browser.SETUP(browsertype); // Defaulting to Chrome; can be parameterized if needed.
			var driver = browser.WEBDRIVER;

			driver.Navigate().GoToUrl(WEB_URL);
			extentTest.Log(Status.Info, $"Navigated to URL: {WEB_URL}");

			return (driver, extentTest);
		}

		/// <summary>
		/// Finalizes the test by capturing a screenshot and quitting the WebDriver.
		/// </summary>
		/// <param name="driver">The WebDriver instance to quit.</param>
		/// <param name="extentTest">The ExtentTest instance for reporting.</param>
		public void FinalizeTest(IWebDriver driver, ExtentTest? extentTest)
		{
			// try
			// {
			//     UIServices.TakeScreenShot(driver, extentTest.Model.Name);
			//     extentTest?.AddScreenCaptureFromPath("../../../Screenshots/ScreenshotsTrain.png");
			// }
			// finally
			// {
			//     driver.Quit();
			// }
		}
		public string GetPageTitle(IWebDriver driver)
		{
			return driver.Title;
		}
		public string GetCurrentUrl(IWebDriver driver)
		{
			return driver.Url;
		}
		public static void DatePicker(DateTime desiredDate, IWebDriver driver)
		{
			DatePickerPage datePicker = new DatePickerPage(driver);
			datePicker.SelectYear(desiredDate.Year);
			datePicker.SelectMonth(desiredDate.Month);
			datePicker.SelectDay(desiredDate.Day);
		}
		public static void SelectYearInCalendar(int year, IWebDriver driver, WebDriverWait wait)
		{
			while (true)
			{
				IWebElement yearDropdown = driver.FindElement(By.ClassName("yearselect"));
				int currentYear = int.Parse(yearDropdown.GetAttribute("value"));

				if (currentYear == year)
				{
					break;
				}
				else if (currentYear < year)
				{
					IWebElement nextButton = wait.Until(d => d.FindElement(By.CssSelector(".next.available")));
					nextButton.Click();
				}
				else
				{
					IWebElement prevButton = wait.Until(d => d.FindElement(By.CssSelector(".prev.available")));
					prevButton.Click();
				}
			}
		}

		public static bool IsElementVisible(By elementLocator, WebDriverWait wait)
		{
			try
			{
				return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator)).Displayed;
			}
			catch (WebDriverTimeoutException)
			{
				return false; // Return false if element is not visible within the wait time
			}
		}
		public static void TestGrid()
		{
			NODE_URL = "http://100.96.1.91:5555/wd/hub"; //Pass Node URL Here
			var capabilities = new ChromeOptions();
			capabilities.PlatformName = "Windows 10";
			capabilities.BrowserVersion = "108.0.5359.7100";
			RemoteWebDriver driver = new RemoteWebDriver(new Uri(NODE_URL), capabilities);
			driver.Manage().Window.FullScreen();
			driver.Navigate().GoToUrl(B2B_URL);

		}
		public void Dispose()
		{
			browser.WEBDRIVER.Quit();
			services.EXTENT?.Flush();
		}


	}

}
