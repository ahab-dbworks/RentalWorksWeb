using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.DealClassification
{
    [Route("api/v1/[controller]")]
    public class DealClassificationController : RwDataController
    {
        public DealClassificationController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{16457FA2-FB52-4FA9-A94D-3DAB697D6B21}")]
        public async Task<IActionResult> Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DealClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus
        [HttpGet]
        [Authorize(Policy = "{EC95C419-BD71-46CB-8BF6-17CB1164552C}")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealClassificationLogic>(pageno, pagesize, sort, typeof(DealClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{73EEDAAC-6133-476A-837B-FCDAED43BDF7}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealClassificationLogic>(id, typeof(DealClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost]
        [Authorize(Policy = "{3BB08EBF-52CE-4F9C-980D-F162570018CC}")]
        public async Task<IActionResult> PostAsync([FromBody]DealClassificationLogic l)
        {
            return await DoPostAsync<DealClassificationLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customerstatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{941CE445-FC04-44ED-A041-F9705334AE9A}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DealClassificationLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "{71765BAC-A405-4B5C-8C0C-1EC6D5E7598F}")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}