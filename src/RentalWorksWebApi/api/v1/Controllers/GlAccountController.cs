using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class GlAccountController : RwDataController
    {
        public GlAccountController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/glaccount/browse
        [HttpPost("browse")]
        public IActionResult Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return doBrowse(browseRequest, typeof(GlAccountLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/glaccount
        [HttpGet]
        public IActionResult Get(int pageno, int pagesize)
        {
            return doGet<GlAccountLogic>(pageno, pagesize, typeof(GlAccountLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/glaccount/A0000001
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return doGet<GlAccountLogic>(id, typeof(GlAccountLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/glaccount
        [HttpPost]
        public IActionResult Post([FromBody]GlAccountLogic l)
        {
            return doPost<GlAccountLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/glaccount/A0000001
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return doDelete(id, typeof(GlAccountLogic));
        }
        //------------------------------------------------------------------------------------
    }
}