using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.Domain.Entities;

namespace ApplicationSystem.DataAccess
{
    /// <summary>
    /// Application database context.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContextOptions">Database context options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base (dbContextOptions)
        {
        }

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
