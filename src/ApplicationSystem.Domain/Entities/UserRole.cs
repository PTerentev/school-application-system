using Microsoft.AspNetCore.Identity;

namespace ApplicationSystem.Domain.Entities
{
    /// <summary>
    /// Junction between user and role.
    /// </summary>
    public class UserRole : IdentityUserRole<int>
    {
        /// <summary>
        /// Application user.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Application role.
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
