﻿using Fw.Json.Services;
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
            StringBuilder sb;
            string html;
           
            sb = new StringBuilder(base.renderHeaderHtml(styletemplate, headertemplate, printOptions));

            sb.Replace("[CURRENTDATE]", DateTime.Today.ToString("MMMM d, yyyy"));

            html = sb.ToString();

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected override string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        {
            string html;
            dynamic classificationlist, trackedbylist, ranklist;
            FwJsonDataTable dtRentalInventoryCatalog;
            StringBuilder sb;

            classificationlist = request.parameters.classificationlist;
            trackedbylist = request.parameters.trackedbylist;
            ranklist = request.parameters.ranklist;

            dtRentalInventoryCatalog = GetRentalInventoryCatalog(classificationlist, trackedbylist, ranklist);

            html = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
            sb = new StringBuilder(base.renderBodyHtml(styletemplate, bodytemplate, printOptions));
            sb.Replace("[TotalRows]", "Total Rows: " + dtRentalInventoryCatalog.Rows.Count);
            html = sb.ToString();
            html = this.applyTableToTemplate(html, "details", dtRentalInventoryCatalog);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetRentalInventoryCatalog(dynamic classificationlist, dynamic trackedbylist, dynamic ranklist)
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

            select.Add("select rowtype='detail', warehouse, inventorydepartment, category, masterno, master, classdesc, trackedby, qtyowned, dailyrate, weeklyrate, monthlyrate, replacementcost ");
            select.Add("from inventorycatalogrptview m with (nolock)");

            select.Parse();

            select.AddWhere("and", "m.availfor = 'R'");
            if (request.parameters.WarehouseId != "")
            {
                select.AddWhere("and", "m.warehouseid = @warehouseid");
                select.AddParameter("@warehouseid", request.parameters.WarehouseId);
            }
            if (request.parameters.InventoryTypeId != "")
            {
                select.AddWhere("and", "m.inventorydepartmentid = @inventorytypeid");
                select.AddParameter("@inventorytypeid", request.parameters.InventoryTypeId);
            }
            if (request.parameters.CategoryId != "")
            {
                select.AddWhere("and", "m.categoryid = @categoryid");
                select.AddParameter("@categoryid", request.parameters.CategoryId);
            }
            if (request.parameters.SubCategoryId != "")
            {
                select.AddWhere("and", "m.subcategoryid = @subcategoryid");
                select.AddParameter("@subcategoryid", request.parameters.SubCategoryId);
            }
            if (request.parameters.RentalInventoryId != "")
            {
                select.AddWhere("and", "m.masterid = @masterid");

                select.AddParameter("@masterid", request.parameters.RentalInventoryId);
            }

            select.AddWhereInFromCheckboxList("and", "m.class", classificationlist, GetClassificationList(), false);
            select.AddWhereInFromCheckboxList("and", "m.trackedby", trackedbylist, GetTrackedByList(), false);
            select.AddWhereInFromCheckboxList("and", "m.rank", ranklist, GetRankList(), false);
            select.AddOrderBy("m.warehouse, departmentorderby, categoryorderby, subcategoryorderby, m.masterorderby, m.masterno");

            dtDetails = qry.QueryToFwJsonTable(select, true);

            dtDetails.InsertSubTotalRows("warehouse", "rowtype", new string[] { "qtyowned" });
            dtDetails.InsertSubTotalRows("inventorydepartment", "rowtype", new string[] { "qtyowned" });
            dtDetails.InsertSubTotalRows("category", "rowtype", new string[] { "qtyowned" });
            dtDetails.InsertTotalRow("rowtype", "detail", "grandtotal", new string[] { "qtyowned" });

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
        public List<FwReportStatusItem> GetTrackedByList()
        {
            List<FwReportStatusItem> trackedByList;
            trackedByList = new List<FwReportStatusItem>();
            trackedByList.Add(new FwReportStatusItem() { value = "BARCODE", text = "Barcode", selected = "T" });
            trackedByList.Add(new FwReportStatusItem() { value = "QUANTITY", text = "Quantity", selected = "T" });
            trackedByList.Add(new FwReportStatusItem() { value = "SERIALNO", text = "Serial Number", selected = "T" });

            return trackedByList;
        }
        //---------------------------------------------------------------------------------------------
        public List<FwReportStatusItem> GetClassificationList()
        {
            List<FwReportStatusItem> classificationList;
            classificationList = new List<FwReportStatusItem>();
            classificationList.Add(new FwReportStatusItem() { value = "I", text = "Item", selected = "T" });
            classificationList.Add(new FwReportStatusItem() { value = "A", text = "Accessory", selected = "T" });
            classificationList.Add(new FwReportStatusItem() { value = "C", text = "Complete", selected = "T" });
            classificationList.Add(new FwReportStatusItem() { value = "K", text = "Kit", selected = "T" });
            classificationList.Add(new FwReportStatusItem() { value = "N", text = "Container", selected = "T" });
            classificationList.Add(new FwReportStatusItem() { value = "M", text = "Miscellaneous", selected = "F" });

            return classificationList;
        }
        //---------------------------------------------------------------------------------------------
        public List<FwReportStatusItem> GetRankList()
        {
            List<FwReportStatusItem> rankList;
            rankList = new List<FwReportStatusItem>();
            rankList.Add(new FwReportStatusItem() { value = "A", text = "A", selected = "T" });
            rankList.Add(new FwReportStatusItem() { value = "B", text = "B", selected = "T" });
            rankList.Add(new FwReportStatusItem() { value = "C", text = "C", selected = "T" });
            rankList.Add(new FwReportStatusItem() { value = "D", text = "D", selected = "T" });
            rankList.Add(new FwReportStatusItem() { value = "E", text = "E", selected = "T" });
            rankList.Add(new FwReportStatusItem() { value = "F", text = "F", selected = "T" });
            rankList.Add(new FwReportStatusItem() { value = "G", text = "G", selected = "T" });

            return rankList;
        }
    }
}
