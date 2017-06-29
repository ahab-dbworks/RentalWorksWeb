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
    public class CustomerStatusController : RwController
    {
        public CustomerStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        [HttpPost("browse")]
        public FwJsonDataTable Browse([FromBody]BrowseRequestDto request)
        {
            CustomerStatusLogic customerStatus = new CustomerStatusLogic();
            customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
            FwJsonDataTable dt = customerStatus.Browse(request);
            return dt;
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus
        [HttpGet]
        public IEnumerable<CustomerStatusLogic> Get(int pageno, int pagesize)
        {
            BrowseRequestDto request = new BrowseRequestDto();
            request.pageno = pageno;
            request.pagesize = pagesize;
            CustomerStatusLogic customerStatus = new CustomerStatusLogic();
            customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
            IEnumerable<CustomerStatusLogic> customerStatusRecords = customerStatus.Select<CustomerStatusLogic>(request);
            return customerStatusRecords;
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{id}")]
        //public IEnumerable<CustomerStatusLogic> Get(string id)
        //{
        //    string[] ids = id.Split('~');
        //    CustomerStatusLogic customerStatus = new CustomerStatusLogic();
        //    customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
        //    customerStatus.Load<CustomerStatusLogic>(ids);
        //    List<CustomerStatusLogic> records = new List<CustomerStatusLogic>();
        //    records.Add(customerStatus);
        //    return records;
        //}
        public CustomerStatusLogic Get(string id)
        {
            string[] ids = id.Split('~');
            CustomerStatusLogic customerStatus = new CustomerStatusLogic();
            customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
            customerStatus.Load<CustomerStatusLogic>(ids);
            return customerStatus;
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost]
        public CustomerStatusLogic Post([FromBody]CustomerStatusLogic customerStatus)
        {
            customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
            customerStatus.Save();
            return customerStatus;
        }
        //------------------------------------------------------------------------------------
        //// DELETE api/v1/customerstatus/customerstatusid
        //[HttpDelete("{id}")]
        //public void Delete(string id)
        //{
        //    CustomerStatusLogic customerStatus = new CustomerStatusLogic();
        //    customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
        //    customerStatus.CustomerStatusId = id;
        //    customerStatus.Delete();
        //}
        // DELETE api/v1/customerstatus
        [HttpDelete]
        public void Delete([FromBody]CustomerStatusLogic customerStatus)
        {
            customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
            customerStatus.Delete();
        }
        //------------------------------------------------------------------------------------
    }
}