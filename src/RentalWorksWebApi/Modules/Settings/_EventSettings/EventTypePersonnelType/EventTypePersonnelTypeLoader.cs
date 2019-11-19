using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.EventSettings.EventTypePersonnelType
{
    [FwSqlTable("ordertypepersonneltypeview")]
    public class EventTypePersonnelTypeLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypepersonneltypeid", isPrimaryKey: true)]
        public string EventTypePersonnelTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string EventTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "personneltypeid", modeltype: FwDataTypes.Text)]
        public string PersonnelTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "personneltype", modeltype: FwDataTypes.Text)]
        public string PersonnelType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "personneltyperename", modeltype: FwDataTypes.Text)]
        public string PersonnelTypeRename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showofficephone", modeltype: FwDataTypes.Boolean)]
        public bool? ShowOfficePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showofficeext", modeltype: FwDataTypes.Boolean)]
        public bool? ShowOfficeExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showcellular", modeltype: FwDataTypes.Boolean)]
        public bool? ShowCellular { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("EventTypeId", "ordertypeid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}