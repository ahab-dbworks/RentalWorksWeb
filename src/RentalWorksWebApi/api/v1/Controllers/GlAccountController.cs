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
    public class GlAccountController : RwDataController
    {
        public GlAccountController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        protected override FwJsonDataTable doBrowse(BrowseRequestDto request)
        {
            GlAccountLogic l = new GlAccountLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            return l.Browse(request);
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
                GlAccountLogic customerStatus = new GlAccountLogic();
                customerStatus.SetDbConfig(_appConfig.DatabaseSettings);
                if (customerStatus.Load<GlAccountLogic>(ids))
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
        // POST api/v1/glaccount
        [HttpPost]
        public GlAccountLogic Post([FromBody]GlAccountLogic l)
        {
            l.SetDbConfig(_appConfig.DatabaseSettings);
            l.Save();
            l.Load<GlAccountLogic>();
            return l;
        }
        //------------------------------------------------------------------------------------
        protected override void doDelete(string[] ids)
        {
            GlAccountLogic l = new GlAccountLogic();
            l.SetDbConfig(_appConfig.DatabaseSettings);
            l.SetPrimaryKeys(ids);
            l.Delete();
        }
        //------------------------------------------------------------------------------------
    }
}