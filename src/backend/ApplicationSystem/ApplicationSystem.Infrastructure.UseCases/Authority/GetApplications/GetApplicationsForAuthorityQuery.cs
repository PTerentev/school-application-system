using System.Collections.Generic;
using MediatR;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.UseCases.Authority.Validation;

namespace ApplicationSystem.Infrastructure.UseCases.Authority.GetApplications
{
    /// <summary>
    /// Get applications query.
    /// </summary>
    [AuthorityValidation]
    public class GetApplicationsForAuthorityQuery : AuthorityRequestBase, IRequest<ICollection<ApplicationDto>>
    {
    }
}
