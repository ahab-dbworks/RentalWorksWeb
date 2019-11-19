using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderTypeContactTitle
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"HzNQkWcZ8vEC")]
    public class OrderTypeContactTitleController : AppDataController
    {
        public OrderTypeContactTitleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderTypeContactTitleLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypecontacttitle/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"onGTTWgPn1s6", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"j8wo9i4qoUcw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypecontacttitle 
        [HttpGet]
        [FwControllerMethod(Id:"Jqurgl51UXmw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OrderTypeContactTitleLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeContactTitleLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypecontacttitle/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"J1a6akO7xfdI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<OrderTypeContactTitleLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeContactTitleLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypecontacttitle 
        [HttpPost]
        [FwControllerMethod(Id:"HNDpfC8rLGbL", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OrderTypeContactTitleLogic>> NewAsync([FromBody]OrderTypeContactTitleLogic l)
        {
            return await DoNewAsync<OrderTypeContactTitleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/ordertypecontacttitle/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "jXR2xKYeDXb2r", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrderTypeContactTitleLogic>> EditAsync([FromRoute] string id, [FromBody]OrderTypeContactTitleLogic l)
        {
            return await DoEditAsync<OrderTypeContactTitleLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertypecontacttitle/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"bioOTk84ufl2", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderTypeContactTitleLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
