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
    /// <summary>
    /// User controller.
    /// </summary>
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
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
