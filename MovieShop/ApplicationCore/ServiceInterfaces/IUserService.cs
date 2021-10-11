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
        

        Task<UserDetailsResponseModel> GetUserDetailsByEmail(string email);

        Task<UserDetailsResponseModel> GetUserDetailsById(int id);

        Task<UserEditRequestModel> GetUserEditInformation(string email);

        Task<User> UpdateUserInformation(UserEditRequestModel requestmodel);



        Task<bool> AddPurchase(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
        Task AddFavorite(FavoriteRequestModel favoriteRequest);

        Task<bool> FavoriteExists(int id, int movieId);

        Task RemoveFavorite(FavoriteRequestModel favoriteRequest);

        Task<IEnumerable<MovieCardResponseModel>> GetFavoriteMoviesByUserId(int id);


        Task AddMovieReview(ReviewRequestModel reviewRequest);
        Task UpdateMovieReview(ReviewRequestModel reviewRequest);

        Task DeleteMovieReview(int userId, int movieId);

        Task<IEnumerable<ReviewResponseModel>> GetReviewsByUserId(int id);

    }
}
