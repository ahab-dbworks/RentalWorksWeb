using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Home.InventoryCompatible
{
    [FwSqlTable("mastercompatibleview")]
    public class InventoryCompatibleLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mastercompatibleid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryCompatibleId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "compatiblewithid", modeltype: FwDataTypes.Text)]
        public string CompatibleWithInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "compatiblewithno", modeltype: FwDataTypes.Text)]
        public string CompatibleWithICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "compatiblewith", modeltype: FwDataTypes.Text)]
        public string CompatibleWithDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "compatiblewithclass", modeltype: FwDataTypes.Text)]
        public string CompatibleWithClassification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("InventoryId", "masterid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}