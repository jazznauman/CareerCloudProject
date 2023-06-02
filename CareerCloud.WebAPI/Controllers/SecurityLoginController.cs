using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic _securityLoginLogic;
    
    public SecurityLoginController()

        {
            EFGenericRepository<SecurityLoginPoco> repo = new EFGenericRepository<SecurityLoginPoco>();
            _securityLoginLogic = new SecurityLoginLogic(repo);
        }

        [HttpGet, Route("login")]
        [ProducesResponseType(typeof(SecurityLoginPoco), 200)]
        public ActionResult GetAllSecurityLogin()
        {
            return Ok(_securityLoginLogic.GetAll());
        }

        [HttpGet, Route("login/{securityLoginId}")]
        [ProducesResponseType(typeof(SecurityLoginPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLogin(Guid id)
        {
            try
            {
                return Ok(_securityLoginLogic.Get(id));
            }
            catch (Exception ex) { return NotFound(ex.Message); }
        }

        [HttpPost, Route("login")]
        [ProducesResponseType(typeof(SecurityLoginPoco), 200)]
        public ActionResult PostSecurityLogin([FromBody] SecurityLoginPoco[] data)
        {
            _securityLoginLogic.Add(data);
            return Ok();
        }

        [HttpPut, Route("login")]
        [ProducesResponseType(typeof(SecurityLoginPoco),200)]
        [ProducesResponseType(404)]

        public ActionResult PutSecurityLogin([FromBody] SecurityLoginPoco[] data)
        {
            try
            {    _securityLoginLogic.Update(data);
                return Ok();
            }
            catch(Exception ex) { return NotFound(ex.Message); }
        }
        [HttpDelete, Route("login")]
        [ProducesResponseType(typeof(SecurityLoginPoco), 200)]
        public ActionResult DeleteSecurityLogin([FromBody] SecurityLoginPoco[] data) 
        {
            _securityLoginLogic.Delete(data);
            return Ok();
        }

    }
}
