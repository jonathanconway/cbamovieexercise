using System.Collections.Generic;
using MoviesLibrary;

namespace MoviesService
{
    public interface IMovieDataSourceProxy
    {
        List<MovieData> GetAllData();
        MovieData GetDataById(int id);
        int Create(MovieData movie);
        void Update(MovieData movie);
    }
}