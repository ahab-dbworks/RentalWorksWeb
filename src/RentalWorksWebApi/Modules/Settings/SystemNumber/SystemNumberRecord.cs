using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.SystemNumber
{
    [FwSqlTable("locationcounter")]
    public class SystemNumberRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationcounterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string SystemNumberId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "module", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 60)]
        public string Module { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignbyuser", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsAssignByUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "counter", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Counter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "increment", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Increment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("LocationId", "locationid", select, request);
            addFilterToSelect("OfficeLocationId", "locationid", select, request);
        }
        //------------------------------------------------------------------------------------     
    }
}
