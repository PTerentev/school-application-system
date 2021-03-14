using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Common.Pagination;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.UseCases.Public.GetPublishedApplications;

namespace ApplicationSystem.Web.Controllers
{
    /// <summary>
    /// Public controller.
    /// </summary>
    [ApiController]
    [Route("api/public")]
    public class PublicController : Controller
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
        [HttpGet("get-published-applications")]
        public async Task<PagedListMetadataDto<ApplicationDto>> GetPublishedApplications(GetPublishedApplicationsQuery GetPublishedApplicationsQuery, CancellationToken cancellationToken)
        {
            return await mediator.Send(GetPublishedApplicationsQuery, cancellationToken);
        }
    }
}
