//This is under development
using System.Net.Http.Headers;

namespace Tzy.Train.B2C.UI
{
    class VSTS_Helper
    {
        public static async Task GetTestPlanAsync()
        {
            int testPlanId = 135072;

            // Set up the HTTP client
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://dev.azure.com/traveazysource");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Set the personal access token (PAT) to use for authentication
            string pat = "ablaa7f2phiuili6brp3fdtrc2v7bgudiw46culkckdyf3btyfgq";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", pat))));

            // Send the request to get the test plan
            HttpResponseMessage response = await client.GetAsync("/UmeAutomation/_apis/test/plans/" + testPlanId + "?api-version=5.0");
            if (response.IsSuccessStatusCode)
            {
                // Get the test plan from the response
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine("Failed to get test plan: " + response.StatusCode);
            }
        }
    }
}
