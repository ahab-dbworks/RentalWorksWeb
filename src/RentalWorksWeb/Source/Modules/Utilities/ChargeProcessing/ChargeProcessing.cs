using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using OfficeOpenXml;
using Web.Integration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Text;

namespace Web.Source.Reports
{
    class ChargeProcessing : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Charge Batch Report"; }
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
            dynamic chgbatchids;

            locationid = session.security.webUser.locationid;
            location   = FwSqlCommand.GetStringData(FwSqlConnection.RentalWorks, "location", "locationid", locationid, "location");

            chgbatchids = getChgBatchIds(request.parameters.batchno, request.parameters.batchfrom, request.parameters.batchto, locationid);

            for (int i = 0; i < chgbatchids.Count; i++)
            {
                GetChgBatch(locationid, chgbatchids[i].chgbatchid, out chgbatchno, out chgbatchdate);

                dtCharges = GetCharges(chgbatchids[i].chgbatchid);
                buildhtml = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
                buildhtml = base.renderHeaderFields(buildhtml, printOptions);
                buildhtml = this.applyTableToTemplate(buildhtml, "rowtype", new Dictionary<string,string> { {"detail", getBodyDetailRowTemplate()}, {"invoicetotal", getBodyTotalRowTemplate()} }, "[ROWS]", dtCharges);
                buildhtml = buildhtml.Replace("[LOCATION]",   location);
                buildhtml = buildhtml.Replace("[EXPORTED]",   getExportedField(chgbatchids[i].chgbatchid));
                buildhtml = buildhtml.Replace("[CHGBATCHNO]", chgbatchno);
                buildhtml = buildhtml.Replace("[BATCHDATE]",  chgbatchdate);

                html = html + buildhtml;
            }

