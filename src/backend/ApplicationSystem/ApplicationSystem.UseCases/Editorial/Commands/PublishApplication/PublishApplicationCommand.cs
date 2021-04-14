using System.ComponentModel.DataAnnotations;
using MediatR;

namespace ApplicationSystem.UseCases.Editorial.Commands.PublishApplication
{
    /// <summary>
    /// Publish application command.
    /// </summary>
    public class PublishApplicationCommand : IRequest
    {
        /// <summary>
        /// Application Id.
        /// </summary>
        [Required]
        public int ApplicationId { get; set; }
    }
}
