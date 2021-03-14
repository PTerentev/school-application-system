using AutoMapper;

namespace ApplicationSystem.Infrastructure.UseCases.User.Register
{
    /// <summary>
    /// Mapping profile.
    /// </summary>
    internal class MappingProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<RegisterUserCommand, Domain.Entities.User>()
                .ForSourceMember(c => c.Password, opt => opt.DoNotValidate())
                .ForMember(u => u.UserName, opt => opt.MapFrom(c => c.Email));
        }
    }
}
