using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FacilitySettings.FacilityRate
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
        [FwControllerMethod(Id:"YouYawc02Ta", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"N0YCtLSwfUc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/facilityrate 
        [HttpGet]
        [FwControllerMethod(Id:"59sEAgf3CcM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<FacilityRateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityRateLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/facilityrate/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"sKAlQzkau67", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<FacilityRateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityRateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/facilityrate 
        [HttpPost]
        [FwControllerMethod(Id:"Cg6FGgP6S6M", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<FacilityRateLogic>> NewAsync([FromBody]FacilityRateLogic l)
        {
            return await DoNewAsync<FacilityRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/facilityrate/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "K30wBZ2Z0BSeE", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<FacilityRateLogic>> EditAsync([FromRoute] string id, [FromBody]FacilityRateLogic l)
        {
            return await DoEditAsync<FacilityRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/facilityrate/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"lO1rpLPVwNV", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FacilityRateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
