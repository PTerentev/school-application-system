using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationSystem.Infrastructure.Common.Dtos;
using MediatR;

namespace ApplicationSystem.Infrastructure.UseCases.Authority.GetApplications
{
    /// <summary>
    /// Get applications query.
    /// </summary>
    public class GetApplicationsQuery : IRequest<ICollection<ApplicationDto>>
    {
        /// <summary>
        /// Authority Id.
        /// </summary>
        [Required]
        public int AuthorityId { get; set; }

        /// <summary>
        /// User Id.
        /// </summary>
        public int UserId { get; set; }
    }
}
