using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.AttributeValue
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"2uvN8jERScu")]
    public class AttributeValueController : AppDataController
    {
        public AttributeValueController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AttributeValueLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/attributevalue/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"JxiDU2amISb")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"zXuEPQkar33")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/attributevalue
        [HttpGet]
        [FwControllerMethod(Id:"fWhplkXg9Ik")]
        public async Task<ActionResult<IEnumerable<AttributeValueLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AttributeValueLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/attributevalue/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"DbbR4GEMOxh")]
        public async Task<ActionResult<AttributeValueLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AttributeValueLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/attributevalue
        [HttpPost]
        [FwControllerMethod(Id:"IPe9o2Bz9aq")]
        public async Task<ActionResult<AttributeValueLogic>> PostAsync([FromBody]AttributeValueLogic l)
        {
            return await DoPostAsync<AttributeValueLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/attributevalue/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"LITeDKKEZo2")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<AttributeValueLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
