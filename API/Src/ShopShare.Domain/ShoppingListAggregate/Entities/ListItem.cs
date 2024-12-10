using ShopShare.Domain.Models;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;

namespace ShopShare.Domain.ShoppingListAggregate.Entities
{
    public class ListItem : Entity<ListItemId>
    {
        public string ItemName { get; private set; } = null!;
        public DateTime CreationDate { get; private set; }

        private ListItem(
            ListItemId id,
            string itemNamd) : base(id)
        {
            ItemName = itemNamd;
            CreationDate = DateTime.Now;
        }

        public static ListItem CreateNew(string itemName)
        {
            return new(
                ListItemId.CreateUnique(),
                itemName);
        }

#pragma warning disable CS8618
        private ListItem()
        {
        }
#pragma warning restore CS8618
    }
}
