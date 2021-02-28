using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Domain.Entities;

namespace ApplicationSystem.Infrastructure.UseCases.Admin.AddUserToRole
{
    /// <summary>
    /// Add user to role command handler.
    /// </summary>
    internal class AddUserToRoleCommandHandler : IRequestHandler<AddUserToRoleCommand>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AddUserToRoleCommandHandler(UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.SingleAsync(u => u.Id == request.UserId, cancellationToken);
            await userManager.AddToRoleAsync(user, request.Role);

            return Unit.Value;
        }
    }
}
