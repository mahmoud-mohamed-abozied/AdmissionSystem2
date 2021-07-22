using AdmissionSystem2.Entites;
using AdmissionSystem2.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Services
{
    public class AdmissionRepo : IAdmissionRepo
    {
        private AdmissionSystemDbContext _AdmissionSystemDbContext;
        private IMapper _Mapper;

        public AdmissionRepo(AdmissionSystemDbContext admissionSystemDbContext, IMapper Mapper)
        {
            _AdmissionSystemDbContext = admissionSystemDbContext;
            _Mapper = Mapper;
        }

        public void AddApplicant(Applicant Applicant)
        {
             Applicant.Status = "Applied Sucessfuly";
            _AdmissionSystemDbContext.Applicant.Add(Applicant);
        }
        public Applicant GetApplicant(Guid _ApplicantId)
        {
            var Applicant = _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == _ApplicantId);
            return Applicant;

        }


        public void AddParentInfo(Guid _ApplicantId, ParentInfo parentInfo)
        {
            var Applicant = GetApplicant(_ApplicantId);
            if (Applicant != null)
            {
                Applicant.ParentInfo.Add(parentInfo);
            }
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



        public void DeleteDocument(Document Document)
        {
            _AdmissionSystemDbContext.Documents.Remove(Document);

        }

        public void AddDocument(Document Document)
        {
            _AdmissionSystemDbContext.Documents.Add(Document);
        }


        public void UpdateEmergencyContact(EmergencyContact EmergencyContact)
        {
            _AdmissionSystemDbContext.Update(EmergencyContact);
        }

        public bool ApplicantExist(Guid _ApplicantId)
        {
            var Applicant = _AdmissionSystemDbContext.Applicant.Any(a => a.ApplicantId == _ApplicantId);
            return Applicant;
        }
        public void AddEmergencyContact(Guid ApplicantId, EmergencyContact EmergencyContact)
        {
            var Applicant = GetApplicant(ApplicantId);
            if (Applicant != null)
            {
                Applicant.EmergencyContact.Add(EmergencyContact);
            }
        }
        public void AddAdmissionDetails(Guid ApplicantId, AdmissionDetails AdmissionDetails)
        {
            var Applicant = GetApplicant(ApplicantId);
            if (Applicant != null)
            {
                Applicant.AdmissionDetails = AdmissionDetails;
            }
        }
        public void AddDocument(Guid ApplicantId, Document Document)
        {
            var Applicant = GetApplicant(ApplicantId);
            if (Applicant != null)
            {
                Applicant.Documents.Add(Document);
            }
        }

        public void AddSibling(Guid applicantId, Sibling sibling)
        {
            var Applicant = GetApplicant(applicantId);
            if (Applicant != null)
            {
                Applicant.Sibling.Add(sibling);
            }

        }

        public void AddMedicalDetails(Guid applicantId, MedicalHistory medicalHistory)
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
        public AdmissionDetails GetAdmissionDetails(Guid applicantId, Guid AdmissionDetailsId)
        {
            return _AdmissionSystemDbContext.AdmissionDetails.Where(a => a.ApplicantId == applicantId && a.Id == AdmissionDetailsId).FirstOrDefault();
        }
        //public MedicalHistory GetMedicalHistory(int applicantId)


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
        public ParentInfo ParentInfoExist(Guid ApplicantId, Guid ParentInfoId)
        {
            return _AdmissionSystemDbContext.ParentInfo.FirstOrDefault(a => a.ApplicantId == ApplicantId && a.Id == ParentInfoId);
        }

        public void UpdateSibling(Sibling sibling)
        {
            _AdmissionSystemDbContext.Sibling.Update(sibling);
        }

        public void UpdateMedicalDetails(MedicalHistory medicalHistory)
        {
            _AdmissionSystemDbContext.MedicalHistory.Update(medicalHistory);
        }


        public void AddFamilyStatus(Guid ApplicantId, FamilyStatus familyStatus)
        {
            var applicant = GetApplicant(ApplicantId);
            if (applicant != null)
            {
                applicant.Family_Status = familyStatus;
            }
        }
    }
}