            return html;
        }
        //---------------------------------------------------------------------------------------------
        public override ExcelDownloadResult GetExcelDownload()
        {
            ExcelDownloadResult download = new ExcelDownloadResult();
            string html = string.Empty;
            FwJsonDataTable dtCharges;
            string locationid, location, chgbatchno, chgbatchdate;
            dynamic chgbatchids;

            locationid = session.security.webUser.locationid;
            location = FwSqlCommand.GetStringData(FwSqlConnection.RentalWorks, "location", "locationid", locationid, "location");

            chgbatchids = getChgBatchIds(request.parameters.batchno, request.parameters.batchfrom, request.parameters.batchto, locationid);

            for (int i = 0; i < chgbatchids.Count; i++)
            {
                GetChgBatch(locationid, chgbatchids[i].chgbatchid, out chgbatchno, out chgbatchdate);

                dtCharges = GetCharges(chgbatchids[i].chgbatchid);
                ExcelWorksheet worksheet = download.ExcelPackage.Workbook.Worksheets.Add("Batch " + chgbatchno);
                dtCharges.FillExcelWorksheet(worksheet);
            }

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
                    response.export = QBOIntegrationData.ExportInvoicesToQBO(request.batchno, request.batchfrom, request.batchto, session.security.webUser.locationid, session.security.webUser.usersid);
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------
        protected void CreateEquipBatch()
        {
            FwSqlCommand qry, qry2;
            FwDateTime asof = request.asofdate;
            bool receiptsenabled;
            dynamic invoiceids;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.createchargebatch2");
            qry.AddParameter("@sessionid",    session.security.webUser.webusersid);
            qry.AddParameter("@usersid",      session.security.webUser.usersid);
            qry.AddParameter("@asof",         asof.GetSqlValue());
            qry.AddParameter("@dealid",       "");
            qry.AddParameter("@departmentid", "");
            qry.AddParameter("@divisioncode", "");
            qry.AddParameter("@agentid",      "");
            qry.AddParameter("@chgbatchid",   SqlDbType.Char, ParameterDirection.Output);
            qry.AddParameter("@status",       SqlDbType.Int, ParameterDirection.Output);
            qry.AddParameter("@msg",          SqlDbType.VarChar, ParameterDirection.Output);
            qry.Execute();

            response.chgbatchid = qry.GetParameter("@chgbatchid").ToString();
            response.status     = qry.GetParameter("@status").ToString();
            response.msg        = qry.GetParameter("@msg").ToString();

            if (response.status == "0")
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

            receiptsenabled = RwAppData.HasAppOption("Receipts");
            invoiceids      = ExportInvoices(FwSqlConnection.RentalWorks, response.chgbatchid, "", "", session.security.webUser.locationid);
            if (!receiptsenabled)
            {
                for (int i = 0; i < invoiceids.Count; i++)
                {
                    CloseInvoice(FwSqlConnection.RentalWorks, invoiceids[i].invoiceid, session.security.webUser.usersid);
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static void CloseInvoice(FwSqlConnection conn, string invoiceid, string usersid)
        {
            FwSqlCommand sp;

            sp = new FwSqlCommand(conn, "dbo.closeinvoice");
            sp.AddParameter("@invoiceid", invoiceid);
            sp.AddParameter("@usersid",   usersid);
            sp.Execute();
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic ExportInvoices(FwSqlConnection conn, string batchno, FwDateTime batchfrom, FwDateTime batchto, string locationid)
        {
            FwSqlCommand sp;
            dynamic result;

            sp = new FwSqlCommand(conn, "dbo.exportinvoices");
            sp.AddParameter("@chgbatchid", batchno);
            sp.AddParameter("@fromdate",   batchfrom.GetSqlValue());
            sp.AddParameter("@todate",     batchto.GetSqlValue());
            sp.AddParameter("@locationid", locationid);
            result = sp.QueryToDynamicList2();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public List<FwReportOrderByItem> GetOrderByList() 
        {
            List<FwReportOrderByItem> orderByItems;
            orderByItems = new List<FwReportOrderByItem>();
            orderByItems.Add(new FwReportOrderByItem() {value="customer",   text="Customer",    selected="T", orderbydirection="asc"});
            orderByItems.Add(new FwReportOrderByItem() {value="deal",       text="Deal",        selected="T", orderbydirection="asc"});
            orderByItems.Add(new FwReportOrderByItem() {value="invoiceno",  text="Invoice No",  selected="T", orderbydirection="asc"});
            orderByItems.Add(new FwReportOrderByItem() {value="orderno",    text="Order No",    selected="T", orderbydirection="asc"});
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
            select.Add("  and cb.batchtype  = 'INVOICE'");
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
            select.Add("select rowtype='detail', i.locationid, l.location, i.dealid, d.deal, d.dealno, c.customerid, c.customer, cb.chgbatchid, cb.chgbatchno,");
            select.Add("  cb.chgbatchdate, i.invoicedesc, i.invoiceid, i.invoiceno, i.invoicedate, i.departmentid, dp.department, dp.deptcode,");
            select.Add("  orderid      = dbo.getinvoiceorderid(i.invoiceid),");
            select.Add("  orderno      = dbo.getinvoiceorderno(i.invoiceid),");
            select.Add("  billingstart = dbo.getinvoicebillstart(i.invoiceid),");
            select.Add("  billingend   = dbo.getinvoicebillend(i.invoiceid),");
            select.Add("  pono         = dbo.getinvoicepono(i.invoiceid),");
            select.Add("  nocharge = (case i.nocharge");
            select.Add("                  when 'T' then 'NC'");
            select.Add("                  else ''");
            select.Add("                end),");
            select.Add("  resent = (case when exists (select *");
            select.Add("                              from  invoicechgbatch icb with (nolock), chgbatch cb with (nolock)");
            select.Add("                              where icb.chgbatchid = cb.chgbatchid ");
            select.Add("                                and icb.invoiceid  = i.invoiceid ");
            select.Add("                                and cb.chgbatchdatetime > (select cb2.chgbatchdatetime");
            select.Add("                                                           from  chgbatch cb2");
            select.Add("                                                           where cb2.chgbatchid = icb.chgbatchid))");
            select.Add("                then 'T' ");
            select.Add("                else 'F' ");
            select.Add("           end), ");
            select.Add("  i.splitrentalflg, invoicetotal=0.0");
            select.Add("from  invoice i with (nolock) join location l          with (nolock) on (i.locationid   = l.locationid)");
            select.Add("                              join deal d              with (nolock) on (i.dealid       = d.dealid)"); 
            select.Add("                              join department dp       with (nolock) on (i.departmentid = dp.departmentid)"); 
            select.Add("                              join invoicechgbatch icb with (nolock) on (i.dealid       = d.dealid)");  
            select.Add("                              join customer c          with (nolock) on (d.customerid   = c.customerid)"); 
            select.Add("                              join chgbatch cb         with (nolock) on ((icb.chgbatchid = cb.chgbatchid) and (i.invoiceid = icb.invoiceid))"); 
            select.Add("where icb.chgbatchid = @chgbatchid");
            select.AddParameter("@chgbatchid", chgbatchid);
            select.Parse();
            //select.AddWhereIn("and", "icb.chgbatchid", String.Join(",", chgbatchids), false, false);
            select.AddOrderByFromCheckboxList(request.parameters.orderbylist, GetOrderByList());

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("Row Type",            "rowtype",        FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Location ID",         "locationid",     FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("Location",            "location",       FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Deal ID",             "dealid",         FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("Deal",                "deal",           FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Deal No",             "dealno",         FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Customer ID",         "customerid",     FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("Customer",            "customer",       FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Chg Batch ID",        "chgbatchid",     FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("Chg Batch No",        "chgbatchno",     FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Chg Batch Date",      "chgbatchdate",   FwJsonDataTableColumn.DataTypes.Date,                       true,  false, false);
            qry.AddColumn("Invoice Description", "invoicedesc",    FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Invoice ID",          "invoiceid",      FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("Invoice No",          "invoiceno",      FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Invoice Date",        "invoicedate",    FwJsonDataTableColumn.DataTypes.Date,                       true,  false, false);
            qry.AddColumn("Department ID",       "departmentid",   FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("Department",          "department",     FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Dept Code",           "deptcode",       FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Order ID",            "orderid",        FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("Order No",            "orderno",        FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Billing Start",       "billingstart",   FwJsonDataTableColumn.DataTypes.Date,                       true,  false, false);
            qry.AddColumn("Billing End",         "billingend",     FwJsonDataTableColumn.DataTypes.Date,                       true,  false, false);
            qry.AddColumn("PO No",               "pono",           FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Is No Charge",        "nocharge",       FwJsonDataTableColumn.DataTypes.Boolean,                    true,  false, false);
            qry.AddColumn("Re-Sent",             "resent",         FwJsonDataTableColumn.DataTypes.Boolean,                    true,  false, false);
            qry.AddColumn("Is Split Rental",     "splitrentalflg", FwJsonDataTableColumn.DataTypes.Boolean,                    true,  false, false);
            qry.AddColumn("Invoice Total",       "invoicetotal",   FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign, true,  false, false);
            dt = qry.QueryToFwJsonTable(select, true);
            AddTotalColumnToCharges(dt);
            dt.InsertTotalRow("rowtype", "detail", "invoicetotal", new string[] {"invoicetotal"});

            return dt;
        }
        //---------------------------------------------------------------------------------------------
        protected void AddTotalColumnToCharges(FwJsonDataTable dt)
        {
            for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
            {
                string invoiceid, orderid, splitrate;
                decimal rental, meter, sales, space, vehicle, labor, parts, asset, misc, discount, tax1, tax2, tax, total;

                invoiceid = FwCryptography.AjaxDecrypt(dt.GetValue(rowno, "invoiceid").ToString().TrimEnd());
                orderid   = FwCryptography.AjaxDecrypt(dt.GetValue(rowno, "orderid").ToString().TrimEnd());
                splitrate = dt.GetValue(rowno, "splitrentalflg").ToString().TrimEnd();
                calinvoicetotals(invoiceid, orderid, splitrate, 
                                 out rental, out meter, out sales, out space, out vehicle, out labor, out parts, out asset,out misc, out discount, 
                                 out tax1, out tax2, out tax, out total);
                dt.SetValue(rowno, "invoicetotal", FwConvert.ToCurrencyStringNoDollarSign(total));
            }
        }
        //---------------------------------------------------------------------------------------------
        protected void calinvoicetotals(string invoiceid, string orderid, string splitrate,
            out decimal rental, out decimal meter, out decimal sales, out decimal space, out decimal vehicle, out decimal labor, out decimal parts,
            out decimal asset, out decimal misc, out decimal discount, out decimal tax1, out decimal tax2, out decimal tax, out decimal total)
        {
            FwSqlCommand cmd;

            cmd = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.calinvoicetotals", FwQueryTimeouts.Report);
            cmd.AddParameter("@invoiceid", invoiceid);
            cmd.AddParameter("@orderid",   orderid);
            cmd.AddParameter("@splitrate", splitrate);
            cmd.AddParameter("@rental",   SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@meter",    SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@sales",    SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@space",    SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@vehicle",  SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@labor",    SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@parts",    SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@asset",    SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@misc",     SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@discount", SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@tax1",     SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@tax2",     SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@tax",      SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.AddParameter("@total",    SqlDbType.Decimal, ParameterDirection.Output, 16, 10);
            cmd.ExecuteNonQuery();
            rental   = cmd.GetParameter("@rental").ToDecimal();
            meter    = cmd.GetParameter("@meter").ToDecimal();
            sales    = cmd.GetParameter("@sales").ToDecimal();
            space    = cmd.GetParameter("@space").ToDecimal();
            vehicle  = cmd.GetParameter("@vehicle").ToDecimal();
            labor    = cmd.GetParameter("@labor").ToDecimal();
            parts    = cmd.GetParameter("@parts").ToDecimal();
            asset    = cmd.GetParameter("@asset").ToDecimal();
            misc     = cmd.GetParameter("@misc").ToDecimal();
            discount = cmd.GetParameter("@discount").ToDecimal();
            tax1     = cmd.GetParameter("@tax1").ToDecimal();
            tax2     = cmd.GetParameter("@tax2").ToDecimal();
            tax      = cmd.GetParameter("@tax").ToDecimal();
            total    = cmd.GetParameter("@total").ToDecimal();
        }
        //---------------------------------------------------------------------------------------------
        protected string getBodyDetailRowTemplate()
        { 
            StringBuilder html;
            string rowtemplate;
            
            html = new StringBuilder();
            html.AppendLine("<tr data-rowtype=\"{{rowtype}}\">");
            html.AppendLine("  <td><div data-datafield=\"customer\">{{customer}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"deal\">{{deal}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"invoiceno\">{{invoiceno}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"invoicedate\">{{invoicedate}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"orderno\">{{orderno}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"pono\">{{pono}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"invoicedesc\">{{invoicedesc}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"billingstart\">{{billingstart}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"billingend\">{{billingend}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"invoicetotal\" class=\"alignright\">{{invoicetotal}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"nocharge\">{{nocharge}}</div></td>");
            html.AppendLine("</tr>");
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
            html.AppendLine("  <td colspan=\"9\"><div data-datafield=\"invoicetotalcaption\" class=\"alignright\">Total:</div></td>");
            html.AppendLine("  <td><div data-datafield=\"invoicetotal\" class=\"alignright\">{{invoicetotal}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"nocharge\"></div></td>");
            html.AppendLine("</tr>");
            rowtemplate = html.ToString();

            return rowtemplate;
        }
        //---------------------------------------------------------------------------------------------
        protected string getExportedField(string chgbatchid)
        {
            FwSqlCommand qry;
            FwJsonDataTable dt;
            StringBuilder html;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("exportdatetime", false);
            qry.Add("select exportdatetime");
            qry.Add("from  chgbatchexport e");
            qry.Add("where e.chgbatchid = @chgbatchid");
            qry.AddParameter("@chgbatchid", chgbatchid);
            dt = qry.QueryToFwJsonTable();
            html = new StringBuilder();
            for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
            {
                html.AppendLine("<div style=\"margin:5px 0;text-decoration:underline;\"><span class=\"exportedcaption\">Exported: </span>" + dt.GetValue(rowno, "exportdatetime").ToShortDateTimeString() + "</div>");;
            }

            return html.ToString();
        }
        //---------------------------------------------------------------------------------------------
        protected dynamic getChgBatchIds(string chgbatchid, FwDateTime batchfrom, FwDateTime batchto, string locationid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from chgbatch cb");
            qry.Add(" where cb.locationid = @locationid");
            qry.Add("   and cb.batchtype = 'INVOICE'");
            qry.AddParameter("@locationid", locationid);

            if (request.parameters.viewbatch == "T")
            {
                qry.Add("and cb.chgbatchid = @chgbatchid");
                qry.AddParameter("@chgbatchid", chgbatchid);
            }
            else if (request.parameters.viewdates == "T")
            {
                qry.Add("and cb.chgbatchdate >= @batchfrom");
                qry.Add("and cb.chgbatchdate <= @batchto");
                qry.AddParameter("@batchfrom", batchfrom.GetSqlValue());
                qry.AddParameter("@batchto",   batchto.GetSqlValue());
            }

            result = qry.QueryToDynamicList2();

            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}
