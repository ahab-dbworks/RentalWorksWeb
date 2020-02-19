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

namespace WebApi.Modules.Agent.Deal
{
    [FwControl(Caption: "Document", SecurityId: "5pVhTJtGXLVx", ControlType: FwControlTypes.Grid)]
    public partial class DealController
    {
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="dealid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{dealid}/document/browse")]
        [FwControllerMethod(Id: "Ar2pslw5CwkB", FwControllerActionTypes.ControlBrowse, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string dealid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(DealDocumentLogic), "DealId", dealid, string.Empty, string.Empty, request);
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
        /// <param name="dealid">Unique identifier</param>
        /// <param name="browserequest"></param>
        /// <returns></returns>
        [HttpPost("{dealid}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "v2tnr6Wq95UA", FwControllerActionTypes.ControlBrowse, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string dealid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(DealDocumentLogic), "DealId", dealid, string.Empty, string.Empty, browserequest);
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
        /// <param name="dealid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{dealid}/documents")]
        [FwControllerMethod(Id: "KcMEftbqa474", FwControllerActionTypes.ControlBrowse, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string dealid, [FromQuery]DealDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(DealDocumentLogic), "DealId", dealid, string.Empty, string.Empty, request);
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
        /// <param name="dealid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{dealid}/document/{documentid}")]
        [FwControllerMethod(Id: "bgHn8Q47Fyzs", FwControllerActionTypes.ControlBrowse, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<DealDocumentLogic>> DocumentGetOneAsync([FromRoute]string dealid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(DealDocumentLogic), "DealId", dealid, string.Empty, string.Empty, documentid);
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
        /// <param name="dealid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{dealid}/document")]
        [FwControllerMethod(Id: "WJL1tk6IeuV2", FwControllerActionTypes.ControlNew, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<DealDocumentLogic>> DocumentNewAsync([FromRoute]string dealid, [FromBody]DealDocumentPostRequest model)
        {
            try
            {
                var logic = AutoMapper.Mapper.Map<DealDocumentLogic>(model);
                return await FwAppDocumentController.NewAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "DealId", dealid, string.Empty, string.Empty, logic);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Edit a document.
        /// </summary>
        /// <param name="dealid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{dealid}/document/{documentid}")]
        [FwControllerMethod(Id: "rgq1YFxEGCnk", FwControllerActionTypes.ControlEdit, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<DealDocumentLogic>> DocumentEditAsync([FromRoute]string dealid, [FromRoute]string documentid, [FromBody]DealDocumentPutRequest model)
        {
            try
            {
                var logic = AutoMapper.Mapper.Map<DealDocumentLogic>(model);
                return await FwAppDocumentController.EditAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "DealId", dealid, string.Empty, string.Empty, documentid, logic);
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
        /// <param name="dealid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{dealid}/document/{documentid}")]
        [FwControllerMethod(Id: "Mj6UCo67CaAQ", FwControllerActionTypes.ControlDelete, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string dealid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "DealId", dealid, string.Empty, string.Empty, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get document thumbnails for any images attached to the document.
        /// </summary>
        /// <param name="dealid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="pageno">The page number of the result set, starting from page 1.</param>
        /// <param name="pagesize">The number of records per page of the result set.</param>
        /// <returns></returns>
        [HttpGet("{dealid}/document/{documentid}/thumbnails")]
        [FwControllerMethod(Id: "dslOM5N0cfGE", FwControllerActionTypes.ControlBrowse, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string dealid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "deal", "dealid", documentid, pageno, pagesize);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get a fullsize document image.
        /// </summary>
        /// <param name="dealid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{dealid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "bHBWHDzXPYDs", FwControllerActionTypes.ControlBrowse, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string dealid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "deal", "dealid", documentid, imageid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Attach an image to a document (supports multiple images, but must be uploaded in separate requests).
        /// </summary>
        /// <param name="dealid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{dealid}/document/{documentid}/image")]
        [FwControllerMethod(Id: "AX4NPsS7nIuQ", FwControllerActionTypes.ControlNew, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string dealid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "deal", "dealid", documentid, request);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Attach an image to a document (supports multiple images, but must be uploaded in separate requests).
        /// </summary>
        /// <param name="dealid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to POST.</param>
        /// <returns></returns>
        [HttpPost("{dealid}/document/{documentid}/uploadimage")]
        [FwControllerMethod(Id: "TUsk5CeVVEdr", FwControllerActionTypes.ControlNew, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string dealid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "deal", "dealid", documentid, file);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Delete document image.
        /// </summary>
        /// <param name="dealid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{dealid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "oCvKnIanCUGH", FwControllerActionTypes.ControlDelete, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string dealid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "deal", "dealid", documentid, imageid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get document file.
        /// </summary>
        /// <param name="dealid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{dealid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "0MS5Tv3YvqwF", FwControllerActionTypes.ControlBrowse, ParentId: "5pVhTJtGXLVx")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string dealid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<DealDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, "deal", "dealid", documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Attach a file to a document.
        /// </summary>
        /// <param name="dealid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{dealid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "TEeu5N68gYrj", FwControllerActionTypes.ControlNew, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string dealid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "deal", "dealid", documentid, request);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Attach a file to a document.  This will overwrite any existing file attached to this document without warning.
        /// </summary>
        /// <param name="dealid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to PUT.</param>
        /// <returns></returns>
        [HttpPut("{dealid}/document/{documentid}/uploadfile")]
        [FwControllerMethod(Id: "RzSbofEAfnCY", FwControllerActionTypes.ControlNew, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string dealid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "deal", "dealid", documentid, file);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Delete document file.
        /// </summary>
        /// <param name="dealid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{dealid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "udhmtXfoY2Ak", FwControllerActionTypes.ControlDelete, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string dealid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "deal", "dealid", documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
