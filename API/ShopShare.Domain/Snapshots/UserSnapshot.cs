namespace ShopShare.Domain.Snapshots
{
    public record UserSnapshot(
        Guid Id,
        string UserName,
        string Email,
        string FirstName,
        string LastName,
        DateTime CreationDate,
        IEnumerable<Guid> UserRoles);
}
