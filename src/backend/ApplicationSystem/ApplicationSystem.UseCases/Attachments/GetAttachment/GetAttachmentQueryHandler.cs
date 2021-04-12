using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ApplicationSystem.Infrastructure.Abstractions.Attachments;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;

namespace ApplicationSystem.UseCases.Attachments.GetAttachment
{
    /// <summary>
    /// Get attachment query handler.
    /// </summary>
    internal class GetAttachmentQueryHandler : IRequestHandler<GetAttachmentQuery, AttachmentDto>
    {
        private readonly IAttachmentService attachmentService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="attachmentService">Attachment service.</param>
        public GetAttachmentQueryHandler(IAttachmentService attachmentService)
        {
            this.attachmentService = attachmentService;
        }

        /// <inheritdoc/>
        public async Task<AttachmentDto> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
        {
            var attachment = await attachmentService.GetAttachmentAsync(
                new AttachmentInfoDto()
                {
                FileKey = request.FileKey
                },
                cancellationToken);

            return attachment;
        }
    }
}
