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
            CreateMap<Applicant, ApplicantDto>();
            CreateMap<Applicant, ApplicantForUpdate>();
            CreateMap<ApplicantForUpdate, Applicant>();
            CreateMap<ParentInfoForCreation, ParentInfo>();
            CreateMap<ParentInfo, ParentInfoDto>();
            CreateMap<ParentInfo, ParentInfoForUpdate>();
            CreateMap<ParentInfoForUpdate, ParentInfo>();
            CreateMap<EmergencyContactForCreation, EmergencyContact>();
            CreateMap<EmergencyContact, EmergencyContactDto>();
            CreateMap<EmergencyContact, EmergencyContactForUpdate>();
            CreateMap<EmergencyContactForUpdate, EmergencyContact>();
            CreateMap<AdmissionDetailsForCreation, AdmissionDetails>();
            CreateMap<AdmissionDetails, AdmissionDetailsDto>();
            //  CreateMap<Sibling, SiblingDto>();
            // CreateMap<MedicalHistory, MedicalHistoryDto>();
            CreateMap<SiblingForCreation, Sibling>();
            CreateMap<Sibling, SiblingForUpdate>();
            CreateMap<SiblingForUpdate, Sibling>();
            CreateMap<MedicalHistoryForCreation, MedicalHistory>();
            CreateMap<MedicalHistory, MedicalHistoryForUpdate>();
            CreateMap<MedicalHistoryForUpdate, MedicalHistory>();

        }
    }
}
