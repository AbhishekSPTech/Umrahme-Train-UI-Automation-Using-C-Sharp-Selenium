using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tzy.Train.B2C.UI
{
	public class Waits
	{
		/// <summary>
		/// This Method will wait for the given time to check whether the respective element is displayed or not
		/// This Method will throw TimeOutException in case the given element is not displayed
		/// <param name="driver"></param>
		/// <param name="ele"></param>
		/// <param name="time"></param>
		/// </summary>
		public static bool Wait_IsDisplayed(IWebDriver driver, IWebElement ele, int time)
		{
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
			return wait.Until(d => ele.Displayed);
		}

		/// <summary>
		/// This Method will wait for the given time to check whether the respective element is enabled or not
		/// This Method will throw TimeOutException in case the given element is not enabled
		/// <param name="driver"></param>
		/// <param name="ele"></param>
		/// <param name="time"></param>
		/// </summary>
		public static void Wait_IsEnabled(IWebDriver driver, IWebElement ele, int time)
		{
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
			wait.Until(d => ele.Enabled);
		}

		/// <summary>
		/// This Method will wait for the given time to check whether the respective element is selected or not
		/// This Method will throw TimeOutException in case the given element is not selected
		/// <param name="driver"></param>
		/// <param name="ele"></param>
		/// <param name="time"></param>
		/// </summary>
		public static void Wait_IsSelected(IWebDriver driver, IWebElement ele, int time)
		{
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
			wait.Until(d => ele.Selected);
		}

		/// <summary>
		/// This Method will wait for the given time to check whether the respective element is clickable or not
		/// This Method will throw TimeOutException in case the element is not clickable
		/// <param name="driver"></param>
		/// <param name="ele"></param>
		/// <param name="time"></param>
		/// </summary>
		public static void Wait_IsClickable(IWebDriver driver, IWebElement ele, int time)
		{
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ele));
		}

		/// <summary>
		/// This Method will wait for the given time to check whether the respective text is present in the element or not
		/// This Method will throw TimeOutException in case the text is not present
		/// <param name="driver"></param>
		/// <param name="ele"></param>
		/// <param name="text"></param>
		/// <param name="time"></param>
		/// </summary>
		public static void Wait_ForText(IWebDriver driver, IWebElement ele, string text, int time)
		{
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
			wait.Until(d => ele.Text.Contains(text));
		}

		/// <summary>
		/// This Method will wait for the given time to check whether the respective element is invisible or not
		/// This Method will throw TimeOutException in case the element is still visible
		/// <param name="driver"></param>
		/// <param name="ele"></param>
		/// <param name="time"></param>
		/// </summary>
		public static void Wait_IsInvisible(IWebDriver driver, IWebElement ele, int time)
		{
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.Id(ele.GetAttribute("id"))));
		}
	}
}
