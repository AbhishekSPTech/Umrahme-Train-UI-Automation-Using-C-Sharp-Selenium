using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Tzy.Train.B2C.UI.Pages
{
    public class ConfirmationUI(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;

        #region  Locators
        private readonly By TxtBookingConfirmation = By.XPath("//div[@id='div-confirmation-detail']//descendant::div[contains(text(),'Booking Confirmation')]");
        #endregion

        readonly WebDriverWait wait = new(driver, TimeSpan.FromSeconds(30));

        #region  Actions
        public string VerifyBookingConfirmation()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(TxtBookingConfirmation));
            return driver.FindElement(TxtBookingConfirmation).Text;
        }

        #endregion
    }
}
