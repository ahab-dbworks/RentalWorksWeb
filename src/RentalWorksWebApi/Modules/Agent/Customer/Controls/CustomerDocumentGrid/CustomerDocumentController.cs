using FwCore.Controllers;
using FwCore.Grids.AppDocument;
using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Agent.Customer
{
    [FwControl(Caption: "Document", SecurityId: "keTrJRIKRGwN", ControlType: FwControlTypes.Grid)]
    public partial class CustomerController
    {
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get Documents
        /// </summary>
        /// <param name="CustomerId">Unique identifier</param>
        /// <param name="BrowseRequest"></param>
        /// <returns></returns>
        // POST api/v1/customer/{CustomerId}/document/browse 
        [HttpPost("{CustomerId}/document/browse")]
        [FwControllerMethod(Id: "8cdopH5BYqPz", FwControllerActionTypes.ControlBrowse, ParentId: "keTrJRIKRGwN")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string CustomerId, [FromBody]BrowseRequest BrowseRequest)
        {
            //BrowseRequest.filterfields["CustomerId"] = CustomerId;
            //return await DoBrowseAsync<CustomerDocumentLogic>(BrowseRequest);
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(CustomerDocumentLogic), "CustomerId", CustomerId, string.Empty, string.Empty, BrowseRequest);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Export Documents to Excel
        /// </summary>
        /// <param name="CustomerId">Unique identifier</param>
        /// <param name="BrowseRequest"></param>
        /// <returns></returns>
        // POST api/v1/customer/{customerid}/document/exportexcelxlsx 
        [HttpPost("{CustomerId}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "gpGNDR8XPjfL", FwControllerActionTypes.ControlBrowse, ParentId: "keTrJRIKRGwN")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string CustomerId, [FromBody]BrowseRequest BrowseRequest)
        {
            //BrowseRequest.filterfields["CustomerId"] = CustomerId;
            //return await DoExportExcelXlsxFileAsync<CustomerDocumentLogic>(BrowseRequest);
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(CustomerDocumentLogic), "CustomerId", CustomerId, string.Empty, string.Empty, BrowseRequest);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/customer/{CustomerId}/aka?pageno={pageno}&pagesize={pagesize}&sort={sort} 
        //[HttpGet("{PersonId}/aka")]
        //[FwControllerMethod(Id: "Lo5KKNcLsOAF2")]
        //public async Task<ActionResult<IEnumerable<AKALogic>>> AkaGetManyAsync([FromRoute]string PersonId, [FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<AKALogic>(pageno, pagesize, sort);
        //}
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get a Document
        /// </summary>
        /// <param name="CustomerId">Unique identifier</param>
        /// <param name="DocumentId">Unique identifier</param>
        /// <returns></returns>
        // GET api/v1/customer/{CustomerId}/document/{DocumentId}
        [HttpGet("{CustomerId}/document/{DocumentId}")]
        [FwControllerMethod(Id: "Gl3OH5VIaWpU", FwControllerActionTypes.ControlBrowse, ParentId: "keTrJRIKRGwN")]
        public async Task<ActionResult<CustomerDocumentLogic>> DocumentGetOneAsync([FromRoute]string CustomerId, [FromRoute]string DocumentId)
        {
            //return await DoGetAsync<CustomerDocumentLogic>(DocumentId);
            try
            {
                return await FwAppDocumentController.GetOneAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(CustomerDocumentLogic), "CustomerId", CustomerId, string.Empty, string.Empty, DocumentId);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Create a New Document
        /// </summary>
        /// <param name="CustomerId">Unique identifier</param>
        /// <param name="Model"></param>
        /// <returns></returns>
        // POST api/v1/customer/{CustomerId}/document 
        [HttpPost("{CustomerId}/document")]
        [FwControllerMethod(Id: "QTH4eyIyd5Bx", FwControllerActionTypes.ControlNew, ParentId: "keTrJRIKRGwN")]
        public async Task<ActionResult<CustomerDocumentLogic>> DocumentNewAsync([FromRoute]string CustomerId, [FromBody]CustomerDocumemntPostRequest Model)
        {
            //if (Model.CustomerId != CustomerId)
            //{
            //    ModelState.AddModelError("CustomerId", "CustomerId in the url does not match the CustomerId in the model.");
            //    return new BadRequestObjectResult(ModelState);
            //}
            //return await DoNewAsync<CustomerDocumentLogic>(Model);
            try
            {
                var logic = AutoMapper.Mapper.Map<CustomerDocumentLogic>(Model);
                return await FwAppDocumentController.NewAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "CustomerId", CustomerId, string.Empty, string.Empty, logic);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Edit a Document
        /// </summary>
        /// <param name="CustomerId">Unique identifier</param>
        /// <param name="DocumentId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        // POST api/v1/customer/{CustomerId}/document/{DocumentId} 
        [HttpPut("{CustomerId}/document/{DocumentId}")]
        [FwControllerMethod(Id: "t3NG50C07BRf", FwControllerActionTypes.ControlEdit, ParentId: "keTrJRIKRGwN")]
        public async Task<ActionResult<CustomerDocumentLogic>> DocumentEditAsync([FromRoute]string CustomerId, [FromRoute]string DocumentId, [FromBody]CustomerDocumentPutRequest Model)
        {
            //if (Model.CustomerId != CustomerId)
            //{
            //    ModelState.AddModelError("CustomerId", "CustomerId in the url does not match the CustomerId in the model.");
            //    return new BadRequestObjectResult(ModelState);
            //}
            //if (Model.DocumentId != DocumentId)
            //{
            //    ModelState.AddModelError("DocumentId", "DocumentId in the url does not match the DocumentId in the model.");
            //    return new BadRequestObjectResult(ModelState);
            //}
            //return await DoEditAsync<CustomerDocumentLogic>(Model);
            try
            {
                var logic = AutoMapper.Mapper.Map<CustomerDocumentLogic>(Model);
                return await FwAppDocumentController.EditAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "CustomerId", CustomerId, string.Empty, string.Empty, DocumentId, logic);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Delete a Document
        /// </summary>
        /// <param name="CustomerId">Unique identifier</param>
        /// <param name="DocumentId">Unique identifier</param>
        /// <returns></returns>
        // DELETE api/v1/restrictedperson/aka/A0000001 
        [HttpDelete("{CustomerId}/document/{DocumentId}")]
        [FwControllerMethod(Id: "Or34Egyu9U6T", FwControllerActionTypes.ControlDelete, ParentId: "keTrJRIKRGwN")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string CustomerId, [FromRoute]string DocumentId)
        {
            //return await DoDeleteAsync<CustomerDocumentLogic>(DocumentId);
            try
            {
                return await FwAppDocumentController.DeleteAsync<CustomerDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "CustomerId", CustomerId, string.Empty, string.Empty, DocumentId);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
