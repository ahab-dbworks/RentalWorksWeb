using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Home.ItemCompatible
{
    [FwSqlTable("mastercompatibleview")]
    public class ItemCompatibleLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mastercompatibleid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ItemCompatibleId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "compatiblewithid", modeltype: FwDataTypes.Text)]
        public string CompatibleWithItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "compatiblewithno", modeltype: FwDataTypes.Text)]
        public string CompatibleWithIcode { get; set; }
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
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("ItemId", "masterid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}