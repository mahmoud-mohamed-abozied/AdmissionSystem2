﻿using AdmissionSystem2.Entites;
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
        public ICollection<Document> GetDocuments()
        {
            var images = _AdmissionSystemDbContext.Documents.ToList();
            return (images);
        }
        public Applicant GetApplicant(int _ApplicantId)
        {
            var Applicant = _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == _ApplicantId);
            return Applicant;

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


        public bool Save()
        {
            return _AdmissionSystemDbContext.SaveChanges() >= 0;
        }

        public MedicalHistory GetMedicalHistory(int applicantId, Guid MedicalHistoryId)
        {
            return _AdmissionSystemDbContext.MedicalHistory.Where(a => a.ApplicantId == applicantId && a.MedicalHistoryId == MedicalHistoryId).FirstOrDefault();
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
