using System.Collections.Generic;
using MoviesLibrary;
using MoviesService.Contracts;

namespace MoviesService
{
    public interface IMoviesRepository
    {
        IEnumerable<MovieData> GetAll(MovieSortFields? sortField = null, IDictionary<MovieFilterFields, string> filterFieldsValues = null);
        void Create(MovieData model);
        void Update(MovieData model);
    }
}