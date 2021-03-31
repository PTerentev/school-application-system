using Microsoft.AspNetCore.Identity;
using System;

namespace ApplicationSystem.Domain.Entities
{
    /// <summary>
    /// Role.
    /// </summary>
    public class Role : IdentityRole<int>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Role()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="roleName">Role name.</param>
        public Role(string roleName) : base(roleName)
        {
        }

        /// <summary>
        /// Get role represented by <see cref="UserRoles"/>.
        /// </summary>
        public UserRoles GetEnum() => Enum.Parse<UserRoles>(Name);
    }

    /// <summary>
    /// User roles.
    /// </summary>
    public enum UserRoles
    {
        Editor = 1,
        Admin = 2
    }
}
