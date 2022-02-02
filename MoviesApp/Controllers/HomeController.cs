using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Page page = TMDBHelper.GetPopularMoviesPage(1);
            return View(page.results);
        }
    }
}
