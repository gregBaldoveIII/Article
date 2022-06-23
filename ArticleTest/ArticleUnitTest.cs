using AutoFixture;
using Articles.Client.Models;
using Xunit;
using Articles.Services;
using Moq;
using Articles.Client.Services;
using FluentAssertions;

namespace ArticleUnitTest
{
    public class ArticleTest
    {
        private readonly Mock<IArticleService> _mockArticleService;
        private readonly ArticleSearchService _articleSearchService;

        public ArticleTest()
        {
            _mockArticleService = new Mock<IArticleService>();
            _articleSearchService = new ArticleSearchService(_mockArticleService.Object);
        }

        [Theory]
        [InlineData(2, 2, 2)]
        [InlineData(3, 1, 3)]
        [InlineData(1, 3, 1)]
        public void WhenArticleLimitIsSet_ThenReturnCorrectCount(int limit, int page, int expected)
        {
            var fixture = new Fixture();

            var fixtureModels = fixture.Build<Page>()
                .CreateMany(12);

            _mockArticleService.Setup(x => x.getArticlePerPage(page)).ReturnsAsync(expected);

            var result = _articleSearchService.topArticle(limit, page);

            result.Should().NotBeNull();
            result.Result.Should().HaveCount(expected);
        }
    }
}