using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Home.Master
{
    [FwSqlTable("inventoryview")]
    public class MasterLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classdesc", modeltype: FwDataTypes.Text)]
        public string ClassificationDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ClassificationColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text)]
        public string UnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unittype", modeltype: FwDataTypes.Text)]
        public string UnitType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nodiscount", modeltype: FwDataTypes.Boolean)]
        public bool? NonDiscountable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "overrideprofitlosscategory", modeltype: FwDataTypes.Boolean)]
        public bool? OverrideProfitAndLossCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "profitlosscategoryid", modeltype: FwDataTypes.Text)]
        public string ProfitAndLossCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "profitlosscategory", modeltype: FwDataTypes.Text)]
        public string ProfitAndLossCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "copynotes", modeltype: FwDataTypes.Boolean)]
        public bool? AutoCopyNotesToQuoteOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "note", modeltype: FwDataTypes.Text)]
        public string Note { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractinprint", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNoteOnInContract { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractoutprint", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNoteOnOutContract { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractreceiveprint", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNoteOnReceiveContract { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractreturnprint", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNoteOnReturnContract { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceprint", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNoteOnInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderprint", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNoteOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picklistprint", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNoteOnPickList { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poprint", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNoteOnPO { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quoteprint", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNoteOnQuote { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returnlistprint", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNoteOnReturnList { get; set; }
        //------------------------------------------------------------------------------------                 
        [FwSqlDataField(column: "poreceivelistprint", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNoteOnPoReceiveList { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poreturnlistprint", modeltype: FwDataTypes.Boolean)]
        public bool? PrintNoteOnPoReturnList { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 

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