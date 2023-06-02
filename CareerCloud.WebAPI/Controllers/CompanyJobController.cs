using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobController : ControllerBase
    {
        private readonly CompanyJobLogic _companyJobLogic;
        public CompanyJobController()
        {
            EFGenericRepository<CompanyJobPoco> repo = new EFGenericRepository<CompanyJobPoco>();
            _companyJobLogic = new CompanyJobLogic(repo);
        }

        [HttpGet, Route("job")]
        [ProducesResponseType(typeof(CompanyJobPoco), 200)]
        public ActionResult GetAllCompanyJob()
        {
            return Ok(_companyJobLogic.GetAll());
        }


        [HttpGet, Route("job/{companyJobId}")]
        [ProducesResponseType(typeof(CompanyJobPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJob(Guid id) 
        {
            try
            {  
                return Ok(_companyJobLogic.Get(id));
            }       
            catch (Exception ex) { return NotFound(ex.Message); }
        }

        [HttpPost, Route("job")]
        [ProducesResponseType(typeof(CompanyJobPoco), 200)]

        public ActionResult PostCompanyJob([FromBody] CompanyJobPoco[] pocos) 
        {
            _companyJobLogic.Add(pocos);
            return Ok();
        }

        [HttpPut, Route("job")]
        [ProducesResponseType(typeof(CompanyJobPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyJob([FromBody] CompanyJobPoco[] pocos)
        {
            try
            {
                _companyJobLogic.Update(pocos);
                return Ok();
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }

        [HttpDelete, Route("job")]
        [ProducesResponseType(typeof(CompanyJobPoco), 200)]

        public ActionResult DeleteCompanyJob([FromBody]  CompanyJobPoco[] pocos) 
        {
            _companyJobLogic.Delete(pocos);
            return Ok();
        }

    }
}
