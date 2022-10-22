using ECommerceAPI.Application.Abstraction.Services.Authentication;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IAuthenticationService _authService;

        public GoogleLoginCommandHandler(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.GoogleLoginAsync(request.IdToken, request.Provider, 20);
            return new()
            {
                Token = token,
            };
        }
    }
}
