using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Web;

namespace Web.Source.Reports
{
    class PrintOrder : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Print"; }
        //---------------------------------------------------------------------------------------------
        protected override string renderHeaderHtml(string styletemplate, string headertemplate, FwReport.PrintOptions printOptions)
        {
            FwSqlCommand qry;
            FwJsonDataTable dtHeader;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            qry.Add("exec webgetorderprintheader @orderid = @orderid");

            if (request.parameters.OrderId != "")
            {
                qry.AddParameter("@orderid", request.parameters.OrderId);
            }
            else
            {
                qry.AddParameter("@orderid", request.parameters.QuoteId);
            }
            dtHeader = qry.QueryToFwJsonTable(true);


            StringBuilder sb;
            string html, dataUrl;

            sb = new StringBuilder(base.renderHeaderHtml(styletemplate, headertemplate, printOptions));

            if (request.parameters.OrderId != "")
            {
                sb.Replace("[LBLREPORTNAME]", "ORDER");
            }
            else
            {
                sb.Replace("[LBLREPORTNAME]", "QUOTE");
            }

            //Image image = Image.FromFile(HttpContext.Current.Server.MapPath("~/App_Data/Client/4WallLogo.png"));
            //float printwidth = (image.Width / image.HorizontalResolution);
            dataUrl = GetDataURL(HttpContext.Current.Server.MapPath("~/App_Data/Client/4WallLogo.png"));
            sb.Replace("[CLIENTLOGO]", "<img class=\"clientlogo\" src=\"" + dataUrl + "\" style=\"margin-left: 30px; width:1.225in;\" />");

            html = sb.ToString();
            html = this.applyTableToTemplate(html, "header", dtHeader);

         

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected override string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        {
            string html;
            FwJsonDataTable dtPrintOrder;
            StringBuilder sb;

            dtPrintOrder = GetOrder();

            html = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
            sb = new StringBuilder(base.renderBodyHtml(styletemplate, bodytemplate, printOptions));
            sb.Replace("[TotalRows]", "Total Rows: " + dtPrintOrder.Rows.Count);
            html = sb.ToString();
            html = this.applyTableToTemplate(html, "details", dtPrintOrder);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetOrder()
        {
            FwSqlCommand qry;
            FwJsonDataTable dtDetails;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            qry.Add("exec webgetorderprintdetails @orderid = @orderid");

            if (request.parameters.OrderId != "")
            {
                qry.AddParameter("@orderid", request.parameters.OrderId);
            }
            else
            {
                qry.AddParameter("@orderid", request.parameters.QuoteId);
            }
            dtDetails = qry.QueryToFwJsonTable(true);

            dtDetails.InsertSubTotalRows("rectypedisplay", "rowtype", new string[] { "periodextended" });
            dtDetails.InsertTotalRow("rowtype", "detail", "grandtotal", new string[] { "periodextended" });

            return dtDetails;
        }
        //---------------------------------------------------------------------------------------------
        public string GetCommaListDecrypt(string encryptedlist)
        {
            string[] values;
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(encryptedlist))
            {
                values = encryptedlist.Split(new char[] { ',' }, StringSplitOptions.None);
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = FwCryptography.AjaxDecrypt(values[i]);
                }
                result = string.Join(",", values);
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public override void GetData()
        {
            //switch ((string)request.method)
            //{
            //    case "LoadForm":
            //        response.statuslist  = GetStatusList();
            //        response.orderbylist = GetOrderByList();
            //        break;
            //}
        }
        //---------------------------------------------------------------------------------------------
    }
}
