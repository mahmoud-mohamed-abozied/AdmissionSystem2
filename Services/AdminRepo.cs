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
        public MedicalHistory GetMedicalHistory(Guid applicantId)
        {
            return _AdmissionSystemDbContext.MedicalHistory.FirstOrDefault(a => a.ApplicantId == applicantId);
        }
        public AdmissionDetails GetAdmissionDetails(Guid applicantId, Guid AdmissionDetailsId)
        {
            return _AdmissionSystemDbContext.AdmissionDetails.Where(a => a.ApplicantId == applicantId && a.Id == AdmissionDetailsId).FirstOrDefault();
        }
        public MedicalHistory GetMedicalHistory(Guid applicantId, Guid MedicalHistoryId)

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
            int Slots = (HoursInDay * 60 * InterviewCriteriaForCreation.NumberOfInterviewers) / (InterviewCriteriaForCreation.InterviewDuration + InterviewCriteriaForCreation.BreakTime);
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
            string IntialStartDate = InterviewCriteriaForCreation.StartDate;
            int Duration = InterviewCriteriaForCreation.InterviewDuration + InterviewCriteriaForCreation.BreakTime;
            string IntialStartTime = InterviewCriteriaForCreation.StartTime;
            string[] StartHours = InterviewCriteriaForCreation.StartTime.Split(":");
            string[] EndHours = InterviewCriteriaForCreation.EndTime.Split(":");
            Double EndMinutesInDay = (Double.Parse(EndHours[0]) * 60) + Double.Parse(EndHours[1]) - Duration;
            Double StartMinutesInDay = Double.Parse(StartHours[0]) * 60 + Double.Parse(StartHours[1]);

            int EndMonth = Int16.Parse(EndDate[1]);
            int EndDay = Int16.Parse(EndDate[2]);
            int i = 0;
            while (i < ApplicantsCount)
            {
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
                i = i + InterviewCriteriaForCreation.NumberOfInterviewers;


                string[] CurrStartHours = new string[2];
                CurrStartHours[0] = IntialStartTime.Substring(0, 2).ToString();
                CurrStartHours[1] = IntialStartTime.Substring(3).ToString();
                int NextStartTimeInMinutes = ((Int16.Parse(CurrStartHours[0])) * 60) + Int16.Parse(CurrStartHours[1]) + Duration;
                if (NextStartTimeInMinutes > EndMinutesInDay)
                {
                    if ((StartMinutesInDay % 60) < 10)
                    {
                        IntialStartTime = (StartMinutesInDay / 60).ToString() + ":" + "0" + (StartMinutesInDay % 60).ToString();
                    }
                    else
                    {
                        IntialStartTime = (StartMinutesInDay / 60).ToString() + ":" + (StartMinutesInDay % 60).ToString();
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
                        IntialStartTime = (NextStartTimeInMinutes / 60).ToString() + ":" + "0" + (NextStartTimeInMinutes % 60).ToString();
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
        public Sibling GetSibling(Guid applicantId, Guid siblingId)
        {
            return _AdmissionSystemDbContext.Sibling.Where(a => a.ApplicantId == applicantId && a.SibilingId == siblingId).FirstOrDefault();
        }
        public IEnumerable<Sibling> GetSiblings(Guid applicantId)
        {
            return _AdmissionSystemDbContext.Sibling.Where(a => a.ApplicantId == applicantId).OrderBy(a => a.SiblingName).ToList();
        }
        public EmergencyContact GetEmergencyContact(Guid ApplicantId, Guid Id)
        {
            return _AdmissionSystemDbContext.EmergencyContact.FirstOrDefault(a => a.ApplicantId == ApplicantId && a.Id == Id);
        }
        public Document GetDocument(Guid ApplicantId, int DocumentId)
        {
            return _AdmissionSystemDbContext.Documents.FirstOrDefault(a => a.ApplicantId == ApplicantId && a.Id == DocumentId);
        }
        public IEnumerable<ParentInfo> GetParentsInfos(Guid ApplicantId)
        {
            return _AdmissionSystemDbContext.ParentInfo.Where(a => a.ApplicantId == ApplicantId).ToList();

        }
        public ParentInfo GetParentInfos(Guid ApplicantId, string Gender)
        {
            return _AdmissionSystemDbContext.ParentInfo.Where(a => a.ApplicantId == ApplicantId && a.Gender == Gender).FirstOrDefault();

        }
        public AdmissionDetails GetAdmissionDetails(Guid ApplicantId)
        {
            return _AdmissionSystemDbContext.AdmissionDetails.Where(a => a.ApplicantId == ApplicantId).FirstOrDefault();
        }
        public IEnumerable<EmergencyContact> GetEmergencyContacts(Guid ApplicantId)
        {
            return _AdmissionSystemDbContext.EmergencyContact.Where(a => a.ApplicantId == ApplicantId).ToList();
        }
        public IEnumerable<Document> GetDocuments(Guid ApplicantId)
        {
            return _AdmissionSystemDbContext.Documents.Where(a => a.ApplicantId == ApplicantId).ToList();
        }
        public ParentInfo ParentInfoExist(Guid ApplicantId, Guid ParentInfoId)
        {
            return _AdmissionSystemDbContext.ParentInfo.FirstOrDefault(a => a.ApplicantId == ApplicantId && a.Id == ParentInfoId);
        }
        public Applicant GetApplicant(Guid _ApplicantId)
        {
            var Applicant = _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == _ApplicantId);
            return Applicant;

        }
        public void DeleteSibling(Sibling sibling)
        {
            _AdmissionSystemDbContext.Sibling.Remove(sibling);
        }
        public Applicant GetApplication(Guid ApplicantId)
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

         public PagedList<ApplicantsView> GetApplicants(ResourceParameters resourceParameters)
        {
            var collectionBeforePaging = _AdmissionSystemDbContext.Applicant
                .Select(a => new ApplicantsView
                {
                    ApplicantId = a.ApplicantId,
                    FirstName = a.FirstName,
                    SecondName = a.SecondName,
                    LastName = a.LastName,
                    AdmissionDate = a.AdmissionDate,
                    Status = a.Status
                });

            switch (resourceParameters.OrderBy)
            {
                case "ID":
                    collectionBeforePaging = collectionBeforePaging.OrderBy(a => a.ApplicantId).AsQueryable();
                    break;
                case "ID_desc":
                    collectionBeforePaging = collectionBeforePaging.OrderByDescending(a => a.ApplicantId).AsQueryable();
                    break;
                case "Name":
                    collectionBeforePaging = collectionBeforePaging.OrderBy(a => a.FirstName).AsQueryable();
                    break;
                case "Name_desc":
                    collectionBeforePaging = collectionBeforePaging.OrderByDescending(a => a.FirstName).AsQueryable();
                    break;
                case "Date":
                    collectionBeforePaging = collectionBeforePaging.OrderBy(a => a.AdmissionDate).AsQueryable();
                    break;
                case "Date_desc":
                    collectionBeforePaging = collectionBeforePaging.OrderByDescending(a => a.AdmissionDate).AsQueryable();
                    break;
                default:
                    collectionBeforePaging = collectionBeforePaging.OrderByDescending(a => a.ApplicantId).AsQueryable();
                    break;
            }

            if (resourceParameters.StartDate != DateTime.MinValue && resourceParameters.EndDate != DateTime.MinValue)
            {
                
                collectionBeforePaging = collectionBeforePaging.
                    Where(a => a.AdmissionDate >= resourceParameters.StartDate&& a.AdmissionDate <= resourceParameters.EndDate);
            }

            if (!String.IsNullOrEmpty(resourceParameters.Status))
            {
                var StatusForWherecclause = resourceParameters.Status.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging.Where(a => a.Status.ToLower() == StatusForWherecclause);
            }


            if (!String.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                var SearchQueryForWherecclause = resourceParameters.SearchQuery.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.FirstName.ToLower().Contains(SearchQueryForWherecclause)
                    || a.SecondName.ToLower().Contains(SearchQueryForWherecclause)
                        || a.LastName.ToLower().Contains(SearchQueryForWherecclause)
                       || a.ApplicantId.ToString().Contains(SearchQueryForWherecclause));
            }
            return PagedList<ApplicantsView>.Create(collectionBeforePaging, resourceParameters.PageNumber, resourceParameters.PageSize);

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
        public string GetPeriodLeft()
        {
            DateTime startTime = DateTime.Now;
            string Date = GetAdmissionPeriod().EndDate + " " + GetAdmissionPeriod().EndTime;
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
            string Admission_Time = AdmissionPeriod.EndTime.Substring(0, 2);
            string h = AdmissionPeriod.EndTime.Substring(2);
            int Days = Int16.Parse(Admission_Date[2]) + Int16.Parse(period[0]);
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
                Admission_Date[1] = "0" + Month.ToString();
            }
            else
            {
                Admission_Date[1] = Month.ToString();
            }
            if (Days < 10)
            {
                Admission_Date[2] = "0" + Days.ToString();
            }
            else
            {
                Admission_Date[2] = Days.ToString();
            }
            if (Hours < 10)
            {
                Admission_Time = "0" + Hours.ToString();
            }
            else
            {
                Admission_Time = Hours.ToString();
            }
            AdmissionPeriod.EndDate = Admission_Date[0] + "-" + Admission_Date[1] + "-" + Admission_Date[2];
            AdmissionPeriod.EndTime = Admission_Time + h;
            _AdmissionSystemDbContext.AdmissionPeriod.Update(AdmissionPeriod);
        }
        public bool Save()
        {
            return (_AdmissionSystemDbContext.SaveChanges() >= 0);
        }

        public AdministratorOfficer Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var admin = _AdmissionSystemDbContext.AdministratorOfficer.SingleOrDefault(x => x.UserName == username);

            // check if username exists
            if (admin == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, admin.PasswordHash, admin.PasswordSalt))
                return null;

            // authentication successful
            return admin;
        }
     
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }


    }
}
