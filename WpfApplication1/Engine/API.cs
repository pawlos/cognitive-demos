using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WpfApplication1.Engine
{
    static class Api
    {
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
                //{"url":"http://example.com/images/test.jpg"}
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
            }
            return response;
        }
    }
}
