using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Abstractions.Authorization;

namespace ApplicationSystem.UseCases.User.Login
{
    /// <summary>
    /// Login user command handler.
    /// </summary>
    internal class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserQueryResult>
    {
        private readonly SignInManager<Domain.Entities.User> signInManager;
        private readonly UserManager<Domain.Entities.User> userManager;
        private readonly IAccessTokenGenerationService accessTokenGenerationService;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoginUserQueryHandler(
            SignInManager<Domain.Entities.User> signInManager,
            UserManager<Domain.Entities.User> userManager,
            IAccessTokenGenerationService accessTokenGenerationService,
            IMapper mapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.accessTokenGenerationService = accessTokenGenerationService;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<LoginUserQueryResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            var result = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
            if (!result.Succeeded)
            {
                throw new DomainException("Login denied for the user.");
            }

            var principal = await signInManager.CreateUserPrincipalAsync(user);
            var token = GenerateToken(principal.Claims.ToList());

            return new LoginUserQueryResult
            {
                User = mapper.Map<UserDto>(user),
                Token = token
            };
        }

        private string GenerateToken(ICollection<Claim> claims)
        {
            var iatClaim = new Claim(
                JwtTokenConstants.IatClaimType,
                DateTime.UtcNow.Ticks.ToString(),
                ClaimValueTypes.Integer64);

            claims.Add(iatClaim);

            return accessTokenGenerationService.GenerateToken(claims);
        }
    }
}
