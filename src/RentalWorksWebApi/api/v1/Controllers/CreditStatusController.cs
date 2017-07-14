using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class CreditStatusController : RwDataController
    {
        public CreditStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus/browse
        [HttpPost("browse")]
        public IActionResult Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return doBrowse(browseRequest, typeof(CreditStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/creditstatus
        [HttpGet]
        public IActionResult Get(int pageno, int pagesize)
        {
            return doGet<CreditStatusLogic>(pageno, pagesize, typeof(CreditStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/creditstatus/A0000001
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return doGet<CreditStatusLogic>(id, typeof(CreditStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus
        [HttpPost]
        public IActionResult Post([FromBody]CreditStatusLogic l)
        {
            return doPost<CreditStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/creditstatus/A0000001
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return doDelete(id, typeof(CreditStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/creditstatus/validateduplicate
        [HttpPost("validateduplicate")]
        public IActionResult ValidateDuplicate(ValidateDuplicateRequest request)
        {
            return doValidateDuplicate(request);
        }
        //------------------------------------------------------------------------------------
    }
}