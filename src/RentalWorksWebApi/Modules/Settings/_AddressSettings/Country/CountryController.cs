using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.Country
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"FV8c2ibthqUF")]
    public class CountryController : AppDataController
    {
        public CountryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CountryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/Country/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"bsEsUDi9WpvA")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"lR88sEu29oTZ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/Country
        [HttpGet]
        [FwControllerMethod(Id:"Uez8sugmBG3i")]
        public async Task<ActionResult<IEnumerable<CountryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CountryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/Country/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"G7CCMgm7lSbT")]
        public async Task<ActionResult<CountryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CountryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/Country
        [HttpPost]
        [FwControllerMethod(Id:"T6ij8RURX6JI")]
        public async Task<ActionResult<CountryLogic>> PostAsync([FromBody]CountryLogic l)
        {
            return await DoPostAsync<CountryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/Country/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"643Hwtc5kewd")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CountryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
