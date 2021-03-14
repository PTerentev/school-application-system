using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Common.Application;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Abstractions.Emails;
using ApplicationSystem.Infrastructure.Common.Dtos.Emails;

namespace ApplicationSystem.Infrastructure.UseCases.Editorial.RejectApplication
{
    /// <summary>
    /// Reject application command handler.
    /// </summary>
    internal class RejectApplicationCommandHandler : IRequest<RejectApplicationCommand>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IEmailRendererService emailRendererService;
        private readonly ISmptService smptService;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RejectApplicationCommandHandler(
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
        public async Task<Unit> Handle(RejectApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await dbContext.Applications
                                .Where(a => a.Id == request.ApplicationId)
                                .Where(a => a.Status == ApplicationStatus.Check)
                                .SingleAsync(cancellationToken);

            application.Status = ApplicationStatus.Rejected;
            application.RejectComments = request.RejectComments;
            application.CloseDate = DateTime.UtcNow;

            dbContext.Applications.Update(application);
            await dbContext.SaveChangesAsync(cancellationToken);

            if (application.CreatorUserId != null)
            {
                var user = await dbContext.Users.Where(u => u.Id == application.CreatorUserId).SingleAsync();

                var applicationInfo = mapper.Map<ApplicationInfoDto>(application);
                await SendEmailToUserAsync(user.Email, applicationInfo, CancellationToken.None);
            }

            return Unit.Value;
        }

        private async Task SendEmailToUserAsync(string email, ApplicationInfoDto applicationInfoDto, CancellationToken cancellationToken)
        {
            var content = await emailRendererService.RenderRejectedApplicationContentAsync(applicationInfoDto, cancellationToken);

            var emailDto = new EmailDto()
            {
                EmailContent = content
            };
            emailDto.ToMailAddresses.Add(new MailAddress(email));

            await smptService.SendEmailAsync(emailDto, cancellationToken);
        }
    }
}
