using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.Infrastructure.UseCases.User.Login
{
    /// <summary>
    /// Login user command result.
    /// </summary>
    public class LoginUserCommandResult
    {
        /// <summary>
        /// User.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// New token.
        /// </summary>
        public string Token { get; set; }
    }
}
