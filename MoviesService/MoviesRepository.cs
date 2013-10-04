using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MoviesLibrary;

namespace MoviesService
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly IMoviesCache moviesCache;
        private readonly IMovieDataSourceProxy movieDataSourceProxy;

        public MoviesRepository(IMoviesCache moviesCache, IMovieDataSourceProxy movieDataSourceProxy)
        {
            this.moviesCache = moviesCache;
            this.movieDataSourceProxy = movieDataSourceProxy;
        }

        public IEnumerable<MovieData> GetAll(string sortField = "", IDictionary<string, string> searchFieldValues = null)
        {
            var movies = moviesCache.Movies.AsEnumerable();
            var currentCulture = CultureInfo.InvariantCulture;

            // filter
            if (searchFieldValues != null && searchFieldValues.Count > 0)
                movies =
                    searchFieldValues
                        .SelectMany(searchFieldValue =>
                                movies.WhereField(
                                    searchFieldValue.Key,
                                    fieldValue => 
                                        currentCulture.CompareInfo.IndexOf(
                                            fieldValue,
                                            searchFieldValue.Value,
                                            CompareOptions.IgnoreCase) > -1))
                        .ToList();

            // sort
            if (!string.IsNullOrWhiteSpace(sortField))
                movies = movies.OrderByField(sortField);

            return movies;
        }

        public void Create(MovieData model)
        {
            model.MovieId = movieDataSourceProxy.Create(model);

            // update the cache
            moviesCache.Movies.Add(model);
        }

        public void Update(MovieData model)
        {
            movieDataSourceProxy.Update(model);

            // update the cache
            moviesCache.Movies[
                moviesCache.Movies.IndexOf(
                    moviesCache.Movies.FirstOrDefault(movie => movie.MovieId == model.MovieId))] 
                        = model;
        }
    }
}