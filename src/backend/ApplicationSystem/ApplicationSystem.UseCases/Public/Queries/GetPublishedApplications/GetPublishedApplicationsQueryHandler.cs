using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Saritasa.Tools.Common.Pagination;
using Saritasa.Tools.EFCore.Pagination;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Common.Application;

namespace ApplicationSystem.UseCases.Public.GetPublishedApplications
{
    /// <summary>
    /// Get published applications query handler.
    /// </summary>
    internal class GetPublishedApplicationsQueryHandler : IRequestHandler<GetPublishedApplicationsQuery, PagedListMetadataDto<ApplicationDto>>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GetPublishedApplicationsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<PagedListMetadataDto<ApplicationDto>> Handle(GetPublishedApplicationsQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Applications
                .Where(a => a.Status == ApplicationStatus.Published)
                .OrderByDescending(a => a.CloseDate);

            var mappedQuery = mapper.ProjectTo<ApplicationDto>(query);

            var paged = await EFPagedListFactory
                .FromSourceAsync(
                mappedQuery,
                request.Page,
                request.PageSize,
                cancellationToken);

            return paged.ToMetadataObject();
        }
    }
}
