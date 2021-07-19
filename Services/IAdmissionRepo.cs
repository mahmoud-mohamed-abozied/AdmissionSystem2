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
        void AddParentInfo(int _ApplicantId, ParentInfo parentInfo);

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
        Applicant GetApplicant(int ApplicantId);
        PagedList<Applicant> GetApplicants(ResourceParameters resourceParameters);
        //IEnumerable<ParentInfo> GetParentsInfos(int ApplicantId);
        //ParentInfo GetParentInfos(int ApplicantId, string Gender);
        // AdmissionDetails GetAdmissionDetails(int ApplicantId);
        AdmissionDetails GetAdmissionDetails(int applicantId, Guid AdmissionDetailsId);
        //IEnumerable<EmergencyContact> GetEmergencyContacts(int ApplicantId);
        //EmergencyContact GetEmergencyContact(int ApplicantId, Guid Id);
        IEnumerable<Sibling> GetSiblings(int applicantId);
        Sibling GetSibling(int applicantId, Guid siblingId);
        Document GetDocument(int ApplicantId, int DocumentId);
        //IEnumerable<Document> GetDocuments(int ApplicantId);
        //Application GetApplication(int ApplicantId);
   
        
         ParentInfo ParentInfoExist(int ApplicantId, Guid ParentInfoId);
        void DeleteDocument(Document Document);

        void UpdateAdmissionDetails(AdmissionDetails admissionDetails);
        bool ApplicantExist(int _ApplicantId);
        bool Save();
     




    }
}
