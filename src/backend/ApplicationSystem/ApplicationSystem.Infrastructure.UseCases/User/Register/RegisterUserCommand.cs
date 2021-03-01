using ApplicationSystem.Infrastructure.Common.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Infrastructure.UseCases.User.Register
{
    /// <summary>
    /// Register user command.
    /// </summary>
    public class RegisterUserCommand : IRequest<UserDto>
    {
        [Required]
        [MaxLength(250)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }
}
