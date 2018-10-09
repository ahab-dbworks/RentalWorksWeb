using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Unit
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class UnitController : AppDataController
    {
        public UnitController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UnitLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/unit/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(UnitLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UnitLogic>(pageno, pagesize, sort, typeof(UnitLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unit/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UnitLogic>(id, typeof(UnitLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/unit
        [HttpPost]
        public async Task<ActionResult<UnitLogic>> PostAsync([FromBody]UnitLogic l)
        {
            return await DoPostAsync<UnitLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/unit/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(UnitLogic));
        }
        //------------------------------------------------------------------------------------
    }
}