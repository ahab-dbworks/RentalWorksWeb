using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using WebApi.Logic;

namespace WebApi.Modules.Home.InventorySubstitute
{
    [FwSqlTable("dbo.funcsubstitute(@masterid, @warehouseid)")]
    public class InventorySubstituteLoader : AppDataLoadRecord
    {
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
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            string inventoryId = InventoryId;
            string warehouseId = GetUniqueIdAsString("WarehouseId", request) ?? ""; 

            if (string.IsNullOrEmpty(inventoryId))
            {
                inventoryId = GetUniqueIdAsString("InventoryId", request) ?? "";
            }

            if (string.IsNullOrEmpty(inventoryId))
            {
                if (!string.IsNullOrEmpty(InventorySubstituteId))
                {
                    string[] values = AppFunc.GetStringDataAsync(AppConfig, "substitute", new string[] { "mastersubstituteid" }, new string[] { InventorySubstituteId }, new string[] { "masterid" }).Result;
                    inventoryId = values[0];
                }
            }

            select.AddParameter("@masterid", inventoryId);
            select.AddParameter("@warehouseid", warehouseId);
        }
        //------------------------------------------------------------------------------------    
    }
}