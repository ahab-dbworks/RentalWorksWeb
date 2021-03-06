using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
namespace WebApi.Modules.Utilities.RateUpdateItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "QQwyjnERS0Jx")]
    public class RateUpdateItemController : AppDataController
    {
        public RateUpdateItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RateUpdateItemLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rateupdateitem/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "MK2ypz6dQLIX1", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Pending Modification", RwGlobals.RATE_UPDATE_UTILITY_PENDING_MODIFICATION_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "c4DUy6bgAlPn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "i8emEejBnJC0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rateupdateitem 
        [HttpGet]
        [FwControllerMethod(Id: "IY0vmxRY0LML", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<RateUpdateItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateUpdateItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rateupdateitem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "JtHFU6RLESDt", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<RateUpdateItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateUpdateItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateitem 
        //[HttpPost]
        //[FwControllerMethod(Id: "", ActionType: FwControllerActionTypes.New)]
        //public async Task<ActionResult<RateUpdateItemLogic>> NewAsync([FromBody]RateUpdateItemLogic l)
        //{
        //    return await DoNewAsync<RateUpdateItemLogic>(l);
        //}
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/rateupdateitem/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "8X830IANQIqY", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<RateUpdateItemLogic>> EditAsync([FromRoute] string id, [FromBody]RateUpdateItemLogic l)
        {
            return await DoEditAsync<RateUpdateItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/rateupdateitem/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<RateUpdateItemLogic>(id);
        //}
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateutility/many
        [HttpPost("many")]
        [FwControllerMethod(Id: "KnQRxyPTjttq", ActionType: FwControllerActionTypes.Edit)]
        public async Task<List<ActionResult<RateUpdateItemLogic>>> PostAsync([FromBody]List<RateUpdateItemLogic> l)
        {
            FwBusinessLogicList l2 = new FwBusinessLogicList();
            l2.AddRange(l);
            return await DoPostAsync<RateUpdateItemLogic>(l2);
        }
        //------------------------------------------------------------------------------------ 
    }
}
