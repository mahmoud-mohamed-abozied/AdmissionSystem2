using AdmissionSystem2.Entites;
using AdmissionSystem2.Models;
using AutoMapper;
using AdmissionSystem2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Services
{

    public class AdminRepo:IAdminRepo
    {
        private AdmissionSystemDbContext _AdmissionSystemDbContext;
        private IMapper _Mapper;

        public AdminRepo(AdmissionSystemDbContext admissionSystemDbContext, IMapper Mapper)
        {
            _AdmissionSystemDbContext = admissionSystemDbContext;
            _Mapper = Mapper;
        }
        public MedicalHistory GetMedicalHistory(int applicantId)
        {
            return _AdmissionSystemDbContext.MedicalHistory.FirstOrDefault(a => a.ApplicantId == applicantId);
        }
        public AdmissionDetails GetAdmissionDetails(int applicantId, Guid AdmissionDetailsId)
        {
            return _AdmissionSystemDbContext.AdmissionDetails.Where(a => a.ApplicantId == applicantId && a.Id == AdmissionDetailsId).FirstOrDefault();
        }
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
        public EmergencyContact GetEmergencyContact(int ApplicantId, Guid Id)
        {
            return _AdmissionSystemDbContext.EmergencyContact.FirstOrDefault(a => a.ApplicantId == ApplicantId && a.Id == Id);
        }
        public Document GetDocument(int ApplicantId, int DocumentId)
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
        public AdmissionDetails GetAdmissionDetails(int ApplicantId)
        {
            return _AdmissionSystemDbContext.AdmissionDetails.Where(a => a.ApplicantId == ApplicantId).FirstOrDefault();
        }
        public IEnumerable<EmergencyContact> GetEmergencyContacts(int ApplicantId)
        {
            return _AdmissionSystemDbContext.EmergencyContact.Where(a => a.ApplicantId == ApplicantId).ToList();
        }
        public IEnumerable<Document> GetDocuments(int ApplicantId)
        {
            return _AdmissionSystemDbContext.Documents.Where(a => a.ApplicantId == ApplicantId).ToList();
        }
        public ParentInfo ParentInfoExist(int ApplicantId, Guid ParentInfoId)
        {
            return _AdmissionSystemDbContext.ParentInfo.FirstOrDefault(a => a.ApplicantId == ApplicantId && a.Id == ParentInfoId);
        }
        public Applicant GetApplicant(int _ApplicantId)
        {
            var Applicant = _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == _ApplicantId);
            return Applicant;

        }
        public void DeleteSibling(Sibling sibling)
        {
            _AdmissionSystemDbContext.Sibling.Remove(sibling);
        }
        public Application GetApplication(int ApplicantId)
        {
            Application Application = new Application();
            Application.Applicant = _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == ApplicantId);
            Application.AdmissionDetails = _AdmissionSystemDbContext.AdmissionDetails.FirstOrDefault(a => a.ApplicantId == ApplicantId);
            Application.EmergencyContact = GetEmergencyContacts(ApplicantId);
            Application.Sibling = GetSiblings(ApplicantId);
            Application.MedicalHistory = _Mapper.Map<MedicalHistoryDto>(GetMedicalHistory(ApplicantId));

            Application.ParentInfo = GetParentsInfos(ApplicantId);
            ///   Application.Documents = GetDocuments(ApplicantId);
            return Application;

        }
        public bool CheakAdmissionPeriod()
        {
            return _AdmissionSystemDbContext.AdmissionPeriod.Any();
        }
        public bool AddAdmissionPeriod(AdmissionPeriod AdmissionPeriod)
        {
            if (!CheakAdmissionPeriod())
            {
                _AdmissionSystemDbContext.AdmissionPeriod.Add(AdmissionPeriod);
                return false;
            }
            return CheakAdmissionPeriod();
        }
        public AdmissionPeriod GetAdmissionPeriod()
        {
            return _AdmissionSystemDbContext.AdmissionPeriod.FirstOrDefault();
        }
        public string GetPeriodLeft() {
            DateTime startTime = DateTime.Now;
            string Date = GetAdmissionPeriod().EndDate + " "+GetAdmissionPeriod().EndTime;
            DateTime oDate = DateTime.ParseExact(Date, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan span = oDate.Subtract(startTime);
            String yourString = string.Format("{0} days, {1} hours",
                span.Days, span.Hours);
            return yourString;
        }
        public void ExtendAdmissionPeriod(string ExtraPeriod)
        {
            string[] period = ExtraPeriod.Split('/');
            var AdmissionPeriod = GetAdmissionPeriod();
            string[] Admission_Date = AdmissionPeriod.EndDate.Split('-');
            string Admission_Time = AdmissionPeriod.EndTime.Substring(0,2);
            string h= AdmissionPeriod.EndTime.Substring(2);
            int Days=  Int16.Parse(Admission_Date[2]) + Int16.Parse(period[0]);
            int Hours = Int16.Parse(period[1]) + Int16.Parse(Admission_Time);
            int Month = Int16.Parse(Admission_Date[1]);
            if (Hours > 24)
            {
                Days += 1;
                Hours -= 24;
            }
            if (Days > 30)
            {
                Month += 1;
                Days -= 30;
            }
            if (Month < 10)
            {
                Admission_Date[1] = "0"+Month.ToString();
            }
            else {
                Admission_Date[1] = Month.ToString();
            }
            if (Days < 10)
            {
                Admission_Date[2] = "0"+Days.ToString();
            }
            else
            {
                Admission_Date[2] = Days.ToString();
            }
            if (Hours < 10)
            {
                Admission_Time = "0"+Hours.ToString();
            }
            else
            {
                Admission_Time = Hours.ToString();
            }
            AdmissionPeriod.EndDate = Admission_Date[0] +"-"+ Admission_Date[1]+"-"+ Admission_Date[2];
            AdmissionPeriod.EndTime = Admission_Time + h;
            _AdmissionSystemDbContext.AdmissionPeriod.Update(AdmissionPeriod);
        }
        public bool Save()
        {
            return (_AdmissionSystemDbContext.SaveChanges() >= 0);
            }
    public class AdminRepo : IAdminRepo
    {
        private AdmissionSystemDbContext _AdmissionSystemDbContext;

        public AdminRepo(AdmissionSystemDbContext admissionSystemDbContext)
        {
            _AdmissionSystemDbContext = admissionSystemDbContext;
        }
        public IEnumerable<Application> GetApplication(ResourceParameters resourceParameters)
        {
            return _AdmissionSystemDbContext.Appliaction
                .Skip(resourceParameters.PageSize*(resourceParameters.PageNumber-1))
                .Take(resourceParameters.PageSize)
                .ToList();
        }
    }
}
