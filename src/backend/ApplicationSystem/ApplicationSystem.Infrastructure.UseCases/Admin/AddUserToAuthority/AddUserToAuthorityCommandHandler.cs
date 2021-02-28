using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.DataAccess;

namespace ApplicationSystem.Infrastructure.UseCases.Admin.AddUserToAuthority
{
    /// <summary>
    /// Add user to authority command handler.
    /// </summary>
    internal class AddUserToAuthorityCommandHandler : IRequestHandler<AddUserToAuthorityCommand>
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AddUserToAuthorityCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(AddUserToAuthorityCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.SingleAsync(u => u.Id == request.UserId, cancellationToken);
            var authority = await dbContext.Authorities.SingleAsync(u => u.Id == request.AuthorityId, cancellationToken);
            
            if (authority.Users.Any(u => u.Id == user.Id))
            {
                throw new DomainException("The user was already added to the authority");
            }

            authority.Users.Add(user);

            dbContext.Update(authority);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
