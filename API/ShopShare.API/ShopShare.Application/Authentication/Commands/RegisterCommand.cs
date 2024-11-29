using MediatR;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.Authentication.Commands
{
    public record RegisterCommand(
        string UserName,
        string Email,
        string FirstName,
        string LastName,
        string Password) : IRequest<Result>;
}
