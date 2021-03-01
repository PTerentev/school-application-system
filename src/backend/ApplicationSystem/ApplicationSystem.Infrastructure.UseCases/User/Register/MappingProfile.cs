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
                .ForSourceMember(r => r.Password, opt => opt.DoNotValidate());
        }
    }
}
