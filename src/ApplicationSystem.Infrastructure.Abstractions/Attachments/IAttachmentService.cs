using System.Threading;
using System.Threading.Tasks;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;

namespace ApplicationSystem.Infrastructure.Abstractions.Attachments
{
    /// <summary>
    /// Attachment service.
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// Save attachment.
        /// </summary>
        /// <param name="attachmentDto">Attachment.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>File info.</returns>
        Task<AttachmentInfoDto> SaveAttachmentAsync(AttachmentDto attachmentDto, CancellationToken cancellationToken);

        /// <summary>
        /// Get attachment.
        /// </summary>
        /// <param name="fileKey">File key.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Attachment.</returns>
        Task<byte[]> GetAttachmentAsync(string fileKey, CancellationToken cancellationToken);

        /// <summary>
        /// Remove attachment.
        /// </summary>
        /// <param name="fileKey">File key.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task RemoveAttachmentAsync(string fileKey, CancellationToken cancellationToken);
    }
}
