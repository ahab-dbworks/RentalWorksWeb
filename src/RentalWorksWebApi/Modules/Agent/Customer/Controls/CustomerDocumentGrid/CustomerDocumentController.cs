using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
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
            BrowseRequest.filterfields["CustomerId"] = CustomerId;
            return await DoBrowseAsync<CustomerDocumentLogic>(BrowseRequest);
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
            BrowseRequest.filterfields["CustomerId"] = CustomerId;
            return await DoExportExcelXlsxFileAsync<CustomerDocumentLogic>(BrowseRequest);
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
        /// <param name="AppDocumentId">Unique identifier</param>
        /// <returns></returns>
        // GET api/v1/customer/{CustomerId}/document/{AppDocumentId}
        [HttpGet("{CustomerId}/document/{AppDocumentId}")]
        [FwControllerMethod(Id: "Gl3OH5VIaWpU", FwControllerActionTypes.ControlBrowse, ParentId: "keTrJRIKRGwN")]
        public async Task<ActionResult<CustomerDocumentLogic>> DocumentGetOneAsync([FromRoute]string CustomerId, [FromRoute]string AppDocumentId)
        {
            return await DoGetAsync<CustomerDocumentLogic>(AppDocumentId);
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
        public async Task<ActionResult<CustomerDocumentLogic>> DocumentNewAsync([FromRoute]string CustomerId, [FromBody]CustomerDocumentLogic Model)
        {
            if (Model.UniqueId1 != CustomerId)
            {
                ModelState.AddModelError("UniqueId1", "CustomerId in the url does not match the Uniqueid1 in the model.");
                return new BadRequestObjectResult(ModelState);
            }
            return await DoNewAsync<CustomerDocumentLogic>(Model);
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Edit a Document
        /// </summary>
        /// <param name="CustomerId">Unique identifier</param>
        /// <param name="Model"></param>
        /// <returns></returns>
        // POST api/v1/customer/{CustomerId}/document/{AppDocumentId} 
        [HttpPut("{CustomerId}/document/{AppDocumentId}")]
        [FwControllerMethod(Id: "t3NG50C07BRf", FwControllerActionTypes.ControlEdit, ParentId: "keTrJRIKRGwN")]
        public async Task<ActionResult<CustomerDocumentLogic>> DocumentEditAsync([FromRoute]string CustomerId, [FromRoute]string AppDocumentId, [FromBody]CustomerDocumentLogic Model)
        {
            if (Model.UniqueId1 != CustomerId)
            {
                ModelState.AddModelError("UniqueId1", "CustomerId in the url does not match the Uniqueid1 in the model.");
                return new BadRequestObjectResult(ModelState);
            }
            if (Model.AppDocumentId != AppDocumentId)
            {
                ModelState.AddModelError("AppDocumentId", "AppDocumentId in the url does not match the AppDocumentId in the model.");
                return new BadRequestObjectResult(ModelState);
            }
            return await DoEditAsync<CustomerDocumentLogic>(Model);
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Delete a Document
        /// </summary>
        /// <param name="CustomerId">Unique identifier</param>
        /// <param name="AppDocumentId">Unique identifier</param>
        /// <returns></returns>
        // DELETE api/v1/restrictedperson/aka/A0000001 
        [HttpDelete("{CustomerId}/document/{AppDocumentId}")]
        [FwControllerMethod(Id: "Or34Egyu9U6T", FwControllerActionTypes.ControlDelete, ParentId: "keTrJRIKRGwN")]
        public async Task<ActionResult<bool>> DocumentDeleteAsync([FromRoute]string CustomerId, [FromRoute]string AppDocumentId)
        {
            return await DoDeleteAsync<CustomerDocumentLogic>(AppDocumentId);
        }
        //------------------------------------------------------------------------------------ 
        ///// <summary>
        ///// Test Option for AKA Grid
        ///// </summary>
        ///// <param name="PersonId">A unique identifier for RestrictedPerson</param>
        ///// <param name="AkaId">A unique identifier for Aka</param>
        ///// <returns></returns>
        //// POST api/v1/restrictedperson/aka/A0000001/testoption 
        //[HttpPost("{PersonId}/aka/{AkaId}/testoption")]
        //[FwControllerMethod(Id: "jvEXx7LuCRDM", ActionType: FwControllerActionTypes.ControlOption, ParentId: "keTrJRIKRGwN", Caption: "Test Option")]
        //public async Task<ActionResult> TestOptionAsync([FromRoute]string PersonId, [FromRoute]string AkaId)
        //{
        //    await Task.CompletedTask;
        //    throw new NotImplementedException();
        //}
        //------------------------------------------------------------------------------------ 
    }
}
