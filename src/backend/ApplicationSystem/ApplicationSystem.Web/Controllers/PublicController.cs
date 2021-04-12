using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Common.Pagination;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.UseCases.Public.GetPublishedApplications;

namespace ApplicationSystem.Web.Controllers
{
    /// <summary>
    /// Public controller.
    /// </summary>
    [ApiController]
    [Route("api/public")]
    public class PublicController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PublicController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get published applications.
        /// </summary>
        [HttpGet("applications/published")]
        public async Task<PagedListMetadataDto<ApplicationDto>> GetPublishedApplications([FromQuery] GetPublishedApplicationsQuery GetPublishedApplicationsQuery, CancellationToken cancellationToken)
        {
            return await mediator.Send(GetPublishedApplicationsQuery, cancellationToken);
        }
    }
}
