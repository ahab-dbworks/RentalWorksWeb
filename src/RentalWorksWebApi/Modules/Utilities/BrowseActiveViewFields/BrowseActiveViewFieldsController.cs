using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Utilities.BrowseActiveViewFields
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "orE2GTiLSL78u")]
    public class BrowseActiveViewFieldsController : AppDataController
    {
        public BrowseActiveViewFieldsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BrowseActiveViewFieldsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/browseactiveviewfields/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "VPjHMyPIydkp")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/browseactiveviewfields/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "7GT4iSXVJRqU")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/browseactiveviewfields 
        [HttpGet]
        [FwControllerMethod(Id: "EFqgNmI2uOJX")]
        public async Task<ActionResult<IEnumerable<BrowseActiveViewFieldsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BrowseActiveViewFieldsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/browseactiveviewfields/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "kBLo5wQO7sRc")]
        public async Task<ActionResult<BrowseActiveViewFieldsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BrowseActiveViewFieldsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/browseactiveviewfields 
        [HttpPost]
        [FwControllerMethod(Id: "TY5nOe465Jdd")]
        public async Task<ActionResult<BrowseActiveViewFieldsLogic>> PostAsync([FromBody]BrowseActiveViewFieldsLogic l)
        {
            return await DoPostAsync<BrowseActiveViewFieldsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/browseactiveviewfields/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "ASkfPgAgqqNK")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<BrowseActiveViewFieldsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
