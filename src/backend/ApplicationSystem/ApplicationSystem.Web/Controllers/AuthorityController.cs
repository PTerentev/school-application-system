using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.UseCases.Authority.GetApplications;
using ApplicationSystem.UseCases.Authority.SendReplyOnReview;

namespace ApplicationSystem.Web.Controllers
{
    /// <summary>
    /// Authority controller.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/authority")]
    public class AuthorityController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public AuthorityController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get applications.
        /// </summary>
        [HttpGet("applications/all")]
        public async Task<ICollection<ApplicationDto>> GetApplications(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetApplicationsForAuthorityQuery(), cancellationToken);
        }

        /// <summary>
        /// Send reply on review.
        /// </summary>
        [HttpPost("applications/{applicationId}/reply")]
        public async Task<StatusCodeResult> SendReplyOnReview(int applicationId, [FromForm] string replyMessage, [FromForm] IFormFileCollection formFiles, CancellationToken cancellationToken)
        {
            var command = new SendReplyOnReviewCommand()
            {
                ApplicationId = applicationId,
                ReplyMessage = replyMessage,
                FormFiles = formFiles
            };

            await mediator.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
