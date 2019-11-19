using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.HomeControls.InventoryPrep
{
    [FwSqlTable("masterprepview")]
    public class InventoryPrepLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterprepid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventoryPrepId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, required: true)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preprateid", modeltype: FwDataTypes.Text, required: true)]
        public string PrepRateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prepmasterno", modeltype: FwDataTypes.Text)]
        public string PrepICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prepdescription", modeltype: FwDataTypes.Text)]
        public string PrepDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prepunit", modeltype: FwDataTypes.Text)]
        public string PrepUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prepunittype", modeltype: FwDataTypes.Text)]
        public string PrepUnitType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsDefault { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preprate", modeltype: FwDataTypes.Decimal)]
        public decimal? PrepRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preptime", modeltype: FwDataTypes.Text)]
        public string PrepTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prepextended", modeltype: FwDataTypes.Decimal)]
        public decimal? PrepExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QtyOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.Decimal)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Boolean)]
        public bool? OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Boolean)]
        public bool? MasterItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("InventoryId", "masterid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}