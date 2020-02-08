using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderSettings.OrderStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "eVptJYEMeXsD9")]
    public class OrderStatusController : AppDataController
    {
        public OrderStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderStatusLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderstatus/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "Dr0R4cQwqT9ip", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "F1wXrwSelwhQo", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/orderstatus 
        //[HttpGet]
        //[FwControllerMethod(Id: "zinmnKZwpOvHM", ActionType: FwControllerActionTypes.Browse)]
        //public async Task<ActionResult<IEnumerable<OrderStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<OrderStatusLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/orderstatus/A0000001 
        //[HttpGet("{id}")]
        //[FwControllerMethod(Id: "a6PIcfPJZQrXi", ActionType: FwControllerActionTypes.View)]
        //public async Task<ActionResult<OrderStatusLogic>> GetOneAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<OrderStatusLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/orderstatus 
        //[HttpPost]
        //[FwControllerMethod(Id: "9k86M5cjTJC2G", ActionType: FwControllerActionTypes.New)]
        //public async Task<ActionResult<OrderStatusLogic>> NewAsync([FromBody]OrderStatusLogic l)
        //{
        //    return await DoNewAsync<OrderStatusLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// PUT api/v1/orderstatus/A0000001
        //[HttpPut("{id}")]
        //[FwControllerMethod(Id: "NFAc6K7KlbBLP", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<OrderStatusLogic>> EditAsync([FromRoute] string id, [FromBody]OrderStatusLogic l)
        //{
        //    return await DoEditAsync<OrderStatusLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/orderstatus/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "Becsf9z5uwM01", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<OrderStatusLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
