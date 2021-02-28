using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Domain.Entities;
using AutoMapper;
using System.Collections.Generic;
using ApplicationSystem.Infrastructure.Common.Dtos;
using Saritasa.Tools.Domain.Exceptions;
using MediatR;
using ApplicationSystem.Infrastructure.Common.Application;

namespace ApplicationSystem.Infrastructure.UseCases.Authority.GetApplications
{
    /// <summary>
    /// Get applications query handler.
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
            var authority = await dbContext.Authorities.Where(a => a.Id == request.AuthorityId).SingleAsync(cancellationToken);
            if (!authority.Users.Any(u => u.Id == request.UserId))
            {
                throw new ValidationException("The user does not have access to the authority.");
            }

            var query = dbContext.Applications
                .Where(a => a.AuthorityId == request.AuthorityId)
                .Where(a => a.Status == ApplicationStatus.Sent);

            return await mapper.ProjectTo<ApplicationDto>(query).ToListAsync(cancellationToken);
        }
    }
}
