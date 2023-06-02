using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityRoleController : ControllerBase
    {
        private readonly SecurityRoleLogic _securityRoleLogic;

        public SecurityRoleController()
        {
            EFGenericRepository<SecurityRolePoco> repo = new EFGenericRepository<SecurityRolePoco>();
            _securityRoleLogic= new SecurityRoleLogic(repo);
        }

        [HttpGet, Route("role")]
        [ProducesResponseType(typeof(SecurityRolePoco), 200)]
        public ActionResult GetAllSecurityRole()
        {
            return Ok(_securityRoleLogic.GetAll());
        }

        [HttpGet, Route("role/{securityRoleId}")]
        [ProducesResponseType(typeof(SecurityRolePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityRole(Guid  id) 
        {
            try
            {
                return Ok(_securityRoleLogic.Get(id));
            }
            catch (Exception ex)
            {
                   return NotFound(ex.Message);
            }
        }

        [HttpPost, Route("role")]
        [ProducesResponseType(typeof (SecurityRolePoco), 200)]
        public ActionResult PostSecurityRole([FromBody] SecurityRolePoco[] pocos)
        {
            _securityRoleLogic.Add(pocos);
            return Ok();
        }

        [HttpPut, Route("role")]
        [ProducesResponseType(typeof(SecurityRolePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutSecurityRole([FromBody] SecurityRolePoco[] pocos)
        {
            try
            {   _securityRoleLogic.Update(pocos);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete, Route("role")]
        [ProducesResponseType(typeof(SecurityRolePoco),200)]
        public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] pocos)
        {
            _securityRoleLogic.Delete(pocos);
            return Ok();
        }

    }
}
