using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;
using ApplicationSystem.UseCases.Authority.Dtos;
using ApplicationSystem.UseCases.Applicant.Dtos;

namespace ApplicationSystem.UseCases.Common.Mapping
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
            CreateMap<Domain.Entities.ApplicationType, ApplicationTypeDto>();
            CreateMap<Domain.Entities.Authority, AuthorityDto>();
            CreateMap<Domain.Entities.Application, ApplicationDto>();
            CreateMap<Attachment, AttachmentInfoDto>()
                .ReverseMap();
            CreateMap<Reply, ReplyDto>();
            CreateMap<Domain.Entities.User, UserDto>();
            CreateMap<IFormFile, AttachmentDto>()
                .ForMember(a => a.Data, opt => opt.MapFrom(f => GetByteArray(f)));
        }

        private static byte[] GetByteArray(IFormFile formFile)
        {
            using var stream = formFile.OpenReadStream();
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
