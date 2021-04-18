using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.UseCases.Applicant.Queries.GetApplicationsForApplicant;
using ApplicationSystem.UseCases.Applicant.Commands.SendApplication;

namespace ApplicationSystem.Web.Controllers
{
    /// <summary>
    /// Applicant controller.
    /// </summary>
    [ApiController]
    [Route("api/applicant")]
    public class ApplicantController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public ApplicantController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get applications.
        /// </summary>
        [Authorize]
        [HttpGet("applications/all")]
        public async Task<IEnumerable<ApplicationInfoDto>> GetApplications(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetApplicationsForApplicantQuery(), cancellationToken);
        }

        /// <summary>
        /// Send application.
        /// </summary>
        [HttpPost("applications")]
        public async Task<StatusCodeResult> SendApplication([FromForm] SendApplicationCommand sendApplicationCommand, CancellationToken cancellationToken)
        {
            await mediator.Send(sendApplicationCommand, cancellationToken);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
