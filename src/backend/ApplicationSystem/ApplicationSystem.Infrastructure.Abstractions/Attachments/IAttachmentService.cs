using System.Threading;
using System.Threading.Tasks;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;

namespace ApplicationSystem.Infrastructure.Abstractions.Attachments
{
    /// <summary>
    /// File info DTO.
    /// </summary>
    public record FileInfoDto (string FileUrl);

    /// <summary>
    /// Attachment service.
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// Save attachment. 
        /// </summary>
        /// <param name="attachmentDto">Attachment.</param>
        /// <returns>File info.</returns>
        Task<FileInfoDto> SaveAttachmentAsync(AttachmentDto attachmentDto, CancellationToken cancellationToken);

        /// <summary>
        /// Get attachment.
        /// </summary>
        /// <param name="fileInfo">File info.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Attachment.</returns>
        Task<AttachmentDto> GetAttachmentAsync(FileInfoDto fileInfo, CancellationToken cancellationToken);
    }
}
