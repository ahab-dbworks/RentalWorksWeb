using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "{A681A1CC-5F38-4C6A-A96A-9B72EC884EB4}")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CustomerTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype
        [HttpGet]
        [Authorize(Policy = "{38EC0AB9-BA14-429F-ACEE-F5F7A9130A7D}")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerTypeLogic>(pageno, pagesize, sort, typeof(CustomerTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{B8EB8539-A5EE-4E9F-90A8-599BFB548988}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerTypeLogic>(id, typeof(CustomerTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype
        [HttpPost]
        [Authorize(Policy = "{4A6544A8-12A3-4E13-92CB-19207AF1187E}")]
        public async Task<IActionResult> PostAsync([FromBody]CustomerTypeLogic l)
        {
            return await DoPostAsync<CustomerTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customertype/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{AEAA52B7-CD39-4EFC-A9AB-54BB373F3A66}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CustomerTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "{F88A7D84-D56C-4AD7-AE9C-4B55EA53CD17}")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
    //------------------------------------------------------------------------------------
}
}