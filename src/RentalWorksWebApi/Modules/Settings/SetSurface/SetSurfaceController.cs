using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SetSurface
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class SetSurfaceController : AppDataController
    {
        public SetSurfaceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SetSurfaceLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/setsurface/browse
        [HttpPost("browse")]
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
        // GET api/v1/setsurface
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SetSurfaceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SetSurfaceLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setsurface/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<SetSurfaceLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SetSurfaceLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setsurface
        [HttpPost]
        public async Task<ActionResult<SetSurfaceLogic>> PostAsync([FromBody]SetSurfaceLogic l)
        {
            return await DoPostAsync<SetSurfaceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/setsurface/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    }
}