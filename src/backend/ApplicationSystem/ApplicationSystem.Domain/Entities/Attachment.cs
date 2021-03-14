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
        /// File URL.
        /// </summary>
        [Required]
        public string FileUrl { get; set; }

        /// <summary>
        /// Content type.
        /// </summary>
        [Required]
        public string ContentType { get; set; }
    }
}
