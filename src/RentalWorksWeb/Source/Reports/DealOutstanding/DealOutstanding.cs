using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalWorksWeb.Source.Reports
{
    class DealOutstanding : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Deal Outstanding Items"; }
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
            FwJsonDataTable dtDealOutstanding;
            StringBuilder sb;

            dtDealOutstanding = GetDealOutstanding();

            html        = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
            sb          = new StringBuilder(base.renderBodyHtml(styletemplate, bodytemplate, printOptions));
            sb.Replace("[LBLDEPARTMENT]", (request.parameters.ShowDepartmentFrom == "O") ? "Company Department" : "Inventory Type");
            if      (request.parameters.IncludeValueCost == "R") { sb.Replace("[LBLITEMVALUE]", "Replacement"); } 
            else if (request.parameters.IncludeValueCost == "U") { sb.Replace("[LBLITEMVALUE]", "Unit Value" ); }
            else if (request.parameters.IncludeValueCost == "P") { sb.Replace("[LBLITEMVALUE]", "Unit Cost"  ); }
            else                                                 { sb.Replace("[LBLITEMVALUE]", ""           ); }

            sb.Replace("[TOTOALROWS]", "Total Rows: " + dtDealOutstanding.Rows.Count);
            if (request.parameters.IncludeValueCost == "on")
            {
                sb.Replace("[TOTALVALUE]", "");
            }
            else
            {
                int itemvalueindex = dtDealOutstanding.ColumnIndex["itemvalue"];
                decimal total = 0M;
                for (int i = 0; i < dtDealOutstanding.Rows.Count; i++)
                {
                    decimal tempval;
                    decimal? itemvalue = decimal.TryParse((string)dtDealOutstanding.Rows[i][itemvalueindex], out tempval) ? tempval : (decimal?)null;
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
            html        = this.applyTableToTemplate(html, "details", dtDealOutstanding);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetDealOutstanding()
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails, dtTotals;
            List<object> totalsRow;

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
            select.Add("select rowtype='detail', * ");
            select.Add("from  dbo.funcdealoutstandingrpt3(@fromdate,");
            select.Add("                                  @todate,");
            select.Add("                                  @datetouse,");
            select.Add("                                  @departmenttype,");
            select.Add("                                  @alleverout,");
            select.Add("                                  @showpending,");
            select.Add("                                  @shownones,");
            select.Add("                                  @contractid,");
            select.Add("                                  @showbarcodes,");
            select.Add("                                  @showresponsibleperson,");
            select.Add("                                  @showitemvalue,");
            select.Add("                                  @returnimagemode,");
            select.Add("                                  @officelocationids,");
            select.Add("                                  @companydepartmentids,");
            select.Add("                                  @customerids,");
            select.Add("                                  @dealids,");
            select.Add("                                  @orderunitids,");
            select.Add("                                  @orderids,");
            select.Add("                                  @inventorydepartmentids,");
            select.Add("                                  @categoryids,");
            select.Add("                                  @masterids) rpt");
            select.AddParameter("@fromdate",                request.parameters.fromdate);
            select.AddParameter("@todate",                  request.parameters.todate);
            select.AddParameter("@datetouse",               request.parameters.datetouse);
            select.AddParameter("@departmenttype",          request.parameters.ShowDepartmentFrom);
            select.AddParameter("@alleverout",              request.parameters.AllEverOut);
            select.AddParameter("@showpending",             request.parameters.IncludePendingExchanges);
            select.AddParameter("@shownones",               request.parameters.PrintNone);
            select.AddParameter("@contractid" ,             "");
            select.AddParameter("@showbarcodes",            request.parameters.ShowBarcodes);
            select.AddParameter("@showresponsibleperson",   request.parameters.ShowResponsiblePerson);
            select.AddParameter("@showitemvalue",           request.parameters.IncludeValueCost);
            select.AddParameter("@returnimagemode",         "T");
            select.AddParameter("@officelocationids",       GetCommaListDecrypt(request.parameters.officelocation));
            select.AddParameter("@companydepartmentids",    GetCommaListDecrypt(request.parameters.companydepartment));
            select.AddParameter("@customerids",             GetCommaListDecrypt(request.parameters.customer));
            select.AddParameter("@dealids",                 GetCommaListDecrypt(request.parameters.deal));
            select.AddParameter("@orderunitids",            GetCommaListDecrypt(request.parameters.order));
            select.AddParameter("@orderids",                GetCommaListDecrypt(request.parameters.orderunit));
            select.AddParameter("@inventorydepartmentids",  GetCommaListDecrypt(request.parameters.inventorytype));
            select.AddParameter("@categoryids",             GetCommaListDecrypt(request.parameters.category));
            select.AddParameter("@masterids",               GetCommaListDecrypt(request.parameters.icode));

            select.Parse();

            select.Add("order by location, displaydepartment, deal, orderno, itemorder, barcode");
            dtDetails = qry.QueryToFwJsonTable(select, true);

            //totals row
            //qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            //qry.AddColumn("itemvalue", false, FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            //select = new FwSqlSelect();
            //select.Add("select top 1 rowtype='totals', title='Total: " + dtDetails.Rows.Count + "', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', itemvalue=sum(rpt.itemvalue), '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', ''");
            //select.Add("from  dbo.funcdealoutstandingrpt3(@fromdate,");
            //select.Add("                                  @todate,");
            //select.Add("                                  @datetouse,");
            //select.Add("                                  @departmenttype,");
            //select.Add("                                  @alleverout,");
            //select.Add("                                  @showpending,");
            //select.Add("                                  @shownones,");
            //select.Add("                                  @contractid,");
            //select.Add("                                  @showbarcodes,");
            //select.Add("                                  @showresponsibleperson,");
            //select.Add("                                  @showitemvalue,");
            //select.Add("                                  @returnimagemode,");
            //select.Add("                                  @officelocationids,");
            //select.Add("                                  @companydepartmentids,");
            //select.Add("                                  @customerids,");
            //select.Add("                                  @dealids,");
            //select.Add("                                  @orderunitids,");
            //select.Add("                                  @orderids,");
            //select.Add("                                  @inventorydepartmentids,");
            //select.Add("                                  @categoryids,");
            //select.Add("                                  @masterids) rpt");
            //select.Parse();
            //select.AddParameter("@fromdate",                request.parameters.fromdate);
            //select.AddParameter("@todate",                  request.parameters.todate);
            //select.AddParameter("@datetouse",               request.parameters.datetouse);
            //select.AddParameter("@departmenttype",          request.parameters.ShowDepartmentFrom);
            //select.AddParameter("@alleverout",              request.parameters.AllEverOut);
            //select.AddParameter("@showpending",             request.parameters.IncludePendingExchanges);
            //select.AddParameter("@shownones",               request.parameters.PrintNone);
            //select.AddParameter("@contractid" ,             "");
            //select.AddParameter("@showbarcodes",            request.parameters.ShowBarcodes);
            //select.AddParameter("@showresponsibleperson",   request.parameters.ShowResponsiblePerson);
            //select.AddParameter("@showitemvalue",           request.parameters.IncludeValueCost);
            //select.AddParameter("@returnimagemode",         "T");
            //select.AddParameter("@officelocationids",       GetCommaListDecrypt(request.parameters.officelocation));
            //select.AddParameter("@companydepartmentids",    GetCommaListDecrypt(request.parameters.companydepartment));
            //select.AddParameter("@customerids",             GetCommaListDecrypt(request.parameters.customer));
            //select.AddParameter("@dealids",                 GetCommaListDecrypt(request.parameters.deal));
            //select.AddParameter("@orderunitids",            GetCommaListDecrypt(request.parameters.order));
            //select.AddParameter("@orderids",                GetCommaListDecrypt(request.parameters.orderunit));
            //select.AddParameter("@inventorydepartmentids",  GetCommaListDecrypt(request.parameters.inventorytype));
            //select.AddParameter("@categoryids",             GetCommaListDecrypt(request.parameters.category));
            //select.AddParameter("@masterids",               GetCommaListDecrypt(request.parameters.icode));
            //dtTotals = qry.QueryToFwJsonTable(select, true);
            //totalsRow = new List<object>();
            //if (dtTotals.Rows.Count != 1) throw new Exception("Report returned no records.");
            //for (int colNo = 0; colNo < dtTotals.Columns.Count; colNo++)
            //{
            //    totalsRow.Add(dtTotals.Rows[0][colNo]);
            //}
            //dtDetails.Rows.Add(totalsRow);

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
