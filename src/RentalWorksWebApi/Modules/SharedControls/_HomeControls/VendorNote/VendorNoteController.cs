using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.HomeControls.VendorNote
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
        [FwControllerMethod(Id:"5G4H0Shzdcg4h", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Ec9G67UkJrEoX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendornote
        [HttpGet]
        [FwControllerMethod(Id:"3OeQA322529eH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VendorNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorNoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendornote/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"gd7D9PexoM10P", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<VendorNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendornote
        [HttpPost]
        [FwControllerMethod(Id:"GcEaN2JYQR7Qp", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VendorNoteLogic>> NewAsync([FromBody]VendorNoteLogic l)
        {
            return await DoNewAsync<VendorNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/vendornot/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "O4wM8Q8P7OztZ", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VendorNoteLogic>> EditAsync([FromRoute] string id, [FromBody]VendorNoteLogic l)
        {
            return await DoEditAsync<VendorNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendornote/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"B3G7du7OWsv9B", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
