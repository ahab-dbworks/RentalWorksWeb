using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Logic;
using System;

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
        public async Task<ActionResult<ItemLogic>> GetOneByBarCodeAsync(string barCode)
        {
            string itemId = AppFunc.GetStringDataAsync(AppConfig, "rentalitem", "barcode", barCode, "rentalitemid").Result;
            if (string.IsNullOrEmpty(itemId))
            {
                SystemException ex = new SystemException($"Invalid Bar Code {barCode}");
                return GetApiExceptionResult(ex);
            }
            else
            {
                return await DoGetAsync<ItemLogic>(itemId);
            }
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
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
