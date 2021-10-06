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
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        public AccountController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var email = _currentUserService.Email;
            var user = await _userService.GetUserDetailsByEmail(email);
            user.FullName = _currentUserService.FullName;

            // call the User Service to get User Details and dispaly in a View
            // get user id from httpcontext and send it to user services
            return Ok(user);
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register( [FromBody] UserRegisterRequestModel model)
        {
            var user = await _userService.RegisterUser(model);
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel model)
        {
            var user = await _userService.ValidateUser(model);
            if (user == null)
            {
                return NotFound("No user is found");
            }
            return Ok(user);
        }




    }
}
