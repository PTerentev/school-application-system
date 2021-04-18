namespace ApplicationSystem.Infrastructure.Common.Application
{
    /// <summary>
    /// Application status.
    /// </summary>
    public enum ApplicationStatus
    {
        /// <summary>
        /// Check status.
        /// </summary>
        Check = 0,

        /// <summary>
        /// Sent status.
        /// </summary>
        Sent = 1,

        /// <summary>
        /// Review status.
        /// </summary>
        Review = 2,

        /// <summary>
        /// Published status.
        /// </summary>
        Published = 3,

        /// <summary>
        /// Rejected status.
        /// </summary>
        Rejected = 4
    }
}
