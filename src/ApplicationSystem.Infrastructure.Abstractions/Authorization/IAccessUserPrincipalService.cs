namespace ApplicationSystem.Infrastructure.Abstractions.Authorization
{
    /// <summary>
    /// Access user principal service.
    /// </summary>
    public interface IAccessUserPrincipalService
    {
        /// <summary>
        /// Get user Id.
        /// </summary>
        /// <returns>User Id.</returns>
        int? GetUserId();
    }
}
