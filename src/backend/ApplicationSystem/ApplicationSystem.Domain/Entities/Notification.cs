using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Domain.Entities
{
    /// <summary>
    /// Notification.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// To user Id.
        /// </summary>
        [Required]
        public int ToUserId { get; set; }

        /// <summary>
        /// To user.
        /// </summary>
        [Required]
        public User ToUser { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

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
        /// Creation date.
        /// </summary>
        [Required]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Has user seen the notification.
        /// </summary>
        [Required]
        public bool HasUserSeen { get; set; }
    }
}
