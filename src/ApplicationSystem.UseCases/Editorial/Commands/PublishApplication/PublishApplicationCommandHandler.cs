using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ApplicationSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ApplicationSystem.Infrastructure.Common.Application;
using ApplicationSystem.Infrastructure.Abstractions.Emails;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Common.Dtos.Emails;
using Saritasa.Tools.Domain.Exceptions;

namespace ApplicationSystem.UseCases.Editorial.Commands.PublishApplication
{
    /// <summary>
    /// Publish application command handler.
    /// </summary>
    internal class PublishApplicationCommandHandler : IRequestHandler<PublishApplicationCommand>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IEmailRendererService emailRendererService;
        private readonly ISmptService smptService;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PublishApplicationCommandHandler(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IEmailRendererService emailRendererService,
            ISmptService smptService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.emailRendererService = emailRendererService;
            this.smptService = smptService;
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(PublishApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await dbContext.Applications
                                .Where(a => a.Id == request.ApplicationId)
                                .Where(a => a.Status == ApplicationStatus.Review)
                                .SingleOrDefaultAsync(cancellationToken);

            if (application == null)
            {
                throw new DomainException($"Application with Id:{request.ApplicationId} cannot be published.");
            }

            application.Status = ApplicationStatus.Published;
            application.CloseDate = DateTime.UtcNow;

            dbContext.Applications.Update(application);
            await dbContext.SaveChangesAsync(cancellationToken);

            if (application.CreatorUserId != null)
            {
                var user = await dbContext.Users.Where(u => u.Id == application.CreatorUserId).SingleAsync(CancellationToken.None);

                var applicationInfo = mapper.Map<ApplicationInfoDto>(application);
                await SendEmailToUserAsync(user.Email, applicationInfo, CancellationToken.None);
            }

            return Unit.Value;
        }

        private async Task SendEmailToUserAsync(string email, ApplicationInfoDto applicationInfoDto, CancellationToken cancellationToken)
        {
            var content = await emailRendererService.RenderPublishedApplicationContentAsync(applicationInfoDto, cancellationToken);

            var emailDto = new EmailDto()
            {
                EmailContent = content
            };
            emailDto.ToMailAddresses.Add(new MailAddress(email));

            await smptService.SendEmailAsync(emailDto, cancellationToken);
        }
    }
}
