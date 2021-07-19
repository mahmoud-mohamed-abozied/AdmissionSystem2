using AdmissionSystem2.Entites;
using AdmissionSystem2.Helpers;
using AdmissionSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Services
{
    public interface IAdmissionRepo
    {
        void AddApplicant(Applicant Applicant);
        Applicant GetApplicant(int _ApplicantId);
        void AddParentInfo(int _ApplicantId, ParentInfo parentInfo);
        void AddFamilyStatus(int ApplicantId, FamilyStatus familyStatus);
        void AddEmergencyContact(int ApplicantId, EmergencyContact EmergencyContact);
        void AddDocument(int ApplicantId, Document Document);
        void AddAdmissionDetails(int ApplicantId, AdmissionDetails AdmissionDetails);
        void AddSibling(int applicantId, Sibling sibling);
        void AddMedicalDetails(int applicantId, MedicalHistory medicalHistory);
        void MakePayment(Payment payment);
        void UpdateApplicant(Applicant Applicant);
        void UpdateEmergencyContact(EmergencyContact EmergencyContact);
        void AddDocument(Document Document);
        void UpdateApplicant1(Applicant Applicant);
        void UpdateParentInfo(ParentInfo ParentInfo);
        void DeleteSibling(Sibling sibling);
        void UpdateSibling(Sibling sibling);
        void UpdateMedicalDetails(MedicalHistory medicalHistory);
        PagedList<Applicant> GetApplicants(ResourceParameters resourceParameters);
        
        //AdmissionDetails GetAdmissionDetails(int applicantId, Guid AdmissionDetailsId);
        //IEnumerable<Sibling> GetSiblings(int applicantId);
        //Sibling GetSibling(int applicantId, Guid siblingId);
        //Document GetDocument(int ApplicantId, int DocumentId);
        //IEnumerable<Document> GetDocuments(int ApplicantId);
        //void AddDocument(int ApplicantId, Document Document);
        //Application GetApplication(int ApplicantId);
        //IEnumerable<EmergencyContact> GetEmergencyContacts(int ApplicantId);
        //EmergencyContact GetEmergencyContact(int ApplicantId, Guid Id);
        //IEnumerable<ParentInfo> GetParentsInfos(int ApplicantId);
        //ParentInfo GetParentInfos(int ApplicantId, string Gender);
        // AdmissionDetails GetAdmissionDetails(int ApplicantId);


        ParentInfo ParentInfoExist(int ApplicantId, Guid ParentInfoId);
        //MedicalHistory GetMedicalHistory(int applicantId);
        void DeleteDocument(Document Document);
        void UpdateAdmissionDetails(AdmissionDetails admissionDetails);
        bool ApplicantExist(int _ApplicantId);
        bool Save();




    }
}
