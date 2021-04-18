using System.Threading;
using System.Threading.Tasks;
using ApplicationSystem.Infrastructure.Common.Dtos.Emails;

namespace ApplicationSystem.Infrastructure.Abstractions.Emails
{
    /// <summary>
    /// SMPT service.
    /// </summary>
    public interface ISmptService
    {
        /// <summary>
        /// Send email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task SendEmailAsync(EmailDto email, CancellationToken cancellationToken);
    }
}
