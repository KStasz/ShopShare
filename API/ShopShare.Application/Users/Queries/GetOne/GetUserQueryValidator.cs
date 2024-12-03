using FluentValidation;

namespace ShopShare.Application.Users.Queries.GetOne
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}
