using MediatR;
using System.Collections.Generic;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.UseCases.Admin.Queries.GetUsers
{
    /// <summary>
    /// Get users query.
    /// </summary>
    public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }
}
