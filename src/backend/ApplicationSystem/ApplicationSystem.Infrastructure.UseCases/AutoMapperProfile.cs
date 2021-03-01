using AutoMapper;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;

namespace ApplicationSystem.Infrastructure.UseCases
{
    /// <summary>
    /// Auto mapper profile.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Application, ApplicationDto>();
            CreateMap<Attachment, AttachmentInfoDto>();
            CreateMap<Reply, ReplyDto>();
            CreateMap<Application, ApplicationInfoDto>();
            CreateMap<Domain.Entities.User, UserDto>();
        }
    }
}
