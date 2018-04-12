using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Source.Reports
{
    class InvoiceSummaryReport : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Invoice Summary"; }
        //---------------------------------------------------------------------------------------------
        protected override string renderHeaderHtml(string styletemplate, string headertemplate, FwReport.PrintOptions printOptions)
        {
            //justin 04/02/2018 removing this section for speed.  query not required for header

            //FwSqlSelect select;
            //FwSqlCommand qry;
            //FwJsonDataTable dtDetails;

            //qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            //select = new FwSqlSelect();

            //if (request.parameters.DateType == "InvoiceDate")
            //{
            //    select.Add("select top 1 * from dbo.funcinvoicesummaryrpt");
            //    select.Add("(@startdate, @enddate, null, null, 'F', 'F', 'F')");
            //    select.Add("order by location, department, customer, deal");
            //    select.AddParameter("@startdate", request.parameters.StartDate);
            //    select.AddParameter("@enddate", request.parameters.EndDate);
            //}
            //else if (request.parameters.DateType == "BillingStartDate")
            //{
            //    select.Add("select top 1 * from dbo.funcinvoicesummaryrpt");
            //    select.Add("(null, null, @startdate, @enddate, 'F', 'F', 'F')");
            //    select.Add("order by location, department, customer, deal");
            //    select.AddParameter("@startdate", request.parameters.StartDate);
            //    select.AddParameter("@enddate", request.parameters.EndDate);
            //}

            //select.Parse();
            //dtDetails = qry.QueryToFwJsonTable(select, true);

            StringBuilder sb;
            string html;
           
            sb = new StringBuilder(base.renderHeaderHtml(styletemplate, headertemplate, printOptions));
            sb.Replace("[FROMDATE]", request.parameters.StartDate);
            sb.Replace("[TODATE]", request.parameters.EndDate);
            html = sb.ToString();
            //html = this.applyTableToTemplate(html, "header", dtDetails);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected override string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        {
            string html;
            dynamic statuslist;
            FwJsonDataTable dtInvoiceSummaryReport;
            StringBuilder sb;

            statuslist = request.parameters.statuslist;
            dtInvoiceSummaryReport = GetInvoiceSummaryReport(statuslist);

            html = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
            sb = new StringBuilder(base.renderBodyHtml(styletemplate, bodytemplate, printOptions));
            sb.Replace("[TotalRows]", "Total Rows: " + dtInvoiceSummaryReport.Rows.Count);
            html = sb.ToString();
            html = this.applyTableToTemplate(html, "details", dtInvoiceSummaryReport);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetInvoiceSummaryReport(dynamic statuslist)
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            select = new FwSqlSelect();

            //if (request.parameters.DateType == "InvoiceDate")
            //{
            //    select.Add("select * from dbo.funcinvoicesummaryrpt");
            //    select.Add("(@startdate, @enddate, null, null, 'F', 'F', 'F')");
            //    select.Add("order by location, department, customer, deal");
            //    select.AddParameter("@startdate", request.parameters.StartDate);
            //    select.AddParameter("@enddate", request.parameters.EndDate);
            //}
            //else if (request.parameters.DateType == "BillingStartDate")
            //{
            //    select.Add("select * from dbo.funcinvoicesummaryrpt");
            //    select.Add("(null, null, @startdate, @enddate, 'F', 'F', 'F')");
            //    select.Add("order by location, department, customer, deal");
            //    select.AddParameter("@startdate", request.parameters.StartDate);
            //    select.AddParameter("@enddate", request.parameters.EndDate);
            //}

            select.Add("select * ");
            select.Add(" from  invoicesummaryrptview");
            select.Add("order by location, department, customer, deal");

            select.Parse();

            if (request.parameters.DateType == "InvoiceDate")
            {
                select.AddWhere("where", "invoicedate >= @startdate");
                select.AddWhere("and", "invoicedate <= @enddate");
            }
            else  // using Billing Start Date
            {
                select.AddWhere("where", "billingstart >= @startdate");
                select.AddWhere("and", "billingstart <= @enddate");
            }
            select.Add("order by location, department, customer, deal");
            select.AddParameter("@startdate", request.parameters.StartDate);
            select.AddParameter("@enddate", request.parameters.EndDate);

            if (request.parameters.OfficeLocationId != "")
            {
                select.AddWhere("and", "locationid = @officelocation");
                select.AddParameter("@officelocation", request.parameters.OfficeLocationId);
            }
            if (request.parameters.DepartmentId != "")
            {
                select.AddWhere("and", "departmentid = @department");
                select.AddParameter("@department", request.parameters.DepartmentId);
            }
            if (request.parameters.CustomerId != "")
            {
                select.AddWhere("and", "customerid = @customer");
                select.AddParameter("@customer", request.parameters.CustomerId);
            }
            if (request.parameters.DealId != "")
            {
                select.AddWhere("and", "dealid = @deal");
                select.AddParameter("@deal", request.parameters.DealId);
            }
            select.AddWhereInFromCheckboxList("and", "status", statuslist, GetStatusList(), false);
          

            dtDetails = qry.QueryToFwJsonTable(select, true);
            for (int i = 0; i < dtDetails.Rows.Count; i++)
            {
                dtDetails.Rows[i][dtDetails.ColumnIndex["invoicedate"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["invoicedate"]]));
                dtDetails.Rows[i][dtDetails.ColumnIndex["billingstart"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["billingstart"]]));
                dtDetails.Rows[i][dtDetails.ColumnIndex["billingend"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["billingend"]]));
                dtDetails.Rows[i][dtDetails.ColumnIndex["invoicetotal"]] = FwConvert.ToCurrencyStringNoDollarSign(Convert.ToDecimal(dtDetails.Rows[i][dtDetails.ColumnIndex["invoicetotal"]]));
            }
            
            return dtDetails;
        }
        //---------------------------------------------------------------------------------------------
        protected override FwReport.PrintOptions getDefaultPrintOptions()
        {
            FwReport.PrintOptions printoptions;

            printoptions = new PrintOptions(PrintOptions.PrintLayout.Landscape);
            printoptions.HeaderHeight = 1.6f;

            return printoptions;
        }
        //---------------------------------------------------------------------------------------------
        public List<FwReportStatusItem> GetStatusList()
        {
            List<FwReportStatusItem> statuslist;
            statuslist = new List<FwReportStatusItem>();
            statuslist.Add(new FwReportStatusItem() { value = "NEW", text = "New", selected = "T" });
            statuslist.Add(new FwReportStatusItem() { value = "RETURNED", text = "Returned", selected = "T" });
            statuslist.Add(new FwReportStatusItem() { value = "REVISED", text = "Revised", selected = "T" });
            statuslist.Add(new FwReportStatusItem() { value = "APPROVED", text = "Approved", selected = "T" });
            statuslist.Add(new FwReportStatusItem() { value = "PROCESSED", text = "Processed", selected = "T" });
            statuslist.Add(new FwReportStatusItem() { value = "CLOSED", text = "Closed", selected = "T" });
            statuslist.Add(new FwReportStatusItem() { value = "VOID", text = "Void", selected = "T" });

            return statuslist;
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
    }
}
