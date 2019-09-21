using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.InventoryType
{
    [FwSqlTable("dbo.funcinventorytype(@rectype)")]
    public class InventoryTypeLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
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
        [FwSqlDataField(column: "availabilitygrace", modeltype: FwDataTypes.Integer)]
        public int? LowAvailabilityPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "barcodeprintqty", modeltype: FwDataTypes.Integer)]
        public int? BarCodePrintQty { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "usedesigner", modeltype: FwDataTypes.Boolean)]
        public bool? BarCodePrintUseDesigner { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "profitlossgroup", modeltype: FwDataTypes.Boolean)]
        public bool? GroupProfitLoss { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "categorycount", modeltype: FwDataTypes.Integer)]
        public int? CategoryCount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(rental='T' or sales = 'T' or parts='T' or sets='T' or props='T' or wardrobe='T' or vehicle ='T')");
            addFilterToSelect("Rental", "rental", select, request);
            addFilterToSelect("Sales", "sales", select, request);
            addFilterToSelect("Parts", "parts", select, request);
            addFilterToSelect("Sets", "sets", select, request);
            addFilterToSelect("Props", "props", select, request);
            addFilterToSelect("Wardrobe", "wardrobe", select, request);
            addFilterToSelect("Vehicle", "vehicle", select, request);

            string recType = GetUniqueIdAsString("RecType", request) ?? "";
            select.AddParameter("@rectype", recType);

            if (GetUniqueIdAsBoolean("HasCategories", request).GetValueOrDefault(false))
            {
                select.AddWhere("(categorycount > 0)");
            }

        }
        //------------------------------------------------------------------------------------
    }
}
