using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.InventoryType
{
    [FwSqlTable("inventorydepartment")]
    public class InventoryTypeLoader: RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool Rental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool Sales { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "parts", modeltype: FwDataTypes.Boolean)]
        public bool Parts { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sets", modeltype: FwDataTypes.Boolean)]
        public bool Sets { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "props", modeltype: FwDataTypes.Boolean)]
        public bool Props { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "wardrobe", modeltype: FwDataTypes.Boolean)]
        public bool Wardrobe { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean)]
        public bool Vehicle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availabilitygrace", modeltype: FwDataTypes.Integer)]
        public int LowAvailabilityPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "barcodeprintqty", modeltype: FwDataTypes.Integer)]
        public int BarCodePrintQty { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "usedesigner", modeltype: FwDataTypes.Boolean)]
        public bool BarCodePrintUseDesigner { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "profitlossgroup", modeltype: FwDataTypes.Boolean)]
        public bool GroupProfitLoss { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(rental='T' or sales = 'T' or parts='T' or sets='T' or props='T' or wardrobe='T')");
        }
        //------------------------------------------------------------------------------------
    }
}
