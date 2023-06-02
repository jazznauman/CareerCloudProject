using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyDescriptionController : ControllerBase
    {
        private readonly CompanyDescriptionLogic _companyDescriptionLogic;

        public CompanyDescriptionController()
        {
            EFGenericRepository<CompanyDescriptionPoco> repo = new EFGenericRepository<CompanyDescriptionPoco>();
            _companyDescriptionLogic = new CompanyDescriptionLogic(repo);
        }

        [HttpGet, Route("description")]
        [ProducesResponseType(typeof(CompanyDescriptionPoco), 200)]

        public ActionResult GetAllCompanyDescription()
        {
            return Ok(_companyDescriptionLogic.GetAll());
        }

        [HttpGet, Route("description/{companyDescriptionId}")]
        [ProducesResponseType(typeof(CompanyDescriptionPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyDescription(Guid id)
        {
            try
            {
                return Ok(_companyDescriptionLogic.Get(id));
            }
            catch(Exception ex) { return NotFound(ex.Message); }
        }

        [HttpPost, Route("description")]
        [ProducesResponseType(typeof(CompanyDescriptionPoco), 200)]
        public ActionResult PostCompanyDescription([FromBody] CompanyDescriptionPoco[] pocos)
        {
            _companyDescriptionLogic.Add(pocos);
            return Ok();
        }

        [HttpPut, Route("description")]
        [ProducesResponseType(typeof(CompanyDescriptionPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyDescription([FromBody] CompanyDescriptionPoco[] pocos)
        {
            try
            {
                _companyDescriptionLogic.Update(pocos);
                return Ok();
            }
            catch(Exception ex) { return NotFound(ex.Message); }
        }




        [HttpDelete, Route("description")]
        [ProducesResponseType(typeof(CompanyDescriptionPoco), 200)]
        public ActionResult DeleteCompanyDescription([FromBody] CompanyDescriptionPoco[] pocos) 
        {

            _companyDescriptionLogic.Delete(pocos);
            return Ok();
        }
    }
}
