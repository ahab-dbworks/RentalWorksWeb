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

namespace WebApi.Modules.Agent.Project
{
    [FwControl(Caption: "Document", SecurityId: "xTTNkaom7t5q", ControlType: FwControlTypes.Grid)]
    public partial class ProjectController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select projectid from projectview with (nolock)";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="projectid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{projectid}/document/browse")]
        [FwControllerMethod(Id: "lrhBRfLM2aYI", FwControllerActionTypes.ControlBrowse, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string projectid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(ProjectDocumentLogic), "ProjectId", projectid, string.Empty, string.Empty, request);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="browserequest"></param>
        /// <returns></returns>
        [HttpPost("{projectid}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "f29uUU6nLAap", FwControllerActionTypes.ControlBrowse, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string projectid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(ProjectDocumentLogic), "ProjectId", projectid, string.Empty, string.Empty, browserequest);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{projectid}/documents")]
        [FwControllerMethod(Id: "g6QvpUtqQeCR", FwControllerActionTypes.ControlBrowse, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string projectid, [FromQuery]ProjectDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(ProjectDocumentLogic), "ProjectId", projectid, string.Empty, string.Empty, request);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{projectid}/document/{documentid}")]
        [FwControllerMethod(Id: "E3Ukhv6x4kIt", FwControllerActionTypes.ControlBrowse, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<ProjectDocumentLogic>> DocumentGetOneAsync([FromRoute]string projectid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<ProjectDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(ProjectDocumentLogic), "ProjectId", projectid, string.Empty, string.Empty, documentid);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectid}/document")]
        [FwControllerMethod(Id: "b3BmwROTq2vV", FwControllerActionTypes.ControlNew, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<ProjectDocumentLogic>> DocumentNewAsync([FromRoute]string projectid, [FromBody]ProjectDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<ProjectDocumentPostRequest, ProjectDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "ProjectId", projectid, string.Empty, string.Empty, model);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{projectid}/document/{documentid}")]
        [FwControllerMethod(Id: "yqyhRI2bAr02", FwControllerActionTypes.ControlEdit, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<ProjectDocumentLogic>> DocumentEditAsync([FromRoute]string projectid, [FromRoute]string documentid, [FromBody]ProjectDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<ProjectDocumentPutRequest, ProjectDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "ProjectId", projectid, string.Empty, string.Empty, documentid, model);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{projectid}/document/{documentid}")]
        [FwControllerMethod(Id: "aVfqDNu5yyuv", FwControllerActionTypes.ControlDelete, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string projectid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<ProjectDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "ProjectId", projectid, string.Empty, string.Empty, documentid);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="pageno">The page number of the result set, starting from page 1.</param>
        /// <param name="pagesize">The number of records per page of the result set.</param>
        /// <returns></returns>
        [HttpGet("{projectid}/document/{documentid}/thumbnails")]
        [FwControllerMethod(Id: "ug48ARCLH8V1", FwControllerActionTypes.ControlBrowse, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string projectid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<ProjectDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{projectid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "6YPwsH2TdImK", FwControllerActionTypes.ControlBrowse, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string projectid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<ProjectDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{projectid}/document/{documentid}/image")]
        [FwControllerMethod(Id: "LyqLHzya6cWK", FwControllerActionTypes.ControlEdit, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string projectid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<ProjectDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to POST.</param>
        /// <returns></returns>
        [HttpPost("{projectid}/document/{documentid}/imageformupload")]
        [FwControllerMethod(Id: "O3KSYouc9puO", FwControllerActionTypes.ControlEdit, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string projectid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<ProjectDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{projectid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "88M1FJsU5twb", FwControllerActionTypes.ControlEdit, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string projectid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<ProjectDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{projectid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "B0djJV44beRz", FwControllerActionTypes.ControlBrowse, ParentId: "xTTNkaom7t5q")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string projectid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<ProjectDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{projectid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "6l4YpjKxeLNJ", FwControllerActionTypes.ControlEdit, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string projectid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<ProjectDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to PUT.</param>
        /// <returns></returns>
        [HttpPut("{projectid}/document/{documentid}/fileformupload")]
        [FwControllerMethod(Id: "VHEwUbPvHJr9", FwControllerActionTypes.ControlEdit, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string projectid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<ProjectDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="projectid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{projectid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "kkCouAM4ro5w", FwControllerActionTypes.ControlEdit, ParentId: "xTTNkaom7t5q")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string projectid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<ProjectDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
