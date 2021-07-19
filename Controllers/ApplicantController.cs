using AdmissionSystem2.Entites;
using AdmissionSystem2.Helpers;
using AdmissionSystem2.Models;
using AdmissionSystem2.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionSystem2.Controllers
{
    [Route("api/applicant")]
    public class ApplicantController : Controller
    {
        
        private IAdmissionRepo _AdmissionRepo;
        private IAdminRepo _AdminRepo;
        private readonly IMapper _Mapper;
        private readonly IHttpContextAccessor _Accessor;
        private readonly LinkGenerator _Generator;
        public ApplicantController(IAdmissionRepo AdmissionRepo,IAdminRepo AdminRepo ,IMapper Mapper, IHttpContextAccessor Accessor, LinkGenerator Generator)
        {
            _AdmissionRepo = AdmissionRepo;
            _AdminRepo = AdminRepo;
            _Mapper = Mapper;
            _Accessor = Accessor;
            _Generator = Generator;
    }
        

        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost()]
        public IActionResult AddApplicant([FromBody] ApplicantForCreation ApplicantForCreation)
        {
            if (ApplicantForCreation == null)
            {
                return BadRequest();
            }
            var final = _Mapper.Map<Applicant>(ApplicantForCreation);
            _AdmissionRepo.AddApplicant(final);
            _AdmissionRepo.Save();

            return Ok();
        }

        [HttpPost("{ApplicantId}/ParentInfo")]
        public IActionResult AddParentInfo(int ApplicantId, [FromBody] ParentInfoForCreation ParentInfoForCreation)
        {
           
            if (ParentInfoForCreation == null)
            {
                return BadRequest();
            }
            if (_AdminRepo.GetApplicant(ApplicantId) == null)
            {
                return NotFound();
            }
            var ParentInfo = _Mapper.Map<ParentInfo>(ParentInfoForCreation);
            _AdmissionRepo.AddParentInfo(ApplicantId, ParentInfo);
            _AdmissionRepo.Save();
            return Ok();

        }

        [HttpPost("{ApplicantId}/EmergencyContact")]
        public IActionResult AddEmergencyContact(int ApplicantId, [FromBody] EmergencyContactForCreation EmergencyContactForCreation)
        {
            if (EmergencyContactForCreation == null)
            {
                return BadRequest();
            }
            if (_AdminRepo.GetApplicant(ApplicantId) == null)
            {
                return NotFound();
            }
            var EmergencyContact = _Mapper.Map<EmergencyContact>(EmergencyContactForCreation);
            _AdmissionRepo.AddEmergencyContact(ApplicantId, EmergencyContact);
            _AdmissionRepo.Save();
            return Ok();
        }
        [HttpPost("{ApplicantId}/FamilyStatus")]
        public IActionResult AddFamilyStatus(int ApplicantId, [FromBody] FamilyStatusForCreation familyStatusForCreation)
        {
            if (familyStatusForCreation == null)
            {
                return BadRequest();
            }
            if (_AdmissionRepo.GetApplicant(ApplicantId) == null)
            {
                return NotFound();
            }
            var FamilyStatus = _Mapper.Map<FamilyStatus>(familyStatusForCreation);
            _AdmissionRepo.AddFamilyStatus(ApplicantId,FamilyStatus );
           if(!_AdmissionRepo.Save())
            {
                throw new Exception("failed to add Family Status ");
            }
            return Ok();
        }

        [HttpPost("{applicantId}/Sibling")]
        public IActionResult AddSibling(int applicantId, [FromBody] SiblingForCreation sibling)

        {
            if (sibling == null)
            {
                return BadRequest();
            }
            //HttpContext.Session.SetString("ApplicantId", "10");
            if (_AdmissionRepo.GetApplicant(applicantId) == null)
            {
                return NotFound();
            }

            var siblingEntity = _Mapper.Map<Sibling>(sibling);
            _AdmissionRepo.AddSibling(applicantId, siblingEntity);

            if (!_AdmissionRepo.Save())
            {
                throw new Exception("failed to add a sibling");
            }

            var siblingToReturn = _Mapper.Map<SiblingDto>(siblingEntity);
            return CreatedAtRoute("getSibling", new { applicantId = applicantId, id = siblingToReturn.SibilingId }, siblingToReturn);

        }

        [HttpPost("{applicantId}/Siblings")]
        public IActionResult AddSiblings(int applicantId, [FromBody] IEnumerable<SiblingForCreation> siblings)
        {
            if (siblings == null)
            {
                return BadRequest();
            }

            if (_AdmissionRepo.GetApplicant(applicantId) == null)
            {
                return NotFound();
            }

            var siblingEntities = _Mapper.Map<IEnumerable<Sibling>>(siblings);

            foreach (var sibling in siblingEntities)
            {
                _AdmissionRepo.AddSibling(applicantId, sibling);
            }

            if (!_AdmissionRepo.Save())
            {
                throw new Exception("failed to add a sibling");
            }

            return Ok();
        }


        [HttpPost("{applicantId}/Medical")]
        public IActionResult AddMedicalDetails(int applicantId, [FromBody] MedicalHistoryForCreation medicalHistory)
        {
            if (medicalHistory == null)
            {
                return BadRequest();
            }

            if (_AdmissionRepo.GetApplicant(applicantId) == null)
            {
                return NotFound();
            }

            var MedicalEntity = _Mapper.Map<MedicalHistory>(medicalHistory);
            _AdmissionRepo.AddMedicalDetails(applicantId, MedicalEntity);

            if (!_AdmissionRepo.Save())
            {
                throw new Exception("failed to add an medical details");
            }

            var MedicalHistoryToReturn = _Mapper.Map<MedicalHistoryDto>(MedicalEntity);
            return CreatedAtRoute("getMedicalHistory", new { applicantId = applicantId, id = MedicalHistoryToReturn.MedicalHistoryId }, MedicalHistoryToReturn);
        }


        [HttpPost("Payment")]
        public async Task<IActionResult> MakePaymentAsync()
        {

            var Auth = JsonConvert.SerializeObject(
                new
                {
                    api_key = "ZXlKMGVYQWlPaUpLVjFRaUxDSmhiR2NpT2lKSVV6VXhNaUo5LmV5SmpiR0Z6Y3lJNklrMWxjbU5vWVc1MElpd2ljSEp2Wm1sc1pWOXdheUk2TVRBek9URTRMQ0p1WVcxbElqb2lhVzVwZEdsaGJDSjkubDZkaGtQMmtaOTFiN1lKQ2Jyd0d6bnNnN0xBTk1tOU9kdUs0LUhXN010OURmeWJRckxORzJhbllxM1pOX2xHQy1RWmZjVDNQeUpTUzBHNTdMbnk2S3c="
                }
                );
            var reqContent1 = new StringContent(Auth, Encoding.UTF8, "application/json");
            var url = "https://accept.paymob.com/api/auth/tokens";

            HttpClient client = new HttpClient();
            var response = await client.PostAsync(url, reqContent1);


            string AuthResult = response.Content.ReadAsStringAsync().Result;

            //Console.WriteLine(AuthResult);

            dynamic marchentData = JObject.Parse(AuthResult);
            //Console.WriteLine(marchentData.token);

            //==========================
            var OrderRegistration = JsonConvert.SerializeObject(
                new
                {
                    auth_token = marchentData.token,
                    delivery_needed = "false",
                    merchant_id = marchentData.profile.user.id,      // merchant_id obtained from step 1
                       amount_cents = "100",               // The price of the order in cents.
                       currency = "EGP"
                }
                );
            var reqContent2 = new StringContent(OrderRegistration, Encoding.UTF8, "application/json");
            url = "https://accept.paymob.com/api/ecommerce/orders";

            client = new HttpClient();
            response = await client.PostAsync(url, reqContent2);

            string OrderResult = response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(OrderResult);


            dynamic orderData = JObject.Parse(OrderResult);
            //Console.WriteLine(orderData.token);

            //====================================================
            // if(method == visa ){
            var GetKey = JsonConvert.SerializeObject(
                new
                {
                    auth_token = marchentData.token, // auth token obtained from step1
                       delivery_needed = "false",
                    order_id = orderData.id,
                    expiration = 3600000000,
                    integration_id = 267725,      // card integration_id will be provided upon signing up,
                       billing_data = new
                    {
                        apartment = "2",
                        email = "Mahmoud@gmail.com",
                        floor = "8",
                        first_name = "Mahmoud",
                        street = "haram",
                        building = "8028",
                        phone_number = "+86(8)9135210487",
                        shipping_method = "PKG",
                        postal_code = "01898",
                        city = "Jaskolskiburgh",
                        country = "CR",
                        last_name = "Ali",
                        state = "Utah"
                    },
                    merchant_id = marchentData.profile.user.id,      // merchant_id obtained from step 1
                       amount_cents = 20000,     //The price should be paid through this payment channel with this payment key token.
                       currency = "EGP"

                }
                );
            var reqContent3 = new StringContent(GetKey, Encoding.UTF8, "application/json");
            url = "https://accept.paymob.com/api/acceptance/payment_keys";

            client = new HttpClient();
            response = await client.PostAsync(url, reqContent3);

            string lastOrderResult = response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(lastOrderResult);

            dynamic lastOrderData = JObject.Parse(lastOrderResult);
            //Console.WriteLine(lastOrderData.token);
            //Console.WriteLine("--------------- url ------------");

            //Console.WriteLine("https://accept.paymob.com/api/acceptance/iframes/241121?payment_token=" + lastOrderData.token);
            Redirect("https://accept.paymob.com/api/acceptance/iframes/241121?payment_token=" + lastOrderData.token);


            /*if (method = aman)
            {
                var GetKioskKey = JsonConvert.SerializeObject(
                new
                {
                    auth_token = marchentData.token, // auth token obtained from step1
                    delivery_needed = "false",
                    order_id = orderData.id,
                    expiration = 3600000000,
                    integration_id = 270214,    // card integration_id will be provided upon signing up,
                    billing_data = new
                    {
                        apartment = "2",
                        email = "Mahmoud@gmail.com",
                        floor = "8",
                        first_name = "Mahmoud",
                        street = "haram",
                        building = "8028",
                        phone_number = "+86(8)9135210487",
                        shipping_method = "PKG",
                        postal_code = "01898",
                        city = "Jaskolskiburgh",
                        country = "CR",
                        last_name = "Ali",
                        state = "Utah"
                    },
                    merchant_id = marchentData.profile.user.id,      // merchant_id obtained from step 1
                    amount_cents = 20000,        //The price should be paid through this payment channel with this payment key token.
                    currency = "EGP"

                }
                );
                var reqResult4 = new StringContent(GetKey, Encoding.UTF8, "application/json");
                url = "https://accept.paymob.com/api/acceptance/payment_keys";

                client = new HttpClient();
                response = await client.PostAsync(url, reqResult4);

                string lastOrderResult = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(lastOrderResult);

                dynamic results3 = JObject.Parse(lastOrderResult);

                var GetBillReference = JsonConvert.SerializeObject(
                new
                {
                    source = new
                    {
                        identifier = "AGGREGATOR",
                        subtype = "AGGREGATOR"
                    },
                    payment_token = results3.token
                }
                );
                var reqContent = new StringContent(GetKey, Encoding.UTF8, "application/json");
                url = "https://accept.paymob.com/api/acceptance/payments/pay";

                client = new HttpClient();
                response = await client.PostAsync(url, lastOrderData);

                string lastOrderResult = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(lastOrderResult);

                dynamic results3 = JObject.Parse(lastOrderResult);
            }
            */



            return Ok();
        }


        [HttpPost("{ApplicantId}/AdmissionDetails")]
        public IActionResult AddAdmissionDetails(int ApplicantId, [FromBody] AdmissionDetailsForCreation admissionDetails)
        {
            if (admissionDetails == null)
            {
                return BadRequest();
            }
            if (_AdmissionRepo.GetApplicant(ApplicantId) == null)
            {
                return NotFound();
            }
            var AdmissionDetailsToSave = _Mapper.Map<AdmissionDetails>(admissionDetails);
            _AdmissionRepo.AddAdmissionDetails(ApplicantId, AdmissionDetailsToSave);
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("failed to add a add Admission Details");
            }
            return Ok();



        }


        [HttpPost("{ApplicantId}/Document")]
        public IActionResult AddDocument(int ApplicantID, [FromForm] DocumentForCreation DocumentForCreation)
        {
            if (DocumentForCreation == null)
            {
                return BadRequest();
            }
            if (_AdmissionRepo.GetApplicant(ApplicantID) == null)
            {
                return NotFound();
            }
        //    var DocumentToSave = _Mapper.Map<Document>(DocumentForCreation);
           

            var file = DocumentForCreation.Copy;
            Document DocumentToSave = new Document();
            if (file.Length > 0)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);
                DocumentToSave.Copy= ms.ToArray();
              /*  if (fileBytes.Length != 0) {
                    // fileBytes.CopyTo(DocumentToSave.Copy, 1);
                    Buffer.BlockCopy(fileBytes, 0, DocumentToSave.Copy, 0, fileBytes.Length);

                }*/
            }

            DocumentToSave.ApplicantId = ApplicantID;
            DocumentToSave.DocumentName = DocumentForCreation.DocumentName;
            DocumentToSave.DocumentType = DocumentForCreation.DocumentType;
            _AdmissionRepo.AddDocument(DocumentToSave);
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("Failed To Add Document");
            }


            return Ok();

                  }

        [HttpPost("{ApplicantId}/Document/{Id}")]
        public IActionResult UpdateDocument(int ApplicantID,int Id ,[FromForm] DocumentForCreation DocumentForCreation)
        {
            if (DocumentForCreation == null)
            {
                return BadRequest();
            }
            if (_AdmissionRepo.GetApplicant(ApplicantID) == null)
            {
                return NotFound();
            }
            //    var DocumentToSave = _Mapper.Map<Document>(DocumentForCreation);


            var file = DocumentForCreation.Copy;
            Document DocumentToSave = new Document();
            if (file.Length > 0)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);
                DocumentToSave.Copy = ms.ToArray();
                /*  if (fileBytes.Length != 0) {
                      // fileBytes.CopyTo(DocumentToSave.Copy, 1);
                      Buffer.BlockCopy(fileBytes, 0, DocumentToSave.Copy, 0, fileBytes.Length);

                  }*/
            }

            DocumentToSave.ApplicantId = ApplicantID;
            DocumentToSave.DocumentName = DocumentForCreation.DocumentName;
            DocumentToSave.DocumentType = DocumentForCreation.DocumentType;
            _AdmissionRepo.AddDocument(DocumentToSave);
            _AdmissionRepo.DeleteDocument(_AdmissionRepo.GetDocument(ApplicantID,Id));
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("Failed To Add Document");
            }


            return Ok();

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
    /*    [HttpGet("{applicantId}/Document/{id}")]
        public IActionResult GetDocument(int applicantId, int id)
        {
            var DocumentFromRepo = _AdminRepo.GetDocument(applicantId, id);
            if (DocumentFromRepo == null)
            {
                return NotFound();
            }


            string imageBase64Data = Convert.ToBase64String(DocumentFromRepo.Copy);
            string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            return Ok(imageDataURL);

        }
        


      /*  [HttpGet("{ApplicantId}")]
        public IActionResult GetApplicant(int applicantId)
        {
            var Applicant = _AdmissionRepo.GetApplicant(applicantId);
            if (Applicant == null)
            {
                return NotFound();
            }
            var ApplicantToRetrive = _Mapper.Map<ApplicantDto>(Applicant);
            return Ok(ApplicantToRetrive);


        }*/


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

         }*/


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


       /* [HttpGet("{applicantId}/Medical")]
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
       */

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

            var ApplicantToRetrive = _Mapper.Map<IEnumerable<ApplicantDto>>(Applicants);
            return Ok(ApplicantToRetrive);


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
                            values: new {
                                orderBy = resourceParameters.OrderBy,
                                searchQuery = resourceParameters.SearchQuery,
                                Name = resourceParameters.Name,
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
                           values: new {
                              orderBy = resourceParameters.OrderBy,
                              searchQuery = resourceParameters.SearchQuery,
                              Name = resourceParameters.Name,
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
                                 Name = resourceParameters.Name,
                                 pageNumber = resourceParameters.PageNumber,
                                 pageSize = resourceParameters.PageSize
                             });
            }
        }









        [HttpGet("{applicantId}/Siblings/{id}", Name = "getSibling")]
        public IActionResult GetSibling(int applicantId, Guid id)
        {
            if (_AdmissionRepo.GetApplicant(applicantId) == null)
            {
                return NotFound();
            }

            var SiblingFromRepo = _AdminRepo.GetSibling(applicantId, id);
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

            var SiblingsFromRepo = _AdminRepo.GetSiblings(applicantId);
            if (SiblingsFromRepo == null)
            {
                return NotFound();
            }

            var Siblings = _Mapper.Map<IEnumerable<SiblingDto>>(SiblingsFromRepo);


            return Ok(Siblings);

        }
       /* [HttpGet("{ApplicantId}/GetApplication")]
        public IActionResult GetApplication(int ApplicantId)
        {
            if (_AdmissionRepo.GetApplicant(ApplicantId) == null)
            {
                return NotFound();
            }
            Application  ApplicationToReturn = _AdmissionRepo.GetApplication(ApplicantId);
            return Ok (ApplicationToReturn);
        }*/


        [HttpDelete("{applicantId}/siblings/{id}")]
        public IActionResult DeleteSibling(int applicantId, Guid id)
        {
            if (_AdmissionRepo.GetApplicant(applicantId) == null)
            {
                return NotFound();
            }

            var SiblingFromRepo = _AdminRepo.GetSibling(applicantId, id);
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


        [HttpPatch("{applicantId}")]
        public IActionResult UpdateApplicant(int applicantId, [FromBody] JsonPatchDocument<ApplicantForUpdate> patchdoc)
        {
            if (patchdoc == null)
            {
                return BadRequest();
            }
            if (_AdmissionRepo.GetApplicant(applicantId) == null)
            {
                return BadRequest();
            }
            var ApplicantFromRepo = _AdmissionRepo.GetApplicant(applicantId);
            var ApplicantToPatch = _Mapper.Map<ApplicantForUpdate>(ApplicantFromRepo);
            patchdoc.ApplyTo(ApplicantToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }
            _Mapper.Map(ApplicantToPatch, ApplicantFromRepo);
            _AdmissionRepo.UpdateApplicant(ApplicantFromRepo);
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("Failed To Update Applicant");
            }
            return NoContent();

        }




       /* [HttpPatch("{applicantId}/medical/{id}")]
        public IActionResult PartiallyUpdateMedicalHistory(int applicantId, Guid id,
           [FromBody] JsonPatchDocument<MedicalHistoryForUpdate> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (_AdmissionRepo.GetApplicant(applicantId) == null)
            {
                return NotFound();
            }

            var MedicalHistoryFromRepo = _AdminRepo.GetMedicalHistory(applicantId);
            if (MedicalHistoryFromRepo == null)
            {
                return NotFound();
            }

            var medicalHistoryToPatch = _Mapper.Map<MedicalHistoryForUpdate>(MedicalHistoryFromRepo);
            patchDoc.ApplyTo(medicalHistoryToPatch, ModelState);


            //add validation
            //ModelState, any errors in the patch document will make modelState invalid
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            _Mapper.Map(medicalHistoryToPatch, MedicalHistoryFromRepo);

            _AdmissionRepo.UpdateMedicalDetails(MedicalHistoryFromRepo);
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("failed to update a Medical Details");
            }

            return NoContent();
        }
       */


        [HttpPatch("{ApplicantId}/ParentInfo/{Id}")]
        public IActionResult UpdateParentInfo(int ApplicantId, Guid Id, [FromBody] JsonPatchDocument<ParentInfoForUpdate> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            if (!_AdmissionRepo.ApplicantExist(ApplicantId))
            {
                return NotFound();
            }
            if (_AdmissionRepo.ParentInfoExist(ApplicantId, Id) == null)
            {
                return NotFound();
            }
            var ParentInfoFromRepo = _AdmissionRepo.ParentInfoExist(ApplicantId, Id);

            var ParentInfoToPatch = _Mapper.Map<ParentInfoForUpdate>(ParentInfoFromRepo);

            patchDoc.ApplyTo(ParentInfoToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);

            }
            _Mapper.Map(ParentInfoToPatch, ParentInfoFromRepo);
            _AdmissionRepo.UpdateParentInfo(ParentInfoFromRepo);
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("Failed to update parent info");

            }
            return NoContent();


        }
        [HttpPatch("{ApplicantId}/EmergencyContact/{Id}")]
        public IActionResult UpdateEmergencyContact(int ApplicantId, Guid Id, [FromBody] JsonPatchDocument<EmergencyContactForUpdate> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            if (_AdmissionRepo.GetApplicant(ApplicantId) == null)
            {
                return NotFound();
            }
            if (_AdminRepo.GetEmergencyContacts(ApplicantId) == null)
            {
                return NotFound();
            }
            var EmergencyContactFromRepo = _AdminRepo.GetEmergencyContact(ApplicantId, Id);

            var EmergencyContactToPatch = _Mapper.Map<EmergencyContactForUpdate>(EmergencyContactFromRepo);

            patchDoc.ApplyTo(EmergencyContactToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);

            }
            _Mapper.Map(EmergencyContactToPatch, EmergencyContactFromRepo);
            _AdmissionRepo.UpdateEmergencyContact(EmergencyContactFromRepo);
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("Failed to update Emergency Contact");

            }
            return NoContent();


        }

        [HttpPatch("{applicantId}/siblings/{id}")]
        public IActionResult PartiallyUpdateSibling(int applicantId, Guid id,
           [FromBody] JsonPatchDocument<SiblingForUpdate> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (_AdmissionRepo.GetApplicant(applicantId) == null)
            {
                return NotFound();
            }

            var SiblingFromRepo = _AdminRepo.GetSibling(applicantId, id);
            if (SiblingFromRepo == null)
            {
                return NotFound();
            }

            var siblingToPatch = _Mapper.Map<SiblingForUpdate>(SiblingFromRepo);
            patchDoc.ApplyTo(siblingToPatch, ModelState);


            //add validation
            //ModelState, any errors in the patch document will make modelState invalid
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            _Mapper.Map(siblingToPatch, SiblingFromRepo);

            _AdmissionRepo.UpdateSibling(SiblingFromRepo);
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("failed to update a sibling");
            }

            return NoContent();
        }

        [HttpPatch("{applicantId}/medical/{id}")]
        public IActionResult PartiallyUpdateAdmissionDetails(int applicantId, Guid id,
           [FromBody] JsonPatchDocument<AdmissionDetailsForUpdate> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (_AdmissionRepo.GetApplicant(applicantId) == null)
            {
                return NotFound();
            }

            var AdmissionDetailsFromRepo = _AdminRepo.GetAdmissionDetails(applicantId, id);
            if (AdmissionDetailsFromRepo == null)
            {
                return NotFound();
            }

            var AdmissionDetailsToPatch = _Mapper.Map<AdmissionDetailsForUpdate>(AdmissionDetailsFromRepo);
            patchDoc.ApplyTo(AdmissionDetailsToPatch, ModelState);


            //add validation
            //ModelState, any errors in the patch document will make modelState invalid
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            _Mapper.Map(AdmissionDetailsToPatch, AdmissionDetailsFromRepo);

            _AdmissionRepo.UpdateAdmissionDetails(AdmissionDetailsFromRepo);
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("failed to update a Admission Details");
            }

            return NoContent();
        }
       /* [HttpPost("{ApplicantId}/Document/{Id}")]
        public IActionResult UpdateDocument(int ApplicantID, int Id, [FromForm] DocumentForCreation DocumentForCreation)
        {
            if (DocumentForCreation == null)
            {
                return BadRequest();
            }
            if (_AdmissionRepo.GetApplicant(ApplicantID) == null)
            {
                return NotFound();
            }
            //    var DocumentToSave = _Mapper.Map<Document>(DocumentForCreation);


            var file = DocumentForCreation.Copy;
            Document DocumentToSave = new Document();
            if (file.Length > 0)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);
                DocumentToSave.Copy = ms.ToArray();
                /*  if (fileBytes.Length != 0) {
                      // fileBytes.CopyTo(DocumentToSave.Copy, 1);
                      Buffer.BlockCopy(fileBytes, 0, DocumentToSave.Copy, 0, fileBytes.Length);

                  }
            }

            DocumentToSave.ApplicantId = ApplicantID;
            DocumentToSave.DocumentName = DocumentForCreation.DocumentName;
            DocumentToSave.DocumentType = DocumentForCreation.DocumentType;
            _AdmissionRepo.AddDocument(DocumentToSave);
            _AdmissionRepo.DeleteDocument(_AdminRepo.GetDocument(ApplicantID, Id));
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("Failed To Add Document");
            }


            return Ok();

        }*/


    }
}