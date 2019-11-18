using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Sound
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
        [FwControllerMethod(Id:"yuAzDpW1Dx1VI")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sound/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"bV1pLKNlukR06")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/sound 
        [HttpGet]
        [FwControllerMethod(Id:"zlfaq8vkOacSP")]
        public async Task<ActionResult<IEnumerable<SoundLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SoundLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/sound/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"GVaVy2CVQRWl3")]
        public async Task<ActionResult<SoundLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SoundLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/sound 
        [HttpPost]
        [FwControllerMethod(Id:"iH2amJ0NUqwCN")]
        public async Task<ActionResult<SoundLogic>> PostAsync([FromBody]SoundLogic l)
        {
            return await DoPostAsync<SoundLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/sound/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"J7o8XvkLRJcW3")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SoundLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
