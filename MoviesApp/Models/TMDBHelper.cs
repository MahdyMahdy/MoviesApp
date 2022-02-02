using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MoviesApp.Models
{
    public class TMDBHelper
    {
        private const string json_base_url = "https://api.themoviedb.org/3/movie/";
        private const string api_key = "90f1a97417721d66d405f554e092a4ba";
        private const string image_base_url = "https://image.tmdb.org/t/p/original/";
        public static Page GetPopularMoviesPage(int page)
        {
            using (WebClient wc = new WebClient())
            {
                string request = json_base_url + "popular?api_key=" + api_key+ "&page="+page;
                var json = wc.DownloadString(request);
                Page res = JsonConvert.DeserializeObject<Page>(json);
                return res;
            }
        }

        public static Images GetMovieImages(int id)
        {
            using (WebClient wc = new WebClient())
            {
                string request = json_base_url + id+"/images?api_key=" + api_key ;
                var json = wc.DownloadString(request);
                Images res = JsonConvert.DeserializeObject<Images>(json);
                return res;
            }
        }

        public static byte[] GetImageFromPath(string path)
        {
            using (WebClient wc = new WebClient())
            {
                string request = image_base_url + path;
                byte[] image_bytes = wc.DownloadData(request);
                return image_bytes;
            }
        }
    }
}
