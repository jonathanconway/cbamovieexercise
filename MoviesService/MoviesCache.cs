using System;
using System.Collections.Generic;
using MoviesLibrary;

namespace MoviesService
{
    /// <summary>
    /// Keeps the movies in a 24-hour memory cache, as calls to the service are expensive.
    /// </summary>
    public class MoviesCache : IMoviesCache
    {
        private static List<MovieData> _movies;
        private static DateTime _lastAccessTime = DateTime.MinValue;
        
        private readonly IMovieDataSourceProxy movieDataSourceProxy;

        public MoviesCache(IMovieDataSourceProxy movieDataSourceProxy)
        {
            this.movieDataSourceProxy = movieDataSourceProxy;
        }

        public List<MovieData> Movies
        {
            get
            {
                if (DateTime.Now.Subtract(_lastAccessTime).TotalHours >= 24)
                {
                    _movies = movieDataSourceProxy.GetAllData();
                    _lastAccessTime = DateTime.Now;
                }
                return _movies;
            }
        }
    }
}