using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationSystem.Infrastructure.UseCases.Editorial.PublishApplication
{
    /// <summary>
    /// Publish application command handler.
    /// </summary>
    internal class PublishApplicationCommandHandler : IRequestHandler<PublishApplicationCommand>
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PublishApplicationCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(PublishApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await dbContext.Applications
                                .Where(a => a.Id == request.ApplicationId)
                                .Where(a => a.Status == ApplicationStatus.Review)
                                .SingleAsync(cancellationToken);

            application.Status = ApplicationStatus.Published;
            application.CloseDate = DateTime.UtcNow;

            dbContext.Applications.Update(application);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
