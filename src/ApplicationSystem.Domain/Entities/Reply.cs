﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Domain.Entities
{
    /// <summary>
    /// Application reply.
    /// </summary>
    public class Reply
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Reply()
        {
            Attachments = new List<Attachment>();
        }

        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Reply text.
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Last change date.
        /// </summary>
        [Required]
        public DateTime LastChangeDate { get; set; }

        /// <summary>
        /// Edit comments.
        /// </summary>
        public string EditComments { get; set; }

        /// <summary>
        /// Edit required flag.
        /// </summary>
        public bool EditRequired { get; set; }

        /// <summary>
        /// Application Id.
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// Application.
        /// </summary>
        public virtual Application Application { get; set; }

        /// <summary>
        /// Attachments.
        /// </summary>
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
