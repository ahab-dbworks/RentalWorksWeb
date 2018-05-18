using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Source.Reports
{
    class PrintOrder : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Print Order"; }
        //---------------------------------------------------------------------------------------------
        protected override string renderHeaderHtml(string styletemplate, string headertemplate, FwReport.PrintOptions printOptions)
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            select = new FwSqlSelect();
            qry.AddColumn("officelocation", false, FwJsonDataTableColumn.DataTypes.Text);
            select.Add("exec webgetorderprintheader @orderid = @order");
            select.AddParameter("@order", request.parameters.OrderId);

            select.Parse();
            dtDetails = qry.QueryToFwJsonTable(select, true);

            StringBuilder sb;
            string html;
      
            sb = new StringBuilder(base.renderHeaderHtml(styletemplate, headertemplate, printOptions));

            sb.Replace("[LBLREPORTNAME]", getReportName());
      

            html = sb.ToString();
            html = this.applyTableToTemplate(html, "header", dtDetails);

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
            sb          = new StringBuilder(base.renderBodyHtml(styletemplate, bodytemplate, printOptions));
            sb.Replace("[TotalRows]", "Total Rows: " + dtPrintOrder.Rows.Count);
            html        = sb.ToString();
            html = this.applyTableToTemplate(html, "details", dtPrintOrder);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetOrder()
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            select = new FwSqlSelect();
            select.Add("exec webgetorderprintdetails @orderid = orderId");
            select.AddParameter("orderId", request.parameters.OrderId);
      
            select.Parse();
            dtDetails = qry.QueryToFwJsonTable(select, true);

            dtDetails.InsertSubTotalRows("masterno", "rowtype", new string[] { "periodextended" });
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
                values = encryptedlist.Split(new char[]{','}, StringSplitOptions.None);
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
