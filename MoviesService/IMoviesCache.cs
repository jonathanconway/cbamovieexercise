using System.Collections.Generic;
using MoviesLibrary;

namespace MoviesService
{
    public interface IMoviesCache
    {
        List<MovieData> Movies { get; }
    }
}