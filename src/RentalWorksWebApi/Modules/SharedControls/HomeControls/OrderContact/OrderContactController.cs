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
namespace WebApi.Modules.HomeControls.OrderContact
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"7CUe9WvpWNat")]
    public class OrderContactController : AppDataController
    {
        public OrderContactController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderContactLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordercontact/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"nS6HRjeiLDiu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"YJhndz1jgfBZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordercontact 
        [HttpGet]
        [FwControllerMethod(Id:"6tFFA6e3LDF2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OrderContactLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderContactLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordercontact/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"tfInR9PXZqvg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<OrderContactLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderContactLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordercontact 
        [HttpPost]
        [FwControllerMethod(Id:"DOG6Gm2pVrkF", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OrderContactLogic>> NewAsync([FromBody]OrderContactLogic l)
        {
            return await DoNewAsync<OrderContactLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //justin hoffman 12/16/2019 #1471
        // this is a special Put command to support a behavior of this API that used to exist before we split Post in to Post/Put
        // user can Put an object here with a blank ID and the API will treat it as a Post for New.
        // PUT api/v1/ordercontact
        [HttpPut]
        [FwControllerMethod(Id: "5Jt2GEqYyncCP", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrderContactLogic>> NewAsync2([FromRoute] string id, [FromBody]OrderContactLogic l)
        {
            return await DoNewAsync<OrderContactLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/ordercontact/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "9i8zqc18s1gxa", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrderContactLogic>> EditAsync([FromRoute] string id, [FromBody]OrderContactLogic l)
        {
            return await DoEditAsync<OrderContactLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordercontact/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"mJJETd9WoD4a", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderContactLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordercontact/validatecontact/browse
        [HttpPost("validatecontact/browse")]
        [FwControllerMethod(Id: "zYfu0HD7MUNn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContactBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContactLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordercontact/validatecontacttitle/browse
        [HttpPost("validatecontacttitle/browse")]
        [FwControllerMethod(Id: "vn5DGeGrnD3T", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateContactTitleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContactTitleLogic>(browseRequest);
        }
    }
}
