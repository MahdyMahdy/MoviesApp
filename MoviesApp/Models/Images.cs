using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Models
{
    public class Images
    {
        public List<Backdrop> backdrops { get; set; }
        public int id { get; set; }
        public List<Logo> logos { get; set; }
        public List<Poster> posters { get; set; }
    }
}
