using FwCore.Api;
using FwCore.Controllers;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Threading.Tasks;
using static FwCore.Controllers.FwDataController;

namespace FwCore.Grids.AppDocument
{
    public static class FwAppDocumentController
    {
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<FwJsonDataTable>> BrowseAsync(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, 
            Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, BrowseRequest browseRequest)
        {
            return await FwGridController.BrowseAsync(appConfig, userSession, modelState, logicType, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<GetResponse<T>>> GetManyAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, GetRequest request)
        {
            if (!string.IsNullOrEmpty(uniqueId1FieldName))
            {
                request.filters[uniqueId1FieldName] = new GetManyRequestFilter(uniqueId1FieldName, "eq", uniqueId1FieldValue, false);
            }
            if (!string.IsNullOrEmpty(uniqueId2FieldName))
            {
                request.filters[uniqueId2FieldName] = new GetManyRequestFilter(uniqueId2FieldName, "eq", uniqueId2FieldValue, false);
            }
            return await FwGridController.GetManyAsync<T>(appConfig, userSession, modelState, logicType, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, request);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync(FwApplicationConfig appConfig, FwUserSession userSession, 
            ModelStateDictionary modelState, Type logicType, string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, 
            BrowseRequest browseRequest)
        {
            return await FwGridController.ExportExcelXlsxFileAsync(appConfig, userSession, modelState, logicType, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<T>> GetOneAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, Type logicType, 
            string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, string appDocumentId)
        {
            return await FwGridController.GetOneAsync<T>(appConfig, userSession, modelState, logicType, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, appDocumentId);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<TResponse>> NewAsync<TRequest, TResponse>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, 
            string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, TRequest model) 
            where TRequest : AppDocumentPostRequest where TResponse : AppDocumentLogic 
        {
            try
            {
                AppDocumentLogic document = AutoMapper.Mapper.Map<AppDocumentLogic>(model);
                document.AppConfig = appConfig;
                document.UserSession = userSession;
                var date = DateTime.UtcNow;
                document.AttachDate = date.ToString("yyyy-MM-dd");
                document.AttachTime = date.ToString("hh:mm:ss");
                document.DateStamp = date.ToString("yyyy-MM-ddThh:mm:ssz");
                document.SetUniqueId1(uniqueId1FieldValue);
                document.SetUniqueId2(uniqueId2FieldValue);
                return await FwGridController.DoNewAsync<TResponse>(appConfig, userSession, modelState, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, document);
                //await model.SaveAppDocumentImageAsync(FwStandard.Grids.AppDocument.AppDocumentLogic.SaveModes.Insert);
                //return model;
            }
            catch(Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<TResponse>> EditAsync<TRequest, TResponse>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, 
            string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, string documentId, TRequest model) 
            where TRequest : AppDocumentPutRequest where TResponse : AppDocumentLogic
        {
            try
            {
                if (model.DocumentId != documentId)
                {
                    modelState.AddModelError("AppDocumentId", "AppDocumentId in the url does not match the AppDocumentId in the model.");
                    return new BadRequestObjectResult(modelState);
                }
                AppDocumentLogic document = AutoMapper.Mapper.Map<AppDocumentLogic>(model);
                document.AppConfig = appConfig;
                document.UserSession = userSession;
                var date = DateTime.UtcNow;
                document.AttachDate = date.ToString("yyyy-MM-dd");
                document.AttachTime = date.ToString("hh:mm:ss");
                document.DateStamp = date.ToString("yyyy-MM-ddThh:mm:ssz");
                document.SetUniqueId1(uniqueId1FieldValue);
                document.SetUniqueId2(uniqueId2FieldValue);
                return await FwGridController.DoEditAsync<TResponse>(appConfig, userSession, modelState, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, document);
                //await document.SaveAppDocumentImageAsync(FwStandard.Grids.AppDocument.AppDocumentLogic.SaveModes.Update);
                //return model;
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<bool>> DeleteAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState, 
            string uniqueId1FieldName, string uniqueId1FieldValue, string uniqueId2FieldName, string uniqueId2FieldValue, string appDocumentId)
        {
            return await FwGridController.DoDeleteAsync<T>(appConfig, userSession, modelState, uniqueId1FieldName, uniqueId1FieldValue, uniqueId2FieldName, uniqueId2FieldValue, appDocumentId);
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<GetDocumentThumbnailsResponse>> GetThumbnailsAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, int pageNo, int pageSize)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                GetDocumentThumbnailsResponse response = await bl.GetThumbnailsAsync(validateAginstTable, validateAgainstField, pageNo, pageSize);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<GetDocumentImageResponse>> GetImageAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, string appImageId)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                GetDocumentImageResponse response = await bl.GetImageAsync(validateAginstTable, validateAgainstField, appImageId);
                if (response == null)
                {
                    return new NotFoundObjectResult("Image not found.");
                }
                else
                {
                    return new OkObjectResult(response);
                }
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<bool>> AttachImageFromDataUrlAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, PostDocumentImageRequest request)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                await bl.LoadAsync<T>();
                var response = await bl.AttachImageAsync(validateAginstTable, validateAgainstField, request.DataUrl);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<bool>> AttachImageFromFormAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, IFormFile file)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                await bl.LoadAsync<T>();
                byte[] image = null;
                using (Stream stream = file.OpenReadStream())
                {
                    image = new byte[stream.Length];
                    stream.Read(image, 0, image.Length);
                }
                var response = await bl.AttachImageAsync(validateAginstTable, validateAgainstField, image);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<bool>> DeleteImageAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, string appImageId)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                bool response = await bl.DeleteImageAsync(validateAginstTable, validateAgainstField, appImageId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<IActionResult> GetFileAsync<T>(HttpResponse response, FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId) where T: AppDocumentLogic
        {
            string filePath = string.Empty;
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                await bl.LoadAsync<T>();
                var documentFile = await bl.GetFileAsync(validateAginstTable, validateAgainstField);
                if (documentFile == null)
                {
                    return new NotFoundObjectResult("File not found.");
                }
                else
                {
                    string filename = bl.Description + "." + Guid.NewGuid().ToString().Replace("-", string.Empty) + "." + documentFile.File.Extension ;
                    filePath = Path.Combine(Environment.CurrentDirectory, "wwwroot/temp/downloads/" + filename);
                    using (var stream = File.OpenWrite(filePath))
                    {
                        await stream.WriteAsync(documentFile.File.Data, 0, documentFile.File.Data.Length);
                    }
                    response.Headers["Content-Disposition"] = "inline";
                    return new TempPhysicalFileResult(filePath, documentFile.File.ContentType)
                    {
                        FileDownloadName = filename
                    };
                    //return new FileContentResult(documentFile.File.Data, documentFile.File.ContentType)
                    //{
                    //    //FileDownloadName = filename
                    //};
                }
            }
            catch(Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<bool>> AttachFileFromDataUrlAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, PutDocumentFileRequest request)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                await bl.LoadAsync<T>();
                var response = await bl.AttachFileAsync(validateAginstTable, validateAgainstField, request.DataUrl, request.FileExtension);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<bool>> AttachFileFromUploadAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId, IFormFile file)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                await bl.LoadAsync<T>();
                byte[] fileData = null;
                using (Stream stream = file.OpenReadStream())
                {
                    fileData = new byte[stream.Length];
                    stream.Read(fileData, 0, fileData.Length);
                }
                string fileExtension = Path.GetExtension(file.FileName).ToUpper();
                if (fileExtension.StartsWith("."))
                {
                    fileExtension = fileExtension.Substring(1);
                }
                var response = await bl.AttachFileAsync(validateAginstTable, validateAgainstField, fileData, fileExtension);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<ActionResult<bool>> DeleteFileAsync<T>(FwApplicationConfig appConfig, FwUserSession userSession, ModelStateDictionary modelState,
            string validateAginstTable, string validateAgainstField, string appDocumentId)
        {
            try
            {
                AppDocumentLogic bl = (AppDocumentLogic)Activator.CreateInstance(typeof(T));
                bl.SetDependencies(appConfig, userSession);
                bl.DocumentId = appDocumentId;
                await bl.LoadAsync<T>();
                var response = await bl.DeleteFileAsync(validateAginstTable, validateAgainstField);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return FwGridController.GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
