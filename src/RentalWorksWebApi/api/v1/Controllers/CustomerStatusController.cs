using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System;
using System.Collections.Generic;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class CustomerStatusController : RwController
    {
        public CustomerStatusController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        [HttpPost("browse")]
        //[Authorize(Policy = "User")]
        public IActionResult Browse([FromBody]BrowseRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CustomerStatusLogic customerStatus = new CustomerStatusLogic();
                customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
                FwJsonDataTable dt = customerStatus.Browse(request);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus
        [HttpGet]
        //[Authorize(Policy = "User")]
        public IActionResult Get(int pageno, int pagesize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BrowseRequestDto request = new BrowseRequestDto();
                request.pageno = pageno;
                request.pagesize = pagesize;
                CustomerStatusLogic customerStatus = new CustomerStatusLogic();
                customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
                IEnumerable<CustomerStatusLogic> customerStatusRecords = customerStatus.Select<CustomerStatusLogic>(request);
                return new OkObjectResult(customerStatusRecords);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{id}")]
        //[Authorize(Policy = "User")]
        public IActionResult Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                CustomerStatusLogic customerStatus = new CustomerStatusLogic();
                customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
                customerStatus.Load<CustomerStatusLogic>(ids);
                // need to get rid of the load call above and return a list or something
                List<CustomerStatusLogic> records = new List<CustomerStatusLogic>();
                records.Add(customerStatus);
                if (records.Count == 0)
                {
                    return NotFound();
                }
                return new OkObjectResult(customerStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost]
        //[Authorize(Policy = "User")]
        public IActionResult Post([FromBody]CustomerStatusLogic customerStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
                customerStatus.Save();
                return new OkObjectResult(customerStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customerstatus/customerstatusid
        [HttpDelete("{id}")]
        //[Authorize(Policy = "User")]
        public IActionResult Delete([FromBody]CustomerStatusLogic customerStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
                customerStatus.Delete();
                return new OkResult();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        //------------------------------------------------------------------------------------
    }
}