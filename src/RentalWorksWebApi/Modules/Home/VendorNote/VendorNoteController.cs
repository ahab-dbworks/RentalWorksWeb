using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.VendorNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"zuywROD73X60O")]
    public class VendorNoteController : AppDataController
    {
        public VendorNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorNoteLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendornote/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"5G4H0Shzdcg4h")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"Ec9G67UkJrEoX")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendornote
        [HttpGet]
        [FwControllerMethod(Id:"3OeQA322529eH")]
        public async Task<ActionResult<IEnumerable<VendorNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorNoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendornote/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"gd7D9PexoM10P")]
        public async Task<ActionResult<VendorNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendornote
        [HttpPost]
        [FwControllerMethod(Id:"GcEaN2JYQR7Qp")]
        public async Task<ActionResult<VendorNoteLogic>> PostAsync([FromBody]VendorNoteLogic l)
        {
            return await DoPostAsync<VendorNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendornote/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"B3G7du7OWsv9B")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
