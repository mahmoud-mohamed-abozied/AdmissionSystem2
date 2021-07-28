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
        private IMailingService _MailingService;
        private IMapper _Mapper;

        public AdminRepo(AdmissionSystemDbContext admissionSystemDbContext,IMailingService MailingService, IMapper Mapper)
        {
            _AdmissionSystemDbContext = admissionSystemDbContext;
            _MailingService = MailingService;
            _Mapper = Mapper;
        }

        public int GetApplicantsCount()
        {
            return _AdmissionSystemDbContext.Applicant.Count();
        }
        public MedicalHistory GetMedicalHistory(Guid applicantId)
        {
            return _AdmissionSystemDbContext.MedicalHistory.Where(a => a.ApplicantId == applicantId).FirstOrDefault();
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
                      //  var ApplicantList = _AdmissionSystemDbContext.Applicant.ToList();
                       // var Applicant = ApplicantList.ElementAt(i + y);
                        Interview interview = new Interview();
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
        public void AcceptApplicant(Guid ApplicantId)
        {
            var Applicant = _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == ApplicantId);
            Applicant.Status = "Accepted";
            _MailingService.SendEmailAsync(Applicant.Email, "School Acception", " Congratulations !               " +
                "We Are Happy to Inform you that you have been accepted in peselvanya Inter National School , You have To attend to school starts from 15/8 to buy your books and Uniform");
            _AdmissionSystemDbContext.Applicant.Update(Applicant);
        }
        public void DeclineApplicant(Guid ApplicantId,string Reason)
        {
            var Applicant = _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == ApplicantId);
            Applicant.Status = "Declined";
            _MailingService.SendEmailAsync(Applicant.Email, "School Declination", " Hi, We are sorry to inform you that you have been declined from our school for reason : " + Reason + ". So we hope you understand our reason and do the actions needed ") ; 
            _AdmissionSystemDbContext.Applicant.Update(Applicant);
        }
        public void SetInterviewForApplicant(Guid ApplicantId)
        {
            var Interview = _AdmissionSystemDbContext.Interview.FirstOrDefault(a => a.ApplicantEmail == null);
            var Applicant = _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == ApplicantId);
            Interview.ApplicantId = ApplicantId;
            Interview.ApplicantEmail = Applicant.Email;
            _AdmissionSystemDbContext.Interview.Update(Interview);
            Applicant.Status = "Waiting For Interview";
            _MailingService.SendEmailAsync(Applicant.Email, "Interview Date", "Hi " + Applicant.FirstName + " We have a set an interview Date for You    Interview Date : " + Interview.InterviewDate + " , Interview Time : " + Interview.InterviewTime + "  So please try to be there at least 10 mins before your interview time");
            _AdmissionSystemDbContext.Applicant.Update(Applicant);
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
            return _AdmissionSystemDbContext.Sibling.Where(a => a.ApplicantId == applicantId && a.Id == siblingId).FirstOrDefault();
        }
        public IEnumerable<Sibling> GetSiblings(Guid applicantId)
        {
            return _AdmissionSystemDbContext.Sibling.Where(a => a.ApplicantId == applicantId).OrderBy(a => a.SiblingName).ToList();
        }
        public EmergencyContact GetEmergencyContact(Guid ApplicantId, Guid Id)
        {
            return _AdmissionSystemDbContext.EmergencyContact.FirstOrDefault(a => a.ApplicantId == ApplicantId && a.Id == Id);
        }
        public Document GetDocument(Guid ApplicantId, string DocumentName)
        {
            return _AdmissionSystemDbContext.Documents.FirstOrDefault(a => a.ApplicantId == ApplicantId && a.DocumentName == DocumentName);
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
        public Application GetApplication(Guid ApplicantId)
        {
            /*Applicant Applicant = _AdmissionSystemDbContext.Applicant
                .Include(a => a.ParentInfo)
                .Include(a => a.AdmissionDetails)
                .Include(a => a.EmergencyContact)
                .Include(a => a.Sibling)
                .Include(a => a.MedicalHistory)
                .Include(a => a.Documents)
                .Include(a => a.Payment)
                .FirstOrDefault(a => a.ApplicantId == ApplicantId);*/
            Application Application = new Application();
            Application.Applicant = _Mapper.Map<ApplicantDto>(_AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == ApplicantId));
            //Application.Applicant = _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == ApplicantId);
            Application.AdmissionDetails = _Mapper.Map<AdmissionDetailsDto>(GetAdmissionDetails(ApplicantId));
            Application.EmergencyContact = _Mapper.Map<IEnumerable<EmergencyContactDto>>(GetEmergencyContacts(ApplicantId));
            Application.Sibling = _Mapper.Map<IEnumerable<SiblingDto>>(GetSiblings(ApplicantId)); 
            Application.MedicalHistory = _Mapper.Map<MedicalHistoryDto>(GetMedicalHistory(ApplicantId));
            Application.ParentInfo = _Mapper.Map<IEnumerable<ParentInfoDto>>(GetParentsInfos(ApplicantId));
            Application.Documents = _Mapper.Map<IEnumerable<DocumentDto>>(GetDocuments(ApplicantId));
            Application.FamilyStatus = _Mapper.Map<FamilyStatusDto>(_AdmissionSystemDbContext.FamilyStatus.FirstOrDefault(a => a.ApplicantId == ApplicantId));
            return Application;

        }
        public string GetNameOfApplicant(Guid _ApplicantId)
        {
            var Applicant = _AdmissionSystemDbContext.Applicant.FirstOrDefault(a => a.ApplicantId == _ApplicantId);
            return (Applicant.FirstName + Applicant.SecondName + Applicant.LastName);

        }

        public PagedList<AppliantsForInterview> GetApplicantsForInterview(ResourceParameters resourceParameters)
        {
            var collectionBeforePaging = _AdmissionSystemDbContext.Interview
                .Select(a => new AppliantsForInterview
                {
                    ApplicantId = a.ApplicantId,
                    ApplicantName = GetNameOfApplicant(a.ApplicantId),
                    InterviewDate = DateTime.ParseExact(a.InterviewDate, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                   // Score = a.Score,
                    Status = _AdmissionSystemDbContext.Applicant.FirstOrDefault(x => x.ApplicantId == a.ApplicantId).Status
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
                    collectionBeforePaging = collectionBeforePaging.OrderBy(a => a.ApplicantName).AsQueryable();
                    break;
                case "Name_desc":
                    collectionBeforePaging = collectionBeforePaging.OrderByDescending(a => a.ApplicantName).AsQueryable();
                    break;
                case "Date":
                    collectionBeforePaging = collectionBeforePaging.OrderBy(a => a.InterviewDate).AsQueryable();
                    break;
                case "Date_desc":
                    collectionBeforePaging = collectionBeforePaging.OrderByDescending(a => a.InterviewDate).AsQueryable();
                    break;
                default:
                    collectionBeforePaging = collectionBeforePaging.OrderByDescending(a => a.ApplicantName).AsQueryable();
                    break;
            }

            if (resourceParameters.StartDate != DateTime.MinValue && resourceParameters.EndDate != DateTime.MinValue)
            {
                collectionBeforePaging = collectionBeforePaging.
                    Where(a => a.InterviewDate >= resourceParameters.StartDate && a.InterviewDate <= resourceParameters.EndDate);
            }

            /* if (!String.IsNullOrEmpty(resourceParameters.Status))
             {
                 var StatusForWherecclause = resourceParameters.Status.Trim().ToLowerInvariant();
                 collectionBeforePaging = collectionBeforePaging.Where(a => a.Status.ToLower() == StatusForWherecclause);
             }*/


            if (!String.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                var SearchQueryForWherecclause = resourceParameters.SearchQuery.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.ApplicantName.ToLower().Contains(SearchQueryForWherecclause)
                       || a.ApplicantId.ToString().Contains(SearchQueryForWherecclause));
            }
            return PagedList<AppliantsForInterview>.Create(collectionBeforePaging, resourceParameters.PageNumber, resourceParameters.PageSize);


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
        public bool ClearAdmissionPeriod()
        {
            /* var CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
             AdmissionPeriod AdmissionPeriod = _AdmissionSystemDbContext.AdmissionPeriod.First();
             if (AdmissionPeriod.EndDate.Equals(CurrentDate))
             {
                 _AdmissionSystemDbContext.AdmissionPeriod.Remove(AdmissionPeriod);
                 return true;
             }*/
            AdmissionPeriod AdmissionPeriod = _AdmissionSystemDbContext.AdmissionPeriod.First();
            DateTime startTime = DateTime.Now;
            string EndDate = GetAdmissionPeriod().EndDate + " " + GetAdmissionPeriod().EndTime;
            DateTime EDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan Endspan = EDate.Subtract(startTime);
            if (Endspan.Days <= -1)
            {
                _AdmissionSystemDbContext.AdmissionPeriod.Remove(AdmissionPeriod);
                return true;
            }
            return false;
        }
       
        public bool AddAdmissionPeriod(AdmissionPeriod AdmissionPeriod)
        {
            if (!CheakAdmissionPeriod())
            {
                _AdmissionSystemDbContext.AdmissionPeriod.Add(AdmissionPeriod);
                return false;
            }
            if (ClearAdmissionPeriod())
            {
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
            string AdmissionMinutes = AdmissionPeriod.EndTime.Substring(3);
            string h = AdmissionPeriod.EndTime.Substring(2);
            int Days = Int16.Parse(Admission_Date[2]) + Int16.Parse(period[0]);
            int Hours = Int16.Parse(period[1]) + Int16.Parse(Admission_Time);
            int Month = Int16.Parse(Admission_Date[1]);
            int Min = Int16.Parse(AdmissionMinutes);
            if (((Hours * 60) + Min) > (24 * 60))
            {
                Days += 1;
                Hours = 0;
            }
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
            string Minutes = Min.ToString();
            if (Min < 10)
            {
                Minutes = "0" + Min.ToString();
            }
            else
            {
                Minutes = Min.ToString();
            }
            AdmissionPeriod.EndDate = Admission_Date[0] + "-" + Admission_Date[1] + "-" + Admission_Date[2];
            AdmissionPeriod.EndTime = Admission_Time + ":" + Minutes;
            _AdmissionSystemDbContext.AdmissionPeriod.Update(AdmissionPeriod);
        }
        public Guid GetCurrentId()
        {
            return _AdmissionSystemDbContext.Applicant.OrderBy(a => a.ApplicantId).Last().ApplicantId;
        }
        public void AddInterviewScore(Guid ApplicantId, InterviewScore InterviewScore)
        {
            var Interview = _AdmissionSystemDbContext.Interview.FirstOrDefault(a => a.ApplicantId == ApplicantId);
            Interview.Score = InterviewScore.Score;
            Interview.ScoreGrade = InterviewScore.ScoreGrade;
            Interview.InterviewerName = InterviewScore.InterviewerName;
            _AdmissionSystemDbContext.Interview.Update(Interview);

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
           // if (!VerifyPasswordHash(password, admin.PasswordHash, admin.PasswordSalt))
             //   return null;

            // authentication successful
            return admin;
        }

        /*  private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
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
          }*/
        public void AddDocumentCriteria(DocumentCriteria documentCriterias)
        {
            _AdmissionSystemDbContext.DocumentCriteria.Add(documentCriterias);

        }
        public IEnumerable<DocumentCriteria> GetDocumentCriterias()
        {
            return _AdmissionSystemDbContext.DocumentCriteria.ToList();
        }
        public void DeleteDocumentCriteria()
        {
            var Criteria = GetDocumentCriterias();
            foreach (var _document in Criteria)
            {
                _AdmissionSystemDbContext.DocumentCriteria.Remove(_document);
            }
        }
        public bool CheakPeriodOpened()
        {
            if (!_AdmissionSystemDbContext.AdmissionPeriod.Any())
            {
                return false;
            }
            DateTime startTime = DateTime.Now;
            string Date = GetAdmissionPeriod().StartDate + " " + GetAdmissionPeriod().StartTime;
            DateTime oDate = DateTime.ParseExact(Date, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan span = startTime.Subtract(oDate);
            /*String yourString = string.Format("{0} days, {1} hours",
                span.Days, span.Hours);*/
            if ((span.Days ^ span.Hours ^ span.Minutes)  <= -1)
            {
                return false;
            }
            string EndDate = GetAdmissionPeriod().EndDate + " " + GetAdmissionPeriod().EndTime;
            DateTime EDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan Endspan = EDate.Subtract(startTime);
            if (Endspan.Days <= -1)
            {
                return false;
            }
            if ((Endspan.Days <=-1 && Endspan.Hours <= -1))
            {
                return false;
            }
            return true;


        }
        public List<DashBoard> StudentStatus()
        {
            var UniqueList=new List<string>();
            //DashBoard dashBoard1 = new DashBoard();
            List<DashBoard> Dashboards = new List<DashBoard>();
            var ApplicantCount = _AdmissionSystemDbContext.Applicant.Count();
            var Applicants = _AdmissionSystemDbContext.Applicant.ToList();
            for(int i = 0; i < ApplicantCount; i++)
            {
                if (!UniqueList.Contains(Applicants.ElementAt(i).Status))
                {
                    UniqueList.Add(Applicants.ElementAt(i).Status);
                }
            }
            var UniqueListCount = new List<int>();
            for(int i = 0; i < UniqueList.Count; i++)
            {
                var count = _AdmissionSystemDbContext.Applicant.Where(a => a.Status == UniqueList.ElementAt(i)).Count();
                UniqueListCount.Add(count);
            }
            for(int i = 0; i < UniqueList.Count; i++)
            {
                DashBoard dashBoard1 = new DashBoard();
                dashBoard1.name = UniqueList.ElementAt(i);
                dashBoard1.value = UniqueListCount.ElementAt(i);
                Dashboards.Add(dashBoard1);

            }
            return Dashboards;
        }
        public List<DashBoard> PaymentStatus()
        {
            var UniqueList = new List<string>();
            //DashBoard dashBoard1 = new DashBoard();
            List<DashBoard> Dashboards = new List<DashBoard>();
            var ApplicantCount = _AdmissionSystemDbContext.Applicant.Count();
            var Applicants = _AdmissionSystemDbContext.Applicant.ToList();
            for (int i = 0; i < ApplicantCount; i++)
            {
                if (!UniqueList.Contains(Applicants.ElementAt(i).PaymentStatus))
                {
                    UniqueList.Add(Applicants.ElementAt(i).PaymentStatus);
                }
            }
            var UniqueListCount = new List<int>();
            for (int i = 0; i < UniqueList.Count; i++)
            {
                var count = _AdmissionSystemDbContext.Applicant.Where(a => a.PaymentStatus == UniqueList.ElementAt(i)).Count();
                UniqueListCount.Add(count);
            }
            for (int i = 0; i < UniqueList.Count; i++)
            {
                DashBoard dashBoard1 = new DashBoard();
                dashBoard1.name = UniqueList.ElementAt(i);
                dashBoard1.value = UniqueListCount.ElementAt(i);
                Dashboards.Add(dashBoard1);

            }
            return Dashboards;
        }
        public List<DashBoard> PlaceOfBirthStatus()
        {
            var UniqueList = new List<string>();
            //DashBoard dashBoard1 = new DashBoard();
            List<DashBoard> Dashboards = new List<DashBoard>();
            var ApplicantCount = _AdmissionSystemDbContext.Applicant.Count();
            var Applicants = _AdmissionSystemDbContext.Applicant.ToList();
            for (int i = 0; i < ApplicantCount; i++)
            {
                if (!UniqueList.Contains(Applicants.ElementAt(i).PlaceOfBirth))
                {
                    UniqueList.Add(Applicants.ElementAt(i).PlaceOfBirth);
                }
            }
            var UniqueListCount = new List<int>();
            for (int i = 0; i < UniqueList.Count; i++)
            {
                var count = _AdmissionSystemDbContext.Applicant.Where(a => a.PlaceOfBirth== UniqueList.ElementAt(i)).Count();
                UniqueListCount.Add(count);
            }
            for (int i = 0; i < UniqueList.Count; i++)
            {
                DashBoard dashBoard1 = new DashBoard();
                dashBoard1.name = UniqueList.ElementAt(i);
                dashBoard1.value = UniqueListCount.ElementAt(i);
                Dashboards.Add(dashBoard1);

            }
            return Dashboards;
        }
        public List<DashBoard> NewStudentStatus()
        {
            var UniqueList = new List<string>();
            //DashBoard dashBoard1 = new DashBoard();
            List<DashBoard> Dashboards = new List<DashBoard>();
            var ApplicantCount = _AdmissionSystemDbContext.AdmissionDetails.Count();
            var Admission = _AdmissionSystemDbContext.AdmissionDetails.ToList();
            for (int i = 0; i < ApplicantCount; i++)
            {
                if (!UniqueList.Contains(Admission.ElementAt(i).NewStudent))
                {
                    UniqueList.Add(Admission.ElementAt(i).NewStudent);
                }
            }
            var UniqueListCount = new List<int>();
            for (int i = 0; i < UniqueList.Count; i++)
            {
                var count = _AdmissionSystemDbContext.AdmissionDetails.Where(a => a.NewStudent == UniqueList.ElementAt(i)).Count();
                UniqueListCount.Add(count);
            }
            for (int i = 0; i < UniqueList.Count; i++)
            {
                DashBoard dashBoard1 = new DashBoard();
                dashBoard1.name = UniqueList.ElementAt(i);
                dashBoard1.value = UniqueListCount.ElementAt(i);
                Dashboards.Add(dashBoard1);

            }
            return Dashboards;
        }
        public List<DashBoard> HasSiblingsStatus()
        {
            var UniqueList = new List<string>();
            //DashBoard dashBoard1 = new DashBoard();
            List<DashBoard> Dashboards = new List<DashBoard>();
            var ApplicantCount = _AdmissionSystemDbContext.Applicant.Count();
            var Applicants = _AdmissionSystemDbContext.Applicant.ToList();
            var Sibligs = _AdmissionSystemDbContext.Sibling.ToList();
            List<string> ApplicantSiblingsCount = new List<string>();
            for(int i=0; i < ApplicantCount; i++)
            {
                var count = _AdmissionSystemDbContext.Sibling.Where(a => a.ApplicantId == Applicants.ElementAt(i).ApplicantId).Count();
                ApplicantSiblingsCount.Add(count.ToString());
            }
            List<string> SiblingsValue = new List<string>();
            int zero=0  ,one=0 , two=0 , other=0 ;
            for(int i = 0; i < ApplicantSiblingsCount.Count; i++)
            {
                if (ApplicantSiblingsCount.ElementAt(i) == "0")
                {
                    zero++;
                }
                else if (ApplicantSiblingsCount.ElementAt(i) == "1")
                {
                    one++;
                }
                else if (ApplicantSiblingsCount.ElementAt(i) == "2")
                {
                    two++;
                }
                else
                {
                    other++;
                }


            }
            DashBoard Zero = new DashBoard();
            DashBoard One = new DashBoard();
            DashBoard Two = new DashBoard();
            DashBoard Other = new DashBoard();
            Zero.name = "0";
            Zero.value = zero;
            One.name = "1";
            One.value = one;
            Two.name = "2";
            Two.value = two;
            Other.name = "Other";
            Other.value = other;
            Dashboards.Add(Zero);
            Dashboards.Add(One);
            Dashboards.Add(Two);
            Dashboards.Add(Other);

            return Dashboards;
        }


    }
}
