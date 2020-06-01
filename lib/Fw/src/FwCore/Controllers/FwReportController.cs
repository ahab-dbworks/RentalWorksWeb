using FwStandard.Models;
using FwStandard.Reporting;
using FwStandard.SqlServer;
using FwStandard.Data;
using FwStandard.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FwStandard.AppManager;
using static FwCore.Controllers.FwDataController;
using PuppeteerSharp;

namespace FwCore.Controllers
{
    public abstract class FwReportController : FwController
    {
        public FwReportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //---------------------------------------------------------------------------------------------
        protected Type loaderType = null;
        //---------------------------------------------------------------------------------------------
        protected Type logicType = null;
        //---------------------------------------------------------------------------------------------
        protected abstract string GetReportFileName(FwReportRenderRequest request);
        //---------------------------------------------------------------------------------------------
        protected abstract string GetReportFriendlyName();
        //---------------------------------------------------------------------------------------------
        protected async Task<ActionResult<FwReportRenderResponse>> DoRender(FwReportRenderRequest request)
        {
            FwReportRenderResponse response = new FwReportRenderResponse();
            string apiUrl = this.GetFullyQualifiedBaseUrl();
            Console.WriteLine($"apiUrl: {apiUrl}");
            string guid = Guid.NewGuid().ToString().Replace("-", string.Empty);
            //string baseFileName = $"{this.GetReportFileName()}{this.UserSession.WebUsersId}_{guid}";
            //string reportFileName = this.GetReportFileName(request).Replace(" ", "_").Replace("/", "_").Replace("-", "_");

            string reportFileName = this.GetReportFileName(request);
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                reportFileName = reportFileName.Replace(c, '_');
            }
            reportFileName = reportFileName.Replace(' ', '_').Replace('-', '_').Replace('+', '_').Replace('#', '_');

            string baseFileName = $"{reportFileName}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}";
            string htmlFileName = $"{baseFileName}.html";
            //string pathHtmlReport = Path.Combine(FwDownloadController.GetDownloadsDirectory(), htmlFileName);
            //string urlHtmlReport = $"{baseUrl}/Reports/{reportFileName}/index.html?nocache={Guid.NewGuid().ToString().Replace("-", "")}";
            string reportName = this.GetType().Name.Replace("Controller", "");
            string reportUrl = $"{apiUrl}/Reports/{reportName}/index.html?nocache={Guid.NewGuid().ToString().Replace("-", "")}";
            Console.WriteLine($"reportUrl: {reportUrl}");
            //string urlHtmlReport = $"{baseUrl}/temp/downloads/{htmlFileName}";
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            response.renderMode = request.renderMode;
            if (request.renderMode == "Html")
            {
                response.htmlReportUrl = reportUrl;
            }
            else if (request.renderMode == "Pdf" || request.renderMode == "Email")
            {
                //string pdfFileName = $"{baseFileName}.pdf";
                string pdfFileName = "";

                if ((string.IsNullOrEmpty(reportFileName)) || (reportFileName.Equals(reportName)))  // default
                {
                    pdfFileName = $"{baseFileName}.pdf";
                }
                else 
                { 
                    pdfFileName = $"{reportFileName}.pdf";
                }

                string guidDownloadPath = Path.Combine(FwDownloadController.GetDownloadsDirectory(), guid);
                System.IO.Directory.CreateDirectory(guidDownloadPath);

                //string pathPdfReport = Path.Combine(FwDownloadController.GetDownloadsDirectory(), pdfFileName);
                string pathPdfReport = Path.Combine(guidDownloadPath, pdfFileName);
                //response.pdfReportUrl = $"{baseUrl}/temp/downloads/{pdfFileName}";
                response.pdfReportUrl = $"{apiUrl}/temp/downloads/{guid}/{pdfFileName}";
                response.consoleOutput = await FwReport.GeneratePdfFromUrlAsync(apiUrl, reportUrl, pathPdfReport, authorizationHeader, request.parameters, GetPdfOptions());

                if (request.renderMode == "Email")
                {
                    if (String.IsNullOrEmpty(request.email.from)) this.ModelState.AddModelError("email.from", "E-mail From is required.");
                    if (String.IsNullOrEmpty(request.email.to)) this.ModelState.AddModelError("email.to", "E-mail To is required.");
                    if (this.ModelState.IsValid && System.IO.File.Exists(pathPdfReport))
                    {
                        if (request.email.from == "[me]")
                        {
                            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                            {
                                request.email.from = await FwSqlCommand.GetStringDataAsync(conn, this.AppConfig.DatabaseSettings.QueryTimeout, "webusersview", "webusersid", this.UserSession.WebUsersId, "email");
                            }
                        }
                        if (request.email.to == "[me]")
                        {
                            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                            {
                                request.email.to = await FwSqlCommand.GetStringDataAsync(conn, this.AppConfig.DatabaseSettings.QueryTimeout, "webusersview", "webusersid", this.UserSession.WebUsersId, "email");
                            }
                        }
                        if (request.email.subject == "[reportname]")
                        {
                            request.email.subject = GetReportFriendlyName();
                        }
                        string uniqueid = this.GetUniqueId(request);
                        await FwReport.EmailPdfAsync(
                            fromusersid: this.UserSession.UsersId,
                            uniqueid: uniqueid,
                            title: GetReportFriendlyName(),
                            from: request.email.from,
                            to: request.email.to,
                            cc: request.email.cc,
                            subject: request.email.subject,
                            body: request.email.body,
                            pdfPath: pathPdfReport,
                            appConfig: this.AppConfig);
                    }
                }
            }
            else if (request.renderMode == "EmailImage")
            {
                if (String.IsNullOrEmpty(request.email.from)) this.ModelState.AddModelError("email.from", "E-mail From is required.");
                if (String.IsNullOrEmpty(request.email.to)) this.ModelState.AddModelError("email.to", "E-mail To is required.");
                if (this.ModelState.IsValid)
                {
                    if (request.email.from == "[me]")
                    {
                        using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                        {
                            request.email.from = await FwSqlCommand.GetStringDataAsync(conn, this.AppConfig.DatabaseSettings.QueryTimeout, "webusersview", "webusersid", this.UserSession.WebUsersId, "email");
                        }
                    }
                    if (request.email.to == "[me]")
                    {
                        using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
                        {
                            request.email.to = await FwSqlCommand.GetStringDataAsync(conn, this.AppConfig.DatabaseSettings.QueryTimeout, "webusersview", "webusersid", this.UserSession.WebUsersId, "email");
                        }
                    }
                    if (request.email.subject == "[reportname]")
                    {
                        request.email.subject = GetReportFriendlyName();
                    }
                    string uniqueid = this.GetUniqueId(request);
                    ViewPortOptions viewPortOptions = new ViewPortOptions();
                    viewPortOptions.Width = request.emailImageOptions.Width;
                    viewPortOptions.Height = request.emailImageOptions.Height;
                    viewPortOptions.DeviceScaleFactor = 1;
                    ScreenshotOptions screenshotOptions = new ScreenshotOptions();
                    screenshotOptions.Type = ScreenshotType.Png;
                    await FwReport.EmailImageAsync(
                        apiUrl: apiUrl,
                        reportUrl: reportUrl,
                        authorizationHeader: authorizationHeader,
                        parameters: request.parameters,
                        fromusersid: this.UserSession.UsersId,
                        uniqueid: uniqueid,
                        title: GetReportFriendlyName(),
                        from: request.email.from,
                        to: request.email.to,
                        cc: request.email.cc,
                        subject: request.email.subject,
                        bodyHeader: string.Empty,
                        bodyFooter: request.email.body,
                        appConfig: this.AppConfig,
                        viewPortOptions: viewPortOptions,
                        screenshotOptions: screenshotOptions);
                }
            }
            if (!this.ModelState.IsValid) return new BadRequestObjectResult(this.ModelState);
            return new OkObjectResult(response);
        }

