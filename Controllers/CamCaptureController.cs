using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class CamCaptureController : Controller
    {
        // GET: CamCapture
        public ActionResult Index()
        {
            return View();
        }
        static async Task<string> MakeAnalysisRequest(byte[] imageBytes)
        {

            const string subscriptionKey = "78e1d7e704104ace958b47f6af9b6d5c";
            const string uriBase = "https://fvvsdsousfsdgasgvxzxcvsdfgasdg.cognitiveservices.azure.com/";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Face", subscriptionKey);
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age, gender, headPose, smile, facialHair, glasses, emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.  
            byte[] byteData = imageBytes;

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".  
                // The other content types you can use are "application/json" and "multipart/form-data".  
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.  
                response = await client.PostAsync(uri, content);

                // Get the JSON response.  
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the JSON response.  

                return JsonPrettyPrint(contentString);

            }
        }
        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }

            return sb.ToString().Trim();
        }
        [HttpPost]
        public async Task<dynamic> Capture(string base64String)
        {

            if (!string.IsNullOrEmpty(base64String))
            {
                var imageParts = base64String.Split(',').ToList<string>();
                byte[] imageBytes = Convert.FromBase64String(imageParts[1]);
                DateTime nm = DateTime.Now;
                string date = nm.ToString("yyyymmddMMss");
                var path = Server.MapPath("~/CapturedPhotos/" + date + "CamCapture.jpg");

                var response = await MakeAnalysisRequest(imageBytes);

                System.IO.File.WriteAllBytes(path, imageBytes);
                return Json(data: response);
            }
            else
            {
                return Json(data: false);
            }
        }
    }
}