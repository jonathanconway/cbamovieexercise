using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using MoviesService.Contracts;

namespace MoviesService
{
    [ServiceContract]
    public interface IMoviesService
    {
        [OperationContract]
        Movie[] GetAllMovies();

        [OperationContract]
        Movie[] GetMovies(MovieSortFields? sortField = null, IDictionary<MovieFilterFields, string> filterFieldsValues = null);

        [OperationContract]
        void Create(Movie movie);

        [OperationContract]
        void Update(Movie movie);
    }
}