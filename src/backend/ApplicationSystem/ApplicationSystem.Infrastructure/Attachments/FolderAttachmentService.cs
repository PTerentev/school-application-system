using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HeyRed.Mime;
using ApplicationSystem.Infrastructure.Abstractions.Attachments;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;

namespace ApplicationSystem.Infrastructure.Attachments
{
    /// <inheritdoc cref="IAttachmentService"/>
    public class FolderAttachmentService : IAttachmentService
    {
        private readonly string folderPath;

        /// <summary>
        /// Constructor.
        /// </summary>
        public FolderAttachmentService()
        {
            var applicationFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            folderPath = Directory.CreateDirectory(Path.Combine(applicationFolder, "ApplicationSystem")).FullName;
        }

        /// <inheritdoc/>
        public async Task<AttachmentDto> GetAttachmentAsync(AttachmentInfoDto fileInfo, CancellationToken cancellationToken)
        {
            var path = GetFullFilePath(fileInfo.FileKey);
            var bytes = await File.ReadAllBytesAsync(path, cancellationToken);
            var contentType = MimeTypesMap.GetMimeType(fileInfo.FileKey);

            return new AttachmentDto()
            {
                Data = bytes,
                ContentType = contentType
            };
        }

        /// <inheritdoc/>
        public Task RemoveAttachmentAsync(AttachmentInfoDto fileInfo, CancellationToken cancellationToken)
        {
            var path = GetFullFilePath(fileInfo.FileKey);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task<AttachmentInfoDto> SaveAttachmentAsync(AttachmentDto attachmentDto, CancellationToken cancellationToken)
        {
            var fileName = $"{Guid.NewGuid()}.{MimeTypesMap.GetExtension(attachmentDto.ContentType)}";
            var path = GetFullFilePath(fileName);
            await File.WriteAllBytesAsync(path, attachmentDto.Data, cancellationToken);

            return new AttachmentInfoDto()
            {
                FileKey = path
            };
        }

        private string GetFullFilePath(string fileName) => Path.Combine(folderPath, fileName);
    }
}
