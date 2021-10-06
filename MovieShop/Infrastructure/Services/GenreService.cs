using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<IEnumerable<GenreModel>> ListAllGenresAsync()
        {
            var genres = await _genreRepository.ListAllAsync();
            var allGenres = new List<GenreModel>();
            foreach (var genre in genres)
            {
                allGenres.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }
            return allGenres;
        }


    }
}
