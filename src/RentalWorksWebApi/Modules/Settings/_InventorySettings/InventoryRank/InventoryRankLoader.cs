using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.InventorySettings.InventoryRank
{
    [FwSqlTable("rankview")]
    public class InventoryRankLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rankid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryRankId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
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
        [FwSqlDataField(column: "type", modeltype: FwDataTypes.Text)]
        public string Type { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "typedisplay", modeltype: FwDataTypes.Text)]
        public string TypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "afromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? AFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "atovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? AToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bfromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? BFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "btovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? BToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cfromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? CFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ctovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? CToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dfromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? DFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dtovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? DToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "efromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? EFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "etovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? EToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ffromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? FFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ftovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? FToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gfromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? GFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gtovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? GToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rankupdated", modeltype: FwDataTypes.UTCDateTime)]
        public string RankUpdated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("WarehouseId", "warehouseid", select, request); 
            addFilterToSelect("InventoryTypeId", "inventorydepartmentid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}