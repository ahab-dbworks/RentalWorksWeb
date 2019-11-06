using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.PurchaseOrderReturnBarCode
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"JkwkAFQ4tL7q0")]
    public class PurchaseOrderReturnBarCodeController : AppDataController
    {
        public PurchaseOrderReturnBarCodeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PurchaseOrderReturnBarCodeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceivebarcode/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"wr19b3MDUa0IB")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/purchaseorderreceivebarcode/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"DPl3TW16tsVTk")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorderreceivebarcode 
        [HttpGet]
        [FwControllerMethod(Id:"Cd7MeD6DIgfCm")]
        public async Task<ActionResult<IEnumerable<PurchaseOrderReturnBarCodeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PurchaseOrderReturnBarCodeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/purchaseorderreceivebarcode/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Td095DqnZkar7")]
        public async Task<ActionResult<PurchaseOrderReturnBarCodeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PurchaseOrderReturnBarCodeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
