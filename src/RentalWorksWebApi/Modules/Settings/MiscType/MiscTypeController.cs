using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.MiscType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class MiscTypeController : AppDataController
    {
        public MiscTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MiscTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/misctype/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MiscTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misctype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MiscTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MiscTypeLogic>(pageno, pagesize, sort, typeof(MiscTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misctype/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<MiscTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<MiscTypeLogic>(id, typeof(MiscTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/misctype
        [HttpPost]
        public async Task<ActionResult<MiscTypeLogic>> PostAsync([FromBody]MiscTypeLogic l)
        {
            return await DoPostAsync<MiscTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/misctype/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(MiscTypeLogic));
        }
        //------------------------------------------------------------------------------------
    }
}