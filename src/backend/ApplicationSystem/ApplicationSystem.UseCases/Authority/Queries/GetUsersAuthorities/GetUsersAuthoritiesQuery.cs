using MediatR;
using System.Collections.Generic;
using ApplicationSystem.UseCases.Authority.Dtos;

namespace ApplicationSystem.UseCases.Authority.Queries.GetUsersAuthorities
{
    /// <summary>
    /// Get authorities query.
    /// </summary>
    public class GetUsersAuthoritiesQuery : IRequest<IEnumerable<AuthorityDto>>
    {
    }
}
