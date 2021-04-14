using MediatR;

namespace ApplicationSystem.UseCases.Admin.RemoveAuthority
{
    /// <summary>
    /// Remove authority command.
    /// </summary>
    public class RemoveAuthorityCommand : IRequest
    {
        /// <summary>
        /// Authority Id.
        /// </summary>
        public int AuthorityId { get; set; }
    }
}
