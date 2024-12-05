using ShopShare.Domain.Models;

namespace ShopShare.Domain.ShoppingListAggregate.ValueObjects
{
    public class ShoppingListId : ValueObject
    {
        public Guid Value { get; }

        private ShoppingListId(Guid value)
        {
            Value = value;
        }

        public static ShoppingListId Create(Guid id)
        {
            return new(id);
        }

        public static ShoppingListId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
