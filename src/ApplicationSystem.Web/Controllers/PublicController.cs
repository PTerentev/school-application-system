using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Common.Pagination;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.UseCases.Public.GetPublishedApplications;
using Microsoft.AspNetCore.Authorization;

namespace ApplicationSystem.Web.Controllers
{
    /// <summary>
    /// Public controller.
    /// </summary>
    [ApiController]
    [Route("api/public")]
    [AllowAnonymous]
    public class PublicController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public PublicController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get published applications.
        /// </summary>
        [HttpGet("applications/published")]
        public async Task<PagedListMetadataDto<ApplicationDto>> GetPublishedApplications([FromQuery] GetPublishedApplicationsQuery getPublishedApplicationsQuery, CancellationToken cancellationToken)
        {
            return await mediator.Send(getPublishedApplicationsQuery, cancellationToken);
        }
    }
}
