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

        [Route("MovieDetails/{id}")]
        public ActionResult Index(int id)
        {
            Movie movie = GetMovie(id);
            Images images = GetMovieImages(id);
            Credits credits = GetMovieCredits(id);
            MovieDetails movieDetails = new MovieDetails(movie, images,credits);
            ViewBag.inDB = inDB;
            return View(movieDetails);
        }

        [Route("MovieDetails/SaveMovie")]
        [HttpPost]
        public ActionResult SaveMovie(int id)
        {
            Movie movie = TMDBHelper.GetMovie(id, _memoryCache);
            Images images = TMDBHelper.GetMovieImages(id, _memoryCache);
            Credits credits = TMDBHelper.GetMovieCredits(id, _memoryCache);
            movie.backdrop_bytes = TMDBHelper.GetImageBytes(TMDBHelper.GetImageFullPath(movie.backdrop_path), _memoryCache);
            movie.poster_bytes = TMDBHelper.GetImageBytes(TMDBHelper.GetImageFullPath(movie.poster_path), _memoryCache);
            db.movies.Add(movie);
            db.images.Add(images);
            db.credits.Add(credits);
            db.SaveChanges();
            return Redirect("~/MovieDetails/" + id);
        }

        public Credits GetMovieCredits(int id)
        {
            Credits credits = db.credits.Find(id);
            if (credits != null)
            {
                credits.cast = (from Cast cast in db.casts where cast.Creditsid == id select cast).OrderBy(cast => cast.cast_id).ToList();
                inDB = true;
                return credits;
            }
            credits = TMDBHelper.GetMovieCredits(id, _memoryCache);
            return credits;
        }

        public Images GetMovieImages(int id)
        {
            Images images = db.images.Find(id);
            if (images != null)
            {
                images.posters = (from Poster poster in db.posters where poster.Imagesid == id select poster).ToList();
                inDB = true;
                return images;
            }
            images = TMDBHelper.GetMovieImages(id, _memoryCache);
            return images;
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
    }
}
