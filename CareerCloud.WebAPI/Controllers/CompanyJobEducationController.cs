using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly CompanyJobEducationLogic _companyJobEducationLogic;

        public CompanyJobEducationController()
        {
            EFGenericRepository<CompanyJobEducationPoco> repo = new EFGenericRepository<CompanyJobEducationPoco>();
            _companyJobEducationLogic = new CompanyJobEducationLogic(repo);
        }

        [HttpGet, Route("jobEducation")]
        [ProducesResponseType(typeof(CompanyJobEducationPoco), 200)]

        public ActionResult GetAllCompanyJobEducation()
        {
            return Ok(_companyJobEducationLogic.GetAll());
        }

        [HttpGet, Route("jobEducation/{companyJobEducationId}")]
        [ProducesResponseType(typeof(CompanyJobEducationPoco), 200)]
        [ProducesResponseType(404)]

        public ActionResult GetCompanyJobEducation(Guid id)
        {
            try
            {
                return Ok(_companyJobEducationLogic.Get(id));
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }

        [HttpPost, Route("jobEducation")]
        [ProducesResponseType(typeof(CompanyJobEducationPoco), 200)]
        public ActionResult PostCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
        {
            _companyJobEducationLogic.Add(pocos);
            return Ok();
        }

        [HttpPut, Route("jobEducation")]
        [ProducesResponseType(typeof(CompanyJobEducationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
        {
            try
            {
                _companyJobEducationLogic.Update(pocos);
                return Ok();
            }
            catch (Exception ex) { return NotFound(ex.Message); }


        }
        [HttpDelete, Route("jobEducation")]
        [ProducesResponseType(typeof(CompanyJobEducationPoco), 200)]

        public ActionResult DeleteCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
        {
            _companyJobEducationLogic.Delete(pocos);
            return Ok();
        }




    }
}
