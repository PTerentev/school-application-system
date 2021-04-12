using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using ApplicationSystem.Infrastructure.UseCases.Authority.Validation;

namespace ApplicationSystem.Infrastructure.UseCases.Authority.SendReplyOnReview
{
    /// <summary>
    /// Send reply on review command.
    /// </summary>
    [AuthorityValidation]
    public class SendReplyOnReviewCommand : AuthorityRequestBase, IRequest
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
