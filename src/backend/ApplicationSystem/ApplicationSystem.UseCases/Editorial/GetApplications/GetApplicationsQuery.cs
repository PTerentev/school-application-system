using ApplicationSystem.Infrastructure.Common.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ApplicationSystem.UseCases.Editorial.GetApplications
{
    public class GetApplicationsQuery : IRequest<ICollection<ApplicationDto>>
    {
    }
}
