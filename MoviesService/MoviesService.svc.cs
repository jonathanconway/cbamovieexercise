using System.Collections.Generic;
using System.Linq;
using MoviesLibrary;
using MoviesService.Contracts;

namespace MoviesService
{
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesRepository moviesRepository;

        public MoviesService(IMoviesRepository moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public Movie[] GetAllMovies()
        {
            var movies = 
                moviesRepository
                    .GetAll()
                    .Select(MapMovieDataToMovie)
                    .ToArray();
            return movies;
        }

        public Movie[] GetMovies(MovieSortFields? sortField = null, IDictionary<MovieFilterFields, string> filterFieldsValues = null)
        {
            return moviesRepository
                .GetAll(sortField, filterFieldsValues)
                .Select(MapMovieDataToMovie)
                .ToArray();
        }

        public void Create(Movie movie)
        {
            moviesRepository.Create(MapMovieToMovieData(movie));
        }

        public void Update(Movie movie)
        {
            moviesRepository.Update(MapMovieToMovieData(movie));
        }

        private static Movie MapMovieDataToMovie(MovieData movieData)
        {
            return new Movie
            {
                Title = movieData.Title,
                Cast = movieData.Cast,
                Classification = movieData.Classification,
                MovieId = movieData.MovieId,
                Genre = movieData.Genre,
                ReleaseDate = movieData.ReleaseDate,
                Rating = movieData.Rating
            };
        }

        private static MovieData MapMovieToMovieData(Movie movieData)
        {
            return new MovieData
            {
                Title = movieData.Title,
                Cast = movieData.Cast,
                Classification = movieData.Classification,
                MovieId = movieData.MovieId,
                Genre = movieData.Genre,
                ReleaseDate = movieData.ReleaseDate,
                Rating = movieData.Rating
            };
        }
    }
}