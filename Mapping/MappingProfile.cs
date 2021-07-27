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
         //   CreateMap<Application, ApplicationDto>().ForMember(dest => dest.AppliantName, src => src.MapFrom(
           //     src => $"{src.Applicant.FirstName}{src.Applicant.SecondName}{src.Applicant.LastName}" ));
            CreateMap<Applicant, ApplicantForUpdate>();
            CreateMap<ApplicantForUpdate, Applicant>();
            CreateMap<ParentInfoForCreation, ParentInfo>();
            CreateMap<ParentInfo, ParentInfoDto>();
            CreateMap<ParentInfo, ParentInfoForUpdate>();
            CreateMap<ParentInfoForUpdate, ParentInfo>();
            CreateMap<EmergencyContactForCreation, EmergencyContact>();
            CreateMap<SiblingForCreation, Sibling>();
            CreateMap<MedicalHistoryForCreation, MedicalHistory>();
            CreateMap<Sibling, SiblingDto>();
            CreateMap<MedicalHistory, MedicalHistoryDto>();
            CreateMap<AdmissionDetails, AdmissionDetailsForUpdate>().ReverseMap();
            CreateMap<Sibling, SiblingForUpdate>().ReverseMap();
            CreateMap<MedicalHistory, MedicalHistoryForUpdate>().ReverseMap();
            CreateMap<FamilyStatusForCreation, FamilyStatus>();
            CreateMap<AdmissionPeriodForCreation, AdmissionPeriod>();
            CreateMap<EmergencyContact, EmergencyContactDto>();
            CreateMap<EmergencyContact, EmergencyContactForUpdate>();
            CreateMap<EmergencyContactForUpdate, EmergencyContact>();
            CreateMap<AdmissionDetailsForCreation, AdmissionDetails>();
            CreateMap<AdmissionDetails, AdmissionDetailsDto>();
            CreateMap<DocumentForCreation, Document>();
            //  CreateMap<Sibling, SiblingDto>();
            // CreateMap<MedicalHistory, MedicalHistoryDto>();
            CreateMap<SiblingForCreation, Sibling>();
            CreateMap<Sibling, SiblingForUpdate>();
            CreateMap<SiblingForUpdate, Sibling>();
            CreateMap<MedicalHistoryForCreation, MedicalHistory>();
            CreateMap<MedicalHistory, MedicalHistoryForUpdate>();
            CreateMap<MedicalHistoryForUpdate, MedicalHistory>();
            CreateMap<MedicalHistory, MedicalHistoryDto>();
            CreateMap<InterviewCriteriaForCreation, InterviewCriteria>();
            CreateMap<AdministratorOfficer, UserLoginModel>();
            CreateMap<DocumentDto, Document>().ReverseMap();
            CreateMap<FamilyStatusDto, FamilyStatus>().ReverseMap();
            CreateMap<DocumentCriteria, DocumentCriteriaForCreation>().ReverseMap();
        }
    }
}
