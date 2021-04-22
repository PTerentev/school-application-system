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
                    .SingleOrDefaultAsync(cancellationToken);

            if (application == null)
            {
                throw new DomainException("Cannot send reply on review. It can be already sent.");
            }

            application.Status = ApplicationStatus.Review;

            if (application.Reply != null)
            {
                dbContext.Replies.Remove(application.Reply);
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

            return Unit.Value;
        }
    }
}
