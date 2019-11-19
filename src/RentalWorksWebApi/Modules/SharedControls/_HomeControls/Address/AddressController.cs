using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.Address
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"YuRTivbSA")]
    public class AddressController : AppDataController
    {
        public AddressController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AddressLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/address/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"9vLnfbW3Jm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"mHMGwlWX2u", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/address 
        [HttpGet]
        [FwControllerMethod(Id:"aBuSCUT9FC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<AddressLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AddressLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/address/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"ytwsw1hYaR", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<AddressLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AddressLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/address 
        [HttpPost]
        [FwControllerMethod(Id:"OaE1nk50q6", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<AddressLogic>> NewAsync([FromBody]AddressLogic l)
        {
            return await DoNewAsync<AddressLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/address/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "0iNdbM0Yo5JBX", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<AddressLogic>> EditAsync([FromRoute] string id, [FromBody]AddressLogic l)
        {
            return await DoEditAsync<AddressLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/address/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"cw3ukmZB2d", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<AddressLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
