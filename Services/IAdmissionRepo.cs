using AdmissionSystem2.Entites;
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
        Applicant GetApplicant(Guid _ApplicantId);
        void AddParentInfo(Guid _ApplicantId, ParentInfo parentInfo);
        void AddFamilyStatus(Guid ApplicantId, FamilyStatus familyStatus);
        void AddEmergencyContact(Guid ApplicantId, EmergencyContact EmergencyContact);
        void AddDocument(Guid ApplicantId, Document Document);
        void AddAdmissionDetails(Guid ApplicantId, AdmissionDetails AdmissionDetails);
        void AddSibling(Guid applicantId, Sibling sibling);
        void AddMedicalDetails(Guid applicantId, MedicalHistory medicalHistory);
        void MakePayment(Payment payment);
        void UpdateApplicant(Applicant Applicant);
        void UpdateEmergencyContact(EmergencyContact EmergencyContact);
        void AddDocument(Document Document);
        void UpdateApplicant1(Applicant Applicant);
        void UpdateParentInfo(ParentInfo ParentInfo);
        void DeleteSibling(Sibling sibling);
        void UpdateSibling(Sibling sibling);
        void UpdateMedicalDetails(MedicalHistory medicalHistory);
        Applicant Authenticate(string username, string password);

        //Application GetApplication(int ApplicantId);
        //MedicalHistory GetMedicalHistory(int applicantId);
        ParentInfo ParentInfoExist(Guid ApplicantId, Guid ParentInfoId);
        void DeleteDocument(Document Document);
        void UpdateAdmissionDetails(AdmissionDetails admissionDetails);
        bool ApplicantExist(Guid _ApplicantId);
        bool Save();




    }
}