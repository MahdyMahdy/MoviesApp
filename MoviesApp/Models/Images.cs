using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Models
{
    public class Backdrop
    {
        [Key]
        public int id { get; set; }
        public double aspect_ratio { get; set; }
        public int height { get; set; }
        public string iso_639_1 { get; set; }
        public string file_path { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public int width { get; set; }
        [ForeignKey("Images")]
        public int Imagesid { get; set; }
    }

    public class Logo
    {
        [Key]
        public int id { get; set; }
        public double aspect_ratio { get; set; }
        public int height { get; set; }
        public string iso_639_1 { get; set; }
        public string file_path { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public int width { get; set; }
        [ForeignKey("Images")]
        public int Imagesid { get; set; }
    }

    public class Poster
    {
        [Key]
        public int id { get; set; }
        public double aspect_ratio { get; set; }
        public int height { get; set; }
        public string iso_639_1 { get; set; }
        public string file_path { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public int width { get; set; }
        [ForeignKey("Images")]
        public int Imagesid { get; set; }
    }

    public class Images
    {
        public List<Backdrop> backdrops { get; set; }
        [Key]
        public int id { get; set; }
        public List<Logo> logos { get; set; }
        public List<Poster> posters { get; set; }
    }
}
