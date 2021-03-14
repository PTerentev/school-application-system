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
        /// <inheritdoc cref="UserDto.Email"/>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <inheritdoc cref="UserDto.FirstName"/>
        [Required]
        public string FirstName { get; set; }

        /// <inheritdoc cref="UserDto.LastName"/>
        [Required]
        public string LastName { get; set; }

        /// <inheritdoc cref="UserDto.MiddleName"/>
        public string MiddleName { get; set; }
    }
}
