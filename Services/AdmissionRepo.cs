﻿using AdmissionSystem2.Entites;
using AdmissionSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Services
{
    public class AdmissionRepo : IAdmissionRepo
    {
        private AdmissionSystemDbContext _AdmissionSystemDbContext;

        public AdmissionRepo(AdmissionSystemDbContext admissionSystemDbContext)
        {
            _AdmissionSystemDbContext = admissionSystemDbContext;
        }

        public void AddApplicant(Applicant Applicant)
        {
            _AdmissionSystemDbContext.Applicant.Add(Applicant);
        }

        public void AddParentInfo(int _ApplicantId, ParentInfo parentInfo)
        {
            var Applicant = GetApplicant(_ApplicantId);
            if (Applicant != null)
            {
                Applicant.ParentInfo.Add(parentInfo);
            }
        }
        public ParentInfo ParentInfoExist(int ApplicantId,Guid ParentInfoId)
        {
            return _AdmissionSystemDbContext.ParentInfo.FirstOrDefault(a => a.ApplicantId == ApplicantId && a.Id == ParentInfoId);
        }
        public Applicant GetApplicant(int _ApplicantId)
        {
            var Applicant = _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == _ApplicantId);
            return Applicant;

        }

       /* public Application GetApplication(int ApplicantId)
        {
            Application Application = new Application();
            Application.Applicant= _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == ApplicantId);
            Application.AdmissionDetails = GetAdmissionDetails(ApplicantId);
            Application.EmergencyContact = GetEmergencyContacts(ApplicantId);
            Application.Sibling = GetSiblings(ApplicantId);
            Application.MedicalHistory = GetMedicalHistory(ApplicantId);
            Application.ParentInfo = GetParentsInfos(ApplicantId);
            Application.Documents = GetDocuments(ApplicantId);
            return Application;

        }*/

        public IEnumerable<Document>GetDocuments(int ApplicantId)
        {
            return _AdmissionSystemDbContext.Documents.Where(a => a.ApplicantId == ApplicantId).ToList();
        }
       public void DeleteDocument(Document Document)
        {
            _AdmissionSystemDbContext.Documents.Remove(Document);

        }
        public Document GetDocument(int ApplicantId,int DocumentId)
        {
            return _AdmissionSystemDbContext.Documents.FirstOrDefault(a => a.ApplicantId == ApplicantId && a.Id == DocumentId);
        }
        public IEnumerable<ParentInfo> GetParentsInfos(int ApplicantId)
        {
            return _AdmissionSystemDbContext.ParentInfo.Where(a => a.ApplicantId == ApplicantId).ToList();

        }
        public ParentInfo GetParentInfos(int ApplicantId, string Gender)
        {
            return _AdmissionSystemDbContext.ParentInfo.Where(a => a.ApplicantId == ApplicantId && a.Gender == Gender).FirstOrDefault();

        }
        public void AddDocument(Document Document)
        {
            _AdmissionSystemDbContext.Documents.Add(Document);
        }

        public IEnumerable<EmergencyContact> GetEmergencyContacts(int ApplicantId)
        {
            return _AdmissionSystemDbContext.EmergencyContact.Where(a => a.ApplicantId == ApplicantId).ToList();
        }
         public void UpdateEmergencyContact(EmergencyContact EmergencyContact)
        {
            _AdmissionSystemDbContext.Update(EmergencyContact);
        }
        public EmergencyContact GetEmergencyContact(int ApplicantId,Guid Id)
        {
            return _AdmissionSystemDbContext.EmergencyContact.FirstOrDefault(a => a.ApplicantId == ApplicantId && a.Id == Id);
        }
        public AdmissionDetails GetAdmissionDetails(int ApplicantId)
        {
            return _AdmissionSystemDbContext.AdmissionDetails.Where(a => a.ApplicantId == ApplicantId).FirstOrDefault();
        }
        public bool ApplicantExist(int _ApplicantId)
        {
            var Applicant = _AdmissionSystemDbContext.Applicant.Any(a => a.ApplicantId == _ApplicantId);
            return Applicant;
        }
        public void AddEmergencyContact(int ApplicantId, EmergencyContact EmergencyContact)
        {
            var Applicant = GetApplicant(ApplicantId);
            if (Applicant != null)
            {
                Applicant.EmergencyContact.Add(EmergencyContact);
            }
        }
        public void AddAdmissionDetails(int ApplicantId, AdmissionDetails AdmissionDetails)
        {
            var Applicant = GetApplicant(ApplicantId);
            if (Applicant != null)
            {
                Applicant.AdmissionDetails = AdmissionDetails;
            }
        }
        public void AddDocument(int ApplicantId, Document Document)
        {
            var Applicant = GetApplicant(ApplicantId);
            if (Applicant != null)
            {
                Applicant.Documents.Add(Document);
            }
        }

        public void AddSibling(int applicantId, Sibling sibling)
        {
            var Applicant = GetApplicant(applicantId);
            if (Applicant != null)
            {
                Applicant.Sibling.Add(sibling);
            }

        }

        public void AddMedicalDetails(int applicantId, MedicalHistory medicalHistory)
        {
            var Applicant = GetApplicant(applicantId);
            if (Applicant != null)
            {

                Applicant.MedicalHistory = medicalHistory;
            }
        }



        public void MakePayment(Payment payment)
        {
            throw new NotImplementedException();
        }
        public void UpdateApplicant(Applicant Applicant)
        {
             _AdmissionSystemDbContext.Update(Applicant);
        }

        public bool Save()
        {
            return (_AdmissionSystemDbContext.SaveChanges() >= 0);
        }
        

        public AdmissionDetails GetAdmissionDetails(int applicantId, Guid AdmissionDetailsId)
        {
            return _AdmissionSystemDbContext.AdmissionDetails.Where(a => a.ApplicantId == applicantId && a.Id == AdmissionDetailsId).FirstOrDefault();
        }
        //public MedicalHistory GetMedicalHistory(int applicantId)
        public MedicalHistory GetMedicalHistory(int applicantId, Guid MedicalHistoryId)

        {
            return _AdmissionSystemDbContext.MedicalHistory.Where(a => a.ApplicantId == applicantId).FirstOrDefault();
        }

        public Sibling GetSibling(int applicantId, Guid siblingId)
        {
            return _AdmissionSystemDbContext.Sibling.Where(a => a.ApplicantId == applicantId && a.SibilingId == siblingId).FirstOrDefault();
        }

        public IEnumerable<Sibling> GetSiblings(int applicantId)
        {
            return _AdmissionSystemDbContext.Sibling.Where(a => a.ApplicantId == applicantId).OrderBy(a => a.SiblingName).ToList();
        }

        public void DeleteSibling(Sibling sibling)
        {
            _AdmissionSystemDbContext.Sibling.Remove(sibling);
            //Applicant.Sibling.Remove(sibling);
        }
       public void UpdateApplicant1(Applicant Applicant)
        {
            _AdmissionSystemDbContext.Applicant.Update(Applicant);
            //throw new NotImplementedException();
        }
        public void UpdateParentInfo(ParentInfo ParentInfo)
        {
            _AdmissionSystemDbContext.ParentInfo.Update(ParentInfo);
            //throw new NotImplementedException();
        }

        public void UpdateAdmissionDetails(AdmissionDetails admissionDetails)
        {
            _AdmissionSystemDbContext.AdmissionDetails.Update(admissionDetails);
            //throw new NotImplementedException();
        }

        public void UpdateSibling(Sibling sibling)
        {
            _AdmissionSystemDbContext.Sibling.Update(sibling);
            //throw new NotImplementedException();
        }

        public void UpdateMedicalDetails(MedicalHistory medicalHistory)
        {
            _AdmissionSystemDbContext.MedicalHistory.Update(medicalHistory);
            //throw new NotImplementedException();
        }
    }
}
