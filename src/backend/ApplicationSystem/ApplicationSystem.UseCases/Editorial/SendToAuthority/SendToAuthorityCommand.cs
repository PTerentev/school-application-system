using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.UseCases.Editorial.SendToAuthority
{
    /// <summary>
    /// Send to authority command.
    /// </summary>
    public class SendToAuthorityCommand : IRequest
    {
        /// <summary>
        /// Application Id.
        /// </summary>
        [Required]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Authority Id.
        /// </summary>
        [Required]
        public int AuthorityId { get; set; }
    }
}
