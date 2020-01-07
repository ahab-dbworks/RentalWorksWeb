using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;

namespace WebApi.Modules.Settings.OrderSettings.OrderLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"ezKyPjJBJKjQ")]
    public class OrderLocationController : AppDataController
    {
        public OrderLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderLocationLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderlocation/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"5A2jvHPZngbB", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Sdv325O6rjMG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderlocation 
        [HttpGet]
        [FwControllerMethod(Id:"mB6x1dqO5SNS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OrderLocationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderLocationLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderlocation/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"MmWGFd9D0l8l", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<OrderLocationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderlocation 
        [HttpPost]
        [FwControllerMethod(Id:"H1jV1CQMVBpq", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OrderLocationLogic>> NewAsync([FromBody]OrderLocationLogic l)
        {
            return await DoNewAsync<OrderLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/orderlocation/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "IIBFauCOE1rY9", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrderLocationLogic>> EditAsync([FromRoute] string id, [FromBody]OrderLocationLogic l)
        {
            return await DoEditAsync<OrderLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/orderlocation/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"mfOKZ6zzsEOu", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/orderlocation/validatelocation/browse
        [HttpPost("validatelocation/browse")]
        [FwControllerMethod(Id: "0sIfwURRAUEL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
    }
}
