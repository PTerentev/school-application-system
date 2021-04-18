using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Web.Models
{
    /// <summary>
    /// Send reply on review model.
    /// </summary>
    public class SendReplyOnReviewModel
    {
        /// <summary>
        /// Reply message.
        /// </summary>
        [Required]
        public string ReplyMessage { get; set; }

        /// <summary>
        /// Form files.
        /// </summary>
        public IFormFileCollection FormFiles { get; set; }
    }
}
