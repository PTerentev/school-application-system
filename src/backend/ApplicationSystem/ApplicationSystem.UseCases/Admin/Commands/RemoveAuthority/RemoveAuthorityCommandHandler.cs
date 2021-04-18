using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.DataAccess;

namespace ApplicationSystem.UseCases.Admin.RemoveAuthority
{
    /// <summary>
    /// Remove authority command handler.
    /// </summary>
    internal class RemoveAuthorityCommandHandler : IRequestHandler<RemoveAuthorityCommand>
    {
        private readonly ApplicationDbContext applicationDbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="applicationDbContext">Application database context.</param>
        public RemoveAuthorityCommandHandler(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(RemoveAuthorityCommand request, CancellationToken cancellationToken)
        {
            var authority = await applicationDbContext.Authorities.SingleOrDefaultAsync(a => a.Id == request.AuthorityId, cancellationToken);

            if (authority == null)
            {
                throw new NotFoundException("The authority are not found.");
            }

            applicationDbContext.Authorities.Remove(authority);

            await applicationDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}