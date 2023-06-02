using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantWorkHistoryController : ControllerBase
    {
        private readonly ApplicantWorkHistoryLogic _applicantWorkHistoryLogic;

        public ApplicantWorkHistoryController()
        {
            EFGenericRepository<ApplicantWorkHistoryPoco> repo = new EFGenericRepository<ApplicantWorkHistoryPoco>();

            _applicantWorkHistoryLogic = new ApplicantWorkHistoryLogic(repo);
        }

        [HttpGet, Route("workHistory")]
        [ProducesResponseType(typeof(ApplicantWorkHistoryPoco), 200)]

        public ActionResult GetAllApplicantWorkHistory()
        {
            return Ok(_applicantWorkHistoryLogic.GetAll());
        }
       
        
        [HttpGet, Route("workHistory/{applicantWorkHistoryId}")]
        [ProducesResponseType(typeof(ApplicantWorkHistoryPoco), 200)]
        [ProducesResponseType(404)]

        public ActionResult GetApplicantWorkHistory(Guid id)
        {
            try
            {
                return Ok(_applicantWorkHistoryLogic.Get(id));
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }
        [HttpPost, Route("workHistory")]
        [ProducesResponseType(typeof(ApplicantWorkHistoryPoco), 200)]

        public ActionResult PostApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {
            _applicantWorkHistoryLogic.Add(pocos);
            return Ok();
        }


        [HttpPut, Route("workHistory")]
        [ProducesResponseType(typeof(ApplicantWorkHistoryPoco), 200)]
        [ProducesResponseType(404)]

        public ActionResult PutApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {  try
            {
                _applicantWorkHistoryLogic.Update(pocos);
                return Ok();
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }

        [HttpDelete, Route("workHistory")]
        [ProducesResponseType(typeof(ApplicantWorkHistoryPoco), 200)]


        public ActionResult DeleteApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {
            _applicantWorkHistoryLogic.Delete(pocos);
            return Ok();
        }
    }
}
