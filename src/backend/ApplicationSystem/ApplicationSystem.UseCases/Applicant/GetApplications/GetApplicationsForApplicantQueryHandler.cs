using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Abstractions.Authorization;

namespace ApplicationSystem.UseCases.Applicant.GetApplications
{
    /// <summary>
    /// Get applications query handler.
    /// </summary>
    internal class GetApplicationsForApplicantQueryHandler : IRequestHandler<GetApplicationsForApplicantQuery, ICollection<ApplicationInfoDto>>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IAccessUserPrincipalService accessUserPrincipalService;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GetApplicationsForApplicantQueryHandler(ApplicationDbContext dbContext, IMapper mapper, IAccessUserPrincipalService accessUserPrincipalService)
        {
            this.accessUserPrincipalService = accessUserPrincipalService;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<ICollection<ApplicationInfoDto>> Handle(GetApplicationsForApplicantQuery request, CancellationToken cancellationToken)
        {
            var userId = accessUserPrincipalService.GetUserId();
            var query = dbContext.Applications.Where(a => a.CreatorUserId != null && a.CreatorUserId == userId);
            return await mapper.ProjectTo<ApplicationInfoDto>(query).ToListAsync(cancellationToken);
        }
    }
}
