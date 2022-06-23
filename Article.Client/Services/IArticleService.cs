using Articles.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articles.Client.Services
{
    public interface IArticleService
    {
        Task<Page> getArticlePerPage(int pageId);
    }
}
    