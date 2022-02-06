using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public ActionResult Index()
        {
            Page page = TMDBHelper.GetPopularMoviesPage(1,_memoryCache);
            return View(page);
        }
        [Route("Home/{page}")]
        public ActionResult Index(int page)
        {
            if (page < 1)
                page = 1;
            Page res = TMDBHelper.GetPopularMoviesPage(page,_memoryCache);
            return View(res);
        }
    }
}
