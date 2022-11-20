using ECommerceAPI.Application.Abstraction.Services.User;
using ECommerceAPI.Application.Dtos.User;
using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserDTO dto = new()
            {
                NameSurname = request.NameSurname,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
            };
            var response = await userService.CreateAsync(dto);
            return new()
            {
                Message = response.Message,
                Success = response.Success,
            };
        }
    }
}
