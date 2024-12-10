using FluentValidation;

namespace ShopShare.Application.Users.Queries.GetAll
{
    public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersQueryValidator()
        {
        }
    }
}
