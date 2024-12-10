namespace ShopShare.Contracts.ListITemContracts
{
    public record CreateListItemRequest(
        Guid ShoppingListId,
        string ItemName);
}
