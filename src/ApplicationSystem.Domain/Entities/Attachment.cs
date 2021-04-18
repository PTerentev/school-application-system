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
        /// File key.
        /// </summary>
        [Required]
        public string FileKey { get; set; }

        /// <summary>
        /// Content type.
        /// </summary>
        [Required]
        public string ContentType { get; set; }
    }
}
