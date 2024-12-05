namespace ShopShare.Contracts.ShoppingListContracts
{
    public record CreateShoppingListRequest(
        string ListName,
        string Description);
}
