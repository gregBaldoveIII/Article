using Articles.Client.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articles.Client.Services
{
    public interface IArticleClient : IDisposable
    {
        Task<HttpOperationResponse<Page>> GetArticlePerPageWithHttpMessagesAsync(int pageId);
    }
}
