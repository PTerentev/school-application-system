using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Abstractions.Attachments;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;

namespace ApplicationSystem.UseCases.Common
{
    /// <summary>
    /// Attachment helper.
    /// </summary>
    internal static class AttachmentHelper
    {
        /// <summary>
        /// Generate attachments.
        /// </summary>
        /// <param name="formFiles">Form files.</param>
        /// <param name="attachmentService">Attachment service.</param>
        /// <param name="mapper">Mapper.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Attachments.</returns>
        public static async Task<ICollection<Attachment>> GenerateAttachmentsAsync(
            IFormFileCollection formFiles, 
            IAttachmentService attachmentService, 
            IMapper mapper,
            CancellationToken cancellationToken)
        {
            var attachments = new List<Attachment>();

            foreach (var formFile in formFiles)
            {
                var attachmentDto = mapper.Map<AttachmentDto>(formFile);
                var fileInfo = await attachmentService.SaveAttachmentAsync(attachmentDto, cancellationToken);

                var attachment = new Attachment()
                {
                    ContentType = attachmentDto.ContentType,
                    FileKey = fileInfo.FileKey
                };

                attachments.Add(attachment);
            }

            return attachments;
        }
    }
}
