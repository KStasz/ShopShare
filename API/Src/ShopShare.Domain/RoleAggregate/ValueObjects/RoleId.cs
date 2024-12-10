using ShopShare.Domain.Models;

namespace ShopShare.Domain.RoleAggregate.ValueObjects
{
    public class RoleId : ValueObject
    {
        public Guid Value { get; }

        private RoleId(Guid value)
        {
            Value = value;
        }

        public static RoleId CreateUnique()
        {
            return new RoleId(Guid.NewGuid());
        }

        public static RoleId Create(Guid guid)
        {
            return new RoleId(guid);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
