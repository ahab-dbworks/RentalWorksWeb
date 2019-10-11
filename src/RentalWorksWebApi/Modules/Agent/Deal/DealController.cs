using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Agent.Deal
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"8WdRib388fFF")]
    public class DealController : AppDataController
    {
        public DealController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/deal/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"2GGRtZiCZeW7")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"FHXGMbYmyiFG")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/deal 
        [HttpGet]
        [FwControllerMethod(Id:"xQzGjNkxeW2l")]
        public async Task<ActionResult<IEnumerable<DealLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/deal/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"flXfXIRxf178")]
        public async Task<ActionResult<DealLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/deal 
        [HttpPost]
        [FwControllerMethod(Id:"cEdOt5p0EujB")]
        public async Task<ActionResult<DealLogic>> PostAsync([FromBody]DealLogic l)
        {
            return await DoPostAsync<DealLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/deal/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"0cMsOO5hEIR3")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DealLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
