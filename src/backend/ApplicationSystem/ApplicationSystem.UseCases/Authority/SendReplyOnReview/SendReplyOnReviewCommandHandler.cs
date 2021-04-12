using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Abstractions.Attachments;
using ApplicationSystem.Infrastructure.Common.Application;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;
using ApplicationSystem.UseCases.Attachments.SaveAttachment;
using System.Collections.Generic;

namespace ApplicationSystem.UseCases.Authority.SendReplyOnReview
{
    /// <summary>
    /// SendReply on review command handler.
    /// </summary>
    internal class SendReplyOnReviewCommandHandler : IRequestHandler<SendReplyOnReviewCommand>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SendReplyOnReviewCommandHandler(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IMediator mediator)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(SendReplyOnReviewCommand request, CancellationToken cancellationToken)
        {
            var application = await dbContext.Applications
                    .Where(a => a.Id == request.ApplicationId)
                    .Where(a => a.Status == ApplicationStatus.Sent)
                    .SingleAsync(cancellationToken);

            application.Status = ApplicationStatus.Review;

            var reply = application.Reply;
            reply.Application = application;
            reply.Text = request.ReplyMessage;
            reply.LastChangeDate = DateTime.UtcNow;

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

            dbContext.Replies.Update(reply);
            dbContext.Applications.Update(application);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
