namespace ShopShare.Contracts.ListITemContracts
{
    public record DeleteListItemRequest(
        Guid ShoppingListId,
        Guid ListItemId);
}
