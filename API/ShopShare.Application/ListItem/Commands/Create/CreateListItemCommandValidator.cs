using FluentValidation;

namespace ShopShare.Application.ListItem.Commands.Create
{
    public class CreateListItemCommandValidator : AbstractValidator<CreateListItemCommand>
    {
        public CreateListItemCommandValidator()
        {
        }
    }
}
