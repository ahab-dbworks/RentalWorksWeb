using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace WebApi.Modules.Settings.OrderSettings.OrderType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"yFStSrvTlwWY")]
    public class OrderTypeController : AppDataController
    {
        public OrderTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"MHlbiSdovGWE", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"hOF38oVCra1v", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype 
        [HttpGet]
        [FwControllerMethod(Id:"EYbePapZ2YwD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OrderTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"FNlE3pAUrgB6", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<OrderTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype 
        [HttpPost]
        [FwControllerMethod(Id:"oKiVbogMmpRo", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OrderTypeLogic>> NewAsync([FromBody]OrderTypeLogic l)
        {
            return await DoNewAsync<OrderTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/ordertype/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "UFiuBbZA52O5c", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrderTypeLogic>> EditAsync([FromRoute] string id, [FromBody]OrderTypeLogic l)
        {
            return await DoEditAsync<OrderTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertype/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"1EbvgGERbr1X", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
