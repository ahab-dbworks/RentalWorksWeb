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
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Settings.ShipViaSettings.ShipVia;

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
        [FwControllerMethod(Id:"0dDbYCxFuymO", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"27KAqkcCTYPbF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorcatalog
        [HttpGet]
        [FwControllerMethod(Id:"LWSlHFex0VIJq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VendorCatalogLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorCatalogLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorcatalog/A0000001
        [HttpGet("{id}")]
        [Produces(typeof(VendorCatalogLogic))]
        [SwaggerResponse(200, Type = typeof(VendorCatalogLogic))]
        [FwControllerMethod(Id:"tE4iYy3o0UlSS", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<VendorCatalogLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorCatalogLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorcatalog
        [HttpPost]
        [FwControllerMethod(Id:"EHZa6o3TPHDag", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VendorCatalogLogic>> NewAsync([FromBody]VendorCatalogLogic l)
        {
            return await DoNewAsync<VendorCatalogLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/vendorcatalo/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "XHPWfBPGmKKeQ", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VendorCatalogLogic>> EditAsync([FromRoute] string id, [FromBody]VendorCatalogLogic l)
        {
            return await DoEditAsync<VendorCatalogLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendorcatalog/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"VDOfrJqSNX6kY", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorCatalogLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorcatalog/validateinventorytype/browse
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "PMrlBtOeazMl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorcatalog/validatevendor/browse
        [HttpPost("validatevendor/browse")]
        [FwControllerMethod(Id: "KQ2A8eoGDKbC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateVendorBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorcatalog/validatecarrier/browse
        [HttpPost("validatecarrier/browse")]
        [FwControllerMethod(Id: "OI6IQcYjN8Sf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCarrierBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorcatalog/validateshipvia/browse
        [HttpPost("validateshipvia/browse")]
        [FwControllerMethod(Id: "mbdUw5DWJhxb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateShipViaBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ShipViaLogic>(browseRequest);
        }
    }
}
