using System.Net;
using System.Net.Mail;
using System.Security;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;

namespace Tzy.Train.B2C.UI.Services
{
    public class UIServices
    {

        public static string screenshotpath { get; set; }
        public ExtentReports EXTENT { get; set; }
        public static ExtentTest Test { get; set; }

        /// <summary>
        /// This Method will create a new Extent report.
        /// </summary>
        public void ExtentStart()
        {
            // Define the path for the report output
            string reportPath = @"../../../Reports/" + DateTime.Now.ToString("MMddyyyy_hhmmtt") + ".html";

            // Create an instance of ExtentReports
            EXTENT = new ExtentReports();

            // Create a new HTML reporter
            var htmlReporter = new ExtentHtmlReporter(reportPath);

            // Attach the HTML reporter to the ExtentReports instance
            EXTENT.AttachReporter(htmlReporter);

            // Optional: Set the report details like document title, encoding, etc.
            htmlReporter.Config.DocumentTitle = "Automation Test Report";
            htmlReporter.Config.ReportName = "Functional Test Results";
            htmlReporter.Config.Encoding = "UTF-8";
        }

        /// <summary>
		/// This Method will take the screenshot and save it in Screenshots folder
		/// <param name="driver"></param>
		/// <param name="screenshotname"></param>
		/// </summary>
		public static void TakeScreenShot(IWebDriver driver, string screenshotname)
        {
            string date_time = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            screenshotname = screenshotname + "_" + date_time;
            string screenshotDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

            if (!Directory.Exists(screenshotDirectory))
            {
                Directory.CreateDirectory(screenshotDirectory);
            }
            string screenshotpath = Path.Combine(screenshotDirectory, screenshotname + ".png");
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(screenshotpath);
        }

        /// <summary>
		/// This Method will be used to send an email
		/// subject = which will contain Mail subjec"
		/// mailbody = which will contain the body of the mail (it accepts Html)
		/// fromemail = which will contain senders email
		/// toemail = which will contain receivers email
		/// password = which will contain password of the senders email
		/// SecureString SecuredPassword = new NetworkCredential("", "password").SecurePassword;
		/// <param name="subject"></param>
		/// <param name="mailbody"></param>
		/// <param name="fromemail"></param>
		/// <param name="password"></param>
		/// <param name="toemail"></param>
		/// </summary>
		public void SendMail(string subject, string mailbody, string fromemail, SecureString password, string toemail)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromemail);
            mail.To.Add(toemail);
            mail.Subject = subject;
            mail.Body = mailbody;
            mail.IsBodyHtml = true;
            // mail.CC.Add("ProductionSupport@traveazy.com");
            mail.Attachments.Add(new Attachment(@"../../../Reports/index.html"));
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential(fromemail, password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Send(mail);
        }


        /// <summary>
        /// This method will be for uploading file from windows explorer
        /// This methods need two parameters
        /// Relative_exe_Path = which will contain executable file relative path e.g "../../../Helper/upload.exe"
        /// relative_file_path = which will contain executable file relative path e.g "../../../DataFiles/TestData.xlsx"
        /// <param name="Relative_exe_Path"></param>
        /// <param name="relative_file_path"></param>
        /// </summary>
        public static void FileUpload(string Relative_exe_Path, string relative_file_path)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @relative_file_path);
            string sFilePath = Path.GetFullPath(sFile);
            System.Diagnostics.Process.Start(@Relative_exe_Path, sFilePath);
        }
    }
}
