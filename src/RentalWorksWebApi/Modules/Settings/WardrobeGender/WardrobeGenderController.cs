using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeGender
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WardrobeGenderController : AppDataController
    {
        public WardrobeGenderController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeGenderLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobegender/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WardrobeGenderLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobegender
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeGenderLogic>(pageno, pagesize, sort, typeof(WardrobeGenderLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobegender/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeGenderLogic>(id, typeof(WardrobeGenderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobegender
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WardrobeGenderLogic l)
        {
            return await DoPostAsync<WardrobeGenderLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobegender/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WardrobeGenderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobegender/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}