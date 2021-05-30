using MediatR;

namespace ApplicationSystem.UseCases.Admin.Commands.AddApplicationType
{
    /// <summary>
    /// Add application type command.
    /// </summary>
    public class AddApplicationTypeCommand : IRequest
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Authority Id.
        /// </summary>
        public int AuthorityId { get; set; }
    }
}
