using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.MailList
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"vUT6JZ1Owu5n")]
    public class MailListController : AppDataController
    {
        public MailListController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MailListLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/maillist/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"GsXF52pwF3HV")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"JJOdhKFXSm0a")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/maillist
        [HttpGet]
        [FwControllerMethod(Id:"bHR8VGe4sI")]
        public async Task<ActionResult<IEnumerable<MailListLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<MailListLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/maillist/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"t3959oiyDVDE")]
        public async Task<ActionResult<MailListLogic>> GetAsync(string id)
        {
            return await DoGetAsync<MailListLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/maillist
        [HttpPost]
        [FwControllerMethod(Id:"p0e3tDkbdXqk")]
        public async Task<ActionResult<MailListLogic>> PostAsync([FromBody]MailListLogic l)
        {
            return await DoPostAsync<MailListLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/maillist/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"lv3T92YSlIpE")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<MailListLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
