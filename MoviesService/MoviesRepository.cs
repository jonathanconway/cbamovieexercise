using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MoviesLibrary;
using MoviesService.Contracts;

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

        private static bool Compare(string value, string search)
        {
            return value.Trim().ToLower() == search.Trim().ToLower();
        }

        private static bool Compare(int value, string search)
        {
            if (search == null)
                return false;

            return value == int.Parse(search);
        }

        public IEnumerable<MovieData> GetAll(MovieSortFields? sortField = null, IDictionary<MovieFilterFields, string> filterFieldsValues = null)
        {
            var movies = moviesCache.Movies.AsEnumerable();

            // filter
            if (filterFieldsValues != null && filterFieldsValues.Count > 0)
            {
                if (filterFieldsValues.ContainsKey(MovieFilterFields.MovieId))
                    movies = movies.Where(movie => Compare(movie.MovieId, filterFieldsValues[MovieFilterFields.MovieId]));

                if (filterFieldsValues.ContainsKey(MovieFilterFields.Title))
                    movies = movies.Where(movie => Compare(movie.Title, filterFieldsValues[MovieFilterFields.Title]));

                if (filterFieldsValues.ContainsKey(MovieFilterFields.Cast))
                {
                    movies = movies.Where(movie =>
                        movie.Cast != null &&
                        movie.Cast.Count(cast =>
                            Compare(cast, filterFieldsValues[MovieFilterFields.Cast])) > 0).ToList();
                }

                if (filterFieldsValues.ContainsKey(MovieFilterFields.Classification))
                    movies = movies.Where(movie => Compare(movie.Classification, filterFieldsValues[MovieFilterFields.Classification]));

                if (filterFieldsValues.ContainsKey(MovieFilterFields.Genre))
                    movies = movies.Where(movie => Compare(movie.Genre, filterFieldsValues[MovieFilterFields.Genre]));

                if (filterFieldsValues.ContainsKey(MovieFilterFields.Rating))
                    movies = movies.Where(movie => Compare(movie.Rating, filterFieldsValues[MovieFilterFields.Rating]));

                if (filterFieldsValues.ContainsKey(MovieFilterFields.ReleaseDate))
                    movies = movies.Where(movie => Compare(movie.ReleaseDate, filterFieldsValues[MovieFilterFields.ReleaseDate]));
            }

            if (sortField.HasValue)
            {
                var sortFieldsToOrderByFunc =
                    new Dictionary<MovieSortFields, Func<MovieData, object>>
                    {
                        {MovieSortFields.Title, (movie) => movie.Title},
                        {MovieSortFields.Classification, (movie) => movie.Classification},
                        {MovieSortFields.Genre, (movie) => movie.Genre},
                        {MovieSortFields.MovieId, (movie) => movie.MovieId},
                        {MovieSortFields.Rating, (movie) => movie.Rating},
                        {MovieSortFields.ReleaseDate, (movie) => movie.ReleaseDate}
                    };
                movies = movies.OrderBy(sortFieldsToOrderByFunc[sortField.Value]);
            }

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