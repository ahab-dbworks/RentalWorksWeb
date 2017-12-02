using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.ContactTitle
{
    [Route("api/v1/[controller]")]
    public class ContactTitleController : RwDataController
    {
        public ContactTitleController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/contacttitle/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{ADCFFDE3-E33B-4BE8-9B9C-B040617A332E}")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ContactTitleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contacttitle
        [HttpGet]
        [Authorize(Policy = "{9E71C0F1-70A3-494B-A871-FFE69100BBB3}")]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<ContactTitleLogic>(pageno, pagesize, sort, typeof(ContactTitleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contacttitle/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{470B79CB-242D-4104-AF97-6416283CBCA8}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<ContactTitleLogic>(id, typeof(ContactTitleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contacttitle
        [HttpPost]
        [Authorize(Policy = "{194C54FA-A4AC-4CD9-9B23-16BB87B0B214}")]
        public async Task<IActionResult> PostAsync([FromBody]ContactTitleLogic l)
        {
            return await DoPostAsync<ContactTitleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/contacttitle/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{16D7F840-B67F-497B-804B-F806B413F806}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(ContactTitleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contacttitle/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "{48FAF20E-04C5-4A6B-ABAA-21F2A30395E1}")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}