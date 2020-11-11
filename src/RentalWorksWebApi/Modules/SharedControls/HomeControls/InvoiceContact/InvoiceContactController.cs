using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Agent.Contact;
using WebApi.Modules.Settings.ContactSettings.ContactTitle;
namespace WebApi.Modules.HomeControls.InvoiceContact
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "9Rbf19uTJj1tv")]
    public class InvoiceContactController : AppDataController
    {
        public InvoiceContactController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InvoiceContactLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicecontact/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "08b1zCIRYDw62", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "RCrWewVQX4ZQK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/invoicecontact 
        //[HttpGet]
        //[FwControllerMethod(Id:"xxxxxxxxxxxx", ActionType: FwControllerActionTypes.Browse)]
        //public async Task<ActionResult<IEnumerable<InvoiceContactLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<InvoiceContactLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/invoicecontact/A0000001 
        //[HttpGet("{id}")]
        //[FwControllerMethod(Id:"xxxxxxxxxxxx", ActionType: FwControllerActionTypes.Browse)]
        //public async Task<ActionResult<InvoiceContactLogic>> GetOneAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<InvoiceContactLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/invoicecontact 
        //[HttpPost]
        //[FwControllerMethod(Id:"xxxxxxxxxxxx", ActionType: FwControllerActionTypes.New)]
        //public async Task<ActionResult<InvoiceContactLogic>> NewAsync([FromBody]InvoiceContactLogic l)
        //{
        //    return await DoNewAsync<InvoiceContactLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        ////justin hoffman 12/16/2019 #1471
        //// this is a special Put command to support a behavior of this API that used to exist before we split Post in to Post/Put
        //// user can Put an object here with a blank ID and the API will treat it as a Post for New.
        //// PUT api/v1/invoicecontact
        //[HttpPut]
        //[FwControllerMethod(Id: "xxxxxxxxxxxx", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<InvoiceContactLogic>> NewAsync2([FromRoute] string id, [FromBody]InvoiceContactLogic l)
        //{
        //    return await DoNewAsync<InvoiceContactLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// PUT api/v1/invoicecontact/A0000001
        //[HttpPut("{id}")]
        //[FwControllerMethod(Id: "xxxxxxxxxxxx", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<InvoiceContactLogic>> EditAsync([FromRoute] string id, [FromBody]InvoiceContactLogic l)
        //{
        //    return await DoEditAsync<InvoiceContactLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/invoicecontact/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"xxxxxxxxxxxx", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<InvoiceContactLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/invoicecontact/validatecontact/browse
        //[HttpPost("validatecontact/browse")]
        //[FwControllerMethod(Id: "xxxxxxxxxxxx", ActionType: FwControllerActionTypes.Browse)]
        //public async Task<ActionResult<FwJsonDataTable>> ValidateContactBrowseAsync([FromBody]BrowseRequest browseRequest)
        //{
        //    return await DoBrowseAsync<ContactLogic>(browseRequest);
        //}
        ////------------------------------------------------------------------------------------ 
        //// POST api/v1/invoicecontact/validatecontacttitle/browse
        //[HttpPost("validatecontacttitle/browse")]
        //[FwControllerMethod(Id: "xxxxxxxxxxxx", ActionType: FwControllerActionTypes.Browse)]
        //public async Task<ActionResult<FwJsonDataTable>> ValidateContactTitleBrowseAsync([FromBody]BrowseRequest browseRequest)
        //{
        //    return await DoBrowseAsync<ContactTitleLogic>(browseRequest);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
