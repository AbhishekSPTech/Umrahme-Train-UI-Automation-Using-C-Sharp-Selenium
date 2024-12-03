using OpenQA.Selenium;

namespace Tzy.Train.B2C.UI.Pages
{
    public class HomeUI(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;

        // Locators
        private readonly By trainLink = By.XPath("/html/body/div[2]/div/div/div[5]/a");
        // private readonly By trainLink = By.XPath("//div[contains(@class,'position-relative')]//child::a[contains(@href,'train')]");

        // Actions
        public void NavigateToTrainHome()
        {
            driver.FindElement(trainLink).Click();
        }
    }
}
