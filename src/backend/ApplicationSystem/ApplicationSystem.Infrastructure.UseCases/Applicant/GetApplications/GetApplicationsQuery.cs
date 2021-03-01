using System.Collections.Generic;
using MediatR;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.Infrastructure.UseCases.Applicant.GetApplications
{
    /// <summary>
    /// Get applications by applicant query.
    /// </summary>
    public class GetApplicationsQuery : IRequest<ICollection<ApplicationInfoDto>>
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public int UserId { get; set; }
    }
}
