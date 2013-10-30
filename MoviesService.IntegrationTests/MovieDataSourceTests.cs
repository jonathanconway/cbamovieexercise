using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesLibrary;
using System.Linq;

namespace MoviesService.IntegrationTests
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
            Assert.AreEqual(resultThatShouldBeRetrieved.Cast, resultById.Cast);
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
                MovieId =  999,
                Cast = new [] { "Martin Sheen", "Marlon Brando", "Robert Duvall" },
                Classification = "R",
                Genre = "Drama",
                Rating = 5,
                ReleaseDate = 1979
            };

            // Act
            movieDataSource.Create(newMovie);


            // Assert
            var testResult = movieDataSource.GetDataById(999);
            Assert.AreEqual(newMovie.MovieId, testResult.MovieId);
            Assert.AreEqual(newMovie.Cast, testResult.Cast);
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
