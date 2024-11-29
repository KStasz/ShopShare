using MediatR;
using ShopShare.Application.Authentication.Models;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.Authentication.Query
{
    public record LoginQuery(
        string Email,
        string Password) : IRequest<Result<AuthenticationResult>>;
}