        //------------------------------------------------------------------------------------ 

        protected virtual async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> DoExportExcelXlsxFileAsync(FwJsonDataTable dt, string worksheetName = "", bool includeIdColumns = true)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (string.IsNullOrEmpty(worksheetName))
                {
                    worksheetName = GetReportFriendlyName();
                }

                string strippedWorksheetName = new string(worksheetName.Where(c => char.IsLetterOrDigit(c)).ToArray());
                string downloadFileName = $"{strippedWorksheetName}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}";
                string filename = $"{this.UserSession.WebUsersId}_{strippedWorksheetName}_{Guid.NewGuid().ToString().Replace("-", string.Empty)}_xlsx";
                string directory = FwDownloadController.GetDownloadsDirectory();
                string path = Path.Combine(directory, filename);

                // Delete any existing excel files belonginng to this user
                FwDownloadController.DeleteCurrentWebUserDownloads(this.UserSession.WebUsersId);

                if (!includeIdColumns)
                {
                    foreach (FwJsonDataTableColumn col in dt.Columns)
                    {
                        string dataField = col.DataField.ToUpper();
                        if ((!includeIdColumns) && (dataField.EndsWith("ID") || dataField.EndsWith("KEY")))
                        {
                            col.IsVisible = false;
                        }
                    }
                }

