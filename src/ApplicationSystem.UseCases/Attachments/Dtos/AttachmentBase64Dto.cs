namespace ApplicationSystem.UseCases.Attachments.Dtos
{
    /// <summary>
    /// Attachment Base64 DTO.
    /// </summary>
    public class AttachmentBase64Dto
    {
        /// <summary>
        /// Data in Base64.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Content type.
        /// </summary>
        public string ContentType { get; set; }
    }
}
