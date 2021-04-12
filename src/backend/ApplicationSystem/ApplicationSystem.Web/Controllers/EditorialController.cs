using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.UseCases.Editorial.GetApplications;
using ApplicationSystem.UseCases.Editorial.PublishApplication;
using ApplicationSystem.UseCases.Editorial.RejectApplication;
using ApplicationSystem.UseCases.Editorial.ResendToAuthority;
using ApplicationSystem.Web.Infrastructure.Authorization;

namespace ApplicationSystem.Web.Controllers
{
    /// <summary>
    /// Editorial controller.
    /// </summary>
    [ApiController]
    [Route("api/editorial")]
    [RoleBasedAuthorize(UserRoles.Editor)]
    public class EditorialController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EditorialController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get applications.
        /// </summary>
        [HttpGet("applications/all")]
        public async Task<ICollection<ApplicationDto>> GetApplications(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetApplicationsQuery(), cancellationToken);
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
        public async Task<StatusCodeResult> RejectApplication(int applicationId, [FromBody] string rejectComments, CancellationToken cancellationToken)
        {
            var command = new RejectApplicationCommand()
            {
                ApplicationId = applicationId,
                RejectComments = rejectComments
            };

            await mediator.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Resend to authority.
        /// </summary>
        [HttpPost("applications/{applicationId}/resend")]
        public async Task<StatusCodeResult> ResendToAuthority(int applicationId, [FromBody] string editComments, CancellationToken cancellationToken)
        {
            var command = new ResendToAuthorityCommand()
            {
                ApplicationId = applicationId,
                EditComments = editComments
            };

            await mediator.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
