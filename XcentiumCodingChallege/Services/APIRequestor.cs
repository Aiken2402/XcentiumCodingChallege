using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using XcentiumCodingChallege.Models;

namespace XcentiumCodingChallege.Services
{
    public static class APIRequestor
    {
        public static async Task<PageContent> LoadUrl(string url)
        {
            try
            {
                PageContent responseObj = new PageContent();
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    Uri uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                    string requested = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
                    client.BaseAddress = new Uri(requested);

                    // Setting content type.  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP GET  
                    var requestUri = new Uri($"/api/webapi/?url={HttpUtility.UrlEncode(url)}", UriKind.Relative);
                    response = await client.GetAsync(requestUri).ConfigureAwait(false);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonConvert.DeserializeObject<PageContent>(result);                        
                    }
                    else
                    {
                       throw new HttpResponseException(response);
                    }

                    return responseObj;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}