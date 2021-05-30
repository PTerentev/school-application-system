using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.DataAccess;

namespace ApplicationSystem.UseCases.Admin.AddUserToRole
{
    /// <summary>
    /// Add user to role command handler.
    /// </summary>
    internal class AddUserToRoleCommandHandler : IRequestHandler<AddUserToRoleCommand>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<Domain.Entities.User> userManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AddUserToRoleCommandHandler(UserManager<Domain.Entities.User> userManager, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.SingleAsync(u => u.Id == request.UserId, cancellationToken);
            var role = await dbContext.Roles.SingleAsync(r => r.Id == request.RoleId, cancellationToken);
            var result = await userManager.AddToRoleAsync(user, role.NormalizedName);

            if (!result.Succeeded)
            {
                throw new DomainException("Role data is invalid.");
            }

            return Unit.Value;
        }
    }
}
