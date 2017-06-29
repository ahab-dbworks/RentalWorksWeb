using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using RentalWorksWebApi;
using System.Collections.Generic;

namespace RentalWorksCoreApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class OrderController : RwController
    {
        public OrderController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/browse
        [HttpPost("browse")]
        public FwJsonDataTable Browse([FromBody]BrowseRequestDto request)
        {
            OrderLogic l = new OrderLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            FwJsonDataTable dt = l.Browse(request);
            return dt;
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order
        [HttpGet]
        public IEnumerable<OrderLogic> Get(int pageno, int pagesize)
        {
            BrowseRequestDto request = new BrowseRequestDto();
            request.pageno = pageno;
            request.pagesize = pagesize;
            OrderLogic l = new OrderLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            IEnumerable<OrderLogic> records = l.Select<OrderLogic>(request);
            return records;
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order/A0000001
        [HttpGet("{id}")]
        //public IEnumerable<OrderLogic> Get(string id)
        //{
        //    string[] ids = id.Split('~');
        //    OrderLogic l = new OrderLogic();
        //    l.SetDbConfig(_appConfig.DatabaseSettings);
        //    l.Load<OrderLogic>(ids);
        //    List<OrderLogic> records = new List<OrderLogic>();
        //    records.Add(l);
        //    return records;
        //}
        public OrderLogic Get(string id)
        {
            string[] ids = id.Split('~');
            OrderLogic l = new OrderLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            l.Load<OrderLogic>(ids);
            return l;
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order
        [HttpPost]
        public OrderLogic Post([FromBody]OrderLogic l)
        {
            l.SetDbConfig(_appConfig.DatabaseSettings);
            l.Save();
            return l;
        }
        //------------------------------------------------------------------------------------
        //// DELETE api/v1/order/A0000001
        //[HttpDelete("{id}")]
        //public void Delete(string id)
        //{
        //    OrderLogic l = new OrderLogic();
        //    l.SetDbConfig(_appConfig.DatabaseSettings);
        //    l.OrderId = id;
        //    l.Delete();
        //}
        // DELETE api/v1/order
        [HttpDelete]
        public void Delete([FromBody]OrderLogic l)
        {
            l.SetDbConfig(_appConfig.DatabaseSettings);
            l.Delete();
        }
        //------------------------------------------------------------------------------------
    }
}