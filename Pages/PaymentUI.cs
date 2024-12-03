using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Tzy.Train.B2C.UI.Pages
{
    public class PaymentUI(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;

        #region  Locators
        private readonly By BtnCard = By.XPath("//a[@id='cardbtn']");
        private readonly By BtnMada = By.XPath("//a[@id='madabtn']");
        private readonly By BtnPaypal = By.XPath("//div[@id='buttons-container']]");
        private readonly By TxtCardNo = By.XPath("//input[@id='cardNoInput']");
        private readonly By TxtExpiry = By.XPath("//input[@id='expDateInput']");
        private readonly By TxtCvv = By.XPath("//input[@id='cvvInput']");
        private readonly By TxtName = By.XPath("//input[@id='chNameInput']");
        private readonly By BtnSubmit = By.XPath("//button[@id='submitBtn']");
        private readonly By TxtOtp = By.XPath("//*[@id='Secure3dsForm_password']");
        private readonly By BtnOtpSubmit = By.XPath("//*[@id='submit-simulator']");
        private readonly By EleCard = By.XPath("//*[@id='card']");
        private readonly By ButtonRetry = By.XPath("//button[@id='retryButton']");
        #endregion

        readonly WebDriverWait wait = new(driver, TimeSpan.FromSeconds(30));

        #region  Actions
        public void ClickCardButton()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(BtnCard));
            driver.FindElement(BtnCard).Click();
        }
        public void ClickMadaButton()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(BtnMada));
            driver.FindElement(BtnMada).Click();
        }
        public void ClickPaypalButton()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(BtnPaypal));
            driver.FindElement(BtnPaypal).Click();
        }
        public void EnterCardNumber(string cardNo)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(TxtCardNo));
            driver.FindElement(TxtCardNo).SendKeys(cardNo);
        }
        public void EnterExpiryDate(string expiry)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(TxtExpiry));
            driver.FindElement(TxtExpiry).SendKeys(expiry);
        }
        public void EnterCvv(string cvv)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(TxtCvv));
            driver.FindElement(TxtCvv).SendKeys(cvv);
        }
        public void EnterName(string name)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(TxtName));
            driver.FindElement(TxtName).SendKeys(name);
        }
        public void ClickSubmitButton()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(BtnSubmit));
            driver.FindElement(BtnSubmit).Click();
        }
        public void EnterOtp(string otp)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(TxtOtp));
            driver.FindElement(TxtOtp).SendKeys(otp);
        }
        public void ClickOtpSubmitButton()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(BtnOtpSubmit));
            driver.FindElement(BtnOtpSubmit).Click();
        }
        public bool IsOtpDisplayed()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(TxtOtp));
            return driver.FindElement(EleCard).Displayed;
        }
        #endregion
    }
}
