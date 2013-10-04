using System.Collections.Generic;
using MoviesLibrary;

namespace MoviesService
{
    /// <summary>
    /// Wraps the MovieDataSource so classes that consume it (e.g. MoviesRepository) can be unit-tested.
    /// </summary>
    public class MovieDataSourceProxy : IMovieDataSourceProxy
    {
        private readonly MovieDataSource movieDataSource = new MovieDataSource();

        public List<MovieData> GetAllData()
        {
            return movieDataSource.GetAllData();
        }

        public MovieData GetDataById(int id)
        {
            return movieDataSource.GetDataById(id);
        }

        public int Create(MovieData movie)
        {
            return movieDataSource.Create(movie);
        }

        public void Update(MovieData movie)
        {
            movieDataSource.Update(movie);
        }
    }
}