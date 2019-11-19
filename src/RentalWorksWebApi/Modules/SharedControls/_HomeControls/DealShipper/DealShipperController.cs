using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.DealShipper
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"5cMD0y0jSUgz")]
    public class DealShipperController : AppDataController
    {
        public DealShipperController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealShipperLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealshipper/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"WiMZfOfdSmC0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"PqYoG6uoX6gL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/dealshipper 
        [HttpGet]
        [FwControllerMethod(Id:"bRGhK8dDTTwT", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DealShipperLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealShipperLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/dealshipper/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"UbJ73I5UrvIL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DealShipperLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealShipperLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/dealshipper 
        [HttpPost]
        [FwControllerMethod(Id:"qWqzlHXJl2nn", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DealShipperLogic>> NewAsync([FromBody]DealShipperLogic l)
        {
            return await DoNewAsync<DealShipperLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/dealshipper/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "kYwYTWj76jit5", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DealShipperLogic>> EditAsync([FromRoute] string id, [FromBody]DealShipperLogic l)
        {
            return await DoEditAsync<DealShipperLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/dealshipper/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"NcyqlUiuyjcY", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DealShipperLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
