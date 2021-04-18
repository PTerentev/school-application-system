using System.Threading;
using System.Threading.Tasks;
using ApplicationSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationSystem.DataAccess.Services
{
    /// <summary>
    /// Custom application user store.
    /// </summary>
    internal class ApplicationUserStore : UserStore<
        User,
        Role,
        ApplicationDbContext,
        int,
        IdentityUserClaim<int>,
        UserRole,
        IdentityUserLogin<int>,
        IdentityUserToken<int>,
        IdentityRoleClaim<int>>
    {
        /// <inheritdoc />
        public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        /// <inheritdoc />
        public override Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return Users.FirstOrDefaultAsync(u => u.Id == int.Parse(userId), cancellationToken);
        }

        /// <inheritdoc />
        public override Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken);
        }
    }
}
