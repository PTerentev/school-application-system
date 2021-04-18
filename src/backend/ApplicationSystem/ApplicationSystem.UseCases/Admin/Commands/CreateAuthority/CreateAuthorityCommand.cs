using System.ComponentModel.DataAnnotations;
using MediatR;

namespace ApplicationSystem.UseCases.Admin.CreateAuthority
{
    /// <summary>
    /// Create authority command.
    /// </summary>
    public class CreateAuthorityCommand : IRequest
    {
        /// <summary>
        /// Name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
