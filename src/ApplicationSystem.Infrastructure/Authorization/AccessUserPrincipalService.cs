using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ApplicationSystem.Infrastructure.Abstractions.Authorization;

namespace ApplicationSystem.Infrastructure.Authorization
{
    /// <inheritdoc cref="IAccessUserPrincipalService"/>
    public class AccessUserPrincipalService : IAccessUserPrincipalService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="httpContextAccessor">HTTP context accessor.</param>
        public AccessUserPrincipalService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public int? GetUserId()
        {
            var value = httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?
                .Value;

            return value == null ? null : Convert.ToInt32(value);
        }
    }
}
