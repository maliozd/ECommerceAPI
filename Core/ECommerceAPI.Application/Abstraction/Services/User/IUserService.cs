using ECommerceAPI.Application.Dtos.User;
using ECommerceAPI.Application.Features.Queries.User.GetUserInfo;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstraction.Services.User
{
    public interface IUserService
    {
        Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO createUserDTO);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenExpirationDate, int addMinuteOnAccessToken);
        Task<GetUserInfoQueryResponse> GetUserInfoAsync(string userName);
    }
}
