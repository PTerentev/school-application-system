using System.ComponentModel.DataAnnotations;
using MediatR;
using ApplicationSystem.UseCases.Attachments.Dtos;

namespace ApplicationSystem.UseCases.Attachments.GetAttachment
{
    /// <summary>
    /// Get attachment query.
    /// </summary>
    public class GetAttachmentQuery : IRequest<AttachmentBase64Dto>
    {
        /// <summary>
        /// Attachment Id.
        /// </summary>
        [Required]
        public int AttachmentId { get; set; }
    }
}
