using AdmissionSystem2.Entites;
using AdmissionSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Services
{
     public interface IAdminRepo
    {
        IEnumerable<Document> GetDocuments(int ApplicantId);
        Application GetApplication(int ApplicantId);
        Applicant GetApplicant(int ApplicantId);
        Document GetDocument(int ApplicantId, int DocumentId);
        EmergencyContact GetEmergencyContact(int ApplicantId, Guid Id);
        ParentInfo GetParentInfos(int ApplicantId, string Gender);
        IEnumerable<EmergencyContact> GetEmergencyContacts(int ApplicantId);
        AdmissionDetails GetAdmissionDetails(int ApplicantId);
        MedicalHistory GetMedicalHistory(int applicantId);
        Sibling GetSibling(int applicantId, Guid siblingId);
        AdmissionDetails GetAdmissionDetails(int applicantId, Guid AdmissionDetailsId);
        IEnumerable<Sibling> GetSiblings(int applicantId);
        IEnumerable<ParentInfo> GetParentsInfos(int ApplicantId);
        ParentInfo ParentInfoExist(int ApplicantId, Guid ParentInfoId);
        void DeleteSibling(Sibling sibling);
        bool Save();

    }
}
