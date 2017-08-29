using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.VendorNoteGrid
{
    [Route("api/v1/[controller]")]
    public class VendorNoteGridController : RwDataController
    {
        public VendorNoteGridController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryattributevalue/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VendorNoteGridLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventoryattributevalue
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorNoteGridLogic>(pageno, pagesize, sort, typeof(VendorNoteGridLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventoryattributevalue/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorNoteGridLogic>(id, typeof(VendorNoteGridLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryattributevalue
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]VendorNoteGridLogic l)
        {
            return await DoPostAsync<VendorNoteGridLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryattributevalue
        [HttpPost("saveform")]
        public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request)
        {
            return await DoSaveFormAsync<VendorNoteGridLogic>(request, typeof(VendorNoteGridLogic));
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventoryattributevalue/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VendorNoteGridLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventoryattributevalue/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}