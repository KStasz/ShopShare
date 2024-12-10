using FluentValidation;

namespace ShopShare.Application.Users.Commands.AddUserToRole
{
    public class AddUserToRoleCommandValidator : AbstractValidator<AddUserToRoleCommand>
    {
        public AddUserToRoleCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.RoleId).NotNull();
        }
    }
}
