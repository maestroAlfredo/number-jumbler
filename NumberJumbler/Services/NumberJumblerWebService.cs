using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NumberJumbler.Models;
using System.Threading.Tasks;
using System.Net.Http;

namespace NumberJumbler.Services
{
    public class NumberJumblerWebService : INumberJumblerWebService
    {
        public async Task<NumberJumblerResult> FindLeader(HttpRequestBase request, bool errorCheck)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:58505/api/Maths");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //post content
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StreamContent(request.Files.Get("uploadInput").InputStream));           

            var response = await client.PostAsync("?Validate=" + errorCheck.ToString(), content);
            if (response.IsSuccessStatusCode)
            {
                var dataObject = await response.Content.ReadAsAsync(typeof(NumberJumblerResult));
                return (NumberJumblerResult)dataObject;
            }
            else
            {
                return (new NumberJumblerResult() { HasErrors = true, ErrorMessage = "Cannot access API", Result = -1 });
            }
        }
    }
}