                dt.ToExcelXlsxFile(worksheetName, path);
                DoExportExcelXlsxExportFileAsyncResult result = new DoExportExcelXlsxExportFileAsyncResult();
                result.downloadUrl = $"api/v1/download/{filename}?downloadasfilename={downloadFileName}.xlsx";
                await Task.CompletedTask; // get rid of the no async call warning
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }

        //------------------------------------------------------------------------------------ 

        [HttpGet("emptyobject")]
        [FwControllerMethod("", FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public ActionResult<FwJsonDataTable> GetEmptyObject()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Type type = loaderType;
                FwReportLoader l = (FwReportLoader)Activator.CreateInstance(type);
                l.SetDependencies(AppConfig, UserSession);
                return new OkObjectResult(l);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        protected ObjectResult GetApiExceptionResult(Exception ex)
        {
            FwApiException jsonException = new FwApiException();
            jsonException.StatusCode = StatusCodes.Status500InternalServerError;
            jsonException.Message = ex.Message;
            if (ex.InnerException != null)
            {
                jsonException.Message += $"\n\nInnerException: \n{ex.InnerException.Message}";
            }
            jsonException.StackTrace = ex.StackTrace;
            return StatusCode(jsonException.StatusCode, jsonException);
        }
        //------------------------------------------------------------------------------------
        protected FwBusinessLogic CreateBusinessLogic(Type type, FwApplicationConfig appConfig, FwUserSession userSession)
        {
            FwBusinessLogic bl = (FwBusinessLogic)Activator.CreateInstance(type);
            bl.AppConfig = appConfig;
            bl.UserSession = userSession;
            bl.SetDependencies(appConfig, userSession);
            return bl;
        }
        //-------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<FwJsonDataTable>> DoBrowseAsync(BrowseRequest browseRequest, Type type = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (type == null)
                {
                    type = logicType;
                }

                FwBusinessLogic l = CreateBusinessLogic(type, this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.BrowseAsync(browseRequest);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<FwJsonDataTable>> DoBrowseAsync<T>(BrowseRequest browseRequest)
        {
            return await this.DoBrowseAsync(browseRequest, typeof(T));
        }
        //------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// When a PDF is emailed, it's logged in a table an accesible by the uniqueid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract string GetUniqueId(FwReportRenderRequest request);

        protected abstract PuppeteerSharp.PdfOptions GetPdfOptions();

        //[HttpPost("preview")]
        //public virtual void Preview([FromBody]PreviewRequest request)
        //{
        //    response.downloadurl = renderHtml(request.templates.stylesheet, request.templates.header, request.templates.body, request.templates.footer, getRequestPrintOptions());
        //}
        ////---------------------------------------------------------------------------------------------
        //public virtual void DownloadPdf()
        //{
        //    DownloadableResult renderPdfResult;

        //    renderPdfResult      = renderPdf(request.templates.stylesheet, request.templates.header, request.templates.body, request.templates.footer, getRequestPrintOptions());
        //    response.downloadurl = renderPdfResult.DownloadUrl;
        //}
        ////---------------------------------------------------------------------------------------------
        //public virtual void DownloadExcel()
        //{
        //    PrintOptions printOptions = getRequestPrintOptions();
        //    string filename, path;

        //    ExcelDownloadResult excelPackageResult = GetExcelDownload();
        //    if (excelPackageResult != null)
        //    {
        //        if (excelPackageResult.Filename == null)
        //        {
        //            excelPackageResult.Filename = getReportFileName();
        //        }
        //        DownloadableResult result = new DownloadableResult(); ;
        //        filename = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".xlsx";
        //        path = HttpContext.Current.Server.MapPath("~/App_Data/Temp/Downloads/" + filename);
        //        result.DownloadUrl = VirtualPathUtility.ToAbsolute("~/fwdownload/" + getReportFileName() + ".pdf?filename=" + HttpUtility.UrlEncode(filename) + "&saveas=" + HttpUtility.UrlEncode(excelPackageResult.Filename + ".xlsx") + "&asattachment=" + printOptions.DownloadAsAttachment.ToString().ToLower());
        //        result.Path = path;
        //        excelPackageResult.ExcelPackage.File = new FileInfo(path);
        //        excelPackageResult.ExcelPackage.Save();
        //        response.downloadurl = result.DownloadUrl;
        //    }
        //    else
        //    {
        //        //throw new Exception("Excel download is not available for this report.  Contact Database Works if you need this functionality.");
        //        throw new Exception("Excel download is not available for this report."); //justin 05/30/2018 removed text per Terry
        //    }
        //}
        ////---------------------------------------------------------------------------------------------
        //public class ExcelDownloadResult
        //{
        //    public string Filename { get; set; } = null;
        //    public ExcelPackage ExcelPackage { get; set; } = new ExcelPackage();
        //}
        //public virtual ExcelDownloadResult GetExcelDownload()
        //{
        //    return null;
        //}
        ////---------------------------------------------------------------------------------------------
        ////public virtual void SendHtmlEmail()
        ////{
        ////    RenderPdfResult renderPdfResult;
        ////    string from, to, cc, subject, body;
        ////    FwSqlData.GetEmailReportControlResponse emailreportcontrol;

        ////    emailreportcontrol = FwSqlData.GetEmailReportControl(GetApplicationSqlConnection());
        ////    from    = "";
        ////    to      = request.email.to;
        ////    cc      = request.email.cc;
        ////    subject = getReportName();
        ////    body    = renderHtml(request.templates.stylesheet, request.templates.header, request.templates.body, request.templates.footer, getRequestPrintOptions());
        ////    renderPdfResult = renderPdf(request.templates.stylesheet, request.templates.header, request.templates.body, request.templates.footer, getRequestPrintOptions());
        ////    FwEmailService.SendEmail(FwSqlConnection.AppConnection, from, to, cc, subject, body, getReportName() + ".pdf", renderPdfResult.Path);
        ////}
        ////---------------------------------------------------------------------------------------------
        //public virtual void SendPdfEmail()
        //{
        //    DownloadableResult renderPdfResult;
        //    string from, to, cc, subject, body;
        //    FwSqlData.GetEmailReportControlResponse emailreportcontrol;
        //    dynamic webuser;

        //    emailreportcontrol = FwSqlData.GetEmailReportControl(GetApplicationSqlConnection());
        //    webuser = FwSqlData.GetWebUsersView(GetApplicationSqlConnection(), session.security.webUser.webusersid);
        //    from = webuser.email;
        //    to   = request.email.to;
        //    if (to == "[me]") to = webuser.email;
        //    cc      = request.email.cc;
        //    subject = request.email.subject;
        //    if (subject == "[reportname]") subject = getReportName();
        //    body    = request.email.body;
        //    renderPdfResult = renderPdf(request.templates.stylesheet, request.templates.header, request.templates.body, request.templates.footer, getRequestPrintOptions());
        //    FwEmailService.SendEmail(FwSqlConnection.AppConnection, from, to, cc, subject, body, true, getReportName() + ".pdf", renderPdfResult.Path);
        //}
        ////---------------------------------------------------------------------------------------------
        //public void GetFromEmail()
        //{
        //    response.fromemail = FwSqlData.GetWebUsersView(GetApplicationSqlConnection(), session.security.webUser.webusersid).email;
        //}
        ////---------------------------------------------------------------------------------------------
        //public void GetEmailByWebUsersId()
        //{
        //    const string METHOD_NAME = "GetEmailByWebUsersId";
        //    string[] webusersids, toemails;
        //    List<string> emails;
        //    dynamic webusersview;
        //    StringBuilder sb;
        //    FwSqlConnection conn;
        //    string emailto;


        //    FwValidate.TestPropertyDefined(METHOD_NAME, request, "webusersids");
        //    FwValidate.TestPropertyDefined(METHOD_NAME, request, "to");
        //    conn = GetApplicationSqlConnection();
        //    webusersids = ((string)request.webusersids).Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
        //    toemails    = ((string)request.to).Split(new char[]{',', ';'}, StringSplitOptions.RemoveEmptyEntries);
        //    emails      = new List<string>();
        //    foreach (string email in toemails)
        //    {
        //        if (!emails.Contains(email)) 
        //        {
        //            emails.Add(email);
        //        }
        //    }
        //    foreach (string webusersidencrypted in webusersids)
        //    {
        //        string webusersid, email;
        //        webusersid = FwCryptography.AjaxDecrypt(webusersidencrypted);
        //        webusersview = FwSqlData.GetWebUsersView(conn, webusersid);
        //        email = webusersview.email;
        //        if (!string.IsNullOrWhiteSpace(email))
        //        {
        //            if (!emails.Contains(email)) 
        //            {
        //                emails.Add(email);
        //            }
        //        }
        //    }
        //    sb = new StringBuilder();
        //    for (int i = 0; i < emails.Count; i++)
        //    {
        //        if (i > 0) sb.Append(";");
        //        sb.Append(emails[i]);
        //    }
        //    emailto = sb.ToString();
        //    response.emailto = emailto;
        //}
        ////---------------------------------------------------------------------------------------------
        //public virtual void GetData()
        //{

        //}
        ////---------------------------------------------------------------------------------------------
        //public void GetDefaultPrintOptions()
        //{
        //    response.printoptions = getDefaultPrintOptions();
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual PrintOptions getDefaultPrintOptions()
        //{
        //    return new PrintOptions(PrintOptions.PrintLayout.Landscape);
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual string getReportName()
        //{
        //    return "Untitled Report";
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual string getReportFileName()
        //{
        //    String filename = new String(getReportName()
        //        .Where(ch => Char.IsLetterOrDigit(ch))
        //        .ToArray());
        //    return filename;
        //}
        ////---------------------------------------------------------------------------------------------
        //private PrintOptions getRequestPrintOptions()
        //{
        //    PrintOptions printOptions;

        //    printOptions = new PrintOptions(PrintOptions.PrintLayout.Landscape);
        //    printOptions.PageWidth    = FwConvert.ToFloat(request.parameters.pagewidth);
        //    printOptions.PageHeight   = FwConvert.ToFloat(request.parameters.pageheight);
        //    printOptions.MarginTop    = FwConvert.ToFloat(request.parameters.margintop);
        //    printOptions.MarginRight  = FwConvert.ToFloat(request.parameters.marginright);
        //    printOptions.MarginBottom = FwConvert.ToFloat(request.parameters.marginbottom);
        //    printOptions.MarginLeft   = FwConvert.ToFloat(request.parameters.marginleft);
        //    printOptions.HeaderHeight = FwConvert.ToFloat(request.parameters.headerheight);
        //    printOptions.FooterHeight = FwConvert.ToFloat(request.parameters.footerheight);
        //    if (FwValidate.IsPropertyDefined(request.parameters, "downloadasattachment"))
        //    {
        //        printOptions.DownloadAsAttachment = request.parameters.downloadasattachment;
        //    }

        //    return printOptions;
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual string renderCommonFields(string template, PrintOptions printOptions)
        //{
        //    StringBuilder sb;
        //    string result, dataUrl;
        //    FwControl control;

        //    control = FwSqlData.GetControl(GetApplicationSqlConnection());
        //    sb = new StringBuilder(template);
        //    sb.Replace("[report]", getReportName());
        //    sb.Replace("[company]", control.Company);
        //    sb.Replace("[system]", control.System);
        //    if (!File.Exists(HttpContext.Current.Server.MapPath("~/App_Data/Client/ReportLogo.png"))) {
        //        throw new Exception("Missing Report Logo at: " + HttpContext.Current.Server.MapPath("~/App_Data/Client/ReportLogo.png") + ". Please use a 300-600 DPI image to ensure a high quality result (png is recommended). The print width of the image will determine the size on the report.  When saving images from Photoshop, make sure to use Save As and not Save for Web, which will lower the DPI.");
        //    }
        //    Image image = Image.FromFile(HttpContext.Current.Server.MapPath("~/App_Data/Client/ReportLogo.png"));
        //    float printwidth = (image.Width / image.HorizontalResolution);
        //    dataUrl = GetDataURL(HttpContext.Current.Server.MapPath("~/App_Data/Client/ReportLogo.png"));
        //    sb.Replace("[logo]", "<img class=\"clientlogo\" src=\"" + dataUrl + "\" style=\"width:" + printwidth + "in;\" />");
        //    sb.Replace("[datetime]", FwConvert.ToUSShortDateTime(DateTime.Now));
        //    result = sb.ToString();

        //    return result;
        //}
        ////---------------------------------------------------------------------------------------------
        //public static string GetDataURL(string imgFile)
        //{
        //    return "data:image/" 
        //                + Path.GetExtension(imgFile).Replace(".","")
        //                + ";base64," 
        //                + Convert.ToBase64String(File.ReadAllBytes(imgFile));
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual string renderStyleSheetFields(string template, PrintOptions printOptions)
        //{
        //    StringBuilder sb;
        //    string result;

        //    sb = new StringBuilder(template);
        //    sb.Replace("[applicationstylesheet]",       File.ReadAllText(HttpContext.Current.Server.MapPath("~/source/Reports/Application/StyleSheet.css")));
        //    result = renderCommonFields(sb.ToString(), printOptions);

        //    return result;
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual string renderHeaderFields(string template, PrintOptions printOptions)
        //{
        //    StringBuilder sb;
        //    string result;

        //    sb = new StringBuilder(template);
        //    sb.Replace("[applicationlandscapeheader]", File.ReadAllText(HttpContext.Current.Server.MapPath("~/source/Reports/Application/LandscapeHeader.htm")));
        //    sb.Replace("[applicationportraitheader]",  File.ReadAllText(HttpContext.Current.Server.MapPath("~/source/Reports/Application/PortraitHeader.htm")));
        //    result = renderCommonFields(sb.ToString(), printOptions);

        //    return result;
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual string renderFooterFields(string template, PrintOptions printOptions)
        //{
        //    StringBuilder sb;
        //    string result;

        //    sb = new StringBuilder(template);
        //    sb.Replace("[applicationlandscapefooter]", File.ReadAllText(HttpContext.Current.Server.MapPath("~/source/Reports/Application/LandscapeFooter.htm")));
        //    sb.Replace("[applicationportraitfooter]",  File.ReadAllText(HttpContext.Current.Server.MapPath("~/source/Reports/Application/PortraitFooter.htm")));
        //    result = renderCommonFields(sb.ToString(), printOptions);

        //    return result;
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual string renderHtml(string styletemplate, string headertemplate, string bodytemplate, string footertemplate, PrintOptions printOptions)
        //{
        //    StringBuilder sb;
        //    string html, bodywidth, filename, path, downloadurl;

        //    bodywidth      = (printOptions.BodyWidth * 96).ToString();
        //    styletemplate  = renderStyleSheetFields(styletemplate, printOptions);
        //    headertemplate = renderHeaderFields(headertemplate, printOptions);
        //    footertemplate = renderFooterFields(footertemplate, printOptions);
        //    bodytemplate   = renderCommonFields(bodytemplate, printOptions);
        //    sb = new StringBuilder();
        //    sb.Append("<!DOCTYPE HTML>");
        //    sb.Append("<html class=\"renderhtml\">");
        //    sb.Append("<head>");
        //    sb.Append("<meta id=\"metaFormatDetection\" name=\"format-detection\" content=\"telephone=no\">");
        //    sb.AppendFormat("<title>{0}</title>", getReportName());
        //    sb.Append("<style>");
        //    sb.Append(renderStyleSheet(styletemplate, printOptions));
        //    sb.Append("</style>");
        //    sb.Append("</head>");
        //    sb.Append("<body>");
        //    sb.AppendFormat("<div style=\"width:{0}px;\">", bodywidth);
        //    if (printOptions.HeaderHeight > 0)
        //    {
        //        sb.Append(renderHeaderHtml(string.Empty, headertemplate, printOptions));
        //    }
        //    sb.Append(renderBodyHtml(string.Empty, bodytemplate, printOptions));
        //    if (printOptions.FooterHeight > 0)
        //    {
        //        sb.Append(renderFooterHtml(string.Empty, footertemplate, printOptions));
        //    }
        //    sb.Append("</div>");
        //    sb.Append("</body>");
        //    sb.Append("</html>");
        //    html = sb.ToString();

        //    filename    = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".html";
        //    path        = HttpContext.Current.Server.MapPath("~/App_Data/Temp/Downloads/" + filename);
        //    File.WriteAllText(path, html);
        //    downloadurl = VirtualPathUtility.ToAbsolute("~/fwdownload/" + getReportFileName() + ".html?filename=" + HttpUtility.UrlEncode(filename) + "&saveas=" + HttpUtility.UrlEncode(getReportFileName() + ".html") + "&asattachment=false");

        //    return downloadurl;
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual string renderStyleSheet(string styletemplate, PrintOptions printOptions)
        //{
        //    StringBuilder sb;
        //    string html;

        //    sb = new StringBuilder();
        //    if (!string.IsNullOrEmpty(styletemplate))
        //    {
        //        sb.Append("<style>");
        //        sb.Append(styletemplate);
        //        sb.Append("</style>");
        //    }
        //    html = sb.ToString();

        //    return html;
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual string renderHeaderHtml(string styletemplate, string headertemplate, PrintOptions printOptions)
        //{
        //    StringBuilder sb;
        //    string html;

        //    sb = new StringBuilder();
        //    if (!string.IsNullOrEmpty(styletemplate))
        //    {
        //        sb.Append("<style>");
        //        sb.Append(styletemplate);
        //        sb.Append("</style>");
        //    }
        //    sb.Append(headertemplate);
        //    html = sb.ToString();

        //    return html;
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        //{
        //    StringBuilder sb;
        //    string html;

        //    sb = new StringBuilder();
        //    if (!string.IsNullOrEmpty(styletemplate))
        //    {
        //        sb.Append("<style>");
        //        sb.Append(styletemplate);
        //        sb.Append("</style>");
        //    }
        //    sb.Append(bodytemplate);
        //    html = sb.ToString();

        //    return html;
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual string renderFooterHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        //{
        //    StringBuilder sb;
        //    string html;

        //    sb = new StringBuilder();
        //    if (!string.IsNullOrEmpty(styletemplate))
        //    {
        //        sb.Append("<style>");
        //        sb.Append(styletemplate);
        //        sb.Append("</style>");
        //    }
        //    sb.Append(bodytemplate);
        //    html = sb.ToString();

        //    return html;
        //}
        ////---------------------------------------------------------------------------------------------
        //public class DownloadableResult
        //{
        //    public string DownloadUrl { get; set; }
        //    public string Path { get; set; }
        //}
        ////---------------------------------------------------------------------------------------------
        //protected virtual DownloadableResult renderPdf(string styletemplate, string headertemplate, string bodytemplate, string footertemplate, PrintOptions printOptions)
        //{
        //    string headerHtml, bodyHtml, footerHtml, filename, path, bodyWidth, renderedstyletemplate, renderedheadertemplate,
        //        rendereredbodytemplate, renderedfootertemplate;
        //    StringBuilder sbHeaderHtml, sbBodyHtml, sbFooterHtml;
        //    DownloadableResult renderPdfResult;
        //    //PdfDocument pdfdoc;
        //    //HtmlToPdfResult htmltopdfresult;

        //    styletemplate          = renderStyleSheetFields(styletemplate, printOptions);
        //    headertemplate         = renderHeaderFields(headertemplate, printOptions);
        //    footertemplate         = renderFooterFields(footertemplate, printOptions);
        //    bodytemplate           = renderCommonFields(bodytemplate, printOptions);
        //    renderPdfResult        = new DownloadableResult();
        //    bodyWidth              = (printOptions.BodyWidth * 96).ToString();
        //    renderedstyletemplate  = renderStyleSheet(styletemplate, printOptions);
        //    renderedheadertemplate = renderHeaderHtml(string.Empty, headertemplate, printOptions);
        //    rendereredbodytemplate = renderBodyHtml(string.Empty, bodytemplate, printOptions);
        //    renderedfootertemplate = renderFooterHtml(string.Empty, footertemplate, printOptions);

        //    sbHeaderHtml = new StringBuilder();
        //    sbHeaderHtml.Append(renderedstyletemplate);
        //    sbHeaderHtml.AppendFormat("<div class=\"renderpdf\" style=\"width:{0}px;overflow:hidden;\">", bodyWidth);
        //    sbHeaderHtml.Append(renderedheadertemplate);
        //    sbHeaderHtml.Append("</div>");
        //    headerHtml = sbHeaderHtml.ToString();

        //    sbBodyHtml = new StringBuilder();
        //    sbBodyHtml.Append(renderedstyletemplate);
        //    sbHeaderHtml.AppendFormat("<div class=\"renderpdf\" style=\"width:{0}px;overflow:hidden;\">", bodyWidth);
        //    sbBodyHtml.Append(rendereredbodytemplate);
        //    sbBodyHtml.Append("</div>");
        //    bodyHtml = sbBodyHtml.ToString();

        //    sbFooterHtml = new StringBuilder();
        //    sbFooterHtml.Append(renderedstyletemplate);
        //    sbFooterHtml.AppendFormat("<div class=\"renderpdf\" style=\"width:{0}px;overflow:hidden;\">", bodyWidth);
        //    sbFooterHtml.Append(renderedfootertemplate);
        //    sbFooterHtml.Append("</div>");
        //    footerHtml = sbFooterHtml.ToString();

        //    HtmlToPdf.Options.BaseUrl                    = FwFunc.GetRequestBaseUrl();
        //    HtmlToPdf.Options.AutoAdjustForDPI           = false;
        //    HtmlToPdf.Options.RepeatTableHeaderAndFooter = true;
        //    HtmlToPdf.Options.JpegQualityLevel = 100; //.PreserveHighResImages      = true;
        //    HtmlToPdf.Options.PageSize                   = printOptions.GetPageSize();
        //    HtmlToPdf.Options.OutputArea                 = printOptions.GetOutputArea();
        //    HtmlToPdf.Options.AutoFitX                   = HtmlToPdfAutoFitMode.None;
        //    HtmlToPdf.Options.AutoFitY                   = HtmlToPdfAutoFitMode.None;
        //    if (printOptions.HeaderHeight > 0)
        //    {
        //        HtmlToPdf.Options.HeaderHtmlPosition         = printOptions.GetHtmlHeaderPosition();
        //        HtmlToPdf.Options.HeaderHtmlFormat           = headerHtml;
        //    }
        //    if (printOptions.FooterHeight > 0)
        //    {
        //        HtmlToPdf.Options.FooterHtmlPosition         = printOptions.GetHtmlFooterPosition();
        //        HtmlToPdf.Options.FooterHtmlFormat           = footerHtml;
        //    }
        //    filename = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".pdf";
        //    path     = HttpContext.Current.Server.MapPath("~/App_Data/Temp/Downloads/" + filename);
        //    HtmlToPdf.ConvertHtml(bodyHtml, path);
        //    //pdfdoc = new PdfDocument();
        //    //htmltopdfresult = HtmlToPdf.ConvertHtml(bodyHtml, pdfdoc);
        //    //pdfdoc.Save(path);
        //    renderPdfResult.DownloadUrl = VirtualPathUtility.ToAbsolute("~/fwdownload/" + getReportFileName() + ".pdf?filename=" + HttpUtility.UrlEncode(filename) + "&saveas=" + HttpUtility.UrlEncode(getReportFileName() + ".pdf") + "&asattachment=" + printOptions.DownloadAsAttachment.ToString().ToLower());
        //    renderPdfResult.Path        = path;

        //    return renderPdfResult;
        //}
        ////---------------------------------------------------------------------------------------------
        //protected string applyFieldsToTemplate(string template, Dictionary<string, string> fields)
        //{
        //    string result;
        //    result = Mustache.Render.StringToString(template, fields);
        //    return result;
        //}
        ////---------------------------------------------------------------------------------------------
        //protected string applyTableToTemplate(string template, string field, FwJsonDataTable dt)
        //{
        //    string result;
        //    Dictionary<string, List<Dictionary<string, object>>> objs;
        //    List<Dictionary<string,object>> table;
        //    Dictionary<string, object> row;

        //    objs          = new Dictionary<string,List<Dictionary<string,object>>>();
        //    table         = new List<Dictionary<string,object>>();
        //    objs[field] = table;
        //    for(int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
        //    {
        //        row = new Dictionary<string,object>();
        //        for(int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
        //        {
        //            row[dt.ColumnNameByIndex[colIndex]] = dt.Rows[rowIndex][colIndex];
        //        }
        //        table.Add(row);
        //    }

        //    result = Mustache.Render.StringToString(template, objs);

        //    return result;
        //}
        ////---------------------------------------------------------------------------------------------
        //protected string applyTableToTemplate(string template, string nameRowTypeColumn, Dictionary<string, string> rowTemplates, string field, FwJsonDataTable dt)
        //{
        //    string result, html, rowtype, cellvalue;
        //    StringBuilder sb, rowtemplate;
        //    int indexRowTypeColumn;

        //    sb = new StringBuilder();
        //    indexRowTypeColumn = dt.ColumnIndex[nameRowTypeColumn];
        //    for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
        //    {
        //        rowtype = dt.Rows[rowno][indexRowTypeColumn].ToString();
        //        rowtemplate = new StringBuilder(rowTemplates[rowtype]);
        //        for (int colno = 0; colno < dt.Columns.Count; colno++)
        //        {
        //            if (dt.Rows[rowno][colno] != null)
        //            {
        //                cellvalue = dt.Rows[rowno][colno].ToString();
        //            }
        //            else
        //            {
        //                cellvalue = string.Empty;
        //            }
        //            rowtemplate.Replace("{{" + dt.ColumnNameByIndex[colno] + "}}", cellvalue);
        //        }
        //        sb.Append(rowtemplate.ToString());
        //    }
        //    html   = sb.ToString();
        //    result = template.Replace(field, html);

        //    return result;
        //}
        ////---------------------------------------------------------------------------------------------
        ////protected string getGenericBodyTemplate(string tablename, FwJsonDataTable dt)
        ////{
        ////    StringBuilder sb;
        ////    string html;

        ////    sb = new StringBuilder();
        ////    sb.AppendLine("<div id=\"body\">");
        ////    sb.AppendLine("  <table>");
        ////    sb.AppendLine("    <thead>");
        ////    sb.AppendLine("      <tr>");
        ////    // build header
        ////    for(int colindex = 0; colindex < dt.Columns.Count; colindex++)
        ////    {
        ////        sb.AppendLine("        <td>");
        ////        sb.AppendLine("          <div class=\"" + dt.ColumnNameByIndex[colindex] + "\">" + dt.ColumnNameByIndex[colindex] + "</div>");
        ////        sb.AppendLine("        </td>");
        ////    }
        ////    sb.AppendLine("      </tr>");
        ////    sb.AppendLine("    </thead>");
        ////    sb.AppendLine("    <tbody>");
        ////    sb.AppendLine("      {{#" + tablename + "}}");
        ////    sb.AppendLine("      <tr class=\"detail\">");
        ////    for(int colindex = 0; colindex < dt.Columns.Count; colindex++)
        ////    {
        ////        sb.AppendLine("        <td>");
        ////        sb.AppendLine("          <div class=\"" + dt.ColumnNameByIndex[colindex] + "\">{{" + dt.ColumnNameByIndex[colindex] + "}}</div>");
        ////        sb.AppendLine("        </td>");
        ////    }
        ////    sb.AppendLine("      </tr>");
        ////    sb.AppendLine("      {{/" + tablename + "}}");
        ////    sb.AppendLine("    </tbody>");
        ////    sb.AppendLine("  </table>");
        ////    sb.AppendLine("</div>");
        ////    html = sb.ToString();

        ////    return html;
        ////}
        ////---------------------------------------------------------------------------------------------
        //protected Dictionary<string, string> getTemplates(string xml)
        //{
        //    XmlDocument doc;
        //    XmlNode node;
        //    Dictionary<string, string> templates;

        //    doc = new XmlDocument();
        //    doc.LoadXml("<div>" + xml + "</div>");
        //    templates = new Dictionary<string, string>();

        //    node = doc.SelectSingleNode("//style[@id='stylesheet']");
        //    templates["stylesheet"] = node.OuterXml;

        //    node = doc.SelectSingleNode("//div[@id='header']");
        //    templates["header"] = node.OuterXml;

        //    node = doc.SelectSingleNode("//div[@id='body']");
        //    templates["body"] = node.OuterXml;

        //    node = doc.SelectSingleNode("//div[@id='footer']");
        //    templates["footer"] = node.OuterXml;

        //    return templates;
        //}
        ////---------------------------------------------------------------------------------------------
        //public class PrintOptions
        //{
        //    public enum PrintLayout {Landscape, Portrait}
        //    //---------------------------------------------------------------------------------------------
        //    public float MarginTop    { get; set; }
        //    public float MarginRight  { get; set; }
        //    public float MarginBottom { get; set; }
        //    public float MarginLeft   { get; set; }
        //    public float PageWidth    { get; set; }
        //    public float PageHeight   { get; set; }
        //    public float HeaderHeight { get; set; }
        //    public float FooterHeight { get; set; }
        //    public float BodyWidth    { get { return PageWidth - MarginLeft - MarginRight; } }
        //    public float BodyHeight   { get { return PageHeight - MarginTop - HeaderHeight - FooterHeight - MarginBottom; } }
        //    public bool DownloadAsAttachment {get; set; }
        //    //---------------------------------------------------------------------------------------------
        //    public PrintOptions(PrintLayout printlayout)
        //    {
        //        DownloadAsAttachment = false;
        //        switch(printlayout)
        //        {
        //            case PrintLayout.Landscape:
        //                this.MarginTop    = 0.25f;
        //                this.MarginRight  = 0.25f;
        //                this.MarginBottom = 0.25f;
        //                this.MarginLeft   = 0.25f;
        //                this.PageWidth    = 11f;
        //                this.PageHeight   = 8.5f;
        //                this.HeaderHeight = 0.75f;
        //                this.FooterHeight = 0.25f;
        //                break;
        //            case PrintLayout.Portrait:
        //                this.MarginTop    = 0.25f;
        //                this.MarginRight  = 0.25f;
        //                this.MarginBottom = 0.25f;
        //                this.MarginLeft   = 0.25f;
        //                this.PageWidth    = 8.5f;
        //                this.PageHeight   = 11f;
        //                this.HeaderHeight = 0.75f;
        //                this.FooterHeight = 0.25f;
        //                break;
        //        }

        //    }
        //    //---------------------------------------------------------------------------------------------
        //    public SizeF GetPageSize()
        //    {
        //        SizeF result;

        //        result = new SizeF(PageWidth, PageHeight);

        //        return result;
        //    }
        //    //---------------------------------------------------------------------------------------------
        //    public RectangleF GetOutputArea()
        //    {
        //        RectangleF result;
        //        float x, y, width, height;

        //        x      = MarginLeft;
        //        y      = MarginTop + HeaderHeight;
        //        width  = PageWidth - MarginLeft - MarginRight;
        //        height = PageHeight - MarginTop - HeaderHeight - FooterHeight - MarginBottom;
        //        result = new RectangleF(x, y, width, height);

        //        return result;
        //    }
        //    //---------------------------------------------------------------------------------------------
        //    public float GetHtmlHeaderPosition()
        //    {
        //        float result;
        //        result = MarginTop;
        //        return result;
        //    }
        //    //---------------------------------------------------------------------------------------------
        //    public float GetHtmlFooterPosition()
        //    {
        //        float result;
        //        result = MarginTop + HeaderHeight + BodyHeight;
        //        return result;
        //    }
        //    //---------------------------------------------------------------------------------------------
        //}
        ////---------------------------------------------------------------------------------------------
        //public Dictionary<string, string>GetMustacheRowModel(FwJsonDataTable dt, int rowno)
        //{
        //    Dictionary<string, string> model = new Dictionary<string, string>();
        //    for (int colno = 0; colno < dt.Columns.Count; colno++)
        //    {
        //        var col = dt.Columns[colno];
        //        model[col.DataField] = dt.GetValue(rowno, colno).ToString().TrimEnd();
        //    }
        //    return model;
        //}
        ////---------------------------------------------------------------------------------------------
        //public string GetDateRangeString(FwDateTime fromdatetime, FwDateTime todatetime)
        //{
        //    string daterange = string.Empty;
        //    if (request.parameters.fromdate == "" && request.parameters.todate == "")
        //    {
        //        daterange = "";
        //    }
        //    else if (request.parameters.fromdate != "" && request.parameters.todate == "")
        //    {
        //        daterange = "Since " + fromdatetime.ToDateTime().ToString(); 
        //    }
        //    else if (request.parameters.fromdate == "" && request.parameters.todate != "")
        //    {
        //        daterange = "Before " + todatetime.ToDateTime().ToString(); 
        //    }
        //    else if (request.parameters.fromdate != "" && request.parameters.todate != "")
        //    {
        //        daterange = fromdatetime.ToDateTime().ToString() + " - " + todatetime.ToDateTime().ToString();
        //    }
        //    return daterange;
        //}
        ////---------------------------------------------------------------------------------------------
        //public string GetValidationParameter(string parametername)
        //{
        //    return FwCryptography.AjaxDecrypt(((IDictionary<string,object>)request.parameters)[parametername].ToString());
        //}
        ////---------------------------------------------------------------------------------------------
    }
}
