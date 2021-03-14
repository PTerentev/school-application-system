using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSystem.Infrastructure.UseCases.User.Login
{
    /// <summary>
    /// Login user query.
    /// </summary>
    public class LoginUserQuery : IRequest<LoginUserQueryResult>
    {
        /// <summary>
        /// Email address.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
