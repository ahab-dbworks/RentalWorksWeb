using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using System;

namespace WebApi.Modules.Home.PhysicalInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "JIuxFUWTLDC6")]
    public class PhysicalInventoryController : AppDataController
    {
        public PhysicalInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PhysicalInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "t9au78IU5hbv")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventory/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "v62RNrqrfIF1")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/physicalinventory 
        [HttpGet]
        [FwControllerMethod(Id: "YlG1W5e9sS0d")]
        public async Task<ActionResult<IEnumerable<PhysicalInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PhysicalInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/physicalinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "TBXTq4dEa6wL")]
        public async Task<ActionResult<PhysicalInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PhysicalInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventory 
        [HttpPost]
        [FwControllerMethod(Id: "Tz9oHvrpRNNt")]
        public async Task<ActionResult<PhysicalInventoryLogic>> PostAsync([FromBody]PhysicalInventoryLogic l)
        {
            return await DoPostAsync<PhysicalInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventory/updateicodes 
        [HttpPost("updateicodes")]
        [FwControllerMethod(Id: "VCVEpGsZmrSWt")]
        public async Task<ActionResult<PhysicalInventoryUpdateICodesResponse>> UpdateICodes([FromBody]PhysicalInventoryUpdateICodesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PhysicalInventoryLogic l = new PhysicalInventoryLogic();
                l.SetDependencies(AppConfig, UserSession);
                l.PhysicalInventoryId = request.PhysicalInventoryId;
                if (await l.LoadAsync<PhysicalInventoryLogic>())
                {
                    PhysicalInventoryUpdateICodesResponse response = await PhysicalInventoryFunc.UpdateICodes(AppConfig, UserSession, request);
                    return new OkObjectResult(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/physicalinventory/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "xryKgL6RDOYa")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <PhysicalInventoryLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
