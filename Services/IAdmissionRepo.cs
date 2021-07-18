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
      //  void AddDocument(int ApplicantId, Document Document);
        //Application GetApplication(int ApplicantId);
        //MedicalHistory GetMedicalHistory(int applicantId);
        ParentInfo ParentInfoExist(int ApplicantId, Guid ParentInfoId);
        void DeleteDocument(Document Document);
        void UpdateAdmissionDetails(AdmissionDetails admissionDetails);
        bool ApplicantExist(int _ApplicantId);
        bool Save();




    }
}
