using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class WarehouseController : RwDataController
    {
        public WarehouseController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype/browse
        [HttpPost("browse")]
        public IActionResult Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return doBrowse(browseRequest, typeof(WarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype
        [HttpGet]
        public IActionResult Get(int pageno, int pagesize)
        {
            return doGet<WarehouseLogic>(pageno, pagesize, typeof(WarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype/A0000001
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return doGet<WarehouseLogic>(id, typeof(WarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype
        [HttpPost]
        public IActionResult Post([FromBody]WarehouseLogic l)
        {
            return doPost<WarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customertype/A0000001
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return doDelete(id, typeof(WarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype/validateduplicate
        [HttpPost("validateduplicate")]
        public IActionResult ValidateDuplicate(ValidateDuplicateRequest request)
        {
            return doValidateDuplicate(request);
        }
        //------------------------------------------------------------------------------------
    }
}