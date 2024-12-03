using Xunit;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using Tzy.Train.B2C.UI.Services;
using Tzy.Train.B2C.UI.Pages;

namespace Tzy.Train.B2C.UI.Test
{
    public class PaymentPageTests(Helper setup) : IClassFixture<Helper>
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
                trainHomePage.ClickBookNowButton();
                extentTest.Log(Status.Pass, "Clicked Book Now button.");

                Assert.True(trainHomePage.IsBookingDetailsTextDisplayed(), "Guest Details Page not displayed.");
                extentTest.Log(Status.Pass, "Navigated to Guest Details Page.");
            }
            else
            {
                extentTest.Log(Status.Fail, "Train Listing not found.");
                Assert.True(false, "Train Listing not displayed.");
            }
        }
        private void FillGuestDetails(TrainUI trainHomePage)
        {
            if (trainHomePage.IsBookingDetailsTextDisplayed())
            {
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

                // trainHomePage.ClickContinueButton();
                // extentTest.Log(Status.Pass, "Clicked Continue button.");

                // Assert.Equal(guestDetailsUrl, trainHomePage.GetBackToDetailsButtonHref());
                // extentTest.Log(Status.Pass, "Verified Guest Details Page URL.");
            }
            else
            {
                extentTest.Log(Status.Fail, "Guest Details Page not displayed.");
                Assert.True(false, "Guest Details Page not displayed.");
            }
        }
        private void FillGroupGuestDetails(TrainUI trainHomePage)
        {
            if (trainHomePage.IsBookingDetailsTextDisplayed())
            {
                // Fill Guest Details Form
                trainHomePage.SelectBuyerTitle("Mr");
                trainHomePage.EnterBuyerGivenName("Automation");
                trainHomePage.EnterBuyerSurname("Traveazy UI");
                trainHomePage.EnterPhoneNo("9834724397");
                trainHomePage.EnterEmail("automation@traveazy.com");
                extentTest.Log(Status.Pass, "Guest Details form filled successfully.");

            }
            else
            {
                extentTest.Log(Status.Fail, "Guest Details Page not displayed.");
                Assert.True(false, "Guest Details Page not displayed.");
            }
        }


        [Fact]
        public void VerifyGroupPayment()
        {
            (driver, extentTest) = setup.InitializeTest("Verify Group Payment.", "chrome", "https://www.traveazy.me/home/en-in");
            var homePage = new HomeUI(driver);
            var trainHomePage = new TrainUI(driver);
            var paymentPage = new PaymentUI(driver);

            try
            {
                Assert.Equal("Umrah Tours - Umrah Package, Tour, Hotels, Local Transport & Everything - Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Verified Home Page title.");

                NavigateToTrainHome(homePage, trainHomePage);
                PerformTrainSearch(trainHomePage, "MKX", "DMX", new int[] { 11, 0, 0 });
                VerifyTrainListing(trainHomePage);
                FillGroupGuestDetails(trainHomePage);
                string guestDetailsUrl = driver.Url;

                trainHomePage.ClickContinueButton();
                extentTest.Log(Status.Pass, "Clicked Continue button.");

                Assert.Equal(guestDetailsUrl, trainHomePage.GetBackToDetailsButtonHref());
                extentTest.Log(Status.Pass, "Navigated to Payment Page.");

                paymentPage.ClickCardButton();
                extentTest.Log(Status.Pass, "Clicked Card button.");

                paymentPage.EnterCardNumber("4557012345678902");
                extentTest.Log(Status.Pass, "Entered Card Number.");

                paymentPage.EnterExpiryDate("12/30");
                extentTest.Log(Status.Pass, "Entered Expiry Date.");

                paymentPage.EnterCvv("123");
                extentTest.Log(Status.Pass, "Entered CVV.");

                paymentPage.EnterName("Automation Traveazy");
                extentTest.Log(Status.Pass, "Entered Name.");

                paymentPage.ClickSubmitButton();
                extentTest.Log(Status.Pass, "Clicked Submit button.");

                Assert.True(paymentPage.IsOtpDisplayed(), "OTP not displayed.");
                extentTest.Log(Status.Pass, "OTP displayed.");

                paymentPage.EnterOtp("12345");
                extentTest.Log(Status.Pass, "Entered OTP.");

                paymentPage.ClickOtpSubmitButton();
                extentTest.Log(Status.Pass, "Clicked OTP Submit button.");

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
        public void VerifyCardPayment()
        {
            (driver, extentTest) = setup.InitializeTest("Verify Card Payment", "chrome", "https://www.traveazy.me/home/en-in");
            var homePage = new HomeUI(driver);
            var trainHomePage = new TrainUI(driver);
            var paymentPage = new PaymentUI(driver);

            try
            {
                Assert.Equal("Umrah Tours - Umrah Package, Tour, Hotels, Local Transport & Everything - Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Verified Home Page title.");

                NavigateToTrainHome(homePage, trainHomePage);
                PerformTrainSearch(trainHomePage, "MKX", "DMX", new int[] { 1, 0, 0 });
                VerifyTrainListing(trainHomePage);
                FillGuestDetails(trainHomePage);
                string guestDetailsUrl = driver.Url;

                trainHomePage.ClickContinueButton();
                extentTest.Log(Status.Pass, "Clicked Continue button.");

                Assert.Equal(guestDetailsUrl, trainHomePage.GetBackToDetailsButtonHref());
                extentTest.Log(Status.Pass, "Navigated to Payment Page.");

                paymentPage.ClickCardButton();
                extentTest.Log(Status.Pass, "Clicked Card button.");

                paymentPage.EnterCardNumber("4557012345678902");
                extentTest.Log(Status.Pass, "Entered Card Number.");

                paymentPage.EnterExpiryDate("12/30");
                extentTest.Log(Status.Pass, "Entered Expiry Date.");

                paymentPage.EnterCvv("123");
                extentTest.Log(Status.Pass, "Entered CVV.");

                paymentPage.EnterName("Automation Traveazy");
                extentTest.Log(Status.Pass, "Entered Name.");

                paymentPage.ClickSubmitButton();
                extentTest.Log(Status.Pass, "Clicked Submit button.");

                Assert.True(paymentPage.IsOtpDisplayed(), "OTP not displayed.");
                extentTest.Log(Status.Pass, "OTP displayed.");

                paymentPage.EnterOtp("12345");
                extentTest.Log(Status.Pass, "Entered OTP.");

                paymentPage.ClickOtpSubmitButton();
                extentTest.Log(Status.Pass, "Clicked OTP Submit button.");

                // Assert.True(paymentPage.IsPaymentSuccess(), "Payment not successful.");
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
        public void VerifyMadaPayment()
        {
            (driver, extentTest) = setup.InitializeTest("Verify MADA Payment", "chrome", "https://www.traveazy.me/home/en-in");
            var homePage = new HomeUI(driver);
            var trainHomePage = new TrainUI(driver);
            var paymentPage = new PaymentUI(driver);

            try
            {
                Assert.Equal("Umrah Tours - Umrah Package, Tour, Hotels, Local Transport & Everything - Umrahme", driver.Title);
                extentTest.Log(Status.Pass, "Verified Home Page title.");

                NavigateToTrainHome(homePage, trainHomePage);
                PerformTrainSearch(trainHomePage, "MKX", "DMX", new int[] { 1, 0, 0 });
                VerifyTrainListing(trainHomePage);
                FillGuestDetails(trainHomePage);
                string guestDetailsUrl = driver.Url;

                trainHomePage.ClickContinueButton();
                extentTest.Log(Status.Pass, "Clicked Continue button.");

                Assert.Equal(guestDetailsUrl, trainHomePage.GetBackToDetailsButtonHref());
                extentTest.Log(Status.Pass, "Navigated to Payment Page.");

                paymentPage.ClickMadaButton();
                extentTest.Log(Status.Pass, "Clicked Card button.");

                paymentPage.EnterCardNumber("5297412542005689");
                extentTest.Log(Status.Pass, "Entered Card Number.");

                paymentPage.EnterExpiryDate("12/30");
                extentTest.Log(Status.Pass, "Entered Expiry Date.");

                paymentPage.EnterCvv("123");
                extentTest.Log(Status.Pass, "Entered CVV.");

                paymentPage.EnterName("Automation Traveazy");
                extentTest.Log(Status.Pass, "Entered Name.");

                paymentPage.ClickSubmitButton();
                extentTest.Log(Status.Pass, "Clicked Submit button.");

                Assert.True(paymentPage.IsOtpDisplayed(), "OTP not displayed.");
                extentTest.Log(Status.Pass, "OTP displayed.");

                paymentPage.EnterOtp("12345");
                extentTest.Log(Status.Pass, "Entered OTP.");

                paymentPage.ClickOtpSubmitButton();
                extentTest.Log(Status.Pass, "Clicked OTP Submit button.");

                // Assert.True(paymentPage.IsPaymentSuccess(), "Payment not successful.");
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