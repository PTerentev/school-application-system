using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ApplicationSystem.Domain.Entities;

namespace ApplicationSystem.Web.Infrastructure.Authorization
{
    /// <summary>
    /// Role based authorize.
    /// </summary>
    public class RoleBasedAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="roles">List of allowed roles.</param>
        public RoleBasedAuthorizeAttribute(params UserRoles[] roles)
        {
            if (roles != null)
            {
                Roles = string.Join(',', roles.Select(_ => _.ToString()));
            }
        }
    }
}
