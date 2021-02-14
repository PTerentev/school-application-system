using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Infrastructure.UseCases.Editorial.ResendToAuthority
{
    /// <summary>
    /// Resend to authority command.
    /// </summary>
    public class ResendToAuthorityCommand
    {
        /// <summary>
        /// Application Id.
        /// </summary>
        [Required]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Edit comments.
        /// </summary>
        [Required]
        public string EditComments { get; set; }
    }
}
