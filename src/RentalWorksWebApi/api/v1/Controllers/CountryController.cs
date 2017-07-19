using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class CountryController : RwDataController
    {
        public CountryController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/Country/browse
        [HttpPost("browse")]
        public IActionResult Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return doBrowse(browseRequest, typeof(CountryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/Country
        [HttpGet]
        public IActionResult Get(int pageno, int pagesize)
        {
            return doGet<CountryLogic>(pageno, pagesize, typeof(CountryLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/Country/A0000001
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return doGet<CountryLogic>(id, typeof(CountryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/Country
        [HttpPost]
        public IActionResult Post([FromBody]CountryLogic l)
        {
            return doPost<CountryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/Country/A0000001
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return doDelete(id, typeof(CountryLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/Country/validateduplicate
        [HttpPost("validateduplicate")]
        public IActionResult ValidateDuplicate(ValidateDuplicateRequest request)
        {
            return doValidateDuplicate(request);
        }
        //------------------------------------------------------------------------------------
    }
}