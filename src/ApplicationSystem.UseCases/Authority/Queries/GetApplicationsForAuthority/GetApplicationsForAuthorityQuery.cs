using System.Collections.Generic;
using MediatR;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.UseCases.Authority.Common;

namespace ApplicationSystem.UseCases.Authority.Queries.GetApplicationsForAuthority
{
    /// <summary>
    /// Get applications query.
    /// </summary>
    public class GetApplicationsForAuthorityQuery : AuthorityRequestBase, IRequest<IEnumerable<ApplicationDto>>
    {
    }
}
