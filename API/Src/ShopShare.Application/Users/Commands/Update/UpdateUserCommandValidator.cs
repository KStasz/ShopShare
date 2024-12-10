using FluentValidation;

namespace ShopShare.Application.Users.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.User.Email).EmailAddress();
            RuleFor(x => x.User.FirstName).NotEmpty();
            RuleFor(x => x.User.LastName).NotEmpty();
            RuleFor(x => x.User.UserName).NotEmpty();
        }
    }
}
