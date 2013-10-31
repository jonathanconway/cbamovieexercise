using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoviesLibrary;

namespace MoviesService.Tests.Unit
{
    [TestClass]
    public class MoviesCacheTests
    {
        [TestMethod]
        public void Movies_CallsDataSourceFirstTime_ButNotSecond()
        {
            var mockMovieDataSourceProxy = new Mock<IMovieDataSourceProxy>();
            var movieDataSourceProxy = mockMovieDataSourceProxy.Object;
            mockMovieDataSourceProxy.Setup(x => x.GetAllData()).Returns(new List<MovieData>
            {
                new MovieData(), new MovieData(), new MovieData()
            });
            var moviesCache = new MoviesCache(movieDataSourceProxy);

            Console.WriteLine(moviesCache.Movies.Count());
            Console.WriteLine(moviesCache.Movies.Count());

            mockMovieDataSourceProxy.Verify(x => x.GetAllData(), Times.Once);
        }
    }
}
