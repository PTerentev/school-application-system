using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.UseCases.User.Register
{
    /// <summary>
    /// Register user command handler.
    /// </summary>
    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly UserManager<Domain.Entities.User> userManager;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RegisterUserCommandHandler(UserManager<Domain.Entities.User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<Domain.Entities.User>(request);
            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new ValidationException(ValidationErrors.CreateFromErrors(
                        "An error occurred in user registration!",
                        result.Errors.Select(e => e.Description).ToArray()));
            }

            return mapper.Map<UserDto>(user);
        }
    }
}
