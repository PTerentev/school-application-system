using System.Threading;
using System.Threading.Tasks;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Common.Dtos.Emails;
using ApplicationSystem.Infrastructure.Abstractions.Emails;

namespace ApplicationSystem.Infrastructure.Emails
{
    /// <inheritdoc cref="IEmailRendererService"/>
    public class EmailRendererService : IEmailRendererService
    {
        /// <inheritdoc/>
        public Task<EmailContentDto> RenderAuthorityContentAsync(ApplicationInfoDto applicationDto, CancellationToken cancellationToken)
        {
            var content = new EmailContentDto()
            {
                Subject = "Система жалоб и предложений: Нужно дать ответ на жалобу",
                Body = $"<p>Название: {applicationDto.Name}</p>" +
                        $"<p>Описание: {applicationDto.Description}</p>"
            };

            return Task.FromResult(content);
        }

        /// <inheritdoc/>
        public Task<EmailContentDto> RenderNewApplicationContentAsync(ApplicationInfoDto applicationDto, CancellationToken cancellationToken)
        {
            var content = new EmailContentDto()
            {
                Subject = "Система жалоб и предложений: Новая жалоба",
                Body = $"<p>Название: {applicationDto.Name}</p>" +
                        $"<p>Описание: {applicationDto.Description}</p>"
            };

            return Task.FromResult(content);
        }

        /// <inheritdoc/>
        public Task<EmailContentDto> RenderPublishedApplicationContentAsync(ApplicationInfoDto applicationDto, CancellationToken cancellationToken)
        {
            var content = new EmailContentDto()
            {
                Subject = "Система жалоб и предложений: Ответ на жалобу был опубликован",
                Body = $"<p>Название: {applicationDto.Name}</p>" +
                        $"<p>Описание: {applicationDto.Description}</p>" +
                        $"<p>Ответ: {applicationDto.ReplyText}</p>"
            };

            return Task.FromResult(content);
        }

        /// <inheritdoc/>
        public Task<EmailContentDto> RenderRejectedApplicationContentAsync(ApplicationInfoDto applicationDto, CancellationToken cancellationToken)
        {
            var content = new EmailContentDto()
            {
                Subject = "Система жалоб и предложений: Жалоба была отменена",
                Body = $"<p>Название: {applicationDto.Name}</p>" +
                        $"<p>Описание: {applicationDto.Description}</p>" +
                        $"<p>Комментарий: {applicationDto.RejectComments}</p>"
            };

            return Task.FromResult(content);
        }
    }
}
