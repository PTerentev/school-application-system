using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Domain.Entities;

namespace ApplicationSystem.UseCases.Admin.Commands.AddApplicationType
{
    /// <summary>
    /// Handler for <see cref="AddApplicationTypeCommand"/>.
    /// </summary>
    internal class AddApplicationTypeCommandHandler : IRequestHandler<AddApplicationTypeCommand>
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">DB context.</param>
        public AddApplicationTypeCommandHandler(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(AddApplicationTypeCommand request, CancellationToken cancellationToken)
        {
            if (!await dbContext.Authorities.AnyAsync(a => a.Id == request.AuthorityId, cancellationToken))
            {
                var applicationType = new ApplicationType()
                {
                    Name = request.Name,
                    AuthorityId = request.AuthorityId
                };

                dbContext.ApplicationTypes.Add(applicationType);
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
