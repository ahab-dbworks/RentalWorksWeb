using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.RentalInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class RentalInventoryController : AppDataController
    {
        public RentalInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RentalInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rentalinventory 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RentalInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rentalinventory/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RentalInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rentalinventory 
        [HttpPost]
        public async Task<ActionResult<RentalInventoryLogic>> PostAsync([FromBody]RentalInventoryLogic l)
        {
            return await DoPostAsync<RentalInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/rentalinventory/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
