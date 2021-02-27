using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationSystem.Infrastructure.Abstractions.Attachments
{
    /// <summary>
    /// Attachment DTO.
    /// </summary>
    public class AttachmentDto
    {
        /// <summary>
        /// Data.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Content type.
        /// </summary>
        public string ContentType { get; set; }
    }
}
