using Articles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Articles.Services
{
    public interface IArticleSearchService
    {
        Task<List<string>> topArticle(int limit, int pageNumber);
    }
}
