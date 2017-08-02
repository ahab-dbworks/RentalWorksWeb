using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.CustomerType
{
    [Route("api/v1/[controller]")]
    public class CustomerTypeController : RwDataController
    {
        public CustomerTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CustomerTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerTypeLogic>(pageno, pagesize, sort, typeof(CustomerTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerTypeLogic>(id, typeof(CustomerTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CustomerTypeLogic l)
        {
            return await DoPostAsync<CustomerTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customertype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CustomerTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
    //------------------------------------------------------------------------------------
}
}