using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationSystem.UseCases.Attachments.SaveAttachment
{
    /// <summary>
    /// Save attachment command.
    /// </summary>
    public class SaveAttachmentCommand : IRequest<IEnumerable<Attachment>>
    {
        /// <summary>
        /// Attachment.
        /// </summary>
        public IEnumerable<AttachmentDto> Attachments { get; set; } = Enumerable.Empty<AttachmentDto>();
    }
}
