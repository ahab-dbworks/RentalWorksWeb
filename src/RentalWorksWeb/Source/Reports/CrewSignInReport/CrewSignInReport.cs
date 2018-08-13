//using Fw.Json.Services;
//using Fw.Json.SqlServer;
//using Fw.Json.Utilities;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Web.Source.Reports
//{
//    class AgentBillingReport : RwReport
//    {
//        //---------------------------------------------------------------------------------------------
//        protected override string getReportName() { return "Agent Billing"; }
//        //---------------------------------------------------------------------------------------------
//        protected override string renderHeaderHtml(string styletemplate, string headertemplate, FwReport.PrintOptions printOptions)
//        {
//            StringBuilder sb;
//            string html;
           
//            sb = new StringBuilder(base.renderHeaderHtml(styletemplate, headertemplate, printOptions));
//            sb.Replace("[FROMDATE]", request.parameters.FromDate);
//            sb.Replace("[TODATE]", request.parameters.ToDate);
//            html = sb.ToString();

//            return html;
//        }
//        //---------------------------------------------------------------------------------------------
//        protected override string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
//        {
//            string html;
//            dynamic statuslist;
//            FwJsonDataTable dtInvoiceSummaryReport;
//            StringBuilder sb;

//            statuslist = request.parameters.statuslist;
//            dtInvoiceSummaryReport = GetInvoiceSummaryReport(statuslist);

//            html = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
//            sb = new StringBuilder(base.renderBodyHtml(styletemplate, bodytemplate, printOptions));
//            sb.Replace("[TotalRows]", "Total Rows: " + dtInvoiceSummaryReport.Rows.Count);
//            html = sb.ToString();
//            html = this.applyTableToTemplate(html, "details", dtInvoiceSummaryReport);

//            return html;
//        }
//        //---------------------------------------------------------------------------------------------
//        protected FwJsonDataTable GetInvoiceSummaryReport(dynamic statuslist)
//        {
//            FwSqlSelect select;
//            FwSqlCommand qry;
//            FwJsonDataTable dtDetails;

//            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
//            select = new FwSqlSelect();

//            select.Add("select rowtype='detail', location, department, invoiceno, orderno, invoicedate, customer, deal, invoicedesc, billingstart, billingend, periodtype, invoicetotal  ");
//            select.Add(" from  invoicesummaryrptview");
//            select.Add("order by location, department, customer, deal");

//            select.Parse();

//            if (request.parameters.DateType == "InvoiceDate")
//            {
//                select.AddWhere("where", "invoicedate >= @startdate");
//                select.AddWhere("and", "invoicedate <= @enddate");
//            }
//            else  // using Billing Start Date
//            {
//                select.AddWhere("where", "billingstart >= @startdate");
//                select.AddWhere("and", "billingstart <= @enddate");
//            }
//            //select.Add("order by location, department, customer, deal");
//            select.AddParameter("@startdate", request.parameters.StartDate);
//            select.AddParameter("@enddate", request.parameters.EndDate);

//            if (request.parameters.OfficeLocationId != "")
//            {
//                select.AddWhere("and", "locationid = @officelocation");
//                select.AddParameter("@officelocation", request.parameters.OfficeLocationId);
//            }
//            if (request.parameters.DepartmentId != "")
//            {
//                select.AddWhere("and", "departmentid = @department");
//                select.AddParameter("@department", request.parameters.DepartmentId);
//            }
//            if (request.parameters.CustomerId != "")
//            {
//                select.AddWhere("and", "customerid = @customer");
//                select.AddParameter("@customer", request.parameters.CustomerId);
//            }
//            if (request.parameters.DealId != "")
//            {
//                select.AddWhere("and", "dealid = @deal");
//                select.AddParameter("@deal", request.parameters.DealId);
//            }
//            select.AddWhereInFromCheckboxList("and", "status", statuslist, GetStatusList(), false);
          

//            dtDetails = qry.QueryToFwJsonTable(select, true);

//            dtDetails.InsertSubTotalRows("location", "rowtype", new string[] { "invoicetotal" });
//            dtDetails.InsertSubTotalRows("department", "rowtype", new string[] { "invoicetotal" });
//            dtDetails.InsertSubTotalRows("customer", "rowtype", new string[] { "invoicetotal" });
//            dtDetails.InsertSubTotalRows("deal", "rowtype", new string[] { "invoicetotal" });
//            dtDetails.InsertTotalRow("rowtype", "detail", "grandtotal", new string[] { "invoicetotal" });

//            for (int i = 0; i < dtDetails.Rows.Count; i++)
//            {
//                dtDetails.Rows[i][dtDetails.ColumnIndex["invoicedate"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["invoicedate"]]));
//                dtDetails.Rows[i][dtDetails.ColumnIndex["billingstart"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["billingstart"]]));
//                dtDetails.Rows[i][dtDetails.ColumnIndex["billingend"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["billingend"]]));
//                dtDetails.Rows[i][dtDetails.ColumnIndex["invoicetotal"]] = FwConvert.ToCurrencyStringNoDollarSign(Convert.ToDecimal(dtDetails.Rows[i][dtDetails.ColumnIndex["invoicetotal"]]));
//            }
            
//            return dtDetails;
//        }
//        //---------------------------------------------------------------------------------------------
//        protected override FwReport.PrintOptions getDefaultPrintOptions()
//        {
//            FwReport.PrintOptions printoptions;

//            printoptions = new PrintOptions(PrintOptions.PrintLayout.Landscape);
//            printoptions.HeaderHeight = 1.6f;

//            return printoptions;
//        }
//        //---------------------------------------------------------------------------------------------
//        public List<FwReportStatusItem> GetStatusList()
//        {
//            List<FwReportStatusItem> statuslist;
//            statuslist = new List<FwReportStatusItem>();
//            statuslist.Add(new FwReportStatusItem() { value = "NEW", text = "New", selected = "T" });
//            statuslist.Add(new FwReportStatusItem() { value = "RETURNED", text = "Returned", selected = "T" });
//            statuslist.Add(new FwReportStatusItem() { value = "REVISED", text = "Revised", selected = "T" });
//            statuslist.Add(new FwReportStatusItem() { value = "APPROVED", text = "Approved", selected = "T" });
//            statuslist.Add(new FwReportStatusItem() { value = "PROCESSED", text = "Processed", selected = "T" });
//            statuslist.Add(new FwReportStatusItem() { value = "CLOSED", text = "Closed", selected = "T" });
//            statuslist.Add(new FwReportStatusItem() { value = "VOID", text = "Void", selected = "T" });

//            return statuslist;
//        }
//        //---------------------------------------------------------------------------------------------
//        public string GetCommaListDecrypt(string encryptedlist)
//        {
//            string[] values;
//            string result = string.Empty;

//            if (!string.IsNullOrWhiteSpace(encryptedlist))
//            {
//                values = encryptedlist.Split(new char[] { ',' }, StringSplitOptions.None);
//                for (int i = 0; i < values.Length; i++)
//                {
//                    values[i] = FwCryptography.AjaxDecrypt(values[i]);
//                }
//                result = string.Join(",", values);
//            }

//            return result;
//        }
//        //---------------------------------------------------------------------------------------------
//    }
//}
