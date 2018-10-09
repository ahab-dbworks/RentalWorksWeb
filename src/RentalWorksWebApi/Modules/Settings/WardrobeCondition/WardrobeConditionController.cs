using FwStandard.SqlServer;
using System.Collections.Generic;
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
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecondition
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<ActionResult<IEnumerable<WardrobeConditionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeConditionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecondition/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<ActionResult<WardrobeConditionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeConditionLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecondition
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<ActionResult<WardrobeConditionLogic>> PostAsync([FromBody]WardrobeConditionLogic l)
        {
            return await DoPostAsync<WardrobeConditionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobecondition/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}