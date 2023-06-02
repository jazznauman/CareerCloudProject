using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantProfileController : ControllerBase
    {
        private readonly ApplicantProfileLogic _applicantProfileLogic;


        public ApplicantProfileController()
        {
            EFGenericRepository<ApplicantProfilePoco> repo = new EFGenericRepository<ApplicantProfilePoco>();
            _applicantProfileLogic = new ApplicantProfileLogic(repo);
        }


        [HttpGet, Route("profile")]
        [ProducesResponseType(typeof(ApplicantProfilePoco), 200)]

        public ActionResult GetAllApplicantProfile()
        {
            return Ok(_applicantProfileLogic.GetAll());

        }




        [HttpGet, Route("profile/{applicantProfileId}")]
        [ProducesResponseType(typeof(ApplicantProfilePoco), 200)]
        [ProducesResponseType(404)]

        public ActionResult GetApplicantProfile(Guid id)
        {
            try
            {
                
                return Ok(_applicantProfileLogic.Get(id));
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }



        [HttpPost, Route("profile")]
        [ProducesResponseType(typeof(ApplicantProfilePoco), 200)]

        public ActionResult PostApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)

        {   _applicantProfileLogic.Add(pocos);
            return Ok();
        }


        [HttpPut, Route("profile")]
        [ProducesResponseType(typeof(ApplicantProfilePoco), 200)]
        [ProducesResponseType(404)]

        public ActionResult PutApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)

        {
            try
            {   _applicantProfileLogic.Update(pocos);
                return Ok();
            }
            catch(Exception ex) { return NotFound(ex.Message); }
        }



        [HttpDelete, Route("profile")]
        [ProducesResponseType(typeof(ApplicantProfilePoco), 200)]

        public ActionResult DeleteApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            _applicantProfileLogic.Delete(pocos);
            return Ok();
        }

       

       




    }
}
