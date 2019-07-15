using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.DiscountItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"UMKuETy6vOLA")]
    public class DiscountItemController : AppDataController
    {
        public DiscountItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DiscountItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/discountitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"Y3jAcjLkbnIv")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"7VW0H7awipPK")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discountitem 
        [HttpGet]
        [FwControllerMethod(Id:"fsU23r6XoB39")]
        public async Task<ActionResult<IEnumerable<DiscountItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DiscountItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discountitem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"trLqZAVvntAU")]
        public async Task<ActionResult<DiscountItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DiscountItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/discountitem 
        [HttpPost]
        [FwControllerMethod(Id:"pP0RhTPKpzHq")]
        public async Task<ActionResult<DiscountItemLogic>> PostAsync([FromBody]DiscountItemLogic l)
        {
            return await DoPostAsync<DiscountItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/discountitem/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"SPkaYZTVB6Tl")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DiscountItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
