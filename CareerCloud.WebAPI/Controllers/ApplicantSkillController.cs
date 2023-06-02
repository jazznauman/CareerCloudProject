using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantSkillController : ControllerBase
    {
        private readonly ApplicantSkillLogic _applicantSkillLogic;

        public ApplicantSkillController()
        {
            EFGenericRepository<ApplicantSkillPoco> repo = new EFGenericRepository<ApplicantSkillPoco>();
            _applicantSkillLogic = new ApplicantSkillLogic(repo);
        }

        [HttpGet, Route("skill")]
        [ProducesResponseType(typeof(ApplicantSkillPoco), 200)]


        public ActionResult GetAllApplicantSkill()
        {
            return Ok(_applicantSkillLogic.GetAll());
        }

        [HttpGet, Route("skill/{applicantSkillId}")]
        [ProducesResponseType(typeof(ApplicantSkillPoco), 200)]
        [ProducesResponseType(404)]

        public ActionResult GetApplicantSkill(Guid id)
        {
            try
            {   
                return Ok(_applicantSkillLogic.Get(id));
            }
            catch(Exception ex) { return NotFound(ex.Message); }
        }


        [HttpPost, Route("skill")]
        [ProducesResponseType(typeof(ApplicantSkillPoco), 200)]

        public ActionResult PostApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
        {
            _applicantSkillLogic.Add(pocos);
            return Ok();
        }

        [HttpPut, Route("skill")]
        [ProducesResponseType(typeof(ApplicantSkillPoco), 200)]
        [ProducesResponseType(404)]

        public ActionResult PutApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
        {
            try
            {    _applicantSkillLogic.Update(pocos);
                return Ok();
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }
        [HttpDelete, Route("skill")]
        [ProducesResponseType(typeof(ApplicantSkillPoco), 200)]
        public ActionResult DeleteApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
        {
            _applicantSkillLogic.Delete(pocos);
            return Ok();
        }


    }
}
