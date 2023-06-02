using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsRoleController : ControllerBase

    {
        private readonly SecurityLoginsRoleLogic _securityLoginsRoleLogic;
        public SecurityLoginsRoleController()
        {
            EFGenericRepository<SecurityLoginsRolePoco> repo = new EFGenericRepository<SecurityLoginsRolePoco>();
            _securityLoginsRoleLogic = new SecurityLoginsRoleLogic(repo);
        }

        [HttpGet, Route("loginsRole")]
        [ProducesResponseType(typeof(SecurityLoginsRolePoco), 200)]
        public ActionResult GetAllSecurityLoginsRole()
        {
            return Ok(_securityLoginsRoleLogic.GetAll());
        }
        [HttpGet, Route("loginsRole/{securityLoginsRoleId}")]
        [ProducesResponseType(typeof(SecurityLoginsRolePoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLoginsRole(Guid id)
        {
            try
            {
                return Ok(_securityLoginsRoleLogic.Get(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost, Route("loginsRole")]
        [ProducesResponseType(typeof(SecurityLoginsRolePoco), 200)]
        public ActionResult PostSecurityLoginsRole([FromBody] SecurityLoginsRolePoco[] entity)
        {
            _securityLoginsRoleLogic.Add(entity);
            return Ok();
        }

        [HttpPut, Route("loginsRole")]
        [ProducesResponseType(typeof(SecurityLoginsRolePoco),200)]
        [ProducesResponseType(404)]
        public ActionResult PutSecurityLoginsRole([FromBody] SecurityLoginsRolePoco[] entity)
        {
            try
            {
                _securityLoginsRoleLogic.Update(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete, Route("loginsRole")]
        [ProducesResponseType(typeof(SecurityLoginsRolePoco),200)]
        public ActionResult DeleteSecurityLoginsRole([FromBody] SecurityLoginsRolePoco[] pocos)
        {
            
            _securityLoginsRoleLogic.Delete(pocos);
        return Ok();
        }
    }
}
