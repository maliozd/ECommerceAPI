﻿namespace ECommerceAPI.Application.Features.Commands.Role.UpdateRole
{
    public class UpdateRoleCommandResponse
    {
        public bool Succeeded { get; set; }
        public UpdateRoleCommandResponse(bool succeeded)
        {
            Succeeded = succeeded;
        }
    }
}