using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;
using ApplicationSystem.UseCases.Attachments.GetAttachment;
using Microsoft.AspNetCore.Authorization;
using ApplicationSystem.UseCases.Attachments.Dtos;

namespace ApplicationSystem.Web.Controllers
{
    /// <summary>
    /// Attachment controller.
    /// </summary>
    [ApiController]
    [Route("api/attachment")]
    public class AttachmentController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public AttachmentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get attachment.
        /// </summary>
        [HttpGet("{attachmentId}")]
        [AllowAnonymous]
        public async Task<AttachmentBase64Dto> GetAttachment(int attachmentId, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetAttachmentQuery() { AttachmentId = attachmentId }, cancellationToken);
        }
    }
}
