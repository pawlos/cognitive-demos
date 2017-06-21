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
        public static async Task<HttpResponseMessage> RequestVoiceEnrollment(byte[] data, Guid profileId)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["SpeechAPI"]);
            var uri = $"https://westus.api.cognitive.microsoft.com/spid/v1.0/verificationProfiles/{profileId}/enroll";

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

        public static async Task<HttpResponseMessage> RequestFaceAnalisys(byte[] data)
        {
            var client = new HttpClient();

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["returnFaceAttributes"] = "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";
            queryString["returnFaceLandmarks"] = "true";
            var uri = "https://westeurope.api.cognitive.microsoft.com/face/v1.0/detect?" + queryString;
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["FaceAPI"]);

            HttpResponseMessage response;
            using (var content = new ByteArrayContent(data))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
            }
            return response;
        }

        public static async Task<HttpResponseMessage> RequestEmotionsAnalisys(byte[] data)
        {
            var client = new HttpClient();
            var uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize";
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["EmotionsAPI"]);

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

            var uri = "https://westus.api.cognitive.microsoft.com/spid/v1.0/verificationProfiles";
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["SpeechAPI"]);
            HttpResponseMessage response;

            using (var content = new StringContent(@"{'locale': 'en-us'}"))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
            return response;
        }

        public static async Task<HttpResponseMessage> RequestVoiceVerification(byte[] data, Guid speakerIdentificationId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["SpeechAPI"]);

            var uri = $"https://westus.api.cognitive.microsoft.com/spid/v1.0/verify?verificationProfileId={speakerIdentificationId}";

            HttpResponseMessage response;

            using (var content = new ByteArrayContent(data))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
            }
            return response;
        }

        public static async Task<HttpResponseMessage> GetAutosuggestResult(string searchPhrase)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["AutosuggestApi"]);
            client.DefaultRequestHeaders.Add("BingAPIs-Market", "en-US");
            var uri = "https://api.cognitive.microsoft.com/bing/v5.0/suggestions/?q=" + searchPhrase;

            return await client.GetAsync(uri);
        }

        public static async Task<HttpResponseMessage> LoadProfile(Guid verificationProfileId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["SpeechAPI"]);

            var uri =
                $"https://westus.api.cognitive.microsoft.com/spid/v1.0/verificationProfiles/{verificationProfileId}";

            return await client.GetAsync(uri);
        }
    }
}
