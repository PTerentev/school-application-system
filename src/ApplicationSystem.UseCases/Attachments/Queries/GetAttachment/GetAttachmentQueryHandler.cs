using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ApplicationSystem.Infrastructure.Abstractions.Attachments;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;
using ApplicationSystem.UseCases.Attachments.Dtos;
using AutoMapper;
using ApplicationSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;

namespace ApplicationSystem.UseCases.Attachments.GetAttachment
{
    /// <summary>
    /// Get attachment query handler.
    /// </summary>
    internal class GetAttachmentQueryHandler : IRequestHandler<GetAttachmentQuery, AttachmentBase64Dto>
    {
        private readonly IAttachmentService attachmentService;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        /// <param name="attachmentService">Attachment service.</param>
        /// <param name="mapper">Mapper.</param>
        public GetAttachmentQueryHandler(
            ApplicationDbContext dbContext,
            IAttachmentService attachmentService,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.attachmentService = attachmentService;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<AttachmentBase64Dto> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
        {
            var attachment = await dbContext.Attachments.FirstOrDefaultAsync(a => a.Id == request.AttachmentId, cancellationToken);

            if (attachment == null)
            {
                throw new ValidationException("Attachment is not found.");
            }

            var data = await attachmentService.GetAttachmentAsync(attachment.FileKey, cancellationToken);
            var attachmentDto = new AttachmentDto()
            {
                Data = data,
                ContentType = attachment.ContentType
            };
            return mapper.Map<AttachmentBase64Dto>(attachmentDto);
        }
    }
}
