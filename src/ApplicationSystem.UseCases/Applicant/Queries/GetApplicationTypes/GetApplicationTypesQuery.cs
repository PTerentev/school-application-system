using MediatR;
using System.Collections.Generic;
using ApplicationSystem.UseCases.Applicant.Dtos;

namespace ApplicationSystem.UseCases.Applicant.Queries.GetApplicationTypes
{
    /// <summary>
    /// Get application types query.
    /// </summary>
    public class GetApplicationTypesQuery : IRequest<IEnumerable<ApplicationTypeDto>>
    {
    }
}
