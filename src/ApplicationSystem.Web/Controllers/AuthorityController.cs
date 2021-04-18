using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.UseCases.Authority.Queries.GetApplicationsForAuthority;
using ApplicationSystem.UseCases.Authority.Commands.SendReplyOnReview;
using ApplicationSystem.UseCases.Authority.Dtos;
using ApplicationSystem.UseCases.Authority.Queries.GetUsersAuthorities;
using ApplicationSystem.Web.Models;

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
        /// Get user`s authorities.
        /// </summary>
        [HttpGet("my")]
        public async Task<IEnumerable<AuthorityDto>> GetUsersAuthorities(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetUsersAuthoritiesQuery(), cancellationToken);
        }

        /// <summary>
        /// Get applications.
        /// </summary>
        [HttpGet("{authorityId}/applications")]
        public async Task<IEnumerable<ApplicationDto>> GetApplications(int authorityId, CancellationToken cancellationToken)
        {
            var query = new GetApplicationsForAuthorityQuery()
            {
                AuthorityId = authorityId
            };

            return await mediator.Send(query, cancellationToken);
        }

        /// <summary>
        /// Send reply on review.
        /// </summary>
        [HttpPost("{authorityId}/applications/{applicationId}/reply")]
        public async Task<StatusCodeResult> SendReplyOnReview(int authorityId, int applicationId, [FromForm] SendReplyOnReviewModel model, CancellationToken cancellationToken)
        {
            var command = new SendReplyOnReviewCommand()
            {
                AuthorityId = authorityId,
                ApplicationId = applicationId,
                ReplyMessage = model.ReplyMessage,
                FormFiles = model.FormFiles
            };

            await mediator.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
