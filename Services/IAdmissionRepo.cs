using AdmissionSystem2.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Services
{
    public interface IAdmissionRepo
    {
        void AddApplicant(Applicant Applicant);
        void AddParentInfo(int _ApplicantId, ParentInfo parentInfo);
        ParentInfo ParentInfoExist(int ApplicantId, Guid ParentInfoId);
        void AddEmergencyContact(int ApplicantId, EmergencyContact EmergencyContact);
        void AddDocument(int ApplicantId, Document Document);
        void AddAdmissionDetails(int ApplicantId, AdmissionDetails AdmissionDetails);
        void DeleteDocument(Document Document);
        void AddSibling(int applicantId, Sibling sibling);
        void AddMedicalDetails(int applicantId, MedicalHistory medicalHistory);
        void MakePayment(Payment payment);
        Applicant GetApplicant(int ApplicantId);
        void UpdateApplicant(Applicant Applicant);
        IEnumerable<ParentInfo> GetParentsInfos (int ApplicantId);
        void UpdateEmergencyContact(EmergencyContact EmergencyContact);
        void AddDocument(Document Document);
        Document GetDocument(int ApplicantId, int DocumentId);
        EmergencyContact GetEmergencyContact (int ApplicantId, Guid Id);
        ParentInfo GetParentInfos(int ApplicantId, string Gender);
        IEnumerable<EmergencyContact> GetEmergencyContacts(int ApplicantId);
        AdmissionDetails GetAdmissionDetails(int ApplicantId);
        void UpdateApplicant1(Applicant Applicant);
        void UpdateParentInfo(ParentInfo ParentInfo);
        MedicalHistory GetMedicalHistory(int applicantId, Guid MedicalHistoryId);
        Sibling GetSibling(int applicantId, Guid siblingId);
        AdmissionDetails GetAdmissionDetails(int applicantId, Guid AdmissionDetailsId);
        IEnumerable<Sibling> GetSiblings(int applicantId);
        void DeleteSibling(Sibling sibling);
        void UpdateSibling(Sibling sibling);
        void UpdateMedicalDetails(MedicalHistory medicalHistory);


        void UpdateAdmissionDetails(AdmissionDetails admissionDetails);



        bool ApplicantExist(int _ApplicantId);

        bool Save();



    }
}
