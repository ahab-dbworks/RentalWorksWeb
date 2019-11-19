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
        [FwControllerMethod(Id:"MHlbiSdovGWE")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"hOF38oVCra1v")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype 
        [HttpGet]
        [FwControllerMethod(Id:"EYbePapZ2YwD")]
        public async Task<ActionResult<IEnumerable<OrderTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"FNlE3pAUrgB6")]
        public async Task<ActionResult<OrderTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype 
        [HttpPost]
        [FwControllerMethod(Id:"oKiVbogMmpRo")]
        public async Task<ActionResult<OrderTypeLogic>> PostAsync([FromBody]OrderTypeLogic l)
        {
            return await DoPostAsync<OrderTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertype/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"1EbvgGERbr1X")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
