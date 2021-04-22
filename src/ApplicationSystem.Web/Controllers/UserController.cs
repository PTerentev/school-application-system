using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.UseCases.User.GetInfo;
using ApplicationSystem.UseCases.User.Login;
using ApplicationSystem.UseCases.User.Register;
using Microsoft.AspNetCore.Authorization;

namespace ApplicationSystem.Web.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    [ApiController]
    [Route("api/user")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get user info.
        /// </summary>
        [HttpGet("{UserId}")]
        public async Task<UserDto> GetUserInfo(int userId, CancellationToken cancellationToken)
        {
            var query = new GetUserInfoQuery()
            {
                UserId = userId
            };

            return await mediator.Send(query, cancellationToken);
        }

        /// <summary>
        /// Login user.
        /// </summary>
        [HttpPost("login")]
        public async Task<LoginUserQueryResult> Login([FromBody] LoginUserQuery loginUserQuery, CancellationToken cancellationToken)
        {
            return await mediator.Send(loginUserQuery, cancellationToken);
        }

        /// <summary>
        /// Register user.
        /// </summary>
        [HttpPost("register")]
        public async Task<UserDto> Register([FromBody] RegisterUserCommand registerUserCommand, CancellationToken cancellationToken)
        {
            return await mediator.Send(registerUserCommand, cancellationToken);
        }
    }
}
