namespace ShopShare.Contracts.Users
{
    public record AddUserToRoleRequest(
        Guid UserId, 
        Guid RoleId);
}
