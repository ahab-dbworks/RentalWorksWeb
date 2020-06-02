using FwCore.Controllers;
using FwCore.Grids.AppDocument;
using FwStandard.AppManager;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Modules.Inventory.PartsInventory
{
    [FwControl(Caption: "Document", SecurityId: "lwYABhj5zknM", ControlType: FwControlTypes.Grid)]
    public partial class PartsInventoryController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select masterid from master with (nolock) where availfor = 'P'";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{inventoryid}/document/browse")]
        [FwControllerMethod(Id: "3yMvnZepFo29", FwControllerActionTypes.ControlBrowse, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string inventoryid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(PartsInventoryDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, request);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Export documents to Excel.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="browserequest"></param>
        /// <returns></returns>
        [HttpPost("{inventoryid}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "J8ux1Vq1czKe", FwControllerActionTypes.ControlBrowse, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string inventoryid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(PartsInventoryDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, browserequest);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{inventoryid}/documents")]
        [FwControllerMethod(Id: "8ZD6ALP8eOSt", FwControllerActionTypes.ControlBrowse, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string inventoryid, [FromQuery]PartsInventoryDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(PartsInventoryDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, request);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get a document.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{inventoryid}/document/{documentid}")]
        [FwControllerMethod(Id: "nQ7Snbcph6Y8", FwControllerActionTypes.ControlBrowse, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<PartsInventoryDocumentLogic>> DocumentGetOneAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<PartsInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(PartsInventoryDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Create a new document.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{inventoryid}/document")]
        [FwControllerMethod(Id: "yuQHPVBiXrOE", FwControllerActionTypes.ControlNew, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<PartsInventoryDocumentLogic>> DocumentNewAsync([FromRoute]string inventoryid, [FromBody]PartsInventoryDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<PartsInventoryDocumentPostRequest, PartsInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "InventoryId", inventoryid, string.Empty, string.Empty, model);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Update a document.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{inventoryid}/document/{documentid}")]
        [FwControllerMethod(Id: "kqPV0U2AqOxJ", FwControllerActionTypes.ControlEdit, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<PartsInventoryDocumentLogic>> DocumentEditAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromBody]PartsInventoryDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<PartsInventoryDocumentPutRequest, PartsInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "InventoryId", inventoryid, string.Empty, string.Empty, documentid, model);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Delete a document.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{inventoryid}/document/{documentid}")]
        [FwControllerMethod(Id: "zig4wif7FDuE", FwControllerActionTypes.ControlDelete, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<PartsInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "InventoryId", inventoryid, string.Empty, string.Empty, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get thumbnails for any images attached to the document.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="pageno">The page number of the result set, starting from page 1.</param>
        /// <param name="pagesize">The number of records per page of the result set.</param>
        /// <returns></returns>
        [HttpGet("{inventoryid}/document/{documentid}/thumbnails")]
        [FwControllerMethod(Id: "4GV6PVZj3Qta", FwControllerActionTypes.ControlBrowse, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<PartsInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get a fullsize image.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{inventoryid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "G4UFLsThdCa8", FwControllerActionTypes.ControlBrowse, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<PartsInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Attach an image from a dataurl.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{inventoryid}/document/{documentid}/image")]
        [FwControllerMethod(Id: "RssoZyfB1GNY", FwControllerActionTypes.ControlEdit, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<PartsInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Attach an image from a form submission.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to POST.</param>
        /// <returns></returns>
        [HttpPost("{inventoryid}/document/{documentid}/imageformupload")]
        [FwControllerMethod(Id: "Yb1QLewzqGlH", FwControllerActionTypes.ControlEdit, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<PartsInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Delete an attached image.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{inventoryid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "wZhLLsJTfjgs", FwControllerActionTypes.ControlEdit, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<PartsInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get the attached file.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{inventoryid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "YqTR3TYSxLeB", FwControllerActionTypes.ControlBrowse, ParentId: "lwYABhj5zknM")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<PartsInventoryDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Update the attached file from a dataurl.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{inventoryid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "7tHBcka2SO1C", FwControllerActionTypes.ControlEdit, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<PartsInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Update attached file from a form submission.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to PUT.</param>
        /// <returns></returns>
        [HttpPut("{inventoryid}/document/{documentid}/fileformupload")]
        [FwControllerMethod(Id: "8t7cjWvofkgH", FwControllerActionTypes.ControlEdit, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<PartsInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Delete attached file.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{inventoryid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "pZasTEyzOnCj", FwControllerActionTypes.ControlEdit, ParentId: "lwYABhj5zknM")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<PartsInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
