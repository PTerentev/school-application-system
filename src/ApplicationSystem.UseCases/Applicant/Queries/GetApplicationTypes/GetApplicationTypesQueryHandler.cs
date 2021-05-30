using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.UseCases.Applicant.Dtos;

namespace ApplicationSystem.UseCases.Applicant.Queries.GetApplicationTypes
{
    /// <summary>
    /// Handler for <see cref="GetApplicationTypesQuery"/>.
    /// </summary>
    internal class GetApplicationTypesQueryHandler : IRequestHandler<GetApplicationTypesQuery, IEnumerable<ApplicationTypeDto>>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">DB context.</param>
        /// <param name="mapper">Mapper.</param>
        public GetApplicationTypesQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ApplicationTypeDto>> Handle(GetApplicationTypesQuery request, CancellationToken cancellationToken)
        {
            return await mapper.ProjectTo<ApplicationTypeDto>(dbContext.ApplicationTypes).ToListAsync(cancellationToken);
        }
    }
}
