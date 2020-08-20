using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Modules.HomeControls.Inventory;
using WebApi;

namespace WebApi.Modules.Inventory.RentalInventory
{
    [FwSqlTable("inventoryview")]
    public class RentalInventoryLoader : InventoryLoader
    {

        //set/wall
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "openingid", modeltype: FwDataTypes.Text)]
        public string SetOpeningId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "opening", modeltype: FwDataTypes.Text)]
        public string SetOpening { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "walltypeid", modeltype: FwDataTypes.Text)]
        public string WallTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "walltype", modeltype: FwDataTypes.Text)]
        public string WallType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "surfaceid", modeltype: FwDataTypes.Text)]
        public string SetSurfaceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "surface", modeltype: FwDataTypes.Text)]
        public string SetSurface { get; set; }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionwidthft", modeltype: FwDataTypes.Integer)]
        public int? WallWidthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionwidthin", modeltype: FwDataTypes.Integer)]
        public int? WallWidthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionheightft", modeltype: FwDataTypes.Integer)]
        public int? WallHeightFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionheightin", modeltype: FwDataTypes.Integer)]
        public int? WallHeightIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionlengthft", modeltype: FwDataTypes.Integer)]
        public int? WallLengthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionlengthin", modeltype: FwDataTypes.Integer)]
        public int? WallLengthIn { get; set; }

        // for cusomizing browse 
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        //public decimal? DailyRate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        //public decimal? WeeklyRate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        //public decimal? Week2Rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        //public decimal? Week3Rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        //public decimal? Week4Rate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        //public decimal? MonthlyRate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        //public decimal? UnitValue { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        //public decimal? ReplacementCost { get; set; }
        ////------------------------------------------------------------------------------------ 


        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            //select.Parse();
            select.AddWhere("(availfor='" + RwConstants.INVENTORY_AVAILABLE_FOR_RENT + "')");
            addFilterToSelect("ScannableInventoryId", "scannablemasterid", select, request);
        }
        //------------------------------------------------------------------------------------
    }
}