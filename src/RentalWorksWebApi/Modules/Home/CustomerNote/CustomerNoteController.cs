using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Home.CustomerNote
{
    [Route("api/v1/[controller]")]
    public class CustomerNoteController : RwDataController
    {
        public CustomerNoteController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/customernote/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CustomerNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customernote
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerNoteLogic>(pageno, pagesize, sort, typeof(CustomerNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customernote/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerNoteLogic>(id, typeof(CustomerNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customernote
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CustomerNoteLogic l)
        {
            return await DoPostAsync<CustomerNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customernote
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<CustomerNoteLogic>(request, typeof(CustomerNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customernote/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CustomerNoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customernote/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}