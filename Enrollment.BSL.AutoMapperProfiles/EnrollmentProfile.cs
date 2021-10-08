using AutoMapper;
using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;

namespace Enrollment.BSL.AutoMapperProfiles
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Personal, PersonalModel>().ReverseMap();
            CreateMap<Academic, AcademicModel>().ReverseMap();
            CreateMap<Admissions, AdmissionsModel>().ReverseMap();
            CreateMap<Certification, CertificationModel>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoModel>()
                .ForMember(dest => dest.ConfirmSocialSecurityNumber, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Institution, InstitutionModel>().ReverseMap();
            CreateMap<MoreInfo, MoreInfoModel>().ReverseMap();
            CreateMap<Residency, ResidencyModel>().ReverseMap();
            CreateMap<StateLivedIn, StateLivedInModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<LookUps, LookUpsModel>().ReverseMap();
        }
    }
}
