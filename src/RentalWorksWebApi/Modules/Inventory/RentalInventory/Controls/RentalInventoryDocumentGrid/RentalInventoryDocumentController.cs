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

namespace WebApi.Modules.Inventory.RentalInventory
{
    [FwControl(Caption: "Document", SecurityId: "DCfBWlHhvSDH", ControlType: FwControlTypes.Grid)]
    public partial class RentalInventoryController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select masterid from master with (nolock) where availfor = 'R'";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{inventoryid}/document/browse")]
        [FwControllerMethod(Id: "VyvNY16aFHBR", FwControllerActionTypes.ControlBrowse, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string inventoryid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(RentalInventoryDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, request);
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
        [FwControllerMethod(Id: "ymCjlZHNv37G", FwControllerActionTypes.ControlBrowse, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string inventoryid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(RentalInventoryDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, browserequest);
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
        [FwControllerMethod(Id: "Ee62dIAWuVNx", FwControllerActionTypes.ControlBrowse, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string inventoryid, [FromQuery]RentalInventoryDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(RentalInventoryDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, request);
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
        [FwControllerMethod(Id: "5EyYsKfnuqex", FwControllerActionTypes.ControlBrowse, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<RentalInventoryDocumentLogic>> DocumentGetOneAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<RentalInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(RentalInventoryDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, documentid);
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
        [FwControllerMethod(Id: "a3aM2EFoy1MJ", FwControllerActionTypes.ControlNew, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<RentalInventoryDocumentLogic>> DocumentNewAsync([FromRoute]string inventoryid, [FromBody]RentalInventoryDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<RentalInventoryDocumentPostRequest, RentalInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "InventoryId", inventoryid, string.Empty, string.Empty, model);
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
        [FwControllerMethod(Id: "rq9KnsE1OcGY", FwControllerActionTypes.ControlEdit, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<RentalInventoryDocumentLogic>> DocumentEditAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromBody]RentalInventoryDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<RentalInventoryDocumentPutRequest, RentalInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "InventoryId", inventoryid, string.Empty, string.Empty, documentid, model);
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
        [FwControllerMethod(Id: "A7cnuE45bIgV", FwControllerActionTypes.ControlDelete, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<RentalInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "InventoryId", inventoryid, string.Empty, string.Empty, documentid);
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
        [FwControllerMethod(Id: "T036xble1ne6", FwControllerActionTypes.ControlBrowse, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<RentalInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
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
        [FwControllerMethod(Id: "3hmQthSSuTCi", FwControllerActionTypes.ControlBrowse, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<RentalInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        [FwControllerMethod(Id: "V5JCtjjPX4Qh", FwControllerActionTypes.ControlEdit, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<RentalInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        [FwControllerMethod(Id: "6Z6V3SWbstX8", FwControllerActionTypes.ControlEdit, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<RentalInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        [FwControllerMethod(Id: "nx2rBisrCmhU", FwControllerActionTypes.ControlEdit, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<RentalInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        [FwControllerMethod(Id: "8uovxKTqX5tG", FwControllerActionTypes.ControlBrowse, ParentId: "DCfBWlHhvSDH")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<RentalInventoryDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
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
        [FwControllerMethod(Id: "2KG8mjOw5pDb", FwControllerActionTypes.ControlEdit, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<RentalInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        [FwControllerMethod(Id: "MAj7pjMgzr5R", FwControllerActionTypes.ControlEdit, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<RentalInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        [FwControllerMethod(Id: "6dNTtYUnqGbu", FwControllerActionTypes.ControlEdit, ParentId: "DCfBWlHhvSDH")]
        public async Task<ActionResult<bool>> DeleteFileAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<RentalInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
