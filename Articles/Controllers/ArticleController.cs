using Articles.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Articles.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticleSearchService _articleSearchService;

        public ArticleController(IArticleSearchService articleSearchService)
        {
            _articleSearchService = articleSearchService ?? throw new ArgumentNullException(nameof(articleSearchService));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTopArticleFromPageNumber([FromQuery] int limit, int pageNumber)
        {
            var result = await _articleSearchService.topArticle(limit, pageNumber);
            return Ok(result);
        }
    }
}
