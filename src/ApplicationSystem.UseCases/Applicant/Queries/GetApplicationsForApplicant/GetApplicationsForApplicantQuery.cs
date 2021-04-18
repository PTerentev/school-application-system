using System.Collections.Generic;
using MediatR;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.UseCases.Applicant.Queries.GetApplicationsForApplicant
{
    /// <summary>
    /// Get applications by applicant query.
    /// </summary>
    public class GetApplicationsForApplicantQuery : IRequest<IEnumerable<ApplicationInfoDto>>
    {
    }
}
