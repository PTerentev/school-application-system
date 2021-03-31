using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.UseCases.Authority.GetApplications;
using ApplicationSystem.Infrastructure.UseCases.Authority.SendReplyOnReview;

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
        public AuthorityController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get applications.
        /// </summary>
        [HttpGet("get-applications")]
        public async Task<ICollection<ApplicationDto>> GetApplications(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetApplicationsForAuthorityQuery(), cancellationToken);
        }

        /// <summary>
        /// Send reply on review.
        /// </summary>
        [HttpPost("send-reply-on-review")]
        public async Task<StatusCodeResult> SendReplyOnReview([FromBody] SendReplyOnReviewCommand sendReplyOnReviewCommand, CancellationToken cancellationToken)
        {
            await mediator.Send(sendReplyOnReviewCommand, cancellationToken);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
