using AventStack.ExtentReports;
using NPOI.POIFS.Properties;
using NPOI.SS.Formula.Functions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Tzy.Train.B2C.UI.Utility;
using Xunit;

namespace Tzy.Train.B2C.UI.Pages
{
    public class TrainUI(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;

        #region  Locators

        #region Search
        private readonly By DDDeparture = By.XPath("//*[@id='select-first-departure']");
        private readonly By DDDepartureOption = By.XPath("//*[contains(@id, 'MKX')]");
        private readonly By DDArrival = By.XPath("//*[@id='select-first-arrival']");
        private readonly By DDArrivalOption = By.XPath("//*[contains(@id, 'DMX')]");
        private readonly By SelectDeparture = By.XPath("//*[@id='kt_daterangepicker_3']");
        private readonly By DDTravelers = By.XPath("//*[@id='dropdownMenuTraveler']");
        private readonly By TxtAdult = By.XPath("//div[contains(@class,'d-flex flex-column')][1]//div[contains(@class,'fw-bold') and text()='Adults']");
        private readonly By TxtAdultDesp = By.XPath("//div[contains(@class,'d-flex flex-column')][1]//div[contains(@class,'text-secondary') and text()='Ages 12+ yrs.']");
        private readonly By BtnAdultRemove = By.XPath("//button[contains(@class,'adult-decrement')]");
        private readonly By SelectAdult = By.XPath("//input[contains(@class,'adult-count')]");
        private readonly By BtnAdultAdd = By.XPath("//button[contains(@class,'adult-increment')]");
        private readonly By TxtChild = By.XPath("//div[contains(@class,'d-flex flex-column')][2]//div[contains(@class,'fw-bold') and text()='Children']");
        private readonly By TxtChildDesp = By.XPath("//div[contains(@class,'d-flex flex-column')][2]//div[contains(@class,'text-secondary') and text()='Ages 2-12 yrs.']");
        private readonly By BtnChildRemove = By.XPath("//button[contains(@class,'child-decrement')]");
        private readonly By SelectChild = By.XPath("//input[contains(@class,'child-count')]");
        private readonly By BtnChildAdd = By.XPath("//button[@id='child-increment']");
        private readonly By TxtInfant = By.XPath("//div[contains(@class,'d-flex flex-column')][3]//div[contains(@class,'fw-bold') and text()='Infants']");
        private readonly By TxtInfantDesp = By.XPath("//div[contains(@class,'d-flex flex-column')][3]//div[contains(@class,'text-secondary') and text()='Ages 0-2 yrs.']");
        private readonly By BtnInfantRemove = By.XPath("//button[contains(@class,'infant-decrement')]");
        private readonly By SelectInfant = By.XPath("//input[contains(@class,'infant-count')]");
        private readonly By BtnInfantAdd = By.XPath("//button[contains(@class,'infant-increment')]");
        private readonly By BtnApplyOccupancy = By.XPath("//button[contains(@class,'btn-primary') and contains(@class,'get-travelers')]");
        private readonly By BtnSearchTrain = By.XPath("//*[@id='btn-search-train']");
        private readonly By LinkTimetable = By.XPath("//*[@id='collapseSearch']//ancestor::a");
        #endregion

        #region Listing
        private readonly By BtnBookNowEconomy = By.XPath("//div[@id='collapseTrainDetail_0']//descendant::div[@data-fc='Y']//button");
        private readonly By BtnBookNowBusiness = By.XPath("//div[@id='collapseTrainDetail_0']//descendant::div[@data-fc='C']//button");
        #endregion

        #region Guest Details
        private readonly By TxtBookingDetails = By.XPath("//*[contains(text(),'Booking Details')]");
        private readonly By BtnBackToListing = By.XPath("//*[contains(text(),'Back to Train Listing Page')]");
        private readonly By SelectTitleBuyer = By.XPath("//*[@id='BuyerInfo_NamePrefix']");
        private readonly By SelectGivenNameBuyer = By.XPath("//*[@id='BuyerInfo_GivenName']");
        private readonly By SelectSurnameBuyer = By.XPath("//*[@id='BuyerInfo_SurName']");
        private readonly By SelectPhoneNoBuyer = By.XPath("//*[@id='BuyerInfo_PhoneNo']");
        private readonly By SelectEmailBuyer = By.XPath("//*[@id='BuyerInfo_EmailId']");
        private readonly By SelectTitlePax = By.XPath("//*[@id='Traveler_0__NamePrefix']");
        private readonly By SelectGivenNamePax = By.XPath("//*[@id='Traveler_0__GivenName']");
        private readonly By SelectSurnamePax = By.XPath("//*[@id='Traveler_0__SurName']");
        private readonly By SelectDateOfBirthPax = By.XPath("//*[@id='Traveler[0].BirthDate']");
        private readonly By SelectDocumentTypePax = By.XPath("//*[@id='Traveler[0].PassportType']");
        private readonly By SelectCORPAX = By.XPath("//*[@id='Traveler[0].CountryOfResidence']");
        private readonly By SelectNationalityPax = By.XPath("//*[@id='Traveler[0].Nationality']");
        private readonly By SelectIDNoPax = By.XPath("//*[@id='Traveler_0__PassportNo' and @name='Traveler[0].NationalId']");
        private readonly By SelectIDExpiryPax = By.XPath("//*[@id='Traveler[0].NationalIdExpiryDate']");
        private readonly By BtnContinue = By.XPath("//*[contains(text(),'Confirm & Continue')]");
        private readonly By BtnBackToDetails = By.XPath("//*[contains(text(),'Back to Detail page')]");
        #endregion

        #endregion

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

        #region  Actions

        #region Search
        public void SelectDepartureStation(string Departure)
        {
            // driver.FindElement(DDDeparture).Click();
            // JavaScriptUtility.drawBorder(driver.FindElement(DDDeparture), driver);
            SelectElement select = new SelectElement(driver.FindElement(DDDeparture));
            select.SelectByValue(Departure);
            // driver.FindElement(DDDepartureOption).Click();string Dept
        }

        public void SelectArrivalStation(string Arrival)
        {
            // driver.FindElement(DDArrival).Click();
            // JavaScriptUtility.drawBorder(driver.FindElement(DDArrival), driver);
            SelectElement select = new SelectElement(driver.FindElement(DDArrival));
            select.SelectByValue(Arrival);
            // driver.FindElement(DDArrivalOption).Click();
        }

        public void SelectTravelers(int adults, int children, int infants)
        {
            driver.FindElement(DDTravelers).Click();
            var adultElement = driver.FindElement(SelectAdult);
            var childElement = driver.FindElement(SelectChild);
            var infantElement = driver.FindElement(SelectInfant);
            Actions actions = new Actions(driver);
            actions.MoveToElement(adultElement).Click().SendKeys(Keys.Control + "a").SendKeys(Keys.Backspace).SendKeys(adults.ToString()).Build().Perform();
            actions.MoveToElement(childElement).Click().SendKeys(Keys.Control + "a").SendKeys(Keys.Backspace).SendKeys(children.ToString()).Build().Perform();
            actions.MoveToElement(infantElement).Click().SendKeys(Keys.Control + "a").SendKeys(Keys.Backspace).SendKeys(infants.ToString()).Build().Perform();
            driver.FindElement(BtnApplyOccupancy).Click();
        }

        public void SelectDepartureDate()
        {
            driver.FindElement(SelectDeparture).Click();
            string dateXPath = $"//div[@class='calendar-table']//ancestor::div[contains(@style, 'display: block;') and contains(@class, 'daterangepicker')]//div[contains(@style, 'display: block;')]//child::td[contains(@class, 'available') and text()='{DateTime.Now.AddDays(10).Day}']";
            driver.FindElement(By.XPath(dateXPath)).Click();
        }

        public void ClickSearchButton()
        {
            driver.FindElement(BtnSearchTrain).Click();
        }
        #endregion

        #region Listing
        public bool IsTrainListingDisplayed()
        {
            if (Helper.IsElementVisible(BtnBookNowEconomy, wait))
            {
                return true;
            }
            if (Helper.IsElementVisible(BtnBookNowBusiness, wait))
            {
                return true;
            }
            throw new NoSuchElementException("No 'Book Now' button is visible.");
        }
        public void ClickBookNowButton()
        {
            if (Helper.IsElementVisible(BtnBookNowEconomy, wait))
            {
                if (driver.FindElement(BtnBookNowEconomy).Displayed)
                {
                    var economyButton = driver.FindElement(BtnBookNowEconomy);
                    economyButton.Click();
                    return;
                }


            }
            if (Helper.IsElementVisible(BtnBookNowBusiness, wait))
            {
                if (driver.FindElement(BtnBookNowBusiness).Displayed)
                {
                    var businessButton = driver.FindElement(BtnBookNowBusiness);
                    businessButton.Click();
                    return;
                }
            }
            else
            {
                throw new Exception("No 'Book Now' button is visible.");
            }
        }

        #endregion

        #region Guest Details
        public void ClickBackToListingButton()
        {
            driver.FindElement(BtnBackToListing).Click();
        }
        public bool IsBookingDetailsTextDisplayed()
        {
            try
            {
                return driver.FindElement(TxtBookingDetails).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public void SelectBuyerTitle(string title)
        {
            SelectElement select = new SelectElement(driver.FindElement(SelectTitleBuyer));
            select.SelectByValue(title);
        }
        public void EnterBuyerGivenName(string GivenName)
        {
            driver.FindElement(SelectGivenNameBuyer).SendKeys(GivenName);
        }
        public void EnterBuyerSurname(string Surname)
        {
            driver.FindElement(SelectSurnameBuyer).SendKeys(Surname);
        }
        public void EnterPhoneNo(string PhoneNo)
        {
            driver.FindElement(SelectPhoneNoBuyer).SendKeys(PhoneNo);
        }
        public void EnterEmail(string Email)
        {
            driver.FindElement(SelectEmailBuyer).SendKeys(Email);
        }
        public void SelectPaxTitle(string title)
        {
            SelectElement select = new SelectElement(driver.FindElement(SelectTitlePax));
            select.SelectByValue(title);
        }
        public void EnterPaxGivenName(string GivenName)
        {
            driver.FindElement(SelectGivenNamePax).SendKeys(GivenName);
        }
        public void EnterPaxSurname(string Surname)
        {
            driver.FindElement(SelectSurnamePax).SendKeys(Surname);
        }
        public void EnterPaxDateOfBirth(string DateOfBirth)
        {
            DateTime desiredDate = DateTime.Parse(DateOfBirth);
            driver.FindElement(SelectDateOfBirthPax).Click();
            Helper.DatePicker(desiredDate, driver);
        }
        public void SelectDocumentType(string DocumentType)
        {
            SelectElement select = new SelectElement(driver.FindElement(SelectDocumentTypePax));
            select.SelectByValue(DocumentType);
        }
        public void SelectPaxCOR(string COR)
        {
            SelectElement select = new SelectElement(driver.FindElement(SelectCORPAX));
            select.SelectByValue(COR);
        }
        public void SelectPaxNational(string Nat)
        {
            SelectElement select = new SelectElement(driver.FindElement(SelectNationalityPax));
            select.SelectByValue(Nat);
        }
        public void EnterPaxIDNo(string IDNo)
        {
            driver.FindElement(SelectIDNoPax).SendKeys(IDNo);
        }
        public void EnterPaxIDExpiry(string IDExpiry)
        {
            DateTime desiredDate = DateTime.Parse(IDExpiry);
            IWebElement Calendar = driver.FindElement(SelectIDExpiryPax);
            Calendar.Click();
            Actions actions = new Actions(driver);
            actions.MoveToElement(Calendar).Perform();
            actions.ScrollToElement(Calendar).Perform();
            Helper.DatePicker(desiredDate, driver);
        }
        public void ClickContinueButton()
        {
            driver.FindElement(BtnContinue).Click();

        }
        public string GetBackToDetailsButtonHref()
        {
            try
            {
                // Wait for the element to be clickable.
                IWebElement backToDetailsButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(BtnBackToDetails));

                // Return the 'href' attribute if the button is displayed, otherwise return null.
                return backToDetailsButton.Displayed ? backToDetailsButton.GetAttribute("href") : String.Empty;
            }
            catch (NoSuchElementException)
            {
                return String.Empty; // Return null if the element is not found.
            }
        }
        public void ClickBackToDetailsButton()
        {
            driver.FindElement(BtnBackToDetails).Click();
        }
        #endregion

        #endregion

    }
}
