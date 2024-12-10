using FluentValidation;

namespace ShopShare.Application.Roles.Commands.Delete
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}
