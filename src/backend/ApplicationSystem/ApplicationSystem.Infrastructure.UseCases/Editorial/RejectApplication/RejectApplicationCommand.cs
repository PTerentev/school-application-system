using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Infrastructure.UseCases.Editorial.RejectApplication
{
    /// <summary>
    /// Reject application command.
    /// </summary>
    public class RejectApplicationCommand : IRequest
    {
        /// <summary>
        /// Application Id.
        /// </summary>
        [Required]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Reject comments.
        /// </summary>
        [Required]
        public string RejectComments { get; set; }
    }
}
