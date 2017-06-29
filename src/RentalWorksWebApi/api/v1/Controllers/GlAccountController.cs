﻿using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using RentalWorksWebApi;
using System.Collections.Generic;

namespace RentalWorksCoreApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class GlAccountController : RwController
    {
        public GlAccountController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/glaccount/browse
        [HttpPost("browse")]
        public FwJsonDataTable Browse([FromBody]BrowseRequestDto request)
        {
            GlAccountLogic l = new GlAccountLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            FwJsonDataTable dt = l.Browse(request);
            return dt;
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/glaccount
        [HttpGet]
        public IEnumerable<GlAccountLogic> Get(int pageno, int pagesize)
        {
            BrowseRequestDto request = new BrowseRequestDto();
            request.pageno = pageno;
            request.pagesize = pagesize;
            GlAccountLogic l = new GlAccountLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            IEnumerable<GlAccountLogic> records = l.Select<GlAccountLogic>(request);
            return records;
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/glaccount/A0000001
        [HttpGet("{id}")]
        //public IEnumerable<GlAccountLogic> Get(string id)
        //{
        //    string[] ids = id.Split('~');
        //    GlAccountLogic l = new GlAccountLogic();
        //    l.SetDbConfig(_appConfig.DatabaseSettings);
        //    l.Load<GlAccountLogic>(ids);
        //    List<GlAccountLogic> records = new List<GlAccountLogic>();
        //    records.Add(l);
        //    return records;
        //}
        public GlAccountLogic Get(string id)
        {
            string[] ids = id.Split('~');
            GlAccountLogic l = new GlAccountLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            l.Load<GlAccountLogic>(ids);
            return l;
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/glaccount
        [HttpPost]
        public GlAccountLogic Post([FromBody]GlAccountLogic l)
        {
            l.SetDbConfig(_appConfig.DatabaseSettings);
            l.Save();
            return l;
        }
        //------------------------------------------------------------------------------------
        //// DELETE api/v1/glaccount/A0000001
        //[HttpDelete("{id}")]
        //public void Delete(string id)
        //{
        //    GlAccountLogic l = new GlAccountLogic();
        //    l.SetDbConfig(_appConfig.DatabaseSettings);
        //    l.GlAccountId = id;
        //    l.Delete();
        //}
        // DELETE api/v1/glaccount
        [HttpDelete]
        public void Delete([FromBody]GlAccountLogic l)
        {
            l.SetDbConfig(_appConfig.DatabaseSettings);
            l.Delete();
        }
        //------------------------------------------------------------------------------------
    }
}