using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebLibrary;
using WebApi.Modules.Settings.InventoryStatus;

namespace WebApi.Modules.Home.Item
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "kSugPLvkuNsH")]
    public class ItemController : AppDataController
    {
        public ItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/item/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "8EjRJqgdgpgQ")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/item/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "g02Y2myXv8pqI")]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            InventoryStatusLogic s = new InventoryStatusLogic();
            s.SetDependencies(AppConfig, UserSession);
            GetManyResponse<InventoryStatusLogic> r = await s.GetManyAsync<InventoryStatusLogic>(new GetManyRequest());

            Dictionary<string, string> legend = new Dictionary<string, string>();
            foreach (InventoryStatusLogic l in r.Items)
            {
               legend.Add(FwConvert.ToTitleCase(l.InventoryStatus.ToLower()), l.Color);
            }
            legend.Add("QC Required", RwGlobals.QC_REQUIRED_COLOR);
            legend.Add("Suspended", RwGlobals.SUSPEND_COLOR);
            //await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "Y5fE4iVc3UhZ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/item 
        [HttpGet]
        [FwControllerMethod(Id: "EsvBT0cfnwU2")]
        public async Task<ActionResult<IEnumerable<ItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/item/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "zjCQVTktDrdU")]
        public async Task<ActionResult<ItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/item/bybarcode 
        [HttpGet("bybarcode")]
        [FwControllerMethod(Id: "HtxHyTMbNpqOM")]
        public async Task<ActionResult<ItemByBarCodeResponse>> GetOneByBarCodeAsync(string barCode)
        {
            return await ItemFunc.GetByBarCode(AppConfig, UserSession, barCode);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/item 
        [HttpPost]
        [FwControllerMethod(Id: "vf0mEqWKxcv3")]
        public async Task<ActionResult<ItemLogic>> PostAsync([FromBody]ItemLogic l)
        {
            return await DoPostAsync<ItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/item/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"NfaaIo4GI")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <ItemLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
