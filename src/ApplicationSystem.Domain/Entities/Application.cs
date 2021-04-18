using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationSystem.Infrastructure.Common.Application;

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
        public int? CreatorUserId { get; set; }

        /// <summary>
        /// Creator user.
        /// </summary>
        public virtual User CreatorUser { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        [Required]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Authority Id.
        /// </summary>
        public int? AuthorityId { get; set; }

        /// <summary>
        /// Authority.
        /// </summary>
        public virtual Authority Authority { get; set; }

        /// <summary>
        /// Close date.
        /// </summary>
        public DateTime? CloseDate { get; set; }

        /// <summary>
        /// Reply.
        /// </summary>
        public virtual Reply Reply { get; set; }

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
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
