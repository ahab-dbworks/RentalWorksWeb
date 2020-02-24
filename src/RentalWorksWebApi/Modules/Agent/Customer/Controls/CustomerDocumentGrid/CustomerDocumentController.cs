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

namespace WebApi.Modules.Agent.Customer
{
    [FwControl(Caption: "Document", SecurityId: "0zkYs0eRgG7E", ControlType: FwControlTypes.Grid)]
    public partial class CustomerController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select customerid from customer with (nolock)";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="customerid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{customerid}/document/browse")]
        [FwControllerMethod(Id: "28Wn7OEbpkMY", FwControllerActionTypes.ControlBrowse, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string customerid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(CustomerDocumentLogic), "CustomerId", customerid, string.Empty, string.Empty, request);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="browserequest"></param>
        /// <returns></returns>
        [HttpPost("{customerid}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "HhE0vIcNtjok", FwControllerActionTypes.ControlBrowse, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string customerid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(CustomerDocumentLogic), "CustomerId", customerid, string.Empty, string.Empty, browserequest);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{customerid}/documents")]
        [FwControllerMethod(Id: "uCE3XLVdPg7t", FwControllerActionTypes.ControlBrowse, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string customerid, [FromQuery]CustomerDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(CustomerDocumentLogic), "CustomerId", customerid, string.Empty, string.Empty, request);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{customerid}/document/{documentid}")]
        [FwControllerMethod(Id: "d1TCc6prY09S", FwControllerActionTypes.ControlBrowse, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<CustomerDocumentLogic>> DocumentGetOneAsync([FromRoute]string customerid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(CustomerDocumentLogic), "CustomerId", customerid, string.Empty, string.Empty, documentid);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{customerid}/document")]
        [FwControllerMethod(Id: "lUgfTfUSTM3F", FwControllerActionTypes.ControlNew, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<CustomerDocumentLogic>> DocumentNewAsync([FromRoute]string customerid, [FromBody]CustomerDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<CustomerDocumentPostRequest, CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "CustomerId", customerid, string.Empty, string.Empty, model);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{customerid}/document/{documentid}")]
        [FwControllerMethod(Id: "lKoNzIjAkkVD", FwControllerActionTypes.ControlEdit, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<CustomerDocumentLogic>> DocumentEditAsync([FromRoute]string customerid, [FromRoute]string documentid, [FromBody]CustomerDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<CustomerDocumentPutRequest, CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "CustomerId", customerid, string.Empty, string.Empty, documentid, model);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{customerid}/document/{documentid}")]
        [FwControllerMethod(Id: "XJKx37vDDVTd", FwControllerActionTypes.ControlDelete, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string customerid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "CustomerId", customerid, string.Empty, string.Empty, documentid);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="pageno">The page number of the result set, starting from page 1.</param>
        /// <param name="pagesize">The number of records per page of the result set.</param>
        /// <returns></returns>
        [HttpGet("{customerid}/document/{documentid}/thumbnails")]
        [FwControllerMethod(Id: "EQuKwoJB8ptC", FwControllerActionTypes.ControlBrowse, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string customerid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{customerid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "RL1qQIH98Nrk", FwControllerActionTypes.ControlBrowse, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string customerid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{customerid}/document/{documentid}/image")]
        [FwControllerMethod(Id: "Ey0WrAcy0klS", FwControllerActionTypes.ControlEdit, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string customerid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to POST.</param>
        /// <returns></returns>
        [HttpPost("{customerid}/document/{documentid}/imageformupload")]
        [FwControllerMethod(Id: "pIe7LvFJrhOs", FwControllerActionTypes.ControlEdit, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string customerid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{customerid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "feV4oUe1zlf8", FwControllerActionTypes.ControlEdit, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string customerid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{customerid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "xVlHn4MHCjbj", FwControllerActionTypes.ControlBrowse, ParentId: "0zkYs0eRgG7E")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string customerid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<CustomerDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{customerid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "EKyMjR3xSHQn", FwControllerActionTypes.ControlEdit, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string customerid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to PUT.</param>
        /// <returns></returns>
        [HttpPut("{customerid}/document/{documentid}/fileformupload")]
        [FwControllerMethod(Id: "OjkffstfCgJY", FwControllerActionTypes.ControlEdit, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string customerid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="customerid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{customerid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "m7xwFg07dMzM", FwControllerActionTypes.ControlEdit, ParentId: "0zkYs0eRgG7E")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string customerid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
