using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.RepositoryInterfaces;

namespace MovieShopMVC.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly IMovieRepository _movieRepository;
        public HeaderViewComponent(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var genres =  _movieRepository.GetAllGenres();
            return View(genres);
        }

    }
}
