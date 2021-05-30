using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using ApplicationSystem.UseCases.Authority.Common;
using ApplicationSystem.UseCases.Common.Behaviors;

namespace ApplicationSystem.UseCases.Authority.Commands.SendReplyOnReview
{
    /// <summary>
    /// Send reply on review command.
    /// </summary>
    public class SendReplyOnReviewCommand : AuthorityRequestBase, IRequestWithAttachment, IRequest
    {
        /// <summary>
        /// Application Id.
        /// </summary>
        [Required]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Text.
        /// </summary>
        [Required]
        public string ReplyMessage { get; set; }

        /// <summary>
        /// Attachment files.
        /// </summary>
        public IFormFileCollection FormFiles { get; set; }
    }
}
