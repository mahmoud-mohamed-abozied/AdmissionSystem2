using AdmissionSystem2.Entites;
using AdmissionSystem2.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicantForCreation, Applicant>();
            CreateMap<ParentInfoForCreation, ParentInfo>();
            CreateMap<EmergencyContactForCreation, EmergencyContact>();
            CreateMap<SiblingForCreation, Sibling>();
            CreateMap<MedicalHistoryForCreation, MedicalHistory>();
            CreateMap<Sibling, SiblingDto>();
            CreateMap<MedicalHistory, MedicalHistoryDto>();
            CreateMap<AdmissionDetails, AdmissionDetailsForUpdate>().ReverseMap();

            CreateMap<Sibling, SiblingForUpdate>().ReverseMap();
            
            CreateMap<MedicalHistory, MedicalHistoryForUpdate>().ReverseMap();



        }
    }
}
