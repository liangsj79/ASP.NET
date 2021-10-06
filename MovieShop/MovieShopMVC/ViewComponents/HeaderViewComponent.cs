using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace MovieShopMVC.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly IGenreService _genreService;
        public HeaderViewComponent(IGenreService genreService)
        {
            _genreService = genreService;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _genreService.ListAllGenresAsync();
            return View(genres);
        }

    }
}
