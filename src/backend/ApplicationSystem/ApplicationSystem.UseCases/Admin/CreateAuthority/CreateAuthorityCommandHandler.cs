using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ApplicationSystem.DataAccess;

namespace ApplicationSystem.UseCases.Admin.CreateAuthority
{
    /// <summary>
    /// Create authority command handler.
    /// </summary>
    internal class CreateAuthorityCommandHandler : IRequestHandler<CreateAuthorityCommand>
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="applicationDbContext">Application database context.</param>
        /// <param name="mapper">Mapper.</param>
        public CreateAuthorityCommandHandler(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper)
        {
            this.mapper = mapper;
            this.applicationDbContext = applicationDbContext;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(CreateAuthorityCommand request, CancellationToken cancellationToken)
        {
            var authority = mapper.Map<Domain.Entities.Authority>(request);
            applicationDbContext.Authorities.Add(authority);

            await applicationDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
