using ApplicationSystem.Infrastructure.Common.Dtos;

namespace ApplicationSystem.Infrastructure.UseCases.User.Login
{
    /// <summary>
    /// Login user query result.
    /// </summary>
    public class LoginUserQueryResult
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
