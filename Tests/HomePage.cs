using Xunit;
using AventStack.ExtentReports;
using Tzy.Train.B2C.UI.Services;
using Tzy.Train.B2C.UI.Pages;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Tzy.Train.B2C.UI.Test
{
    public class HomePageTests(Helper setup) : IClassFixture<Helper>
    {
        private readonly Helper setup = setup;
        private IWebDriver driver;
        private ExtentTest extentTest;

        [Fact]
        public void VerifyLandingPage()
        {
            (driver, extentTest) = setup.InitializeTest("Verify Train Landing Page", "chrome", "https://www.traveazy.me/train/en-in");
            try
            {
                Assert.Equal(setup.WEB_URL, setup.GetCurrentUrl(driver));
                extentTest.Log(Status.Pass, "Page URL verified successfully.");

                Assert.Equal("Book Haramain Train Tickets Faster and Easier | Umrahme", setup.GetPageTitle(driver));
                extentTest.Log(Status.Pass, "Page title verified successfully.");
            }
            catch (Exception ex)
            {
                extentTest.Log(Status.Fail, $"Test failed: {ex.Message}");
                throw;
            }
            finally
            {
                setup.FinalizeTest(driver, extentTest);
            }
        }

        [Fact]
        public void VerifyNavigationToTrainUI()
        {
            (driver, extentTest) = setup.InitializeTest("Verify Navigation to Train UI from Home", "chrome", "https://www.traveazy.me/home/en-in");

            var homePage = new HomeUI(driver);

            try
            {
                Assert.Equal(setup.WEB_URL, setup.GetCurrentUrl(driver));
                extentTest.Log(Status.Pass, "Home Page URL verified successfully.");

                Assert.Equal("Umrah Tours - Umrah Package, Tour, Hotels, Local Transport & Everything - Umrahme", setup.GetPageTitle(driver));
                extentTest.Log(Status.Pass, "Home Page title verified successfully.");

                homePage.NavigateToTrainHome();
                extentTest.Log(Status.Info, "Navigated to Train Landing Page.");
                extentTest.Log(Status.Pass, "Successfully Navigated to Train Landing Page.");

                Assert.Equal("Book Haramain Train Tickets Faster and Easier | Umrahme", setup.GetPageTitle(driver));
                extentTest.Log(Status.Pass, "Train Landing Page title verified successfully.");
            }
            catch (NoSuchElementException ex)
            {
                extentTest.Log(Status.Fail, $"Element not found: {ex.Message}");
                throw;
            }
            catch (ElementNotInteractableException ex)
            {
                extentTest.Log(Status.Fail, $"Element not interactable: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                extentTest.Log(Status.Fail, $"Unexpected error: {ex.Message}");
                throw;
            }
            finally
            {
                setup.FinalizeTest(driver, extentTest);
            }
        }
    }
}
