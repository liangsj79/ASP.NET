using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IConfiguration _configuration;
        public AccountController(IUserService userService, ICurrentUserService currentUserService, IConfiguration configuration)
        {
            _userService = userService;
            _currentUserService = currentUserService;
            _configuration = configuration;
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

        [HttpGet]
        [Route("{id:int}", Name = "GetUser")]
        public async Task<ActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetUserDetailsById(id);
            return Ok(user);
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register( [FromBody] UserRegisterRequestModel model)
        {
            var user = await _userService.RegisterUser(model);
            return Ok(user);
        }

      

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel model)
        {
            var user = await _userService.ValidateUser(model);
            if (user == null)
            {
                //invalid un/password
                return Unauthorized();
            }

            //un/pw is valid
            //create JWT and send it to client(Angular), add claims info in the token
            return Ok(new { token = GenerateJWT(user) });
        }

        private string GenerateJWT(UserLoginResponseModel user)
        {
            var claims = new List<Claim> { 
            
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            };

            // add the required claims to identity object

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // get the secret key for signing the token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));

            // specify the algorithm to sign the token
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("ExpirationHours"));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _configuration["Issuer"],
                Audience = _configuration["Audience"]
            };

            var encodedJwt = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(encodedJwt);


        }

    }
}
