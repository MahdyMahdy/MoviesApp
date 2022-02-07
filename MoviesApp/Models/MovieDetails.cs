using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Models
{
    public class MovieDetails
    {
        public MovieDetails(Movie movie,Images images,Credits credits)
        {
            this.movie = movie;
            this.images = images;
            this.credits = credits;
        }
        public Movie movie { get; set; }
        public Images images { get; set; }
        public Credits credits { get; set; }
    }
}
