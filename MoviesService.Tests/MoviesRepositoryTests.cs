using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Moq;
using MoviesLibrary;
using NUnit.Framework;

namespace MoviesService.Tests
{
    [TestFixture]
    public class MoviesRepositoryTests
    {
        [Test]
        public void GetAll_Works()
        {
            // Arrange
            var mockMoviesCache = new Mock<IMoviesCache>();
            mockMoviesCache.Setup(x => x.Movies).Returns(new List<MovieData>
            {
                new MovieData {},
                new MovieData {},
                new MovieData {}
            });
            var movieDataSourceProxy = new Mock<IMovieDataSourceProxy>().Object;
            var moviesRepo = new MoviesRepository(mockMoviesCache.Object, movieDataSourceProxy);

            // Act
            var results = moviesRepo.GetAll().ToArray();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count());
        }

        [Test]
        public void GetAll_SortsCorrectly()
        {
            // Arrange
            var mockMoviesCache = new Mock<IMoviesCache>();
            mockMoviesCache.Setup(x => x.Movies).Returns(new List<MovieData>
            {
                new MovieData {Title = "ccc", MovieId = 1},
                new MovieData {Title = "bbb", MovieId = 2},
                new MovieData {Title = "aaa", MovieId = 3}
            });
            var movieDataSourceProxy = new Mock<IMovieDataSourceProxy>().Object;
            var moviesRepo = new MoviesRepository(mockMoviesCache.Object, movieDataSourceProxy);
            
            // Act
            var results = moviesRepo.GetAll("title").ToArray();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results[0].MovieId);
            Assert.AreEqual(2, results[1].MovieId);
            Assert.AreEqual(1, results[2].MovieId);
        }

        [Test]
        public void GetAll_FiltersCorrectly()
        {
            // Arrange
            var mockMoviesCache = new Mock<IMoviesCache>();
            mockMoviesCache.Setup(x => x.Movies).Returns(new List<MovieData>
            {
                new MovieData {MovieId = 1, Classification = "G" },
                new MovieData {MovieId = 2, Classification = "PG" },
                new MovieData { MovieId = 3, Classification = "PG" }
            });
            var movieDataSourceProxy = new Mock<IMovieDataSourceProxy>().Object;
            var moviesRepo = new MoviesRepository(mockMoviesCache.Object, movieDataSourceProxy);

            // Act
            var results = moviesRepo.GetAll(null, new Dictionary<string, string> { { "classification", "PG" } }).ToArray();

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(2, results[0].MovieId);
            Assert.AreEqual(3, results[1].MovieId);
        }

        [Test]
        public void Create_CreatesMovie_AndUpdatesCache()
        {
            // Arrange
            var mockMoviesCache = new Mock<IMoviesCache>();
            mockMoviesCache.Setup(x => x.Movies).Returns(new List<MovieData> {});
            var mockMovieDataSourceProxy = new Mock<IMovieDataSourceProxy>();
            var moviesCache = mockMoviesCache.Object;
            var moviesRepo = new MoviesRepository(moviesCache, mockMovieDataSourceProxy.Object);
            var movieData = new MovieData
                                {
                                    Title = "Test",
                                    Cast = new [] {"Jonathan Conway", "Abigail Conway"},
                                    Genre = "Horror",
                                    MovieId = 1,
                                    Classification = "R",
                                    Rating = 5,
                                    ReleaseDate = 1
                                };

            // Act
            moviesRepo.Create(movieData);

            // Assert
            mockMovieDataSourceProxy.Verify(x => x.Create(It.Is<MovieData>(y => y.Title == movieData.Title)));
            moviesCache.Movies.Contains(movieData);
        }

        [Test]
        public void Update_UpdatesMovie_AndUpdatesCache()
        {
            // Arrange
            var mockMoviesCache = new Mock<IMoviesCache>();
            mockMoviesCache.Setup(x => x.Movies).Returns(new List<MovieData> { 
                new MovieData
                {
                    Title = "Test"
                }});
            var mockMovieDataSourceProxy = new Mock<IMovieDataSourceProxy>();
            var moviesCache = mockMoviesCache.Object;
            var moviesRepo = new MoviesRepository(moviesCache, mockMovieDataSourceProxy.Object);
            var movieData = new MovieData
            {
                Title = "New Title"
            };

            // Act
            moviesRepo.Update(movieData);

            // Assert
            mockMovieDataSourceProxy.Verify(x => x.Update(It.Is<MovieData>(y => y.Title == movieData.Title)));
            moviesCache.Movies.Any(x => x.Title == movieData.Title);
        }
    }
}
