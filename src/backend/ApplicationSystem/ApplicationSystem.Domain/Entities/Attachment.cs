using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Domain.Entities
{
    /// <summary>
    /// Attachment.
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Application Id.
        /// </summary>
        [Required]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Application.
        /// </summary>
        [Required]
        public Application Application { get; set; }

        /// <summary>
        /// Link.
        /// </summary>
        [Required]
        public string Link { get; set; }

        /// <summary>
        /// Attachment type.
        /// </summary>
        public AttachmentType Type { get; set; }
    }

    /// <summary>
    /// Attachment type.
    /// </summary>
    public enum AttachmentType
    {
        None = 0,
        Image = 1,
        Video = 2
    }
}
