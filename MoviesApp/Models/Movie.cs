using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Models
{
    public class BelongsToCollection
    {
        [Key]
        public int c_id { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
        [ForeignKey("Movie")]
        public int Movieid { get; set; }
    }
    public class Genre
    {
        [Key]
        public int genre_id { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        [ForeignKey("Movie")]
        public int Movieid { get; set; }
    }

    public class ProductionCompany
    {
        [Key]
        public int pc_id { get; set; }
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
        [ForeignKey("Movie")]
        public int Movieid { get; set; }
    }
    public class ProductionCountry
    {
        [Key]
        public int pc_id { get; set; }
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
        [ForeignKey("Movie")]
        public int Movieid { get; set; }
    }

    public class SpokenLanguage
    {
        [Key]
        public int sl_id { get; set; }
        public string english_name { get; set; }
        public string iso_639_1 { get; set; }
        public string name { get; set; }
        [ForeignKey("Movie")]
        public int Movieid { get; set; }

    }
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public byte[] backdrop_bytes { get; set; }
        public BelongsToCollection belongs_to_collection { get; set; }
        public int budget { get; set; }
        public List<Genre> genres { get; set; }
        public string homepage { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public byte[] poster_bytes { get; set; }
        public List<ProductionCompany> production_companies { get; set; }
        public List<ProductionCountry> production_countries { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public List<SpokenLanguage> spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }
}
