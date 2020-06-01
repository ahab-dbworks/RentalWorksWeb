using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using EO.Pdf;
using Fw.Json.SqlServer;
using Fw.Json.SqlServer.Entities;
using Fw.Json.Utilities;
using OfficeOpenXml;
using Fw.Json.ValueTypes;
using System.Linq;

namespace Fw.Json.Services
{
    // MV 2014-12-31 properties are lowercase for serialization to JSON
    public class FwReportOrderByItem
    {
        public string value = string.Empty;
        public string text = string.Empty;
        public string selected = "F";
        public string orderbydirection = "asc";
    }

    // MV 2014-12-31 properties are lowercase for serialization to JSON
    public class FwReportStatusItem
    {
        public string value = string.Empty;
        public string text = string.Empty;
        public string selected = "F";
    }
    
    public abstract class FwReport
    {
        protected dynamic request, response, session;
        //---------------------------------------------------------------------------------------------
        public FwReport()
        {
            
        }
        //---------------------------------------------------------------------------------------------
        public static void AddLicense()
        {
            //2015
            //EO.Pdf.Runtime.AddLicense(
            //    "iZ9Zl8DADOul5vvPuIlZl6Sx5+6r2+kD9O2f5qT1DPOetKbC2rFwprbB3LRb" +
            //    "l/cGDcSx5+0DEPJ668Gz3K5rrrPD27BvmaQHEPGs4PP/6KFvmaTA6YxDl6Sx" +
            //    "y7us4Ov/DPOu6enPFLKzyvz2FsKqxrrp4tltrM39/PJs3sHO566s4Ov/DPOu" +
            //    "6enPuIl1pvD6DuSn6unPuIl14+30EO2s3MKetZ9Zl6TNF+ic3PIEEMidtbrJ" +
            //    "4rFvqbrB27F1pvD6DuSn6unaD71GgaSxy5914+30EO2s3OnP566l4Of2GfKe" +
            //    "3MKetZ9Zl6TNDOul5vvPuIlZl6Sxy59Zl8DyD+NZ6/0BELxbvNO/++OfmaQH" +
            //    "EPGs4PP/6KFqrLLBzZ9otZGbyw==");

            //2017
            //EO.Pdf.Runtime.AddLicense(
            //    "LdoPvUaBpLHLn3Xj7fQQ7azc6c/nrqXg5/YZ8p7cwp61n1mXpM0M66Xm+8+4" +
            //    "iVmXpLHLn1mXwPIP41nr/QEQvFu807/745+ZpAcQ8azg8//ooWqussHNn2i1" +
            //    "kZvLn1mXwMAM66Xm+8+4iVmXpLHn7qvb6QP07Z/mpPUM8560psLasXCmtsHc" +
            //    "tFuX9wYNxLHn7QMQ8nrrwbPcrmuus8PbsHGZpAcQ8azg8//ooW+ZpMDpjEOX" +
            //    "pLHLu6zg6/8M867p6c/61IHH+P7/so/tuwkA7HHu+dLtsYbewc7nrqzg6/8M" +
            //    "867p6c+4iXWm8PoO5Kfq6c+4iXXj7fQQ7azcwp61n1mXpM0X6Jzc8gQQyJ21" +
            //    "usnisW+pusHbsXWm8PoO5Kfq6Q==");

            //2017
            //EO.Pdf.Runtime.AddLicense(
            //    "tZf3Bg3EseftAxDyeuvBs9yua66zw9uwcZmkBxDxrODz/+ihb5mkwOmMQ5ek" + 
            //    "scu7rODr/wzzrunpz/rUgcf4/v+yj+27CQDsce750u2xht7BzueurODr/wzz" + 
            //    "runpz7iJdabw+g7kp+rpz7iJdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6" + 
            //    "yeKxb6m6wduxdabw+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hny" + 
            //    "ntzCnrWfWZekzQzrpeb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mk" + 
            //    "BxDxrODz/+ihaq6ywc2faLWRm8ufWZfAwAzrpeb7z7iJWZeksefuq9vpA/Tt" + 
            //    "n+ak9QzznrSmwtqxcKa2wdy0Ww==");

            //2018
            //EO.Pdf.Runtime.AddLicense(
            //    "op/mpPUM8560psLasXCmtsHctFuX9wYNxLHn7QMQ8nrrwbPcrmuus8PbsHGZ" + 
            //    "pAcQ8azg8//ooW+ZpMDpjEOXpLHLu6zg6/8M867p6c8d0W6/8Nv256/QxfcN" + 
            //    "+Gu7veYP1aa4wc7nrqzg6/8M867p6c+4iXWm8PoO5Kfq6c+4iXXj7fQQ7azc" + 
            //    "wp61n1mXpM0X6Jzc8gQQyJ21usnisW+pusHbsXWm8PoO5Kfq6doPvUaBpLHL" + 
            //    "n3Xj7fQQ7azc6c/nrqXg5/YZ8p7cwp61n1mXpM0M66Xm+8+4iVmXpLHLn1mX" + 
            //    "wPIP41nr/QEQvFu807/745+ZpAcQ8azg8//ooWqvssHNn2i1kZvLn1mXwMAM" + 
            //    "66Xm+8+4iVmXpLHn7qvb6QP07Q==");

            //2020
            EO.Pdf.Runtime.AddLicense(
                "dKQHEPGs4PP/6KFrp7LBzZ9otZGby59Zl8DADOul5vvPuIlZl6Sx5+6r2+kD" +
                "9O2f5qT1DPOetKbC2rFwprbB3LRbl/cGDcSx5+0DEPJ668Gz3K5rrrPD27Fq" +
                "maQHEPGs4PP/6KFvmaTA6YxDl6Sxy7us4Ov/DPOu6enP9bCjwer13sKvrMbm" +
                "+LKAq94K/eBq3sHO566s4Ov/DPOu6enPuIl1pvD6DuSn6unPuIl14+30EO2s" +
                "3MKetZ9Zl6TNF+ic3PIEEMidtbrJ4rFvqbrB27F1pvD6DuSn6unaD71GgaSx" +
                "y5914+30EO2s3OnP566l4Of2GfKe3MKetZ9Zl6TNDOul5vvPuIlZl6Sxy59Z" +
                "l8DyD+NZ6/0BELxbvNO/++OfmQ==");
        }
        //---------------------------------------------------------------------------------------------
        abstract protected FwSqlConnection GetApplicationSqlConnection();
        //---------------------------------------------------------------------------------------------
        public void Init(IDictionary<String, object> irequest, IDictionary<String, object> iresponse, IDictionary<String, object> isession)
        {
            this.request  = irequest;
            this.response = iresponse;
            this.session  = isession;
        }
        //---------------------------------------------------------------------------------------------
        public virtual void Preview()
        {
            response.downloadurl = renderHtml(request.templates.stylesheet, request.templates.header, request.templates.body, request.templates.footer, getRequestPrintOptions());
        }
        //---------------------------------------------------------------------------------------------
        public virtual void DownloadPdf()
        {
            DownloadableResult renderPdfResult;

            renderPdfResult      = renderPdf(request.templates.stylesheet, request.templates.header, request.templates.body, request.templates.footer, getRequestPrintOptions());
            response.downloadurl = renderPdfResult.DownloadUrl;
        }
        //---------------------------------------------------------------------------------------------
        public virtual void DownloadExcel()
        {
            PrintOptions printOptions = getRequestPrintOptions();
            string filename, path;

            ExcelDownloadResult excelPackageResult = GetExcelDownload();
            if (excelPackageResult != null)
            {
                if (excelPackageResult.Filename == null)
                {
                    excelPackageResult.Filename = getReportFileName();
                }
                DownloadableResult result = new DownloadableResult(); ;
                filename = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".xlsx";
                path = HttpContext.Current.Server.MapPath("~/App_Data/Temp/Downloads/" + filename);
                result.DownloadUrl = VirtualPathUtility.ToAbsolute("~/fwdownload/" + getReportFileName() + ".pdf?filename=" + HttpUtility.UrlEncode(filename) + "&saveas=" + HttpUtility.UrlEncode(excelPackageResult.Filename + ".xlsx") + "&asattachment=" + printOptions.DownloadAsAttachment.ToString().ToLower());
                result.Path = path;
                excelPackageResult.ExcelPackage.File = new FileInfo(path);
                excelPackageResult.ExcelPackage.Save();
                response.downloadurl = result.DownloadUrl;
            }
            else
            {
                //throw new Exception("Excel download is not available for this report.  Contact Database Works if you need this functionality.");
                throw new Exception("Excel download is not available for this report."); //justin 05/30/2018 removed text per Terry
            }
        }
        //---------------------------------------------------------------------------------------------
        public class ExcelDownloadResult
        {
            public string Filename { get; set; } = null;
            public ExcelPackage ExcelPackage { get; set; } = new ExcelPackage();
        }
        public virtual ExcelDownloadResult GetExcelDownload()
        {
            return null;
        }
        //---------------------------------------------------------------------------------------------
        //public virtual void SendHtmlEmail()
        //{
        //    RenderPdfResult renderPdfResult;
        //    string from, to, cc, subject, body;
        //    FwSqlData.GetEmailReportControlResponse emailreportcontrol;
            
