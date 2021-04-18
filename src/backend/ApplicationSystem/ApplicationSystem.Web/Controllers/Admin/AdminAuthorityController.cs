using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.UseCases.Admin.CreateAuthority;
using ApplicationSystem.UseCases.Admin.RemoveAuthority;
using ApplicationSystem.Web.Infrastructure.Authorization;
using ApplicationSystem.UseCases.Authority.Queries.GetAllAuthoritiesQuery;
using ApplicationSystem.UseCases.Authority.Dtos;
using ApplicationSystem.Web.Models;
using ApplicationSystem.UseCases.Admin.AddUserToAuthority;

namespace ApplicationSystem.Web.Controllers.Admin
{
    /// <summary>
    /// Admin controller for authority managing.
    /// </summary>
    [ApiController]
    [Route("api/admin/authorities")]
    [RoleBasedAuthorize(UserRoles.Admin)]
    public class AdminAuthorityController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public AdminAuthorityController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get authorities.
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<AuthorityDto>> GetAuthorities(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetAllAuthoritiesQuery(), cancellationToken);
        }

        /// <summary>
        /// Remove authority.
        /// </summary>
        [HttpDelete("{authorityId}")]
        public async Task<StatusCodeResult> RemoveAuthority(int authorityId, CancellationToken cancellationToken)
        {
            var command = new RemoveAuthorityCommand()
            {
                AuthorityId = authorityId
            };

            await mediator.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Add user to authority.
        /// </summary>
        [HttpPost("{authorityId}/user")]
        public async Task<StatusCodeResult> AddUserToAuthority(int authorityId, [FromBody] AddUserToAuthorityModel model, CancellationToken cancellationToken)
        {
            var command = new AddUserToAuthorityCommand()
            {
                UserId = model.UserId.Value,
                AuthorityId = authorityId
            };

            await mediator.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Create authority.
        /// </summary>
        [HttpPost]
        public async Task<StatusCodeResult> CreateAuthority([FromBody] CreateAuthorityCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
