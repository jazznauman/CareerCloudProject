using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly ApplicantJobApplicationLogic _applicantJobApplicationLogic;

        public ApplicantJobApplicationController()
        {
            EFGenericRepository<ApplicantJobApplicationPoco> repo = new EFGenericRepository<ApplicantJobApplicationPoco>();  
            _applicantJobApplicationLogic = new ApplicantJobApplicationLogic(repo);
        }

        [HttpGet, Route("jobApplication")] //Get all 
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco), 200)]


        public ActionResult GetAllApplicantJobApplication()
        {
            return Ok(_applicantJobApplicationLogic.GetAll());
        }


        [HttpGet, Route("jobApplication/{applicantJobApplicationId}")] //Get by id
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco), 200)]
        [ProducesResponseType(404)]

        public ActionResult GetApplicantJobApplication(Guid id)
        {
            try
            {
                var poco = _applicantJobApplicationLogic.Get(id);
                return Ok(poco);
            }
            catch (Exception ex) 
            { 
                return NotFound(ex.Message);
            }
        }



        [HttpPost, Route("jobApplication")]
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco), 200)]
        public ActionResult PostApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)
        {
            _applicantJobApplicationLogic.Add(pocos);
            return Ok();
        }

        [HttpPut, Route("jobApplication")]
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco), 200)]
        [ProducesResponseType(404)]



        public ActionResult PutApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)
        {
            try
            {
                _applicantJobApplicationLogic.Update(pocos);
                return Ok(); 

            }
            catch (Exception ex)
            { 
                return NotFound(ex.Message);
            }

        }

        [HttpDelete, Route("jobApplication")]
                                             
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco), 200)]
        public ActionResult DeleteApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)

        {


            _applicantJobApplicationLogic.Delete(pocos);
            return Ok();


        }
    }
}


