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
using ApplicationSystem.Infrastructure.UseCases.Common;

namespace ApplicationSystem.Infrastructure.UseCases.Authority.SendReplyOnReview
{
    /// <summary>
    /// SendReply on review command handler.
    /// </summary>
    internal class SendReplyOnReviewCommandHandler : IRequestHandler<SendReplyOnReviewCommand>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IAttachmentService attachmentService;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SendReplyOnReviewCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IAttachmentService attachmentService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.attachmentService = attachmentService;
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
            reply.Text = request.Text;
            reply.LastChangeDate = DateTime.UtcNow;

            if (request.FormFiles != null)
            {
                var attachments = await AttachmentHelper.GenerateAttachmentsAsync(
                    request.FormFiles,
                    attachmentService,
                    mapper,
                    cancellationToken
                    );

                reply.Attachments = attachments;
                dbContext.Attachments.AddRange(attachments);
            }

            application.Reply = reply;

            dbContext.Replies.Update(reply);
            dbContext.Applications.Update(application);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
