using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using System;

namespace WebApi.Modules.Inventory.PhysicalInventory
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
        // POST api/v1/physicalinventory/prescan
        [HttpPost("prescan")]
        [FwControllerMethod(Id: "Pxn9KhE4LOVRv")]
        public async Task<ActionResult<PhysicalInventoryPrescanResponse>> Prescan([FromBody]PhysicalInventoryPrescanRequest request)
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
                    PhysicalInventoryPrescanResponse response = await PhysicalInventoryFunc.Prescan(AppConfig, UserSession, request);
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
        // POST api/v1/physicalinventory/initiate
        [HttpPost("initiate")]
        [FwControllerMethod(Id: "CVr2bgh4JCuBL")]
        public async Task<ActionResult<PhysicalInventoryInitiateResponse>> Initiate([FromBody]PhysicalInventoryInitiateRequest request)
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
                    PhysicalInventoryInitiateResponse response = await PhysicalInventoryFunc.Initiate(AppConfig, UserSession, request);
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
        // POST api/v1/physicalinventory/A0000001/updatestep/printcountsheet
        [HttpPost("{id}/updatestep/{stepname}")]
        [FwControllerMethod(Id: "oKpeX6tpLVHXq")]
        public async Task<ActionResult<PhysicalInventoryLogic>> UpdateStep([FromRoute]string id, [FromRoute]string stepname)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PhysicalInventoryLogic l = new PhysicalInventoryLogic();
                l.SetDependencies(AppConfig, UserSession);
                l.PhysicalInventoryId = id;
                if (await l.LoadAsync<PhysicalInventoryLogic>())
                {

                    //PhysicalInventoryLogic lOrig = new PhysicalInventoryLogic();
                    //lOrig.SetDependencies(AppConfig, UserSession);
                    //lOrig.PhysicalInventoryId = id;
                    //await lOrig.LoadAsync<PhysicalInventoryLogic>();

                    if (stepname.Equals("printcountsheet"))
                    {
                        l.StepPrintCountSheets++;
                    }
                    else
                    {
                        return NotFound();
                    }
                    //l.SetDependencies(AppConfig, UserSession);
                    //await l.SaveAsync(lOrig);
                    await l.SaveAsync(null);
                    return new OkObjectResult(l);
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
        // POST api/v1/physicalinventory/countbarcode 
        [HttpPost("countbarcode")]
        [FwControllerMethod(Id: "CsBdv9lRxfqhd")]
        public async Task<ActionResult<PhysicalInventoryCountBarCodeResponse>> CountBarCode([FromBody]PhysicalInventoryCountBarCodeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //PhysicalInventoryLogic l = new PhysicalInventoryLogic();
                //l.SetDependencies(AppConfig, UserSession);
                //l.PhysicalInventoryId = request.PhysicalInventoryId;
                //if (await l.LoadAsync<PhysicalInventoryLogic>())
                //{
                PhysicalInventoryCountBarCodeResponse response = await PhysicalInventoryFunc.CountBarCode(AppConfig, UserSession, request);
                return new OkObjectResult(response);
                //}
                //else
                //{
                //    return NotFound();
                //}
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventory/countquantity 
        [HttpPost("countquantity")]
        [FwControllerMethod(Id: "PqV4CZyzk6jtb")]
        public async Task<ActionResult<PhysicalInventoryCountBarCodeResponse>> CountQuantity([FromBody]PhysicalInventoryCountQuantityRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //PhysicalInventoryLogic l = new PhysicalInventoryLogic();
                //l.SetDependencies(AppConfig, UserSession);
                //l.PhysicalInventoryId = request.PhysicalInventoryId;
                //if (await l.LoadAsync<PhysicalInventoryLogic>())
                //{
                PhysicalInventoryCountQuantityResponse response = await PhysicalInventoryFunc.CountQuantity(AppConfig, UserSession, request);
                return new OkObjectResult(response);
                //}
                //else
                //{
                //    return NotFound();
                //}
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/physicalinventory/approve
        [HttpPost("approve")]
        [FwControllerMethod(Id: "OS4eFb7OULch4")]
        public async Task<ActionResult<PhysicalInventoryApproveResponse>> Approve([FromBody]PhysicalInventoryApproveRequest request)
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
                    PhysicalInventoryApproveResponse response = await PhysicalInventoryFunc.Approve(AppConfig, UserSession, request);
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
        // POST api/v1/physicalinventory/close
        [HttpPost("close")]
        [FwControllerMethod(Id: "cwtTFcSUMUkSf")]
        public async Task<ActionResult<PhysicalInventoryCloseResponse>> Close([FromBody]PhysicalInventoryCloseRequest request)
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
                    PhysicalInventoryCloseResponse response = await PhysicalInventoryFunc.Close(AppConfig, UserSession, request);
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