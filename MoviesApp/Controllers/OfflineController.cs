using Microsoft.AspNetCore.Mvc;
using MoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Controllers
{
    public class OfflineController : Controller
    {
        DataBase db;

        public OfflineController(DataBase dataBase)
        {
            db = dataBase;
        }
        [Route("Offline")]
        public ActionResult Index()
        {
            ViewBag.page = 1;
            var selection = from movie in db.movies select movie;
            if(selection.Count()==0)
            {
                return Redirect("Home");
            }
            List<Movie> movies;
            try
            {
                movies = selection.ToList().GetRange(0, 10);
            }
            catch(Exception)
            {
                movies = selection.ToList();
            }
            return View(movies);
        }
        [Route("Offline/{page}")]
        public ActionResult Index(int page)
        {
            if (page < 1)
                page = 1;
            ViewBag.page = page;
            var selection = from movie in db.movies select movie;
            if (selection.Count() == 0)
            {
                return Redirect("Home");
            }
            List<Movie> movies;
            try
            {
                movies = selection.ToList().GetRange(10*(page-1), 10);
            }
            catch (Exception)
            {
                try
                {
                    movies = selection.ToList().GetRange(10 * (page - 1), selection.Count() - 10 * (page - 1));
                }
                catch(Exception)
                {
                    return Redirect("Offline");
                }
            }
            return View(movies);
        }
    }
}
