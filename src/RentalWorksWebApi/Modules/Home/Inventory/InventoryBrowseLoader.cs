using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using WebApi.Modules.Home.Master;
using System.Collections.Generic;

namespace WebApi.Modules.Home.Inventory
{
    [FwSqlTable("inventoryview")]
    public class InventoryBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classdesc", modeltype: FwDataTypes.Text)]
        public string ClassificationDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ClassificationColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 

            select.AddWhere("(availfor='" + AvailFor + "')");


            addFilterToSelect("TrackedBy", "trackedby", select, request);
            addFilterToSelect("Classification", "class", select, request);
            addFilterToSelect("InventoryTypeId", "inventorydepartmentid", select, request);
            addFilterToSelect("CategoryId", "categoryid", select, request);
            addFilterToSelect("SubCategoryId", "subcategoryid", select, request);

            if ((request != null) && (request.activeview != null))
            {
                switch (request.activeview)
                {
                    case "ITEM":
                        select.AddWhere("(class = @classification)");
                        select.AddParameter("@classification", "I");
                        break;
                    case "ACCESSORY":
                        select.AddWhere("(class = @classification)");
                        select.AddParameter("@classification", "A");
                        break;
                    case "COMPLETE":
                        select.AddWhere("(class = @classification)");
                        select.AddParameter("@classification", "C");
                        break;
                    case "KIT":
                        select.AddWhere("(class = @classification)");
                        select.AddParameter("@classification", "K");
                        break;
                    case "KITSET":
                        select.AddWhere("(class = @classification)");
                        select.AddParameter("@classification", "K");
                        break;
                    case "MISC":
                        select.AddWhere("(class = @classification)");
                        select.AddParameter("@classification", "M");
                        break;
                    case "CONTAINER":
                        select.AddWhere("(class = @classification)");
                        select.AddParameter("@classification", "N");
                        break;
                    case "ALL":
                        break;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}