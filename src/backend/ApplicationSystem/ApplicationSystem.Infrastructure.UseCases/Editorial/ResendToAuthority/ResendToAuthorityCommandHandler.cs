using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Common.Dtos.Emails;
using ApplicationSystem.Infrastructure.Common.Application;
using ApplicationSystem.Infrastructure.Abstractions.Emails;

namespace ApplicationSystem.Infrastructure.UseCases.Editorial.ResendToAuthority
{
    /// <summary>
    /// Resend to authority command handler.
    /// </summary>
    internal class ResendToAuthorityCommandHandler : IRequestHandler<ResendToAuthorityCommand>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IEmailRendererService emailRendererService;
        private readonly ISmptService smptService;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResendToAuthorityCommandHandler(
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
        public async Task<Unit> Handle(ResendToAuthorityCommand request, CancellationToken cancellationToken)
        {
            var application = await dbContext.Applications
                                .Where(a => a.Id == request.ApplicationId)
                                .Where(a => a.Status == ApplicationStatus.Review)
                                .SingleOrDefaultAsync(cancellationToken);

            if (application == null)
            {
                throw new ValidationException("Send to authority operation is not available.");
            }

            application.Status = ApplicationStatus.Sent;
            application.Reply.EditComments = request.EditComments;

            dbContext.Applications.Update(application);
            dbContext.Replies.Update(application.Reply);
            await dbContext.SaveChangesAsync(cancellationToken);

            var authorities = await dbContext.Authorities.Where(a => a.Id == application.AuthorityId).SingleAsync(CancellationToken.None);
            var emails = authorities.Users.Select(u => u.Email);

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
