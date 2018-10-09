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
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class TestController : AppDataController
    {
        public TestController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/test 
        //[HttpGet]
        //public async Task<ActionResult<FwJsonDataTable>> GetAsync()
        //{
        //    FwJsonDataTable dtDetails;
        //    try
        //    {

        //        using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
        //        {
        //            FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.QueryTimeout);
        //            qry.Add("select top 20 rowtype='detail', location, customer, deal, totaldeposit, remaining=(totaldeposit - totalapplied - totalrefunded)");
        //            qry.Add("from creditsonaccountview");
        //            qry.Add("where (totaldeposit - totalapplied - totalrefunded) > 0");
        //            qry.Add("order by location, customer, deal");

        //            qry.AddColumn("rowtype");
        //            qry.AddColumn("location");
        //            qry.AddColumn("customer");
        //            qry.AddColumn("deal");
        //            qry.AddColumn("totaldeposit", false, FwDataTypes.Decimal);
        //            qry.AddColumn("remaining", false, FwDataTypes.Decimal);

        //            dtDetails = await qry.QueryToFwJsonTableAsync(true);
        //        }
        //        dtDetails.InsertSubTotalRows("customer", "rowtype", new string[] { "totaldeposit", "remaining" });
        //        dtDetails.InsertTotalRow("rowtype", "detail", "grandtotal", new string[] { "totaldeposit", "remaining" });
        //        return new OkObjectResult(dtDetails);

        //    }
        //    catch (Exception ex)
        //    {
        //        FwApiException jsonException = new FwApiException();
        //        jsonException.StatusCode = StatusCodes.Status500InternalServerError;
        //        jsonException.Message = ex.Message;
        //        jsonException.StackTrace = ex.StackTrace;
        //        return StatusCode(jsonException.StatusCode, jsonException);
        //    }


        //}
        ////------------------------------------------------------------------------------------ 

    }
}