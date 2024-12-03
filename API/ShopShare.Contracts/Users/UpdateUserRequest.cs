namespace ShopShare.Contracts.Users
{
    public record UpdateUserRequest(
        Guid Id,
        string UserName,
        string Email,
        string FirstName,
        string LastName,
        DateTime CreationDate,
        List<Guid> RoleIds);
}
