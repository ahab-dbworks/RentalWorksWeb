using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Settings.FacilitySettings.FacilityStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"xJ4UyFe61kC")]
    public class FacilityStatusController : AppDataController
    {
        public FacilityStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitystatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"pag3dNi989O")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"qpcDK3gVTsd")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitystatus
        [HttpGet]
        [FwControllerMethod(Id:"xYIDDOLzdho")]
        public async Task<ActionResult<IEnumerable<FacilityStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitystatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Iyz07pUTcsp")]
        public async Task<ActionResult<FacilityStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitystatus
        [HttpPost]
        [FwControllerMethod(Id:"aNERQ33ltbW")]
        public async Task<ActionResult<FacilityStatusLogic>> PostAsync([FromBody]FacilityStatusLogic l)
        {
            return await DoPostAsync<FacilityStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilitystatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"0jKhVmj9NiS")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FacilityStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
