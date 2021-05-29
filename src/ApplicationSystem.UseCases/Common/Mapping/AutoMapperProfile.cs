using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Common.Dtos.Attachments;
using ApplicationSystem.UseCases.Authority.Dtos;
using ApplicationSystem.UseCases.Applicant.Dtos;
using ApplicationSystem.UseCases.Attachments.Dtos;
using System;
using System.Linq;

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
            CreateMap<Domain.Entities.Application, ApplicationDto>()
                .ForMember(a => a.Attachments, opt => opt.MapFrom(a => a.Attachments.Select(a => a.Id)));
            CreateMap<Attachment, AttachmentInfoDto>()
                .ReverseMap();
            CreateMap<Reply, ReplyDto>();
            CreateMap<Domain.Entities.User, UserDto>();
            CreateMap<IFormFile, AttachmentDto>()
                .ForMember(a => a.Data, opt => opt.MapFrom(f => GetByteArray(f)));

            CreateMap<AttachmentDto, AttachmentBase64Dto>()
                .ForMember(a => a.Data, opt => opt.MapFrom(a => MapToBase64(a.Data)));
        }

        private static byte[] GetByteArray(IFormFile formFile)
        {
            using var stream = formFile.OpenReadStream();
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        private static string MapToBase64(byte[] bytes) => Convert.ToBase64String(bytes);
    }
}
