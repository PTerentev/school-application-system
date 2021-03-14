using System.Collections.Generic;
using System.Net.Mail;

namespace ApplicationSystem.Infrastructure.Common.Dtos.Emails
{
    /// <summary>
    /// Email DTO.
    /// </summary>
    public class EmailDto
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public EmailDto()
        {
            ToMailAddresses = new List<MailAddress>();
            EmailContent = new EmailContentDto();
        }

        /// <summary>
        /// To mail addresses.
        /// </summary>
        public ICollection<MailAddress> ToMailAddresses { get; set; }

        /// <summary>
        /// Email content.
        /// </summary>
        public EmailContentDto EmailContent { get; set; }
    }
}
