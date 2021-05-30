using MediatR;
using System.Collections.Generic;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.UseCases.Admin.Queries.GetRoles
{
    /// <summary>
    /// Get users query.
    /// </summary>
    public class GetRolesQuery : IRequest<IEnumerable<RoleDto>>
    {
    }
}
