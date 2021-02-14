using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Infrastructure.UseCases.Dtos
{
    /// <summary>
    /// Reply DTO.
    /// </summary>
    public class ReplyDto
    {
        /// <summary>
        /// Text.
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Attachment files.
        /// </summary>
        [Required]
        public IFormFileCollection FormFiles { get; set; }
    }
}
