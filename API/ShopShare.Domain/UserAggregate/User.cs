using ShopShare.Domain.Models;
using ShopShare.Domain.RoleAggregate.ValueObjects;
using ShopShare.Domain.Snapshots;
using ShopShare.Domain.UserAggregate.ValueObjects;

namespace ShopShare.Domain.UserAggregate
{
    public class User : AggregateRoot<UserId>
    {
        private readonly List<RoleId> _userRoles = new();

        public string UserName { get; private set; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime CreationDate { get; }

        public IReadOnlyList<RoleId> UserRoles => _userRoles.AsReadOnly();

        private User(
            UserId id,
            string userName,
            string email,
            string firstName,
            string lastName,
            DateTime creationDate) : base(id)
        {
            UserName = userName;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            CreationDate = creationDate;
        }

        public static User Create(
            string userName,
            string email,
            string firstName,
            string lastName)
        {
            return new(
                UserId.CreateUnique(),
                userName,
                email,
                firstName,
                lastName,
                DateTime.Now);
        }

        public static User Create(
            UserId id,
            string userName,
            string email,
            string firstName,
            string lastName,
            DateTime creationDate)
        {
            return new(
                id,
                userName,
                email,
                firstName,
                lastName,
                creationDate);
        }

        public void ChangeUserName(string newUserName)
        {
            UserName = newUserName;
        }

        public void AddRole(RoleId roleId)
        {
            _userRoles.Add(roleId);
        }

        public void DeleteRole(RoleId roleId)
        {
            _userRoles.Remove(roleId);
        }

        public void SetRoles(IEnumerable<RoleId> roleIds)
        {
            _userRoles.Clear();
            _userRoles.AddRange(roleIds);
        }

        public UserSnapshot ToSnapshot()
        {
            return new UserSnapshot(
                Id.Value,
                UserName,
                Email,
                FirstName,
                LastName,
                CreationDate,
                UserRoles.Select(x => x.Value));
        }

        public static User FromShapshot(UserSnapshot snapshot)
        {
            var user = new User(
                UserId.Create(snapshot.Id),
                snapshot.UserName,
                snapshot.Email,
                snapshot.FirstName,
                snapshot.LastName,
                snapshot.CreationDate);

            user.SetRoles(
                snapshot.UserRoles.Select(
                    RoleId.Create));

            return user;
        }
    }
}
