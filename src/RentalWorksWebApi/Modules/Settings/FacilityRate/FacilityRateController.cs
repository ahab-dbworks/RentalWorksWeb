using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FacilityRate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"rA0UZvSMuSF")]
    public class FacilityRateController : AppDataController
    {
        public FacilityRateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityRateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/facilityrate/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"YouYawc02Ta")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"N0YCtLSwfUc")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/facilityrate 
        [HttpGet]
        [FwControllerMethod(Id:"59sEAgf3CcM")]
        public async Task<ActionResult<IEnumerable<FacilityRateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityRateLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/facilityrate/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"sKAlQzkau67")]
        public async Task<ActionResult<FacilityRateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityRateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/facilityrate 
        [HttpPost]
        [FwControllerMethod(Id:"Cg6FGgP6S6M")]
        public async Task<ActionResult<FacilityRateLogic>> PostAsync([FromBody]FacilityRateLogic l)
        {
            return await DoPostAsync<FacilityRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/facilityrate/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"lO1rpLPVwNV")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FacilityRateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
