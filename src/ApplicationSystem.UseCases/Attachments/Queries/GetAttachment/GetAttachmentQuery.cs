using System.ComponentModel.DataAnnotations;
using MediatR;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;

namespace ApplicationSystem.UseCases.Attachments.GetAttachment
{
    /// <summary>
    /// Get attachment query.
    /// </summary>
    public class GetAttachmentQuery : IRequest<AttachmentDto>
    {
        /// <summary>
        /// File key.
        /// </summary>
        [Required]
        public string FileKey { get; set; }
    }
}
