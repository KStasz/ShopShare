namespace ShopShare.Contracts.ShoppingListContracts
{
    public record ShoppingListResponse(
        Guid Id,
        string Name,
        string Description,
        DateTime CreationDate,
        DateTime UpdatedDate,
        List<ListItemResponse> ListItems);
}
