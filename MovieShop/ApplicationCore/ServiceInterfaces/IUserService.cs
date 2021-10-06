using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> ValidateUser(UserLoginRequestModel requestModel);

        Task<IEnumerable<MovieCardResponseModel>> GetPurchasedMoviesByUserId(int id);
        Task<IEnumerable<MovieCardResponseModel>> GetFavoriteMoviesByUserId(int id);

        Task<UserDetailsResponseModel> GetUserDetailsByEmail(string email);

        Task<UserEditRequestModel> GetUserEditInformation(string email);

        Task<User> UpdateUserInformation(UserEditRequestModel requestmodel);
    }
}
