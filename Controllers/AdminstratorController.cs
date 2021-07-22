﻿using AdmissionSystem2.Entites;
using AdmissionSystem2.Helpers;
using AdmissionSystem2.Models;
using AdmissionSystem2.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionSystem2.Controllers
{
    [Authorize]
    [Route("api/Admin")]
    public class AdminstratorController : Controller
    {

        private IAdminRepo _AdmissionRepo;
        private readonly IMapper _Mapper;
        private readonly IHttpContextAccessor _Accessor;
        private readonly LinkGenerator _Generator;
        private readonly IMailingService _mailingService;
        private readonly JWT _JWT;
        
        public AdminstratorController(IAdminRepo AdmissionRepo, IMapper Mapper, IHttpContextAccessor Accessor, LinkGenerator Generator, IMailingService mailingService, IOptions <JWT> jwt)
        {
            _AdmissionRepo = AdmissionRepo;
            _Mapper = Mapper;
            _Accessor = Accessor;
            _Generator = Generator;
            _mailingService = mailingService;
            _JWT = jwt.Value;   
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("{ApplicantId}/ParentInfo/{Gender}")]
        public IActionResult GetParentInfo(Guid ApplicantId, string Gender)
        {
            var ParentInfo = _AdmissionRepo.GetParentInfos(ApplicantId, Gender);
            if (ParentInfo == null)
            {
                return NotFound();
            }
            var ParentInfoToReturn = _Mapper.Map<ParentInfoDto>(ParentInfo);
            return Ok(ParentInfoToReturn);
        }

        [HttpGet("{ApplicantId}/ParentInfo")]
        public IActionResult GetParentsInfo(Guid ApplicantId)
        {
            var ParentInfo = _AdmissionRepo.GetParentsInfos(ApplicantId);
            if (ParentInfo == null)
            {
                return NotFound();
            }
            var ParentInfoToReturn = _Mapper.Map<IEnumerable<ParentInfoDto>>(ParentInfo);
            return Ok(ParentInfoToReturn);
        }

        [HttpGet("ApplicantsCount")]
        public IActionResult GetApplicantsCount()
        {
            var Count = _AdmissionRepo.GetApplicantsCount();
            return Ok(Count);
        }

        
        [HttpGet("{applicantId}/Document/{id}")]
        public IActionResult GetDocument(Guid applicantId, int id)
        {
            var DocumentFromRepo = _AdmissionRepo.GetDocument(applicantId, id);
            if (DocumentFromRepo == null)
            {
                return NotFound();
            }


            string imageBase64Data = Convert.ToBase64String(DocumentFromRepo.Copy);
            string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            return Ok(imageDataURL);

        }


        [HttpGet("{ApplicantId}")]
        public IActionResult GetApplicant(Guid applicantId)
        {
            var Applicant = _AdmissionRepo.GetApplicant(applicantId);
            if (Applicant == null)
            {
                return NotFound();
            }
            var ApplicantToRetrive = _Mapper.Map<ApplicantDto>(Applicant);
            return Ok(ApplicantToRetrive);


        }

        [HttpPost("AdmissionPeriod")]
        public IActionResult AddAdmissionPeriod([FromBody] AdmissionPeriodForCreation AdmissionPeriodForCreation)
        {
            if (AdmissionPeriodForCreation == null)
            {
                return BadRequest();
            }
            var AdmissionPeriodToAdd = _Mapper.Map<AdmissionPeriod>(AdmissionPeriodForCreation);
            if (!_AdmissionRepo.AddAdmissionPeriod(AdmissionPeriodToAdd))
            {
                _AdmissionRepo.Save();
                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("PeriodLeft")]
        public IActionResult GetPeriodLeft()
        {
            var periodLeft = _AdmissionRepo.GetPeriodLeft();
            return Ok(periodLeft);
        }
        [HttpGet("{ApplicantId}/EmergencyContacts")]
        public IActionResult GetEmergencyContacts(Guid ApplicantId)
        {
            var EmergencyContacts = _AdmissionRepo.GetEmergencyContacts(ApplicantId);
            if (EmergencyContacts == null)
            {
                return NotFound();
            }
            var EmergencyContactsToRetrive = _Mapper.Map<IEnumerable<EmergencyContactDto>>(EmergencyContacts);
            return Ok(EmergencyContactsToRetrive);

        }


        [HttpGet("{ApplicantId}/AdmissionDetails")]
        public IActionResult GetAdmissionDetails(Guid ApplicantId)
        {
            var AdmissionDetails = _AdmissionRepo.GetAdmissionDetails(ApplicantId);
            if (AdmissionDetails == null)
            {
                return NotFound();
            }
            var AdmissionDetailsToReturn = _Mapper.Map<AdmissionDetailsDto>(AdmissionDetails);
            return Ok(AdmissionDetailsToReturn);
        }

        [HttpPost("AdmissionPeriodExtension")]
        public IActionResult ExtendAdmissionPeriod([FromBody] string Period)
        {
            if (Period == null)
            {
                return BadRequest();
            }
            _AdmissionRepo.ExtendAdmissionPeriod(Period);
            _AdmissionRepo.Save();
            return Ok();
        }

        [HttpGet("{applicantId}/Medical")]
        public IActionResult GetMedicalDetails(Guid applicantId)
        {
            var MedicalDetailsFromRepo = _AdmissionRepo.GetMedicalHistory(applicantId);
            if (MedicalDetailsFromRepo == null)
            {
                return NotFound();
            }

            var MedicalDetails = _Mapper.Map<MedicalHistoryDto>(MedicalDetailsFromRepo);


            return Ok(MedicalDetails);

        }


        [HttpGet("{applicantId}/Siblings/{id}", Name = "getSibling")]
        public IActionResult GetSibling(Guid applicantId, Guid id)
        {
            if (_AdmissionRepo.GetApplicant(applicantId) == null)
            {
                return NotFound();
            }

            var SiblingFromRepo = _AdmissionRepo.GetSibling(applicantId, id);
            if (SiblingFromRepo == null)
            {
                return NotFound();
            }

            var Sibling = _Mapper.Map<SiblingDto>(SiblingFromRepo);


            return Ok(Sibling);

        }


        [HttpGet("{applicantId}/Siblings")]
        public IActionResult GetSiblings(Guid applicantId)
        {
            if (_AdmissionRepo.GetApplicant(applicantId) == null)
            {
                return NotFound();
            }

            var SiblingsFromRepo = _AdmissionRepo.GetSiblings(applicantId);
            if (SiblingsFromRepo == null)
            {
                return NotFound();
            }

            var Siblings = _Mapper.Map<IEnumerable<SiblingDto>>(SiblingsFromRepo);


            return Ok(Siblings);

        }

        [HttpPost("InterviewCriteria")]
        public IActionResult SetInterviewCriteria([FromBody] InterviewCriteriaForCreation InterviewCriteriaForCreation)
        {
            if (InterviewCriteriaForCreation == null)
            {
                return BadRequest();
            }
            if (_AdmissionRepo.CheakInterviewCriteria())
            {
                return BadRequest("Interview Criteria Already been set ");
            }
            if (!_AdmissionRepo.CheakInterviewCriteria(InterviewCriteriaForCreation))
            {
                return BadRequest("Not Fit Criteria");
            }

            var InterviewCriteriaToAdd = _Mapper.Map<InterviewCriteria>(InterviewCriteriaForCreation);
            _AdmissionRepo.AddInterviewCritera(InterviewCriteriaToAdd);
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("Failed to Add Interview Criteria");
            }
            _AdmissionRepo.AddInterviewDatesForApplicant(InterviewCriteriaForCreation);
            return Ok("Successful Add For Interview Criteria");
        }
        /*   [HttpGet("{ApplicantId}/GetApplication")]
           public IActionResult GetApplication(int ApplicantId)
           {
               if (_AdmissionRepo.GetApplicant(ApplicantId) == null)
               {
                   return NotFound();
               }
               Application ApplicationToReturn = _AdmissionRepo.GetApplication(ApplicantId);
               return Ok(ApplicationToReturn);
           }
        */

        [HttpDelete("{applicantId}/siblings/{id}")]
        public IActionResult DeleteSibling(Guid applicantId, Guid id)
        {
            if (_AdmissionRepo.GetApplicant(applicantId) == null)
            {
                return NotFound();
            }

            var SiblingFromRepo = _AdmissionRepo.GetSibling(applicantId, id);
            if (SiblingFromRepo == null)
            {
                return NotFound();
            }

            _AdmissionRepo.DeleteSibling(SiblingFromRepo);

            if (!_AdmissionRepo.Save())
            {
                throw new Exception("failed to delete a sibling");
            }

            return NoContent();

        }
        [HttpGet("{ApplicantId}/GetApplication")]
        public IActionResult GetApplication(Guid ApplicantId)
        {
            if (_AdmissionRepo.GetApplicant(ApplicantId) == null)
            {
                return NotFound();
            }
            Applicant ApplicationToReturn = _AdmissionRepo.GetApplication(ApplicantId);
            return Ok(ApplicationToReturn);
        }



        [HttpGet("AdmissionApplicants", Name = "GetApplicants")]
        public IActionResult GetApplicants(ResourceParameters resourceParameters)
        {
            var Applicants = _AdmissionRepo.GetApplicants(resourceParameters);

            var previousPageLink = Applicants.HasPrevious ?
                    CreateResourceUri(resourceParameters,
                    ResourceUriType.PreviousPage) : null;

            var nextPageLink = Applicants.HasNext ?
                CreateResourceUri(resourceParameters,
                ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink,
                totalCount = Applicants.TotalCount,
                pageSize = Applicants.PageSize,
                currentPage = Applicants.CurrentPage,
                totalPages = Applicants.TotalPages
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            //var ApplicantToRetrive = _Mapper.Map<IEnumerable<ApplicantDto>>(Applicants);
            return Ok(Applicants);


        }

        private string CreateResourceUri(
            ResourceParameters resourceParameters,
            ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _Generator.GetUriByAction(_Accessor.HttpContext,
                        action: "GetApplicants",
                            values: new
                            {
                                orderBy = resourceParameters.OrderBy,
                                searchQuery = resourceParameters.SearchQuery,
                                //Name = resourceParameters.Name,
                                pageNumber = resourceParameters.PageNumber - 1,
                                pageSize = resourceParameters.PageSize
                            });

                /*  return _urlHelper.Link("GetApplicants",
                    new
                    {
                        //fields = resourceParameters.Fields,
                        orderBy = resourceParameters.OrderBy,
                        searchQuery = resourceParameters.SearchQuery,
                        Name = resourceParameters.Name,
                        pageNumber = resourceParameters.PageNumber - 1,
                        pageSize = resourceParameters.PageSize
                    });*/
                case ResourceUriType.NextPage:
                    return _Generator.GetUriByAction(_Accessor.HttpContext,
                          action: "GetApplicants",
                             values: new
                             {
                                 orderBy = resourceParameters.OrderBy,
                                 searchQuery = resourceParameters.SearchQuery,
                                 //Name = resourceParameters.Name,
                                 pageNumber = resourceParameters.PageNumber + 1,
                                 pageSize = resourceParameters.PageSize
                             });
                case ResourceUriType.Current:
                default:
                    return _Generator.GetUriByAction(_Accessor.HttpContext,
                        action: "GetApplicants",
                             values: new
                             {
                                 orderBy = resourceParameters.OrderBy,
                                 searchQuery = resourceParameters.SearchQuery,
                                 //Name = resourceParameters.Name,
                                 pageNumber = resourceParameters.PageNumber,
                                 pageSize = resourceParameters.PageSize
                             });
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLoginModel model) //Get Token - login
        {
            var admin = _AdmissionRepo.Authenticate(model.UserName, model.Password);

            if (admin == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenString = GenerateJwtToken(admin);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = admin.Id,
                Username = admin.UserName,
                Token = tokenString
            });
        }

        public string GenerateJwtToken(AdministratorOfficer admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_JWT.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", admin.Id.ToString()),
                    new Claim("UserName", admin.UserName.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendEmail([FromForm] EmailDto email)
        {
            await _mailingService.SendEmailAsync(email.ToEmail, email.Subject, email.Body, email.Attachments);
            return Ok();
        }





    }
}