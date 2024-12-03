using Xunit;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using Tzy.Train.B2C.UI.Services;
using Tzy.Train.B2C.UI.Pages;

namespace Tzy.Train.B2C.UI.Test
{
    public class GuestDetailsTests(Helper setup) : IClassFixture<Helper>
    {
        private readonly Helper setup = setup;
        private ExtentTest? extentTest { get; set; }
        private IWebDriver driver;

        private void NavigateToTrainHome(HomeUI homePage, TrainUI trainHomePage)
        {
            homePage.NavigateToTrainHome();
            extentTest.Log(Status.Pass, "Navigated to Train Home UI");

            Assert.Equal("Book Haramain Train Tickets Faster and Easier | Umrahme", driver.Title);
            extentTest.Log(Status.Pass, "Verified Train Home Page title.");
        }
        private void PerformTrainSearch(TrainUI trainHomePage, string departure, string arrival, int[] ints)
        {
            trainHomePage.SelectDepartureStation(departure);
            extentTest.Log(Status.Pass, $"Departure station selected: {departure}");

            trainHomePage.SelectArrivalStation(arrival);
            extentTest.Log(Status.Pass, $"Arrival station selected: {arrival}");

            trainHomePage.SelectDepartureDate();
            extentTest.Log(Status.Pass, "Departure date selected.");

            trainHomePage.SelectTravelers(ints[0], ints[1], ints[2]);
            extentTest.Log(Status.Pass, "Travelers selected.");

            trainHomePage.ClickSearchButton();
            extentTest.Log(Status.Pass, "Search button clicked.");
        }
        private void VerifyTrainListing(TrainUI trainHomePage)
        {
            if (trainHomePage.IsTrainListingDisplayed())
            {
                extentTest.Log(Status.Pass, "Train Listing found.");
            }
            else
            {
                extentTest.Log(Status.Fail, "Train Listing not found.");
                Assert.True(false, "Train Listing not displayed.");
            }
        }

        [Fact]
        public void VerifyNavigationToGuestDetails()
        {
            (driver, extentTest) = setup.InitializeTest("Verify Navigation to Guest Details", "chrome", "https://www.traveazy.me/home/en-in");
            var homePage = new HomeUI(driver);
            var trainHomePage = new TrainUI(driver);

            try
            {
                Assert.Equal("Umrah Tours - Umrah Package, Tour, Hotels, Local Transport & Everything - Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Verified Home Page title.");

                NavigateToTrainHome(homePage, trainHomePage);
                PerformTrainSearch(trainHomePage, "MKX", "DMX", new int[] { 1, 0, 0 });
                VerifyTrainListing(trainHomePage);

                trainHomePage.ClickBookNowButton();
                extentTest.Log(Status.Pass, "Clicked Book Now button.");

                Assert.True(trainHomePage.IsBookingDetailsTextDisplayed(), "Guest Details Page not displayed.");
                extentTest.Log(Status.Pass, "Navigated to Guest Details Page.");
            }
            catch (Exception ex)
            {
                extentTest?.Log(Status.Fail, $"Test failed: {ex.Message}");
                throw;
            }
            finally
            {
                setup.FinalizeTest(driver, extentTest);
            }
        }

        [Fact]
        public void VerifyNavigationBackFromGuestDetails()
        {
            (driver, extentTest) = setup.InitializeTest("Verify Navigation Back from Guest Details", "chrome", "https://www.traveazy.me/home/en-in");
            var homePage = new HomeUI(driver);
            var trainHomePage = new TrainUI(driver);

            try
            {
                Assert.Equal("Umrah Tours - Umrah Package, Tour, Hotels, Local Transport & Everything - Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Verified Home Page title.");

                NavigateToTrainHome(homePage, trainHomePage);
                PerformTrainSearch(trainHomePage, "MKX", "DMX", new int[] { 1, 0, 0 });
                VerifyTrainListing(trainHomePage);

                trainHomePage.ClickBookNowButton();
                extentTest.Log(Status.Pass, "Clicked Book Now button.");

                Assert.True(trainHomePage.IsBookingDetailsTextDisplayed(), "Guest Details Page not displayed.");
                extentTest.Log(Status.Pass, "Navigated to Guest Details Page.");

                trainHomePage.ClickBackToListingButton();
                extentTest.Log(Status.Pass, "Clicked Back to Listing button.");

                Assert.True(trainHomePage.IsTrainListingDisplayed(), "Train Listing not displayed after navigating back.");
                extentTest.Log(Status.Pass, "Verified Train Listing after navigating back.");
            }
            catch (Exception ex)
            {
                extentTest?.Log(Status.Fail, $"Test failed: {ex.Message}");
                throw;
            }
            finally
            {
                setup.FinalizeTest(driver, extentTest);
            }
        }

