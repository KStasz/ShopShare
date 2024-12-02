using FluentValidation;

namespace ShopShare.Application.Roles.Commands.Update
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.Role.Name).NotEmpty();
        }
    }
}
