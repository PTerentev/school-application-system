using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.UseCases.User.GetInfo
{
    /// <summary>
    /// Get user info query handler.
    /// </summary>
    internal class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserDto>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GetUserInfoQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<UserDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.SingleAsync(u => u.Id == request.UserId, cancellationToken);
            var userDto = mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
