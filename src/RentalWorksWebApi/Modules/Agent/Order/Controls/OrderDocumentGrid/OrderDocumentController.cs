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

namespace WebApi.Modules.Agent.Order
{
    [FwControl(Caption: "Document", SecurityId: "O9wP1M9xrgEY", ControlType: FwControlTypes.Grid)]
    public partial class OrderController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select orderid from dealorder o with (nolock) where ordertype = 'O'";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="orderid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{orderid}/document/browse")]
        [FwControllerMethod(Id: "v0wM18cKaW9T", FwControllerActionTypes.ControlBrowse, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string orderid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(OrderDocumentLogic), "OrderId", orderid, string.Empty, string.Empty, request);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="browserequest"></param>
        /// <returns></returns>
        [HttpPost("{orderid}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "p1Qr6NVaGWDX", FwControllerActionTypes.ControlBrowse, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string orderid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(OrderDocumentLogic), "OrderId", orderid, string.Empty, string.Empty, browserequest);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{orderid}/documents")]
        [FwControllerMethod(Id: "Uco2R7mB57Ko", FwControllerActionTypes.ControlBrowse, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string orderid, [FromQuery]OrderDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(OrderDocumentLogic), "OrderId", orderid, string.Empty, string.Empty, request);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{orderid}/document/{documentid}")]
        [FwControllerMethod(Id: "SmER7942aLBa", FwControllerActionTypes.ControlBrowse, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<OrderDocumentLogic>> DocumentGetOneAsync([FromRoute]string orderid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<OrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(OrderDocumentLogic), "OrderId", orderid, string.Empty, string.Empty, documentid);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{orderid}/document")]
        [FwControllerMethod(Id: "5RBNvEsIxI1Y", FwControllerActionTypes.ControlNew, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<OrderDocumentLogic>> DocumentNewAsync([FromRoute]string orderid, [FromBody]OrderDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<OrderDocumentPostRequest, OrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "OrderId", orderid, string.Empty, string.Empty, model);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{orderid}/document/{documentid}")]
        [FwControllerMethod(Id: "Uk3dKHYrrDHU", FwControllerActionTypes.ControlEdit, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<OrderDocumentLogic>> DocumentEditAsync([FromRoute]string orderid, [FromRoute]string documentid, [FromBody]OrderDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<OrderDocumentPutRequest, OrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "OrderId", orderid, string.Empty, string.Empty, documentid, model);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{orderid}/document/{documentid}")]
        [FwControllerMethod(Id: "lecH2KWlKo69", FwControllerActionTypes.ControlDelete, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string orderid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<OrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "OrderId", orderid, string.Empty, string.Empty, documentid);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="pageno">The page number of the result set, starting from page 1.</param>
        /// <param name="pagesize">The number of records per page of the result set.</param>
        /// <returns></returns>
        [HttpGet("{orderid}/document/{documentid}/thumbnails")]
        [FwControllerMethod(Id: "GIgAOb1luO7W", FwControllerActionTypes.ControlBrowse, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string orderid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<OrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{orderid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "f1sCVkTClPEr", FwControllerActionTypes.ControlBrowse, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string orderid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<OrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{orderid}/document/{documentid}/image")]
        [FwControllerMethod(Id: "BUQVURpGo3Gc", FwControllerActionTypes.ControlEdit, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string orderid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<OrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to POST.</param>
        /// <returns></returns>
        [HttpPost("{orderid}/document/{documentid}/imageformupload")]
        [FwControllerMethod(Id: "AyLpMyAEgqBa", FwControllerActionTypes.ControlEdit, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string orderid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<OrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{orderid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "tuxAEE00d084", FwControllerActionTypes.ControlEdit, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string orderid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<OrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{orderid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "veyMAAInupcy", FwControllerActionTypes.ControlBrowse, ParentId: "O9wP1M9xrgEY")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string orderid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<OrderDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{orderid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "IE0Vp9lmuPCB", FwControllerActionTypes.ControlEdit, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string orderid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<OrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to PUT.</param>
        /// <returns></returns>
        [HttpPut("{orderid}/document/{documentid}/fileformupload")]
        [FwControllerMethod(Id: "vYcTdQ8JzR3H", FwControllerActionTypes.ControlEdit, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string orderid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<OrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="orderid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{orderid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "9XeQRoPds919", FwControllerActionTypes.ControlEdit, ParentId: "O9wP1M9xrgEY")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string orderid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<OrderDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
