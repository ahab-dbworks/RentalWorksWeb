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

namespace WebApi.Modules.Agent.Quote
{
    [FwControl(Caption: "Document", SecurityId: "xCSRqSpYe73d", ControlType: FwControlTypes.Grid)]
    public partial class QuoteController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select orderid from dealorder o with (nolock) where ordertype = 'Q'";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{quoteid}/document/browse")]
        [FwControllerMethod(Id: "TGClY5PyaIBk", FwControllerActionTypes.ControlBrowse, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string quoteid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(QuoteDocumentLogic), "QuoteId", quoteid, string.Empty, string.Empty, request);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="browserequest"></param>
        /// <returns></returns>
        [HttpPost("{quoteid}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "uG1LRTjbSHSM", FwControllerActionTypes.ControlBrowse, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string quoteid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(QuoteDocumentLogic), "QuoteId", quoteid, string.Empty, string.Empty, browserequest);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{quoteid}/documents")]
        [FwControllerMethod(Id: "JL0T1TTpetM8", FwControllerActionTypes.ControlBrowse, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string quoteid, [FromQuery]QuoteDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(QuoteDocumentLogic), "QuoteId", quoteid, string.Empty, string.Empty, request);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{quoteid}/document/{documentid}")]
        [FwControllerMethod(Id: "OPhh7aMXbIxS", FwControllerActionTypes.ControlBrowse, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<QuoteDocumentLogic>> DocumentGetOneAsync([FromRoute]string quoteid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<QuoteDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(QuoteDocumentLogic), "QuoteId", quoteid, string.Empty, string.Empty, documentid);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{quoteid}/document")]
        [FwControllerMethod(Id: "1NhuX2vnX0Ko", FwControllerActionTypes.ControlNew, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<QuoteDocumentLogic>> DocumentNewAsync([FromRoute]string quoteid, [FromBody]QuoteDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<QuoteDocumentPostRequest, QuoteDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "QuoteId", quoteid, string.Empty, string.Empty, model);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{quoteid}/document/{documentid}")]
        [FwControllerMethod(Id: "f5HqYmgb437b", FwControllerActionTypes.ControlEdit, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<QuoteDocumentLogic>> DocumentEditAsync([FromRoute]string quoteid, [FromRoute]string documentid, [FromBody]QuoteDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<QuoteDocumentPutRequest, QuoteDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "QuoteId", quoteid, string.Empty, string.Empty, documentid, model);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{quoteid}/document/{documentid}")]
        [FwControllerMethod(Id: "LbdS44Laog3o", FwControllerActionTypes.ControlDelete, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string quoteid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<QuoteDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "QuoteId", quoteid, string.Empty, string.Empty, documentid);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="pageno">The page number of the result set, starting from page 1.</param>
        /// <param name="pagesize">The number of records per page of the result set.</param>
        /// <returns></returns>
        [HttpGet("{quoteid}/document/{documentid}/thumbnails")]
        [FwControllerMethod(Id: "MzBn8AlXf4cb", FwControllerActionTypes.ControlBrowse, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string quoteid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<QuoteDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{quoteid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "Dx9YCvY8QgRV", FwControllerActionTypes.ControlBrowse, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string quoteid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<QuoteDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{quoteid}/document/{documentid}/image")]
        [FwControllerMethod(Id: "XLs4pJ99cuNz", FwControllerActionTypes.ControlEdit, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string quoteid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<QuoteDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to POST.</param>
        /// <returns></returns>
        [HttpPost("{quoteid}/document/{documentid}/imageformupload")]
        [FwControllerMethod(Id: "U3dDSj6R9cpH", FwControllerActionTypes.ControlEdit, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string quoteid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<QuoteDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{quoteid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "5VI2r4GGQOyw", FwControllerActionTypes.ControlEdit, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string quoteid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<QuoteDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{quoteid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "jqJF5d8lTJeI", FwControllerActionTypes.ControlBrowse, ParentId: "xCSRqSpYe73d")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string quoteid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<QuoteDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{quoteid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "swpHkaWUxU8D", FwControllerActionTypes.ControlEdit, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string quoteid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<QuoteDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to PUT.</param>
        /// <returns></returns>
        [HttpPut("{quoteid}/document/{documentid}/fileformupload")]
        [FwControllerMethod(Id: "agRjWgRfFQvC", FwControllerActionTypes.ControlEdit, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string quoteid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<QuoteDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="quoteid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{quoteid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "IAV6dfXvQaK5", FwControllerActionTypes.ControlEdit, ParentId: "xCSRqSpYe73d")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string quoteid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<QuoteDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
