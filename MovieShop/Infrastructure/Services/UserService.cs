using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
        }
        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // first check if the email user entered exists in the database
            // if yes, throw an throw exception or send a message saying email exists
            var user = await _userRepository.GetUserByEmail(requestModel.Email);

            if (user != null)
            {
                // email exits in the database
                throw new Exception($"Email {requestModel.Email} exists, please try to login");
            }
            // continue
            // create a random salt and hash the password with the salt

            var salt = GenerateSalt();
            var hashedPassword = GenerateHashedPassword(requestModel.Password, salt);

            // create user entity object and call user repo to save
            var newUser = new User
            {
                Email = requestModel.Email,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                DateOfBirth = requestModel.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };

            var createdUser = await _userRepository.AddAsync(newUser);

            var userRegisterResponseModel = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return userRegisterResponseModel;


        }

        public async Task<UserLoginResponseModel> ValidateUser(UserLoginRequestModel requestModel)
        {
            // get the user info form database by email
            var user = await _userRepository.GetUserByEmail(requestModel.Email);
            if (user == null)
            {
                return null;
            }

            var hashedPassword = GenerateHashedPassword(requestModel.Password, user.Salt);
            // we need to hash the user entered password along with salt from database
            if (hashedPassword == user.HashedPassword)
            {
                // user entered the correct password
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth

                };
                userLoginResponseModel.Roles = new List<RoleModel>();
                foreach(var userRole in user.Roles)
                {
                    userLoginResponseModel.Roles.Add(new RoleModel
                    {
                        Id = userRole.Role.Id,
                        Name = userRole.Role.Name
                    });
                }
                return userLoginResponseModel;
            }
            return null;
            
        }


        private string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);

        }

        private string GenerateHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;

        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetPurchasedMoviesByUserId(int id)
        {
            
            var purchases = await _purchaseRepository.ListAsync(p => p.UserId == id);
            if (purchases == null)
            {
                return null;
            }
            var movieCardResponseModel = new List<MovieCardResponseModel>();

            foreach (var purchase in purchases)
            {
                movieCardResponseModel.Add(new MovieCardResponseModel
                {
                    Id = purchase.Movie.Id,
                    PosterUrl = purchase.Movie.PosterUrl,
                    Revenue = purchase.Movie.Revenue.GetValueOrDefault(),
                    Title = purchase.Movie.Title
                });
            }


            return movieCardResponseModel;

        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetFavoriteMoviesByUserId(int id)
        {

            var favorites= await _favoriteRepository.ListAsync(p => p.UserId == id);
            if (favorites == null)
            {
                return null;
            }
            var movieCardResponseModel = new List<MovieCardResponseModel>();

            foreach (var purchase in favorites)
            {
                movieCardResponseModel.Add(new MovieCardResponseModel
                {
                    Id = purchase.Movie.Id,
                    PosterUrl = purchase.Movie.PosterUrl,
                    Revenue = purchase.Movie.Revenue.GetValueOrDefault(),
                    Title = purchase.Movie.Title
                });
            }


            return movieCardResponseModel;

        }

        public async Task<UserDetailsResponseModel> GetUserDetailsByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            var userDetailsResponseModel = new UserDetailsResponseModel
            {
                Email  = user.Email,
                DateOfBirth =user.DateOfBirth.GetValueOrDefault(),
                PhoneNumber = user.PhoneNumber,
                TwoFactorEnabled =user.TwoFactorEnabled.GetValueOrDefault()
            };
            return userDetailsResponseModel;
        }


        public async Task<UserEditRequestModel> GetUserEditInformation(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            var userEditRequestModel = new UserEditRequestModel
            {
                Id =user.Id,
                Email = user.Email,
                FirstName =user.FirstName,
                LastName =user.LastName,
                DateOfBirth = user.DateOfBirth.GetValueOrDefault(),
               
            };
            return userEditRequestModel;
        }

        public async Task<User> UpdateUserInformation(UserEditRequestModel requestModel)
        {
            var user = await _userRepository.GetUserByEmail(requestModel.Email);
            if ( requestModel.Email != null && requestModel.FirstName!=null && requestModel.LastName != null)
            {
                user.Email = requestModel.Email;
                user.FirstName = requestModel.FirstName;
                user.LastName = requestModel.LastName;
                if(requestModel.Password != null)
                {
                    var hashedPassword = GenerateHashedPassword(requestModel.Password, user.Salt);
                    user.HashedPassword = hashedPassword;
                }
                if(requestModel.DateOfBirth != null)
                {
                    user.DateOfBirth = requestModel.DateOfBirth;
                }
                var response = await _userRepository.UpdateAsync(user);
                return response;
            }
            else
            {
                throw new System.Exception("User information is not updated");
            }
          
        }
    }
}
