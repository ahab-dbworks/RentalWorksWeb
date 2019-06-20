using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.Modules.Administrator.AlertWebUsers;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
namespace WebApi.Modules.Administrator.AlertWebUsers
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "REgcmntq4LWE")]
    public class AlertWebUsersController : AppDataController
    {
        public AlertWebUsersController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AlertWebUsersLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alertwebusers/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "W8tc28TbRfbq")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alertwebusers/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "UyjhSZeRz0kiD")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/alertwebusers 
        [HttpGet]
        [FwControllerMethod(Id: "BLt1TByqGEDu")]
        public async Task<ActionResult<IEnumerable<AlertWebUsersLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AlertWebUsersLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/alertwebusers/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "6pbS1g4dEEHi")]
        public async Task<ActionResult<AlertWebUsersLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AlertWebUsersLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alertwebusers 
        [HttpPost]
        [FwControllerMethod(Id: "dPxB8IbKkXnJx")]
        public async Task<ActionResult<AlertWebUsersLogic>> PostAsync([FromBody]AlertWebUsersLogic l)
        {
            return await DoPostAsync<AlertWebUsersLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/alertwebusers/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "B866tX0CwraC")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
