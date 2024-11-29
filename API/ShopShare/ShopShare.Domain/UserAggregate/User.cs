using ShopShare.Domain.Models;
using ShopShare.Domain.UserAggregate.ValueObjects;

namespace ShopShare.Domain.UserAggregate
{
    public class User : AggregateRoot<UserId>
    {
        public string UserName { get; private set; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime CreationDate { get; }

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
    }
}
