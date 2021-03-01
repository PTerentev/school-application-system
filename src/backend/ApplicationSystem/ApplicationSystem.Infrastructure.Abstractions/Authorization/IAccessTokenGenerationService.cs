﻿using System.Collections.Generic;
using System.Security.Claims;

namespace ApplicationSystem.Infrastructure.Abstractions.Authorization
{
    /// <summary>
    /// Access token generation service.
    /// </summary>
    public interface IAccessTokenGenerationService
    {
        /// <summary>
        /// Generate access token.
        /// </summary>
        string GenerateToken(IEnumerable<Claim> claims);

        /// <summary>
        /// Get token claims.
        /// </summary>
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}
