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
    class ReceiptProcessing : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Receipt Processing"; }
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

                dtCharges = GetReceipts(chgbatchids[i].chgbatchid);
                buildhtml = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
                buildhtml = base.renderHeaderFields(buildhtml, printOptions);
                buildhtml = this.applyTableToTemplate(buildhtml, "rowtype", new Dictionary<string,string> { {"detail", getBodyDetailRowTemplate()}, {"amount", getBodyTotalRowTemplate()} }, "[ROWS]", dtCharges);
                buildhtml = buildhtml.Replace("[LOCATION]",   location);
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

                dtCharges = GetReceipts(chgbatchids[i].chgbatchid);
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
                case "ProcessReceipts":
                    ProcessReceipts();
                    break;
                case "LoadForm":
                    response.orderbylist = GetOrderByList();
                    response.qbo         = ValidateQBOConnected(session.security.webUser.locationid);
                    break;
                case "ExportToQBO":
                    response.export = QBOIntegrationData.ExportReceiptsToQBO(request.batchno, request.batchfrom, request.batchto, session.security.webUser.locationid);
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------
        protected void ProcessReceipts()
        {
            FwSqlCommand qry, qry2;
            FwDateTime processfrom = request.processfrom;
            FwDateTime processto   = request.processto;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.createarchargebatch");
            qry.AddParameter("@usersid",      session.security.webUser.usersid);
            qry.AddParameter("@fromdate",     processfrom.GetSqlValue());
            qry.AddParameter("@todate",       processto.GetSqlValue());
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
            orderByItems.Add(new FwReportOrderByItem() {value="customer",   text="Customer",      selected="T", orderbydirection="asc"});
            orderByItems.Add(new FwReportOrderByItem() {value="deal",       text="Deal",          selected="T", orderbydirection="asc"});
            orderByItems.Add(new FwReportOrderByItem() {value="ardate",     text="Receipt Date",  selected="T", orderbydirection="asc"});
            orderByItems.Add(new FwReportOrderByItem() {value="checkno",    text="Ref/Check No.", selected="T", orderbydirection="asc"});
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
            select.Add("  and cb.batchtype  = 'AR'");
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
        protected FwJsonDataTable GetReceipts(string chgbatchid)
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dt;

            select = new FwSqlSelect();
            select.Add("select rowtype='detail', *");
            select.Add("from  funcreceiptbatchrpt() f");
            select.Add("where f.chgbatchid = @chgbatchid");
            select.AddParameter("@chgbatchid", chgbatchid);
            select.Parse();
            select.AddOrderByFromCheckboxList(request.parameters.orderbylist, GetOrderByList());

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.AddColumn("Row Type",            "rowtype",        FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Deal ID",             "dealid",         FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("Deal",                "deal",           FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Deal No",             "dealno",         FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Customer ID",         "customerid",     FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("Customer",            "customer",       FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Chg Batch ID",        "chgbatchid",     FwJsonDataTableColumn.DataTypes.Text,                       false, true,  false);
            qry.AddColumn("Chg Batch No",        "chgbatchno",     FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Chg Batch Date",      "chgbatchdate",   FwJsonDataTableColumn.DataTypes.Date,                       true,  false, false);
            qry.AddColumn("Payment Type",        "paytype",        FwJsonDataTableColumn.DataTypes.Text,                       true,  false, false);
            qry.AddColumn("Amount",              "amount",         FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign, true,  false, false);
            dt = qry.QueryToFwJsonTable(select, true);
            dt.InsertTotalRow("rowtype", "detail", "amount", new string[] {"amount"});

            return dt;
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
            html.AppendLine("  <td><div data-datafield=\"chgbatchdate\">{{chgbatchdate}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"checkno\">{{checkno}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"paytype\">{{paytype}}</div></td>");
            html.AppendLine("  <td><div data-datafield=\"amount\" class=\"alignright\">{{amount}}</div></td>");
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
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{customer}}</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{deal}}</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{chgbatchdate}}</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{checkno}}</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{paytype}}</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{amount}}</ss:Data>");
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
            html.AppendLine("  <td colspan=\"5\"><div data-datafield=\"invoicetotalcaption\" class=\"alignright\">Total:</div></td>");
            html.AppendLine("  <td><div data-datafield=\"amount\" class=\"alignright\">{{amount}}</div></td>");
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
            html.AppendLine("        <ss:Cell ss:MergeAcross=\"5\">");
            html.AppendLine("          <ss:Data ss:Type=\"String\">Total:</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("        <ss:Cell>");
            html.AppendLine("          <ss:Data ss:Type=\"String\">{{amount}}</ss:Data>");
            html.AppendLine("        </ss:Cell>");
            html.AppendLine("      </ss:Row>");
            rowtemplate = html.ToString();

            return rowtemplate;
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
            qry.Add("   and cb.batchtype = 'AR'");
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
