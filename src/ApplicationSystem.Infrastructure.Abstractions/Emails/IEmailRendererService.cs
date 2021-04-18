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
        /// <param name="applicationDto">Application.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Email content.</returns>
        Task<EmailContentDto> RenderRejectedApplicationContentAsync(ApplicationInfoDto applicationDto, CancellationToken cancellationToken);

        /// <summary>
        /// Render published application content.
        /// </summary>
        /// <param name="applicationDto">Application.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Email content.</returns>
        Task<EmailContentDto> RenderPublishedApplicationContentAsync(ApplicationInfoDto applicationDto, CancellationToken cancellationToken);

        /// <summary>
        /// Render new application content.
        /// For editorials.
        /// </summary>
        /// <param name="applicationDto">Application.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Email content.</returns>
        Task<EmailContentDto> RenderNewApplicationContentAsync(ApplicationInfoDto applicationDto, CancellationToken cancellationToken);

        /// <summary>
        /// Render authority content.
        /// For editorials.
        /// </summary>
        /// <param name="applicationDto">Application.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Email content.</returns>
        Task<EmailContentDto> RenderAuthorityContentAsync(ApplicationInfoDto applicationDto, CancellationToken cancellationToken);
    }
}
