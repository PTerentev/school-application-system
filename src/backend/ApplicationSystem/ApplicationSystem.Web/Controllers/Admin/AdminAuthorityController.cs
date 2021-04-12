using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.UseCases.Admin.CreateAuthority;
using ApplicationSystem.UseCases.Admin.RemoveAuthority;
using ApplicationSystem.Web.Infrastructure.Authorization;

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
