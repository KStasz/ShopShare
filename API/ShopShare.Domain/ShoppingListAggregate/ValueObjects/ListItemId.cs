using ShopShare.Domain.Models;

namespace ShopShare.Domain.ShoppingListAggregate.ValueObjects
{
    public class ListItemId : ValueObject
    {
        public Guid Value { get; }

        private ListItemId(Guid value)
        {
            Value = value;
        }

        public static ListItemId Create(Guid id)
        {
            return new(id);
        }

        public static ListItemId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
