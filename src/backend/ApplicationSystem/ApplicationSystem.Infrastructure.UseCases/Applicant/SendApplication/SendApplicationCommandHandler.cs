using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Abstractions.Attachments;

namespace ApplicationSystem.Infrastructure.UseCases.Applicant.SendApplication
{
    /// <summary>
    /// Send application command handler.
    /// </summary>
    internal class SendApplicationCommandHandler : IRequestHandler<SendApplicationCommand>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IAttachmentService attachmentService;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SendApplicationCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IAttachmentService attachmentService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.attachmentService = attachmentService;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(SendApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = new Application()
            {
                Name = request.Name,
                Description = request.Description,
                CreatorUserId = request.UserId,
                CreationDate = DateTime.UtcNow
            };

            if (request.FormFiles != null)
            {
                var attachments = attachmentService
                    .GenerateAttachments(request.FormFiles)
                    .Select(a => new Attachment()
                    {
                        Data = a.Data,
                        ContentType = a.ContentType
                    }).ToList();

                dbContext.Attachments.AddRange(attachments);
                application.Attachments = attachments;
            }

            var reply = new Reply();
            application.Reply = reply;

            dbContext.Replies.Add(reply);
            dbContext.Applications.Add(application);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
