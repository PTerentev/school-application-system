using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using AutoMapper;
using MediatR;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Common.Application;

namespace ApplicationSystem.Infrastructure.UseCases.Authority.GetApplications
{
    /// <summary>
    /// Get applications query handler.
    /// </summary>
    internal class GetApplicationsForAuthorityQueryHandler : IRequestHandler<GetApplicationsForAuthorityQuery, ICollection<ApplicationDto>>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GetApplicationsForAuthorityQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<ICollection<ApplicationDto>> Handle(GetApplicationsForAuthorityQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Applications
                .Where(a => a.AuthorityId == request.AuthorityId)
                .Where(a => a.Status == ApplicationStatus.Sent);

            return await mapper.ProjectTo<ApplicationDto>(query).ToListAsync(cancellationToken);
        }
    }
}
