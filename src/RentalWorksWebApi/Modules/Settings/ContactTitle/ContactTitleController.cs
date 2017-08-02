using FwStandard.Models;
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
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ContactTitleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contacttitle
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<ContactTitleLogic>(pageno, pagesize, sort, typeof(ContactTitleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contacttitle/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<ContactTitleLogic>(id, typeof(ContactTitleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contacttitle
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ContactTitleLogic l)
        {
            return await DoPostAsync<ContactTitleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/contacttitle/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(ContactTitleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contacttitle/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}