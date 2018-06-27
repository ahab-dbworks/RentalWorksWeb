using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.ProductionType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class ProductionTypeController : AppDataController
    {
        public ProductionTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProductionTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/productiontype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> Browse([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProductionTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/productiontype
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProductionTypeLogic>(pageno, pagesize, sort, typeof(ProductionTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/productiontype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProductionTypeLogic>(id, typeof(ProductionTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/productiontype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ProductionTypeLogic l)
        {
            return await DoPostAsync<ProductionTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/productiontype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProductionTypeLogic));
        }
        //------------------------------------------------------------------------------------
    }
}