using System;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ApplicationSystem.Infrastructure.Common.Options;
using ApplicationSystem.Infrastructure.Abstractions.Authorization;

namespace ApplicationSystem.Infrastructure.Authorization
{
    /// <inheritdoc cref="IAccessTokenGenerationService"/>
    public class AccessTokenGenerationService : IAccessTokenGenerationService
    {
        private readonly JwtOptions jwtOptions;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="jwtOptions">JWT options.</param>
        public AccessTokenGenerationService(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }

        /// <inheritdoc/>
        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.MinValue,
                expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(jwtOptions.ExpirationTimeSeconds)),
                audience: jwtOptions.Audience,
                issuer: jwtOptions.Issuer,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(jwtOptions.SecretKey)), 
                        SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
