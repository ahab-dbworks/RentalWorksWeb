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

namespace WebApi.Modules.Inventory.Repair
{
    [FwControl(Caption: "Document", SecurityId: "JSUZfEv10RSr", ControlType: FwControlTypes.Grid)]
    public partial class RepairController
    {
        const string VALIDATE_UNIQUEID1_QUERY = "select masterid from master with (nolock) where availfor = 'R'";
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get documents.
        /// </summary>
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{inventoryid}/document/browse")]
        [FwControllerMethod(Id: "62z0EnDcN2nd", FwControllerActionTypes.ControlBrowse, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string inventoryid, [FromBody]BrowseRequest request)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(RepairDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, request);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="browserequest"></param>
        /// <returns></returns>
        [HttpPost("{inventoryid}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "HElENzVYnCEc", FwControllerActionTypes.ControlBrowse, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string inventoryid, [FromBody]BrowseRequest browserequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(RepairDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, browserequest);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{inventoryid}/documents")]
        [FwControllerMethod(Id: "dYiO9lySRGcY", FwControllerActionTypes.ControlBrowse, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<GetResponse<AppDocumentGetManyResponse>>> DocumentGetManyAsync([FromRoute]string inventoryid, [FromQuery]RepairDocumentGetRequest request)
        {
            try
            {
                return await FwAppDocumentController.GetManyAsync<AppDocumentGetManyResponse>(this.AppConfig, this.UserSession, this.ModelState, typeof(RepairDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, request);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{inventoryid}/document/{documentid}")]
        [FwControllerMethod(Id: "p8e6m5MSGYab", FwControllerActionTypes.ControlBrowse, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<RepairDocumentLogic>> DocumentGetOneAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<RepairDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof (RepairDocumentLogic), "InventoryId", inventoryid, string.Empty, string.Empty, documentid);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{inventoryid}/document")]
        [FwControllerMethod(Id: "xfrhGNBCZ7E4", FwControllerActionTypes.ControlNew, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<RepairDocumentLogic>> DocumentNewAsync([FromRoute]string inventoryid, [FromBody]RepairDocumentPostRequest model)
        {
            try
            {
                return await FwAppDocumentController.NewAsync<RepairDocumentPostRequest, RepairDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "InventoryId", inventoryid, string.Empty, string.Empty, model);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{inventoryid}/document/{documentid}")]
        [FwControllerMethod(Id: "4P5PX4Ma7SWG", FwControllerActionTypes.ControlEdit, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<RepairDocumentLogic>> DocumentEditAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromBody]RepairDocumentPutRequest model)
        {
            try
            {
                return await FwAppDocumentController.EditAsync<RepairDocumentPutRequest, RepairDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "InventoryId", inventoryid, string.Empty, string.Empty, documentid, model);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{inventoryid}/document/{documentid}")]
        [FwControllerMethod(Id: "et1wyVCGDVcR", FwControllerActionTypes.ControlDelete, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<RepairDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "InventoryId", inventoryid, string.Empty, string.Empty, documentid);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="pageno">The page number of the result set, starting from page 1.</param>
        /// <param name="pagesize">The number of records per page of the result set.</param>
        /// <returns></returns>
        [HttpGet("{inventoryid}/document/{documentid}/thumbnails")]
        [FwControllerMethod(Id: "qQEhRhkmdgu5", FwControllerActionTypes.ControlBrowse, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<GetDocumentThumbnailsResponse>> DocumentGetThumbnailsAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromQuery]int pageno, [FromQuery]int pagesize)
        {
            try
            {
                return await FwAppDocumentController.GetThumbnailsAsync<RepairDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, pageno, pagesize);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{inventoryid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "pkxE649Aw9RG", FwControllerActionTypes.ControlBrowse, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<GetDocumentImageResponse>> DocumentGetImageAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.GetImageAsync<RepairDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{inventoryid}/document/{documentid}/image")]
        [FwControllerMethod(Id: "8wAKekEKSEB2", FwControllerActionTypes.ControlEdit, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromDataUrlAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromBody]PostDocumentImageRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromDataUrlAsync<RepairDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to POST.</param>
        /// <returns></returns>
        [HttpPost("{inventoryid}/document/{documentid}/imageformupload")]
        [FwControllerMethod(Id: "As6avKslD7Q6", FwControllerActionTypes.ControlEdit, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<bool>> DocumentAttachImageFromFormAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachImageFromFormAsync<RepairDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="imageid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{inventoryid}/document/{documentid}/image/{imageid}")]
        [FwControllerMethod(Id: "UzzxXfpqyGJz", FwControllerActionTypes.ControlEdit, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromRoute]string imageid)
        {
            try
            {
                return await FwAppDocumentController.DeleteImageAsync<RepairDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, imageid);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{inventoryid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "1Z1aBKAqKneQ", FwControllerActionTypes.ControlBrowse, ParentId: "JSUZfEv10RSr")]
        public async Task<IActionResult> GetFileAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.GetFileAsync<RepairDocumentLogic>(Response, this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{inventoryid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "QiPBZCRsbp5v", FwControllerActionTypes.ControlEdit, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromDataUrlAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromBody]PutDocumentFileRequest request)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromDataUrlAsync<RepairDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, request);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <param name="file">The file to PUT.</param>
        /// <returns></returns>
        [HttpPut("{inventoryid}/document/{documentid}/fileformupload")]
        [FwControllerMethod(Id: "NRaqH3tmr5G4", FwControllerActionTypes.ControlEdit, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<bool>> DocumentAttachFileFromUploadAsync([FromRoute]string inventoryid, [FromRoute]string documentid, [FromForm]IFormFile file)
        {
            try
            {
                return await FwAppDocumentController.AttachFileFromUploadAsync<RepairDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid, file);
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
        /// <param name="inventoryid">Unique identifier</param>
        /// <param name="documentid">Unique identifier</param>
        /// <returns></returns>
        [HttpDelete("{inventoryid}/document/{documentid}/file")]
        [FwControllerMethod(Id: "fiE1SMa3MZeV", FwControllerActionTypes.ControlEdit, ParentId: "JSUZfEv10RSr")]
        public async Task<ActionResult<bool>> DeleteImageAsync([FromRoute]string inventoryid, [FromRoute]string documentid)
        {
            try
            {
                return await FwAppDocumentController.DeleteFileAsync<RepairDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, VALIDATE_UNIQUEID1_QUERY, documentid);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
