using MediatR;
using Saritasa.Tools.Common.Pagination;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.UseCases.Public.GetPublishedApplications
{
    /// <summary>
    /// Get published applications query.
    /// </summary>
    public class GetPublishedApplicationsQuery : IRequest<PagedListMetadataDto<ApplicationDto>>
    {
        /// <summary>
        /// Page number.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize { get; set; } = 1000;
    }
}
