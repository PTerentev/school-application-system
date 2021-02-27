using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApplicationSystem.Infrastructure.UseCases.Authority.SendReplyOnReview
{
    /// <summary>
    /// Send reply on review command.
    /// </summary>
    public class SendReplyOnReviewCommand : IRequest
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Authority Id.
        /// </summary>
        [Required]
        public int AuthorityId { get; set; }

        /// <summary>
        /// Application Id.
        /// </summary>
        [Required]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Text.
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Attachment files.
        /// </summary>
        public IFormFileCollection FormFiles { get; set; }
    }
}
