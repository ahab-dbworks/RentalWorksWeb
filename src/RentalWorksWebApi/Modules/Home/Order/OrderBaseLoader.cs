using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Home.Order
{
    [FwSqlTable("orderview")]
    public abstract class OrderBaseLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "officelocation", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "maxcumulativediscount", modeltype: FwDataTypes.Decimal)]
        public decimal MaximumCumulativeDiscount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poapprovalstatusid", modeltype: FwDataTypes.Text)]
        public string PoApprovalStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("OfficeLocationId", "locationid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("DealId", "dealid", select, request);
            addFilterToSelect("CustomerId", "customerid", select, request);

            if ((request != null) && (request.activeview != null))
            {
                switch (request.activeview)
                {
                    case "PROSPECT":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "PROSPECT");
                        break;
                    case "RESERVED":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "RESERVED");
                        break;
                    case "CONFIRMED":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "CONFIRMED");
                        break;
                    case "HOLD":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "HOLD");
                        break;
                    case "ORDERED":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "ORDERED");
                        break;
                    case "ACTIVE":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "ACTIVE");
                        break;
                    case "COMPLETE":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "COMPLETE");
                        break;
                    case "CLOSED":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "CLOSED");
                        break;
                    case "CANCELLED":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", "CANCELLED");
                        break;
                    case "ALL":
                        break;
                }
            }
        }
        //------------------------------------------------------------------------------------    
    }
}
