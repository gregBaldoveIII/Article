using Articles.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Articles.Client.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleClient _client;
        public ArticleService(IArticleClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<Page> getArticlePerPage(int pageId)
        {
            try
            {
                var response = await _client.GetArticlePerPageWithHttpMessagesAsync(pageId);

                if (response.Response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Body;
                }

                return null;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Failed to retrieve articles", e);
            }
        }
    }
}
