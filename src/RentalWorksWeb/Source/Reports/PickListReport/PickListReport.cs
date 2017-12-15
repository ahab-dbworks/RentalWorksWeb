using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Source.Reports
{
    class PickListReport : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Order Pick List"; }
        //---------------------------------------------------------------------------------------------
        protected override string renderHeaderHtml(string styletemplate, string headertemplate, FwReport.PrintOptions printOptions)
        {
            StringBuilder sb;
            string html;
      
            sb = new StringBuilder(base.renderHeaderHtml(styletemplate, headertemplate, printOptions));
            sb.Replace("[LBLREPORTNAME]", getReportName());
            //sb.Replace("[LBLDEPARTMENT]", (request.parameters.ShowDepartmentFrom == "O") ? "Company Department" : "Inventory Type");
            //if      (request.parameters.IncludeValueCost == "" ) { sb.Replace("[LBLITEMVALUE]", ""           ); }
            //else if (request.parameters.IncludeValueCost == "R") { sb.Replace("[LBLITEMVALUE]", "Replacement"); } 
            //else if (request.parameters.IncludeValueCost == "U") { sb.Replace("[LBLITEMVALUE]", "Unit Value" ); }
            //else if (request.parameters.IncludeValueCost == "P") { sb.Replace("[LBLITEMVALUE]", "Unit Cost"  ); }
            //sb.Replace("[LBLASOF]", "AS OF:");
            //sb.Replace("[LBLFROMDATE]", "");
            //sb.Replace("[LBLTODATE]"  , "");

            html = sb.ToString();

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected override string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        {
            string html;
            FwJsonDataTable dtPickListReport;
            StringBuilder sb;

            dtPickListReport = GetPickListReport();

            html        = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
            sb          = new StringBuilder(base.renderBodyHtml(styletemplate, bodytemplate, printOptions));
            sb.Replace("[LBLDEPARTMENT]", (request.parameters.ShowDepartmentFrom == "O") ? "Company Department" : "Inventory Type");
            if      (request.parameters.IncludeValueCost == "R") { sb.Replace("[LBLITEMVALUE]", "Replacement"); } 
            else if (request.parameters.IncludeValueCost == "U") { sb.Replace("[LBLITEMVALUE]", "Unit Value" ); }
            else if (request.parameters.IncludeValueCost == "P") { sb.Replace("[LBLITEMVALUE]", "Unit Cost"  ); }
            else                                                 { sb.Replace("[LBLITEMVALUE]", ""           ); }

            sb.Replace("[TOTOALROWS]", "Total Rows: " + dtPickListReport.Rows.Count);
            if (request.parameters.IncludeValueCost == "on")
            {
                sb.Replace("[TOTALVALUE]", "");
            }
            else
            {
                int itemvalueindex = dtPickListReport.ColumnIndex["itemvalue"];
                decimal total = 0M;
                for (int i = 0; i < dtPickListReport.Rows.Count; i++)
                {
                    decimal tempval;
                    decimal? itemvalue = decimal.TryParse((string)dtPickListReport.Rows[i][itemvalueindex], out tempval) ? tempval : (decimal?)null;
                    total = itemvalue.Value + total;
                }
                sb.Replace("[TOTALVALUE]", "Total Value: " + string.Format("{0:C}", total));
            }

            if (request.parameters.ShowImages == "T")
            {
                sb.Replace("<div id=\"imagecaption\"></div>", "Image");
                sb.Replace("<div id=\"imagevalue\"></div>", "<img style=\"max-height:200px;max-width:200px;\" src=\"{{image}}\">");
            }

            html        = sb.ToString();
            html        = this.applyTableToTemplate(html, "details", dtPickListReport);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetPickListReport()
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails/*, dtTotals*/;
            //List<object> totalsRow;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            qry.AddColumn("orderdate",       false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("estrentfrom",     false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("estrentto",       false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("billperiodstart", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("billperiodend",   false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("contractdate",    false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("image",           false, FwJsonDataTableColumn.DataTypes.JpgDataUrl);
            qry.AddColumn("itemvalue",       false, FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);

            select = new FwSqlSelect();
            select.Add("select *");
            select.Add("from  picklistrptview with (nolock)");
            select.Add("where picklistid = @picklistid");
            select.Add("order by orderno, pickdate, rectypesequence, itemorder, masterno");
            select.AddParameter("@picklistid",              request.parameters.picklistid);

            select.Parse();
            dtDetails = qry.QueryToFwJsonTable(select, true);

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
