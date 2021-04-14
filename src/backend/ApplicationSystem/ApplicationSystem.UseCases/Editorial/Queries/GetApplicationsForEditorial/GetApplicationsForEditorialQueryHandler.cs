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

namespace ApplicationSystem.UseCases.Editorial.Queries.GetApplicationsForEditorial
{
    /// <summary>
    /// Get application query handler.
    /// </summary>
    internal class GetApplicationsForEditorialQueryHandler : IRequestHandler<GetApplicationsForEditorialQuery, IEnumerable<ApplicationDto>>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GetApplicationsForEditorialQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ApplicationDto>> Handle(GetApplicationsForEditorialQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Applications.Where(a => a.Status == ApplicationStatus.Check || a.Status == ApplicationStatus.Review);
            return await mapper.ProjectTo<ApplicationDto>(query).ToListAsync(cancellationToken);
        }
    }
}
