using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Domain.Entities
{
    /// <summary>
    /// Application.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Application()
        {
            Attachments = new List<Attachment>();
        }

        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the application.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Creator user Id.
        /// </summary>
        public int CreatorUserId { get; set; }

        /// <summary>
        /// Creator user.
        /// </summary>
        public User CreatorUser { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        [Required]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Authority Id.
        /// </summary>
        public int AuthorityId { get; set; }

        /// <summary>
        /// Authority.
        /// </summary>
        public Authority Authority { get; set; }

        /// <summary>
        /// Close date.
        /// </summary>
        public DateTime? CloseDate { get; set; }

        /// <summary>
        /// Reply.
        /// </summary>
        public Reply Reply { get; set; }

        /// <summary>
        /// Status.
        /// </summary>
        public ApplicationStatus Status { get; set; }

        /// <summary>
        /// Reject comments.
        /// </summary>
        public string RejectComments { get; set; }

        /// <summary>
        /// Attachments.
        /// </summary>
        public ICollection<Attachment> Attachments { get; set; }

        /// <summary>
        /// Application status.
        /// </summary>
        public enum ApplicationStatus
        {
            Open = 0,
            Published = 1,
            Rejected = 2
        }
    }
}
