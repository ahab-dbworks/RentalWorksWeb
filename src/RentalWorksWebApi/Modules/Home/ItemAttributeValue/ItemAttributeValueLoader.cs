using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Home.ItemAttributeValue
{
    [FwSqlTable("itemattributeview")]
    public class ItemAttributeValueLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemattributeid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ItemAttributeValueId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attributeid", modeltype: FwDataTypes.Text)]
        public string AttributeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attribute", modeltype: FwDataTypes.Text)]
        public string Attribute { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attributevalueid", modeltype: FwDataTypes.Text)]
        public string AttributeValueId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attributevalue", modeltype: FwDataTypes.Text)]
        public string AttributeValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "numericvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? NumericValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "numericonly", modeltype: FwDataTypes.Boolean)]
        public bool? NumericOnly { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("ItemId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}