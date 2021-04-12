using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.Infrastructure.Common.Application;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Abstractions.Emails;
using ApplicationSystem.Infrastructure.Common.Dtos.Emails;

namespace ApplicationSystem.UseCases.Editorial.SendToAuthority
{
    /// <summary>
    /// Send to authority command handler.
    /// </summary>
    internal class SendToAuthorityCommandHandler : IRequestHandler<SendToAuthorityCommand>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IEmailRendererService emailRendererService;
        private readonly ISmptService smptService;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SendToAuthorityCommandHandler(
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
        public async Task<Unit> Handle(SendToAuthorityCommand request, CancellationToken cancellationToken)
        {
            var authority = await dbContext.Authorities.SingleAsync(a => a.Id == request.AuthorityId, cancellationToken);
            var application = await dbContext.Applications
                                .Where(a => a.Id == request.ApplicationId)
                                .Where(a => a.Status == ApplicationStatus.Check)
                                .Where(a => a.AuthorityId == null)
                                .SingleOrDefaultAsync(cancellationToken);

            if (application == null)
            {
                throw new ValidationException("Send to authority operation is not available.");
            }

            application.Status = ApplicationStatus.Sent;
            application.AuthorityId = authority.Id;

            dbContext.Applications.Update(application);
            await dbContext.SaveChangesAsync(cancellationToken);

            var emails = authority.Users.Select(u => u.Email);
            await SendEmailsToAuthority(emails, mapper.Map<ApplicationInfoDto>(application), CancellationToken.None);

            return Unit.Value;
        }

        private async Task SendEmailsToAuthority(IEnumerable<string> emails, ApplicationInfoDto applicationInfo, CancellationToken cancellationToken)
        {
            var content = await emailRendererService.RenderAuthorityContentAsync(applicationInfo, cancellationToken);

            var emailDto = new EmailDto()
            {
                EmailContent = content,
                ToMailAddresses = emails.Select(e => new MailAddress(e)).ToList()
            };

            await smptService.SendEmailAsync(emailDto, cancellationToken);
        }
    }
}
