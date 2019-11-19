using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.HomeControls.CompanyTaxOption
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"B9CzDEmYe1Zf")]
    public class CompanyTaxOptionController : AppDataController
    {
        public CompanyTaxOptionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CompanyTaxOptionLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/companytaxoption/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"zFk5XqrpU9wL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"CHmn2Nz0QcLp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/companytaxoption
        [HttpGet]
        [FwControllerMethod(Id:"7FWfsyD36AIJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CompanyTaxOptionLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<CompanyTaxOptionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/companytaxoption/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"ruPmZgIcLg4a", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<CompanyTaxOptionLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<CompanyTaxOptionLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/companytaxoption
        [HttpPost]
        [FwControllerMethod(Id:"IKitIaPutFUf", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CompanyTaxOptionLogic>> NewAsync([FromBody]CompanyTaxOptionLogic l)
        {
            // NOTE: Newrt is currently not supported becase there is no surrogate key in this module
            return await DoPostAsync<CompanyTaxOptionLogic>(l);
        }
        // PUT api/v1/companytaxoptio/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "nPUvwZ5i4IRJq", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CompanyTaxOptionLogic>> EditAsync([FromRoute] string id, [FromBody]CompanyTaxOptionLogic l)
        {
            // NOTE: Editrt is currently not supported becase there is no surrogate key in this module
            return await DoPostAsync<CompanyTaxOptionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/companytaxoption/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"qYpDSTRcigYE", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<CompanyTaxOptionLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
