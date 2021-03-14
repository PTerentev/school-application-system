using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.UseCases.User.GetInfo;
using ApplicationSystem.Infrastructure.UseCases.User.Login;
using ApplicationSystem.Infrastructure.UseCases.User.Register;

namespace ApplicationSystem.Web.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get user info.
        /// </summary>
        [HttpGet("get-info")]
        public async Task<UserDto> GetUserInfo(GetUserInfoQuery getUserInfoQuery, CancellationToken cancellationToken)
        {
            return await mediator.Send(getUserInfoQuery, cancellationToken);
        }

        /// <summary>
        /// Login user.
        /// </summary>
        [HttpGet("login")]
        public async Task<LoginUserQueryResult> Login([FromBody] LoginUserQuery loginUserQuery, CancellationToken cancellationToken)
        {
            return await mediator.Send(loginUserQuery, cancellationToken);
        }

        /// <summary>
        /// Register user.
        /// </summary>
        [HttpGet("register")]
        public async Task<UserDto> Register([FromBody] RegisterUserCommand registerUserCommand, CancellationToken cancellationToken)
        {
            return await mediator.Send(registerUserCommand, cancellationToken);
        }
    }
}
