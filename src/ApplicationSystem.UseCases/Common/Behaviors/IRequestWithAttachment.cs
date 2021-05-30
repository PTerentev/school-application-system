using Microsoft.AspNetCore.Http;

namespace ApplicationSystem.UseCases.Common.Behaviors
{
    /// <summary>
    /// Request with attachment.
    /// The abstraction uses for attachment validation.
    /// </summary>
    public interface IRequestWithAttachment
    {
        /// <summary>
        /// Form files.
        /// </summary>
        IFormFileCollection FormFiles { get; }
    }
}
