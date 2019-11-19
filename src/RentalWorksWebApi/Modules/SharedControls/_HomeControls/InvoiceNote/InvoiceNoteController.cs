using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.InvoiceNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "PjT15E4lWmo7")]
    public class InvoiceNoteController : AppDataController
    {
        public InvoiceNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InvoiceNoteLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicenote/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "9dnx8rl1QWyQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicenote/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "IXG6uR8OyIaO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoicenote 
        [HttpGet]
        [FwControllerMethod(Id: "p2CyCRju3bqY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InvoiceNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InvoiceNoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoicenote/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "Fo5dYJL3aKvw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InvoiceNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InvoiceNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicenote 
        [HttpPost]
        [FwControllerMethod(Id: "QrmQpq1hLEW7", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InvoiceNoteLogic>> NewAsync([FromBody]InvoiceNoteLogic l)
        {
            return await DoNewAsync<InvoiceNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/invoicenote/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "ntPN1AwbiCr6r", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InvoiceNoteLogic>> EditAsync([FromRoute] string id, [FromBody]InvoiceNoteLogic l)
        {
            return await DoEditAsync<InvoiceNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/invoicenote/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "Rc1UU2xwTMTZ", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InvoiceNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
