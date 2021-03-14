using MediatR;

namespace ApplicationSystem.Infrastructure.UseCases.Admin.AddUserToRole
{
    /// <summary>
    /// Add user to role command.
    /// </summary>
    public class AddUserToRoleCommand : IRequest
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Role.
        /// </summary>
        public string Role { get; set; }
    }
}
