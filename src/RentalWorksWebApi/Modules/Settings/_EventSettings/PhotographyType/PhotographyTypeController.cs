using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.EventSettings.PhotographyType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"bFH6YcKYCqye")]
    public class PhotographyTypeController : AppDataController
    {
        public PhotographyTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PhotographyTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/photographytype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"s5ZY0H6q6U8P")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ESWIafoNVZsF")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/photographytype
        [HttpGet]
        [FwControllerMethod(Id:"Zxbgqrn4Li")]
        public async Task<ActionResult<IEnumerable<PhotographyTypeLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<PhotographyTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/photographytype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"ldt0wET9A0")]
        public async Task<ActionResult<PhotographyTypeLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<PhotographyTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/photographytype
        [HttpPost]
        [FwControllerMethod(Id:"WWt1RSHfkE1e")]
        public async Task<ActionResult<PhotographyTypeLogic>> PostAsync([FromBody]PhotographyTypeLogic l)
        {
            return await DoPostAsync<PhotographyTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/photographytype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"LiGKi4KguDgz")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<PhotographyTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
