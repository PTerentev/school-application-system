using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.UseCases.Applicant.GetApplications;
using ApplicationSystem.Infrastructure.UseCases.Applicant.SendApplication;

namespace ApplicationSystem.Web.Controllers
{
    /// <summary>
    /// Applicant controller.
    /// </summary>
    [ApiController]
    [Route("api/applicant")]
    public class ApplicantController : Controller
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ApplicantController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get applications.
        /// </summary>
        [Authorize]
        [HttpPost("get-applications")]
        public async Task<ICollection<ApplicationInfoDto>> GetApplications(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetApplicationsForApplicantQuery(), cancellationToken);
        }

        /// <summary>
        /// Send application.
        /// </summary>
        [HttpGet("send-application")]
        public async Task<StatusCodeResult> SendApplication([FromBody] SendApplicationCommand sendApplicationCommand, CancellationToken cancellationToken)
        {
            await mediator.Send(sendApplicationCommand, cancellationToken);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