        [Fact]
        public void VerifyGuestDetails()
        {
            (driver, extentTest) = setup.InitializeTest("Verify Guest Details", "chrome", "https://www.traveazy.me/home/en-in");
            var homePage = new HomeUI(driver);
            var trainHomePage = new TrainUI(driver);

            try
            {
                Assert.Equal("Umrah Tours - Umrah Package, Tour, Hotels, Local Transport & Everything - Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Verified Home Page title.");

                NavigateToTrainHome(homePage, trainHomePage);
                PerformTrainSearch(trainHomePage, "MKX", "DMX", new int[] { 1, 0, 0 });
                VerifyTrainListing(trainHomePage);

                trainHomePage.ClickBookNowButton();
                extentTest.Log(Status.Pass, "Clicked Book Now button.");

                Assert.True(trainHomePage.IsBookingDetailsTextDisplayed(), "Guest Details Page not displayed.");
                extentTest.Log(Status.Pass, "Navigated to Guest Details Page.");

                string guestDetailsUrl = driver.Url;

                // Fill Guest Details Form
                trainHomePage.SelectBuyerTitle("Mr");
                trainHomePage.EnterBuyerGivenName("Automation");
                trainHomePage.EnterBuyerSurname("Traveazy UI");
                trainHomePage.EnterPhoneNo("9834724397");
                trainHomePage.EnterEmail("automation@traveazy.com");
                extentTest.Log(Status.Pass, "Guest Details form filled successfully.");

                // Fill Pax Details Form
                trainHomePage.SelectPaxTitle("Mr");
                trainHomePage.EnterPaxGivenName("AutomationPax");
                trainHomePage.EnterPaxSurname("TraveazyPax");
                trainHomePage.EnterPaxDateOfBirth("26/01/1999");
                trainHomePage.SelectDocumentType("NATIONALID");
                trainHomePage.SelectPaxCOR("IN");
                // trainHomePage.SelectPaxNational("IN");
                trainHomePage.EnterPaxIDNo("1234567890");
                trainHomePage.EnterPaxIDExpiry("12/12/2040");
                extentTest.Log(Status.Pass, "Pax Details form filled successfully.");

                trainHomePage.ClickContinueButton();
                extentTest.Log(Status.Pass, "Clicked Continue button.");

                Assert.Equal(guestDetailsUrl, trainHomePage.GetBackToDetailsButtonHref());
                extentTest.Log(Status.Pass, "Verified Guest Details Page URL.");
            }
            catch (Exception ex)
            {
                extentTest?.Log(Status.Fail, $"Test failed: {ex.Message}");
                throw;
            }
            finally
            {
                setup.FinalizeTest(driver, extentTest);
            }
        }

        [Fact]
        public void VerifyGuestDetailsGroupBooking()
        {
            (driver, extentTest) = setup.InitializeTest("Verify Guest Details", "chrome", "https://www.traveazy.me/home/en-in");
            var homePage = new HomeUI(driver);
            var trainHomePage = new TrainUI(driver);

            try
            {
                Assert.Equal("Umrah Tours - Umrah Package, Tour, Hotels, Local Transport & Everything - Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Verified Home Page title.");

                NavigateToTrainHome(homePage, trainHomePage);
                PerformTrainSearch(trainHomePage, "MKX", "DMX", new int[] { 11, 0, 0 });
                VerifyTrainListing(trainHomePage);

                trainHomePage.ClickBookNowButton();
                extentTest.Log(Status.Pass, "Clicked Book Now button.");

                Assert.True(trainHomePage.IsBookingDetailsTextDisplayed(), "Guest Details Page not displayed.");
                extentTest.Log(Status.Pass, "Navigated to Guest Details Page.");

                string guestDetailsUrl = driver.Url;

                // Fill Guest Details Form
                trainHomePage.SelectBuyerTitle("Mr");
                trainHomePage.EnterBuyerGivenName("Automation");
                trainHomePage.EnterBuyerSurname("Traveazy UI");
                trainHomePage.EnterPhoneNo("9834724397");
                trainHomePage.EnterEmail("automation@traveazy.com");
                extentTest.Log(Status.Pass, "Guest Details form filled successfully.");

                trainHomePage.ClickContinueButton();
                extentTest.Log(Status.Pass, "Clicked Continue button.");

                Assert.Equal(guestDetailsUrl, trainHomePage.GetBackToDetailsButtonHref());
                extentTest.Log(Status.Pass, "Verified Guest Details Page URL.");
            }
            catch (Exception ex)
            {
                extentTest?.Log(Status.Fail, $"Test failed: {ex.Message}");
                throw;
            }
            finally
            {
                setup.FinalizeTest(driver, extentTest);
            }
        }
    }
}
