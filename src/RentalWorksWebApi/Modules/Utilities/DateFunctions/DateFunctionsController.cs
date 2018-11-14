using FwStandard.AppManager;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Utilities.DateFunctions
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id:"Fe6C5BAHC7r2")]
    public class DateFunctionsController : AppDataController
    {
        public DateFunctionsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/datefunctions/adddays
        [HttpGet("adddays")]
        [FwControllerMethod(Id:"lhlW1yhlcggq")]
        public IActionResult AddDays(DateTime date, int Days)
        {
            try
            {
                DateTime newDate = date.AddDays(Days);
                return new OkObjectResult(newDate);

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
        // GET api/v1/datefunctions/addmonths 
        [HttpGet("addmonths")]
        [FwControllerMethod(Id:"D22HgC7vhAKU")]
        public IActionResult AddMonths(DateTime date, int Months)
        {
            try
            {
                DateTime newDate = date.AddMonths(Months);
                return new OkObjectResult(newDate);

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
        // GET api/v1/datefunctions/numberofmonths 
        [HttpGet("numberofmonths")]
        [FwControllerMethod(Id:"wNu87lSHEd8F")]
        public IActionResult NumberOfMonths(DateTime fromDate, DateTime toDate)
        {
            try
            {
                int numberOfMonths = 0;

                DateTime theDate = fromDate;
                while (toDate >= theDate)
                {
                    theDate = theDate.AddMonths(1);
                    numberOfMonths++;
                }

                return new OkObjectResult(numberOfMonths);

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
