using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Home.Contract
{
    [FwSqlTable("contractview")]
    public class ContractBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public ContractBrowseLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "contractid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ContractId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractno", modeltype: FwDataTypes.Text)]
        public string ContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contracttype", modeltype: FwDataTypes.Text)]
        public string ContractType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractdate", modeltype: FwDataTypes.Date)]
        public string ContractDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ContractDateColor
        {
            get { return getContractDateColor(IsMigrated); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "contracttime", modeltype: FwDataTypes.Text)]
        public string ContractTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ContractTimeColor
        {
            get { return getContractTimeColor(HasVoid); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text)]
        public string LocationCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldate", modeltype: FwDataTypes.Text)]
        public string BillingDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasadjustedrentaldate", modeltype: FwDataTypes.Boolean)]
        public bool? BillingDateAdjusted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string BillingDateColor
        {
            get { return getBillingDateColor(BillingDateAdjusted); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderdesc", modeltype: FwDataTypes.Text)]
        public string PoOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasvoid", modeltype: FwDataTypes.Boolean)]
        public bool? HasVoid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "migrated", modeltype: FwDataTypes.Boolean)]
        public bool? IsMigrated { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            addFilterToSelect("OfficeLocationId", "locationid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("DealId", "dealid", select, request);
            addFilterToSelect("CustomerId", "customerid", select, request);
            addFilterToSelect("ContractType", "contracttype", select, request);

            SelectedCheckBoxListItems items = new SelectedCheckBoxListItems();
            items.Add(new SelectedCheckBoxListItem(RwConstants.CONTRACT_TYPE_RECEIVE));
            items.Add(new SelectedCheckBoxListItem(RwConstants.CONTRACT_TYPE_OUT));
            items.Add(new SelectedCheckBoxListItem(RwConstants.CONTRACT_TYPE_EXCHANGE));
            items.Add(new SelectedCheckBoxListItem(RwConstants.CONTRACT_TYPE_IN));
            items.Add(new SelectedCheckBoxListItem(RwConstants.CONTRACT_TYPE_RETURN));
            items.Add(new SelectedCheckBoxListItem(RwConstants.CONTRACT_TYPE_LOST));
            select.AddWhereIn("contracttype", items);

            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("OrderId"))
                {
                    select.AddWhere("exists (select * from ordercontract oc where oc.contractid = " + TableAlias + ".contractid and oc.orderid = @orderid)");
                    select.AddParameter("@orderid", uniqueIds["OrderId"].ToString());
                }
                if (uniqueIds.ContainsKey("PurchaseOrderId"))
                {
                    select.AddWhere("exists (select * from ordercontract oc where oc.contractid = " + TableAlias + ".contractid and oc.poid = @poid)");
                    select.AddParameter("@poid", uniqueIds["PurchaseOrderId"].ToString());
                }
            }

            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);
            AddActiveViewFieldToSelect("LocationId", "locationid", select, request);

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
                        row[dt.GetColumnNo("ContractDateColor")] = getContractDateColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsMigrated")].ToString()));
                        row[dt.GetColumnNo("ContractTimeColor")] = getContractTimeColor(FwConvert.ToBoolean(row[dt.GetColumnNo("HasVoid")].ToString()));
                        row[dt.GetColumnNo("BillingDateColor")] = getBillingDateColor(FwConvert.ToBoolean(row[dt.GetColumnNo("BillingDateAdjusted")].ToString()));
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------    
        protected string getContractDateColor(bool? isMigrated)
        {
            string color = null;
            if (isMigrated.GetValueOrDefault(false))
            {
                color = RwGlobals.CONTRACT_MIGRATED_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getContractTimeColor(bool? hasVoid)
        {
            string color = null;
            if (hasVoid.GetValueOrDefault(false))
            {
                color = RwGlobals.CONTRACT_ITEM_VOIDED_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        protected string getBillingDateColor(bool? billingDateAdjusted)
        {
            string color = null;
            if (billingDateAdjusted.GetValueOrDefault(false))
            {
                color = RwGlobals.CONTRACT_BILLING_DATE_ADJUSTED_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
    }
}
