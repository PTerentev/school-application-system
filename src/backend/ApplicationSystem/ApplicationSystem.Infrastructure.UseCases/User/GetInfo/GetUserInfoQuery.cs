using MediatR;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.Infrastructure.UseCases.User.GetInfo
{
    /// <summary>
    /// Get user info query.
    /// </summary>
    public class GetUserInfoQuery : IRequest<UserDto>
    {
        public string UserId { get; set; }
    }
}
