using API.Helpers;
using Newtonsoft.Json;
using ReadLater.Entities;
using ReadLater.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    
    
    public class BookmarkController : ApiController
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [Authorize]
        [Route("bookmarks/")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetBookmarks()
        {
            var result = await Task.Factory.StartNew(action =>
            {
                try
                {
                    return _bookmarkService.GetBookmarks(null);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }, new System.Threading.CancellationToken());

            return new HttpResponseMessage() { Content = new JsonContent(result), StatusCode = HttpStatusCode.OK };
        }
    }
}
