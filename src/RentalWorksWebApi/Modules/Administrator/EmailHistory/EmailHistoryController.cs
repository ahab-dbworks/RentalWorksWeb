using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;

namespace WebApi.Modules.Administrator.EmailHistory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "3XHEm3Q8WSD8z")]
    public class EmailHistoryController : AppDataController
    {
        public EmailHistoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(EmailHistoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emailhistory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "d0CZr8bgwdxL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emailhistory/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "yNIVlLI5Yyn5m", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/emailhistory 
        [HttpGet]
        [FwControllerMethod(Id: "vhAvYNjOIugg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<EmailHistoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<EmailHistoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/emailhistory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "MYSg6hx3hsiL", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<EmailHistoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<EmailHistoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emailhistory 
        //[HttpPost]
        //[FwControllerMethod(Id: "1DaAm2JUufFg", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<EmailHistoryLogic>> PostAsync([FromBody]EmailHistoryLogic l)
        //{
        //    return await DoPostAsync<EmailHistoryLogic>(l);
        //}
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/emailhistory/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "YYgEXn11pJj6", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <EmailHistoryLogic>DoDeleteAsync(id);
        //}
        //------------------------------------------------------------------------------------ 
    }
}
