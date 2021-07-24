using AdmissionSystem2.Entites;
using AdmissionSystem2.Helpers;
using AdmissionSystem2.Models;
using AdmissionSystem2.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
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
        private readonly JWT _JWT;
        public ApplicantController(IAdmissionRepo AdmissionRepo, IAdminRepo AdminRepo, IMapper Mapper, IOptions<JWT> jwt)
        {
            _AdmissionRepo = AdmissionRepo;
            _AdminRepo = AdminRepo;
            _Mapper = Mapper;
            _JWT = jwt.Value;
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

            var date = DateTime.Today.ToString("yyyy/MM/dd");
            final.AdmissionDate = Convert.ToDateTime(date).Date;
            //final.Status = "Applied Sucessfuly";

            _AdmissionRepo.AddApplicant(final);
            _AdmissionRepo.Save();

            return Ok();
        }

        [HttpPost("{ApplicantId}/ParentInfo")]
        public IActionResult AddParentInfo(Guid ApplicantId, [FromBody]IEnumerable< ParentInfoForCreation> ParentInfoForCreation)
        {

            if (ParentInfoForCreation == null)
            {
                return BadRequest();
            }
            if (_AdminRepo.GetApplicant(ApplicantId) == null)
            {
                return NotFound();
            }
            var ParentInfo = _Mapper.Map<IEnumerable< ParentInfo>>(ParentInfoForCreation);
            foreach (var Parent in ParentInfo)
            {
                _AdmissionRepo.AddParentInfo(ApplicantId, Parent);
            }
            _AdmissionRepo.Save();
            return Ok();

        }

        [HttpPost("{ApplicantId}/EmergencyContact")]
        public IActionResult AddEmergencyContact(Guid ApplicantId, [FromBody] EmergencyContactForCreation EmergencyContactForCreation)
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

        [HttpPost("{ApplicantId}/EmergencyContacts")]
        public IActionResult AddEmergencyContacts(Guid ApplicantId, [FromBody] IEnumerable<EmergencyContactForCreation> EmergencyContactForCreation)
        {
            if (EmergencyContactForCreation == null)
            {
                return BadRequest();
            }
            if (_AdminRepo.GetApplicant(ApplicantId) == null)
            {
                return NotFound();
            }
            var EmergencyContactEntities = _Mapper.Map< IEnumerable<EmergencyContact>>(EmergencyContactForCreation);
            foreach (var EmergencyContact in EmergencyContactEntities)
            {
                _AdmissionRepo.AddEmergencyContact(ApplicantId, EmergencyContact);
            }
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("failed to add Emergency Contact ");
            }
            
            return Ok();
        }
        [HttpPost("{ApplicantId}/FamilyStatus")]
        public IActionResult AddFamilyStatus(Guid ApplicantId, [FromBody] FamilyStatusForCreation familyStatusForCreation)
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
            _AdmissionRepo.AddFamilyStatus(ApplicantId, FamilyStatus);
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("failed to add Family Status ");
            }
            return Ok();
        }

        [HttpPost("{applicantId}/Sibling")]
        public IActionResult AddSibling(Guid applicantId, [FromBody] SiblingForCreation sibling)

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
            return Ok();

        }

        [HttpPost("{applicantId}/Siblings")]
        public IActionResult AddSiblings(Guid applicantId, [FromBody] IEnumerable<SiblingForCreation> siblings)
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
        public IActionResult AddMedicalDetails(Guid applicantId, [FromBody] MedicalHistoryForCreation medicalHistory)
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
            return Ok();
        }


        [HttpGet("Payment")]
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
            //Redirect("https://accept.paymob.com/api/acceptance/iframes/241121?payment_token=" + lastOrderData.token);


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

           // string paymentUrl = ;

            return Ok("https://accept.paymob.com/api/acceptance/iframes/241121?payment_token=" + lastOrderData.token);
        }


        [HttpPost("{ApplicantId}/AdmissionDetails")]
        public IActionResult AddAdmissionDetails(Guid ApplicantId, [FromBody] AdmissionDetailsForCreation admissionDetails)
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
        public IActionResult AddDocument(Guid ApplicantID, [FromForm] DocumentForCreation DocumentForCreation)
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
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("Failed To Add Document");
            }


            return Ok();

        }

        [HttpPost("{ApplicantId}/UpdateDocument")]
        public IActionResult UpdateDocument(Guid ApplicantID, [FromForm] DocumentForCreation DocumentForCreation)
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
            _AdmissionRepo.DeleteDocument(_AdminRepo.GetDocument(ApplicantID,DocumentForCreation.DocumentName));
            if (!_AdmissionRepo.Save())
            {
                throw new Exception("Failed To Add Document");
            }


            return Ok();

        }


        [HttpDelete("{applicantId}/siblings/{id}")]
        public IActionResult DeleteSibling(Guid applicantId, Guid id)
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
        public IActionResult UpdateApplicant(Guid applicantId, [FromBody] JsonPatchDocument<ApplicantForUpdate> patchdoc)
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




         [HttpPatch("{applicantId}/Medical")]
         public IActionResult PartiallyUpdateMedicalHistory(Guid applicantId,
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
        


        [HttpPatch("{ApplicantId}/ParentInfo/{Id}")]
        public IActionResult UpdateParentInfo(Guid ApplicantId, Guid Id, [FromBody] JsonPatchDocument<ParentInfoForUpdate> patchDoc)
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
        public IActionResult UpdateEmergencyContact(Guid ApplicantId, Guid Id, [FromBody] JsonPatchDocument<EmergencyContactForUpdate> patchDoc)
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
        public IActionResult PartiallyUpdateSibling(Guid applicantId, Guid id,
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

        [HttpPatch("{applicantId}/AdmissionDetails")]
        public IActionResult PartiallyUpdateAdmissionDetails(Guid applicantId,
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

            var AdmissionDetailsFromRepo = _AdminRepo.GetAdmissionDetails(applicantId);
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

        

     /*   [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLoginModel model) //Get Token - login
        {
            var Applicant = _AdmissionRepo.Authenticate(model.UserName, model.Password);

            if (Applicant == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenString = GenerateJwtToken(Applicant);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = Applicant.Id,
                Username = Applicant.UserName,
                Token = tokenString
            });
        }
     */
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

        [HttpGet("DecodeJwt")]
        public IActionResult DecodeJwt([FromBody] string jwtEncodedString)
        {
            var token = jwtEncodedString;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            return Ok(jwtSecurityToken.Claims);

            //return Ok(jwtSecurityToken.Claims.First(claim => claim.Type == "UserName").Value);
        }
    }
}