using System;
using System.Collections.Generic;
using ApplicationSystem.Infrastructure.Common.Application;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;

namespace ApplicationSystem.Infrastructure.Common.Dtos
{
    /// <summary>
    /// Application info DTO.
    /// </summary>
    public class ApplicationInfoDto
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the application.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Status.
        /// </summary>
        public ApplicationStatus Status { get; set; }

        /// <summary>
        /// Attachments.
        /// </summary>
        public ICollection<AttachmentInfoDto> Attachments { get; set; }

        /// <summary>
        /// Close date.
        /// </summary>
        public DateTime? CloseDate { get; set; }

        /// <summary>
        /// Reply.
        /// </summary>
        public string ReplyText { get; set; }

        /// <summary>
        /// Reply attachments.
        /// </summary>
        public ICollection<AttachmentInfoDto> ReplyAttachments { get; set; }

        /// <summary>
        /// Reject comments.
        /// </summary>
        public string RejectComments { get; set; }
    }
}
