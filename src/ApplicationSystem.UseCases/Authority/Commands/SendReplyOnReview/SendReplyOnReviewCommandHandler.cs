using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Common.Application;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;
using ApplicationSystem.UseCases.Attachments.SaveAttachment;
using ApplicationSystem.Domain.Entities;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.Infrastructure.Abstractions.Attachments;

namespace ApplicationSystem.UseCases.Authority.Commands.SendReplyOnReview
{
    /// <summary>
    /// SendReply on review command handler.
    /// </summary>
    internal class SendReplyOnReviewCommandHandler : IRequestHandler<SendReplyOnReviewCommand>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IAttachmentService attachmentService;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SendReplyOnReviewCommandHandler(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IMediator mediator,
            IAttachmentService attachmentService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.mediator = mediator;
            this.attachmentService = attachmentService;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(SendReplyOnReviewCommand request, CancellationToken cancellationToken)
        {
            var application = await dbContext.Applications
                    .Where(a => a.Id == request.ApplicationId)
                    .Where(a => a.Status == ApplicationStatus.Sent)
                    .SingleOrDefaultAsync(cancellationToken);

            if (application == null)
            {
                throw new DomainException("Cannot send reply on review. It can be already sent.");
            }

            application.Status = ApplicationStatus.Review;

            var oldAttachmentKeys = Enumerable.Empty<string>();
            if (application.Reply != null)
            {
                oldAttachmentKeys = application.Reply.Attachments.Select(a => a.FileKey).ToList();
                dbContext.Replies.Remove(application.Reply);
                dbContext.Attachments.RemoveRange(application.Reply.Attachments);
            }

            var reply = new Reply()
            {
                ApplicationId = application.Id,
                Text = request.ReplyMessage,
                LastChangeDate = DateTime.UtcNow,
            };

            if (request.FormFiles != null)
            {
                var attachmentDtos = mapper.Map<IEnumerable<AttachmentDto>>(request.FormFiles);

                var attachments = await mediator.Send(
                    new SaveAttachmentCommand()
                    {
                        Attachments = attachmentDtos
                    },
                    cancellationToken);

                reply.Attachments = attachments.ToList();
            }

            application.Reply = reply;

            dbContext.Replies.Add(reply);
            dbContext.Applications.Update(application);
            await dbContext.SaveChangesAsync(cancellationToken);
            await RemoveOldAttachmentFilesAsync(oldAttachmentKeys);

            return Unit.Value;
        }

        private async Task RemoveOldAttachmentFilesAsync(IEnumerable<string> fileKeys)
        {
            try
            {
                foreach (var fileKey in fileKeys)
                {
                    await attachmentService.RemoveAttachmentAsync(fileKey, CancellationToken.None);
                }
            }
            catch
            {
            }
        }
    }
}