        //    emailreportcontrol = FwSqlData.GetEmailReportControl(GetApplicationSqlConnection());
        //    from    = "";
        //    to      = request.email.to;
        //    cc      = request.email.cc;
        //    subject = getReportName();
        //    body    = renderHtml(request.templates.stylesheet, request.templates.header, request.templates.body, request.templates.footer, getRequestPrintOptions());
        //    renderPdfResult = renderPdf(request.templates.stylesheet, request.templates.header, request.templates.body, request.templates.footer, getRequestPrintOptions());
        //    FwEmailService.SendEmail(FwSqlConnection.AppConnection, from, to, cc, subject, body, getReportName() + ".pdf", renderPdfResult.Path);
        //}
        //---------------------------------------------------------------------------------------------
        public virtual void SendPdfEmail()
        {
            DownloadableResult renderPdfResult;
            string from, to, cc, subject, body;
            FwSqlData.GetEmailReportControlResponse emailreportcontrol;
            dynamic webuser;
            
            emailreportcontrol = FwSqlData.GetEmailReportControl(GetApplicationSqlConnection());
            webuser = FwSqlData.GetWebUsersView(GetApplicationSqlConnection(), session.security.webUser.webusersid);
            from = webuser.email;
            to   = request.email.to;
            if (to == "[me]") to = webuser.email;
            cc      = request.email.cc;
            subject = request.email.subject;
            if (subject == "[reportname]") subject = getReportName();
            body    = request.email.body;
            renderPdfResult = renderPdf(request.templates.stylesheet, request.templates.header, request.templates.body, request.templates.footer, getRequestPrintOptions());
            FwEmailService.SendEmail(FwSqlConnection.AppConnection, from, to, cc, subject, body, true, getReportName() + ".pdf", renderPdfResult.Path);
        }
        //---------------------------------------------------------------------------------------------
        public void GetFromEmail()
        {
            response.fromemail = FwSqlData.GetWebUsersView(GetApplicationSqlConnection(), session.security.webUser.webusersid).email;
        }
        //---------------------------------------------------------------------------------------------
        public void GetEmailByWebUsersId()
        {
            const string METHOD_NAME = "GetEmailByWebUsersId";
            string[] webusersids, toemails;
            List<string> emails;
            dynamic webusersview;
            StringBuilder sb;
            FwSqlConnection conn;
            string emailto;

            
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "webusersids");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "to");
            conn = GetApplicationSqlConnection();
            webusersids = ((string)request.webusersids).Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
            toemails    = ((string)request.to).Split(new char[]{',', ';'}, StringSplitOptions.RemoveEmptyEntries);
            emails      = new List<string>();
            foreach (string email in toemails)
            {
                if (!emails.Contains(email)) 
                {
                    emails.Add(email);
                }
            }
            foreach (string webusersidencrypted in webusersids)
            {
                string webusersid, email;
                webusersid = FwCryptography.AjaxDecrypt(webusersidencrypted);
                webusersview = FwSqlData.GetWebUsersView(conn, webusersid);
                email = webusersview.email;
                if (!string.IsNullOrWhiteSpace(email))
                {
                    if (!emails.Contains(email)) 
                    {
                        emails.Add(email);
                    }
                }
            }
            sb = new StringBuilder();
            for (int i = 0; i < emails.Count; i++)
            {
                if (i > 0) sb.Append(";");
                sb.Append(emails[i]);
            }
            emailto = sb.ToString();
            response.emailto = emailto;
        }
        //---------------------------------------------------------------------------------------------
        public virtual void GetData()
        {
            
        }
        //---------------------------------------------------------------------------------------------
        public void GetDefaultPrintOptions()
        {
            response.printoptions = getDefaultPrintOptions();
        }
        //---------------------------------------------------------------------------------------------
        protected virtual PrintOptions getDefaultPrintOptions()
        {
            return new PrintOptions(PrintOptions.PrintLayout.Landscape);
        }
        //---------------------------------------------------------------------------------------------
        protected virtual string getReportName()
        {
            return "Untitled Report";
        }
        //---------------------------------------------------------------------------------------------
        protected virtual string getReportFileName()
        {
            String filename = new String(getReportName()
                .Where(ch => Char.IsLetterOrDigit(ch))
                .ToArray());
            return filename;
        }
        //---------------------------------------------------------------------------------------------
        private PrintOptions getRequestPrintOptions()
        {
            PrintOptions printOptions;

            printOptions = new PrintOptions(PrintOptions.PrintLayout.Landscape);
            printOptions.PageWidth    = FwConvert.ToFloat(request.parameters.pagewidth);
            printOptions.PageHeight   = FwConvert.ToFloat(request.parameters.pageheight);
            printOptions.MarginTop    = FwConvert.ToFloat(request.parameters.margintop);
            printOptions.MarginRight  = FwConvert.ToFloat(request.parameters.marginright);
            printOptions.MarginBottom = FwConvert.ToFloat(request.parameters.marginbottom);
            printOptions.MarginLeft   = FwConvert.ToFloat(request.parameters.marginleft);
            printOptions.HeaderHeight = FwConvert.ToFloat(request.parameters.headerheight);
            printOptions.FooterHeight = FwConvert.ToFloat(request.parameters.footerheight);
            if (FwValidate.IsPropertyDefined(request.parameters, "downloadasattachment"))
            {
                printOptions.DownloadAsAttachment = request.parameters.downloadasattachment;
            }

            return printOptions;
        }
        //---------------------------------------------------------------------------------------------
        protected virtual string renderCommonFields(string template, PrintOptions printOptions)
        {
            StringBuilder sb;
            string result, dataUrl;
            FwControl control;
            
            control = FwSqlData.GetControl(GetApplicationSqlConnection());
            sb = new StringBuilder(template);
            sb.Replace("[report]", getReportName());
            sb.Replace("[company]", control.Company);
            sb.Replace("[system]", control.System);
            if (!File.Exists(HttpContext.Current.Server.MapPath("~/App_Data/Client/ReportLogo.png"))) {
                throw new Exception("Missing Report Logo at: " + HttpContext.Current.Server.MapPath("~/App_Data/Client/ReportLogo.png") + ". Please use a 300-600 DPI image to ensure a high quality result (png is recommended). The print width of the image will determine the size on the report.  When saving images from Photoshop, make sure to use Save As and not Save for Web, which will lower the DPI.");
            }
            Image image = Image.FromFile(HttpContext.Current.Server.MapPath("~/App_Data/Client/ReportLogo.png"));
            float printwidth = (image.Width / image.HorizontalResolution);
            dataUrl = GetDataURL(HttpContext.Current.Server.MapPath("~/App_Data/Client/ReportLogo.png"));
            sb.Replace("[logo]", "<img class=\"clientlogo\" src=\"" + dataUrl + "\" style=\"width:" + printwidth + "in;\" />");
            sb.Replace("[datetime]", FwConvert.ToUSShortDateTime(DateTime.Now));
            result = sb.ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static string GetDataURL(string imgFile)
        {
            return "data:image/" 
                        + Path.GetExtension(imgFile).Replace(".","")
                        + ";base64," 
                        + Convert.ToBase64String(File.ReadAllBytes(imgFile));
        }
        //---------------------------------------------------------------------------------------------
        protected virtual string renderStyleSheetFields(string template, PrintOptions printOptions)
        {
            StringBuilder sb;
            string result;

            sb = new StringBuilder(template);
            sb.Replace("[applicationstylesheet]",       File.ReadAllText(HttpContext.Current.Server.MapPath("~/source/Reports/Application/StyleSheet.css")));
            result = renderCommonFields(sb.ToString(), printOptions);

            return result;
        }
        //---------------------------------------------------------------------------------------------
        protected virtual string renderHeaderFields(string template, PrintOptions printOptions)
        {
            StringBuilder sb;
            string result;
            
            sb = new StringBuilder(template);
            sb.Replace("[applicationlandscapeheader]", File.ReadAllText(HttpContext.Current.Server.MapPath("~/source/Reports/Application/LandscapeHeader.htm")));
            sb.Replace("[applicationportraitheader]",  File.ReadAllText(HttpContext.Current.Server.MapPath("~/source/Reports/Application/PortraitHeader.htm")));
            result = renderCommonFields(sb.ToString(), printOptions);

            return result;
        }
        //---------------------------------------------------------------------------------------------
        protected virtual string renderFooterFields(string template, PrintOptions printOptions)
        {
            StringBuilder sb;
            string result;
            
            sb = new StringBuilder(template);
            sb.Replace("[applicationlandscapefooter]", File.ReadAllText(HttpContext.Current.Server.MapPath("~/source/Reports/Application/LandscapeFooter.htm")));
            sb.Replace("[applicationportraitfooter]",  File.ReadAllText(HttpContext.Current.Server.MapPath("~/source/Reports/Application/PortraitFooter.htm")));
            result = renderCommonFields(sb.ToString(), printOptions);

            return result;
        }
        //---------------------------------------------------------------------------------------------
        protected virtual string renderHtml(string styletemplate, string headertemplate, string bodytemplate, string footertemplate, PrintOptions printOptions)
        {
            StringBuilder sb;
            string html, bodywidth, filename, path, downloadurl;
            
            bodywidth      = (printOptions.BodyWidth * 96).ToString();
            styletemplate  = renderStyleSheetFields(styletemplate, printOptions);
            headertemplate = renderHeaderFields(headertemplate, printOptions);
            footertemplate = renderFooterFields(footertemplate, printOptions);
            bodytemplate   = renderCommonFields(bodytemplate, printOptions);
            sb = new StringBuilder();
            sb.Append("<!DOCTYPE HTML>");
            sb.Append("<html class=\"renderhtml\">");
            sb.Append("<head>");
            sb.Append("<meta id=\"metaFormatDetection\" name=\"format-detection\" content=\"telephone=no\">");
            sb.AppendFormat("<title>{0}</title>", getReportName());
            sb.Append("<style>");
            sb.Append(renderStyleSheet(styletemplate, printOptions));
            sb.Append("</style>");
            sb.Append("</head>");
            sb.Append("<body>");
            sb.AppendFormat("<div style=\"width:{0}px;\">", bodywidth);
            if (printOptions.HeaderHeight > 0)
            {
                sb.Append(renderHeaderHtml(string.Empty, headertemplate, printOptions));
            }
            sb.Append(renderBodyHtml(string.Empty, bodytemplate, printOptions));
            if (printOptions.FooterHeight > 0)
            {
                sb.Append(renderFooterHtml(string.Empty, footertemplate, printOptions));
            }
            sb.Append("</div>");
            sb.Append("</body>");
            sb.Append("</html>");
            html = sb.ToString();

            filename    = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".html";
            path        = HttpContext.Current.Server.MapPath("~/App_Data/Temp/Downloads/" + filename);
            File.WriteAllText(path, html);
            downloadurl = VirtualPathUtility.ToAbsolute("~/fwdownload/" + getReportFileName() + ".html?filename=" + HttpUtility.UrlEncode(filename) + "&saveas=" + HttpUtility.UrlEncode(getReportFileName() + ".html") + "&asattachment=false");

            return downloadurl;
        }
        //---------------------------------------------------------------------------------------------
        protected virtual string renderStyleSheet(string styletemplate, PrintOptions printOptions)
        {
            StringBuilder sb;
            string html;

            sb = new StringBuilder();
            if (!string.IsNullOrEmpty(styletemplate))
            {
                sb.Append("<style>");
                sb.Append(styletemplate);
                sb.Append("</style>");
            }
            html = sb.ToString();

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected virtual string renderHeaderHtml(string styletemplate, string headertemplate, PrintOptions printOptions)
        {
            StringBuilder sb;
            string html;

            sb = new StringBuilder();
            if (!string.IsNullOrEmpty(styletemplate))
            {
                sb.Append("<style>");
                sb.Append(styletemplate);
                sb.Append("</style>");
            }
            sb.Append(headertemplate);
            html = sb.ToString();

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected virtual string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        {
            StringBuilder sb;
            string html;

            sb = new StringBuilder();
            if (!string.IsNullOrEmpty(styletemplate))
            {
                sb.Append("<style>");
                sb.Append(styletemplate);
                sb.Append("</style>");
            }
            sb.Append(bodytemplate);
            html = sb.ToString();

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected virtual string renderFooterHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        {
            StringBuilder sb;
            string html;

            sb = new StringBuilder();
            if (!string.IsNullOrEmpty(styletemplate))
            {
                sb.Append("<style>");
                sb.Append(styletemplate);
                sb.Append("</style>");
            }
            sb.Append(bodytemplate);
            html = sb.ToString();

            return html;
        }
        //---------------------------------------------------------------------------------------------
        public class DownloadableResult
        {
            public string DownloadUrl { get; set; }
            public string Path { get; set; }
        }
        //---------------------------------------------------------------------------------------------
        protected virtual DownloadableResult renderPdf(string styletemplate, string headertemplate, string bodytemplate, string footertemplate, PrintOptions printOptions)
        {
            string headerHtml, bodyHtml, footerHtml, filename, path, bodyWidth, renderedstyletemplate, renderedheadertemplate,
                rendereredbodytemplate, renderedfootertemplate;
            StringBuilder sbHeaderHtml, sbBodyHtml, sbFooterHtml;
            DownloadableResult renderPdfResult;
            //PdfDocument pdfdoc;
            //HtmlToPdfResult htmltopdfresult;

            styletemplate          = renderStyleSheetFields(styletemplate, printOptions);
            headertemplate         = renderHeaderFields(headertemplate, printOptions);
            footertemplate         = renderFooterFields(footertemplate, printOptions);
            bodytemplate           = renderCommonFields(bodytemplate, printOptions);
            renderPdfResult        = new DownloadableResult();
            bodyWidth              = (printOptions.BodyWidth * 96).ToString();
            renderedstyletemplate  = renderStyleSheet(styletemplate, printOptions);
            renderedheadertemplate = renderHeaderHtml(string.Empty, headertemplate, printOptions);
            rendereredbodytemplate = renderBodyHtml(string.Empty, bodytemplate, printOptions);
            renderedfootertemplate = renderFooterHtml(string.Empty, footertemplate, printOptions);

            sbHeaderHtml = new StringBuilder();
            sbHeaderHtml.Append(renderedstyletemplate);
            sbHeaderHtml.AppendFormat("<div class=\"renderpdf\" style=\"width:{0}px;overflow:hidden;\">", bodyWidth);
            sbHeaderHtml.Append(renderedheadertemplate);
            sbHeaderHtml.Append("</div>");
            headerHtml = sbHeaderHtml.ToString();

            sbBodyHtml = new StringBuilder();
            sbBodyHtml.Append(renderedstyletemplate);
            sbHeaderHtml.AppendFormat("<div class=\"renderpdf\" style=\"width:{0}px;overflow:hidden;\">", bodyWidth);
            sbBodyHtml.Append(rendereredbodytemplate);
            sbBodyHtml.Append("</div>");
            bodyHtml = sbBodyHtml.ToString();

            sbFooterHtml = new StringBuilder();
            sbFooterHtml.Append(renderedstyletemplate);
            sbFooterHtml.AppendFormat("<div class=\"renderpdf\" style=\"width:{0}px;overflow:hidden;\">", bodyWidth);
            sbFooterHtml.Append(renderedfootertemplate);
            sbFooterHtml.Append("</div>");
            footerHtml = sbFooterHtml.ToString();

            HtmlToPdf.Options.BaseUrl                    = FwFunc.GetRequestBaseUrl();
            HtmlToPdf.Options.AutoAdjustForDPI           = false;
            HtmlToPdf.Options.RepeatTableHeaderAndFooter = true;
            HtmlToPdf.Options.JpegQualityLevel = 100; //.PreserveHighResImages      = true;
            HtmlToPdf.Options.PageSize                   = printOptions.GetPageSize();
            HtmlToPdf.Options.OutputArea                 = printOptions.GetOutputArea();
            HtmlToPdf.Options.AutoFitX                   = HtmlToPdfAutoFitMode.None;
            HtmlToPdf.Options.AutoFitY                   = HtmlToPdfAutoFitMode.None;
            if (printOptions.HeaderHeight > 0)
            {
                HtmlToPdf.Options.HeaderHtmlPosition         = printOptions.GetHtmlHeaderPosition();
                HtmlToPdf.Options.HeaderHtmlFormat           = headerHtml;
            }
            if (printOptions.FooterHeight > 0)
            {
                HtmlToPdf.Options.FooterHtmlPosition         = printOptions.GetHtmlFooterPosition();
                HtmlToPdf.Options.FooterHtmlFormat           = footerHtml;
            }
            filename = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".pdf";
            path     = HttpContext.Current.Server.MapPath("~/App_Data/Temp/Downloads/" + filename);
            HtmlToPdf.ConvertHtml(bodyHtml, path);
            //pdfdoc = new PdfDocument();
            //htmltopdfresult = HtmlToPdf.ConvertHtml(bodyHtml, pdfdoc);
            //pdfdoc.Save(path);
            renderPdfResult.DownloadUrl = VirtualPathUtility.ToAbsolute("~/fwdownload/" + getReportFileName() + ".pdf?filename=" + HttpUtility.UrlEncode(filename) + "&saveas=" + HttpUtility.UrlEncode(getReportFileName() + ".pdf") + "&asattachment=" + printOptions.DownloadAsAttachment.ToString().ToLower());
            renderPdfResult.Path        = path;

            return renderPdfResult;
        }
        //---------------------------------------------------------------------------------------------
        protected string applyFieldsToTemplate(string template, Dictionary<string, string> fields)
        {
            string result;
            result = Mustache.Render.StringToString(template, fields);
            return result;
        }
        //---------------------------------------------------------------------------------------------
        protected string applyTableToTemplate(string template, string field, FwJsonDataTable dt)
        {
            string result;
            Dictionary<string, List<Dictionary<string, object>>> objs;
            List<Dictionary<string,object>> table;
            Dictionary<string, object> row;

            objs          = new Dictionary<string,List<Dictionary<string,object>>>();
            table         = new List<Dictionary<string,object>>();
            objs[field] = table;
            for(int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                row = new Dictionary<string,object>();
                for(int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                {
                    row[dt.ColumnNameByIndex[colIndex]] = dt.Rows[rowIndex][colIndex];
                }
                table.Add(row);
            }
            
            result = Mustache.Render.StringToString(template, objs);

            return result;
        }
        //---------------------------------------------------------------------------------------------
        protected string applyTableToTemplate(string template, string nameRowTypeColumn, Dictionary<string, string> rowTemplates, string field, FwJsonDataTable dt)
        {
            string result, html, rowtype, cellvalue;
            StringBuilder sb, rowtemplate;
            int indexRowTypeColumn;

            sb = new StringBuilder();
            indexRowTypeColumn = dt.ColumnIndex[nameRowTypeColumn];
            for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
            {
                rowtype = dt.Rows[rowno][indexRowTypeColumn].ToString();
                rowtemplate = new StringBuilder(rowTemplates[rowtype]);
                for (int colno = 0; colno < dt.Columns.Count; colno++)
                {
                    if (dt.Rows[rowno][colno] != null)
                    {
                        cellvalue = dt.Rows[rowno][colno].ToString();
                    }
                    else
                    {
                        cellvalue = string.Empty;
                    }
                    rowtemplate.Replace("{{" + dt.ColumnNameByIndex[colno] + "}}", cellvalue);
                }
                sb.Append(rowtemplate.ToString());
            }
            html   = sb.ToString();
            result = template.Replace(field, html);

            return result;
        }
        //---------------------------------------------------------------------------------------------
        //protected string getGenericBodyTemplate(string tablename, FwJsonDataTable dt)
        //{
        //    StringBuilder sb;
        //    string html;
            
        //    sb = new StringBuilder();
        //    sb.AppendLine("<div id=\"body\">");
        //    sb.AppendLine("  <table>");
        //    sb.AppendLine("    <thead>");
        //    sb.AppendLine("      <tr>");
        //    // build header
        //    for(int colindex = 0; colindex < dt.Columns.Count; colindex++)
        //    {
        //        sb.AppendLine("        <td>");
        //        sb.AppendLine("          <div class=\"" + dt.ColumnNameByIndex[colindex] + "\">" + dt.ColumnNameByIndex[colindex] + "</div>");
        //        sb.AppendLine("        </td>");
        //    }
        //    sb.AppendLine("      </tr>");
        //    sb.AppendLine("    </thead>");
        //    sb.AppendLine("    <tbody>");
        //    sb.AppendLine("      {{#" + tablename + "}}");
        //    sb.AppendLine("      <tr class=\"detail\">");
        //    for(int colindex = 0; colindex < dt.Columns.Count; colindex++)
        //    {
        //        sb.AppendLine("        <td>");
        //        sb.AppendLine("          <div class=\"" + dt.ColumnNameByIndex[colindex] + "\">{{" + dt.ColumnNameByIndex[colindex] + "}}</div>");
        //        sb.AppendLine("        </td>");
        //    }
        //    sb.AppendLine("      </tr>");
        //    sb.AppendLine("      {{/" + tablename + "}}");
        //    sb.AppendLine("    </tbody>");
        //    sb.AppendLine("  </table>");
        //    sb.AppendLine("</div>");
        //    html = sb.ToString();

        //    return html;
        //}
        //---------------------------------------------------------------------------------------------
        protected Dictionary<string, string> getTemplates(string xml)
        {
            XmlDocument doc;
            XmlNode node;
            Dictionary<string, string> templates;

            doc = new XmlDocument();
            doc.LoadXml("<div>" + xml + "</div>");
            templates = new Dictionary<string, string>();
            
            node = doc.SelectSingleNode("//style[@id='stylesheet']");
            templates["stylesheet"] = node.OuterXml;
            
            node = doc.SelectSingleNode("//div[@id='header']");
            templates["header"] = node.OuterXml;

            node = doc.SelectSingleNode("//div[@id='body']");
            templates["body"] = node.OuterXml;

            node = doc.SelectSingleNode("//div[@id='footer']");
            templates["footer"] = node.OuterXml;

            return templates;
        }
        //---------------------------------------------------------------------------------------------
        public class PrintOptions
        {
            public enum PrintLayout {Landscape, Portrait}
            //---------------------------------------------------------------------------------------------
            public float MarginTop    { get; set; }
            public float MarginRight  { get; set; }
            public float MarginBottom { get; set; }
            public float MarginLeft   { get; set; }
            public float PageWidth    { get; set; }
            public float PageHeight   { get; set; }
            public float HeaderHeight { get; set; }
            public float FooterHeight { get; set; }
            public float BodyWidth    { get { return PageWidth - MarginLeft - MarginRight; } }
            public float BodyHeight   { get { return PageHeight - MarginTop - HeaderHeight - FooterHeight - MarginBottom; } }
            public bool DownloadAsAttachment {get; set; }
            //---------------------------------------------------------------------------------------------
            public PrintOptions(PrintLayout printlayout)
            {
                DownloadAsAttachment = false;
                switch(printlayout)
                {
                    case PrintLayout.Landscape:
                        this.MarginTop    = 0.25f;
                        this.MarginRight  = 0.25f;
                        this.MarginBottom = 0.25f;
                        this.MarginLeft   = 0.25f;
                        this.PageWidth    = 11f;
                        this.PageHeight   = 8.5f;
                        this.HeaderHeight = 0.75f;
                        this.FooterHeight = 0.25f;
                        break;
                    case PrintLayout.Portrait:
                        this.MarginTop    = 0.25f;
                        this.MarginRight  = 0.25f;
                        this.MarginBottom = 0.25f;
                        this.MarginLeft   = 0.25f;
                        this.PageWidth    = 8.5f;
                        this.PageHeight   = 11f;
                        this.HeaderHeight = 0.75f;
                        this.FooterHeight = 0.25f;
                        break;
                }
                
            }
            //---------------------------------------------------------------------------------------------
            public SizeF GetPageSize()
            {
                SizeF result;

                result = new SizeF(PageWidth, PageHeight);

                return result;
            }
            //---------------------------------------------------------------------------------------------
            public RectangleF GetOutputArea()
            {
                RectangleF result;
                float x, y, width, height;

                x      = MarginLeft;
                y      = MarginTop + HeaderHeight;
                width  = PageWidth - MarginLeft - MarginRight;
                height = PageHeight - MarginTop - HeaderHeight - FooterHeight - MarginBottom;
                result = new RectangleF(x, y, width, height);
                
                return result;
            }
            //---------------------------------------------------------------------------------------------
            public float GetHtmlHeaderPosition()
            {
                float result;
                result = MarginTop;
                return result;
            }
            //---------------------------------------------------------------------------------------------
            public float GetHtmlFooterPosition()
            {
                float result;
                result = MarginTop + HeaderHeight + BodyHeight;
                return result;
            }
            //---------------------------------------------------------------------------------------------
        }
        //---------------------------------------------------------------------------------------------
        public Dictionary<string, string>GetMustacheRowModel(FwJsonDataTable dt, int rowno)
        {
            Dictionary<string, string> model = new Dictionary<string, string>();
            for (int colno = 0; colno < dt.Columns.Count; colno++)
            {
                var col = dt.Columns[colno];
                model[col.DataField] = dt.GetValue(rowno, colno).ToString().TrimEnd();
            }
            return model;
        }
        //---------------------------------------------------------------------------------------------
        public string GetDateRangeString(FwDateTime fromdatetime, FwDateTime todatetime)
        {
            string daterange = string.Empty;
            if (request.parameters.fromdate == "" && request.parameters.todate == "")
            {
                daterange = "";
            }
            else if (request.parameters.fromdate != "" && request.parameters.todate == "")
            {
                daterange = "Since " + fromdatetime.ToDateTime().ToString(); 
            }
            else if (request.parameters.fromdate == "" && request.parameters.todate != "")
            {
                daterange = "Before " + todatetime.ToDateTime().ToString(); 
            }
            else if (request.parameters.fromdate != "" && request.parameters.todate != "")
            {
                daterange = fromdatetime.ToDateTime().ToString() + " - " + todatetime.ToDateTime().ToString();
            }
            return daterange;
        }
        //---------------------------------------------------------------------------------------------
        public string GetValidationParameter(string parametername)
        {
            return FwCryptography.AjaxDecrypt(((IDictionary<string,object>)request.parameters)[parametername].ToString());
        }
        //---------------------------------------------------------------------------------------------
    }
}
