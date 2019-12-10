using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Settings.LaborSettings.LaborType;
using WebApi.Modules.Settings.LaborSettings.LaborCategory;
using WebApi.Modules.Settings.InventorySettings.Unit;
using WebApi.Modules.Settings.SubCategory;

namespace WebApi.Modules.Settings.LaborSettings.Position
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"ZKb7ET3WoPs2")]
    public class PositionController : AppDataController
    {
        public PositionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PositionLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/position/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"CSgiodfm4Gv3", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"SQKMLiwGHtiJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/position 
        [HttpGet]
        [FwControllerMethod(Id:"0SCcUCSgNDEE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PositionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PositionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/position/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"BmbDTN0hlqyX", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<PositionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PositionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/position 
        [HttpPost]
        [FwControllerMethod(Id:"GIDNEYljjedr", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PositionLogic>> NewAsync([FromBody]PositionLogic l)
        {
            return await DoNewAsync<PositionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/position/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "mx3SrxcApeEt8", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PositionLogic>> EditAsync([FromRoute] string id, [FromBody]PositionLogic l)
        {
            return await DoEditAsync<PositionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/position/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"DAghbxbvBTj2", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PositionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/settings/validatelabortype/browse
        [HttpPost("validatelabortype/browse")]
        [FwControllerMethod(Id: "ybCEYy1aKIFD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLaborTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<LaborTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validatelaborcategory/browse
        [HttpPost("validatelaborcategory/browse")]
        [FwControllerMethod(Id: "osdKOQacAy5x", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLaborCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<LaborCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validatesubcategory/browse
        [HttpPost("validatesubcategory/browse")]
        [FwControllerMethod(Id: "TPjXM7D14LVS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SubCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateunit/browse
        [HttpPost("validateunit/browse")]
        [FwControllerMethod(Id: "ENFjH4JwB2K2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUnitBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UnitLogic>(browseRequest);
        }
    }
} 
