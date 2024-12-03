using System;
using OpenQA.Selenium;
using System.Threading;

namespace Tzy.Train.B2C.UI.Utility
{
    public class JavaScriptUtility
    {
        public static void flash(IWebElement element, IWebDriver driver)
        {
            String bgcolor = element.GetCssValue("backgroundColor");
            for (int i = 0; i < 50; i++)
            {
                changeColor("#ed0404", element, driver); // 1
                changeColor(bgcolor, element, driver); // 2
            }
        }

        public static void changeColor(String color, IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
            js.ExecuteScript("arguments[0].style.backgroundColor='" + color + "'", element);
            try
            {
                Thread.Sleep(20);
            }
            catch 
            {

            }
        }

        public static void drawBorder(IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
            js.ExecuteScript("arguments[0].style.border='3px solid red'", element);
        }

        public static String getTitleByJS(IWebDriver driver)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
            String title = js.ExecuteScript("return document.title;").ToString();
            return title;
        }

        public static void clickElementByJS(IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
            js.ExecuteScript("arguments[0].click();", element);
        }

        public static void generateAlert(IWebDriver driver, String message)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
            js.ExecuteScript("alert('" + message + "')");
        }

        public static void refreshBrowserByJS(IWebDriver driver)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
            js.ExecuteScript("history.go(e)");
        }

        public static void scrollIntoView(IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
            js.ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'end', inline: 'nearest'});", element);
        }

        public static void scrollPageDown(IWebDriver driver)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        public static void elementClick(IWebDriver driver, String path)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
            js.ExecuteScript("document.getElementById(" + path + ").click();");
        }

        public static void creatAlert(IWebDriver driver)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
            js.ExecuteScript("window.alert('Create a Alert');");
        }

        public static void scrollBy(IWebDriver driver, int vert, int horiz)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(" + vert + "," + horiz + ")");
        }
        public static void scrollTo(IWebDriver driver, int vert, int horiz)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(" + vert + "," + horiz + ")");
        }
        public static void scrollToHeight(IWebDriver driver, String script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");
        }
        public static void scrollToWidth(IWebDriver driver, String script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,document.body.scrollWidth)");
        }
    }

}
