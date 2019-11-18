using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.RepairCost
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"THGHEcObwRTDc")]
    public class RepairCostController : AppDataController
    {
        public RepairCostController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RepairCostLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repaircost/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"bvJN02i8BAtBW")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"j0WCeNrd9125s")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repaircost 
        [HttpGet]
        [FwControllerMethod(Id:"QCkZfb1A9s5ZD")]
        public async Task<ActionResult<IEnumerable<RepairCostLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairCostLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repaircost/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"KMVAo8xUiDJAp")]
        public async Task<ActionResult<RepairCostLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairCostLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repaircost 
        [HttpPost]
        [FwControllerMethod(Id:"dHGscb5NY2Qqw")]
        public async Task<ActionResult<RepairCostLogic>> PostAsync([FromBody]RepairCostLogic l)
        {
            return await DoPostAsync<RepairCostLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/repaircost/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"nOCnLGjFNSakj")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<RepairCostLogic>(id);
        }
        //------------------------------------------------------------------------------------ 

        public class RepairCostExtended
        {
            public Decimal? Quantity;
            public Decimal? Rate;
            public Decimal? DiscountAmount;
            public Decimal? Extended;
            public Decimal? ExtendedUnrounded;
        }

        // GET api/v1/repaircost/calculateextended
        [HttpGet("calculateextended")]
        [FwControllerMethod(Id:"KN0VmwWfYd4eE")]
        public IActionResult CalculateExtended(Decimal? Quantity, Decimal? Rate, Decimal? DiscountAmount)
        {
            try
            {
                RepairCostExtended extended = new RepairCostExtended();
                extended.Quantity = Quantity;
                extended.Rate = Rate;
                extended.DiscountAmount = DiscountAmount;

                //calculate extended here
                extended.ExtendedUnrounded = ((extended.Quantity * extended.Rate) - extended.DiscountAmount);
                extended.Extended = Math.Round(extended.ExtendedUnrounded.Value, 2);


                return new OkObjectResult(extended);
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
 


    }
}
