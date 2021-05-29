using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ByteSizeLib;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.Infrastructure.Common.Options;

namespace ApplicationSystem.UseCases.Common.Behaviors
{
    /// <summary>
    /// Request with attachment behavior.
    /// </summary>
    public class RequestWithAttachmentBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly AttachmentValidationOptions validationOptions;

        /// <summary>
        /// Request with attachment behavior.
        /// </summary>
        /// <param name="validationOptions">Validation options.</param>
        public RequestWithAttachmentBehavior(IOptions<AttachmentValidationOptions> validationOptions)
        {
            this.validationOptions = validationOptions.Value;
        }

        /// <inheritdoc/>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is IRequestWithAttachment requestWithAttachment)
            {
                if (requestWithAttachment.FormFiles != null)
                {
                    ValidateAttachments(requestWithAttachment.FormFiles);
                }
            }

            return await next();
        }

        private void ValidateAttachments(IFormFileCollection formFiles)
        {
            if (formFiles.Count() > validationOptions.NumberOfAllowedAttachments)
            {
                throw new ValidationException($"The application allows a maximum of {validationOptions.NumberOfAllowedAttachments} files for attachments.");
            }

            var filesSizeBytes = formFiles.Select(f => f.Length).Sum();

            if (ByteSize.FromBytes(filesSizeBytes).MegaBytes > validationOptions.AllowedFileSizeInMegabytes)
            {
                throw new ValidationException($"The application allows a maximum file size for attachments of {validationOptions.AllowedFileSizeInMegabytes} megabytes.");
            }

            if (!formFiles.All(f => validationOptions.AllowedContentTypes.Contains(f.ContentType)))
            {
                throw new ValidationException($"The application does not allow such file format for attachments.");
            }
        }
    }
}
