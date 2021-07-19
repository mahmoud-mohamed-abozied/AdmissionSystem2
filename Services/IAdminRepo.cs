using AdmissionSystem2.Entites;

using AdmissionSystem2.Models;

using AdmissionSystem2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Services
{

    public interface IAdminRepo
    {
        IEnumerable<Document> GetDocuments(int ApplicantId);
        Applicant GetApplication(int ApplicantId);
        //  Application GetApplication(int ApplicantId);
        int GetApplicantsCount();
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
        AdmissionPeriod GetAdmissionPeriod();
        string GetPeriodLeft();
        void ExtendAdmissionPeriod(string ExtraPeriod);
        void DeleteSibling(Sibling sibling);
        bool CheakAdmissionPeriod();
        bool AddAdmissionPeriod(AdmissionPeriod AdmissionPeriod);
        bool Save();

    }
}