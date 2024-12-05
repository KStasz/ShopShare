using ShopShare.Domain.Models;
using ShopShare.Domain.ShoppingListAggregate.Entities;
using ShopShare.Domain.ShoppingListAggregate.ValueObjects;
using System.Net;

namespace ShopShare.Domain.ShoppingListAggregate
{
    public class ShoppingList : AggregateRoot<ShoppingListId>
    {
        private readonly List<ListItem> _items = new();

        public string ListName { get; private set; }
        public string Description { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }

        public IReadOnlyList<ListItem> Items => _items.AsReadOnly();

        private ShoppingList(
            ShoppingListId id,
            string listName,
            string description) : base(id)
        {
            ListName = listName;
            Description = description;
            CreationDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public static ShoppingList CreateNew(
            string listName,
            string description)
        {
            return new ShoppingList(
                ShoppingListId.CreateUnique(),
                listName,
                description);
        }

        public void AddItem(ListItem item)
        {
            _items.Add(item);
            UpdatedDate = DateTime.Now;
        }

        public void RemoveItem(ListItem item)
        {
            _items.Remove(item);
            UpdatedDate = DateTime.Now;
        }

        public void ChangeName(string newName)
        {
            ListName = newName;
            UpdatedDate = DateTime.Now;
        }

        public void ChangeDescription(string newDescription)
        {
            Description = newDescription;
            UpdatedDate = DateTime.Now;
        }

#pragma warning disable CS8618
        private ShoppingList()
        {
        }
#pragma warning restore CS8618
    }
}
