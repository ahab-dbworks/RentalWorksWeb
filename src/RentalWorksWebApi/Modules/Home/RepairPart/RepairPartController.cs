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

namespace WebApi.Modules.Home.RepairPart
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"k1Qn9brpxHGhp")]
    public class RepairPartController : AppDataController
    {
        public RepairPartController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RepairPartLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairpart/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"oojqUfqgxbIEl")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"NuN2ATQ6bLgar")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repairpart 
        [HttpGet]
        [FwControllerMethod(Id:"MLhmmIphixzmW")]
        public async Task<ActionResult<IEnumerable<RepairPartLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairPartLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repairpart/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"4gGjoYZZJBiCx")]
        public async Task<ActionResult<RepairPartLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairPartLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairpart 
        [HttpPost]
        [FwControllerMethod(Id:"F2U1KDW6JXcsR")]
        public async Task<ActionResult<RepairPartLogic>> PostAsync([FromBody]RepairPartLogic l)
        {
            return await DoPostAsync<RepairPartLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/repairpart/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"SmouYiNneRwq2")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 

        public class RepairPartExtended
        {
            public Decimal? Quantity;
            public Decimal? Rate;
            public Decimal? DiscountAmount;
            public Decimal? Extended;
            public Decimal? ExtendedUnrounded;
        }

        // GET api/v1/repairpart/calculateextended
        [HttpGet("calculateextended")]
        [FwControllerMethod(Id:"IGkRmq7toKAkI")]
        public IActionResult CalculateExtended(Decimal? Quantity, Decimal? Rate, Decimal? DiscountAmount)
        {
            try
            {
                RepairPartExtended extended = new RepairPartExtended();
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
