namespace ShopShare.Contracts.ShoppingListContracts
{
    public record UpdateShoppingListRequest(
        Guid Id,
        string ListName,
        string Description);
}
