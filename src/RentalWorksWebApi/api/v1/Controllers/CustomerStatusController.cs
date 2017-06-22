using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksLogic.Settings;
using RentalWorksWebApi;
using System.Collections.Generic;

namespace RentalWorksCoreApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class CustomerStatusController : Controller
    {
        private readonly ApplicationConfig _appConfig;
        public CustomerStatusController(IOptions<ApplicationConfig> appConfig)
        {
            _appConfig = appConfig.Value;
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        [HttpPost("browse")]
        public FwJsonDataTable Browse([FromBody]BrowseRequestDto request)
        {
            CustomerStatusLogic csl = new CustomerStatusLogic(_appConfig.DatabaseSettings);
            FwJsonDataTable dt = csl.Browse(request);
            return dt;
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus
        [HttpGet]
        public List<CustomerStatusLogic> Get(int pageno, int pagesize)
        {
            BrowseRequestDto request = new BrowseRequestDto();
            request.pageno = pageno;
            request.pagesize = pagesize;
            CustomerStatusLogic cs = new CustomerStatusLogic(_appConfig.DatabaseSettings);
            List<CustomerStatusLogic> customerStatusRecords = cs.Select(request);
            return customerStatusRecords;
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{custstatusid}")]
        public List<CustomerStatusLogic> Get(string custstatusid)
        {
            CustomerStatusLogic csl = new CustomerStatusLogic(_appConfig.DatabaseSettings);
            List<CustomerStatusLogic> records = csl.Get(custstatusid);
            return records;
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost]
        public CustomerStatusLogic Post([FromBody]CustomerStatusLogic model)
        {
            model.Save(_appConfig.DatabaseSettings);
            return model;
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customerstatus/customerstatusid
        [HttpDelete("{custstatusid}")]
        public void Delete(string custstatusid)
        {
            CustomerStatusLogic csl = new CustomerStatusLogic(_appConfig.DatabaseSettings);
            csl.custstatusid = custstatusid;
            csl.Delete();
        }
        //------------------------------------------------------------------------------------
    }
}