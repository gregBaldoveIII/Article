using Articles.Client.Services;
using Articles.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Articles.Services
{
    public class ArticleSearchService : IArticleSearchService
    {
        private readonly IArticleService _articleService;
        public ArticleSearchService(IArticleService articleService)
        {
            _articleService = articleService ?? throw new ArgumentNullException(nameof(articleService));
        }

        public async Task<List<string>> topArticle(int limit, int pageNumber)
        {            
            var result = await _articleService.getArticlePerPage(pageNumber);

            var articleData = result.data.AsEnumerable().OrderByDescending(x => x.num_comments);

            List<TopArticle> dataToReturn = new List<TopArticle>();

            foreach (var article in articleData)
            {
                if (dataToReturn.Count < limit)
                {
                    if (article.title != null)
                        dataToReturn.Add(new TopArticle { article_name = article.title, num_of_comments = article.num_comments });
                    else if (article.story_title != null)
                        dataToReturn.Add(new TopArticle { article_name = article.story_title, num_of_comments = article.num_comments });
                    else
                        continue;
                }
                else
                    break;
            }

            return dataToReturn.OrderByDescending(x => x.num_of_comments).ThenBy(x => x.article_name).Take(limit).Select(x => x.article_name).ToList();
        }
    }
}
