using MediatR;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.UseCases.User.GetInfo
{
    /// <summary>
    /// Get user info query.
    /// </summary>
    public class GetUserInfoQuery : IRequest<UserDto>
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public int UserId { get; set; }
    }
}
