using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using System.Text;
using WebApi.Data;
using WebApi;

namespace WebApi.Modules.Transfers.TransferOrder
{
    [FwSqlTable("transferwebbrowseview")]
    public class TransferOrderBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string TransferId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string TransferNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string TransferDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromwarehouseid", modeltype: FwDataTypes.Text)]
        public string FromWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromwarehouse", modeltype: FwDataTypes.Text)]
        public string FromWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string ToWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "towarehouse", modeltype: FwDataTypes.Text)]
        public string ToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "origorderid", modeltype: FwDataTypes.Text)]
        public string RelatedToOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "origorderno", modeltype: FwDataTypes.Text)]
        public string RelatedToOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "isreturntransferorder", modeltype: FwDataTypes.Boolean)]
        public bool? IsReturnTransferOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickdate", modeltype: FwDataTypes.Date)]
        public string PickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text)]
        public string PickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipdate", modeltype: FwDataTypes.Date)]
        public string ShipDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shiptime", modeltype: FwDataTypes.Text)]
        public string ShipTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requireddate", modeltype: FwDataTypes.Date)]
        public string RequiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredtime", modeltype: FwDataTypes.Text)]
        public string RequiredTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            addFilterToSelect("OfficeLocationId", "locationid", select, request);
            addFilterToSelect("DepartmentId", "departmentid", select, request);


            AddActiveViewFieldToSelect("LocationId", "locationid", select, request);


            //if ((request != null) && (request.uniqueids != null))
            //{
            //    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
            //    if (uniqueIds.ContainsKey("WarehouseId"))
            //    {
            //        select.AddWhere("(warehouseid = @warehouseid1 or fromwarehouseid = @warehouseid1)");
            //        select.AddParameter("@warehouseid1", uniqueIds["WarehouseId"].ToString());
            //    }
            //}

            string warehouseId = GetUniqueIdAsString("WarehouseId", request) ?? "";
            if (!string.IsNullOrEmpty(warehouseId))
            {
                select.AddWhere("(warehouseid = @warehouseid1 or fromwarehouseid = @warehouseid1)");
                select.AddParameter("@warehouseid1", warehouseId);
            }

            string inventoryId = GetUniqueIdAsString("InventoryId", request) ?? "";
            if (!string.IsNullOrEmpty(inventoryId))
            {
                select.AddWhere("exists (select * from masteritem mi where mi.orderid = " + TableAlias + ".orderid and mi.masterid = @masterid)");
                select.AddParameter("@masterid", inventoryId);
            }

            if ((request != null) && (request.activeviewfields != null))
            {
                if (request.activeviewfields.ContainsKey("WarehouseId"))
                {
                    List<string> values = request.activeviewfields["WarehouseId"];
                    if (values.Count == 1)
                    {
                        string value = values[0];
                        if (!value.ToUpper().Equals("ALL"))
                        {
                            select.AddWhere("(warehouseid = @warehouseid2 or fromwarehouseid = @warehouseid2)");
                            select.AddParameter("@warehouseid2", value);
                        }
                    }
                    else if (values.Count > 1)
                    {
                        int v = 2; // prameter index
                        StringBuilder sb = new StringBuilder();
                        foreach (string value in values)
                        {
                            if (!value.ToUpper().Equals("ALL"))
                            {
                                string paramName = "@warehouseid" + v.ToString();
                                sb.AppendLine("(warehouseid = " + paramName + " or fromwarehouseid = " + paramName + ")");
                                sb.AppendLine("or");
                                select.AddParameter(paramName, value);
                            }
                            v++;
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Insert(0, "(");
                        sb.Append(")");
                        select.AddWhere(sb.ToString()); ;
                    }
                }
            }



            if (GetMiscFieldAsBoolean("TransferOut", request).GetValueOrDefault(false))
            {
                select.AddWhereIn("and", "status", RwConstants.TRANSFER_STATUS_CONFIRMED + "," + RwConstants.TRANSFER_STATUS_ACTIVE);

                string transferOutWarehouseId = GetMiscFieldAsString("TransferOutWarehouseId", request);
                if (!string.IsNullOrEmpty(transferOutWarehouseId))
                {
                    select.AddWhere(" ((warehouseid = @transferoutwhid) or exists (select * from masteritem mi with (nolock) where mi.orderid = " + TableAlias + ".orderid and mi.warehouseid = @transferoutwhid))");
                    select.AddParameter("@transferoutwhid", transferOutWarehouseId);
                }
            }


        }
        //------------------------------------------------------------------------------------ 
    }
}
