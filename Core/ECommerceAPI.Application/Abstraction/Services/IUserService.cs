using ECommerceAPI.Application.Dtos.User;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO createUserDTO);
        Task UpdateRefreshTokenAsync(string refreshToken,AppUser user, DateTime accessTokenExpirationDate, int addMinuteOnAccessToken);
    }
}
