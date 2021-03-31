namespace ApplicationSystem.Infrastructure.Common.Options
{
    /// <summary>
    /// Admin account option.
    /// </summary>
    public class AdminAccountOptions
    {
        /// <summary>
        /// Configuration section name.
        /// </summary>
        public const string Name = "AdminAccount";

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }
    }
}
