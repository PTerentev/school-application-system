using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.UseCases.Editorial.Queries.GetApplicationsForEditorial;
using ApplicationSystem.UseCases.Editorial.Commands.PublishApplication;
using ApplicationSystem.UseCases.Editorial.Commands.RejectApplication;
using ApplicationSystem.UseCases.Editorial.Commands.ResendToAuthority;
using ApplicationSystem.Web.Infrastructure.Authorization;
using ApplicationSystem.UseCases.Editorial.Commands.SendToAuthority;
using ApplicationSystem.UseCases.Authority.Dtos;
using ApplicationSystem.UseCases.Authority.Queries.GetAllAuthoritiesQuery;
using ApplicationSystem.Web.Models;

namespace ApplicationSystem.Web.Controllers
{
    /// <summary>
    /// Editorial controller.
    /// </summary>
    [ApiController]
    [Route("api/editorial")]
    [RoleBasedAuthorize(UserRoles.Editor, UserRoles.Admin)]
    public class EditorialController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public EditorialController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get authorities.
        /// </summary>
        [HttpGet("authorities")]
        public async Task<IEnumerable<AuthorityDto>> GetAuthorities(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetAllAuthoritiesQuery(), cancellationToken);
        }

        /// <summary>
        /// Get applications.
        /// </summary>
        [HttpGet("applications/all")]
        public async Task<IEnumerable<ApplicationDto>> GetApplications(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetApplicationsForEditorialQuery(), cancellationToken);
        }

        /// <summary>
        /// Publish application.
        /// </summary>
        [HttpPost("applications/{applicationId}/publish")]
        public async Task<StatusCodeResult> PublishApplication(int applicationId, CancellationToken cancellationToken)
        {
            var command = new PublishApplicationCommand()
            {
                ApplicationId = applicationId
            };

            await mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Reject application.
        /// </summary>
        [HttpPost("applications/{applicationId}/reject")]
        public async Task<StatusCodeResult> RejectApplication(int applicationId, [FromBody] RejectApplicationModel model, CancellationToken cancellationToken)
        {
            var command = new RejectApplicationCommand()
            {
                ApplicationId = applicationId,
                RejectComments = model.RejectComments
            };

            await mediator.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Resend to authority.
        /// </summary>
        [HttpPost("applications/{applicationId}/resend")]
        public async Task<StatusCodeResult> ResendToAuthority(int applicationId, [FromBody] ResendToAuthorityModel model, CancellationToken cancellationToken)
        {
            var command = new ResendToAuthorityCommand()
            {
                ApplicationId = applicationId,
                EditComments = model.EditComments
            };

            await mediator.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Send to authority.
        /// </summary>
        [HttpPost("applications/{applicationId}/send")]
        public async Task<StatusCodeResult> SendToAuthority(int applicationId, [FromBody] SendToAuthorityModel model, CancellationToken cancellationToken)
        {
            var command = new SendToAuthorityCommand()
            {
                ApplicationId = applicationId,
                AuthorityId = model.AuthorityId.Value
            };

            await mediator.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
