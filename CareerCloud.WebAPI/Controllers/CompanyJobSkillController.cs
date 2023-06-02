using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobSkillController : ControllerBase
    {
        private readonly CompanyJobSkillLogic _companyJobSkillLogic;

        public CompanyJobSkillController()
        {
            EFGenericRepository<CompanyJobSkillPoco> repo = new EFGenericRepository<CompanyJobSkillPoco>(); 
            _companyJobSkillLogic = new CompanyJobSkillLogic(repo);
        }

        [HttpGet, Route("jobSkill")]
        [ProducesResponseType(typeof(CompanyJobSkillPoco), 200)]
        public ActionResult GetAllCompanyJobSkill()
        {
            return Ok(_companyJobSkillLogic.GetAll());
        }




        [HttpGet, Route("jobSkill/{companyJobSkillId}")]
        [ProducesResponseType(typeof(CompanyJobSkillPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobSkill(Guid id)
        {
            try
            {
                return Ok(_companyJobSkillLogic.Get(id));
            }
            catch(Exception ex) { return NotFound(ex.Message); }
        }

        [HttpPost, Route("jobSkill")]
        [ProducesResponseType(typeof(CompanyJobSkillPoco), 200)]
        public ActionResult PostCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos)
        {
            _companyJobSkillLogic.Add(pocos);
            return Ok();
        }

          [HttpPut, Route("jobSkill")]
        [ProducesResponseType(typeof(CompanyJobSkillPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos)
        {
            try
            {  _companyJobSkillLogic.Update(pocos);
                return Ok();
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }

        [HttpDelete, Route("jobSkill")]
        [ProducesResponseType(typeof(CompanyJobSkillPoco), 200)]
        public ActionResult DeleteCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos) 
        {   
            _companyJobSkillLogic.Delete(pocos);
            return Ok();
        }

    }
}
