using Articles.Client.Models;
using Microsoft.Rest;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Articles.Client.Services
{
  
    public class ArticleClient : ServiceClient<ArticleClient>, IArticleClient
    {
        public Uri BaseUri { get; set; }

        private void Initialize()
        {
            BaseUri = new Uri("https://jsonmock.hackerrank.com");
        }
        
        public async Task<HttpOperationResponse<Page>> GetArticlePerPageWithHttpMessagesAsync(int pageId)
        {
            Initialize();

            var baseUrl = BaseUri.AbsoluteUri;
            var requestUrl = new Uri(new Uri(baseUrl), "api/articles?page={pageId}").ToString();
            requestUrl = requestUrl.Replace("{pageId}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(pageId).Trim('"')));

            var httpRequest = new HttpRequestMessage();
            HttpResponseMessage httpResponse = null;

            httpRequest.Method = new HttpMethod("GET");
            httpRequest.RequestUri = new Uri(requestUrl);
            
            httpResponse = await HttpClient.SendAsync(httpRequest).ConfigureAwait(false);
            HttpStatusCode statusCode = httpResponse.StatusCode;

            string responseContent = null;

            if ((int)statusCode == 200)
            {
                var result = new HttpOperationResponse<Page>();
                result.Request = httpRequest;
                result.Response = httpResponse;
               
                responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    result.Body = SafeJsonConvert.DeserializeObject<Page>(responseContent);
                    return result;
                }
                catch (Newtonsoft.Json.JsonException ex)
                {
                    throw new SerializationException("Unable to deserialize the response.", responseContent, ex);
                }
            }

            return null;
        }
    }
}
