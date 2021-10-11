using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public UserController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [Authorize]
        [Route("purchase")]
        [HttpPost]
        public async Task<IActionResult> AddPurchase([FromBody] PurchaseRequestModel requestModel)
        {
            var userId = _currentUserService.UserId;
            var response = await _userService.AddPurchase(requestModel, userId);
            if (!response)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [Authorize]
        [Route("favorite")]
        [HttpPost]
        public async Task<IActionResult> AddFavortie([FromBody] FavoriteRequestModel requestModel)
        {
            await _userService.AddFavorite(requestModel);
            return Ok();
        }

        [Authorize]
        [Route("unfavorite")]
        [HttpPost]
        public async Task<IActionResult> Unfavortie([FromBody] FavoriteRequestModel requestModel)
        {
            await _userService.AddFavorite(requestModel);
            return Ok();
        }

        [Authorize]
        [Route("{id:int}/movie/{movieId}/favorite")]
        [HttpGet]
        public async Task<ActionResult> IsFavoriteExists(int id, int movieId)
        {
            var favoriteExists = await _userService.FavoriteExists(id, movieId);
            return Ok(new { isFavorited = favoriteExists });
        }

        [Authorize]
        [Route("review")]
        [HttpPost]
        public async Task<IActionResult> AddMovieReview([FromBody] ReviewRequestModel requestModel)
        {
            await _userService.AddMovieReview(requestModel);
            return Ok();
        }

        [Authorize]
        [Route("review")]
        [HttpPut]
        public async Task<IActionResult> UpdateMovieReview([FromBody] ReviewRequestModel requestModel)
        {
            await _userService.AddMovieReview(requestModel);
            return Ok();
        }


        [Authorize]
        [Route("{userId}/movie/{movieId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMovieReview(int userId, int movieId)
        {
            await _userService.DeleteMovieReview(userId, movieId);
            return Ok();
        }



        [Authorize]
        [Route("purchases")]
        [HttpGet]
        public async Task<IActionResult> GetUserPurchases()
        {
            var userId = _currentUserService.UserId;
            var purchases = await _userService.GetPurchasedMoviesByUserId(userId);
            return Ok(purchases);
        }




        [Authorize]
        [Route("favorites")]
        [HttpGet]
        public async Task<IActionResult> GetUserFavorites()
        {
            var userId = _currentUserService.UserId;
            var favorites = await _userService.GetFavoriteMoviesByUserId(userId);
            return Ok(favorites);
        }



        [Authorize]
        [Route("reviews")]
        [HttpGet]
        public async Task<IActionResult> GetUserReviews()
        {
            var userId = _currentUserService.UserId;
            var reviews = await _userService.GetReviewsByUserId(userId);
            return Ok(reviews);
        }
    }
}
