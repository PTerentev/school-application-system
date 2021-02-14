using Microsoft.AspNetCore.Identity;

namespace ApplicationSystem.Domain.Entities
{
    /// <summary>
    /// Role.
    /// </summary>
    public class Role : IdentityRole<int>
    {
        /// <summary>
        /// Admin role.
        /// </summary>
        public const string AdminRole = "Admin";

        /// <summary>
        /// Editor role.
        /// </summary>
        public const string EditorRole = "Editor";
    }
}
