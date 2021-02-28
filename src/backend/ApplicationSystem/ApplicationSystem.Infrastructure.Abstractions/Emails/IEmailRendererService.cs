using System.Threading;
using System.Threading.Tasks;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Common.Dtos.Emails;

namespace ApplicationSystem.Infrastructure.Abstractions.Emails
{
    /// <summary>
    /// Email renderer service.
    /// </summary>
    public interface IEmailRendererService
    {
        /// <summary>
        /// Render rejected application content.
        /// </summary>
        /// <param name="applicationDto">ApplicationDto</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Email content.</returns>
        Task<EmailContentDto> RenderRejectedApplicationContentAsync(ApplicationDto applicationDto, CancellationToken cancellationToken);
    }
}
