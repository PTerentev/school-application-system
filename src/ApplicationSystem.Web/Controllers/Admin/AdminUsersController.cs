using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.UseCases.Admin.AddUserToAuthority;
using ApplicationSystem.UseCases.Admin.AddUserToRole;
using ApplicationSystem.UseCases.Admin.CreateUser;
using ApplicationSystem.Web.Infrastructure.Authorization;
using ApplicationSystem.Web.Models;

namespace ApplicationSystem.Web.Controllers.Admin
{
    /// <summary>
    /// Admin controller for user managing.
    /// </summary>
    [ApiController]
    [Route("api/admin/users")]
    [RoleBasedAuthorize(UserRoles.Admin)]
    public class AdminUsersController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public AdminUsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Add user to role.
        /// </summary>
        [HttpPost("{userId}/role")]
        public async Task<StatusCodeResult> AddUserToRole(int userId, [FromBody] AddUserToRoleModel model, CancellationToken cancellationToken)
        {
            var command = new AddUserToRoleCommand()
            {
                UserId = userId,
                Role = model.Role
            };

            await mediator.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Add user to authority.
        /// </summary>
        [HttpPost]
        public async Task<UserDto> CreateUserCommand([FromBody] CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            return await mediator.Send(createUserCommand, cancellationToken);
        }
    }
}
