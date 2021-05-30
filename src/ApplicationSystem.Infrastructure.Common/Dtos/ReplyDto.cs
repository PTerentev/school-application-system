using System;
using System.Collections.Generic;

namespace ApplicationSystem.Infrastructure.Common.Dtos
{
    /// <summary>
    /// Reply DTO.
    /// </summary>
    public class ReplyDto
    {
        /// <summary>
        /// Reply text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Last change date.
        /// </summary>
        public DateTime LastChangeDate { get; set; }

        /// <summary>
        /// Edit comments.
        /// </summary>
        public string EditComments { get; set; }

        /// <summary>
        /// Attachments.
        /// </summary>
        public ICollection<int> Attachments { get; set; }
    }
}
