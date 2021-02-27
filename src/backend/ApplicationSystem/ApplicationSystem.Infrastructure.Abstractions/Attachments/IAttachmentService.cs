using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ApplicationSystem.Infrastructure.Abstractions.Attachments
{
    /// <summary>
    /// Attachment service.
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// Generate attachments. 
        /// </summary>
        /// <param name="formFiles">Form files.</param>
        /// <returns>Attachment DTOs.</returns>
        ICollection<AttachmentDto> GenerateAttachments(IFormFileCollection formFiles);
    }
}
