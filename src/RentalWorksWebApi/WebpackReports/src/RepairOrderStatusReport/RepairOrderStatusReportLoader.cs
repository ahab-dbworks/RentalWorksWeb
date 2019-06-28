using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Text;

namespace WebApi.Modules.Reports.RepairOrderStatusReport
{
    [FwSqlTable("repairorderstatusrptwebview")]
    public class RepairOrderStatusReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text)]
        public string RepairId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairno", modeltype: FwDataTypes.Text)]
        public string RepairNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairdate", modeltype: FwDataTypes.Date)]
        public string RepairDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string Rfid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer)]
        public int? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "priority", modeltype: FwDataTypes.Text)]
        public string Priority { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtypending", modeltype: FwDataTypes.Integer)]
        public int? QuantityPending { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyhighpriority", modeltype: FwDataTypes.Integer)]
        public int? QuantityHighPriority { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtymediumpriority", modeltype: FwDataTypes.Integer)]
        public int? QuantityMediumPriority { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtylowpriority", modeltype: FwDataTypes.Integer)]
        public int? QuantityLowPriority { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtybillable", modeltype: FwDataTypes.Integer)]
        public int? QuantityBillable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtybilled", modeltype: FwDataTypes.Integer)]
        public int? QuantityBilled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtynotyetbilled", modeltype: FwDataTypes.Integer)]
        public int? QuantityNotYetBilled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damage", modeltype: FwDataTypes.Text)]
        public string Damage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairitemstatusid", modeltype: FwDataTypes.Text)]
        public string RepairItemStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairitemstatus", modeltype: FwDataTypes.Text)]
        public string RepairItemStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysinrepair", modeltype: FwDataTypes.Integer)]
        public int? DaysInRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairtype", modeltype: FwDataTypes.Text)]
        public string RepairType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billable", modeltype: FwDataTypes.Boolean)]
        public bool? Billable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billabletext", modeltype: FwDataTypes.Text)]
        public string BillableText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billed", modeltype: FwDataTypes.Boolean)]
        public bool? Billed { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedtext", modeltype: FwDataTypes.Text)]
        public string BilledText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagedealid", modeltype: FwDataTypes.Text)]
        public string DamageDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagedeal", modeltype: FwDataTypes.Text)]
        public string DamageDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeorderid", modeltype: FwDataTypes.Text)]
        public string ChargeOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorqty", modeltype: FwDataTypes.Integer)]
        public int? VendorQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorrepairitemstatusid", modeltype: FwDataTypes.Text)]
        public string VendorRepairItemStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorrepairitemstatus", modeltype: FwDataTypes.Text)]
        public string VendorRepairItemStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorcompletiondate", modeltype: FwDataTypes.Date)]
        public string VendorCompletionDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(RepairOrderStatusReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    if (request.IsSummary.GetValueOrDefault(false))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("select     'detail'                                      as [RowType],                 ");
                        sb.Append("           [" + TableAlias + "].[warehouseid]            as [WarehouseId],             ");
                        sb.Append("           [" + TableAlias + "].[warehouse]              as [Warehouse],               ");
                        sb.Append("           [" + TableAlias + "].[whcode]                 as [WarehouseCode],           ");
                        sb.Append("           [" + TableAlias + "].[departmentid]           as [DepartmentId],            ");
                        sb.Append("           [" + TableAlias + "].[department]             as [Department],              ");
                        sb.Append("           [" + TableAlias + "].[inventorydepartmentid]  as [InventoryTypeId],         ");
                        sb.Append("           [" + TableAlias + "].[inventorydepartment]    as [InventoryType],           ");
                        sb.Append("           [" + TableAlias + "].[categoryid]             as [CategoryId],              ");
                        sb.Append("           [" + TableAlias + "].[category]               as [Category],                ");
                        sb.Append("           [" + TableAlias + "].[subcategoryid]          as [SubCategoryId],           ");
                        sb.Append("           [" + TableAlias + "].[subcategory]            as [SubCategory],             ");
                        sb.Append("           [" + TableAlias + "].[masterid]               as [InventoryId],             ");
                        sb.Append("           [" + TableAlias + "].[masterno]               as [ICode],                   ");
                        sb.Append("           [" + TableAlias + "].[description]            as [Description],             ");
                        sb.Append("       sum([" + TableAlias + "].[qty]              )     as [Quantity],                ");
                        sb.Append("       sum([" + TableAlias + "].[qtypending]       )     as [QuantityPending],         ");
                        sb.Append("       sum([" + TableAlias + "].[qtyhighpriority]  )     as [QuantityHighPriority],    ");
                        sb.Append("       sum([" + TableAlias + "].[qtymediumpriority])     as [QuantityMediumPriority],  ");
                        sb.Append("       sum([" + TableAlias + "].[qtylowpriority]   )     as [QuantityLowPriority],     ");
                        sb.Append("       sum([" + TableAlias + "].[qtybillable]      )     as [QuantityBillable],        ");
                        sb.Append("       sum([" + TableAlias + "].[qtybilled]        )     as [QuantityBilled],          ");
                        sb.Append("       sum([" + TableAlias + "].[qtynotyetbilled]  )     as [QuantityNotYetBilled]     ");
                        sb.Append("from " + TableName + " [" + TableAlias + "] with (nolock)                              ");
                        select.Add(sb.ToString());
                        AddPropertiesAsQueryColumns(qry);
                    }
                    else
                    {
                        SetBaseSelectQuery(select, qry);
                    }

                    select.Parse();
                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("departmentid", request.DepartmentId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("subcategoryid", request.SubCategoryId);
                    select.AddWhereIn("repairitemstatusid", request.RepairItemStatusId);
                    select.AddWhereIn("vendorrepairitemstatusid", request.VendorRepairItemStatusId);
                    select.AddWhereIn("vendorid", request.VendorId);
                    select.AddWhereIn("dealid", request.DealId);
                    select.AddWhereIn("status", request.RepairOrderStatus);
                    select.AddWhereIn("priority", request.Priority);
                    if (request.Billable != null)
                    {
                        select.AddWhere("billable " + (request.Billable.GetValueOrDefault(false) ? "=" : "<>") + " 'T'");
                    }
                    if (request.Billed != null)
                    {
                        select.AddWhere("billed " + (request.Billed.GetValueOrDefault(false) ? "=" : "<>") + " 'T'");
                    }
                    if (!request.IncludeOutsideRepairsOnly.GetValueOrDefault(false))
                    {
                        select.AddWhere("vendorid is not null");
                    }
                    if ((request.DaysInRepair != null) && (!request.DaysInRepairFilterMode.Equals("ALL")))
                    {
                        if (request.DaysInRepairFilterMode.Equals("LTE"))
                        {
                            select.AddWhere("daysinrepair <= " + request.DaysInRepair.ToString());
                        }
                        else if (request.DaysInRepairFilterMode.Equals("GT"))
                        {
                            select.AddWhere("daysinrepair > " + request.DaysInRepair.ToString());
                        }
                    }

                    //select.AddOrderBy("warehouse, department, inventorydepartment, category, masterno, description");


                    if (request.IsSummary.GetValueOrDefault(false))
                    {
                        select.AddWhere("", "group by warehouseid, warehouse, whcode, departmentid, department, inventorydepartmentid, inventorydepartment, categoryid, category, subcategoryid, subcategory, masterid, masterno, description");  //#jhtodo: need to be able to do select.AddGroupBy
                        select.AddOrderBy("warehouse, department, inventorydepartment, category, masterno, description");
                    }
                    else
                    {
                        select.AddOrderBy("warehouse, department, inventorydepartment, category, masterno, description, daysinrepair desc");
                    }

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                string[] totalFields = new string[] { "Quantity" };
                dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                dt.InsertSubTotalRows("Department", "RowType", totalFields);
                dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
                dt.InsertSubTotalRows("Category", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}
