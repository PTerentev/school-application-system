using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Domain.Entities;

namespace ApplicationSystem.Infrastructure.UseCases.Editorial.ResendToAuthority
{
    /// <summary>
    /// Resend to authority command handler.
    /// </summary>
    internal class ResendToAuthorityCommandHandler : IRequestHandler<ResendToAuthorityCommand>
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResendToAuthorityCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(ResendToAuthorityCommand request, CancellationToken cancellationToken)
        {
            var application = await dbContext.Applications
                                .Where(a => a.Id == request.ApplicationId)
                                .Where(a => a.Status == ApplicationStatus.Review)
                                .SingleOrDefaultAsync(cancellationToken);

            if (application == null)
            {
                throw new ValidationException("Send to authority operation is not available.");
            }

            application.Status = ApplicationStatus.Sent;
            application.Reply.EditComments = request.EditComments;

            dbContext.Applications.Update(application);
            dbContext.Replies.Update(application.Reply);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
