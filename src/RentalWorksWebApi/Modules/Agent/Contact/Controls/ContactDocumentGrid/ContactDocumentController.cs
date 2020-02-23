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

namespace WebApi.Modules.Agent.Contact
{
    [FwControl(Caption: "Document", SecurityId: "OdKeQWKOM7sL", ControlType: FwControlTypes.Grid)]
    public partial class ContactController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select contactid from contact with (nolock)";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="contactid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{contactid}/document/browse")]
        [FwControllerMethod(Id: "yXjNrC81KsWA", FwControllerActionTypes.ControlBrowse, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string contactid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(ContactDocumentLogic), "ContactId", contactid, string.Empty, string.Empty, request);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="browserequest"></param>
        /// <returns></returns>
        [HttpPost("{contactid}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "5DEfazjv7cfa", FwControllerActionTypes.ControlBrowse, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string contactid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(ContactDocumentLogic), "ContactId", contactid, string.Empty, string.Empty, browserequest);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{contactid}/documents")]
        [FwControllerMethod(Id: "f43Mgp5bGDs3", FwControllerActionTypes.ControlBrowse, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string contactid, [FromQuery]ContactDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(ContactDocumentLogic), "ContactId", contactid, string.Empty, string.Empty, request);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{contactid}/document/{documentid}")]
        [FwControllerMethod(Id: "ebW0b9ywrlln", FwControllerActionTypes.ControlBrowse, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<ContactDocumentLogic>> DocumentGetOneAsync([FromRoute]string contactid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<ContactDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(ContactDocumentLogic), "ContactId", contactid, string.Empty, string.Empty, documentid);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{contactid}/document")]
        [FwControllerMethod(Id: "sENnY6NvSjHw", FwControllerActionTypes.ControlNew, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<ContactDocumentLogic>> DocumentNewAsync([FromRoute]string contactid, [FromBody]ContactDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<ContactDocumentPostRequest, ContactDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "ContactId", contactid, string.Empty, string.Empty, model);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{contactid}/document/{documentid}")]
        [FwControllerMethod(Id: "IvTwxU4BkLa8", FwControllerActionTypes.ControlEdit, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<ContactDocumentLogic>> DocumentEditAsync([FromRoute]string contactid, [FromRoute]string documentid, [FromBody]ContactDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<ContactDocumentPutRequest, ContactDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "ContactId", contactid, string.Empty, string.Empty, documentid, model);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{contactid}/document/{documentid}")]
        [FwControllerMethod(Id: "Ltu8OTS5UPuM", FwControllerActionTypes.ControlDelete, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string contactid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<ContactDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "ContactId", contactid, string.Empty, string.Empty, documentid);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="pageno">The page number of the result set, starting from page 1.</param>
        /// <param name="pagesize">The number of records per page of the result set.</param>
        /// <returns></returns>
        [HttpGet("{contactid}/document/{documentid}/thumbnails")]
        [FwControllerMethod(Id: "KRnKY7l5EBrP", FwControllerActionTypes.ControlBrowse, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string contactid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<ContactDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{contactid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "pc8FqRcQDPLG", FwControllerActionTypes.ControlBrowse, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string contactid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<ContactDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{contactid}/document/{documentid}/image")]
        [FwControllerMethod(Id: "LPQ9ALL2ahUP", FwControllerActionTypes.ControlEdit, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string contactid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<ContactDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to POST.</param>
        /// <returns></returns>
        [HttpPost("{contactid}/document/{documentid}/imageformupload")]
        [FwControllerMethod(Id: "ItMnBeEkbNSL", FwControllerActionTypes.ControlEdit, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string contactid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<ContactDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{contactid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "UnPcNMj5fP8l", FwControllerActionTypes.ControlEdit, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string contactid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<ContactDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{contactid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "rkF2YzOgrs76", FwControllerActionTypes.ControlBrowse, ParentId: "OdKeQWKOM7sL")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string contactid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<ContactDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{contactid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "fYqymBF86eon", FwControllerActionTypes.ControlEdit, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string contactid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<ContactDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to PUT.</param>
        /// <returns></returns>
        [HttpPut("{contactid}/document/{documentid}/fileformupload")]
        [FwControllerMethod(Id: "6Ipy90K2vC6q", FwControllerActionTypes.ControlEdit, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string contactid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<ContactDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="contactid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{contactid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "JMvM9LuCqS7X", FwControllerActionTypes.ControlEdit, ParentId: "OdKeQWKOM7sL")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string contactid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<ContactDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
