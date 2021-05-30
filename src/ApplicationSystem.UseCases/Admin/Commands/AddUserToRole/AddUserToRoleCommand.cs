using MediatR;

namespace ApplicationSystem.UseCases.Admin.AddUserToRole
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
        /// Role Id.
        /// </summary>
        public int RoleId { get; set; }
    }
}
