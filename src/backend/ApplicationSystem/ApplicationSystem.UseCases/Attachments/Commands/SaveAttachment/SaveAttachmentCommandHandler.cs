using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.Extensions.Logging;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Abstractions.Attachments;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;

namespace ApplicationSystem.UseCases.Attachments.SaveAttachment
{
    /// <summary>
    /// Save attachment command handler.
    /// </summary>
    internal class SaveAttachmentCommandHandler : IRequestHandler<SaveAttachmentCommand, IEnumerable<Attachment>>
    {
        private readonly IAttachmentService attachmentService;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ILogger<SaveAttachmentCommandHandler> logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="attachmentService">Attachment service.</param>
        /// <param name="applicationDbContext">Application database context.</param>
        /// <param name="logger">Logger.</param>
        public SaveAttachmentCommandHandler(
            IAttachmentService attachmentService,
            ApplicationDbContext applicationDbContext,
            ILogger<SaveAttachmentCommandHandler> logger)
        {
            this.attachmentService = attachmentService;
            this.applicationDbContext = applicationDbContext;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Attachment>> Handle(SaveAttachmentCommand request, CancellationToken cancellationToken)
        {
            var attachments = new List<Attachment>();

            try
            {
                foreach (var attachmentDto in request.Attachments)
                {
                    var fileInfo = await attachmentService.SaveAttachmentAsync(attachmentDto, cancellationToken);

                    var attachment = new Attachment()
                    {
                        ContentType = attachmentDto.ContentType,
                        FileKey = fileInfo.FileKey
                    };

                    attachments.Add(attachment);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                await TryToRemoveAttachments(attachments);

                throw new DomainException("Saving attachments was failed.", e);
            }

            applicationDbContext.Attachments.AddRange(attachments);
            await applicationDbContext.SaveChangesAsync(CancellationToken.None);

            return attachments;
        }

        private async Task TryToRemoveAttachments(IEnumerable<Attachment> attachments)
        {
            foreach (var attachment in attachments)
            {
                try
                {
                    await attachmentService.RemoveAttachmentAsync(
                        new AttachmentInfoDto()
                        {
                            FileKey = attachment.FileKey
                        },
                        CancellationToken.None);
                }
                catch
                {
                }
            }
        }
    }
}
