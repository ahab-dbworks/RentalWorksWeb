using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Settings.PresentationLayerActivity;
using WebApi.Modules.Settings.MiscellaneousSettings.MiscRate;

namespace WebApi.Modules.Settings.PresentationLayerActivityOverride
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"HWjX0WDoiG79H")]
    public class PresentationLayerActivityOverrideController : AppDataController
    {
        public PresentationLayerActivityOverrideController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PresentationLayerActivityOverrideLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivityoverride/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "s5mcYcsnuPayq", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("User Defined", RwGlobals.PRESENTATION_LAYER_ACTIVITY_REC_TYPE_USER_DEFINED_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivityoverride/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"QQ4urqItSSUZr", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"8Yv1SCYVPCvvu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivityoverride 
        [HttpGet]
        [FwControllerMethod(Id:"sTW80cbSWsG2B", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PresentationLayerActivityOverrideLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PresentationLayerActivityOverrideLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivityoverride/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"unn07ckPnbffx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<PresentationLayerActivityOverrideLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PresentationLayerActivityOverrideLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivityoverride 
        [HttpPost]
        [FwControllerMethod(Id:"NdQdtgeLlIeuU", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PresentationLayerActivityOverrideLogic>> NewAsync([FromBody]PresentationLayerActivityOverrideLogic l)
        {
            return await DoNewAsync<PresentationLayerActivityOverrideLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/presentationlayeractivityoverride/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "OlnWQiwR1tCf5", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PresentationLayerActivityOverrideLogic>> EditAsync([FromRoute] string id, [FromBody]PresentationLayerActivityOverrideLogic l)
        {
            return await DoEditAsync<PresentationLayerActivityOverrideLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/presentationlayeractivityoverride/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"kxiO2Eoj2cdmk", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PresentationLayerActivityOverrideLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivityoverride/validatepresentationlayeractivity/browse
        [HttpPost("validatepresentationlayeractivity/browse")]
        [FwControllerMethod(Id: "WMWIj7bpyEx0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePresentationLayerActivityBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PresentationLayerActivityLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivityoverride/validatemaster/browse
        [HttpPost("validatemaster/browse")]
        [FwControllerMethod(Id: "wuckWGLODO9N", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateMasterBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<MiscRateLogic>(browseRequest);
        }
    }
}
