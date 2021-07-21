using AdmissionSystem2.Entites;
using AdmissionSystem2.Models;
using AutoMapper;
using AdmissionSystem2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AdmissionSystem2.Services
{

    public class AdminRepo : IAdminRepo
    {
        private AdmissionSystemDbContext _AdmissionSystemDbContext;
        private IMapper _Mapper;

        public AdminRepo(AdmissionSystemDbContext admissionSystemDbContext, IMapper Mapper)
        {
            _AdmissionSystemDbContext = admissionSystemDbContext;
            _Mapper = Mapper;
        }
        public int GetApplicantsCount()
        {
            return _AdmissionSystemDbContext.Applicant.Count();
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

        public bool CheakInterviewCriteria(InterviewCriteriaForCreation InterviewCriteriaForCreation)
        {
            var ApplicantCount = GetApplicantsCount();
            string[] StartDate = InterviewCriteriaForCreation.StartDate.Split("-");
            string[] EndDate = InterviewCriteriaForCreation.EndDate.Split("-");
            string StartHours = InterviewCriteriaForCreation.StartTime.Substring(0, 2);
            string EndHours = InterviewCriteriaForCreation.EndTime.Substring(0, 2);
            int HoursInDay = Int16.Parse(EndHours) - Int16.Parse(StartHours);
            int Month = ((Int16.Parse(EndDate[1])) - (Int16.Parse(StartDate[1]))) * 30;
            int Slots = (HoursInDay * 60*InterviewCriteriaForCreation.NumberOfInterviewers) / (InterviewCriteriaForCreation.InterviewDuration + InterviewCriteriaForCreation.BreakTime);
            int AllSlots = (Slots) * ((Month) + (Int16.Parse(EndDate[2]) - Int16.Parse(StartDate[2])));
            if (AllSlots < ApplicantCount)
            {
                return false;
            }
            return true;
        }

        public void AddInterviewDatesForApplicant(InterviewCriteriaForCreation InterviewCriteriaForCreation)
        {
            int ApplicantsCount = _AdmissionSystemDbContext.Applicant.Count();
            string[] EndDate = InterviewCriteriaForCreation.EndDate.Split("-");
            string IntialStartDate= InterviewCriteriaForCreation.StartDate;
            int Duration = InterviewCriteriaForCreation.InterviewDuration+InterviewCriteriaForCreation.BreakTime;
            string IntialStartTime = InterviewCriteriaForCreation.StartTime;
            string[] StartHours = InterviewCriteriaForCreation.StartTime.Split(":");
            string[] EndHours = InterviewCriteriaForCreation.EndTime.Split(":");
            Double EndMinutesInDay = (Double.Parse(EndHours[0]) * 60) + Double.Parse(EndHours[1])-Duration;
            Double StartMinutesInDay= Double.Parse(StartHours[0]) * 60 + Double.Parse(StartHours[1]);
            
            int EndMonth= Int16.Parse(EndDate[1]);
            int EndDay=Int16.Parse(EndDate[2]);
            int i = 0;
            while (i < ApplicantsCount) { 
               // int ApplicantCount = i+1;
                for (int y = 0; y < InterviewCriteriaForCreation.NumberOfInterviewers; y++)
                {
                    if ((i + y) < ApplicantsCount)
                    {
                        var ApplicantList = _AdmissionSystemDbContext.Applicant.ToList();
                        var Applicant = ApplicantList.ElementAt(i + y);
                        Interview interview = new Interview();
                        interview.ApplicantId = Applicant.ApplicantId;
                        interview.InterviewType = "ApplicantInterview";
                        interview.InterviewDate = IntialStartDate;
                        interview.InterviewTime = IntialStartTime;
                        interview.InterviewerName = y.ToString();
                        _AdmissionSystemDbContext.Interview.Add(interview);
                        Save();
                    }
                    else
                    {
                        break;
                    }
                }
                i=i+ InterviewCriteriaForCreation.NumberOfInterviewers;


                string[] CurrStartHours = new string[2]; 
                   CurrStartHours[0]= IntialStartTime.Substring(0,2).ToString();
                CurrStartHours[1] = IntialStartTime.Substring(3).ToString();
                int NextStartTimeInMinutes = ((Int16.Parse(CurrStartHours[0])) *60) + Int16.Parse(CurrStartHours[1]) + Duration ; 
                if (NextStartTimeInMinutes > EndMinutesInDay)
                {
                    if ((StartMinutesInDay % 60) < 10)
                    {
                        IntialStartTime = (StartMinutesInDay / 60).ToString() + ":" + "0" + (StartMinutesInDay % 60).ToString();
                    }
                    else {
                        IntialStartTime = (StartMinutesInDay / 60).ToString() + ":" +  (StartMinutesInDay % 60).ToString();
                    }
                    
                    string[] StartDate = IntialStartDate.Split("-");
                    int StartMonth = Int16.Parse(StartDate[1]);
                    int StartDay = Int16.Parse(StartDate[2]);
                    int StartYear = Int16.Parse(StartDate[0]);
                    StartDay += 1;
                    if (StartDay > 30)
                    {
                        StartMonth += 1;
                        if (StartMonth > 12)
                        {
                            StartYear += 1;
                            StartMonth -= 12;
                        }
                        StartDay -= 30;
                    }
                    IntialStartDate = StartYear.ToString() + "-" + StartMonth.ToString() + "-" + StartDay.ToString();

                }
                else
                {
                    if ((NextStartTimeInMinutes % 60) < 10)
                    {
                        IntialStartTime = (NextStartTimeInMinutes / 60).ToString() + ":" + "0" + (NextStartTimeInMinutes %  60).ToString();
                    }
                    else
                    {
                        IntialStartTime = (NextStartTimeInMinutes / 60).ToString() + ":" + (NextStartTimeInMinutes % 60).ToString();
                    }
                }
            }
            

        }
        public void AddInterviewCritera(InterviewCriteria interviewCriteria)
        {
            _AdmissionSystemDbContext.InterviewCriteria.Add(interviewCriteria);

        }
        public bool CheakInterviewCriteria()
        {
            return _AdmissionSystemDbContext.InterviewCriteria.Any();
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
        public Applicant GetApplication(int ApplicantId)
        {
            Applicant Applicant = _AdmissionSystemDbContext.Applicant
                .Include(a => a.ParentInfo)
                .Include(a => a.AdmissionDetails)
                .Include(a => a.EmergencyContact)
                .Include(a => a.Sibling)
                .Include(a => a.MedicalHistory)
                .Include(a => a.Documents)
                .Include(a => a.Payment)
                .FirstOrDefault(a => a.ApplicantId == ApplicantId);
            /*Application Application = new Application();
            Application.Applicant = _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == ApplicantId);
            Application.AdmissionDetails = _AdmissionSystemDbContext.AdmissionDetails.FirstOrDefault(a => a.ApplicantId == ApplicantId);
            Application.EmergencyContact = GetEmergencyContacts(ApplicantId);
            Application.Sibling = GetSiblings(ApplicantId);
            Application.MedicalHistory = _Mapper.Map<MedicalHistoryDto>(GetMedicalHistory(ApplicantId));
            Application.ParentInfo = GetParentsInfos(ApplicantId);*/
            ///   Application.Documents = GetDocuments(ApplicantId);
            return Applicant;

        }

        /*public IEnumerable<Application> GetApplications(ResourceParameters resourceParameters)
        {
            return _AdmissionSystemDbContext.Appliaction
                .Skip(resourceParameters.PageSize * (resourceParameters.PageNumber - 1))
                .Take(resourceParameters.PageSize)
                .ToList();
        }
        */
        public bool Save()
        {
            return (_AdmissionSystemDbContext.SaveChanges() >= 0);
        }
    }
}
