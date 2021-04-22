using MediatR;
using System.Collections.Generic;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.UseCases.Authority.Queries.GetApplicationsFromAllUsersAuthorities
{
    /// <summary>
    /// Get applications from all users authorities query.
    /// </summary>
    public class GetApplicationsFromAllUsersAuthoritiesQuery : IRequest<IEnumerable<ApplicationDto>>
    {
    }
}
