using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Application;

namespace ApplicationSystem.Infrastructure.UseCases.Editorial.RejectApplication
{
    /// <summary>
    /// Reject application command handler.
    /// </summary>
    internal class RejectApplicationCommandHandler : IRequest<RejectApplicationCommand>
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RejectApplicationCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(RejectApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await dbContext.Applications
                                .Where(a => a.Id == request.ApplicationId)
                                .Where(a => a.Status == ApplicationStatus.Check)
                                .SingleAsync(cancellationToken);

            application.Status = ApplicationStatus.Rejected;
            application.RejectComments = request.RejectComments;
            application.CloseDate = DateTime.UtcNow;

            dbContext.Applications.Update(application);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
