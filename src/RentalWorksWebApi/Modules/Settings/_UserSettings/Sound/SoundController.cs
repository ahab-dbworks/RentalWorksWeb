using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.UserSettings.Sound
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"1SCjkmxKUSbaQ")]
    public class SoundController : AppDataController
    {
        public SoundController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SoundLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sound/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"yuAzDpW1Dx1VI", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sound/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"bV1pLKNlukR06", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/sound 
        [HttpGet]
        [FwControllerMethod(Id:"zlfaq8vkOacSP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<SoundLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SoundLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/sound/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"GVaVy2CVQRWl3", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<SoundLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SoundLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sound 
        [HttpPost]
        [FwControllerMethod(Id:"iH2amJ0NUqwCN", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<SoundLogic>> NewAsync([FromBody]SoundLogic l)
        {
            return await DoNewAsync<SoundLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/sound/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "r6cIr7X2flyHC", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<SoundLogic>> EditAsync([FromRoute] string id, [FromBody]SoundLogic l)
        {
            return await DoEditAsync<SoundLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/sound/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"J7o8XvkLRJcW3", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SoundLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
