using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using OfficeOpenXml;
using Web.Integration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Text;

namespace Web.Source.Reports
{
    class VendorInvoiceProcessing : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Vendor Invoice Processing"; }
        //---------------------------------------------------------------------------------------------
        protected override PrintOptions getDefaultPrintOptions()
        {
            PrintOptions newprintoptions = base.getDefaultPrintOptions();

            newprintoptions.HeaderHeight = 0;

            return newprintoptions;
        }
        //---------------------------------------------------------------------------------------------
        protected override string renderHeaderHtml(string styletemplate, string headertemplate, FwReport.PrintOptions printOptions)
        {
            //const string METHOD = "RwChargeProcessing.renderHeaderHtml";
            string html;//, locationid, chgbatchno, chgbatchdate;
            //StringBuilder sb;

            //FwValidate.TestPropertyDefined(METHOD, request.parameters, "batchno");

            //locationid = session.security.webUser.locationid;
            //GetChgBatch(locationid, request.parameters.batchno, out chgbatchno, out chgbatchdate);
            //sb = new StringBuilder(base.renderHeaderHtml(styletemplate, headertemplate, printOptions));
            //sb.Replace("[CHGBATCHNO]", chgbatchno);
            //sb.Replace("[BATCHDATE]", chgbatchdate);
            //html = sb.ToString();
            html = null;

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected override string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        {
            string html = string.Empty, buildhtml;
            FwJsonDataTable dtCharges;
            string locationid, location, chgbatchno, chgbatchdate;

            locationid = session.security.webUser.locationid;
            location   = FwSqlCommand.GetStringData(FwSqlConnection.RentalWorks, "location", "locationid", locationid, "location");

            GetChgBatch(locationid, request.parameters.batchno, out chgbatchno, out chgbatchdate);

            dtCharges = GetCharges(request.parameters.batchno);
            buildhtml = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
            buildhtml = base.renderHeaderFields(buildhtml, printOptions);
            buildhtml = this.applyTableToTemplate(buildhtml, "rowtype", new Dictionary<string,string> { {"detail", getBodyDetailRowTemplate()}, {"invoicetotal", getBodyTotalRowTemplate()} }, "[ROWS]", dtCharges);
            buildhtml = buildhtml.Replace("[LOCATION]",   location);
            buildhtml = buildhtml.Replace("[CHGBATCHNO]", chgbatchno);
            buildhtml = buildhtml.Replace("[BATCHDATE]",  chgbatchdate);

            html = html + buildhtml;

            return html;
        }
        //---------------------------------------------------------------------------------------------
        public override ExcelDownloadResult GetExcelDownload()
        {
            ExcelDownloadResult download = new ExcelDownloadResult();
            string html = string.Empty;
            FwJsonDataTable dtCharges;
            string locationid, location, chgbatchno, chgbatchdate;

            locationid = session.security.webUser.locationid;
            location = FwSqlCommand.GetStringData(FwSqlConnection.RentalWorks, "location", "locationid", locationid, "location");

            GetChgBatch(locationid, request.parameters.batchno, out chgbatchno, out chgbatchdate);

            dtCharges = GetCharges(request.parameters.batchno);
            ExcelWorksheet worksheet = download.ExcelPackage.Workbook.Worksheets.Add("Batch " + chgbatchno);
            dtCharges.FillExcelWorksheet(worksheet);

            return download;
        }
        //---------------------------------------------------------------------------------------------
        public override void GetData()
        {
            switch ((string)request.method)
            {
                case "CreateCharge":
                    CreateEquipBatch();
                    break;
                case "LoadForm":
                    response.orderbylist = GetOrderByList();
                    response.qbo         = ValidateQBOConnected(session.security.webUser.locationid);
                    break;
                case "ExportToQBO":
                    response.export = QBOIntegrationData.ExportVendorInvoicesToQBO(request.batchno, session.security.webUser.locationid);
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------
        protected void CreateEquipBatch()
        {
            FwSqlCommand qry, qry2;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.createvendorinvoicechargebatch");
            qry.AddParameter("@usersid",      session.security.webUser.usersid);
            qry.AddParameter("@departmentid", "");
            qry.AddParameter("@chgbatchid",   SqlDbType.Char, ParameterDirection.Output);
            qry.Execute();

            response.chgbatchid = qry.GetParameter("@chgbatchid").ToString();

            if (response.chgbatchid != null)
            {
                qry2 = new FwSqlCommand(FwSqlConnection.RentalWorks);
                qry2.Add("select chgbatchno, chgbatchdate");
                qry2.Add("  from chgbatch");
                qry2.Add(" where chgbatchid = @chgbatchid");
                qry2.AddParameter("@chgbatchid", response.chgbatchid);
                qry2.Execute();

                response.chgbatchno   = qry2.GetField("chgbatchno").ToString();
                response.chgbatchdate = qry2.GetField("chgbatchdate").ToShortDateString();
            }
        }
        //---------------------------------------------------------------------------------------------
        public List<FwReportOrderByItem> GetOrderByList() 
        {
            List<FwReportOrderByItem> orderByItems;
            orderByItems = new List<FwReportOrderByItem>();
            orderByItems.Add(new FwReportOrderByItem() {value="vendor", text="Vendor",     selected="T", orderbydirection="asc"});
            orderByItems.Add(new FwReportOrderByItem() {value="invno",  text="Invoice No", selected="T", orderbydirection="asc"});
            orderByItems.Add(new FwReportOrderByItem() {value="pono",   text="PO No",      selected="T", orderbydirection="asc"});
            return orderByItems;
        }
        //---------------------------------------------------------------------------------------------
        public static dynamic ValidateQBOConnected(string locationid)
        {
            dynamic qbokeys, result = new ExpandoObject();

            qbokeys = QBOIntegrationData.GetQBOKeys(FwSqlConnection.RentalWorks, locationid);
            if ((qbokeys != null) && (qbokeys.accesstoken != ""))
            {
                DateTime expiredt = FwConvert.ToDateTime(qbokeys.accesstokendate);
                int expiresindays = (expiredt.AddDays(180) - DateTime.Now.Date).Days;

                if (expiresindays > 0)
                {
                    result.connected = true;
                    result.dateconnected = FwConvert.ToUSShortDate(qbokeys.accesstokendate);
                    result.expiresindays = expiresindays.ToString();
                }
                else {
                    result.connected = false;
                }
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static void GetChgBatch(string locationid, string chgbatchid, out string chgbatchno, out string chgbatchdate)
        {
            FwSqlCommand qry;
            FwSqlSelect select;
            FwJsonDataTable dt;

            chgbatchno   = string.Empty;
            chgbatchdate = string.Empty;

            select = new FwSqlSelect();
            select.Add("select top 1 chgbatchno, chgbatchdate");
            select.Add("from  chgbatchview cb with (nolock)");
            select.Add("where cb.locationid = @locationid");
            select.Add("  and cb.batchtype  = 'VENDORINVOICE'");
            select.Add("  and cb.chgbatchid = @chgbatchid");
            select.AddParameter("@locationid", locationid);
            select.AddParameter("@chgbatchid", chgbatchid);
            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            dt = qry.QueryToFwJsonTable(select, true);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chgbatchno   = dt.GetValue(0, "chgbatchno").ToString().TrimEnd();
                chgbatchdate = dt.GetValue(0, "chgbatchdate").ToShortDateString();
            }
        }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetCharges(string chgbatchid)
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dt;

            select = new FwSqlSelect();
            select.Add("select rowtype='detail', vi.locationid, l.location, v.vendor, v.vendorid,");
            select.Add("       vi.invdate, vi.invoicetotal, pono = po.orderno,");
            select.Add("       vi.invno, vi.vendorinvoiceid, poid = vi.orderid");
            select.Add(" from vendorinvoice vi with (nolock) join location l                 with (nolock) on (vi.locationid        = l.locationid)");
            select.Add("                                     join dealorder po               with (nolock) on (vi.orderid           = po.orderid)");
            select.Add("                                     join vendor v                   with (nolock) on (po.vendorid          = v.vendorid)");
            select.Add("                                     join vendorinvoicechgbatch vicb with (nolock) on (vicb.vendorinvoiceid = vi.vendorinvoiceid)");
            select.Add("where vicb.chgbatchid = @chgbatchid");
            select.AddParameter("@chgbatchid", chgbatchid);
            select.Parse();
            select.AddOrderByFromCheckboxList(request.parameters.orderbylist, GetOrderByList());

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("Row Type",            "rowtype",         FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Location ID",         "locationid",      FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("Location",            "location",        FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Vendor ID",           "vendorid",        FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("Vendor",              "vendor",          FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Invoice Date",        "invdate",         FwJsonDataTableColumn.DataTypes.Date,                       true,  false, false);
            qry.AddColumn("Invoice Total",       "invoicetotal",    FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign, true,  false, false);
            qry.AddColumn("PO No",               "pono",            FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Invoice No",          "invno",           FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Vendor Invoice ID",   "vendorinvoiceid", FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("PO ID",               "poid",            FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            dt = qry.QueryToFwJsonTable(select, true);

            dt.InsertTotalRow("rowtype", "detail", "invoicetotal", new string[] {"invoicetotal"});

            return dt;
        }
        //---------------------------------------------------------------------------------------------
        protected string getBodyDetailRowTemplate()
        { 
            StringBuilder html;
            string rowtemplate;
            
            html = new StringBuilder();
            html.AppendLine("<tr data-rowtype=\"{{rowtype}}\">");
            html.AppendLine("  <td><div data-datafield=\"vendor\">{{vendor}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"invno\">{{invno}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"invdate\">{{invdate}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"pono\">{{pono}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"invoicetotal\" class=\"alignright\">{{invoicetotal}}</div></td>");
            html.AppendLine("</tr>");
            rowtemplate = html.ToString();

            return rowtemplate;
        }
        //---------------------------------------------------------------------------------------------
        protected string getExcelBodyDetailRowTemplate()
        {
            StringBuilder html;
            string rowtemplate;

            html = new StringBuilder();
            html.AppendLine("      <ss:Row ss:StyleID=\"{{rowtype}}\">");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{vendor}}</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{invno}}</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{invdate}}</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{pono}}</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{invoicetotal}}</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("      </ss:Row>");
            rowtemplate = html.ToString();

            return rowtemplate;
        }
        //---------------------------------------------------------------------------------------------
        protected string getBodyTotalRowTemplate()
        { 
            StringBuilder html;
            string rowtemplate;
            
            html = new StringBuilder();
            html.AppendLine("<tr data-rowtype=\"{{rowtype}}\" class=\"totalrow\">");
            html.AppendLine("  <td colspan=\"4\"><div data-datafield=\"invoicetotalcaption\" class=\"alignright\">Total:</div></td>");
            html.AppendLine("  <td><div data-datafield=\"invoicetotal\" class=\"alignright\">{{invoicetotal}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"nocharge\"></div></td>");
            html.AppendLine("</tr>");
            rowtemplate = html.ToString();

            return rowtemplate;
        }
        //---------------------------------------------------------------------------------------------
        protected string getExcelBodyTotalRowTemplate()
        {
            StringBuilder html;
            string rowtemplate;

            html = new StringBuilder();
            html.AppendLine("      <ss:Row ss:StyleID=\"{{rowtype}}\">");
            html.AppendLine("        <ss:Cell ss:MergeAcross=\"4\">");
            html.AppendLine("          <ss:Data ss:Type=\"String\">Total:</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{invoicetotal}}</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\"></ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("      </ss:Row>");
            rowtemplate = html.ToString();

            return rowtemplate;
        }
        //---------------------------------------------------------------------------------------------
    }
}
