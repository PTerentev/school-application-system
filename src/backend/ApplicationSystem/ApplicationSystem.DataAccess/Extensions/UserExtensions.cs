using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ApplicationSystem.Domain.Entities;

namespace ApplicationSystem.DataAccess.Extensions
{
    /// <summary>
    /// User extensions.
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Check if the user has any of specified roles.
        /// </summary>
        /// <typeparam name="TUser">User class type.</typeparam>
        /// <param name="userManager">User manager.</param>
        /// <param name="userId">User Id.</param>
        /// <param name="userRoles">User roles.</param>
        public static async Task<bool> HasAnyRole<TUser>(this UserManager<TUser> userManager, int userId, params UserRoles[] userRoles) where TUser : class
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user != null)
            {
                foreach (var role in userRoles.Select(r => r.ToString()))
                {
                    if (await userManager.IsInRoleAsync(user, role))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
