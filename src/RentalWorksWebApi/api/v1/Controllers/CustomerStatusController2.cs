﻿using FwStandard.Models;
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
    public class CustomerStatusController2 : RwDataController
    {
        public CustomerStatusController2(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        protected override FwJsonDataTable doBrowse(BrowseRequestDto request) {
            CustomerStatusLogic l = new CustomerStatusLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            return l.Browse(request);
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
                if (customerStatus.Load<CustomerStatusLogic>(ids))
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
        // POST api/v1/customerstatus
        [HttpPost]
        public CustomerStatusLogic Post([FromBody]CustomerStatusLogic customerStatus)
        {
            customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
            customerStatus.Save();
            customerStatus.Load<CustomerStatusLogic>();
            return customerStatus;
        }
        //------------------------------------------------------------------------------------
        protected override void doDelete(string[] ids) {
            CustomerStatusLogic l = new CustomerStatusLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            l.SetPrimaryKeys(ids);
            l.Delete();
        }
        //------------------------------------------------------------------------------------
    }
}