using MediatR;
using System.Collections.Generic;
using ApplicationSystem.UseCases.Authority.Dtos;

namespace ApplicationSystem.UseCases.Authority.Queries.GetAllAuthoritiesQuery
{
    /// <summary>
    /// Get authorities query.
    /// </summary>
    public class GetAllAuthoritiesQuery : IRequest<IEnumerable<AuthorityDto>>
    {
    }
}
