using FwStandard.AppManager;
﻿using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Settings.VendorSettings.VendorCatalog
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"086ok4V8ztfCu")]
    public class VendorCatalogController : AppDataController
    {
        public VendorCatalogController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorCatalogLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorcatalog/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"27KAqkcCTYPbF")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"27KAqkcCTYPbF")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorcatalog
        [HttpGet]
        [FwControllerMethod(Id:"LWSlHFex0VIJq")]
        public async Task<ActionResult<IEnumerable<VendorCatalogLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorCatalogLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorcatalog/A0000001
        [HttpGet("{id}")]
        [Produces(typeof(VendorCatalogLogic))]
        [SwaggerResponse(200, Type = typeof(VendorCatalogLogic))]
        [FwControllerMethod(Id:"tE4iYy3o0UlSS")]
        public async Task<ActionResult<VendorCatalogLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorCatalogLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorcatalog
        [HttpPost]
        [FwControllerMethod(Id:"EHZa6o3TPHDag")]
        public async Task<ActionResult<VendorCatalogLogic>> PostAsync([FromBody]VendorCatalogLogic l)
        {
            return await DoPostAsync<VendorCatalogLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendorcatalog/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"VDOfrJqSNX6kY")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorCatalogLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
