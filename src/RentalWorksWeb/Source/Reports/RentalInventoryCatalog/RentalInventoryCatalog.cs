using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Source.Reports
{
    class RentalInventoryCatalog : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Rental Inventory Catalog"; }
        //---------------------------------------------------------------------------------------------
        protected override string renderHeaderHtml(string styletemplate, string headertemplate, FwReport.PrintOptions printOptions)
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            select = new FwSqlSelect();
            
            select.Add("select top 1 * from inventorycatalogrptview m with (nolock)");
            select.Add("where m.availfor = 'R'");
            select.Add(" and   m.trackedby in ('BARCODE','QUANTITY','SERIALNO')");
            select.Add(" and   m.class in ('I','A','C','K','N')");
            select.Add("order by m.warehouse, departmentorderby, categoryorderby, subcategoryorderby, m.masterorderby, m.masterno");            

            select.Parse();
            dtDetails = qry.QueryToFwJsonTable(select, true);

            StringBuilder sb;
            string html;
           
            sb = new StringBuilder(base.renderHeaderHtml(styletemplate, headertemplate, printOptions));

            sb.Replace("[CURRENTDATE]", DateTime.Today.ToString("MMMM d, yyyy"));

            html = sb.ToString();
            html = this.applyTableToTemplate(html, "header", dtDetails);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected override string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        {
            string html;
            FwJsonDataTable dtRentalInventoryCatalog;
            StringBuilder sb;

            dtRentalInventoryCatalog = GetRentalInventoryCatalog();

            html = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
            sb = new StringBuilder(base.renderBodyHtml(styletemplate, bodytemplate, printOptions));
            sb.Replace("[TotalRows]", "Total Rows: " + dtRentalInventoryCatalog.Rows.Count);
            html = sb.ToString();
            html = this.applyTableToTemplate(html, "details", dtRentalInventoryCatalog);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetRentalInventoryCatalog()
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);

            qry.AddColumn("dailyrate", false, FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            qry.AddColumn("weeklyrate", false, FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            qry.AddColumn("monthlyrate", false, FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            qry.AddColumn("replacementcost", false, FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);

            select = new FwSqlSelect();

            select.Add("select * from inventorycatalogrptview m with (nolock)");
            select.Add("where m.availfor = 'R'");
            select.Add(" and   m.trackedby in ('BARCODE','QUANTITY','SERIALNO')");
            select.Add(" and   m.class in ('I','A','C','K','N')");
            select.Add("order by m.warehouse, departmentorderby, categoryorderby, subcategoryorderby, m.masterorderby, m.masterno");

            select.Parse();

            dtDetails = qry.QueryToFwJsonTable(select, true);
            
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
        //---------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------
    }
}
