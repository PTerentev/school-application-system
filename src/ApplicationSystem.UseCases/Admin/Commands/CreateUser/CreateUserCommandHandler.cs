using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.UseCases.Admin.CreateUser
{
    /// <summary>
    /// Create user command handler.
    /// </summary>
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly UserManager<Domain.Entities.User> userManager;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CreateUserCommandHandler(IMapper mapper, UserManager<Domain.Entities.User> userManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<Domain.Entities.User>(request);

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new DomainException("User data is invalid.");
            }

            return mapper.Map<UserDto>(user);
        }
    }
}
