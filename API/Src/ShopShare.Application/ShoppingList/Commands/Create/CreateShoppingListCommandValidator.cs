using FluentValidation;

namespace ShopShare.Application.ShoppingList.Commands.Create
{
    public class CreateShoppingListCommandValidator : AbstractValidator<CreateShoppingListCommand>
    {
        public CreateShoppingListCommandValidator()
        {
            RuleFor(x => x.ListName)
                .NotEmpty()
                .MaximumLength(32);
        }
    }
}
