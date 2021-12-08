using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using ApplicationCore.Models;
using System.Linq;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTest
    {
        private MovieService _sut;

        private static List<Movie> _movies;
        private Mock<IMovieRepository> _mockMovieRepository;
        private Mock<IReviewRepository> _mockReviewRepository;
        private Mock<IMovieGenreRepository> _mockMovieGenreRepository;
        [TestInitialize]
        public void OneTimeSetup()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockReviewRepository = new Mock<IReviewRepository>();
            _mockMovieGenreRepository = new Mock<IMovieGenreRepository>();
            _sut = new MovieService(_mockMovieRepository.Object, _mockReviewRepository.Object, _mockMovieGenreRepository.Object);
            _mockMovieRepository.Setup(expression: m => m.Get30HighestGrossingMovies()).ReturnsAsync(_movies);

            

            
            // [OneTimeSetup] in Nunit
        }

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _movies = new List<Movie> {
                 new Movie {Id = 1, Title="Avengers: Infinity War", Budget =1200000},
                new Movie {Id = 2, Title="Avatar", Budget=1200000}
            };
        }

        [TestMethod]
        public async Task TestListOfHigestGrossingMoviesFromFakeData()
        {
            // SUT System under Test MovieService => Get30HighestGrossingMovies

            // Arrange
            // mock objects, data, methods etc
/*            _sut = new MovieService(new MockMovieRepository(), new MockReviewRepository(), new MockMovieGenreRepository() );
*/            // Act
            var movies = await _sut.Get30HighestGrossingMovies();

            // check the actual output with expected data. 
            // AAA
            // Arrange, Act and Assert

            // Assert
            Assert.IsNotNull(movies);
            Assert.IsInstanceOfType(movies, typeof(IEnumerable<MovieCardResponseModel>));
            Assert.AreEqual(expected: 2, actual: movies.Count());
            
        }
    }

    /*public class MockMovieRepository : IMovieRepository
    {
        public Task<Movie> AddAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            var _movies = new List<Movie>{
                new Movie {Id = 1, Title="Avengers: Infinity War", Budget =1200000},
                new Movie {Id = 2, Title="Avatar", Budget=1200000}

            };
            return _movies;
        }

        public Task<Movie> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(Expression<Func<Movie, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetExistsAsync(Expression<Func<Movie, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesByGenreId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesWithReviews()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> ListAsync(Expression<Func<Movie, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }
    }
    public class MockReviewRepository : IReviewRepository
    {
        public Task<Review> AddAsync(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(Expression<Func<Review, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetExistsAsync(Expression<Func<Review, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> ListAsync(Expression<Func<Review, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Review> UpdateAsync(Review entity)
        {
            throw new NotImplementedException();
        }
    }

    public class MockMovieGenreRepository : IMovieGenreRepository
    {
        public Task<MovieGenre> AddAsync(MovieGenre entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(MovieGenre entity)
        {
            throw new NotImplementedException();
        }

        public Task<MovieGenre> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(Expression<Func<MovieGenre, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetExistsAsync(Expression<Func<MovieGenre, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MovieGenre>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MovieGenre>> ListAsync(Expression<Func<MovieGenre, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<MovieGenre> UpdateAsync(MovieGenre entity)
        {
            throw new NotImplementedException();
        }
    }*/
}
