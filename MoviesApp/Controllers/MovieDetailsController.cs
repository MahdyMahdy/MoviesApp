using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Controllers
{
    public class MovieDetailsController : Controller
    {
        DataBase db;
        private readonly IMemoryCache _memoryCache;
        bool inDB = false;

        public MovieDetailsController(DataBase dataBase,IMemoryCache memoryCache)
        {
            db = dataBase;
            _memoryCache = memoryCache;
        }

        public Movie GetMovie(int id)
        {
            Movie movie = db.movies.Find(id);
            if (movie != null)
            {
                movie.genres = (from Genre genre in db.genres where genre.Movieid == id select genre).ToList();
                movie.spoken_languages = (from SpokenLanguage sl in db.spokenLanguages where sl.Movieid == id select sl).ToList();
                movie.production_companies = (from ProductionCompany pc in db.productionCompanies where pc.Movieid == id select pc).ToList();
                movie.production_countries = (from ProductionCountry pc in db.productionCountries where pc.Movieid == id select pc).ToList();
                try
                {
                    movie.belongs_to_collection = (from BelongsToCollection bc in db.belongsToCollections where bc.Movieid == id select bc).First();
                }
                catch
                {

                }
                inDB = true;
                return movie;
            }
            movie = TMDBHelper.GetMovie(id,_memoryCache);
            return movie;
        }

        [Route("MovieDetails/{id}")]
        public ActionResult Index(int id)
        {
            Movie movie = GetMovie(id);
            ViewBag.inDB = inDB;
            return View(movie);
        }

        [Route("MovieDetails/SaveMovie")]
        [HttpPost]
        public ActionResult SaveMovie(int id)
        {
            Movie movie = TMDBHelper.GetMovie(id, _memoryCache);
            movie.backdrop_bytes = TMDBHelper.GetImageBytes(TMDBHelper.GetImageFullPath(movie.backdrop_path), _memoryCache);
            movie.poster_bytes = TMDBHelper.GetImageBytes(TMDBHelper.GetImageFullPath(movie.poster_path), _memoryCache);
            db.movies.Add(movie);
            db.SaveChanges();
            return Redirect("~/MovieDetails/" + id);
        }
    }
}
