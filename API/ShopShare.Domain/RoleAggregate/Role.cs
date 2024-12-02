using ShopShare.Domain.Models;
using ShopShare.Domain.RoleAggregate.ValueObjects;

namespace ShopShare.Domain.RoleAggregate
{
    public class Role : AggregateRoot<RoleId>
    {
        public string Name { get; private set; }

        private Role(
            RoleId id,
            string name)
            : base(id)
        {
            Name = name;
        }

        public static Role Create(string name)
        {
            return new Role(
                RoleId.CreateUnique(),
                name);
        }

        public static Role Create(
            RoleId id,
            string name)
        {
            return new Role(
                id,
                name);
        }
    }
}
