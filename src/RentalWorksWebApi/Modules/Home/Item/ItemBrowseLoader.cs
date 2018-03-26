using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Home.Item
{
    [FwSqlTable("rentalitemview")]
    public class ItemBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ItemId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        //public string InventoryId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        //public string WarehouseId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "itemdesc", modeltype: FwDataTypes.Text)]
        //public string ItemDescription { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        //public string TrackedBy { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        //public string AvailFor { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "replacementcost", modeltype: FwDataTypes.Decimal)]
        //public decimal? ReplacementCost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        //public string Classification { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "statustype", modeltype: FwDataTypes.Text)]
        //public string StatusType { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalstatus", modeltype: FwDataTypes.Text)]
        public string InventoryStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rentalstatusid", modeltype: FwDataTypes.Text)]
        //public string InventoryStatusId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "textcolor", modeltype: FwDataTypes.Boolean)]
        public bool? TextColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "barcodeforscanning", modeltype: FwDataTypes.Text)]
        //public string BarCodeForScanning { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string RfId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "mfgpartno", modeltype: FwDataTypes.Text)]
        //public string ManufacturerPartNumber { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "mfgdate", modeltype: FwDataTypes.Date)]
        //public string ManufactureDate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "trackedbycode", modeltype: FwDataTypes.Text)]
        //public string TrackedByCode { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentlocation", modeltype: FwDataTypes.Text)]
        public string CurrentLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        //public string InventoryTypeId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        //public string InventoryType { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "inventorydepartmentorderby", modeltype: FwDataTypes.Integer)]
        //public int? InventoryTypeOrderBy { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        //public string CategoryId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        //public string Category { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "categoryorderby", modeltype: FwDataTypes.Decimal)]
        //public decimal? CategoryOrderBy { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        //public string SubCategoryId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        //public string SubCategory { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "subcategoryorderby", modeltype: FwDataTypes.Decimal)]
        //public decimal? SubCategoryOrderBy { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ownership", modeltype: FwDataTypes.Text)]
        //public string Ownership { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean)]
        //public bool? QcRequired { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("InventoryId", "masterid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);


            if ((request != null) && (request.activeview != null))
            {
                if (request.activeview.Contains("WarehouseId="))
                {
                    string whId = request.activeview.Replace("WarehouseId=", "");
                    if (!whId.Equals("ALL"))
                    {
                        select.AddWhere("(warehouseid = @whid)");
                        select.AddParameter("@whid", whId);
                    }
                }
            }

        }
        //------------------------------------------------------------------------------------ 
    }
}