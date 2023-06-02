using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyLocationController : ControllerBase
    {
        private readonly CompanyLocationLogic _companyLocationLogic;

        public CompanyLocationController()
        {
            EFGenericRepository<CompanyLocationPoco> repo = new EFGenericRepository<CompanyLocationPoco>();
            _companyLocationLogic = new CompanyLocationLogic(repo);
        }

        [HttpGet, Route("location")]
        [ProducesResponseType(typeof(CompanyLocationPoco), 200)]
        public ActionResult GetAllCompanyLocation()
        {
            return Ok(_companyLocationLogic.GetAll());
        }


        [HttpGet, Route("location/{companyLocationId}")]
        [ProducesResponseType(typeof(CompanyLocationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyLocation(Guid id)
        {
            try
            {
                return Ok(_companyLocationLogic.Get(id));
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        
        }

        [HttpPost, Route("location")]
        [ProducesResponseType(typeof(CompanyLocationPoco), 200)]
        public ActionResult PostCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {
            _companyLocationLogic.Add(pocos);
            
                return Ok();
        }

        [HttpPut, Route("location")]
        [ProducesResponseType(typeof(CompanyLocationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {
            try
            {
                _companyLocationLogic.Update(pocos);
                return Ok();

            }


            catch (Exception ex) { return NotFound(ex.Message); }
        }


        [HttpDelete, Route("location")]
        [ProducesResponseType(typeof(CompanyLocationPoco), 200)]

        public ActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] pocos) 
        {
            _companyLocationLogic.Delete(pocos);
            return Ok();
        
        }

    }
}
