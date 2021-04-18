using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.UseCases.Applicant.Commands.SendApplication
{
    /// <summary>
    /// Send application command.
    /// </summary>
    public class SendApplicationCommand : IRequest
    {
        /// <summary>
        /// Name of the application.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Attachment files.
        /// </summary>
        public IFormFileCollection FormFiles { get; set; }
    }
}
