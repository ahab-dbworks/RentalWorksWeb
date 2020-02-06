using FwCore.Controllers;
using FwCore.Grids.AppDocument;
using FwStandard.AppManager;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
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
        /// Get Documents
        /// </summary>
        /// <param name="DealId">Unique identifier</param>
        /// <param name="BrowseRequest"></param>
        /// <returns></returns>
        // POST api/v1/deal/{DealId}/document/browse 
        [HttpPost("{DealId}/document/browse")]
        [FwControllerMethod(Id: "Ar2pslw5CwkB", FwControllerActionTypes.ControlBrowse, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<FwJsonDataTable>> DocumentBrowseAsync([FromRoute]string DealId, [FromBody]BrowseRequest BrowseRequest)
        {
            try
            {
                return await FwAppDocumentController.BrowseAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(DealDocumentLogic), "DealId", DealId, string.Empty, string.Empty, BrowseRequest);
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
        /// <param name="DealId">Unique identifier</param>
        /// <param name="BrowseRequest"></param>
        /// <returns></returns>
        // POST api/v1/deal/{DealId}/document/exportexcelxlsx 
        [HttpPost("{DealId}/document/exportexcelxlsx")]
        [FwControllerMethod(Id: "v2tnr6Wq95UA", FwControllerActionTypes.ControlBrowse, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DocumentExportExcelXlsxFileAsync([FromRoute]string DealId, [FromBody]BrowseRequest BrowseRequest)
        {
            try
            {
                return await FwAppDocumentController.ExportExcelXlsxFileAsync(this.AppConfig, this.UserSession, this.ModelState, typeof(DealDocumentLogic), "DealId", DealId, string.Empty, string.Empty, BrowseRequest);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/deal/{DealId}/aka?pageno={pageno}&pagesize={pagesize}&sort={sort} 
        //[HttpGet("{DealId}/document")]
        //[FwControllerMethod(Id: "v56X9mvRvO62")]
        //public async Task<ActionResult<IEnumerable<AppDocumentLogic>>> DocumentGetManyAsync([FromRoute]string DealId, [FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await FwAppDocumentController.DoGetAsync<AppDocumentLogic>(pageno, pagesize, sort);
        //}
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get a Document
        /// </summary>
        /// <param name="DealId">Unique identifier</param>
        /// <param name="DocumentId">Unique identifier</param>
        /// <returns></returns>
        // GET api/v1/deal/{DealId}/document/{AppDocumentId}
        [HttpGet("{DealId}/document/{AppDocumentId}")]
        [FwControllerMethod(Id: "bgHn8Q47Fyzs", FwControllerActionTypes.ControlBrowse, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<DealDocumentLogic>> DocumentGetOneAsync([FromRoute]string DealId, [FromRoute]string DocumentId)
        {
            try
            {
                return await FwAppDocumentController.GetOneAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, typeof(DealDocumentLogic), "DealId", DealId, string.Empty, string.Empty, DocumentId);
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
        /// <param name="DealId">Unique identifier</param>
        /// <param name="Model"></param>
        /// <returns></returns>
        // POST api/v1/deal/{DealId}/document 
        [HttpPost("{DealId}/document")]
        [FwControllerMethod(Id: "WJL1tk6IeuV2", FwControllerActionTypes.ControlNew, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<DealDocumentLogic>> DocumentNewAsync([FromRoute]string DealId, [FromBody]DealDocumentPostRequest Model)
        {
            try
            {
                var logic = AutoMapper.Mapper.Map<DealDocumentLogic>(Model);
                return await FwAppDocumentController.NewAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "DealId", DealId, string.Empty, string.Empty, logic);
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
        /// <param name="DealId">Unique identifier</param>
        /// <param name="DocumentId">Unique identifier</param>
        /// <param name="Model"></param>
        /// <returns></returns>
        // POST api/v1/deal/{DealId}/document/{AppDocumentId} 
        [HttpPut("{DealId}/document/{AppDocumentId}")]
        [FwControllerMethod(Id: "rgq1YFxEGCnk", FwControllerActionTypes.ControlEdit, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<DealDocumentLogic>> DocumentEditAsync([FromRoute]string DealId, [FromRoute]string DocumentId, [FromBody]DealDocumentPutRequest Model)
        {
            try
            {
                var logic = AutoMapper.Mapper.Map<DealDocumentLogic>(Model);
                return await FwAppDocumentController.EditAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "DealId", DealId, string.Empty, string.Empty, DocumentId, logic);
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
        /// <param name="DealId">Unique identifier</param>
        /// <param name="DocumentId">Unique identifier</param>
        /// <returns></returns>
        // DELETE api/v1/deal/aka/A0000001 
        [HttpDelete("{DealId}/document/{AppDocumentId}")]
        [FwControllerMethod(Id: "Mj6UCo67CaAQ", FwControllerActionTypes.ControlDelete, ParentId: "5pVhTJtGXLVx")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string DealId, [FromRoute]string DocumentId)
        {
            try
            {
                return await FwAppDocumentController.DeleteAsync<DealDocumentLogic>(this.AppConfig, this.UserSession, this.ModelState, "DealId", DealId, string.Empty, string.Empty, DocumentId);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
