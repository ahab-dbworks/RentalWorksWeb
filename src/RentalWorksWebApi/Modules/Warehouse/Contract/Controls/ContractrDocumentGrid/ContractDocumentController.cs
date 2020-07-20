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

namespace WebApi.Modules.Warehouse.Contract
{
    [FwControl(Caption: "Document", SecurityId: "ZyFGQw4UylcX", ControlType: FwControlTypes.Grid)]
    public partial class ContractController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select contractid from contract with (nolock)";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="contractid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{contractid}/document/browse")]
        [FwControllerMethod(Id: "nPsWS1jQzkmW", FwControllerActionTypes.ControlBrowse, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string contractid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(ContractDocumentLogic), "ContractId", contractid, string.Empty, string.Empty, request);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="browserequest"></param>
        /// <returns></returns>
        [HttpPost("{contractid}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "LHpsNXWdHyUW", FwControllerActionTypes.ControlBrowse, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string contractid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(ContractDocumentLogic), "ContractId", contractid, string.Empty, string.Empty, browserequest);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{contractid}/documents")]
        [FwControllerMethod(Id: "NUKHsjFGKhJF", FwControllerActionTypes.ControlBrowse, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string contractid, [FromQuery]ContractDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(ContractDocumentLogic), "ContractId", contractid, string.Empty, string.Empty, request);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{contractid}/document/{documentid}")]
        [FwControllerMethod(Id: "e4fM2RMGZYc0", FwControllerActionTypes.ControlBrowse, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<ContractDocumentLogic>> DocumentGetOneAsync([FromRoute]string contractid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<ContractDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof (ContractDocumentLogic), "ContractId", contractid, string.Empty, string.Empty, documentid);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{contractid}/document")]
        [FwControllerMethod(Id: "zrPgXoPp2qhB", FwControllerActionTypes.ControlNew, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<ContractDocumentLogic>> DocumentNewAsync([FromRoute]string contractid, [FromBody]ContractDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<ContractDocumentPostRequest, ContractDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "ContractId", contractid, string.Empty, string.Empty, model);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{contractid}/document/{documentid}")]
        [FwControllerMethod(Id: "s73mbH13s4bR", FwControllerActionTypes.ControlEdit, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<ContractDocumentLogic>> DocumentEditAsync([FromRoute]string contractid, [FromRoute]string documentid, [FromBody]ContractDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<ContractDocumentPutRequest, ContractDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "ContractId", contractid, string.Empty, string.Empty, documentid, model);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{contractid}/document/{documentid}")]
        [FwControllerMethod(Id: "ov0Tp4QWpYRM", FwControllerActionTypes.ControlDelete, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string contractid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<ContractDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "ContractId", contractid, string.Empty, string.Empty, documentid);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="pageno">The page number of the result set, starting from page 1.</param>
        /// <param name="pagesize">The number of records per page of the result set.</param>
        /// <returns></returns>
        [HttpGet("{contractid}/document/{documentid}/thumbnails")]
        [FwControllerMethod(Id: "MLprq6YeAh9a", FwControllerActionTypes.ControlBrowse, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string contractid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<ContractDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{contractid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "wdRthnjGA1Hj", FwControllerActionTypes.ControlBrowse, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string contractid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<ContractDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{contractid}/document/{documentid}/image")]
        [FwControllerMethod(Id: "uhcQT6DZZ4ez", FwControllerActionTypes.ControlEdit, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string contractid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<ContractDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to POST.</param>
        /// <returns></returns>
        [HttpPost("{contractid}/document/{documentid}/imageformupload")]
        [FwControllerMethod(Id: "Rhw5wN6CcmtY", FwControllerActionTypes.ControlEdit, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string contractid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<ContractDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{contractid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "JQQwPJDZX3jt", FwControllerActionTypes.ControlEdit, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string contractid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<ContractDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{contractid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "5FZtpneVOryI", FwControllerActionTypes.ControlBrowse, ParentId: "ZyFGQw4UylcX")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string contractid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<ContractDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{contractid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "gq1qJ898M4sg", FwControllerActionTypes.ControlEdit, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string contractid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<ContractDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to PUT.</param>
        /// <returns></returns>
        [HttpPut("{contractid}/document/{documentid}/fileformupload")]
        [FwControllerMethod(Id: "qKCfMw4g7HrF", FwControllerActionTypes.ControlEdit, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string contractid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<ContractDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="contractid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{contractid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "cSa2hpGTrsrE", FwControllerActionTypes.ControlEdit, ParentId: "ZyFGQw4UylcX")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string contractid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<ContractDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
