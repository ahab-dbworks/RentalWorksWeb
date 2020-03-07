using FwCore.Controllers;
using FwCore.Grids.AppDocument;
using FwStandard.AppManager;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Inventory.Asset
{
    [FwControl(Caption: "Document", SecurityId: "pasdUk6LtsQB", ControlType: FwControlTypes.Grid)]
    public partial class ItemController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select rentalitemid from rentalitem with (nolock)";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="itemid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{itemid}/document/browse")]
        [FwControllerMethod(Id: "g22QWJEd5CC1", FwControllerActionTypes.ControlBrowse, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string itemid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(AssetDocumentLogic), "ItemId", itemid, string.Empty, string.Empty, request);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="browserequest"></param>
        /// <returns></returns>
        [HttpPost("{itemid}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "vYJV69KWEFXj", FwControllerActionTypes.ControlBrowse, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string itemid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(AssetDocumentLogic), "ItemId", itemid, string.Empty, string.Empty, browserequest);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{itemid}/documents")]
        [FwControllerMethod(Id: "TB4Re3EE40RE", FwControllerActionTypes.ControlBrowse, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string itemid, [FromQuery]AssetDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(AssetDocumentLogic), "ItemId", itemid, string.Empty, string.Empty, request);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{itemid}/document/{documentid}")]
        [FwControllerMethod(Id: "LlQevCuWIAS5", FwControllerActionTypes.ControlBrowse, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<AssetDocumentLogic>> DocumentGetOneAsync([FromRoute]string itemid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<AssetDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(AssetDocumentLogic), "ItemId", itemid, string.Empty, string.Empty, documentid);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{itemid}/document")]
        [FwControllerMethod(Id: "LlOuQ37KeT7v", FwControllerActionTypes.ControlNew, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<AssetDocumentLogic>> DocumentNewAsync([FromRoute]string itemid, [FromBody]AssetDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<AssetDocumentPostRequest, AssetDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "ItemId", itemid, string.Empty, string.Empty, model);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{itemid}/document/{documentid}")]
        [FwControllerMethod(Id: "OW9c877EhelV", FwControllerActionTypes.ControlEdit, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<AssetDocumentLogic>> DocumentEditAsync([FromRoute]string itemid, [FromRoute]string documentid, [FromBody]AssetDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<AssetDocumentPutRequest, AssetDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "ItemId", itemid, string.Empty, string.Empty, documentid, model);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{itemid}/document/{documentid}")]
        [FwControllerMethod(Id: "tFCcmbJtS9f4", FwControllerActionTypes.ControlDelete, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string itemid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<AssetDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "ItemId", itemid, string.Empty, string.Empty, documentid);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="pageno">The page number of the result set, starting from page 1.</param>
        /// <param name="pagesize">The number of records per page of the result set.</param>
        /// <returns></returns>
        [HttpGet("{itemid}/document/{documentid}/thumbnails")]
        [FwControllerMethod(Id: "okWa3gA2nvAH", FwControllerActionTypes.ControlBrowse, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string itemid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<AssetDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{itemid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "DrXKCoXHa3bm", FwControllerActionTypes.ControlBrowse, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string itemid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<AssetDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{itemid}/document/{documentid}/image")]
        [FwControllerMethod(Id: "B5zdlNMPQeYW", FwControllerActionTypes.ControlEdit, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string itemid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<AssetDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to POST.</param>
        /// <returns></returns>
        [HttpPost("{itemid}/document/{documentid}/imageformupload")]
        [FwControllerMethod(Id: "k7KaOsI7f4I4", FwControllerActionTypes.ControlEdit, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string itemid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<AssetDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{itemid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "n91EmI4vlLJ8", FwControllerActionTypes.ControlEdit, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string itemid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<AssetDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{itemid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "RxRJRU1yCbAF", FwControllerActionTypes.ControlBrowse, ParentId: "pasdUk6LtsQB")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string itemid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<AssetDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{itemid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "MW3RudfGwE1k", FwControllerActionTypes.ControlEdit, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string itemid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<AssetDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to PUT.</param>
        /// <returns></returns>
        [HttpPut("{itemid}/document/{documentid}/fileformupload")]
        [FwControllerMethod(Id: "WjDhlDNJxMtr", FwControllerActionTypes.ControlEdit, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string itemid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<AssetDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="itemid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{itemid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "K9qvdkCmJv1y", FwControllerActionTypes.ControlEdit, ParentId: "pasdUk6LtsQB")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string itemid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<AssetDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
