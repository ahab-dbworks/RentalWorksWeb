using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Collections.Generic;
using WebApi.Data;
using WebApi;
using System.Text;
using WebApi.Logic;

namespace WebApi.Modules.Agent.Order
{
    [FwSqlTable("orderwebbrowseview")]
    public abstract class OrderBaseBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        public OrderBaseBrowseLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string Type { get; set; }
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
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean)]
        public bool? Miscellaneous { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean)]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "space", modeltype: FwDataTypes.Boolean)]
        public bool? Facilities { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean)]
        public bool? Transportation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsale", modeltype: FwDataTypes.Boolean)]
        public bool? RentalSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "finalld", modeltype: FwDataTypes.Boolean)]
        public bool? LossAndDamage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repair", modeltype: FwDataTypes.Boolean)]
        public bool? Repair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ismultiwarehouse", modeltype: FwDataTypes.Boolean)]
        public bool? IsMultiWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nocharge", modeltype: FwDataTypes.Boolean)]
        public bool? NoCharge { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealstatustype", modeltype: FwDataTypes.Text)]
        public string DealStatusType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerstatustype", modeltype: FwDataTypes.Text)]
        public string CustomerStatusType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodtype", modeltype: FwDataTypes.Text)]
        public string BillingCycleType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "unassignedsubs", modeltype: FwDataTypes.Boolean)]
        public bool? UnassignedSubs { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "projectid", modeltype: FwDataTypes.Text)]
        public string ProjectId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "projectno", modeltype: FwDataTypes.Text)]
        public string ProjectNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "projectdesc", modeltype: FwDataTypes.Text)]
        public string Project { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string NumberColor
        {
            get { return getNumberColor(Type, Status, EstimatedStopDate); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor
        {
            get { return getDescriptionColor(Repair, LossAndDamage); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string WarehouseColor
        {
            get { return getWarehouseColor(IsMultiWarehouse); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string PoNumberColor
        {
            get { return getPoNumberColor(NoCharge); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string StatusColor
        {
            get { return getStatusColor(Type, Status, DealStatusType, CustomerStatusType); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string CurrencyColor
        {
            get { return getCurrencyColor(CurrencyId, OfficeLocationDefaultCurrencyId); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string UnassignedSubsColor
        {
            get { return getUnassignedSubsColor(UnassignedSubs); }
            set { }
        }
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
            addFilterToSelect("BillingCycleType", "billperiodtype", select, request);
            addFilterToSelect("ProjectId", "projectid", select, request);

            if (GetMiscFieldAsBoolean("Staging", request).GetValueOrDefault(false))
            {
                //select.AddWhereIn("and", "status", RwConstants.ORDER_STATUS_CONFIRMED + "," + RwConstants.ORDER_STATUS_ACTIVE + "," + RwConstants.ORDER_STATUS_COMPLETE);
                select.AddWhereIn("and", "status", RwConstants.ORDER_STATUS_CONFIRMED + "," + RwConstants.ORDER_STATUS_ACTIVE);

                string stagingWarehouseId = GetMiscFieldAsString("StagingWarehouseId", request);
                if (!string.IsNullOrEmpty(stagingWarehouseId))
                {
                    select.AddWhere(" ((warehouseid = @stagingwhid) or exists (select * from masteritem mi with (nolock) where mi.orderid = " + TableAlias + ".orderid and mi.warehouseid = @stagingwhid))");
                    select.AddWhere(" exists (select * from masteritem mi with (nolock) where mi.orderid = " + TableAlias + ".orderid and mi.rectype in ('" + RwConstants.RECTYPE_RENTAL + "','" + RwConstants.RECTYPE_SALE + "','" + RwConstants.RECTYPE_USED_SALE + "'))");
                    select.AddParameter("@stagingwhid", stagingWarehouseId);
                }
            }
            else if (GetMiscFieldAsBoolean("Exchange", request).GetValueOrDefault(false))
            {
                //justin - wip
                select.AddWhereIn("and", "status", RwConstants.ORDER_STATUS_ACTIVE);

                string exchangeWarehouseId = GetMiscFieldAsString("ExchangeWarehouseId", request);
                if (!string.IsNullOrEmpty(exchangeWarehouseId))
                {
                    select.AddWhere(" ((warehouseid = @exchangewhid) or exists (select * from masteritem mi with (nolock) where mi.orderid = " + TableAlias + ".orderid and mi.warehouseid = @exchangewhid))");
                    select.AddWhere(" exists (select * from masteritem mi with (nolock) where mi.orderid = " + TableAlias + ".orderid and mi.rectype in ('" + RwConstants.RECTYPE_RENTAL + "','" + RwConstants.RECTYPE_SALE + "','" + RwConstants.RECTYPE_USED_SALE + "'))");
                    select.AddParameter("@exchangewhid", exchangeWarehouseId);
                }
                select.AddWhere("exists (select * from masteritem mi with (nolock) join ordertran ot with (nolock) on (mi.orderid = ot.orderid and mi.masteritemid = ot.masteritemid) where mi.orderid = " + TableAlias + ".orderid and mi.rectype = '" + RwConstants.RECTYPE_RENTAL + "'" + (string.IsNullOrEmpty(exchangeWarehouseId) ? "" : " and mi.warehouseid = @exchangewhid") + ")");
            }
            else if (GetMiscFieldAsBoolean("CheckIn", request).GetValueOrDefault(false))
            {
                //justin - wip
                select.AddWhereIn("and", "status", RwConstants.ORDER_STATUS_ACTIVE);

                string checkInWarehouseId = GetMiscFieldAsString("CheckInWarehouseId", request);
                if (!string.IsNullOrEmpty(checkInWarehouseId))
                {
                    select.AddWhere(" ((warehouseid = @checkinwhid) or exists (select * from masteritem mi with (nolock) where mi.orderid = " + TableAlias + ".orderid and mi.warehouseid = @checkinwhid) or exists (select * from masteritem mi with (nolock) where mi.orderid = " + TableAlias + ".orderid and mi.returntowarehouseid = @checkinwhid))");
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
                select.AddWhere("exists (select * from masteritem mi with (nolock) join ordertran ot with (nolock) on (mi.orderid = ot.orderid and mi.masteritemid = ot.masteritemid) where mi.orderid = " + TableAlias + ".orderid and mi.rectype = '" + RwConstants.RECTYPE_RENTAL + "'" + (string.IsNullOrEmpty(lossAndDamageWarehouseId) ? "" : " and mi.warehouseid = @ldwhid") + " and ot.itemstatus = 'O'" + ")");
            }
            else if (GetMiscFieldAsBoolean("Migrate", request).GetValueOrDefault(false))
            {
                select.AddWhereIn("and", "status", RwConstants.ORDER_STATUS_ACTIVE);

                //string migrateWarehouseId = GetMiscFieldAsString("MigrateWarehouseId", request);
                //if (!string.IsNullOrEmpty(migrateWarehouseId))
                //{
                //    select.AddWhere(" (warehouseid = @whid)");
                //    select.AddParameter("@whid", migrateWarehouseId);
                //}


                string migrateSourceDealId = GetMiscFieldAsString("DealId", request);
                string migrateSourceDepartmentId = GetMiscFieldAsString("DepartmentId", request);
                if (!string.IsNullOrEmpty(migrateSourceDealId))
                {
                    select.AddWhere(" (dealid = @migratesourcedealid)");
                    select.AddParameter("@migratesourcedealid", migrateSourceDealId);
                }
                if (!string.IsNullOrEmpty(migrateSourceDepartmentId))
                {
                    select.AddWhere(" (departmentid = @migratesourcedepartmentid)");
                    select.AddParameter("@migratesourcedepartmentid", migrateSourceDepartmentId);
                }
                //select.AddWhere("exists (select * from masteritem mi with (nolock) join ordertran ot with (nolock) on (mi.orderid = ot.orderid and mi.masteritemid = ot.masteritemid) where mi.orderid = " + TableAlias + ".orderid and mi.rectype = '" + RwConstants.RECTYPE_RENTAL + "'" + (string.IsNullOrEmpty(lossAndDamageWarehouseId) ? "" : " and mi.warehouseid = @ldwhid") + ")");
                select.AddWhere("exists (select * from masteritem mi with (nolock) join ordertran ot with (nolock) on (mi.orderid = ot.orderid and mi.masteritemid = ot.masteritemid) where mi.orderid = " + TableAlias + ".orderid and mi.rectype = '" + RwConstants.RECTYPE_RENTAL + "'" + " and ot.itemstatus = 'O'" + ")");  // has Rentals Out
            }

            string contactId = GetUniqueIdAsString("ContactId", request) ?? "";
            if (!string.IsNullOrEmpty(contactId))
            {
                select.AddWhere("exists (select * from ordercontact oc where oc.orderid = " + TableAlias + ".orderid and oc.contactid = @contactid)");
                select.AddParameter("@contactid", contactId);
            }

            string inventoryId = GetUniqueIdAsString("InventoryId", request) ?? "";
            if (!string.IsNullOrEmpty(inventoryId))
            {
                //select.AddWhere("exists (select * from masteritem mi where mi.orderid = " + TableAlias + ".orderid and mi.masterid = @masterid)");

                //justin hoffman 07/08/2020 #2690
                string classification = AppFunc.GetStringDataAsync(AppConfig, "master", "masterid", inventoryId, "class").Result;
                if (classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT) || classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_CONTAINER))
                {
                    select.AddWhere("exists (select * from masteritem mi where mi.orderid = " + TableAlias + ".orderid and mi.parentid = @masterid)");
                }
                else
                {
                    select.AddWhere("exists (select * from masteritem mi where mi.orderid = " + TableAlias + ".orderid and mi.masterid = @masterid)");
                }

                select.AddParameter("@masterid", inventoryId);
            }

            string itemId = GetUniqueIdAsString("ItemId", request) ?? "";
            if (!string.IsNullOrEmpty(itemId))
            {
                select.AddWhere("exists (select * from masteritem mi join ordertran ot on (mi.orderid = ot.orderid and mi.masteritemid = ot.masteritemid) where mi.orderid = " + TableAlias + ".orderid and ot.rentalitemid = @itemid)");
                select.AddParameter("@itemid", itemId);
            }


            AddActiveViewFieldToSelect("Status", "status", select, request);
            if (UserSession.UserType == "USER")
            {
                AddActiveViewFieldToSelect("LocationId", "locationid", select, request);
                AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);
            }
            else if (UserSession.UserType == "CONTACT")
            {
                // filter out any orders that a Contact does not belong to
                select.AddWhere("DealId in (select companyid from compcontactview with (nolock) where contactid = @contactid and companytype='DEAL' and inactive <> 'T')");
                select.AddParameter("@contactid", this.UserSession.ContactId);


                AddActiveViewFieldToSelect("DealId", "dealid", select, request);
            }

            //ALL, AGENT, PROJECTMANAGER
            if ((request != null) && (request.activeviewfields != null))
            {
                if (request.activeviewfields.ContainsKey("My"))
                {
                    StringBuilder myWhere = new StringBuilder();
                    List<string> myValues = request.activeviewfields["My"];
                    foreach (string myValue in myValues)
                    {
                        if (myValue.ToUpper().Equals(RwConstants.MY_AGENT_ACTIVE_VIEW_VALUE))
                        {
                            if (myWhere.Length > 0)
                            {
                                myWhere.AppendLine("or");
                            }
                            myWhere.AppendLine("agentid = @myusersid");
                        }
                        if (myValue.ToUpper().Equals(RwConstants.MY_PROJECT_MANAGER_ACTIVE_VIEW_VALUE))
                        {
                            if (myWhere.Length > 0)
                            {
                                myWhere.AppendLine("or");
                            }
                            myWhere.AppendLine("projectmanagerid = @myusersid");
                        }
                    }
                    if (myWhere.Length > 0)
                    {
                        myWhere.Insert(0, "(");
                        myWhere.AppendLine(")");
                        select.AddWhere(myWhere.ToString());
                        select.AddParameter("@myusersid", this.UserSession.UsersId);
                    }
                }
            }

        }
        //------------------------------------------------------------------------------------    
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (e.DataTable != null)
            {
                FwJsonDataTable dt = e.DataTable;
                if (dt.Rows.Count > 0)
                {
                    foreach (List<object> row in dt.Rows)
                    {
                        row[dt.GetColumnNo("NumberColor")] = getNumberColor(row[dt.GetColumnNo("Type")].ToString(), row[dt.GetColumnNo("Status")].ToString(), row[dt.GetColumnNo("EstimatedStopDate")].ToString());
                        row[dt.GetColumnNo("DescriptionColor")] = getDescriptionColor(FwConvert.ToBoolean(row[dt.GetColumnNo("Repair")].ToString()), FwConvert.ToBoolean(row[dt.GetColumnNo("LossAndDamage")].ToString()));
                        row[dt.GetColumnNo("WarehouseColor")] = getWarehouseColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsMultiWarehouse")].ToString()));
                        row[dt.GetColumnNo("PoNumberColor")] = getPoNumberColor(FwConvert.ToBoolean(row[dt.GetColumnNo("NoCharge")].ToString()));
                        row[dt.GetColumnNo("StatusColor")] = getStatusColor(row[dt.GetColumnNo("Type")].ToString(), row[dt.GetColumnNo("Status")].ToString(), row[dt.GetColumnNo("DealStatusType")].ToString(), row[dt.GetColumnNo("CustomerStatusType")].ToString());
                        row[dt.GetColumnNo("CurrencyColor")] = getCurrencyColor(row[dt.GetColumnNo("CurrencyId")].ToString(), row[dt.GetColumnNo("OfficeLocationDefaultCurrencyId")].ToString());
                        row[dt.GetColumnNo("UnassignedSubsColor")] = getUnassignedSubsColor(FwConvert.ToBoolean(row[dt.GetColumnNo("UnassignedSubs")].ToString()));
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------    
        protected string getNumberColor(string orderType, string status, string estimatedStopDate)
        {
            string color = null;
            if ((orderType.Equals(RwConstants.ORDER_TYPE_QUOTE)) && (status.Equals(RwConstants.QUOTE_STATUS_ORDERED)))
            {
                color = RwGlobals.QUOTE_ORDER_LOCKED_COLOR;
            }
            else if ((orderType.Equals(RwConstants.ORDER_TYPE_QUOTE)) && (status.Equals(RwConstants.QUOTE_STATUS_RESERVED)))
            {
                color = RwGlobals.QUOTE_RESERVED_COLOR;
            }
            else if ((orderType.Equals(RwConstants.ORDER_TYPE_ORDER)) && (status.Equals(RwConstants.ORDER_STATUS_ACTIVE)) && (FwConvert.ToDateTime(estimatedStopDate) < DateTime.Today))
            {
                color = RwGlobals.ORDER_LATE_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getDescriptionColor(bool? isRepair, bool? isLossAndDamage)
        {
            string color = null;
            if (isRepair.GetValueOrDefault(false))
            {
                color = RwGlobals.ORDER_REPAIR_COLOR;
            }
            else if (isLossAndDamage.GetValueOrDefault(false))
            {
                color = RwGlobals.ORDER_LOSS_AND_DAMAGE_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getWarehouseColor(bool? isMultiWarehouse)
        {
            string color = null;
            if (isMultiWarehouse.GetValueOrDefault(false))
            {
                color = RwGlobals.QUOTE_ORDER_MULTI_WAREHOUSE_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getPoNumberColor(bool? noCharge)
        {
            string color = null;
            if (noCharge.GetValueOrDefault(false))
            {
                color = RwGlobals.QUOTE_ORDER_NO_CHARGE_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getStatusColor(string orderType, string status, string dealStatusType, string customerStatusType)
        {
            string color = null;
            if (orderType.Equals(RwConstants.ORDER_TYPE_QUOTE) && (status.Equals(RwConstants.QUOTE_STATUS_REQUEST)))
            {
                color = RwGlobals.QUOTE_REQUEST_COLOR;
            }
            else if (status.Equals(RwConstants.QUOTE_STATUS_HOLD) || status.Equals(RwConstants.ORDER_STATUS_HOLD) || dealStatusType.Equals(RwConstants.DEAL_STATUS_TYPE_HOLD) || customerStatusType.Equals(RwConstants.CUSTOMER_STATUS_TYPE_HOLD))
            {
                color = RwGlobals.QUOTE_ORDER_ON_HOLD_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getCurrencyColor(string currencyId, string officeLocationCurrencyId)
        {
            string color = null;
            if ((!string.IsNullOrEmpty(currencyId)) && (!currencyId.Equals(officeLocationCurrencyId)))
            {
                color = RwGlobals.FOREIGN_CURRENCY_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getUnassignedSubsColor(bool? unassignedsubs)
        {
            string color = null;
            if (unassignedsubs.GetValueOrDefault(false))
            {
                color = RwGlobals.SUB_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
    }
}
