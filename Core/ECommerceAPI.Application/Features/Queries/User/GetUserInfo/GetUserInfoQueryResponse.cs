namespace ECommerceAPI.Application.Features.Queries.User.GetUserInfo
{
    public class GetUserInfoQueryResponse
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public string? Address { get; set; }
    }
}