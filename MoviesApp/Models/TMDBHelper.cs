using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;
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
        private const string token = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI5MGYxYTk3NDE3NzIxZDY2ZDQwNWY1NTRlMDkyYTRiYSIsInN1YiI6IjU0ZjQ5OGE2OTI1MTQxNzk5ZjAwMjFmMiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.AmH9ahcSsro2udQ5FbFbLBM6d62_nlZH8oyKlCJ8x8w";
        private const string image_base_url = "https://image.tmdb.org/t/p/original/";

        public static Page GetPopularMoviesPage(int page,IMemoryCache memoryCache)
        {
            string url = json_base_url + "popular?page=" + page;
            if (!memoryCache.TryGetValue(url,out Page res))
            {
                using (WebClient wc = new WebClient())
                {
                    RestClient restClient = new RestClient(url);

                    RestRequest request = new RestRequest();

                    request.Method = Method.Get;

                    request.AddHeader("Authorization", "Bearer " + token);

                    var response = restClient.ExecuteGetAsync(request);
                    res = JsonConvert.DeserializeObject<Page>(response.Result.Content);
                    var cacheExpOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromSeconds(20)
                    };
                    memoryCache.Set(url, res, cacheExpOptions);
                }
            }
            return res;
        }

        public static Movie GetMovie(int id, IMemoryCache memoryCache)
        {
            string url = json_base_url + id ;
            if (!memoryCache.TryGetValue(url, out Movie res))
            {
                using (WebClient wc = new WebClient())
                {
                    RestClient restClient = new RestClient(url);

                    RestRequest request = new RestRequest();

                    request.Method = Method.Get;

                    request.AddHeader("Authorization", "Bearer " + token);

                    var response = restClient.ExecuteGetAsync(request);
                    res = JsonConvert.DeserializeObject<Movie>(response.Result.Content);
                    var cacheExpOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromSeconds(20)
                    };
                    memoryCache.Set(request, res, cacheExpOptions);
                }
            }
            return res;
        }

        public static byte[] GetImageBytes(string path, IMemoryCache memoryCache)
        {
            string request = image_base_url + path;
            if (!memoryCache.TryGetValue(request, out byte[] res))
            {
                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        res = wc.DownloadData(request);
                    }
                    catch (Exception)
                    {
                        res = new byte[0];
                    }
                    var cacheExpOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromSeconds(20)
                    };
                    memoryCache.Set(request, res, cacheExpOptions);
                }
            }
            return res;
        }

        public static string GetImageFullPath(string path)
        {
            string request = image_base_url + path;
            return request;
        }
    }
}
