namespace ECommerceAPI.Application.Features.Queries.Role.GetRoleById
{
    public class GetRoleByIdQueryResponse
    {
        public GetRoleByIdQueryResponse(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}