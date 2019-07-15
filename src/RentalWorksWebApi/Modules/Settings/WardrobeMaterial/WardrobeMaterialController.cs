using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WardrobeMaterial
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"rTEb0yyIofBBU")]
    public class WardrobeMaterialController : AppDataController
    {
        public WardrobeMaterialController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeMaterialLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/wardrobematerial/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"Ik2hgwLmoAmsO")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"8On4TWlYPbXd3")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/wardrobematerial 
        [HttpGet]
        [FwControllerMethod(Id:"Nov12tSjfSVtS")]
        public async Task<ActionResult<IEnumerable<WardrobeMaterialLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeMaterialLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/wardrobematerial/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"RGDwdVGNYQJZl")]
        public async Task<ActionResult<WardrobeMaterialLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeMaterialLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/wardrobematerial 
        [HttpPost]
        [FwControllerMethod(Id:"fyqCzA6PcCwRH")]
        public async Task<ActionResult<WardrobeMaterialLogic>> PostAsync([FromBody]WardrobeMaterialLogic l)
        {
            return await DoPostAsync<WardrobeMaterialLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/wardrobematerial/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"gx2ZSzKDPsWyr")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WardrobeMaterialLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
