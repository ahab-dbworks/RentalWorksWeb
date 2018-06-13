using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Administrator.Test
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    public class DateFunctionsController : AppDataController
    {
        public DateFunctionsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/datefunctions/adddays
        [HttpGet("adddays")]
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
    }
}