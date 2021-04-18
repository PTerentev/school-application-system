using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ApplicationSystem.Infrastructure.Abstractions.Emails;
using ApplicationSystem.Infrastructure.Common.Dtos.Emails;

namespace ApplicationSystem.Infrastructure.Emails
{
    /// <inheritdoc cref="ISmptService"/>
    public class FakeSmptService : ISmptService
    {
        private readonly ILogger<FakeSmptService> logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public FakeSmptService(ILogger<FakeSmptService> logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc/>
        public Task SendEmailAsync(EmailDto email, CancellationToken cancellationToken)
        {
            logger.LogDebug($"FAKE Email: Subject: {email.EmailContent.Subject}, Body {email.EmailContent.Body} TO ADDRESS: {email.ToMailAddresses}");
            return Task.CompletedTask;
        }
    }
}
