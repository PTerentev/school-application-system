namespace ApplicationSystem.Infrastructure.Common.Dtos.Attachments
{
    /// <summary>
    /// Attachment DTO.
    /// </summary>
    public class AttachmentDto
    {
        /// <summary>
        /// Data.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Content type.
        /// </summary>
        public string ContentType { get; set; }
    }
}
