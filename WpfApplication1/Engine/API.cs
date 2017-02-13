using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace WpfApplication1.Engine
{
    static class Api
    {
        public static async Task<HttpResponseMessage> RequestVoiceEnrollment(byte[] data)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["SpeechAPI"]);
            var profileId = Guid.Parse("");
            var uri = $"https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles/{profileId}/enroll";

            HttpResponseMessage response;

            using (var content = new ByteArrayContent(data))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
            }
            return response;
        }
        public static async Task<HttpResponseMessage> RequestImageAnalisys(byte[] data)
        {
            var client = new HttpClient();

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["VisionAPI"]);

            // Request parameters
            queryString["visualFeatures"] = "Categories, Tags, Description, Faces, ImageType, Color, Adult";
            queryString["details"] = "Celebrities";
            queryString["language"] = "en";
            var uri = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?" + queryString;

            HttpResponseMessage response;

            using (var content = new ByteArrayContent(data))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
            }
            return response;
        }

        public static async Task<HttpResponseMessage> CreateSpeakerProfile()
        {
            var client = new HttpClient();

            var uri = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles";

            HttpResponseMessage response;

            using (var content = new StringContent(@"{ ""locale"": ""en-us""}"))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                response = await client.PostAsync(uri, content);
            }
            return response;
        }

        public static async Task<HttpResponseMessage> RequestVoiceVerification(byte[] data, Guid speakerIdentificationId)
        {
            var client = new HttpClient();

            var uri = $"https://westus.api.cognitive.microsoft.com/spid/v1.0/verify?verificationProfileId={speakerIdentificationId}";

            HttpResponseMessage response;

            using (var content = new ByteArrayContent(data))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
            }
            return response;
        }
    }
}
