using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Abstractions.Emails;
using ApplicationSystem.Infrastructure.Common.Dtos.Emails;
using ApplicationSystem.Infrastructure.Abstractions.Authorization;
using ApplicationSystem.UseCases.Attachments.SaveAttachment;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;

namespace ApplicationSystem.UseCases.Applicant.Commands.SendApplication
{
    /// <summary>
    /// Send application command handler.
    /// </summary>
    internal class SendApplicationCommandHandler : IRequestHandler<SendApplicationCommand>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IAccessUserPrincipalService accessUserPrincipalService;
        private readonly IMapper mapper;
        private readonly IEmailRendererService emailRendererService;
        private readonly ISmptService smptService;
        private readonly UserManager<Domain.Entities.User> userManager;
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SendApplicationCommandHandler(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IEmailRendererService emailRendererService,
            ISmptService smptService,
            UserManager<Domain.Entities.User> userManager,
            IAccessUserPrincipalService accessUserPrincipalService,
            IMediator mediator)
        {
            this.accessUserPrincipalService = accessUserPrincipalService;
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.emailRendererService = emailRendererService;
            this.smptService = smptService;
            this.userManager = userManager;
            this.mediator = mediator;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(SendApplicationCommand request, CancellationToken cancellationToken)
        {
            var userId = accessUserPrincipalService.GetUserId();
            var application = new Application()
            {
                Name = request.Name,
                Description = request.Description,
                CreatorUserId = userId,
                CreationDate = DateTime.UtcNow
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

                application.Attachments = attachments.ToList();
            }

            dbContext.Applications.Add(application);
            await dbContext.SaveChangesAsync(cancellationToken);

            var editorialUsers = await userManager.GetUsersInRoleAsync(UserRoles.Editor.ToString());
            var emails = editorialUsers.Select(u => u.Email);

            await SendEmailsToEditorial(emails, mapper.Map<ApplicationDto>(application), CancellationToken.None);

            return Unit.Value;
        }

        private async Task SendEmailsToEditorial(IEnumerable<string> emails, ApplicationDto applicationInfo, CancellationToken cancellationToken)
        {
            var content = await emailRendererService.RenderNewApplicationContentAsync(applicationInfo, cancellationToken);

            var emailDto = new EmailDto()
            {
                EmailContent = content,
                ToMailAddresses = emails.Select(e => new MailAddress(e)).ToList()
            };

            await smptService.SendEmailAsync(emailDto, cancellationToken);
        }
    }
}
