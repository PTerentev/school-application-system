using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Common.Application;

namespace ApplicationSystem.Infrastructure.UseCases.Editorial.GetApplications
{
    /// <summary>
    /// Get application query handler.
    /// </summary>
    internal class GetApplicationsQueryHandler : IRequestHandler<GetApplicationsQuery, ICollection<ApplicationDto>>
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
        public async Task<ICollection<ApplicationDto>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Applications.Where(a => a.Status == ApplicationStatus.Check || a.Status == ApplicationStatus.Review);
            return await mapper.ProjectTo<ApplicationDto>(query).ToListAsync(cancellationToken);
        }
    }
}
