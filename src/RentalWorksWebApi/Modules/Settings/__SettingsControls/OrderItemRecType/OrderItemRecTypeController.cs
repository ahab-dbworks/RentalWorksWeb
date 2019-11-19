using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderItemRecType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"ob5teOPyIgv8")]
    public class OrderItemRecTypeController : AppDataController
    {
        public OrderItemRecTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderItemRecTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderitemrectype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"3hIMxbVAAxpp", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"qMrWu7P28Ahe", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderitemrectype 
        [HttpGet]
        [FwControllerMethod(Id:"93ykcqogCTpB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OrderItemRecTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderItemRecTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderitemrectype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"QhsdVA9x1Bi5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<OrderItemRecTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderItemRecTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
