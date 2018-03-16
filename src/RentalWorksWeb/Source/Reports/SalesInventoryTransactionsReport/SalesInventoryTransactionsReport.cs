using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Source.Reports
{
    class SalesInventoryTransactionsReport : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Sales Inventory Transactions"; }
    //---------------------------------------------------------------------------------------------
    protected override string renderHeaderHtml(string styletemplate, string headertemplate, FwReport.PrintOptions printOptions)
    {
      FwSqlSelect select;
      FwSqlCommand qry;
      FwJsonDataTable dtDetails;

      qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
      select = new FwSqlSelect();

      select.Add("select top 1 rv.Warehouse, rpt.*");
      select.Add("from  dbo.funcsalestransactionrpt('S') rpt");                                  
      select.Add("join rptmasterwhview rv with (nolock) on (rpt.masterid = rv.masterid and");
      select.Add("rpt.warehouseid = rv.warehouseid)"); 
      //select.Add("order by masterno");
      select.AddParameter("@startdate", request.parameters.StartDate);
      select.AddParameter("@enddate", request.parameters.EndDate);
      //select.AddParameter("@warehouse", request.parameters.Warehouse);



      select.Parse();
      dtDetails = qry.QueryToFwJsonTable(select, true);

      StringBuilder sb;
      string html;

      sb = new StringBuilder(base.renderHeaderHtml(styletemplate, headertemplate, printOptions));
      sb.Replace("[FROMDATE]", request.parameters.StartDate);
      sb.Replace("[TODATE]", request.parameters.EndDate);
      //sb.Replace("[WAREHOUSE]", request.parameters.WareHouse);
      html = sb.ToString();
      html = this.applyTableToTemplate(html, "header", dtDetails);

      return html;
    }
    //---------------------------------------------------------------------------------------------
    protected override string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        {
            string html;
            FwJsonDataTable dtSalesInventoryTransactionsReport;
            StringBuilder sb;

            dtSalesInventoryTransactionsReport = GetSalesInventoryTransactionsReport();

            html = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
            sb = new StringBuilder(base.renderBodyHtml(styletemplate, bodytemplate, printOptions));
            sb.Replace("[TotalRows]", "Total Rows: " + dtSalesInventoryTransactionsReport.Rows.Count);
            html = sb.ToString();
            html = this.applyTableToTemplate(html, "details", dtSalesInventoryTransactionsReport);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetSalesInventoryTransactionsReport()
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            select = new FwSqlSelect();

                select.Add("select rv.Warehouse, rpt.*");
                select.Add("from  dbo.funcsalestransactionrpt('S') rpt");                                  
                select.Add("join rptmasterwhview rv with (nolock) on (rpt.masterid = rv.masterid and");
                select.Add("rpt.warehouseid = rv.warehouseid)");   
                select.Add(" where rpt.transdate >= @startdate");
                select.Add(" and   rpt.transdate <= @enddate");
                //select.Add("and   rpt.transtype in ('PURCHASE','VENDOR RETURN','SALES','CUSTOMER RETURN','ADJUSTMENT','TRANSFER')
                //select.Add("and   rpt.warehouseid in ('0000000H')
                select.Add("order by rv.[Warehouse], rpt.masterno, rpt.transdate, rpt.orderby");
                select.AddParameter("@startdate", request.parameters.StartDate);
                select.AddParameter("@enddate", request.parameters.EndDate);


      select.Parse();

            dtDetails = qry.QueryToFwJsonTable(select, true);
            //for (int i = 0; i < dtDetails.Rows.Count; i++)
            //{
            //    dtDetails.Rows[i][dtDetails.ColumnIndex["invoicedate"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["invoicedate"]]));
            //    dtDetails.Rows[i][dtDetails.ColumnIndex["billingstart"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["billingstart"]]));
            //    dtDetails.Rows[i][dtDetails.ColumnIndex["billingend"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["billingend"]]));
            //    dtDetails.Rows[i][dtDetails.ColumnIndex["invoicetotal"]] = FwConvert.ToCurrencyStringNoDollarSign(Convert.ToDecimal(dtDetails.Rows[i][dtDetails.ColumnIndex["invoicetotal"]]));
            //}
            
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
