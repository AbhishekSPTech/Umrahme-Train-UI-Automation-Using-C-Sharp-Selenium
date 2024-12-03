using System.Diagnostics;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Tzy.Train.B2C.UI
{
	public class Browser
	{
		public IWebDriver WEBDRIVER { get; set; }

		/// <summary>
		/// This Method will be used to setup specific browser
		/// Pass this as string : chrome,firefox,edge,headless
		/// <param name="browsername"></param>
		/// </summary>
		public void SETUP(string browsername)
		{
			if (browsername == "chrome")
			{
				new DriverManager().SetUpDriver(new ChromeConfig());
				WEBDRIVER = new ChromeDriver();
				WEBDRIVER.Manage().Window.Maximize();

				#region LambdaTest Configuration
				// ChromeOptions capabilities = new ChromeOptions();
				// capabilities.BrowserVersion = "130";
				// Dictionary<string, object> ltOptions = new Dictionary<string, object>();
				// // ltOptions.Add("username", LT_USERNAME);
				// // ltOptions.Add("accessKey", LT_ACCESS_KEY);
				// ltOptions.Add("username", "sagartraveazy");
				// ltOptions.Add("accessKey", "OR4AXXsFczwobOGINGsWUpp90b7JfOJJpO2wfTgWls3dTMpwZz");
				// ltOptions.Add("geoLocation", "IN");
				// ltOptions.Add("visual", true);
				// ltOptions.Add("video", true);
				// ltOptions.Add("platformName", "Windows 10");
				// ltOptions.Add("timezone", "Kolkata");
				// ltOptions.Add("build", "1");
				// ltOptions.Add("project", "Tzy.Train.B2C.UI");
				// ltOptions.Add("console", "true");
				// ltOptions.Add("networkThrottling", "Regular 4G");
				// ltOptions.Add("w3c", true);
				// ltOptions.Add("plugin", "c#-c#");
				// ltOptions.Add("tunnel", true);
				// capabilities.AddAdditionalOption("LT:Options", ltOptions);
				#endregion

			}
			else if (browsername == "firefox")
			{
				new DriverManager().SetUpDriver(new FirefoxConfig());
				WEBDRIVER = new FirefoxDriver();
				WEBDRIVER.Manage().Window.Maximize();
			}
			else if (browsername == "edge")
			{
				new DriverManager().SetUpDriver(new EdgeConfig());
				WEBDRIVER = new EdgeDriver();
			}
			else if (browsername == "headless")
			{
				ChromeOptions options = new();
				options.AddArgument("--headless");
				options.AddArgument("--window-size=1920,1080");
				new DriverManager().SetUpDriver(new ChromeConfig());
				WEBDRIVER = new ChromeDriver(options);
			}
			// Set implicit wait
			WEBDRIVER.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Adjust the time as needed
		}

		public async Task LogNetworkRequests(IWebDriver driver, string url)
		{
			INetwork interceptor = driver.Manage().Network;
			interceptor.NetworkRequestSent += OnNetworkRequestSent;
			interceptor.NetworkResponseReceived += OnNetworkResponseReceived;
			await interceptor.StartMonitoring();
			driver.Url = url;
			await interceptor.StopMonitoring();
		}

		private void OnNetworkRequestSent(object sender, NetworkRequestSentEventArgs e)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("Request {0}", e.RequestId).AppendLine();
			builder.AppendLine("--------------------------------");
			builder.AppendFormat("{0} {1}", e.RequestMethod, e.RequestUrl).AppendLine();
			foreach (KeyValuePair<string, string> header in e.RequestHeaders)
			{
				builder.AppendFormat("{0}: {1}", header.Key, header.Value).AppendLine();
			}
			builder.AppendLine("--------------------------------");
			builder.AppendLine();
			Debug.WriteLine(builder.ToString());
		}

		private void OnNetworkResponseReceived(object sender, NetworkResponseReceivedEventArgs e)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("Response {0}", e.RequestId).AppendLine();
			builder.AppendLine("--------------------------------");
			builder.AppendFormat("{0} {1}", e.ResponseStatusCode, e.ResponseUrl).AppendLine();
			foreach (KeyValuePair<string, string> header in e.ResponseHeaders)
			{
				builder.AppendFormat("{0}: {1}", header.Key, header.Value).AppendLine();
			}

			if (e.ResponseResourceType == "Document")
			{
				builder.AppendLine(e.ResponseBody);
			}
			else if (e.ResponseResourceType == "Script")
			{
				builder.AppendLine("<JavaScript content>");
			}
			else if (e.ResponseResourceType == "Stylesheet")
			{
				builder.AppendLine("<stylesheet content>");
			}
			else if (e.ResponseResourceType == "Image")
			{
				builder.AppendLine("<image>");
			}
			else
			{
				builder.AppendFormat("Content type: {0}", e.ResponseResourceType).AppendLine();
			}

			builder.AppendLine("--------------------------------");
			Console.WriteLine(builder.ToString());
			Debug.WriteLine(builder.ToString());
		}
	}
}
