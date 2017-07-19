using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class StateController : RwDataController
    {
        public StateController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/State/browse
        [HttpPost("browse")]
        public IActionResult Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return doBrowse(browseRequest, typeof(StateLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/State
        [HttpGet]
        public IActionResult Get(int pageno, int pagesize)
        {
            return doGet<StateLogic>(pageno, pagesize, typeof(StateLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/State/A0000001
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return doGet<StateLogic>(id, typeof(StateLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/State
        [HttpPost]
        public IActionResult Post([FromBody]StateLogic l)
        {
            return doPost<StateLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/State/A0000001
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return doDelete(id, typeof(StateLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/State/validateduplicate
        [HttpPost("validateduplicate")]
        public IActionResult ValidateDuplicate(ValidateDuplicateRequest request)
        {
            return doValidateDuplicate(request);
        }
        //------------------------------------------------------------------------------------
    }
}