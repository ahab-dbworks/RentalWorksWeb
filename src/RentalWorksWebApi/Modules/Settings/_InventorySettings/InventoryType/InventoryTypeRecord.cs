using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.InventorySettings.InventoryType
{
    [FwSqlTable("inventorydepartment")]
    public class InventoryTypeRecord : AppDataReadWriteRecord
    {

        /* TODO:
        orderby
        inventoryappreportdesignerid
        rentalitemappreportdesignerid
        orderbypicklist
        */

        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string InventoryTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "parts", modeltype: FwDataTypes.Boolean)]
        public bool? Parts { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "space", modeltype: FwDataTypes.Boolean)]
        public bool? Facilities { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sets", modeltype: FwDataTypes.Boolean)]
        public bool? Sets { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "props", modeltype: FwDataTypes.Boolean)]
        public bool? Props { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "wardrobe", modeltype: FwDataTypes.Boolean)]
        public bool? Wardrobe { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehicle", modeltype: FwDataTypes.Boolean)]
        public bool? Transportation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean)]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean)]
        public bool? Misc { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availabilitygrace", modeltype: FwDataTypes.Integer)]
        public int? LowAvailabilityPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "barcodeprintqty", modeltype: FwDataTypes.Integer)]
        public int? BarCodePrintQty { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "usedesigner", modeltype: FwDataTypes.Boolean)]
        public bool? BarCodePrintUseDesigner { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "stagescheduling", modeltype: FwDataTypes.Boolean)]
        public bool? StageScheduling { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "defaultdw", modeltype: FwDataTypes.Decimal, precision: 5, scale: 3)]
        public decimal? FacilitiesDefaultDw { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "donotdefaultrate", modeltype: FwDataTypes.Boolean)]
        public bool? FacilitiesDoNotDefaultRateOnBooking { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "profitlossgroup", modeltype: FwDataTypes.Boolean)]
        public bool? GroupProfitLoss { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}