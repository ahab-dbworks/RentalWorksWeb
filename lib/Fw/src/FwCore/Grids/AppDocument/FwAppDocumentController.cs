using FwCore.Controllers;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;
using static FwCore.Controllers.FwDataController;

namespace FwCore.Grids.AppDocument
{
    public static class FwAppDocumentController
    {
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<FwJsonDataTable>> BrowseAsync(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, BrowseRequest browseRequest)
        {
            return await FwGridController.BrowseAsync(appConfig, userSession, modelState, logicType, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, BrowseRequest browseRequest)
        {
            return await FwGridController.ExportExcelXlsxFileAsync(appConfig, userSession, modelState, logicType, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<T>> GetOneAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, string appDocumentId)
        {
            return await FwGridController.GetAsync<T>(appConfig, userSession, modelState, logicType, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, appDocumentId);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<T>> NewAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, T model) where T : AppDocumentLogic
        {
            try
            {
                model.AppConfig = appConfig;
                model.UserSession = userSession;
                var date = DateTime.UtcNow;
                model.AttachDate = date.ToString("yyyy-MM-dd");
                model.AttachTime = date.ToString("hh:mm:ss");
                model.DateStamp = date.ToString("yyyy-MM-ddThh:mm:ssz");
                model.SetUniqueId1(uniqueId1FieldValue);
                model.SetUniqueId2(uniqueId2FieldValue);
                await model.SaveAppDocumentImageAsync(FwStandard.Grids.AppDocument.AppDocumentLogic.SaveModes.Insert);
                return model;
            }
            catch(Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<T>> EditAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, string documentId, T model) where T : AppDocumentLogic
        {
            try
            {
                if (model.DocumentId != documentId)
                {
                    modelState.AddModelError("AppDocumentId", "AppDocumentId in the url does not match the AppDocumentId in the model.");
                    return new BadRequestObjectResult(modelState);
                }
                model.AppConfig = appConfig;
                model.UserSession = userSession;
                var date = DateTime.UtcNow;
                model.AttachDate = date.ToString("yyyy-MM-dd");
                model.AttachTime = date.ToString("hh:mm:ss");
                model.DateStamp = date.ToString("yyyy-MM-ddThh:mm:ssz");
                await model.SaveAppDocumentImageAsync(FwStandard.Grids.AppDocument.AppDocumentLogic.SaveModes.Update);
                return model;
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<bool>> DeleteAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, string appDocumentId)
        {
            return await FwGridController.DoDeleteAsync<T>(appConfig, userSession, modelState, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, appDocumentId);
        }
        //------------------------------------------------------------------------------------ 
    }
}
