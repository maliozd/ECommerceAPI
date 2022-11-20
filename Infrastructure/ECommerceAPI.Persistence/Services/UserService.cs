using ECommerceAPI.Application.Abstraction.Services.User;
using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Application.Dtos.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Queries.User.GetUserInfo;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO createUserDTO)
        {
            IdentityResult result = await _userManager.CreateAsync(new() //appUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = createUserDTO.Username,
                Email = createUserDTO.Email,
                NameSurname = createUserDTO.NameSurname

            }, createUserDTO.Password);
            CreateUserResponseDTO response = new()
            {
                Success = result.Succeeded
            };
            if (result.Succeeded)
                response.Message = "New user successfully created.";
            else
            {
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code}- {error.Description}\n";
            }
            return response;
        }

        public async Task<GetUserInfoQueryResponse> GetUserInfoAsync(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new NotFoundUserException();
            }
            return new()
            {
                Address = user.Address,
                Email = user.Email,
                IsEmailConfirmed = user.EmailConfirmed,
                IsPhoneNumberConfirmed = user.PhoneNumberConfirmed,
                IsTwoFactorEnabled = user.TwoFactorEnabled,
                NameSurname = user.NameSurname,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName,
            };
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenExpirationDate, int addMinuteOnAccessToken)
        {
            if (user == null)
            {
                throw new NotFoundUserException();
            }
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireDate = accessTokenExpirationDate.AddMinutes(addMinuteOnAccessToken);
            await _userManager.UpdateAsync(user);
        }
    }
}
