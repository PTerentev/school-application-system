using System.ComponentModel.DataAnnotations;
using MediatR;
using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.Infrastructure.UseCases.User.Register
{
    /// <summary>
    /// Register user command.
    /// </summary>
    public class RegisterUserCommand : IRequest<UserDto>
    {
        /// <summary>
        /// <inheritdoc cref="UserDto.Email"/>
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// <inheritdoc cref="UserDto.FirstName"/>
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// <inheritdoc cref="UserDto.LastName"/>
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// <inheritdoc cref="UserDto.MiddleName"/>
        /// </summary>
        public string MiddleName { get; set; }
    }
}
