using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Web.Source.Reports
{
    class SalesInventoryTransactionReport : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Sales Inventory Transactions"; }
    //---------------------------------------------------------------------------------------------
    protected override string renderHeaderHtml(string styletemplate, string headertemplate, FwReport.PrintOptions printOptions)
    {
        StringBuilder sb;
        string html;

        sb = new StringBuilder(base.renderHeaderHtml(styletemplate, headertemplate, printOptions));
        sb.Replace("[FROMDATE]", request.parameters.FromDate);
        sb.Replace("[TODATE]", request.parameters.ToDate);

        html = sb.ToString();

        return html;
    }
    //---------------------------------------------------------------------------------------------
    protected override string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
    {
        string html;
        dynamic TransTypeList;
        FwJsonDataTable dtSalesInventoryTransactionReport;
        StringBuilder sb;

        TransTypeList = request.parameters.TransTypeList;

        dtSalesInventoryTransactionReport = GetSalesInventoryTransactionReport(TransTypeList);

        html = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
        sb = new StringBuilder(base.renderBodyHtml(styletemplate, bodytemplate, printOptions));
        sb.Replace("[TotalRows]", "Total Rows: " + dtSalesInventoryTransactionReport.Rows.Count);
        html = sb.ToString();
        html = this.applyTableToTemplate(html, "details", dtSalesInventoryTransactionReport);
        return html;
    }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetSalesInventoryTransactionReport(dynamic TransTypeList)
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            select = new FwSqlSelect();

            select.Add("select rowtype='detail', rpt.*");
            select.Add(" from  rptinventorytransaction rpt");
            select.Parse();
            select.AddWhere("rpt.rectype = @rectype");
            select.AddWhereInFromCheckboxList(" and ", "rpt.transtype", TransTypeList, GetTransTypeList(), false);
            select.AddParameter("@rectype", "S");


            if (request.parameters.FromDate != "")
            {
                select.AddWhere("and", "rpt.transdate >= @startdate");
                select.AddParameter("@startdate", request.parameters.FromDate);
            };
            if (request.parameters.ToDate != "")
            {
                select.AddWhere("and", "rpt.transdate <= @enddate");
                select.AddParameter("@enddate", request.parameters.ToDate);
            };
            if (request.parameters.WarehouseId != "")
            {
                select.AddWhere("and", "rpt.warehouseid = @warehouseid");
                select.AddParameter("@warehouseid", request.parameters.WarehouseId);
            };
            if (request.parameters.InventoryTypeId != "")
            {
                select.AddWhere("and", "rpt.inventorydepartmentid = @inventorytypeid");
                select.AddParameter("@inventorytypeid", request.parameters.InventoryTypeId);
            };
            if (request.parameters.CategoryId != "")
            {
                select.AddWhere("and", "rpt.categoryid = @categoryid");
                select.AddParameter("@categoryid", request.parameters.CategoryId);
            };
            if (request.parameters.SubCategoryId != "")
            {
                select.AddWhere("and", "rpt.subcategoryid = @subcategoryid");
                select.AddParameter("@subcategoryid", request.parameters.SubCategoryId);
            };
            if (request.parameters.InventoryId != "")
            {
                select.AddWhere("and", "rpt.masterid = @inventoryid");
                select.AddParameter("@inventoryid", request.parameters.InventoryId);
            };

            select.AddOrderBy("rpt.whcode, rpt.masterno, rpt.transdate, rpt.orderby");

            dtDetails = qry.QueryToFwJsonTable(select, true);

            for (int i = 0; i < dtDetails.Rows.Count; i++)
            {
                dtDetails.Rows[i][dtDetails.ColumnIndex["transdate"]] = FwConvert.ToUSShortDate((String)(dtDetails.Rows[i][dtDetails.ColumnIndex["transdate"]]));
                dtDetails.Rows[i][dtDetails.ColumnIndex["cost"]] = FwConvert.ToCurrencyStringNoDollarSign(Convert.ToDecimal(dtDetails.Rows[i][dtDetails.ColumnIndex["cost"]]));
                dtDetails.Rows[i][dtDetails.ColumnIndex["costextended"]] = FwConvert.ToCurrencyStringNoDollarSign(Convert.ToDecimal(dtDetails.Rows[i][dtDetails.ColumnIndex["costextended"]]));
                dtDetails.Rows[i][dtDetails.ColumnIndex["price"]] = FwConvert.ToCurrencyStringNoDollarSign(Convert.ToDecimal(dtDetails.Rows[i][dtDetails.ColumnIndex["price"]]));
                dtDetails.Rows[i][dtDetails.ColumnIndex["priceextended"]] = FwConvert.ToCurrencyStringNoDollarSign(Convert.ToDecimal(dtDetails.Rows[i][dtDetails.ColumnIndex["priceextended"]]));

            }

            //dtDetails.InsertSubTotalRows("masterno", "rowtype", new string[] { "qtyordered" });  //justin 05/31/2018 removed to prevent dictionary error

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
    public List<FwReportStatusItem> GetTransTypeList()
    {
        List<FwReportStatusItem> TransTypeList;
            TransTypeList = new List<FwReportStatusItem>();
            TransTypeList.Add(new FwReportStatusItem() { value = "PURCHASE", text = "Purchase", selected = "T" });
            TransTypeList.Add(new FwReportStatusItem() { value = "VENDOR RETURN", text = "Vendor Return", selected = "T" });
            TransTypeList.Add(new FwReportStatusItem() { value = "SALES", text = "Sales", selected = "T" });
            TransTypeList.Add(new FwReportStatusItem() { value = "CUSTOMER RETURN", text = "Customer Return", selected = "T" });
            TransTypeList.Add(new FwReportStatusItem() { value = "ADJUSTMENT", text = "Adjustment", selected = "T" });
            TransTypeList.Add(new FwReportStatusItem() { value = "TRANSFER", text = "Transfer", selected = "T" });

        return TransTypeList;
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
