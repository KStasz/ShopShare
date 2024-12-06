using FluentValidation;

namespace ShopShare.Application.ListItem.Queries
{
    public class GetListItemsQueryValidator : AbstractValidator<GetListItemsQuery>
    {
        public GetListItemsQueryValidator()
        {
        }
    }
}
