using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Domain.Entities;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.Infrastructure.Common.Application;

namespace ApplicationSystem.Infrastructure.UseCases.Editorial.SendToAuthority
{
    /// <summary>
    /// Send to authority command handler.
    /// </summary>
    internal class SendToAuthorityCommandHandler : IRequestHandler<SendToAuthorityCommand>
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SendToAuthorityCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(SendToAuthorityCommand request, CancellationToken cancellationToken)
        {
            if (!await dbContext.Authorities.AnyAsync(a => a.Id == request.AuthorityId))
            {
                throw new ValidationException("The authority Id does not exist.");
            }

            var application = await dbContext.Applications
                                .Where(a => a.Id == request.ApplicationId)
                                .Where(a => a.Status == ApplicationStatus.Check)
                                .Where(a => a.AuthorityId == null)
                                .SingleOrDefaultAsync(cancellationToken);

            if (application == null)
            {
                throw new ValidationException("Send to authority operation is not available.");
            }

            application.Status = ApplicationStatus.Sent;
            application.AuthorityId = request.AuthorityId;

            dbContext.Applications.Update(application);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
