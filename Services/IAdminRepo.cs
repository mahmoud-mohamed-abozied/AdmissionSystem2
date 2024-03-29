﻿using AdmissionSystem2.Entites;

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
        IEnumerable<Document> GetDocuments(Guid ApplicantId);
        Application GetApplication(Guid ApplicantId);
        PagedList<ApplicantsView> GetApplicants(ResourceParameters resourceParameters);
        int GetApplicantsCount();
        Applicant GetApplicant(Guid ApplicantId);
        public PagedList<AppliantsForInterview> GetApplicantsForInterview(ResourceParameters resourceParameters);
        Document GetDocument(Guid ApplicantId, string DocumentName);
        EmergencyContact GetEmergencyContact(Guid ApplicantId, Guid Id);
        ParentInfo GetParentInfos(Guid ApplicantId, string Gender);
        IEnumerable<EmergencyContact> GetEmergencyContacts(Guid ApplicantId);
        AdmissionDetails GetAdmissionDetails(Guid ApplicantId);
        MedicalHistory GetMedicalHistory(Guid applicantId);
        Sibling GetSibling(Guid applicantId, Guid siblingId);
        AdmissionDetails GetAdmissionDetails(Guid applicantId, Guid AdmissionDetailsId);
        IEnumerable<Sibling> GetSiblings(Guid applicantId);
        IEnumerable<ParentInfo> GetParentsInfos(Guid ApplicantId);
        ParentInfo ParentInfoExist(Guid ApplicantId, Guid ParentInfoId);
        AdmissionPeriod GetAdmissionPeriod();
        string GetPeriodLeft();
        void AddInterviewDatesForApplicant(InterviewCriteriaForCreation InterviewCriteriaForCreation);
        void ExtendAdmissionPeriod(string ExtraPeriod);
        void AddInterviewCritera(InterviewCriteria interviewCriteria);
        bool CheakInterviewCriteria(InterviewCriteriaForCreation InterviewCriteriaForCreation);
        bool CheakInterviewCriteria();
        void DeleteSibling(Sibling sibling);
        bool CheakAdmissionPeriod();
        bool AddAdmissionPeriod(AdmissionPeriod AdmissionPeriod);
        Guid GetCurrentId();
        bool ClearAdmissionPeriod();
        void AddInterviewScore(Guid ApplicantId, InterviewScore InterviewScore);
        void SetInterviewForApplicant(Guid ApplicantId);
        void DeclineApplicant(Guid ApplicantId, string Reason);
        void AcceptApplicant(Guid ApplicantId);
        bool Save();
        AdministratorOfficer Authenticate(string username, string password);
        void AddDocumentCriteria(DocumentCriteria documentCriterias);
        IEnumerable<DocumentCriteria> GetDocumentCriterias();
        void DeleteDocumentCriteria();
        bool CheakPeriodOpened();
        List <DashBoard> StudentStatus();
        List<DashBoard> PaymentStatus();
        List<DashBoard> PlaceOfBirthStatus();
        List<DashBoard> NewStudentStatus();
        List<DashBoard> HasSiblingsStatus();


    }
}