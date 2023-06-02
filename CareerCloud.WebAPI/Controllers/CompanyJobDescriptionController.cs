using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic _companyJobDescriptionLogic;

        public CompanyJobDescriptionController()
        {
            EFGenericRepository<CompanyJobDescriptionPoco> repo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            _companyJobDescriptionLogic = new CompanyJobDescriptionLogic(repo);
 
        }

        [HttpGet, Route("jobDescription")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco), 200)]
        public ActionResult GetAllCompanyJobDescription()
        {
            return Ok(_companyJobDescriptionLogic.GetAll());
        }

        [HttpGet, Route("jobDescription/{companyJobdescriptionId}")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobDescription(Guid id)
        {
            try
            {
                return Ok(_companyJobDescriptionLogic.Get(id));
            }   
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost, Route("jobDescription")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco), 200)]
        public ActionResult PostCompanyJobDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            _companyJobDescriptionLogic.Add(pocos);
            return Ok();
        }

        [HttpPut, Route("jobDescription")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyJobDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {
            try
            {
                _companyJobDescriptionLogic.Update(pocos);
                return Ok();
            }
            catch (Exception ex) { return NotFound(ex.Message); }

        }

        [HttpDelete, Route("jobDescription")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco), 200)]

        public ActionResult DeleteCompanyJobDescription([FromBody] CompanyJobDescriptionPoco[] pocos) 
        {

            _companyJobDescriptionLogic.Delete(pocos);
            return Ok();
        
        }



    }
}
