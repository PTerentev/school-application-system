namespace ApplicationSystem.Infrastructure.UseCases.User
{
    /// <summary>
    /// JWT token constants.
    /// </summary>
    internal class JwtTokenConstants
    {
        /// <summary>
        /// Issued at date/time claim. https://tools.ietf.org/html/rfc7519#page-10 .
        /// </summary>
        public const string IatClaimType = "iat";
    }
}
