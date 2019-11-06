using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using FwStandard.AppManager;
using System;

namespace WebApi.Modules.Home.OrderDates
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "AU0Sh7yStlPFU")]
    public class OrderDatesController : AppDataController
    {
        public OrderDatesController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderDatesLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderdates/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "aKeanAcrHBMex")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderdates/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "w6iY0joBv0qCa")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------        
        //// POST api/v1/orderdates/apply
        //[HttpPost("apply")]
        //[FwControllerMethod(Id: "YrL7I5AgzKGdI")]
        //public async Task<ActionResult<ApplyOrderDatesAndTimesResponse>> ApplyOrderDatesAndTimes([FromBody] ApplyOrderDatesAndTimesRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        //#jhtodo - this breaks the Audit History function.  Need to to through the normal QuoteController and OrderController for this.

        //        ApplyOrderDatesAndTimesResponse response = new ApplyOrderDatesAndTimesResponse();
        //        if (string.IsNullOrEmpty(request.OrderId))
        //        {
        //            response.success = false;
        //            response.msg = "OrderId is required.";
        //        }
        //        else
        //        {
        //            response = await OrderDatesFunc.ApplyOrderDatesAndTimes(AppConfig, UserSession, request);
        //        }

        //        return new OkObjectResult(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetApiExceptionResult(ex);
        //    }
        //}
        //------------------------------------------------------------------------------------        
    }
}
