using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class ContactEventController : RwDataController
    {
        public ContactEventController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/contactevent/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ContactEventLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contactevent
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<ContactEventLogic>(pageno, pagesize, sort, typeof(ContactEventLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contactevent/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<ContactEventLogic>(id, typeof(ContactEventLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contactevent
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ContactEventLogic l)
        {
            return await DoPostAsync<ContactEventLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/contactevent/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(ContactEventLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contactevent/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}