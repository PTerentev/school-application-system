using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.UseCases.Admin.AddUserToAuthority;
using ApplicationSystem.Infrastructure.UseCases.Admin.AddUserToRole;
using ApplicationSystem.Infrastructure.UseCases.Admin.CreateUser;

namespace ApplicationSystem.Web.Controllers
{
    /// <summary>
    /// Admin controller.
    /// </summary>
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = Role.AdminRole)]
    public class AdminController : Controller
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AdminController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Add user to authority.
        /// </summary>
        [HttpPost("add-user-to-authority")]
        public async Task<StatusCodeResult> AddUserToAuthority([FromBody] AddUserToAuthorityCommand addUserToAuthorityCommand, CancellationToken cancellationToken)
        {
            await mediator.Send(addUserToAuthorityCommand, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Add user to authority.
        /// </summary>
        [HttpPost("add-user-to-role")]
        public async Task<StatusCodeResult> AddUserToRole([FromBody] AddUserToRoleCommand addUserToRoleCommand, CancellationToken cancellationToken)
        {
            await mediator.Send(addUserToRoleCommand, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Add user to authority.
        /// </summary>
        [HttpPost("create-user")]
        public async Task<UserDto> CreateUserCommand([FromBody] CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            return await mediator.Send(createUserCommand, cancellationToken);
        }
    }
}
