using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public class DatePickerPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public DatePickerPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private IWebElement YearDropdown => _wait.Until(d => d.FindElement(By.XPath("//div[contains(@style, 'display: block;') and contains(@class, 'daterangepicker')]//div[contains(@style, 'display: block;')]//child::select[@class='yearselect']")));
    private IWebElement MonthDropdown => _wait.Until(d => d.FindElement(By.XPath("//div[contains(@style, 'display: block;') and contains(@class, 'daterangepicker')]//div[contains(@style, 'display: block;')]//child::select[@class='monthselect']")));
    public IWebElement GetDayElement(int day) => _wait.Until(d => d.FindElement(By.XPath($"//div[contains(@style, 'display: block;') and contains(@class, 'daterangepicker')]//div[contains(@style, 'display: block;')]//child::td[contains(@class, 'available') and text()='{day}']")));

    public void SelectYear(int year)
    {
        var selectYear = new SelectElement(YearDropdown);
        selectYear.SelectByValue(year.ToString());
    }
    public void SelectMonth(int month)
    {
        var selectMonth = new SelectElement(MonthDropdown);
        selectMonth.SelectByValue((month - 1).ToString());
    }
    public void SelectDay(int day)
    {
        IWebElement dayElement = GetDayElement(day);
        dayElement.Click();
    }
}
