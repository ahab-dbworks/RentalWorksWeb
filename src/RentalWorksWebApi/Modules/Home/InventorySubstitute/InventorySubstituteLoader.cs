using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApi.Modules.Home.InventorySubstitute
{
    [FwSqlTable("substituteview")]
    public class InventorySubstituteLoader : AppDataLoadRecord
    {
        private string inventoryId;
        private string warehouseId;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mastersubstituteid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventorySubstituteId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, required: true)]
        public string InventoryId { get; set; } 
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "substituteid", modeltype: FwDataTypes.Text, required: true)]
        public string SubstituteInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgid", modeltype: FwDataTypes.Text)]
        public string ManufacturerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manufacturer", modeltype: FwDataTypes.Text)]
        public string Manufacturer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [JsonIgnore]
        public override string TableName
        {
            get
            {
                return "dbo.funcsubstitute('" + inventoryId + "', '" + warehouseId + "')";
            }
        }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {


            useWithNoLock = false;
            if ((request != null) && (request.uniqueids != null))
            {
                inventoryId = "";
                warehouseId = "";
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("InventoryId"))
                {
                    inventoryId = uniqueIds["InventoryId"].ToString();
                }
                if (uniqueIds.ContainsKey("WarehouseId"))
                {
                    warehouseId = uniqueIds["WarehouseId"].ToString();
                }
            }
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("InventoryId", "masterid", select, request);
        }
        //------------------------------------------------------------------------------------    
    }
}