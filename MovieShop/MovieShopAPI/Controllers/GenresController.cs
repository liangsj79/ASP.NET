using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreservice)
        {
            _genreService = genreservice;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.ListAllGenresAsync();
            if (!genres.Any())
            {
                return NotFound("No Genres Found");
            }
            return Ok(genres);
        }
    }
}
