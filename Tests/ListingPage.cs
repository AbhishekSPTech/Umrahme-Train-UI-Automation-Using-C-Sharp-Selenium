using Xunit;
using AventStack.ExtentReports;
using Tzy.Train.B2C.UI.Services;
using Tzy.Train.B2C.UI.Pages;
using OpenQA.Selenium;

namespace Tzy.Train.B2C.UI.Test
{
    public class ListingPageTests(Helper setup) : IClassFixture<Helper>
    {
        private readonly Helper setup = setup;
        private IWebDriver driver;
        private ExtentTest extentTest;

        [Fact]
        public void VerifyListing()
        {
            (driver, extentTest) = setup.InitializeTest("Verify Train Listing Page", "chrome", "https://www.traveazy.me/home/en-in");

            var homePage = new HomeUI(driver);
            var trainHomePage = new TrainUI(driver);

            try
            {
                // Validate Home Page
                Assert.Equal(setup.WEB_URL, driver.Url);
                Assert.Equal("Umrah Tours - Umrah Package, Tour, Hotels, Local Transport & Everything - Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Home Page title verified successfully.");

                // Navigate to Train Home Page
                homePage.NavigateToTrainHome();
                extentTest.Log(Status.Info, "Navigating to Train Home Page.");

                Assert.Equal("Book Haramain Train Tickets Faster and Easier | Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Train Home Page title verified successfully.");

                // Perform Train Search Actions
                trainHomePage.SelectDepartureStation("MKX");
                extentTest.Log(Status.Pass, "Departure station (MKX) selected.");

                trainHomePage.SelectArrivalStation("DMX");
                extentTest.Log(Status.Pass, "Arrival station (DMX) selected.");

                trainHomePage.ClickSearchButton();
                extentTest.Log(Status.Pass, "Search button clicked.");

                // Validate Train Listing
                Assert.True(trainHomePage.IsTrainListingDisplayed(), "Train Listing should be displayed.");
                extentTest.Log(Status.Pass, "Train Listing found successfully.");
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
        public void VerifyListingDyanamic()
        {
            (driver, extentTest) = setup.InitializeTest("Verify Train Listing Page", "chrome", "https://www.traveazy.me/home/en-in");

            var homePage = new HomeUI(driver);
            var trainHomePage = new TrainUI(driver);

            try
            {
                // Validate Home Page
                Assert.Equal(setup.WEB_URL, driver.Url);
                Assert.Equal("Umrah Tours - Umrah Package, Tour, Hotels, Local Transport & Everything - Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Home Page title verified successfully.");

                // Navigate to Train Home Page
                homePage.NavigateToTrainHome();
                extentTest.Log(Status.Info, "Navigating to Train Home Page.");

                Assert.Equal("Book Haramain Train Tickets Faster and Easier | Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Train Home Page title verified successfully.");

                // Perform Train Search Actions
                trainHomePage.SelectDepartureStation("MKX");
                extentTest.Log(Status.Pass, "Departure station (MKX) selected.");

                trainHomePage.SelectArrivalStation("DMX");
                extentTest.Log(Status.Pass, "Arrival station (DMX) selected.");

                trainHomePage.SelectDepartureDate();
                extentTest.Log(Status.Pass, "Departure date selected.");

                trainHomePage.SelectTravelers(3, 4, 2);
                extentTest.Log(Status.Pass, "Travelers selected.");

                trainHomePage.ClickSearchButton();
                extentTest.Log(Status.Pass, "Search button clicked.");

                // Validate Train Listing
                Assert.True(trainHomePage.IsTrainListingDisplayed(), "Train Listing should be displayed.");
                extentTest.Log(Status.Pass, "Train Listing found successfully.");
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
        public void VerifyGroupListing()
        {
            (driver, extentTest) = setup.InitializeTest("Verify Train Listing Page", "chrome", "https://www.traveazy.me/home/en-in");

            var homePage = new HomeUI(driver);
            var trainHomePage = new TrainUI(driver);

            try
            {
                // Validate Home Page
                Assert.Equal(setup.WEB_URL, driver.Url);
                Assert.Equal("Umrah Tours - Umrah Package, Tour, Hotels, Local Transport & Everything - Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Home Page title verified successfully.");

                // Navigate to Train Home Page
                homePage.NavigateToTrainHome();
                extentTest.Log(Status.Info, "Navigating to Train Home Page.");

                Assert.Equal("Book Haramain Train Tickets Faster and Easier | Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Train Home Page title verified successfully.");

                // Perform Train Search Actions
                trainHomePage.SelectDepartureStation("MKX");
                extentTest.Log(Status.Pass, "Departure station (MKX) selected.");

                trainHomePage.SelectArrivalStation("DMX");
                extentTest.Log(Status.Pass, "Arrival station (DMX) selected.");

                trainHomePage.SelectDepartureDate();
                extentTest.Log(Status.Pass, "Departure date selected.");

                trainHomePage.SelectTravelers(11, 0, 0);
                extentTest.Log(Status.Pass, "Travelers selected.");

                trainHomePage.ClickSearchButton();
                extentTest.Log(Status.Pass, "Search button clicked.");

                // Validate Train Listing
                Assert.True(trainHomePage.IsTrainListingDisplayed(), "Train Listing should be displayed.");
                extentTest.Log(Status.Pass, "Train Listing found successfully.");
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
