using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Application.Dtos.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenExpirationDate,int addSecondOnAccessToken)
        {
            if (user == null)
            {
                throw new NotFoundUserException();
            }
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireDate = accessTokenExpirationDate.AddSeconds(addSecondOnAccessToken);
            await _userManager.UpdateAsync(user);
        }
    }
}
