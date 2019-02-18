using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Data;
using WebLibrary;

namespace WebApi.Modules.Home.Order
{
    [FwSqlTable("orderwebbrowseview")]
    public abstract class OrderBaseBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, isVisible: false)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "officelocation", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, isVisible: false)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, isVisible: false)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text, isVisible: false)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, isVisible: false)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PoNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poamount", modeltype: FwDataTypes.Decimal)]
        public decimal? PoAmount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text, isVisible: false)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text, isVisible: false)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "projectmanager", modeltype: FwDataTypes.Text)]
        public string ProjectManager { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertypedesc", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertotal", modeltype: FwDataTypes.Decimal)]
        public decimal? Total { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string CurrencyColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statuscolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string StatusColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ponocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string PoNumberColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehousecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string WarehouseColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptioncolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "refno", modeltype: FwDataTypes.Text)]
        public string ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pickdate", modeltype: FwDataTypes.Date)]
        public string PickDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text)]
        public string PickTime { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "estfromtime", modeltype: FwDataTypes.Text)]
        public string EstimatedStartTime { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "esttotime", modeltype: FwDataTypes.Text)]
        public string EstimatedStopTime { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesrepresentativecontactid", modeltype: FwDataTypes.Text)]
        public string OutsideSalesRepresentativeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salesrepresentative", modeltype: FwDataTypes.Text)]
        public string OutsideSalesRepresentative { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("OfficeLocationId", "locationid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("DepartmentId", "departmentid", select, request);
            addFilterToSelect("DealId", "dealid", select, request);
            addFilterToSelect("CustomerId", "customerid", select, request);


            if (GetMiscFieldAsBoolean("Staging", request).GetValueOrDefault(false))
            {
                //select.AddWhereIn("and", "status", RwConstants.ORDER_STATUS_CONFIRMED + "," + RwConstants.ORDER_STATUS_ACTIVE + "," + RwConstants.ORDER_STATUS_COMPLETE);
                select.AddWhereIn("and", "status", RwConstants.ORDER_STATUS_CONFIRMED + "," + RwConstants.ORDER_STATUS_ACTIVE);

                string stagingWarehouseId = GetMiscFieldAsString("StagingWarehouseId", request);
                if (!string.IsNullOrEmpty(stagingWarehouseId))
                {
                    select.AddWhere(" ((warehouseid = @stagingwhid) or exists (select * from masteritem mi with (nolock) where mi.orderid = " + TableAlias + ".orderid and mi.warehouseid = @stagingwhid))");
                    select.AddParameter("@stagingwhid", stagingWarehouseId);
                }
            }
            else if (GetMiscFieldAsBoolean("CheckIn", request).GetValueOrDefault(false))
            {
                //justin - wip
                select.AddWhereIn("and", "status", RwConstants.ORDER_STATUS_ACTIVE);

                string checkInWarehouseId = GetMiscFieldAsString("CheckInWarehouseId", request);
                if (!string.IsNullOrEmpty(checkInWarehouseId))
                {
                    select.AddWhere(" ((warehouseid = @checkinwhid) or exists (select * from masteritem mi with (nolock) where mi.orderid = " + TableAlias + ".orderid and mi.warehouseid = @checkinwhid))");
                    select.AddParameter("@checkinwhid", checkInWarehouseId);
                }
            }
            else if (GetMiscFieldAsBoolean("LossAndDamage", request).GetValueOrDefault(false))
            {
                select.AddWhereIn("and", "status", RwConstants.ORDER_STATUS_ACTIVE);

                string lossAndDamageWarehouseId = GetMiscFieldAsString("LossAndDamageWarehouseId", request);
                string lossAndDamageDealId = GetMiscFieldAsString("LossAndDamageDealId", request);
                if (!string.IsNullOrEmpty(lossAndDamageWarehouseId))
                {
                    select.AddWhere(" (warehouseid = @ldwhid)");
                    select.AddParameter("@ldwhid", lossAndDamageWarehouseId);
                }
                if (!string.IsNullOrEmpty(lossAndDamageDealId))
                {
                    select.AddWhere(" (dealid = @lddealid)");
                    select.AddParameter("@lddealid", lossAndDamageDealId);
                }
                select.AddWhere("exists (select * from masteritem mi with (nolock) join ordertran ot with (nolock) on (mi.orderid = ot.orderid and mi.masteritemid = ot.masteritemid) where mi.orderid = " + TableAlias + ".orderid and mi.rectype = '" + RwConstants.RECTYPE_RENTAL + "'" + (string.IsNullOrEmpty(lossAndDamageWarehouseId) ? "" : " and mi.warehouseid = @ldwhid") + ")");
            }

            AddActiveViewFieldToSelect("Status", "status", select, request);
            AddActiveViewFieldToSelect("LocationId", "locationid", select, request);
            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);

            //if ((request != null) && (request.activeview != null))
            //{
            //    switch (request.activeview)
            //    {
            //        case "PROSPECT":
            //            select.AddWhere("(status = @orderstatus)");
            //            select.AddParameter("@orderstatus", RwConstants.QUOTE_STATUS_PROSPECT);
            //            break;
            //        case "RESERVED":
            //            select.AddWhere("(status = @orderstatus)");
            //            select.AddParameter("@orderstatus", RwConstants.QUOTE_STATUS_RESERVED);
            //            break;
            //        case "CONFIRMED":
            //            select.AddWhere("(status = @orderstatus)");
            //            select.AddParameter("@orderstatus", RwConstants.ORDER_STATUS_CONFIRMED);
            //            break;
            //        case "HOLD":
            //            select.AddWhere("(status = @orderstatus)");
            //            select.AddParameter("@orderstatus", RwConstants.ORDER_STATUS_HOLD);
            //            break;
            //        case "ORDERED":
            //            select.AddWhere("(status = @orderstatus)");
            //            select.AddParameter("@orderstatus", RwConstants.QUOTE_STATUS_ORDERED);
            //            break;
            //        case "ACTIVE":
            //            select.AddWhere("(status = @orderstatus)");
            //            select.AddParameter("@orderstatus", RwConstants.ORDER_STATUS_ACTIVE);
            //            break;
            //        case "COMPLETE":
            //            select.AddWhere("(status = @orderstatus)");
            //            select.AddParameter("@orderstatus", RwConstants.ORDER_STATUS_COMPLETE);
            //            break;
            //        case "CLOSED":
            //            select.AddWhere("(status = @orderstatus)");
            //            select.AddParameter("@orderstatus", RwConstants.ORDER_STATUS_CLOSED);
            //            break;
            //        case "CANCELLED":
            //            select.AddWhere("(status = @orderstatus)");
            //            select.AddParameter("@orderstatus", RwConstants.ORDER_STATUS_CANCELLED);
            //            break;
            //        case "ALL":
            //            break;
            //    }

            //    if (request.activeview.Contains("WarehouseId="))
            //    {
            //        string whId = request.activeview.Replace("WarehouseId=", "");
            //        if (!whId.Equals("ALL"))
            //        {
            //            select.AddWhere("(warehouseid = @whid)");
            //            select.AddParameter("@whid", whId);
            //        }
            //    }

            //    string locId = "ALL";
            //    if (request.activeview.Contains("OfficeLocationId="))
            //    {
            //        locId = request.activeview.Replace("OfficeLocationId=", "");
            //    }
            //    else if (request.activeview.Contains("LocationId="))
            //    {
            //        locId = request.activeview.Replace("LocationId=", "");
            //    }
            //    if (!locId.Equals("ALL"))
            //    {
            //        select.AddWhere("(locationid = @locid)");
            //        select.AddParameter("@locid", locId);
            //    }

            //}



        }
        //------------------------------------------------------------------------------------    
    }
}
