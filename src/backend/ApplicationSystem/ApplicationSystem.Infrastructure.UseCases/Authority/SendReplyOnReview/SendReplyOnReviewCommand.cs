using System.ComponentModel.DataAnnotations;
using ApplicationSystem.Infrastructure.UseCases.Dtos;

namespace ApplicationSystem.Infrastructure.UseCases.Authority.SendReplyOnReview
{
    /// <summary>
    /// Send reply on review command.
    /// </summary>
    public class SendReplyOnReviewCommand
    {
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
        /// Reply DTO.
        /// </summary>
        [Required]
        public ReplyDto ReplyDto { get; set; }
    }
}
