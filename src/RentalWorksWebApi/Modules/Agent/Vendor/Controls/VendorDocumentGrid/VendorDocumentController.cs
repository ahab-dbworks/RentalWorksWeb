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

namespace WebApi.Modules.Agent.Vendor
{
    [FwControl(Caption: "Document", SecurityId: "LGV6fYIyFsgT", ControlType: FwControlTypes.Grid)]
    public partial class VendorController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select vendorid from vendor with (nolock)";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{vendorid}/document/browse")]
        [FwControllerMethod(Id: "MJFw7Gda0c6U", FwControllerActionTypes.ControlBrowse, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string vendorid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(VendorDocumentLogic), "VendorId", vendorid, string.Empty, string.Empty, request);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="browserequest"></param>
        /// <returns></returns>
        [HttpPost("{vendorid}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "tfTvA4zRn6lz", FwControllerActionTypes.ControlBrowse, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string vendorid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(VendorDocumentLogic), "VendorId", vendorid, string.Empty, string.Empty, browserequest);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{vendorid}/documents")]
        [FwControllerMethod(Id: "b0SeaaWPAd1m", FwControllerActionTypes.ControlBrowse, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string vendorid, [FromQuery]VendorDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(VendorDocumentLogic), "VendorId", vendorid, string.Empty, string.Empty, request);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{vendorid}/document/{documentid}")]
        [FwControllerMethod(Id: "qgNX0BtIKaN9", FwControllerActionTypes.ControlBrowse, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<VendorDocumentLogic>> DocumentGetOneAsync([FromRoute]string vendorid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<VendorDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(VendorDocumentLogic), "VendorId", vendorid, string.Empty, string.Empty, documentid);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{vendorid}/document")]
        [FwControllerMethod(Id: "en8L3yHQzgvh", FwControllerActionTypes.ControlNew, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<VendorDocumentLogic>> DocumentNewAsync([FromRoute]string vendorid, [FromBody]VendorDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<VendorDocumentPostRequest, VendorDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "VendorId", vendorid, string.Empty, string.Empty, model);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{vendorid}/document/{documentid}")]
        [FwControllerMethod(Id: "X890qrvqoIfu", FwControllerActionTypes.ControlEdit, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<VendorDocumentLogic>> DocumentEditAsync([FromRoute]string vendorid, [FromRoute]string documentid, [FromBody]VendorDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<VendorDocumentPutRequest, VendorDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "VendorId", vendorid, string.Empty, string.Empty, documentid, model);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{vendorid}/document/{documentid}")]
        [FwControllerMethod(Id: "qL2VNLYamSaX", FwControllerActionTypes.ControlDelete, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string vendorid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<VendorDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "VendorId", vendorid, string.Empty, string.Empty, documentid);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="pageno">The page number of the result set, starting from page 1.</param>
        /// <param name="pagesize">The number of records per page of the result set.</param>
        /// <returns></returns>
        [HttpGet("{vendorid}/document/{documentid}/thumbnails")]
        [FwControllerMethod(Id: "bkWbFkVlUG91", FwControllerActionTypes.ControlBrowse, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string vendorid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<VendorDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{vendorid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "BCtuPyUGhNvs", FwControllerActionTypes.ControlBrowse, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string vendorid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<VendorDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{vendorid}/document/{documentid}/image")]
        [FwControllerMethod(Id: "GUdXru5f8mXe", FwControllerActionTypes.ControlEdit, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string vendorid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<VendorDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to POST.</param>
        /// <returns></returns>
        [HttpPost("{vendorid}/document/{documentid}/imageformupload")]
        [FwControllerMethod(Id: "DtTdV5ISLauk", FwControllerActionTypes.ControlEdit, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string vendorid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<VendorDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{vendorid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "sFuX7DPyYEFU", FwControllerActionTypes.ControlEdit, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string vendorid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<VendorDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{vendorid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "h72BL43qOp0x", FwControllerActionTypes.ControlBrowse, ParentId: "LGV6fYIyFsgT")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string vendorid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<VendorDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{vendorid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "ORTmfLPFB3GV", FwControllerActionTypes.ControlEdit, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string vendorid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<VendorDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to PUT.</param>
        /// <returns></returns>
        [HttpPut("{vendorid}/document/{documentid}/fileformupload")]
        [FwControllerMethod(Id: "aghEFAp8W96X", FwControllerActionTypes.ControlEdit, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string vendorid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<VendorDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="vendorid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{vendorid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "dhhQgdGdO7jp", FwControllerActionTypes.ControlEdit, ParentId: "LGV6fYIyFsgT")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string vendorid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<VendorDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
