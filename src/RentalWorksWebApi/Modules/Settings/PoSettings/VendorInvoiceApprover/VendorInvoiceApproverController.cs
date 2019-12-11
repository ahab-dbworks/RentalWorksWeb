using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;

namespace WebApi.Modules.Settings.PoSettings.VendorInvoiceApprover
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"3Hhg9Bl5Rm1mT")]
    public class VendorInvoiceApproverController : AppDataController
    {
        public VendorInvoiceApproverController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceApproverLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceapprover/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ZfU3ujQgYN8CK", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"1ba1KbE4fTWjc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoiceapprover 
        [HttpGet]
        [FwControllerMethod(Id:"PMkRwT0lIGIGp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VendorInvoiceApproverLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorInvoiceApproverLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoiceapprover/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Iti5XAiUb0mGN", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<VendorInvoiceApproverLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorInvoiceApproverLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceapprover 
        [HttpPost]
        [FwControllerMethod(Id:"B1rBGrm0H5Ny7", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VendorInvoiceApproverLogic>> NewAsync([FromBody]VendorInvoiceApproverLogic l)
        {
            return await DoNewAsync<VendorInvoiceApproverLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/vendorinvoiceapprover/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "ZmRuB5wOV2eh7", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VendorInvoiceApproverLogic>> EditAsync([FromRoute] string id, [FromBody]VendorInvoiceApproverLogic l)
        {
            return await DoEditAsync<VendorInvoiceApproverLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/vendorinvoiceapprover/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"IrjXa5WBYpIQf", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorInvoiceApproverLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceapprover/validatelocation/browse
        [HttpPost("validaterole/browse")]
        [FwControllerMethod(Id: "uLIsfH08z5Dq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceapprover/validatedepartment/browse
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "YO91k9V7oFvh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceapprover/validateuser/browse
        [HttpPost("validateuser/browse")]
        [FwControllerMethod(Id: "qwWJflfg0HKA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUserBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
    }
}
