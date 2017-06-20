using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Text;

namespace RentalWorksWeb.Source.Reports
{
    class DealOutstanding : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Deal Outstanding"; }
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
            html        = sb.ToString();
            html        = this.applyTableToTemplate(html, "details", dtDealOutstanding);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetDealOutstanding()
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            qry.AddColumn("orderdate",       false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("estrentfrom",     false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("estrentto",       false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("billperiodstart", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("billperiodend",   false, FwJsonDataTableColumn.DataTypes.Date);
            qry.AddColumn("contractdate",    false, FwJsonDataTableColumn.DataTypes.Date);

            select = new FwSqlSelect();
            select.Add("select * ");
            select.Add("from  dbo.funcdealoutstandingrpt2(@fromdate,");
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
            //select.AddWhereIn("and", "locationid",      request.parameters.officelocation, true);
            //select.AddWhereIn("and", "orderdepartmentid",    request.parameters.companydepartment, true);
            //select.AddWhereIn("and", "customerid",      request.parameters.customer, true);
            //select.AddWhereIn("and", "dealid",          request.parameters.deal, true);
            //select.AddWhereIn("and", "orderid",         request.parameters.order, true);
            //select.AddWhereIn("and", "inventorydepartmentid", request.parameters.inventorytype, true);
            //select.AddWhereIn("and", "categoryid",      request.parameters.category, true);
            //select.AddWhereIn("and", "masterid",        request.parameters.icode, true);

            select.Add("order by location, displaydepartment, deal, dealno, orderby, orderno, itemorder, description");
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
