
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemCountryCodeController : ControllerBase
    {
        private readonly SystemCountryCodeLogic _systemCountryCodeLogic;
        public SystemCountryCodeController()
        {
            EFGenericRepository<SystemCountryCodePoco> repo = new EFGenericRepository<SystemCountryCodePoco>();
            _systemCountryCodeLogic = new SystemCountryCodeLogic(repo);
        }

        [HttpGet, Route("countryCode")]
        [ProducesResponseType(typeof(SystemCountryCodePoco), 200)]
        public ActionResult GetAllSystemCountryCode()
        {
            return Ok(_systemCountryCodeLogic.GetAll());

        }
        [HttpGet, Route("countryCode/{systemCountryCodeId}")]
        [ProducesResponseType(typeof(SystemCountryCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSystemCountryCode(string id)
        {
            try
            {
                return Ok(_systemCountryCodeLogic.Get(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost, Route("countryCode")]
        [ProducesResponseType(typeof(SystemCountryCodePoco), 200)]
        public ActionResult PostSystemCountryCode([FromBody] SystemCountryCodePoco[] data)
        { _systemCountryCodeLogic.Add(data);
            return Ok();
        }




        [HttpPut, Route("countryCode")]
        [ProducesResponseType(typeof(SystemCountryCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutSystemCountryCode([FromBody] SystemCountryCodePoco[] data)
        {
            try
            { _systemCountryCodeLogic.Update(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete, Route("countryCode")]
        [ProducesResponseType(typeof(SystemCountryCodePoco), 200)]
        public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco[] data)
        {
            _systemCountryCodeLogic.Delete(data);
            return Ok();
        }
    }


    }

