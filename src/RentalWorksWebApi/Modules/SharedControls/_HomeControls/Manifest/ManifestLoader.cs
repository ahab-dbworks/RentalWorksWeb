using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Data;
using WebLibrary;

namespace WebApi.Modules.Home.Manifest
{
    [FwSqlTable("manifestwebview")]
    public class ManifestLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ManifestId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractno", modeltype: FwDataTypes.Text)]
        public string ManifestNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractdate", modeltype: FwDataTypes.Date)]
        public string ManifestDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contracttype", modeltype: FwDataTypes.Text)]
        public string ContractType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "namefml", modeltype: FwDataTypes.Text)]
        public string NameFirstMiddleLast { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string TransferId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string TransferNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasvoid", modeltype: FwDataTypes.Boolean)]
        public bool? HasVoid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contracttime", modeltype: FwDataTypes.Text)]
        public string ManifestTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("LocationId", "locationid", select, request);
            addFilterToSelect("ContractType", "contracttype", select, request);
            addFilterToSelect("TransferId", "orderid", select, request);
            AddActiveViewFieldToSelect("LocationId", "locationid", select, request);

            bool hasContractTypeFilter = false;
            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                hasContractTypeFilter = uniqueIds.ContainsKey("ContractType");
            }

            if ((!hasContractTypeFilter) && (string.IsNullOrEmpty(ManifestId)))
            {
                SelectedCheckBoxListItems items = new SelectedCheckBoxListItems();
                items.Add(new SelectedCheckBoxListItem(RwConstants.CONTRACT_TYPE_MANIFEST));
                items.Add(new SelectedCheckBoxListItem(RwConstants.CONTRACT_TYPE_RECEIPT));
                select.AddWhereIn("contracttype", items);
            }



        }
        //------------------------------------------------------------------------------------ 
    }
}
