using ApplicationSystem.Infrastructure.UseCases.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Infrastructure.UseCases.Admin.CreateUser
{
    /// <summary>
    /// Create user command.
    /// </summary>
    public class CreateUserCommand : IRequest<UserDto>
    {
        /// <summary>
        /// First name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        /// <summary>
        /// Middle name.
        /// </summary>
        [MaxLength(255)]
        public string MiddleName { get; set; }

        /// <summary>
        /// User's post.
        /// </summary>
        [MaxLength(255)]
        public string Post { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
