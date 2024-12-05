namespace ShopShare.Contracts.ShoppingListContracts
{
    public record ListItemResponse(
        Guid Id,
        string Name,
        DateTime CreationDate);
}
