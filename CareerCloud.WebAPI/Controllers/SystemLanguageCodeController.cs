using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly SystemLanguageCodeLogic _systemLanguageCodeLogic;
        public SystemLanguageCodeController()
        {
            EFGenericRepository<SystemLanguageCodePoco> repo= new EFGenericRepository<SystemLanguageCodePoco>();
            _systemLanguageCodeLogic = new SystemLanguageCodeLogic(repo);

        }

        [HttpGet, Route("languageCode")]
        [ProducesResponseType(typeof(SystemLanguageCodePoco), 200)]
        public ActionResult GetAllSystemLanguageCode()
        {
            return Ok(_systemLanguageCodeLogic.GetAll());
        }
        
        [HttpGet, Route("languageCode/{systemLanguageCodeId}")]
        [ProducesResponseType(typeof(SystemLanguageCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSystemLanguageCode(string id)
        {
            try
            {
                return Ok(_systemLanguageCodeLogic.Get(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost, Route("languageCode")]
        [ProducesResponseType(typeof(SystemLanguageCodePoco), 200)]
        public ActionResult PostSystemLanguageCode([FromBody] SystemLanguageCodePoco[] data)
        {
            _systemLanguageCodeLogic.Add(data);
            return Ok();
        }

        [HttpPut, Route("languageCode")]
        [ProducesResponseType(typeof(SystemLanguageCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutSystemLanguageCode([FromBody] SystemLanguageCodePoco[] data)
        {
            try
            {   _systemLanguageCodeLogic.Update(data);
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpDelete, Route("languageCode")]
        [ProducesResponseType(typeof(SystemLanguageCodePoco), 200)]
        public ActionResult DeleteSystemLanguageCode([FromBody] SystemLanguageCodePoco[] data)
        {
            _systemLanguageCodeLogic.Delete(data);
            return Ok();
        }
    }
}
