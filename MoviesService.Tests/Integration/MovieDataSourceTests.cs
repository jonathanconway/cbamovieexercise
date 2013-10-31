using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesLibrary;
using System.Linq;

namespace MoviesService.Tests.Integration
{
    [TestClass]
    public class MovieDataSourceTests
    {
        [TestMethod]
        public void GetAllData_GetsRecords()
        {
            // Arrange
            var movieDataSource = new MovieDataSource();

            // Act
            var results = movieDataSource.GetAllData();

            // Assert
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void GetDataById_GetsCorrectRecord()
        {
            // Arrange
            var movieDataSource = new MovieDataSource();

            // Act
            var results = movieDataSource.GetAllData();
            var resultThatShouldBeRetrieved = results.FirstOrDefault();
            var resultById = movieDataSource.GetDataById(resultThatShouldBeRetrieved.MovieId);

            // Assert
            Assert.AreEqual(resultThatShouldBeRetrieved.MovieId, resultById.MovieId);
            for (var i = 0; i < resultThatShouldBeRetrieved.Cast.Count(); i++)
                Assert.AreEqual(resultThatShouldBeRetrieved.Cast[i], resultById.Cast[i]);
            Assert.AreEqual(resultThatShouldBeRetrieved.Classification, resultById.Classification);
            Assert.AreEqual(resultThatShouldBeRetrieved.Genre, resultById.Genre);
            Assert.AreEqual(resultThatShouldBeRetrieved.Rating, resultById.Rating);
            Assert.AreEqual(resultThatShouldBeRetrieved.ReleaseDate, resultById.ReleaseDate);
            Assert.AreEqual(resultThatShouldBeRetrieved.Title, resultById.Title);
        }

        [TestMethod]
        public void Create_CreatesNewRecord()
        {
            // Arrange
            var movieDataSource = new MovieDataSource();

            var newMovie = new MovieData
            {
                Title = "Apocalypse Now",
                Cast = new [] { "Martin Sheen", "Marlon Brando", "Robert Duvall" },
                Classification = "R",
                Genre = "Drama",
                Rating = 5,
                ReleaseDate = 1979
            };

            // Act
            movieDataSource.Create(newMovie);


            // Assert
            var testResult = movieDataSource.GetDataById(newMovie.MovieId);
            Assert.AreEqual(newMovie.MovieId, testResult.MovieId);
            Assert.AreEqual(newMovie.Cast[0], testResult.Cast[0]);
            Assert.AreEqual(newMovie.Cast[1], testResult.Cast[1]);
            Assert.AreEqual(newMovie.Cast[2], testResult.Cast[2]);
            Assert.AreEqual(newMovie.Classification, testResult.Classification);
            Assert.AreEqual(newMovie.Genre, testResult.Genre);
            Assert.AreEqual(newMovie.Rating, testResult.Rating);
            Assert.AreEqual(newMovie.ReleaseDate, testResult.ReleaseDate);
            Assert.AreEqual(newMovie.Title, testResult.Title);
        }

        [TestMethod]
        public void Update_UpdateRecord()
        {
            // Arrange
            var movieDataSource = new MovieDataSource();

            var updateMovie = movieDataSource.GetDataById(1);
            updateMovie.Title = updateMovie.Title = " 2";

            // Act
            movieDataSource.Update(updateMovie);

            // Assert
            var testResult = movieDataSource.GetDataById(1);
            Assert.AreEqual(updateMovie.Title, testResult.Title);
        }
    }
}