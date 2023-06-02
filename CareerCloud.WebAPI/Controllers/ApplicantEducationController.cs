using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantEducationController : ControllerBase
    {
        private readonly ApplicantEducationLogic _applicantEducationLogic;
        public ApplicantEducationController() 
        {
            EFGenericRepository<ApplicantEducationPoco> repo = new EFGenericRepository<ApplicantEducationPoco>();
            _applicantEducationLogic = new ApplicantEducationLogic(repo);

        }
        

        [HttpGet, Route("education")] //Get all 
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]

        public ActionResult GetAllApplicantEducation()
        {
          return Ok(_applicantEducationLogic.GetAll());
        }


        [HttpGet, Route("education/{applicantEducationId}")] //Get by id
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantEducation(Guid id)
        {
            try
            {
                var poco = _applicantEducationLogic.Get(id);
                return Ok(poco);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



        [HttpPost, Route("education")]
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]

        public ActionResult PostApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {
            _applicantEducationLogic.Add(pocos);
            return Ok();
         }


        [HttpPut, Route("education")]
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {
            try
            {
                _applicantEducationLogic.Update(pocos);
                return Ok(); 

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        
        [HttpDelete,Route("education")]
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]

        public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] pocos) 
                                          
        {
            
            
                _applicantEducationLogic.Delete(pocos);
                return Ok();
            

        }
    }
}


       