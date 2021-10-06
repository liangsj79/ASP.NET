using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        public UserController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }
        // showing list of movies
        // Filters
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Purchases()
        {
            var userId = _currentUserService.UserId;
            var purchases = await _userService.GetPurchasedMoviesByUserId(userId);
            // call the User Service to get movies purchased by user, and send the data to the view, and use the existing MovieCard Partial View
            return View(purchases);
        }




        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentUserService.UserId;
            var favorites = await _userService.GetFavoriteMoviesByUserId(userId);
            // call the User Service to get movies favorited by user, and send the data to the view, and use the existing MovieCard Partial View
            return View(favorites);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details()
        {

            var email = _currentUserService.Email;
            var user = await _userService.GetUserDetailsByEmail(email);
            user.FullName = _currentUserService.FullName;

            // call the User Service to get User Details and dispaly in a View
            // get user id from httpcontext and send it to user services
            return View(user);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var email = _currentUserService.Email;
      
            var user = await _userService.GetUserEditInformation(email);

            return View(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(UserEditRequestModel model)
        {
            var user = await _userService.UpdateUserInformation(model);
            return RedirectToAction("Logout", "Account");
        }
    }
}
