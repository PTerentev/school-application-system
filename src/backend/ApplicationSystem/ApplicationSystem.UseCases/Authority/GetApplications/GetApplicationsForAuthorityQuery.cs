using System.Collections.Generic;
using MediatR;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.UseCases.Authority.Validation;

namespace ApplicationSystem.UseCases.Authority.GetApplications
{
    /// <summary>
    /// Get applications query.
    /// </summary>
    [AuthorityValidation]
    public class GetApplicationsForAuthorityQuery : AuthorityRequestBase, IRequest<ICollection<ApplicationDto>>
    {
    }
}
