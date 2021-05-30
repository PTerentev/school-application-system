using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Web.Models;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.UseCases.Admin.CreateUser;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.UseCases.Admin.AddUserToRole;
using ApplicationSystem.UseCases.Admin.Queries.GetUsers;
using ApplicationSystem.Web.Infrastructure.Authorization;
using ApplicationSystem.UseCases.Admin.Queries.GetRoles;

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
                RoleId = model.RoleId
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

        /// <summary>
        /// Get all users.
        /// </summary>
        [HttpGet("all")]
        public async Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetUsersQuery(), cancellationToken);
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        [HttpGet("roles")]
        public async Task<IEnumerable<RoleDto>> GetRoles(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetRolesQuery(), cancellationToken);
        }
    }
}
