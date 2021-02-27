using System;
using System.Collections.Generic;
using ApplicationSystem.Domain.Entities;

namespace ApplicationSystem.Infrastructure.UseCases.Dtos
{
    /// <summary>
    /// Application DTO.
    /// </summary>
    public class ApplicationDto
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
        /// Creator user Id.
        /// </summary>
        public int CreatorUserId { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Authority Id.
        /// </summary>
        public int AuthorityId { get; set; }

        /// <summary>
        /// Status.
        /// </summary>
        public ApplicationStatus Status { get; set; }

        /// <summary>
        /// Attachments.
        /// </summary>
        public ICollection<AttachmentInfoDto> Attachments { get; set; }
    }
}
