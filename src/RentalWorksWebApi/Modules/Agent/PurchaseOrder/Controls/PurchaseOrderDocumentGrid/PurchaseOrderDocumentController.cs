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

namespace WebApi.Modules.Agent.PurchaseOrder
{
    [FwControl(Caption: "Document", SecurityId: "OCGVS960nEwc", ControlType: FwControlTypes.Grid)]
    public partial class PurchaseOrderController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select orderid from powebview with (nolock)";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{purchaseorderid}/document/browse")]
        [FwControllerMethod(Id: "sHlayw1w25Od", FwControllerActionTypes.ControlBrowse, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string purchaseorderid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(PurchaseOrderDocumentLogic), "PurchaseOrderId", purchaseorderid, string.Empty, string.Empty, request);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="browserequest"></param>
        /// <returns></returns>
        [HttpPost("{purchaseorderid}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "QxcO8SsHMhQs", FwControllerActionTypes.ControlBrowse, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string purchaseorderid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(PurchaseOrderDocumentLogic), "PurchaseOrderId", purchaseorderid, string.Empty, string.Empty, browserequest);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{purchaseorderid}/documents")]
        [FwControllerMethod(Id: "F4MIMBWPXA6V", FwControllerActionTypes.ControlBrowse, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string purchaseorderid, [FromQuery]PurchaseOrderDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(PurchaseOrderDocumentLogic), "PurchaseOrderId", purchaseorderid, string.Empty, string.Empty, request);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{purchaseorderid}/document/{documentid}")]
        [FwControllerMethod(Id: "rMG4xIDTXCev", FwControllerActionTypes.ControlBrowse, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<PurchaseOrderDocumentLogic>> DocumentGetOneAsync([FromRoute]string purchaseorderid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<PurchaseOrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(PurchaseOrderDocumentLogic), "PurchaseOrderId", purchaseorderid, string.Empty, string.Empty, documentid);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{purchaseorderid}/document")]
        [FwControllerMethod(Id: "6s1DmYzl11ED", FwControllerActionTypes.ControlNew, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<PurchaseOrderDocumentLogic>> DocumentNewAsync([FromRoute]string purchaseorderid, [FromBody]PurchaseOrderDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<PurchaseOrderDocumentPostRequest, PurchaseOrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "PurchaseOrderId", purchaseorderid, string.Empty, string.Empty, model);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{purchaseorderid}/document/{documentid}")]
        [FwControllerMethod(Id: "TtSBj8pqBqyw", FwControllerActionTypes.ControlEdit, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<PurchaseOrderDocumentLogic>> DocumentEditAsync([FromRoute]string purchaseorderid, [FromRoute]string documentid, [FromBody]PurchaseOrderDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<PurchaseOrderDocumentPutRequest, PurchaseOrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "PurchaseOrderId", purchaseorderid, string.Empty, string.Empty, documentid, model);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{purchaseorderid}/document/{documentid}")]
        [FwControllerMethod(Id: "nykdO7DdYrLv", FwControllerActionTypes.ControlDelete, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string purchaseorderid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<PurchaseOrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "PurchaseOrderId", purchaseorderid, string.Empty, string.Empty, documentid);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="pageno">The page number of the result set, starting from page 1.</param>
        /// <param name="pagesize">The number of records per page of the result set.</param>
        /// <returns></returns>
        [HttpGet("{purchaseorderid}/document/{documentid}/thumbnails")]
        [FwControllerMethod(Id: "JtE9bV5zhckd", FwControllerActionTypes.ControlBrowse, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string purchaseorderid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<PurchaseOrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{purchaseorderid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "0bKYnY3P3FrX", FwControllerActionTypes.ControlBrowse, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string purchaseorderid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<PurchaseOrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{purchaseorderid}/document/{documentid}/image")]
        [FwControllerMethod(Id: "NvF3yBOCRx20", FwControllerActionTypes.ControlEdit, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string purchaseorderid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<PurchaseOrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to POST.</param>
        /// <returns></returns>
        [HttpPost("{purchaseorderid}/document/{documentid}/imageformupload")]
        [FwControllerMethod(Id: "EqpOv9oknhoM", FwControllerActionTypes.ControlEdit, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string purchaseorderid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<PurchaseOrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{purchaseorderid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "xO0ax6BecQvP", FwControllerActionTypes.ControlEdit, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string purchaseorderid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<PurchaseOrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{purchaseorderid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "rcbQxRvClUu7", FwControllerActionTypes.ControlBrowse, ParentId: "OCGVS960nEwc")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string purchaseorderid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<PurchaseOrderDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{purchaseorderid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "Jpv4sfKGh2GS", FwControllerActionTypes.ControlEdit, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string purchaseorderid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<PurchaseOrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to PUT.</param>
        /// <returns></returns>
        [HttpPut("{purchaseorderid}/document/{documentid}/fileformupload")]
        [FwControllerMethod(Id: "KNeU6XWX4Jys", FwControllerActionTypes.ControlEdit, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string purchaseorderid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<PurchaseOrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="purchaseorderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{purchaseorderid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "8GWEVVCLu3Bj", FwControllerActionTypes.ControlEdit, ParentId: "OCGVS960nEwc")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string purchaseorderid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<PurchaseOrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
