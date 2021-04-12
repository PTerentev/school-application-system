using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;
using ApplicationSystem.UseCases.Attachments.GetAttachment;

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
        /// Get applications.
        /// </summary>
        [HttpGet("{fileKey}")]
        public async Task<AttachmentDto> GetAttachment([FromQuery] GetAttachmentQuery query, CancellationToken cancellationToken)
        {
            return await mediator.Send(query, cancellationToken);
        }
    }
}
