using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.HomeControls.ItemQc
{
    [FwSqlTable("rentalitemqcview")]
    public class ItemQcLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemqcid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ItemQcId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcrequiredasof", modeltype: FwDataTypes.UTCDateTime)]
        public string QcRequiredAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcbyusersid", modeltype: FwDataTypes.Text)]
        public string QcByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcbyuser", modeltype: FwDataTypes.Text)]
        public string QcByUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcdatetime", modeltype: FwDataTypes.UTCDateTime)]
        public string QcDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastorderid", modeltype: FwDataTypes.Text)]
        public string LastOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastorderno", modeltype: FwDataTypes.Text)]
        public string LastOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastorderdesc", modeltype: FwDataTypes.Text)]
        public string LastOrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastdealid", modeltype: FwDataTypes.Text)]
        public string LastDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastdealno", modeltype: FwDataTypes.Text)]
        public string LastDealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastdeal", modeltype: FwDataTypes.Text)]
        public string LastDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conditionid", modeltype: FwDataTypes.Text)]
        public string ConditionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "incontractid", modeltype: FwDataTypes.Text)]
        public string InContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "condition", modeltype: FwDataTypes.Text)]
        public string Condition { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "note", modeltype: FwDataTypes.Text)]
        public string Note { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasimage", modeltype: FwDataTypes.Boolean)]
        public bool? HasImage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appimageid", modeltype: FwDataTypes.Text)]
        public string QcAppImageId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datstamp", modeltype: FwDataTypes.UTCDateTime)]
        public string Datstamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("ItemId", "rentalitemid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}