using System.Collections.Generic;
using MoviesLibrary;

namespace MoviesService
{
    public interface IMoviesRepository
    {
        IEnumerable<MovieData> GetAll(string sortField = "", IDictionary<string, string> searchFieldValues = null);
        void Create(MovieData model);
        void Update(MovieData model);
    }
}