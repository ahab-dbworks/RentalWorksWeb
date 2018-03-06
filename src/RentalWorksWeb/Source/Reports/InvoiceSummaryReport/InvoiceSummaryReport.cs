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
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            select = new FwSqlSelect();

            if (request.parameters.DateType == "InvoiceDate")
            {
                select.Add("select top 1 * from dbo.funcinvoicesummaryrpt");
                select.Add("(@startdate, @enddate, null, null, 'F', 'F', 'F')");
                select.Add("order by location, department, customer, deal");
                select.AddParameter("@startdate", request.parameters.StartDate);
                select.AddParameter("@enddate", request.parameters.EndDate);
            }
            else if (request.parameters.DateType == "BillingStartDate")
            {
                select.Add("select top 1 * from dbo.funcinvoicesummaryrpt");
                select.Add("(null, null, @startdate, @enddate, 'F', 'F', 'F')");
                select.Add("order by location, department, customer, deal");
                select.AddParameter("@startdate", request.parameters.StartDate);
                select.AddParameter("@enddate", request.parameters.EndDate);
            }

            select.Parse();
            dtDetails = qry.QueryToFwJsonTable(select, true);

            dtDetails.Rows[0][dtDetails.ColumnIndex["billingstart"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[0][dtDetails.ColumnIndex["billingstart"]]));
            dtDetails.Rows[0][dtDetails.ColumnIndex["billingend"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[0][dtDetails.ColumnIndex["billingend"]]));


            StringBuilder sb;
            string html;
           
            sb = new StringBuilder(base.renderHeaderHtml(styletemplate, headertemplate, printOptions));
            sb.Replace("[STARTDATE]", request.parameters.StartDate);
            sb.Replace("[TODATE]", request.parameters.EndDate);
            html = sb.ToString();
            html = this.applyTableToTemplate(html, "header", dtDetails);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected override string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        {
            string html;
            FwJsonDataTable dtInvoiceSummaryReport;
            StringBuilder sb;

            dtInvoiceSummaryReport = GetInvoiceSummaryReport();

            html = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
            sb = new StringBuilder(base.renderBodyHtml(styletemplate, bodytemplate, printOptions));
            sb.Replace("[TotalRows]", "Total Rows: " + dtInvoiceSummaryReport.Rows.Count);
            html = sb.ToString();
            html = this.applyTableToTemplate(html, "details", dtInvoiceSummaryReport);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetInvoiceSummaryReport()
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            select = new FwSqlSelect();

            if (request.parameters.DateType == "InvoiceDate")
            {
                select.Add("select * from dbo.funcinvoicesummaryrpt");
                select.Add("(@startdate, @enddate, null, null, 'F', 'F', 'F')");
                select.Add("order by location, department, customer, deal");
                select.AddParameter("@startdate", request.parameters.StartDate);
                select.AddParameter("@enddate", request.parameters.EndDate);
            }
            else if (request.parameters.DateType == "BillingStartDate")
            {
                select.Add("select * from dbo.funcinvoicesummaryrpt");
                select.Add("(null, null, @startdate, @enddate, 'F', 'F', 'F')");
                select.Add("order by location, department, customer, deal");
                select.AddParameter("@startdate", request.parameters.StartDate);
                select.AddParameter("@enddate", request.parameters.EndDate);
            }

            select.Parse();

            dtDetails = qry.QueryToFwJsonTable(select, true);
            for (int i = 0; i < dtDetails.Rows.Count; i++)
            {
                dtDetails.Rows[i][dtDetails.ColumnIndex["invoicedate"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["invoicedate"]]));
                dtDetails.Rows[i][dtDetails.ColumnIndex["billingstart"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["billingstart"]]));
                dtDetails.Rows[i][dtDetails.ColumnIndex["billingend"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["billingend"]]));
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
