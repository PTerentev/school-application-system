using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.Infrastructure.UseCases.User.GetInfo
{
    /// <summary>
    /// Get user info query handler.
    /// </summary>
    internal class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserDto>
    {
        private readonly UserManager<Domain.Entities.User> userManager;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GetUserInfoQueryHandler(UserManager<Domain.Entities.User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<UserDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            var userDto = mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
