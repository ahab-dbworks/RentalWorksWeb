using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VehicleMake
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class VehicleMakeController : AppDataController
    {
        public VehicleMakeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleMakeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclemake/browse
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
        // GET api/v1/vehiclemake
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMakeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleMakeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehiclemake/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMakeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleMakeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehiclemake
        [HttpPost]
        public async Task<ActionResult<VehicleMakeLogic>> PostAsync([FromBody]VehicleMakeLogic l)
        {
            return await DoPostAsync<VehicleMakeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehiclemake/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
}
}