using AdmissionSystem2.Entites;
using AdmissionSystem2.Models;
using AdmissionSystem2.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Controllers
{
    [Route("api/Admin")]
    public class AdminstratorController : Controller
    {

        private IAdminRepo _AdmissionRepo;
        private readonly IMapper _Mapper;
        public AdminstratorController (IAdminRepo AdmissionRepo,IMapper Mapper)
        {
            _AdmissionRepo = AdmissionRepo;
            _Mapper = Mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("{ApplicantId}/ParentInfo/{Gender}")]
        public IActionResult GetParentInfo(int ApplicantId, string Gender)
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
        public IActionResult GetParentsInfo(int ApplicantId)
        {
            var ParentInfo = _AdmissionRepo.GetParentsInfos(ApplicantId);
            if (ParentInfo == null)
            {
                return NotFound();
            }
            var ParentInfoToReturn = _Mapper.Map<IEnumerable<ParentInfoDto>>(ParentInfo);
            return Ok(ParentInfoToReturn);
        }

       

        /*
         [HttpPut("ApplicantId/Document/Id")]
           public IActionResult UpdateDocument(int ApplicantID,int Id, [FromForm] JsonPatchDocument<DocumentForUpdate> patchDoc)
           {
               if (patchDoc == null)
               {
                   return BadRequest();
               }
               if (_AdmissionRepo.GetApplicant(ApplicantID) == null)
               {
                   return NotFound();

               }
               var DocumentFromRepo = _AdmissionRepo.GetDocument(ApplicantID, Id);
            if (DocumentFromRepo == null)
            {
                return NotFound();
            }
               patchDoc.ApplyTo(DocumentToPatch, ModelState);
                if (!ModelState.IsValid)
                {
                return new UnprocessableEntityObjectResult(ModelState);

                }
                DocumentFromRepo.DocumentName = DocumentToPatch.DocumentName;
                DocumentFromRepo.DocumentType = DocumentToPatch.DocumentType; 
                DocumentFromRepo.Copy = DocumentToPatch.Copy.

        }

        */
        [HttpGet("{applicantId}/Document/{id}")]
        public IActionResult GetDocument(int applicantId, int id)
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
        public IActionResult GetApplicant(int applicantId)
        {
            var Applicant = _AdmissionRepo.GetApplicant(applicantId);
            if (Applicant == null)
            {
                return NotFound();
            }
            var ApplicantToRetrive = _Mapper.Map<ApplicantDto>(Applicant);
            return Ok(ApplicantToRetrive);


        }


        [HttpGet("{ApplicantId}/EmergencyContacts")]
        public IActionResult GetEmergencyContacts(int ApplicantId)
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
        public IActionResult GetAdmissionDetails(int ApplicantId)
        {
            var AdmissionDetails = _AdmissionRepo.GetAdmissionDetails(ApplicantId);
            if (AdmissionDetails == null)
            {
                return NotFound();
            }
            var AdmissionDetailsToReturn = _Mapper.Map<AdmissionDetailsDto>(AdmissionDetails);
            return Ok(AdmissionDetailsToReturn);
        }


        [HttpGet("{applicantId}/Medical")]
        public IActionResult GetMedicalDetails(int applicantId)
        {
            var MedicalDetailsFromRepo = _AdmissionRepo.GetMedicalHistory(applicantId);
            if (MedicalDetailsFromRepo == null)
            {
                return NotFound();
            }

            var MedicalDetails = _Mapper.Map<MedicalHistoryDto>(MedicalDetailsFromRepo);


            return Ok(MedicalDetails);

        }


        [HttpGet("{applicantId}/Siblings/{id}")]
        public IActionResult GetSibling(int applicantId, Guid id)
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
        public IActionResult GetSiblings(int applicantId)
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
        [HttpGet("{ApplicantId}/GetApplication")]
        public IActionResult GetApplication(int ApplicantId)
        {
            if (_AdmissionRepo.GetApplicant(ApplicantId) == null)
            {
                return NotFound();
            }
            Applicant ApplicationToReturn = _AdmissionRepo.GetApplication(ApplicantId);
            return Ok(ApplicationToReturn);
        }


        [HttpDelete("{applicantId}/siblings/{id}")]
        public IActionResult DeleteSibling(int applicantId, Guid id)
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

        
    }
}
