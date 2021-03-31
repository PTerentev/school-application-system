using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ApplicationSystem.Domain.Entities;

namespace ApplicationSystem.DataAccess
{
    /// <summary>
    /// Application database context.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<
        User,
        Role,
        int,
        IdentityUserClaim<int>,
        UserRole,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContextOptions">Database context options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base (dbContextOptions)
        {
        }

        /// <summary>
        /// User roles.
        /// </summary>
        public override DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Applications.
        /// </summary>
        public DbSet<Application> Applications { get; protected set; }

        /// <summary>
        /// Attachments.
        /// </summary>
        public DbSet<Attachment> Attachments { get; protected set; }

        /// <summary>
        /// Authorities.
        /// </summary>
        public DbSet<Authority> Authorities { get; protected set; }

        /// <summary>
        /// Notifications.
        /// </summary>
        public DbSet<Notification> Notifications { get; protected set; }

        /// <summary>
        /// Replies.
        /// </summary>
        public DbSet<Reply> Replies { get; protected set; }
    }
}
