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

namespace WebApi.Modules.Inventory.SalesInventory
{
    [FwControl(Caption: "Document", SecurityId: "Fpb2CAabL83x", ControlType: FwControlTypes.Grid)]
    public partial class SalesInventoryController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select masterid from master with (nolock) where availfor = 'S'";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{inventoryid}/document/browse")]
        [FwControllerMethod(Id: "KSkg3LLgequd", FwControllerActionTypes.ControlBrowse, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string inventoryid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(SalesInventoryDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, request);
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
        [FwControllerMethod(Id: "oC4mroDnOtOl", FwControllerActionTypes.ControlBrowse, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string inventoryid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(SalesInventoryDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, browserequest);
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
        [FwControllerMethod(Id: "rn2zqCBhQxbz", FwControllerActionTypes.ControlBrowse, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string inventoryid, [FromQuery]SalesInventoryDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(SalesInventoryDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, request);
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
        [FwControllerMethod(Id: "g7PrYQZji0aU", FwControllerActionTypes.ControlBrowse, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<SalesInventoryDocumentLogic>> DocumentGetOneAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<SalesInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(SalesInventoryDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, documentid);
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
        [FwControllerMethod(Id: "OKnUHljMybPP", FwControllerActionTypes.ControlNew, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<SalesInventoryDocumentLogic>> DocumentNewAsync([FromRoute]string inventoryid, [FromBody]SalesInventoryDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<SalesInventoryDocumentPostRequest, SalesInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "InventoryId", inventoryid, string.Empty, string.Empty, model);
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
        [FwControllerMethod(Id: "UpLcHEoojOzY", FwControllerActionTypes.ControlEdit, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<SalesInventoryDocumentLogic>> DocumentEditAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromBody]SalesInventoryDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<SalesInventoryDocumentPutRequest, SalesInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "InventoryId", inventoryid, string.Empty, string.Empty, documentid, model);
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
        [FwControllerMethod(Id: "FSORZgOGpo7Q", FwControllerActionTypes.ControlDelete, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<SalesInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "InventoryId", inventoryid, string.Empty, string.Empty, documentid);
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
        [FwControllerMethod(Id: "FVMUckfUQ3xz", FwControllerActionTypes.ControlBrowse, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<SalesInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
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
        [FwControllerMethod(Id: "BY00JkkmZzmJ", FwControllerActionTypes.ControlBrowse, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<SalesInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        [FwControllerMethod(Id: "ntZ5PSdueOOU", FwControllerActionTypes.ControlEdit, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<SalesInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        [FwControllerMethod(Id: "IX7Xy3hGyFV5", FwControllerActionTypes.ControlEdit, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<SalesInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        [FwControllerMethod(Id: "VF01ZTaIuUJE", FwControllerActionTypes.ControlEdit, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<SalesInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        [FwControllerMethod(Id: "iaRKsvp1wUll", FwControllerActionTypes.ControlBrowse, ParentId: "Fpb2CAabL83x")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<SalesInventoryDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
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
        [FwControllerMethod(Id: "r2jRZGOkZWwP", FwControllerActionTypes.ControlEdit, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<SalesInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        [FwControllerMethod(Id: "uehaOqaxaDAa", FwControllerActionTypes.ControlEdit, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<SalesInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        [FwControllerMethod(Id: "v6s02zTzeqNK", FwControllerActionTypes.ControlEdit, ParentId: "Fpb2CAabL83x")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<SalesInventoryDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
