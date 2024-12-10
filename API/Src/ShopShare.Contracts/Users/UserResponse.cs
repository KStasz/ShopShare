namespace ShopShare.Contracts.Users
{
    public record UserResponse(
        Guid id,
        string UserName,
        string Email,
        string FirstName,
        string LastName,
        DateTime CreationDate,
        IReadOnlyList<Guid> RoleIds);
}
