using MediatR;

namespace ApplicationSystem.Infrastructure.UseCases.Admin.AddUserToAuthority
{
    /// <summary>
    /// Add user to authority command.
    /// </summary>
    public class AddUserToAuthorityCommand : IRequest
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Authority Id.
        /// </summary>
        public int AuthorityId { get; set; }
    }
}
