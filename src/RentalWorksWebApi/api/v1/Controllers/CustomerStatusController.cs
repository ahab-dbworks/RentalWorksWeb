using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class CustomerStatusController : RwDataController
    {
        public CustomerStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        [HttpPost("browse")]
        public IActionResult Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return doBrowse(browseRequest, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus
        [HttpGet]
        public IActionResult Get(int pageno, int pagesize)
        {
            return doGet<CustomerStatusLogic>(pageno, pagesize, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return doGet<CustomerStatusLogic>(id, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost]
        public IActionResult Post([FromBody]CustomerStatusLogic l)
        {
            return doPost<CustomerStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customerstatus/A0000001
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return doDelete(id, typeof(CustomerStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/validateduplicate
        [HttpPost("validateduplicate")]
        public IActionResult ValidateDuplicate(ValidateDuplicateRequest request)
        {
            return new OkObjectResult(true);
        }
        //------------------------------------------------------------------------------------
    }
}