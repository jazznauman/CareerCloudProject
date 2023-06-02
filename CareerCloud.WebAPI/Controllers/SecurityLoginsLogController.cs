using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly SecurityLoginsLogLogic _securityLoginsLogLogic;

        public SecurityLoginsLogController()
        {
            EFGenericRepository<SecurityLoginsLogPoco> repo = new EFGenericRepository<SecurityLoginsLogPoco>();
            _securityLoginsLogLogic = new SecurityLoginsLogLogic(repo);
        }

        [HttpGet, Route("loginsLog")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco), 200)]
        public ActionResult GetAllSecurityLoginsLog()
        {
            return Ok(_securityLoginsLogLogic.GetAll());
        }

        [HttpGet, Route("loginsLog/{securityLoginsLogId}")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLoginsLog(Guid id)
        {
            try
            {
                return Ok(_securityLoginsLogLogic.Get(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost, Route("loginsLog")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco), 200)]
        public ActionResult PostSecurityLoginsLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            _securityLoginsLogLogic.Add(pocos);
            return Ok();

        }
        [HttpPut, Route("loginsLog")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutSecurityLoginsLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            try
            {
                _securityLoginsLogLogic.Update(pocos);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete, Route("loginsLog")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco), 200)]
        public ActionResult DeleteSecurityLoginsLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            _securityLoginsLogLogic.Delete(pocos);
            return Ok();
        }
    }
}