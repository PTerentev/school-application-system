using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.Infrastructure.UseCases.Applicant.GetApplications
{
    /// <summary>
    /// Get applications query handler.
    /// </summary>
    internal class GetApplicationsQueryHandler : IRequestHandler<GetApplicationsQuery, ICollection<ApplicationInfoDto>>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GetApplicationsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<ICollection<ApplicationInfoDto>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Applications.Where(a => a.CreatorUserId != null && a.CreatorUserId == request.UserId);
            return await mapper.ProjectTo<ApplicationInfoDto>(query).ToListAsync(cancellationToken);
        }
    }
}
