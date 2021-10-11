using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService (ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }
        public async Task<CastDetailsResponseModel> GetCastDetailsById(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            var castModel = new CastDetailsResponseModel { 
                Id = cast.Id,
                Gender = cast.Gender,
                Name =cast.Name,
                TmdbUrl = cast.TmdbUrl,
                ProfilePath = cast.ProfilePath,
            };
            castModel.Movies = new List<MovieModel>();
            foreach (var movieCast in cast.Movies)
            {
                castModel.Movies.Add(new MovieModel
                {
                    Title = movieCast.Movie.Title,
                    PosterUrl = movieCast.Movie.PosterUrl,
                    ReleaseDate = movieCast.Movie.ReleaseDate.GetValueOrDefault(),
                    RunTime = movieCast.Movie.RunTime.GetValueOrDefault(),
                    Rating =  movieCast.Movie.Reviews.Count != 0 ? movieCast.Movie.Reviews.Average(r => r.Rating) : 0.0m

                });
            }
           
            return castModel;
        }
    }
}
