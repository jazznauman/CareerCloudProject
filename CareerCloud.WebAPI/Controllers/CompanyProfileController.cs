using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private readonly CompanyProfileLogic _companyProfileLogic;
        public CompanyProfileController()
        {
            EFGenericRepository<CompanyProfilePoco> repo = new EFGenericRepository<CompanyProfilePoco>();

            _companyProfileLogic = new CompanyProfileLogic(repo);


        }

        [HttpGet, Route("profile")]
        [ProducesResponseType(typeof(CompanyProfilePoco), 200)]
        public ActionResult GetAllCompanyProfile()
        {
            return Ok(_companyProfileLogic.GetAll());
        }

        [HttpGet, Route("profile/{companyProfileId}")]
        [ProducesResponseType(typeof(CompanyProfilePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyProfile(Guid id)
        {
            try
            {
                return Ok(_companyProfileLogic.Get(id));
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }

        [HttpPost, Route("profile")]
        [ProducesResponseType(typeof(CompanyProfilePoco), 200)]
        public ActionResult PostCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
        {
            _companyProfileLogic.Add(pocos);
            return Ok();

        }

        [HttpPut, Route("profile")]
        [ProducesResponseType(typeof(CompanyProfilePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
        {
            try
            {
                _companyProfileLogic.Update(pocos);
                return Ok();
            }
            catch (Exception ex) { return NotFound(ex.Message); }

        }

        [HttpDelete, Route("profile")]
        [ProducesResponseType(typeof(CompanyProfilePoco), 200)]
        public ActionResult DeleteCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
        {
            _companyProfileLogic.Delete(pocos);
            return Ok();
        }
    }
}