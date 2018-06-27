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
    public class RepairPartController : AppDataController
    {
        public RepairPartController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RepairPartLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairpart/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RepairPartLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repairpart 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairPartLogic>(pageno, pagesize, sort, typeof(RepairPartLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repairpart/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairPartLogic>(id, typeof(RepairPartLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repairpart 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RepairPartLogic l)
        {
            return await DoPostAsync<RepairPartLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/repairpart/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RepairPartLogic));
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
