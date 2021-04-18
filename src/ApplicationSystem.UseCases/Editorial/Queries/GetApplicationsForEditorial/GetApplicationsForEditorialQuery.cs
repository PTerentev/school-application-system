using ApplicationSystem.Infrastructure.Common.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ApplicationSystem.UseCases.Editorial.Queries.GetApplicationsForEditorial
{
    /// <summary>
    /// Get applications for editorial query.
    /// </summary>
    public class GetApplicationsForEditorialQuery : IRequest<IEnumerable<ApplicationDto>>
    {
    }
}
