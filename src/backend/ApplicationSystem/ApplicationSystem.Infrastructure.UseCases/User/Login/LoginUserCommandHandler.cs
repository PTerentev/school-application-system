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

namespace ApplicationSystem.Infrastructure.UseCases.User.Login
{
    /// <summary>
    /// Login user command handler.
    /// </summary>
    internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResult>
    {
        /// <summary>
        /// Issued at date/time claim. https://tools.ietf.org/html/rfc7519#page-10 .
        /// </summary>
        public const string IatClaimType = "iat";

        private readonly SignInManager<Domain.Entities.User> signInManager;
        private readonly UserManager<Domain.Entities.User> userManager;
        private readonly IAccessTokenGenerationService accessTokenGenerationService;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoginUserCommandHandler(
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
        public async Task<LoginUserCommandResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            var result = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
            if (!result.Succeeded)
            {
                throw new DomainException("Login denied for the user.");
            }

            var principal = await signInManager.CreateUserPrincipalAsync(user);

            return new LoginUserCommandResult
            {
                User = mapper.Map<UserDto>(user),
                Token = Generate(principal.Claims.ToList())
            };
        }

        private string Generate(ICollection<Claim> claims)
        {
            var iatClaim = new Claim(
                IatClaimType,
                DateTime.UtcNow.Ticks.ToString(),
                ClaimValueTypes.Integer64);

            claims.Add(iatClaim);

            return accessTokenGenerationService.GenerateToken(claims);
        }
    }
}
