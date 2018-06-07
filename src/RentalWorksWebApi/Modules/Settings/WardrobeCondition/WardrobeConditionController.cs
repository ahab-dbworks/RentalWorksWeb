using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeCondition
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WardrobeConditionController : AppDataController
    {
        public WardrobeConditionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeConditionLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecondition/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WardrobeConditionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecondition
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeConditionLogic>(pageno, pagesize, sort, typeof(WardrobeConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecondition/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeConditionLogic>(id, typeof(WardrobeConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecondition
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> PostAsync([FromBody]WardrobeConditionLogic l)
        {
            return await DoPostAsync<WardrobeConditionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobecondition/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WardrobeConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecondition/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request) => await DoValidateDuplicateAsync(request);
        //------------------------------------------------------------------------------------
    }
}