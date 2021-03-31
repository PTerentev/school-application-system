using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.UseCases.Editorial.GetApplications;
using ApplicationSystem.Infrastructure.UseCases.Editorial.PublishApplication;
using ApplicationSystem.Infrastructure.UseCases.Editorial.RejectApplication;
using ApplicationSystem.Infrastructure.UseCases.Editorial.ResendToAuthority;
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
        [HttpGet("get-applications")]
        public async Task<ICollection<ApplicationDto>> GetApplications(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetApplicationsQuery(), cancellationToken);
        }

        /// <summary>
        /// Publish application.
        /// </summary>
        [HttpPost("publish-application")]
        public async Task<StatusCodeResult> PublishApplication([FromBody] PublishApplicationCommand publishApplicationCommand, CancellationToken cancellationToken)
        {
            await mediator.Send(publishApplicationCommand, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Reject application.
        /// </summary>
        [HttpPost("reject-application")]
        public async Task<StatusCodeResult> RejectApplication([FromBody] RejectApplicationCommand rejectApplicationCommand, CancellationToken cancellationToken)
        {
            await mediator.Send(rejectApplicationCommand, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Resend to authority.
        /// </summary>
        [HttpPost("resend-to-authority")]
        public async Task<StatusCodeResult> ResendToAuthority([FromBody] ResendToAuthorityCommand resendToAuthorityCommand, CancellationToken cancellationToken)
        {
            await mediator.Send(resendToAuthorityCommand, cancellationToken);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
