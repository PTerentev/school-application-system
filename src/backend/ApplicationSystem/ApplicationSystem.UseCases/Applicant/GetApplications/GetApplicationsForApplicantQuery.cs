using System.Collections.Generic;
using MediatR;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.UseCases.Applicant.GetApplications
{
    /// <summary>
    /// Get applications by applicant query.
    /// </summary>
    public class GetApplicationsForApplicantQuery : IRequest<ICollection<ApplicationInfoDto>>
    {
    }
}
