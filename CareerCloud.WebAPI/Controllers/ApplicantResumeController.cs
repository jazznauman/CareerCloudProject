using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantResumeController : ControllerBase
    {

        private readonly ApplicantResumeLogic _applicantResumeLogic;

        public ApplicantResumeController()
        {
            EFGenericRepository<ApplicantResumePoco> repo = new EFGenericRepository<ApplicantResumePoco>();
            _applicantResumeLogic = new ApplicantResumeLogic(repo);
        }

        [HttpGet, Route("resume")]
        [ProducesResponseType(typeof(ApplicantResumePoco), 200)]
        public ActionResult GetAllApplicantResume()
        {
            return Ok(_applicantResumeLogic.GetAll());

        }

     
        [HttpGet,Route("resume/{applicantResumeId}")]
        [ProducesResponseType(typeof(ApplicantResumePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantResume(Guid id)
        {
            try
            {

                return Ok(_applicantResumeLogic.Get(id));
            }

            catch (Exception ex) { return NotFound(ex.Message); }
        }



        [HttpPost, Route("resume")]
        [ProducesResponseType(typeof(ApplicantResumePoco), 200)]
        public ActionResult PostApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {
            _applicantResumeLogic.Add(pocos);
            return Ok();

        }

        [HttpPut,Route("resume")]
        [ProducesResponseType(typeof(ApplicantResumePoco), 200)]
        [ProducesResponseType(404)]

        public ActionResult PutApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {
            try
            {   _applicantResumeLogic.Update(pocos);
                return Ok();
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }



        [HttpDelete, Route("resume")]
        [ProducesResponseType(typeof(ApplicantResumePoco), 200)]


        public ActionResult DeleteApplicantResume([FromBody] ApplicantResumePoco[] pocos)
        {
            _applicantResumeLogic.Delete(pocos);
            return Ok();
        }
    
    
    }
}
