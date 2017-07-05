using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System;
using System.Collections.Generic;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class OrderController : RwDataController
    {
        public OrderController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
    //------------------------------------------------------------------------------------
    protected override FwJsonDataTable doBrowse(BrowseRequestDto request)
        {
            OrderLogic l = new OrderLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            return l.Browse(request);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order
        [HttpGet]
        public IEnumerable<OrderLogic> Get(int pageno, int pagesize)
        {
            ApplicationLogging.log("performing OrderController.Get");

            BrowseRequestDto request = new BrowseRequestDto();
            request.pageno = pageno;
            request.pagesize = pagesize;
            OrderLogic l = new OrderLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            IEnumerable<OrderLogic> records = l.Select<OrderLogic>(request);
            return records;
        }
        //------------------------------------------------------------------------------------
        public IActionResult Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                OrderLogic customerStatus = new OrderLogic();
                customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
                if (customerStatus.Load<OrderLogic>(ids))
                {
                    return new OkObjectResult(customerStatus);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order
        [HttpPost]
        public OrderLogic Post([FromBody]OrderLogic l)
        {
            l.SetDbConfig(_appConfig.DatabaseSettings);
            l.Save();
            l.Load<OrderLogic>();
            return l;
        }
        //------------------------------------------------------------------------------------
        protected override void doDelete(string[] ids)
        {
            OrderLogic l = new OrderLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            l.SetPrimaryKeys(ids);
            l.Delete();
        }
        //------------------------------------------------------------------------------------
    }
}