using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Home.InventorySubstitute
{
    [FwSqlTable("substituteview")]
    public class InventorySubstituteLoader : RwDataLoadRecord
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
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("InventoryId", "masterid", select, request);
        }
        //------------------------------------------------------------------------------------    
    }
}