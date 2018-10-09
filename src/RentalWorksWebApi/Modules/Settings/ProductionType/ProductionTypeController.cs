using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;

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
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ProductionTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/productiontype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProductionTypeLogic>(pageno, pagesize, sort, typeof(ProductionTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/productiontype/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProductionTypeLogic>(id, typeof(ProductionTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/productiontype
        [HttpPost]
        public async Task<ActionResult<ProductionTypeLogic>> PostAsync([FromBody]ProductionTypeLogic l)
        {
            return await DoPostAsync<ProductionTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/productiontype/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ProductionTypeLogic));
        }
        //------------------------------------------------------------------------------------
    }
}